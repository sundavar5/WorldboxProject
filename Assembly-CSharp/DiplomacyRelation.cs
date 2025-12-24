using System;
using UnityEngine;

// Token: 0x0200026A RID: 618
public class DiplomacyRelation : CoreSystemObject<DiplomacyRelationData>
{
	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06001736 RID: 5942 RVA: 0x000E65A4 File Offset: 0x000E47A4
	public override BaseSystemManager manager
	{
		get
		{
			return World.world.diplomacy;
		}
	}

	// Token: 0x06001737 RID: 5943 RVA: 0x000E65B0 File Offset: 0x000E47B0
	protected sealed override void setDefaultValues()
	{
		base.setDefaultValues();
	}

	// Token: 0x06001738 RID: 5944 RVA: 0x000E65B8 File Offset: 0x000E47B8
	public KingdomOpinion getOpinion(Kingdom pMain, Kingdom pTarget)
	{
		this.recalculate(pMain, pTarget);
		if (this.opinion_k1.target == pTarget)
		{
			return this.opinion_k1;
		}
		return this.opinion_k2;
	}

	// Token: 0x06001739 RID: 5945 RVA: 0x000E65E0 File Offset: 0x000E47E0
	private void recalculate(Kingdom k1 = null, Kingdom k2 = null)
	{
		if (this.opinion_k1 == null)
		{
			this.opinion_k1 = new KingdomOpinion(this.kingdom1, this.kingdom2);
			this.opinion_k2 = new KingdomOpinion(this.kingdom2, this.kingdom1);
		}
		try
		{
			this.opinion_k1.calculate(this.kingdom1, this.kingdom2, this);
			this.opinion_k2.calculate(this.kingdom2, this.kingdom1, this);
		}
		catch (Exception ex)
		{
			Debug.LogError(ex);
			Debug.LogError(this.data.id);
			Debug.LogError(this.data.kingdom1_id);
			Debug.LogError(this.data.kingdom2_id);
			Debug.LogError("kingdom1 " + (this.kingdom1 == null).ToString());
			Debug.LogError("kingdom2 " + (this.kingdom2 == null).ToString());
			Debug.LogError((this.kingdom1 == k1).ToString());
			Debug.LogError((this.kingdom2 == k2).ToString());
			Debug.LogError(JsonUtility.ToJson(this.kingdom1));
			Debug.LogError(JsonUtility.ToJson(this.kingdom2));
			Debug.LogError(JsonUtility.ToJson(k1));
			Debug.LogError(JsonUtility.ToJson(k2));
			throw ex;
		}
	}

	// Token: 0x0600173A RID: 5946 RVA: 0x000E6754 File Offset: 0x000E4954
	public override void Dispose()
	{
		KingdomOpinion kingdomOpinion = this.opinion_k1;
		if (kingdomOpinion != null)
		{
			kingdomOpinion.Dispose();
		}
		KingdomOpinion kingdomOpinion2 = this.opinion_k2;
		if (kingdomOpinion2 != null)
		{
			kingdomOpinion2.Dispose();
		}
		this.opinion_k1 = null;
		this.opinion_k2 = null;
		this.kingdom1 = null;
		this.kingdom2 = null;
		base.Dispose();
	}

	// Token: 0x040012E4 RID: 4836
	public Kingdom kingdom1;

	// Token: 0x040012E5 RID: 4837
	public Kingdom kingdom2;

	// Token: 0x040012E6 RID: 4838
	internal KingdomOpinion opinion_k1;

	// Token: 0x040012E7 RID: 4839
	internal KingdomOpinion opinion_k2;
}
