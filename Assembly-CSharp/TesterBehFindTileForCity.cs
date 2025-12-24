using System;
using ai.behaviours;

// Token: 0x020004C2 RID: 1218
public class TesterBehFindTileForCity : BehaviourActionTester
{
	// Token: 0x060029D8 RID: 10712 RVA: 0x00149DE0 File Offset: 0x00147FE0
	public override BehResult execute(AutoTesterBot pObject)
	{
		TileZone tZone = BehaviourActionBase<AutoTesterBot>.world.zone_calculator.zones.GetRandom<TileZone>();
		for (int i = 0; i < 100; i++)
		{
			if (tZone.isGoodForNewCity())
			{
				pObject.beh_tile_target = tZone.centerTile;
				return BehResult.Continue;
			}
			tZone = BehaviourActionBase<AutoTesterBot>.world.zone_calculator.zones.GetRandom<TileZone>();
		}
		return base.execute(pObject);
	}
}
