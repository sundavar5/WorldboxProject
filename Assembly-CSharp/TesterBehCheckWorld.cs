using System;
using ai.behaviours;
using UnityEngine;

// Token: 0x020004B6 RID: 1206
public class TesterBehCheckWorld : BehaviourActionTester
{
	// Token: 0x060029BE RID: 10686 RVA: 0x001496DC File Offset: 0x001478DC
	public override BehResult execute(AutoTesterBot pObject)
	{
		string errors = TesterBehCheckWorld.checkTestData();
		if (!string.IsNullOrEmpty(errors))
		{
			Debug.Log("Errors:\n" + errors);
			pObject.ai.setTask("shutdown", true, true, false);
			return BehResult.Skip;
		}
		return BehResult.Continue;
	}

	// Token: 0x060029BF RID: 10687 RVA: 0x00149720 File Offset: 0x00147920
	private static string checkTestData()
	{
		string errors = string.Empty;
		if (BehaviourActionBase<AutoTesterBot>.world.cities.Count == 0)
		{
			errors += "cities list is empty - load a map with cities present\n";
		}
		if (BehaviourActionBase<AutoTesterBot>.world.clans.Count == 0)
		{
			errors += "clans list is empty - load a map with clans present\n";
		}
		if (BehaviourActionBase<AutoTesterBot>.world.plots.Count == 0)
		{
			errors += "plots list is empty - load a map with plots present\n";
		}
		if (BehaviourActionBase<AutoTesterBot>.world.alliances.Count == 0)
		{
			errors += "alliances list is empty - load a map with alliances present\n";
		}
		if (BehaviourActionBase<AutoTesterBot>.world.wars.Count == 0)
		{
			errors += "wars list is empty - load a map with wars present\n";
		}
		if (BehaviourActionBase<AutoTesterBot>.world.kingdoms.Count == 0)
		{
			errors += "kingdoms list is empty - load a map with cultures present\n";
		}
		if (BehaviourActionBase<AutoTesterBot>.world.cultures.Count == 0)
		{
			errors += "cultures list is empty - load a map with cultures present\n";
		}
		if (BehaviourActionBase<AutoTesterBot>.world.units.Count == 0)
		{
			errors += "units list is empty - load a map with world present\n";
		}
		return errors + "You can only test this in the editor\n";
	}
}
