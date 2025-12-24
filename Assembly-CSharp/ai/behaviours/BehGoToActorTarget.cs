using System;
using System.Collections.Generic;

namespace ai.behaviours
{
	// Token: 0x020008DF RID: 2271
	public class BehGoToActorTarget : BehaviourActionActor
	{
		// Token: 0x06004529 RID: 17705 RVA: 0x001D0F1A File Offset: 0x001CF11A
		public BehGoToActorTarget(GoToActorTargetType pType = GoToActorTargetType.SameTile, bool pPathOnWater = false, bool pCheckCanAttackTarget = false, bool pCalibrateTargetPosition = false, float pCheckDistance = 2f, bool pCheckSameIsland = true, bool pCheckInsideSomething = true)
		{
			this._path_on_water = pPathOnWater;
			this._type = pType;
			this._check_can_attack_target = pCheckCanAttackTarget;
			this._check_same_island = pCheckSameIsland;
			this._check_inside_something = pCheckInsideSomething;
			this.calibrate_target_position = pCalibrateTargetPosition;
			this.check_actor_target_position_distance = pCheckDistance;
		}

		// Token: 0x0600452A RID: 17706 RVA: 0x001D0F57 File Offset: 0x001CF157
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_actor_target = true;
		}

		// Token: 0x0600452B RID: 17707 RVA: 0x001D0F68 File Offset: 0x001CF168
		public override BehResult execute(Actor pActor)
		{
			BaseSimObject tTarget = pActor.beh_actor_target;
			WorldTile tTileTarget = tTarget.current_tile;
			if (tTarget.isActor())
			{
				Actor tTargetActor = tTarget.a;
				if (this._check_can_attack_target && !pActor.isTargetOkToAttack(tTargetActor))
				{
					return BehResult.Stop;
				}
				if (this._check_same_island && !pActor.isSameIslandAs(tTargetActor))
				{
					return BehResult.Stop;
				}
				if (this._check_inside_something && tTargetActor.isInsideSomething())
				{
					return BehResult.Stop;
				}
			}
			switch (this._type)
			{
			case GoToActorTargetType.SameTile:
				tTileTarget = tTarget.current_tile;
				break;
			case GoToActorTargetType.SameRegion:
				tTileTarget = tTarget.current_tile.region.tiles.GetRandom<WorldTile>();
				break;
			case GoToActorTargetType.NearbyTile:
				tTileTarget = tTarget.current_tile.getTileAroundThisOnSameIsland(pActor.current_tile);
				break;
			case GoToActorTargetType.NearbyTileClosest:
				tTileTarget = tTarget.current_tile.getTileAroundThisOnSameIsland(pActor.current_tile, true);
				break;
			case GoToActorTargetType.RaycastWithAttackRange:
				tTileTarget = this.raycastToTarget(pActor, tTarget);
				if (tTileTarget == pActor.current_tile)
				{
					return BehResult.Continue;
				}
				break;
			}
			if (tTileTarget == null)
			{
				pActor.ignoreTarget(tTarget);
				return BehResult.Stop;
			}
			if (pActor.goTo(tTileTarget, this._path_on_water, false, false, 0) == ExecuteEvent.True)
			{
				return BehResult.Continue;
			}
			pActor.ignoreTarget(tTarget);
			return BehResult.Stop;
		}

		// Token: 0x0600452C RID: 17708 RVA: 0x001D1074 File Offset: 0x001CF274
		private WorldTile raycastToTarget(Actor pSelf, BaseSimObject pTarget)
		{
			WorldTile tActorTile = pSelf.current_tile;
			WorldTile tTargetTile = pTarget.current_tile;
			List<WorldTile> tRaycastResult = PathfinderTools.raycast(tActorTile, tTargetTile, 0.99f);
			WorldTile tResultTile = null;
			float tAttackRange = pSelf.getAttackRangeSquared();
			for (int i = 0; i < tRaycastResult.Count; i++)
			{
				WorldTile tRaycastTile = tRaycastResult[i];
				if (tRaycastTile.isSameIsland(tActorTile) && (float)Toolbox.SquaredDistTile(tRaycastTile, tTargetTile) < tAttackRange)
				{
					tResultTile = tRaycastTile;
					break;
				}
			}
			if (tResultTile == null)
			{
				tResultTile = tTargetTile;
			}
			return tResultTile;
		}

		// Token: 0x0400318E RID: 12686
		private GoToActorTargetType _type;

		// Token: 0x0400318F RID: 12687
		private bool _path_on_water;

		// Token: 0x04003190 RID: 12688
		private bool _check_can_attack_target;

		// Token: 0x04003191 RID: 12689
		private bool _check_same_island;

		// Token: 0x04003192 RID: 12690
		private bool _check_inside_something;
	}
}
