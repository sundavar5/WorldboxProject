using System;
using System.Collections.Generic;

// Token: 0x02000406 RID: 1030
public class Heat
{
	// Token: 0x06002395 RID: 9109 RVA: 0x00128070 File Offset: 0x00126270
	internal void clear()
	{
		if (this.tiles == null)
		{
			this.tiles = new HashSetWorldTile();
		}
		foreach (WorldTile worldTile in this.tiles)
		{
			worldTile.heat = 0;
		}
		this.tiles.Clear();
	}

	// Token: 0x06002396 RID: 9110 RVA: 0x001280E0 File Offset: 0x001262E0
	internal void addTile(WorldTile pTile, int pHeat = 1)
	{
		if (pTile.heat == 0)
		{
			this.tiles.Add(pTile);
		}
		pTile.heat += pHeat;
		if (pTile.heat > 404)
		{
			pTile.heat = 404;
		}
		if (pTile.heat > 5)
		{
			if (pTile.hasBuilding() && pTile.building.isAlive())
			{
				pTile.building.getHit(0f, true, AttackType.Other, null, true, false, true);
			}
			if (pTile.Type.layer_type == TileLayerType.Ocean)
			{
				MapAction.removeLiquid(pTile);
				World.world.particles_smoke.spawn(pTile.posV3);
			}
			if (pTile.isTemporaryFrozen())
			{
				pTile.unfreeze(1);
			}
			if (pTile.Type.grey_goo)
			{
				pTile.startFire(false);
			}
			pTile.setBurned(-1);
		}
		if (pTile.heat > 10)
		{
			if (pTile.Type.burnable)
			{
				if (pTile.Type.IsType("tnt") || pTile.Type.IsType("tnt_timed"))
				{
					AchievementLibrary.tnt_and_heat.check(null);
				}
				pTile.startFire(false);
			}
			if (pTile.hasBuilding() && !pTile.building.isRuin())
			{
				pTile.building.getHit((float)pTile.building.getMaxHealth() * 0.1f, true, AttackType.Divine, null, false, false, true);
			}
			if (pTile.hasBuilding())
			{
				pTile.startFire(false);
			}
			pTile.doUnits(delegate(Actor tActor)
			{
				if (tActor.asset.very_high_flyer)
				{
					return;
				}
				if (tActor.isImmuneToFire())
				{
					return;
				}
				ActionLibrary.addBurningEffectOnTarget(null, tActor, null);
				tActor.getHit(50f, true, AttackType.Fire, null, false, false, true);
			});
		}
		if (pTile.heat > 20)
		{
			if (pTile.Type.explodable && pTile.explosion_wave == 0)
			{
				World.world.explosion_layer.explodeBomb(pTile, false);
			}
			if (pTile.hasBuilding())
			{
				pTile.startFire(false);
			}
			if (pTile.top_type != null)
			{
				MapAction.decreaseTile(pTile, true, "flash");
			}
		}
		if (WorldLawLibrary.world_law_gaias_covenant.isEnabled())
		{
			return;
		}
		if (pTile.heat > 30)
		{
			if (pTile.Type.lava)
			{
				LavaHelper.addLava(pTile, "lava3");
			}
			if (pTile.Type.IsType("soil_low") || pTile.Type.IsType("soil_high"))
			{
				pTile.setTileType("sand");
			}
			if (pTile.Type.road)
			{
				pTile.setTileType("sand");
			}
		}
		if (pTile.heat > 100 && pTile.Type.IsType("sand"))
		{
			LavaHelper.addLava(pTile, "lava3");
		}
		if (pTile.heat > 160)
		{
			if (pTile.Type.IsType("mountains"))
			{
				LavaHelper.addLava(pTile, "lava3");
			}
			if (pTile.Type.IsType("hills"))
			{
				LavaHelper.addLava(pTile, "lava3");
			}
		}
	}

	// Token: 0x06002397 RID: 9111 RVA: 0x001283A8 File Offset: 0x001265A8
	internal void update(float pElapsed)
	{
		if (World.world.isPaused())
		{
			return;
		}
		if (this.tiles.Count == 0)
		{
			return;
		}
		if (this.tickTimer > 0f)
		{
			this.tickTimer -= pElapsed;
			return;
		}
		this.tickTimer = 1f;
		this.tilesToRemove.Clear();
		foreach (WorldTile tTile in this.tiles)
		{
			tTile.heat--;
			if (tTile.heat <= 0)
			{
				this.tilesToRemove.Add(tTile);
			}
		}
		for (int i = 0; i < this.tilesToRemove.Count; i++)
		{
			WorldTile tTile2 = this.tilesToRemove[i];
			this.tiles.Remove(tTile2);
		}
		this.tilesToRemove.Clear();
	}

	// Token: 0x040019B5 RID: 6581
	public const int MAX_HEAT = 404;

	// Token: 0x040019B6 RID: 6582
	private float tickTimer;

	// Token: 0x040019B7 RID: 6583
	private List<WorldTile> tilesToRemove = new List<WorldTile>();

	// Token: 0x040019B8 RID: 6584
	private HashSetWorldTile tiles;
}
