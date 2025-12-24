using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000620 RID: 1568
public class AchievementWindow : MonoBehaviour
{
	// Token: 0x0600335E RID: 13150 RVA: 0x00182E9B File Offset: 0x0018109B
	private void OnEnable()
	{
		this.showList();
	}

	// Token: 0x0600335F RID: 13151 RVA: 0x00182EA4 File Offset: 0x001810A4
	internal void showList()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		for (int i = 0; i < this._elements.Count; i++)
		{
			Object.Destroy(this._elements[i].gameObject);
		}
		this._elements.Clear();
		foreach (AchievementGroupAsset tAchievementGroup in AssetManager.achievement_groups.list)
		{
			this.showElement(tAchievementGroup);
		}
		this.updateTotalBar();
	}

	// Token: 0x06003360 RID: 13152 RVA: 0x00182F40 File Offset: 0x00181140
	private void updateTotalBar()
	{
		int tMax = AssetManager.achievements.list.Count;
		int tUnlocked = AchievementLibrary.countUnlocked();
		this.achievementBar.setBar((float)tUnlocked, (float)tMax, "/" + tMax.ToText(), true, false, true, 0.3f);
	}

	// Token: 0x06003361 RID: 13153 RVA: 0x00182F8C File Offset: 0x0018118C
	private void showElement(AchievementGroupAsset pAchievementGroup)
	{
		AchievementGroup tElement = Object.Instantiate<AchievementGroup>(this.achievementGroupPrefab, this.transformContent);
		tElement.showGroup(pAchievementGroup);
		this._elements.Add(tElement);
	}

	// Token: 0x040026F9 RID: 9977
	public AchievementGroup achievementGroupPrefab;

	// Token: 0x040026FA RID: 9978
	private List<AchievementGroup> _elements = new List<AchievementGroup>();

	// Token: 0x040026FB RID: 9979
	public Transform transformContent;

	// Token: 0x040026FC RID: 9980
	public StatBar achievementBar;
}
