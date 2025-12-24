using System;
using ai.behaviours;

// Token: 0x020003E0 RID: 992
public class BehWarriorCaptainWait : BehCityActor
{
	// Token: 0x060022AA RID: 8874 RVA: 0x00122A40 File Offset: 0x00120C40
	public override BehResult execute(Actor pActor)
	{
		if (!pActor.isArmyGroupLeader())
		{
			return BehResult.Stop;
		}
		Army tArmy = pActor.army;
		WorldTile tCurrentTile = pActor.current_tile;
		int tCloseUnits = 0;
		foreach (Actor tArmyUnit in tArmy.units)
		{
			if (Toolbox.SquaredDist(tCurrentTile.posV3.x, tCurrentTile.posV3.y, (float)tArmyUnit.current_tile.x, (float)tArmyUnit.current_tile.y) < 100f)
			{
				tCloseUnits++;
			}
		}
		float tWaitTarget = 2f;
		float tRatio = (float)tCloseUnits / (float)tArmy.units.Count;
		if (tRatio < 0.2f)
		{
			tWaitTarget = 13f;
		}
		else if (tRatio < 0.4f)
		{
			tWaitTarget = 7f;
		}
		else if (tRatio < 0.6f)
		{
			tWaitTarget = 4f;
		}
		pActor.timer_action = Randy.randomFloat(tWaitTarget, tWaitTarget * 2f);
		return BehResult.Continue;
	}
}
