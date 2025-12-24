using System;

// Token: 0x020004E9 RID: 1257
public class TesterBehSpawnRandomSpecialUnit : TesterBehSpawnRandomUnit
{
	// Token: 0x06002A27 RID: 10791 RVA: 0x0014B88D File Offset: 0x00149A8D
	public TesterBehSpawnRandomSpecialUnit(int pAmount = 1) : base(pAmount, "random")
	{
		this.filter_delegate = ((ActorAsset pActorAsset) => !pActorAsset.isTemplateAsset() && (pActorAsset.unit_other || pActorAsset.special));
	}
}
