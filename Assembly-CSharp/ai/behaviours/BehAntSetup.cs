using System;

namespace ai.behaviours
{
	// Token: 0x02000911 RID: 2321
	public class BehAntSetup : BehaviourActionActor
	{
		// Token: 0x060045AA RID: 17834 RVA: 0x001D3294 File Offset: 0x001D1494
		public override BehResult execute(Actor pActor)
		{
			string tTileType;
			pActor.data.get("tile_type1", out tTileType, null);
			if (string.IsNullOrEmpty(tTileType))
			{
				WorldTile current_tile = pActor.current_tile;
				string pExclude;
				if (current_tile == null)
				{
					pExclude = null;
				}
				else
				{
					TileTypeBase type = current_tile.Type;
					pExclude = ((type != null) ? type.id : null);
				}
				tTileType = BehAntSetup.getRandomTileType(pExclude);
				string tTileType2 = BehAntSetup.getRandomTileType(tTileType);
				pActor.data.set("tile_type1", tTileType);
				pActor.data.set("tile_type2", tTileType2);
			}
			return BehResult.Continue;
		}

		// Token: 0x060045AB RID: 17835 RVA: 0x001D330C File Offset: 0x001D150C
		public static string getRandomTileType(string pExclude = null)
		{
			string tResult = BehAntSetup._ant_tile_types.GetRandom<string>();
			while (tResult == pExclude)
			{
				tResult = BehAntSetup._ant_tile_types.GetRandom<string>();
			}
			return tResult;
		}

		// Token: 0x040031BD RID: 12733
		private static string[] _ant_tile_types = new string[]
		{
			"deep_ocean",
			"close_ocean",
			"shallow_waters",
			"sand",
			"soil_low",
			"soil_high",
			"hills",
			"mountains"
		};
	}
}
