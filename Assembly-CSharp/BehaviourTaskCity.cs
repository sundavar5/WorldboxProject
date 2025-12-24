using System;

// Token: 0x020003E8 RID: 1000
[Serializable]
public class BehaviourTaskCity : BehaviourTaskBase<BehaviourActionCity>
{
	// Token: 0x170001F1 RID: 497
	// (get) Token: 0x060022D9 RID: 8921 RVA: 0x00123294 File Offset: 0x00121494
	protected override string locale_key_prefix
	{
		get
		{
			return "task_city";
		}
	}

	// Token: 0x170001F2 RID: 498
	// (get) Token: 0x060022DA RID: 8922 RVA: 0x0012329B File Offset: 0x0012149B
	protected override bool has_locales
	{
		get
		{
			return false;
		}
	}

	// Token: 0x060022DB RID: 8923 RVA: 0x001232A0 File Offset: 0x001214A0
	public void executeAllActionsForCity(City pCity)
	{
		for (int i = 0; i < this.list.Count; i++)
		{
			this.list[i].startExecute(pCity);
		}
	}
}
