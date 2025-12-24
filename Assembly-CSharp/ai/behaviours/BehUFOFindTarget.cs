using System;
using UnityEngine;

namespace ai.behaviours
{
	// Token: 0x02000965 RID: 2405
	public class BehUFOFindTarget : BehaviourActionActor
	{
		// Token: 0x06004684 RID: 18052 RVA: 0x001DE838 File Offset: 0x001DCA38
		public unsafe override BehResult execute(Actor pActor)
		{
			pActor.beh_tile_target = null;
			int attacksForCity;
			pActor.data.get("attacksForCity", out attacksForCity, 0);
			long cityToAttack;
			pActor.data.get("cityToAttack", out cityToAttack, -1L);
			City tCityToAttack = cityToAttack.hasValue() ? BehaviourActionBase<Actor>.world.cities.get(cityToAttack) : null;
			if (attacksForCity > 0 && tCityToAttack != null)
			{
				if (!tCityToAttack.isAlive() || tCityToAttack.buildings.Count == 0)
				{
					pActor.beh_tile_target = null;
					attacksForCity = 0;
					pActor.data.removeLong("cityToAttack");
				}
				else
				{
					Building tBuilding = tCityToAttack.buildings.GetRandom<Building>();
					pActor.beh_tile_target = tBuilding.current_tile.zone.tiles.GetRandom<WorldTile>();
					attacksForCity--;
				}
			}
			else if (attacksForCity <= 0)
			{
				pActor.beh_tile_target = null;
				pActor.data.removeLong("cityToAttack");
			}
			if (attacksForCity > 0)
			{
				pActor.data.set("attacksForCity", attacksForCity);
			}
			else
			{
				pActor.data.removeInt("attacksForCity");
			}
			if (pActor.beh_tile_target == null)
			{
				WorldTile tTile = Toolbox.getRandomTileWithinDistance(pActor.current_tile, 100);
				if (!BehaviourActionBase<Actor>.world.islands_calculator.hasGround())
				{
					pActor.beh_tile_target = tTile;
					return BehResult.Continue;
				}
				int tTries = 5;
				while (!tTile.Type.ground && tTries > 0)
				{
					tTile = Toolbox.getRandomTileWithinDistance(pActor.current_tile, 100);
					tTries--;
				}
				if (!tTile.Type.ground && BehaviourActionBase<Actor>.world.islands_calculator.getRandomIslandGround(true) != null)
				{
					Span<Vector2Int> tTiles = new Span<Vector2Int>(stackalloc byte[checked(unchecked((UIntPtr)8) * (UIntPtr)sizeof(Vector2Int))], 8);
					for (int i = 0; i < 8; i++)
					{
						*tTiles[i] = BehaviourActionBase<Actor>.world.islands_calculator.tryGetRandomGround().pos;
					}
					Vector2Int tTilePos = Toolbox.getClosestTile(tTiles, pActor.current_tile);
					tTile = BehaviourActionBase<Actor>.world.GetTileSimple(tTilePos.x, tTilePos.y);
				}
				pActor.beh_tile_target = tTile;
			}
			return BehResult.Continue;
		}
	}
}
