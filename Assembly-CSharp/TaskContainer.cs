using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// Token: 0x02000386 RID: 902
[Serializable]
public class TaskContainer<TCondition, TSimObject> where TCondition : BehaviourBaseCondition<TSimObject>
{
	// Token: 0x06002193 RID: 8595 RVA: 0x0011CE3C File Offset: 0x0011B03C
	public void addCondition(TCondition pCondition, bool pResult)
	{
		if (this.conditions == null)
		{
			this.conditions = new Dictionary<TCondition, bool>();
		}
		this.conditions.Add(pCondition, pResult);
		this.has_conditions = true;
	}

	// Token: 0x040018D8 RID: 6360
	[JsonProperty(Order = -1)]
	public string id;

	// Token: 0x040018D9 RID: 6361
	public Dictionary<TCondition, bool> conditions;

	// Token: 0x040018DA RID: 6362
	public bool has_conditions;
}
