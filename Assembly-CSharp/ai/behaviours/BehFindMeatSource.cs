using System;

namespace ai.behaviours
{
	// Token: 0x020008C8 RID: 2248
	public class BehFindMeatSource : BehaviourActionActor
	{
		// Token: 0x060044F2 RID: 17650 RVA: 0x001CFD8C File Offset: 0x001CDF8C
		public BehFindMeatSource(MeatTargetType pMeatTargetType = MeatTargetType.Meat, bool pCheckForFactions = true)
		{
			this._check_for_factions = pCheckForFactions;
			this._meat_target_type = pMeatTargetType;
		}

		// Token: 0x060044F3 RID: 17651 RVA: 0x001CFDA2 File Offset: 0x001CDFA2
		public override BehResult execute(Actor pActor)
		{
			if (pActor.beh_actor_target != null && this.isTargetOk(pActor, pActor.beh_actor_target.a))
			{
				return BehResult.Continue;
			}
			pActor.beh_actor_target = this.getClosestMeatActor(pActor);
			if (pActor.beh_actor_target != null)
			{
				return BehResult.Continue;
			}
			return BehResult.Stop;
		}

		// Token: 0x060044F4 RID: 17652 RVA: 0x001CFDDC File Offset: 0x001CDFDC
		private Actor getClosestMeatActor(Actor pActor)
		{
			bool tRandomShuffle = Randy.randomBool();
			WorldTile tTile = pActor.current_tile;
			float tBestDist = 2.1474836E+09f;
			Actor tBestActor = null;
			int tChunkRadius = Randy.randomInt(1, 3);
			foreach (Actor tTarget in Finder.getUnitsFromChunk(tTile, tChunkRadius, 0f, tRandomShuffle))
			{
				float tDist = (float)Toolbox.SquaredDistTile(tTarget.current_tile, tTile);
				if (tDist < tBestDist && this.isTargetOk(pActor, tTarget))
				{
					bool tIsSameSpecies = tTarget.isSameSpecies(pActor.asset.id);
					switch (this._meat_target_type)
					{
					case MeatTargetType.Meat:
						if (!tTarget.asset.source_meat)
						{
							continue;
						}
						if (tIsSameSpecies)
						{
							continue;
						}
						break;
					case MeatTargetType.MeatSameSpecies:
						if (!tIsSameSpecies)
						{
							continue;
						}
						break;
					case MeatTargetType.Insect:
						if (!tTarget.asset.source_meat_insect || tIsSameSpecies)
						{
							continue;
						}
						break;
					}
					tBestDist = tDist;
					tBestActor = tTarget;
					if (tRandomShuffle && Randy.randomBool())
					{
						break;
					}
				}
			}
			return tBestActor;
		}

		// Token: 0x060044F5 RID: 17653 RVA: 0x001CFEE4 File Offset: 0x001CE0E4
		private bool isTargetOk(Actor pActor, Actor pTarget)
		{
			return pTarget != pActor && pActor.canAttackTarget(pTarget, this._check_for_factions, true) && pTarget.asset.actor_size <= pActor.asset.actor_size && pTarget.current_tile.isSameIsland(pActor.current_tile);
		}

		// Token: 0x0400317B RID: 12667
		private MeatTargetType _meat_target_type;

		// Token: 0x0400317C RID: 12668
		private bool _check_for_factions;
	}
}
