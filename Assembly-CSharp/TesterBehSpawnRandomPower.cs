using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x020004E8 RID: 1256
public class TesterBehSpawnRandomPower : TesterBehSpawnPower
{
	// Token: 0x06002A25 RID: 10789 RVA: 0x0014B7A8 File Offset: 0x001499A8
	public TesterBehSpawnRandomPower() : base(null)
	{
		if (TesterBehSpawnRandomPower.events == null)
		{
			TesterBehSpawnRandomPower.events = new List<string>();
			foreach (GodPower tPower in AssetManager.powers.list)
			{
				if (tPower.id[0] != '_' && tPower.tester_enabled)
				{
					TesterBehSpawnRandomPower.events.Add(tPower.id);
					if (tPower.type == PowerActionType.PowerDrawTile)
					{
						TesterBehSpawnRandomPower.events.Add(tPower.id);
						TesterBehSpawnRandomPower.events.Add(tPower.id);
						TesterBehSpawnRandomPower.events.Add(tPower.id);
					}
				}
			}
		}
	}

	// Token: 0x06002A26 RID: 10790 RVA: 0x0014B874 File Offset: 0x00149A74
	public override BehResult execute(AutoTesterBot pObject)
	{
		this._power = TesterBehSpawnRandomPower.events.GetRandom<string>();
		return base.execute(pObject);
	}

	// Token: 0x04001F6E RID: 8046
	private static List<string> events;
}
