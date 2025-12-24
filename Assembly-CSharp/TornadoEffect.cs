using System;
using System.Collections.Generic;
using UnityEngine;
using UnityPools;

// Token: 0x02000354 RID: 852
public class TornadoEffect : BaseEffect
{
	// Token: 0x0600208D RID: 8333 RVA: 0x00116B90 File Offset: 0x00114D90
	internal override void prepare(WorldTile pTile, float pScale = 0.5f)
	{
		base.prepare(pTile, pScale);
		this.current_tile = World.world.GetTileSimple((int)base.transform.localPosition.x, (int)base.transform.localPosition.y);
		this._target_tile = Toolbox.getRandomTileWithinDistance(this.current_tile, 5);
		base.setScale(0.11000001f);
		this._target_scale = pScale;
		this._tornado_timer_force = 0.1f;
		this._tornado_timer_terraform = 0.3f;
		this._shrink_timer = 10f;
		this.addTornadoToTile();
	}

	// Token: 0x0600208E RID: 8334 RVA: 0x00116C24 File Offset: 0x00114E24
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.updateChangeScale(pElapsed);
		if (World.world.isPaused())
		{
			return;
		}
		if (base.isKilled())
		{
			return;
		}
		this.updateColorEffect(pElapsed);
		if (this.state == 2)
		{
			this.deathAnimation(pElapsed);
			return;
		}
		this.updateMovement();
		this.updateShrinking(pElapsed);
		if (this._tornado_timer_force > 0f)
		{
			this._tornado_timer_force -= pElapsed;
		}
		else
		{
			this._tornado_timer_force = 0.1f;
			this.tornadoActionForce(this.current_tile);
		}
		if (this._tornado_timer_terraform > 0f)
		{
			this._tornado_timer_terraform -= pElapsed;
			return;
		}
		this._tornado_timer_terraform = 0.3f;
		this.tornadoActionTerraform(this.current_tile, this.scale);
	}

	// Token: 0x0600208F RID: 8335 RVA: 0x00116CE8 File Offset: 0x00114EE8
	private void updateMovement()
	{
		WorldTile tNewTile = World.world.GetTileSimple((int)base.transform.localPosition.x, (int)base.transform.localPosition.y);
		if (tNewTile != this.current_tile)
		{
			this.removeTornadoFromTile();
			this.current_tile = tNewTile;
			this.addTornadoToTile();
		}
		if (this.current_tile == this._target_tile)
		{
			this._target_tile = Toolbox.getRandomTileWithinDistance(this.current_tile, 5);
		}
		Vector3 tDirection = (this._target_tile.posV3 - base.transform.localPosition).normalized;
		base.transform.localPosition += tDirection * 0.15f;
	}

	// Token: 0x06002090 RID: 8336 RVA: 0x00116DA3 File Offset: 0x00114FA3
	private void updateShrinking(float pElapsed)
	{
		if (this._shrink_timer > 0f)
		{
			this._shrink_timer -= pElapsed;
			return;
		}
		this._shrink_timer = Randy.randomFloat(7.5f, 12.5f);
		this.shrink();
	}

	// Token: 0x06002091 RID: 8337 RVA: 0x00116DDC File Offset: 0x00114FDC
	private void tornadoActionTerraform(WorldTile pTile, float pScale = 0.5f)
	{
		BrushData brush = Brush.get((int)(pScale * 6f), "circ_");
		bool tDamage = true;
		if (!MapAction.checkTileDamageGaiaCovenant(pTile, true))
		{
			tDamage = false;
		}
		for (int i = 0; i < brush.pos.Length; i++)
		{
			int tX = pTile.pos.x + brush.pos[i].x;
			int tY = pTile.pos.y + brush.pos[i].y;
			if (tX >= 0 && tX < MapBox.width && tY >= 0 && tY < MapBox.height)
			{
				WorldTile tTile = World.world.GetTileSimple(tX, tY);
				if (tTile.Type.ocean)
				{
					MapAction.removeLiquid(tTile);
					if (Randy.randomChance(0.15f))
					{
						TornadoEffect.spawnBurst(tTile, "rain", pScale);
					}
				}
				if (tDamage)
				{
					if (tTile.top_type != null || tTile.Type.life)
					{
						MapAction.decreaseTile(tTile, false, "flash");
					}
					if (tTile.Type.lava)
					{
						LavaHelper.removeLava(tTile);
						TornadoEffect.spawnBurst(tTile, "lava", pScale);
					}
				}
				if (tTile.hasBuilding() && tTile.building.asset.can_be_damaged_by_tornado)
				{
					tTile.building.getHit(1f, true, AttackType.Other, null, true, false, true);
				}
				if (tTile.isTemporaryFrozen())
				{
					tTile.unfreeze(10);
				}
				if (tTile.isOnFire())
				{
					tTile.stopFire();
				}
			}
		}
	}

	// Token: 0x06002092 RID: 8338 RVA: 0x00116F68 File Offset: 0x00115168
	private void tornadoActionForce(WorldTile pTile)
	{
		World.world.applyForceOnTile(pTile, 10, 3f, false, 0, null, null, null, false);
	}

	// Token: 0x06002093 RID: 8339 RVA: 0x00116F90 File Offset: 0x00115190
	private static void spawnBurst(WorldTile pTile, string pType, float pScale)
	{
		if (World.world.drop_manager.getActiveIndex() > 3000)
		{
			return;
		}
		World.world.drop_manager.spawnParabolicDrop(pTile, pType, 0f, 0.62f, 104f * pScale, 0.7f, 23.5f * pScale, -1f);
	}

	// Token: 0x06002094 RID: 8340 RVA: 0x00116FE8 File Offset: 0x001151E8
	internal void shrink()
	{
		if (base.isKilled())
		{
			return;
		}
		float tScale = this.scale * 0.5f;
		if (tScale < 0.1f)
		{
			this.die();
			return;
		}
		this.resizeTornado(tScale);
	}

	// Token: 0x06002095 RID: 8341 RVA: 0x00117024 File Offset: 0x00115224
	internal void grow()
	{
		if (base.isKilled())
		{
			return;
		}
		float tScale = Mathf.Min(this.scale * 1.5f, 5f);
		if (tScale >= 5f)
		{
			AchievementLibrary.tornado.check(null);
		}
		this.resizeTornado(tScale);
	}

	// Token: 0x06002096 RID: 8342 RVA: 0x0011706C File Offset: 0x0011526C
	internal bool split()
	{
		if (base.isKilled())
		{
			return false;
		}
		AchievementLibrary.baby_tornado.check(null);
		float tNextScale = this.scale * 0.5f;
		if (tNextScale < 0.1f)
		{
			this.die();
			return true;
		}
		EffectsLibrary.spawnAtTile("fx_tornado", this.current_tile, tNextScale);
		this.resizeTornado(tNextScale);
		return true;
	}

	// Token: 0x06002097 RID: 8343 RVA: 0x001170C6 File Offset: 0x001152C6
	internal void resizeTornado(float pScale)
	{
		this._target_scale = pScale;
	}

	// Token: 0x06002098 RID: 8344 RVA: 0x001170D0 File Offset: 0x001152D0
	public void startColorEffect(string pType = "red")
	{
		this.colorEffect = 0.3f;
		if (pType == "red")
		{
			this.colorMaterial = LibraryMaterials.instance.mat_damaged;
		}
		else if (pType == "white")
		{
			this.colorMaterial = LibraryMaterials.instance.mat_highlighted;
		}
		this.updateColorEffect(0f);
	}

	// Token: 0x06002099 RID: 8345 RVA: 0x00117130 File Offset: 0x00115330
	private void updateColorEffect(float pElapsed)
	{
		if (this.colorEffect == 0f)
		{
			return;
		}
		this.colorEffect -= pElapsed;
		if (this.colorEffect < 0f)
		{
			this.colorEffect = 0f;
		}
		float num = this.colorEffect;
	}

	// Token: 0x0600209A RID: 8346 RVA: 0x00117180 File Offset: 0x00115380
	internal static void growTornados(WorldTile pTile)
	{
		TornadoEffect.resizeOnTile(pTile, "grow");
		for (int i = 0; i < pTile.neighboursAll.Length; i++)
		{
			TornadoEffect.resizeOnTile(pTile.neighboursAll[i], "grow");
		}
	}

	// Token: 0x0600209B RID: 8347 RVA: 0x001171C0 File Offset: 0x001153C0
	internal static void shrinkTornados(WorldTile pTile)
	{
		TornadoEffect.resizeOnTile(pTile, "shrink");
		for (int i = 0; i < pTile.neighboursAll.Length; i++)
		{
			TornadoEffect.resizeOnTile(pTile.neighboursAll[i], "shrink");
		}
	}

	// Token: 0x0600209C RID: 8348 RVA: 0x00117200 File Offset: 0x00115400
	internal static void resizeOnTile(WorldTile pTile, string direction)
	{
		foreach (BaseEffect tEffect in World.world.stack_effects.get("fx_tornado").getList())
		{
			if (tEffect.active && tEffect.current_tile == pTile)
			{
				TornadoEffect tTornado = tEffect as TornadoEffect;
				if (direction == "grow")
				{
					tTornado.grow();
				}
				else
				{
					tTornado.shrink();
				}
			}
		}
	}

	// Token: 0x0600209D RID: 8349 RVA: 0x00117294 File Offset: 0x00115494
	private void deathAnimation(float pElapsed)
	{
		if (this.scale > 0f)
		{
			this.updateChangeScale(pElapsed);
			return;
		}
		this.kill();
	}

	// Token: 0x0600209E RID: 8350 RVA: 0x001172B1 File Offset: 0x001154B1
	public void die()
	{
		this.state = 2;
		this.resizeTornado(0f);
		this.removeTornadoFromTile();
	}

	// Token: 0x0600209F RID: 8351 RVA: 0x001172CC File Offset: 0x001154CC
	internal void updateChangeScale(float pElapsed)
	{
		if (this.scale == this._target_scale)
		{
			return;
		}
		if (this.scale < this._target_scale)
		{
			this.startColorEffect("red");
		}
		else
		{
			this.startColorEffect("white");
		}
		if (this.scale > this._target_scale)
		{
			base.setScale(this.scale - 0.2f * pElapsed);
			if (this.scale < this._target_scale)
			{
				base.setScale(this._target_scale);
			}
		}
		else
		{
			base.setScale(this.scale + 0.2f * pElapsed);
			if (this.scale > this._target_scale)
			{
				base.setScale(this._target_scale);
			}
		}
		if (this.scale <= 0.1f)
		{
			this.die();
		}
	}

	// Token: 0x060020A0 RID: 8352 RVA: 0x00117390 File Offset: 0x00115590
	private void addTornadoToTile()
	{
		HashSet<TornadoEffect> tEffects;
		if (!TornadoEffect._tornadoes_by_tiles.TryGetValue(this.current_tile, out tEffects))
		{
			tEffects = UnsafeCollectionPool<HashSet<TornadoEffect>, TornadoEffect>.Get();
			TornadoEffect._tornadoes_by_tiles.Add(this.current_tile, tEffects);
		}
		tEffects.Add(this);
	}

	// Token: 0x060020A1 RID: 8353 RVA: 0x001173D0 File Offset: 0x001155D0
	private void removeTornadoFromTile()
	{
		HashSet<TornadoEffect> tEffects;
		if (!TornadoEffect._tornadoes_by_tiles.TryGetValue(this.current_tile, out tEffects))
		{
			return;
		}
		tEffects.Remove(this);
		if (tEffects.Count == 0)
		{
			UnsafeCollectionPool<HashSet<TornadoEffect>, TornadoEffect>.Release(tEffects);
			TornadoEffect._tornadoes_by_tiles.Remove(this.current_tile);
		}
	}

	// Token: 0x060020A2 RID: 8354 RVA: 0x0011741C File Offset: 0x0011561C
	public static HashSet<TornadoEffect> getTornadoesFromTile(WorldTile pTile)
	{
		HashSet<TornadoEffect> tEffects;
		if (!TornadoEffect._tornadoes_by_tiles.TryGetValue(pTile, out tEffects))
		{
			return null;
		}
		return tEffects;
	}

	// Token: 0x060020A3 RID: 8355 RVA: 0x0011743C File Offset: 0x0011563C
	public static void Clear()
	{
		foreach (HashSet<TornadoEffect> toRelease in TornadoEffect._tornadoes_by_tiles.Values)
		{
			UnsafeCollectionPool<HashSet<TornadoEffect>, TornadoEffect>.Release(toRelease);
		}
		TornadoEffect._tornadoes_by_tiles.Clear();
	}

	// Token: 0x040017B4 RID: 6068
	private const float TORNADO_SCALE_DECREASE_SPLIT_MULTIPLIER = 0.5f;

	// Token: 0x040017B5 RID: 6069
	private const float TORNADO_SCALE_DECREASE_MULTIPLIER = 0.5f;

	// Token: 0x040017B6 RID: 6070
	private const float TORNADO_SCALE_INCREASE_MULTIPLIER = 1.5f;

	// Token: 0x040017B7 RID: 6071
	private const float TORNADO_SCALE_MIN = 0.1f;

	// Token: 0x040017B8 RID: 6072
	private const float TORNADO_SCALE_MAX = 5f;

	// Token: 0x040017B9 RID: 6073
	public const float TORNADO_SCALE_DEFAULT = 0.5f;

	// Token: 0x040017BA RID: 6074
	private const int RANDOM_TILE_RANGE = 5;

	// Token: 0x040017BB RID: 6075
	private const float ACTION_INTERVAL_FORCE = 0.1f;

	// Token: 0x040017BC RID: 6076
	private const float ACTION_INTERVAL_TERRAFORM = 0.3f;

	// Token: 0x040017BD RID: 6077
	private const float SHRINK_TIMER = 10f;

	// Token: 0x040017BE RID: 6078
	private const float MOVE_SPEED = 0.15f;

	// Token: 0x040017BF RID: 6079
	private float _tornado_timer_force;

	// Token: 0x040017C0 RID: 6080
	private float _tornado_timer_terraform;

	// Token: 0x040017C1 RID: 6081
	private float _shrink_timer;

	// Token: 0x040017C2 RID: 6082
	private float _target_scale = 0.5f;

	// Token: 0x040017C3 RID: 6083
	private WorldTile _target_tile;

	// Token: 0x040017C4 RID: 6084
	private static readonly Dictionary<WorldTile, HashSet<TornadoEffect>> _tornadoes_by_tiles = new Dictionary<WorldTile, HashSet<TornadoEffect>>();

	// Token: 0x040017C5 RID: 6085
	internal float colorEffect;

	// Token: 0x040017C6 RID: 6086
	internal Material colorMaterial;
}
