using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x020004E5 RID: 1253
public class TesterBehSpawnRandomBuilding : BehaviourActionTester
{
	// Token: 0x06002A1F RID: 10783 RVA: 0x0014B528 File Offset: 0x00149728
	public TesterBehSpawnRandomBuilding()
	{
		if (TesterBehSpawnRandomBuilding.assets.Count == 0)
		{
			TesterBehSpawnRandomBuilding.assets.Add("tree_green_1");
			TesterBehSpawnRandomBuilding.assets.Add("fruit_bush");
			TesterBehSpawnRandomBuilding.assets.Add("palm_tree");
			TesterBehSpawnRandomBuilding.assets.Add("pine_tree");
			TesterBehSpawnRandomBuilding.assets.Add("tumor");
			TesterBehSpawnRandomBuilding.assets.Add("golden_brain");
			TesterBehSpawnRandomBuilding.assets.Add("corrupted_brain");
			TesterBehSpawnRandomBuilding.assets.Add("beehive");
			TesterBehSpawnRandomBuilding.assets.Add("ice_tower");
			TesterBehSpawnRandomBuilding.assets.Add("flame_tower");
			TesterBehSpawnRandomBuilding.assets.Add("volcano");
			TesterBehSpawnRandomBuilding.assets.Add("geyser_acid");
			TesterBehSpawnRandomBuilding.assets.Add("geyser");
		}
	}

	// Token: 0x06002A20 RID: 10784 RVA: 0x0014B610 File Offset: 0x00149810
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (TesterBehSpawnRandomBuilding.last_id > TesterBehSpawnRandomBuilding.assets.Count - 1)
		{
			TesterBehSpawnRandomBuilding.last_id = 0;
			TesterBehSpawnRandomBuilding.assets.Shuffle<string>();
		}
		string tAsset = TesterBehSpawnRandomBuilding.assets[TesterBehSpawnRandomBuilding.last_id++];
		for (int i = 0; i < 3; i++)
		{
			TileZone tZone = BehaviourActionBase<AutoTesterBot>.world.zone_calculator.zones.GetRandom<TileZone>();
			BehaviourActionBase<AutoTesterBot>.world.buildings.addBuilding(tAsset, tZone.centerTile, true, false, BuildPlacingType.New);
		}
		return base.execute(pObject);
	}

	// Token: 0x04001F6C RID: 8044
	private static List<string> assets = new List<string>();

	// Token: 0x04001F6D RID: 8045
	private static int last_id = 0;
}
