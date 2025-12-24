using System;

// Token: 0x020004F8 RID: 1272
[Serializable]
public class BehaviourTaskTester : BehaviourTaskBase<BehaviourActionTester>
{
	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06002A4D RID: 10829 RVA: 0x0014C2E6 File Offset: 0x0014A4E6
	protected override bool has_locales
	{
		get
		{
			return false;
		}
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x06002A4E RID: 10830 RVA: 0x0014C2E9 File Offset: 0x0014A4E9
	protected override string locale_key_prefix
	{
		get
		{
			return "task_tester";
		}
	}
}
