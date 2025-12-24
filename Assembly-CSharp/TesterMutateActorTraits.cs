using System;
using ai.behaviours;

// Token: 0x020004F3 RID: 1267
public class TesterMutateActorTraits : BehaviourActionTester
{
	// Token: 0x06002A3C RID: 10812 RVA: 0x0014BE04 File Offset: 0x0014A004
	public override BehResult execute(AutoTesterBot pObject)
	{
		BehResult result;
		using (ListPool<ActorTrait> tTraits = new ListPool<ActorTrait>())
		{
			foreach (Actor tActor in BehaviourActionBase<AutoTesterBot>.world.units)
			{
				if (!Randy.randomChance(0.9f))
				{
					tTraits.Clear();
					tTraits.AddRange(tActor.getTraits());
					if (tTraits.Count > 0)
					{
						ActorTrait tTrait = tTraits.GetRandom<ActorTrait>();
						if (tTrait.can_be_removed)
						{
							tActor.removeTrait(tTrait);
						}
					}
					int tTries = 10;
					while (tTries-- > 0)
					{
						ActorTrait tTrait2 = AssetManager.traits.list.GetRandom<ActorTrait>();
						if (tTrait2.can_be_given && !tTrait2.id.Contains("zombie") && !tTrait2.id.Contains("plague") && tActor.addTrait(tTrait2, false))
						{
							break;
						}
					}
				}
			}
			result = base.execute(pObject);
		}
		return result;
	}
}
