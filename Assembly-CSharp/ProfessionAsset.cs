using System;
using System.Collections.Generic;

// Token: 0x02000071 RID: 113
[Serializable]
public class ProfessionAsset : Asset
{
	// Token: 0x06000433 RID: 1075 RVA: 0x0002C96E File Offset: 0x0002AB6E
	public bool hasDecisions()
	{
		return this.decision_ids != null;
	}

	// Token: 0x06000434 RID: 1076 RVA: 0x0002C979 File Offset: 0x0002AB79
	public bool hasDecision(string pID)
	{
		return this.decision_ids.Contains(pID);
	}

	// Token: 0x06000435 RID: 1077 RVA: 0x0002C988 File Offset: 0x0002AB88
	public void linkDecisions()
	{
		if (this.decision_ids == null)
		{
			return;
		}
		this.decisions_assets = new DecisionAsset[this.decision_ids.Count];
		for (int i = 0; i < this.decision_ids.Count; i++)
		{
			string tDecisionID = this.decision_ids[i];
			DecisionAsset tDecisionAsset = AssetManager.decisions_library.get(tDecisionID);
			this.decisions_assets[i] = tDecisionAsset;
		}
	}

	// Token: 0x06000436 RID: 1078 RVA: 0x0002C9EC File Offset: 0x0002ABEC
	public void addDecision(string pID)
	{
		if (this.decision_ids == null)
		{
			this.decision_ids = new List<string>();
		}
		this.decision_ids.Add(pID);
	}

	// Token: 0x04000353 RID: 851
	public UnitProfession profession_id;

	// Token: 0x04000354 RID: 852
	public bool can_capture;

	// Token: 0x04000355 RID: 853
	public bool is_civilian;

	// Token: 0x04000356 RID: 854
	public bool cancel_when_no_city;

	// Token: 0x04000357 RID: 855
	public List<string> decision_ids;

	// Token: 0x04000358 RID: 856
	[NonSerialized]
	public DecisionAsset[] decisions_assets;
}
