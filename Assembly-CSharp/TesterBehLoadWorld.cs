using System;
using ai.behaviours;

// Token: 0x020004C4 RID: 1220
public class TesterBehLoadWorld : BehaviourActionTester
{
	// Token: 0x060029DC RID: 10716 RVA: 0x00149E70 File Offset: 0x00148070
	public TesterBehLoadWorld(int pSlot, string pFallback = null)
	{
		this.slot = pSlot;
		this.fallback = pFallback;
	}

	// Token: 0x060029DD RID: 10717 RVA: 0x00149E86 File Offset: 0x00148086
	public override BehResult execute(AutoTesterBot pObject)
	{
		SaveManager.setCurrentSlot(this.slot);
		if (!SaveManager.currentSlotExists())
		{
			SaveManager.loadMapFromResources(this.fallback);
		}
		else
		{
			BehaviourActionBase<AutoTesterBot>.world.save_manager.startLoadSlot();
		}
		return BehResult.Continue;
	}

	// Token: 0x04001F31 RID: 7985
	private int slot;

	// Token: 0x04001F32 RID: 7986
	private string fallback;
}
