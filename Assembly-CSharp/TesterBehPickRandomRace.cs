using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x020004CA RID: 1226
public class TesterBehPickRandomRace : BehaviourActionTester
{
	// Token: 0x060029E8 RID: 10728 RVA: 0x0014A12C File Offset: 0x0014832C
	public TesterBehPickRandomRace()
	{
		if (TesterBehPickRandomRace.assets == null)
		{
			TesterBehPickRandomRace.assets = new List<string>
			{
				"human",
				"elf",
				"orc",
				"dwarf"
			};
		}
	}

	// Token: 0x060029E9 RID: 10729 RVA: 0x0014A17C File Offset: 0x0014837C
	public override BehResult execute(AutoTesterBot pObject)
	{
		pObject.beh_asset_target = TesterBehPickRandomRace.assets.GetRandom<string>();
		return base.execute(pObject);
	}

	// Token: 0x04001F3B RID: 7995
	private static List<string> assets;
}
