using System;
using ai.behaviours;

// Token: 0x020004F2 RID: 1266
public class TesterMutateActorStatus : BehaviourActionTester
{
	// Token: 0x06002A3A RID: 10810 RVA: 0x0014BD08 File Offset: 0x00149F08
	public override BehResult execute(AutoTesterBot pObject)
	{
		BehResult result;
		using (ListPool<Status> tStatuses = new ListPool<Status>())
		{
			foreach (Actor tActor in BehaviourActionBase<AutoTesterBot>.world.units)
			{
				if (tActor.hasSubspecies() && !Randy.randomChance(0.95f))
				{
					if (tActor.hasAnyStatusEffectRaw())
					{
						tStatuses.Clear();
						tStatuses.AddRange(tActor.getStatuses());
						if (tStatuses.Count > 0)
						{
							tActor.finishStatusEffect(tStatuses.GetRandom<Status>().asset.id);
						}
					}
					else
					{
						int tTries = 10;
						while (!tActor.addStatusEffect(AssetManager.status.list.GetRandom<StatusAsset>(), 0f, true) && tTries-- > 0)
						{
						}
					}
				}
			}
			result = base.execute(pObject);
		}
		return result;
	}
}
