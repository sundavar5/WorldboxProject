using System;

namespace ai.behaviours
{
	// Token: 0x02000955 RID: 2389
	public class BehDragonZombieFindGoldenBrain : BehDragon
	{
		// Token: 0x0600465C RID: 18012 RVA: 0x001DD138 File Offset: 0x001DB338
		public override BehResult execute(Actor pActor)
		{
			if (!pActor.hasTrait("zombie"))
			{
				return BehResult.Continue;
			}
			if (this.dragon.aggroTargets.Count > 0)
			{
				return BehResult.Continue;
			}
			long cityToAttack;
			pActor.data.get("cityToAttack", out cityToAttack, -1L);
			if (cityToAttack.hasValue())
			{
				return BehResult.Continue;
			}
			if (BehaviourActionBase<Actor>.world.kingdoms_wild.get("golden_brain").hasBuildings())
			{
				float tBest = 0f;
				WorldTile tTile = null;
				foreach (Building tB in BehaviourActionBase<Actor>.world.kingdoms_wild.get("golden_brain").buildings)
				{
					float tDist = Toolbox.DistTile(tB.current_tile, pActor.current_tile);
					if (tTile == null || tDist < tBest)
					{
						tTile = tB.current_tile;
						tBest = tDist;
					}
				}
				if (tTile != null)
				{
					if (this.dragon.landAttackRange(tTile))
					{
						return base.forceTask(pActor, "dragon_land", true, false);
					}
					pActor.beh_tile_target = this.dragon.randomTileWithinLandAttackRange(tTile);
				}
			}
			return BehResult.Continue;
		}
	}
}
