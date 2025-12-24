using System;
using ai.behaviours;

// Token: 0x020003C7 RID: 967
public class BehMadnessRandomEmotion : BehaviourActionActor
{
	// Token: 0x06002251 RID: 8785 RVA: 0x00120750 File Offset: 0x0011E950
	public override BehResult execute(Actor pActor)
	{
		if (Randy.randomBool())
		{
			using (ListPool<string> tTempStatuses = new ListPool<string>())
			{
				tTempStatuses.Add("laughing");
				tTempStatuses.Add("crying");
				tTempStatuses.Add("swearing");
				string tStatusId = tTempStatuses.GetRandom<string>();
				pActor.addStatusEffect(tStatusId, 10f, false);
				return BehResult.Continue;
			}
		}
		BehResult result;
		using (ListPool<string> tTempTasks = new ListPool<string>())
		{
			tTempTasks.Add("happy_laughing");
			tTempTasks.Add("crying");
			tTempTasks.Add("swearing");
			if (tTempTasks.Count == 0)
			{
				result = BehResult.Stop;
			}
			else
			{
				string tTaskId = tTempTasks.GetRandom<string>();
				result = base.forceTask(pActor, tTaskId, false, true);
			}
		}
		return result;
	}

	// Token: 0x040018EC RID: 6380
	private const int STATUS_DURATION = 10;
}
