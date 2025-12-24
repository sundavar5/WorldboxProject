using System;

// Token: 0x02000314 RID: 788
internal readonly struct TemperatureMod
{
	// Token: 0x06001D89 RID: 7561 RVA: 0x0010789A File Offset: 0x00105A9A
	public TemperatureMod(Actor pActor, int pNewTemperature)
	{
		this.actor = pActor;
		this.new_temperature = pNewTemperature;
	}

	// Token: 0x0400161B RID: 5659
	public readonly Actor actor;

	// Token: 0x0400161C RID: 5660
	public readonly int new_temperature;
}
