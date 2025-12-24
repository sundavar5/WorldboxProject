using System;
using ai.behaviours;

// Token: 0x020004D5 RID: 1237
public class TesterBehSaveWorldIfEmpty : BehaviourActionTester
{
	// Token: 0x060029FC RID: 10748 RVA: 0x0014A583 File Offset: 0x00148783
	public TesterBehSaveWorldIfEmpty(int pSlot)
	{
		this.slot = pSlot;
	}

	// Token: 0x060029FD RID: 10749 RVA: 0x0014A592 File Offset: 0x00148792
	public override BehResult execute(AutoTesterBot pObject)
	{
		SaveManager.setCurrentSlot(this.slot);
		if (!SaveManager.currentSlotExists())
		{
			SaveManager.saveWorldToDirectory(SaveManager.currentSavePath, true, true);
		}
		return BehResult.Continue;
	}

	// Token: 0x04001F49 RID: 8009
	private int slot;
}
