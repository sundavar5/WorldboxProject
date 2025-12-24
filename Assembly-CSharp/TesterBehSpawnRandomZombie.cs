using System;

// Token: 0x020004EC RID: 1260
public class TesterBehSpawnRandomZombie : TesterBehSpawnRandomUnit
{
	// Token: 0x06002A2F RID: 10799 RVA: 0x0014BB29 File Offset: 0x00149D29
	public TesterBehSpawnRandomZombie(int pAmount = 1) : base(pAmount, "random")
	{
		this.filter_delegate = ((ActorAsset pActorAsset) => !pActorAsset.isTemplateAsset() && pActorAsset.id.Contains("zombie"));
	}
}
