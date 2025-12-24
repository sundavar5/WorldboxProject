using System;

namespace ai.behaviours
{
	// Token: 0x02000946 RID: 2374
	public class BehDragonCheckAttackCity : BehDragon
	{
		// Token: 0x0600463E RID: 17982 RVA: 0x001DC7F4 File Offset: 0x001DA9F4
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_tile_target != null)
			{
				return BehResult.Continue;
			}
			long cityToAttack;
			pActor.data.get("cityToAttack", out cityToAttack, -1L);
			if (!cityToAttack.hasValue())
			{
				return BehResult.Continue;
			}
			int attacksForCity;
			pActor.data.get("attacksForCity", out attacksForCity, 0);
			if (attacksForCity < 1)
			{
				pActor.data.removeLong("cityToAttack");
				return BehResult.Continue;
			}
			City tCityToAttack = BehaviourActionBase<Actor>.world.cities.get(cityToAttack);
			bool tClearAttackCity = true;
			if (tCityToAttack != null && tCityToAttack.isAlive() && tCityToAttack.buildings.Count > 0)
			{
				WorldTile tTile = tCityToAttack.buildings.GetRandom<Building>().current_tile.zone.tiles.GetRandom<WorldTile>();
				pActor.beh_tile_target = this.dragon.randomTileWithinLandAttackRange(tTile);
				pActor.data.set("attacksForCity", --attacksForCity);
				tClearAttackCity = false;
			}
			if (tClearAttackCity)
			{
				pActor.data.removeLong("cityToAttack");
				pActor.data.removeInt("attacksForCity");
			}
			return BehResult.Continue;
		}
	}
}
