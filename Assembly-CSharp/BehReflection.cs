using System;
using ai.behaviours;

// Token: 0x020003C8 RID: 968
public class BehReflection : BehaviourActionActor
{
	// Token: 0x06002253 RID: 8787 RVA: 0x00120828 File Offset: 0x0011EA28
	public override BehResult execute(Actor pActor)
	{
		int tHappiness = pActor.getHappiness();
		bool tHappy = tHappiness > 50;
		bool tUnHappy = tHappiness < -70;
		BehResult result;
		using (ListPool<string> tTempPotTasks = new ListPool<string>())
		{
			if (pActor.subspecies.hasTrait("super_positivity"))
			{
				this.fillFromSuperPositivity(pActor, tTempPotTasks);
			}
			else
			{
				this.fillFromHappinessHistory(pActor, tTempPotTasks);
				if (pActor.hasTrait("hotheaded"))
				{
					tTempPotTasks.Add("swearing");
					tTempPotTasks.Add("swearing");
					tTempPotTasks.Add("swearing");
					tTempPotTasks.Add("swearing");
				}
				if (pActor.subspecies.hasTrait("aggressive") && tHappiness < 0)
				{
					tTempPotTasks.Add("start_tantrum");
				}
				if (pActor.hasTrait("hotheaded") && tHappiness < 0)
				{
					tTempPotTasks.Add("start_tantrum");
				}
				if (tHappy)
				{
					tTempPotTasks.Add("happy_laughing");
					tTempPotTasks.Add("happy_laughing");
					tTempPotTasks.Add("happy_laughing");
					tTempPotTasks.Add("singing");
					if (pActor.hasLanguage() && pActor.language.hasTrait("melodic"))
					{
						tTempPotTasks.Add("singing");
						tTempPotTasks.Add("singing");
						tTempPotTasks.Add("singing");
						tTempPotTasks.Add("singing");
					}
					if (pActor.isBaby())
					{
						tTempPotTasks.Add("child_random_flips");
						tTempPotTasks.Add("child_random_flips");
						tTempPotTasks.Add("child_play_at_one_spot");
						tTempPotTasks.Add("child_play_at_one_spot");
						tTempPotTasks.Add("child_random_jump");
						tTempPotTasks.Add("child_random_jump");
					}
					else
					{
						tTempPotTasks.Add("wait5");
					}
				}
				else if (tUnHappy)
				{
					if (!pActor.hasTag("strong_mind"))
					{
						tTempPotTasks.Add("crying");
						tTempPotTasks.Add("crying");
						if (tHappiness <= -100)
						{
							tTempPotTasks.Add("crying");
							tTempPotTasks.Add("crying");
							tTempPotTasks.Add("crying");
							tTempPotTasks.Add("start_tantrum");
						}
						tTempPotTasks.Add("start_tantrum");
					}
					tTempPotTasks.Add("swearing");
					tTempPotTasks.Add("swearing");
					tTempPotTasks.Add("punch_a_tree");
					tTempPotTasks.Add("punch_a_tree");
					tTempPotTasks.Add("punch_a_building");
					tTempPotTasks.Add("punch_a_building");
					tTempPotTasks.Add("wait5");
					if (pActor.hasTrait("pyromaniac"))
					{
						tTempPotTasks.Add("start_fire");
					}
				}
				else
				{
					tTempPotTasks.Add("wait5");
				}
			}
			if (tTempPotTasks.Count == 0)
			{
				result = BehResult.Stop;
			}
			else
			{
				string tTaskID = tTempPotTasks.GetRandom<string>();
				result = base.forceTask(pActor, tTaskID, false, true);
			}
		}
		return result;
	}

	// Token: 0x06002254 RID: 8788 RVA: 0x00120AE8 File Offset: 0x0011ECE8
	private void fillFromHappinessHistory(Actor pActor, ListPool<string> pPot)
	{
		if (!pActor.hasHappinessHistory())
		{
			return;
		}
		foreach (HappinessHistory tHappinessEntry in pActor.happiness_change_history)
		{
			HappinessAsset tAsset = tHappinessEntry.asset;
			if (tAsset.pot_task_id != null)
			{
				for (int i = 0; i < tAsset.pot_amount; i++)
				{
					pPot.Add(tAsset.pot_task_id);
				}
			}
		}
	}

	// Token: 0x06002255 RID: 8789 RVA: 0x00120B6C File Offset: 0x0011ED6C
	private void fillFromSuperPositivity(Actor pActor, ListPool<string> pPot)
	{
		pPot.Add("happy_laughing");
		pPot.Add("happy_laughing");
		pPot.Add("happy_laughing");
		pPot.Add("singing");
		pPot.Add("child_random_flips");
		pPot.Add("child_play_at_one_spot");
		pPot.Add("child_random_jump");
	}
}
