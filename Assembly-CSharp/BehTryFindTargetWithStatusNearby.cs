using System;
using ai.behaviours;

// Token: 0x020003DF RID: 991
public class BehTryFindTargetWithStatusNearby : BehaviourActionActor
{
	// Token: 0x060022A7 RID: 8871 RVA: 0x001228F4 File Offset: 0x00120AF4
	public BehTryFindTargetWithStatusNearby(params string[] pStatusIDs)
	{
		this._status_ids = pStatusIDs;
	}

	// Token: 0x060022A8 RID: 8872 RVA: 0x00122904 File Offset: 0x00120B04
	public override BehResult execute(Actor pActor)
	{
		Actor tTarget = this.getClosestActorWithStatus(pActor, this._status_ids);
		if (tTarget != null)
		{
			pActor.beh_tile_target = tTarget.current_tile.getTileAroundThisOnSameIsland(pActor.current_tile);
			pActor.beh_actor_target = tTarget;
			return BehResult.Continue;
		}
		WorldTile tTile = Finder.findTileInChunk(pActor.current_tile, TileFinderType.FreeTile);
		if (tTile == null)
		{
			return BehResult.Stop;
		}
		pActor.beh_tile_target = tTile;
		return BehResult.Continue;
	}

	// Token: 0x060022A9 RID: 8873 RVA: 0x0012295C File Offset: 0x00120B5C
	private Actor getClosestActorWithStatus(Actor pSelf, string[] pStatusIDs)
	{
		bool tRandomShuffle = Randy.randomBool();
		int tBestDist = int.MaxValue;
		Actor tBest = null;
		foreach (Actor tTarget in Finder.getUnitsFromChunk(pSelf.current_tile, 1, 0f, tRandomShuffle))
		{
			if (tTarget != pSelf)
			{
				int tDist = Toolbox.SquaredDistTile(tTarget.current_tile, pSelf.current_tile);
				if (tDist < tBestDist && pSelf.isSameIslandAs(tTarget) && tTarget.hasAnyStatusEffect())
				{
					bool tHasAnyStatusEffect = false;
					foreach (string tStatusID in pStatusIDs)
					{
						if (tTarget.hasStatus(tStatusID))
						{
							tHasAnyStatusEffect = true;
							break;
						}
					}
					if (tHasAnyStatusEffect)
					{
						tBestDist = tDist;
						tBest = tTarget;
						if (tRandomShuffle)
						{
							break;
						}
						if (Randy.randomBool())
						{
							break;
						}
					}
				}
			}
		}
		return tBest;
	}

	// Token: 0x040018F9 RID: 6393
	private string[] _status_ids;
}
