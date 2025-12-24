using System;
using System.Collections.Generic;

// Token: 0x020005AA RID: 1450
public class SignalLibrary : AssetLibrary<SignalAsset>
{
	// Token: 0x06003010 RID: 12304 RVA: 0x0017401C File Offset: 0x0017221C
	public override void post_init()
	{
		base.post_init();
		using (List<Achievement>.Enumerator enumerator = AssetManager.achievements.list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				Achievement tAchievement = enumerator.Current;
				if (!tAchievement.has_signal)
				{
					SignalAsset tNewSignal = this.add(new SignalAsset
					{
						id = tAchievement.id + "_signal",
						action_achievement = new AchievementCheck(tAchievement.check),
						ban_check_action = ((object _) => tAchievement.isUnlocked())
					});
					tAchievement.setSignal(tNewSignal);
				}
			}
		}
		foreach (SignalAsset tAsset in this.list)
		{
			if (tAsset.action != null)
			{
				tAsset.has_action = true;
			}
			if (tAsset.action_achievement != null)
			{
				tAsset.has_action_achievement = true;
			}
			if (tAsset.ban_check_action != null)
			{
				tAsset.has_ban_check_action = true;
				if (tAsset.ban_check_action(null))
				{
					tAsset.ban();
				}
			}
		}
	}
}
