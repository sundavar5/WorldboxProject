using System;

// Token: 0x020004E3 RID: 1251
public class TesterBehSpawnRandomBoat : TesterBehSpawnRandomUnit
{
	// Token: 0x06002A1C RID: 10780 RVA: 0x0014B482 File Offset: 0x00149682
	public TesterBehSpawnRandomBoat(int pAmount = 1) : base(pAmount, "random")
	{
		this.filter_delegate = ((ActorAsset pActorAsset) => !pActorAsset.isTemplateAsset() && pActorAsset.is_boat);
	}
}
