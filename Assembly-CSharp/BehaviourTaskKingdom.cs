using System;

// Token: 0x020003EC RID: 1004
[Serializable]
public class BehaviourTaskKingdom : BehaviourTaskBase<BehaviourActionKingdom>
{
	// Token: 0x170001F3 RID: 499
	// (get) Token: 0x060022E3 RID: 8931 RVA: 0x00123432 File Offset: 0x00121632
	protected override string locale_key_prefix
	{
		get
		{
			return "task_kingdom";
		}
	}

	// Token: 0x170001F4 RID: 500
	// (get) Token: 0x060022E4 RID: 8932 RVA: 0x00123439 File Offset: 0x00121639
	protected override bool has_locales
	{
		get
		{
			return false;
		}
	}
}
