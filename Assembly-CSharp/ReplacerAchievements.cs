using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006A1 RID: 1697
public class ReplacerAchievements : MonoBehaviour
{
	// Token: 0x0600365C RID: 13916 RVA: 0x0018B72D File Offset: 0x0018992D
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (SmoothLoader.isLoading())
		{
			return;
		}
		this.checkIcon();
	}

	// Token: 0x0600365D RID: 13917 RVA: 0x0018B745 File Offset: 0x00189945
	private void Start()
	{
		this.checkIcon();
	}

	// Token: 0x0600365E RID: 13918 RVA: 0x0018B74D File Offset: 0x0018994D
	private void checkIcon()
	{
		if (AchievementLibrary.isAllUnlocked())
		{
			this._target_icon.sprite = this.icon_gold;
			return;
		}
		this._target_icon.sprite = this.icon_silver;
	}

	// Token: 0x04002847 RID: 10311
	[SerializeField]
	private Image _target_icon;

	// Token: 0x04002848 RID: 10312
	public Sprite icon_gold;

	// Token: 0x04002849 RID: 10313
	public Sprite icon_silver;

	// Token: 0x0400284A RID: 10314
	private BuildingAsset _asset;
}
