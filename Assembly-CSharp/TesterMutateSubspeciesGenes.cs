using System;
using ai.behaviours;

// Token: 0x020004F4 RID: 1268
public class TesterMutateSubspeciesGenes : BehaviourActionTester
{
	// Token: 0x06002A3E RID: 10814 RVA: 0x0014BF24 File Offset: 0x0014A124
	public override BehResult execute(AutoTesterBot pObject)
	{
		foreach (Subspecies tSubspecies in BehaviourActionBase<AutoTesterBot>.world.subspecies)
		{
			if (!Randy.randomChance(0.9f))
			{
				tSubspecies.nucleus.doRandomGeneMutations(2);
				tSubspecies.mutateTraits(1);
				tSubspecies.unstableGenomeEvent();
			}
		}
		return base.execute(pObject);
	}
}
