using System;
using ai.behaviours;

// Token: 0x020004E4 RID: 1252
public class TesterBehSpawnRandomBomb : TesterBehSpawnPower
{
	// Token: 0x06002A1D RID: 10781 RVA: 0x0014B4B8 File Offset: 0x001496B8
	public TesterBehSpawnRandomBomb() : base(null)
	{
		if (TesterBehSpawnRandomBomb.events == null)
		{
			TesterBehSpawnRandomBomb.events = new string[]
			{
				"bomb",
				"grenade",
				"napalm_bomb",
				"atomic_bomb",
				"antimatter_bomb",
				"czar_bomba"
			};
		}
	}

	// Token: 0x06002A1E RID: 10782 RVA: 0x0014B50E File Offset: 0x0014970E
	public override BehResult execute(AutoTesterBot pObject)
	{
		this._power = TesterBehSpawnRandomBomb.events.GetRandom<string>();
		return base.execute(pObject);
	}

	// Token: 0x04001F6B RID: 8043
	internal static string[] events;
}
