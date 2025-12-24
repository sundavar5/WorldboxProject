using System;
using System.Collections.Generic;

// Token: 0x0200016E RID: 366
[Serializable]
public class SpellAsset : Asset
{
	// Token: 0x06000B0D RID: 2829 RVA: 0x000A1794 File Offset: 0x0009F994
	public bool hasDecisions()
	{
		return this.decisions_assets != null;
	}

	// Token: 0x06000B0E RID: 2830 RVA: 0x000A179F File Offset: 0x0009F99F
	public void addDecision(string pID)
	{
		if (this.decision_ids == null)
		{
			this.decision_ids = new List<string>();
		}
		this.decision_ids.Add(pID);
	}

	// Token: 0x04000AC0 RID: 2752
	public float health_ratio;

	// Token: 0x04000AC1 RID: 2753
	public int cost_mana;

	// Token: 0x04000AC2 RID: 2754
	public float chance = 1f;

	// Token: 0x04000AC3 RID: 2755
	public float min_distance;

	// Token: 0x04000AC4 RID: 2756
	public CastTarget cast_target;

	// Token: 0x04000AC5 RID: 2757
	public CastEntity cast_entity = CastEntity.Both;

	// Token: 0x04000AC6 RID: 2758
	public AttackAction action;

	// Token: 0x04000AC7 RID: 2759
	public List<string> decision_ids;

	// Token: 0x04000AC8 RID: 2760
	[NonSerialized]
	public DecisionAsset[] decisions_assets;

	// Token: 0x04000AC9 RID: 2761
	public bool can_be_used_in_combat;
}
