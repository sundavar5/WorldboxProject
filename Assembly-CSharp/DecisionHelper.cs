using System;

// Token: 0x020000E4 RID: 228
public class DecisionHelper
{
	// Token: 0x060006B5 RID: 1717 RVA: 0x00063F54 File Offset: 0x00062154
	public static bool makeDecisionFor(Actor pActor, out string pLastDecisionID)
	{
		pLastDecisionID = string.Empty;
		if (pActor.isStatsDirty())
		{
			pActor.setTask("wait", true, false, false);
			return false;
		}
		DecisionAsset tDecisionAsset = DecisionHelper.decision_system.useOn(pActor, true);
		if (tDecisionAsset == null)
		{
			return false;
		}
		pLastDecisionID = tDecisionAsset.id;
		string tTaskID = tDecisionAsset.id;
		if (!string.IsNullOrEmpty(tDecisionAsset.task_id))
		{
			tTaskID = tDecisionAsset.task_id;
		}
		pActor.setTask(tTaskID, true, false, false);
		return true;
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x00063FC0 File Offset: 0x000621C0
	public static void runSimulation(Actor pActor)
	{
		DecisionHelper.decision_system.useOn(pActor, false);
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x00063FCF File Offset: 0x000621CF
	public static void runSimulationForMindTab(Actor pActor)
	{
		DecisionHelper.decision_system.useOn(pActor, false);
	}

	// Token: 0x04000771 RID: 1905
	internal static UtilityBasedDecisionSystem decision_system = new UtilityBasedDecisionSystem();
}
