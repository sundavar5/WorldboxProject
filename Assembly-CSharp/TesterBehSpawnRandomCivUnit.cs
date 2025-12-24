using System;

// Token: 0x020004E6 RID: 1254
public class TesterBehSpawnRandomCivUnit : TesterBehSpawnRandomUnit
{
	// Token: 0x06002A22 RID: 10786 RVA: 0x0014B6AC File Offset: 0x001498AC
	public TesterBehSpawnRandomCivUnit(int pAmount = 1, string pLocation = "random") : base(pAmount, pLocation)
	{
		this.filter_delegate = ((ActorAsset pActorAsset) => !pActorAsset.isTemplateAsset() && pActorAsset.has_ai_system && !pActorAsset.is_boat && !pActorAsset.unit_other && !pActorAsset.special && !pActorAsset.id.Contains("zombie") && pActorAsset.civ);
	}
}
