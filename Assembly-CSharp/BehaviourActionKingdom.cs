using System;

// Token: 0x020003EB RID: 1003
public class BehaviourActionKingdom : BehaviourActionBase<Kingdom>
{
	// Token: 0x060022E1 RID: 8929 RVA: 0x00123414 File Offset: 0x00121614
	protected override void setupErrorChecks()
	{
		base.setupErrorChecks();
		this.uses_kingdoms = true;
		this.uses_cities = true;
	}
}
