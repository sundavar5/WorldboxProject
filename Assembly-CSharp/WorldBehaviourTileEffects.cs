using System;
using System.Collections.Generic;

// Token: 0x0200030F RID: 783
public class WorldBehaviourTileEffects
{
	// Token: 0x06001D75 RID: 7541 RVA: 0x001071C0 File Offset: 0x001053C0
	public static void tryToStartTileEffects()
	{
		for (int i = 0; i < 5; i++)
		{
			WorldBehaviourTileEffects.spawnEffect();
		}
	}

	// Token: 0x06001D76 RID: 7542 RVA: 0x001071E0 File Offset: 0x001053E0
	public static void spawnEffect()
	{
		if (TrailerMonolith.enable_trailer_stuff)
		{
			return;
		}
		if (!World.world.zone_camera.hasVisibleZones())
		{
			return;
		}
		if (World.world.stack_effects.controller_tile_effects.isLimitReached())
		{
			return;
		}
		WorldTile tTileForEffect = World.world.zone_camera.getVisibleZones().GetRandom<TileZone>().getRandomTile();
		TileEffectAsset tEffectAsset = TileEffectsLibrary.getRandomEffect(tTileForEffect);
		if (tEffectAsset == null)
		{
			return;
		}
		if (!Randy.randomChance(tEffectAsset.chance))
		{
			return;
		}
		foreach (WorldTile tTile in tTileForEffect.neighboursAll)
		{
			if (!tEffectAsset.tile_types.Contains(tTile.Type.id))
			{
				return;
			}
		}
		TileEffect tTileEffect = EffectsLibrary.spawn("fx_tile_effect", tTileForEffect, null, null, 0f, -1f, -1f, null) as TileEffect;
		if (tTileEffect == null)
		{
			return;
		}
		tTileEffect.load(tEffectAsset);
	}

	// Token: 0x06001D77 RID: 7543 RVA: 0x001072C0 File Offset: 0x001054C0
	public static void checkTileForEffectKill(WorldTile pTile, int pRadius)
	{
		BaseEffectController tController = World.world.stack_effects.controller_tile_effects;
		List<BaseEffect> tList = tController.getList();
		for (int i = 0; i < tList.Count; i++)
		{
			BaseEffect tEffect = tList[i];
			if (tEffect.active && Toolbox.Dist(tEffect.transform.position.x, tEffect.transform.position.y, (float)pTile.pos.x, (float)pTile.pos.y) <= (float)pRadius)
			{
				tController.killObject(tEffect);
				return;
			}
		}
	}
}
