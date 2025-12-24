using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200033D RID: 829
public class ExplosionsEffects : MapLayer
{
	// Token: 0x06002000 RID: 8192 RVA: 0x0011342C File Offset: 0x0011162C
	internal override void create()
	{
		this.colorValues = new Color(1f, 1f, 1f, 1f);
		this.colors_amount = 60;
		this.explosionQueue = new List<WorldTile>();
		this.explosionQueueCurrent = new List<WorldTile>();
		this.explosionDict = new Dictionary<WorldTile, TileTypeBase>();
		this.explosionDictCurrent = new Dictionary<WorldTile, TileTypeBase>();
		this.hashsetTiles = new HashSetWorldTile();
		base.create();
	}

	// Token: 0x06002001 RID: 8193 RVA: 0x001134A0 File Offset: 0x001116A0
	internal override void clear()
	{
		this.explosionQueue.Clear();
		this.explosionQueueCurrent.Clear();
		this.explosionDict.Clear();
		this.explosionDictCurrent.Clear();
		this.hashsetTiles.Clear();
		this.timedBombs.Clear();
		this.delayedBombs.Clear();
		this.nextWave.Clear();
		this.hashset_bombs.Clear();
		base.clear();
	}

	// Token: 0x06002002 RID: 8194 RVA: 0x00113516 File Offset: 0x00111716
	internal void activateDelayedBomb(WorldTile pBomb)
	{
		if (!this.delayedBombs.Contains(pBomb))
		{
			this.delayedBombs.Add(pBomb);
			pBomb.delayed_bomb_type = pBomb.Type.id;
			pBomb.delayed_timer_bomb = 0.09f;
		}
	}

	// Token: 0x06002003 RID: 8195 RVA: 0x0011354E File Offset: 0x0011174E
	internal void addTimedTnt(WorldTile pTile)
	{
		if (this.timedBombs.Contains(pTile))
		{
			return;
		}
		pTile.delayed_timer_bomb = 5f;
		this.timedBombs.Add(pTile);
	}

	// Token: 0x06002004 RID: 8196 RVA: 0x00113578 File Offset: 0x00111778
	internal void explodeBomb(WorldTile pBombTile, bool pForce = false)
	{
		if (this.hashset_bombs.Contains(pBombTile))
		{
			return;
		}
		if (pBombTile.Type.explodable_delayed && !pForce)
		{
			this.activateDelayedBomb(pBombTile);
			return;
		}
		World.world.startShake(0.3f, 0.01f, 2f, false, true);
		this.nextWave.Enqueue(pBombTile);
		while (this.nextWave.Count > 0)
		{
			WorldTile tTile = this.nextWave.Dequeue();
			this.hashset_bombs.Add(tTile);
			if (tTile.Type.explodable && !tTile.Type.explodable_delayed)
			{
				tTile.explosion_wave = tTile.Type.explode_range;
				tTile.explosion_power = tTile.Type.explode_range;
			}
			if (tTile.explosion_wave > 0)
			{
				for (int i = 0; i < tTile.neighbours.Length; i++)
				{
					WorldTile tNeighbour = tTile.neighbours[i];
					if (tNeighbour.explosion_wave > 0 && this.hashset_bombs.Contains(tNeighbour))
					{
						if (tNeighbour.explosion_wave < tTile.explosion_wave && !tNeighbour.Type.explodable)
						{
						}
					}
					else
					{
						this.hashset_bombs.Add(tNeighbour);
						tNeighbour.explosion_wave = tTile.explosion_wave - 1;
						tNeighbour.explosion_power = tTile.explosion_power;
						this.nextWave.Enqueue(tNeighbour);
					}
				}
			}
		}
		if (this.hashset_bombs.Count < 20)
		{
			MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionSmall", pBombTile, false, false);
		}
		else
		{
			MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionMiddle", pBombTile, false, false);
		}
		using (ListPool<WorldTile> tTempTiles = new ListPool<WorldTile>(this.hashset_bombs))
		{
			foreach (WorldTile ptr in tTempTiles)
			{
				WorldTile tTile2 = ptr;
				MapAction.explodeTile(tTile2, (float)((tTile2.explosion_power - tTile2.explosion_wave) * 10), (float)(tTile2.explosion_power * 10), pBombTile, AssetManager.terraform.get("bomb"));
			}
		}
	}

	// Token: 0x06002005 RID: 8197 RVA: 0x00113790 File Offset: 0x00111990
	public void prepareNewExplosion(WorldTile pTile)
	{
		if (!this.explosionDict.ContainsKey(pTile))
		{
			this.explosionQueue.Add(pTile);
			this.explosionDict.Add(pTile, pTile.Type);
		}
	}

	// Token: 0x06002006 RID: 8198 RVA: 0x001137C0 File Offset: 0x001119C0
	private void updateExplosionQueue()
	{
		if (this.timerExplosionQueue > 0f)
		{
			this.timerExplosionQueue -= World.world.elapsed;
			return;
		}
		this.timerExplosionQueue = 0.1f;
		if (this.explosionQueue.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this.explosionQueue.Count; i++)
		{
			WorldTile tTile = this.explosionQueue[i];
			this.explosionQueueCurrent.Add(tTile);
			this.explosionDictCurrent.Add(tTile, this.explosionDict[tTile]);
		}
		this.explosionQueue.Clear();
		this.explosionDict.Clear();
		for (int j = 0; j < this.explosionQueueCurrent.Count; j++)
		{
			WorldTile tTile2 = this.explosionQueueCurrent[j];
			MapAction.damageWorld(tTile2, this.explosionDictCurrent[tTile2].explode_range, AssetManager.terraform.get("bomb"), null);
			MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionMiddle", tTile2, false, false);
		}
		this.explosionQueueCurrent.Clear();
		this.explosionDictCurrent.Clear();
	}

	// Token: 0x06002007 RID: 8199 RVA: 0x001138D8 File Offset: 0x00111AD8
	public override void update(float pElapsed)
	{
		this.checkAutoDisable();
		if (this.timedBombs.Count > 0 && !World.world.isPaused())
		{
			int i = 0;
			while (i < this.timedBombs.Count)
			{
				WorldTile tTile = this.timedBombs[i];
				if (tTile.delayed_timer_bomb > 0f)
				{
					tTile.delayed_timer_bomb -= pElapsed;
					i++;
				}
				else
				{
					this.timedBombs.RemoveAt(i);
					if (tTile.Type.explodable_timed)
					{
						MapAction.damageWorld(tTile, tTile.Type.explode_range, AssetManager.terraform.get("bomb"), null);
						MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionMiddle", tTile, false, false);
					}
				}
			}
		}
	}

	// Token: 0x06002008 RID: 8200 RVA: 0x00113995 File Offset: 0x00111B95
	public override void draw(float pElapsed)
	{
		if (this.sprRnd || this.delayedBombs.Count > 0)
		{
			this.UpdateDirty(pElapsed);
		}
	}

	// Token: 0x06002009 RID: 8201 RVA: 0x001139BC File Offset: 0x00111BBC
	protected override void UpdateDirty(float pElapsed)
	{
		if (this.delayedBombs.Count > 0)
		{
			int i = 0;
			while (i < this.delayedBombs.Count)
			{
				WorldTile tBomb = this.delayedBombs[i];
				tBomb.delayed_timer_bomb -= World.world.elapsed;
				if (tBomb.delayed_timer_bomb <= 0f)
				{
					tBomb.delayed_timer_bomb = -100f;
					this.delayedBombs.Remove(tBomb);
					TileTypeBase tType;
					if (!string.IsNullOrEmpty(tBomb.delayed_bomb_type))
					{
						tType = AssetManager.top_tiles.get(tBomb.delayed_bomb_type);
					}
					else
					{
						tType = TopTileLibrary.tnt_timed;
					}
					MapAction.damageWorld(tBomb, tType.explode_range, AssetManager.terraform.get("bomb"), null);
					MusicBox.playSound("event:/SFX/EXPLOSIONS/ExplosionMiddle", tBomb, false, false);
				}
				else
				{
					i++;
				}
			}
		}
		if (this.hashset_bombs.Count > 0)
		{
			foreach (WorldTile worldTile in this.hashset_bombs)
			{
				worldTile.explosion_wave = 0;
				worldTile.explosion_power = 0;
			}
			this.hashset_bombs.Clear();
		}
		if (this.timer > 0f)
		{
			this.timer -= World.world.elapsed;
			return;
		}
		this.timer = this.interval;
		if (this.hashsetTiles.Count == 0)
		{
			return;
		}
		using (ListPool<WorldTile> tilesToRemove = new ListPool<WorldTile>())
		{
			foreach (WorldTile tTile in this.hashsetTiles)
			{
				if (tTile.explosion_fx_stage > 0)
				{
					if (Randy.randomBool())
					{
						this.pixels[tTile.data.tile_id] = Toolbox.clear;
					}
					else
					{
						this.pixels[tTile.data.tile_id] = this.colors[tTile.explosion_fx_stage - 1];
					}
					tTile.explosion_fx_stage--;
					if (tTile.explosion_fx_stage <= 0)
					{
						tTile.explosion_fx_stage = 0;
						tilesToRemove.Add(tTile);
					}
				}
			}
			if (tilesToRemove.Count > 0)
			{
				for (int j = 0; j < tilesToRemove.Count; j++)
				{
					WorldTile tTile2 = tilesToRemove[j];
					this.hashsetTiles.Remove(tTile2);
				}
			}
			base.updatePixels();
		}
	}

	// Token: 0x0600200A RID: 8202 RVA: 0x00113C58 File Offset: 0x00111E58
	internal void setDirty(WorldTile pTile, float pDist, float pRadius)
	{
		int newVal = (int)(60f * (1f - pDist / pRadius));
		if (newVal == 0)
		{
			return;
		}
		if (newVal < pTile.explosion_fx_stage)
		{
			return;
		}
		this.hashsetTiles.Add(pTile);
		pTile.explosion_fx_stage = newVal;
	}

	// Token: 0x0400174D RID: 5965
	private Dictionary<WorldTile, TileTypeBase> explosionDict;

	// Token: 0x0400174E RID: 5966
	private Dictionary<WorldTile, TileTypeBase> explosionDictCurrent;

	// Token: 0x0400174F RID: 5967
	public List<WorldTile> explosionQueue;

	// Token: 0x04001750 RID: 5968
	private List<WorldTile> explosionQueueCurrent;

	// Token: 0x04001751 RID: 5969
	private float timerExplosionQueue;

	// Token: 0x04001752 RID: 5970
	public float interval = 0.01f;

	// Token: 0x04001753 RID: 5971
	internal Queue<WorldTile> nextWave = new Queue<WorldTile>();

	// Token: 0x04001754 RID: 5972
	internal HashSetWorldTile hashset_bombs = new HashSetWorldTile();

	// Token: 0x04001755 RID: 5973
	internal List<WorldTile> delayedBombs = new List<WorldTile>();

	// Token: 0x04001756 RID: 5974
	internal List<WorldTile> timedBombs = new List<WorldTile>();
}
