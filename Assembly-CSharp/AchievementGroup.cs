using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200061F RID: 1567
public class AchievementGroup : MonoBehaviour
{
	// Token: 0x0600335C RID: 13148 RVA: 0x00182DA0 File Offset: 0x00180FA0
	public void showGroup(AchievementGroupAsset pAchievementGroup)
	{
		this.title.GetComponent<LocalizedText>().setKeyAndUpdate(pAchievementGroup.getLocaleID());
		this.title.color = pAchievementGroup.getColor();
		if (pAchievementGroup.achievements_list.Count > 0)
		{
			int tTotalUnlocked = 0;
			foreach (Achievement tAchievement in pAchievementGroup.achievements_list)
			{
				AchievementButton tButton = Object.Instantiate<AchievementButton>(this.achievementButtonPrefab, this.transformContent);
				tButton.Load(tAchievement);
				if (AchievementLibrary.isUnlocked(tAchievement))
				{
					tTotalUnlocked++;
				}
				this._elements.Add(tButton);
			}
			this.counter.text = tTotalUnlocked.ToString() + " / " + pAchievementGroup.achievements_list.Count.ToString();
		}
	}

	// Token: 0x040026F4 RID: 9972
	public AchievementButton achievementButtonPrefab;

	// Token: 0x040026F5 RID: 9973
	private List<AchievementButton> _elements = new List<AchievementButton>();

	// Token: 0x040026F6 RID: 9974
	public Text title;

	// Token: 0x040026F7 RID: 9975
	public Text counter;

	// Token: 0x040026F8 RID: 9976
	public Transform transformContent;
}
