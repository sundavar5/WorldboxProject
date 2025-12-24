using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020008FF RID: 2303
	public class BehSpawnTreeFertilizer : BehaviourActionActor
	{
		// Token: 0x06004578 RID: 17784 RVA: 0x001D24F0 File Offset: 0x001D06F0
		public override BehResult execute(Actor pActor)
		{
			if (!Randy.randomChance(0.3f))
			{
				return BehResult.Stop;
			}
			if (!pActor.current_tile.Type.ground)
			{
				return BehResult.Stop;
			}
			BiomeAsset tBiomeAsset = pActor.current_tile.Type.biome_asset;
			if (tBiomeAsset == null)
			{
				return BehResult.Stop;
			}
			if (tBiomeAsset.grow_vegetation_auto)
			{
				return BehResult.Stop;
			}
			SpellAsset tSpellAsset = AssetManager.spells.get("spawn_vegetation");
			BehSpawnTreeFertilizer._tiles.Clear();
			foreach (WorldTile tTile in pActor.current_tile.region.tiles)
			{
				if (!(tTile.Type.biome_id == "biome_grass"))
				{
					BiomeAsset iBiomeAsset = tTile.Type.biome_asset;
					if (iBiomeAsset != null && iBiomeAsset.grow_vegetation_auto)
					{
						BehSpawnTreeFertilizer._tiles.Add(tTile);
					}
				}
			}
			if (BehSpawnTreeFertilizer._tiles.Count == 0)
			{
				return BehResult.Stop;
			}
			AttackAction action = tSpellAsset.action;
			if (action != null)
			{
				action(pActor, pActor, BehSpawnTreeFertilizer._tiles.GetRandom<WorldTile>());
			}
			pActor.doCastAnimation();
			return BehResult.Continue;
		}

		// Token: 0x040031B5 RID: 12725
		private static List<WorldTile> _tiles = new List<WorldTile>();
	}
}
