using System;
using ai.behaviours;

// Token: 0x020004F5 RID: 1269
public class TesterMutateSubspeciesTraits : BehaviourActionTester
{
	// Token: 0x06002A40 RID: 10816 RVA: 0x0014BFA4 File Offset: 0x0014A1A4
	public override BehResult execute(AutoTesterBot pObject)
	{
		BehResult result;
		using (ListPool<SubspeciesTrait> tTraits = new ListPool<SubspeciesTrait>())
		{
			foreach (Subspecies tSubspecies in BehaviourActionBase<AutoTesterBot>.world.subspecies)
			{
				if (!Randy.randomChance(0.9f))
				{
					tTraits.Clear();
					tTraits.AddRange(tSubspecies.getTraits());
					if (tTraits.Count > 0)
					{
						tSubspecies.removeTrait(tTraits.GetRandom<SubspeciesTrait>());
					}
					int tTries = 10;
					for (int i = 0; i < tTries; i++)
					{
						SubspeciesTrait tTrait = AssetManager.subspecies_traits.list.GetRandom<SubspeciesTrait>();
						if (tTrait.can_be_given && tSubspecies.addTrait(tTrait, false))
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
