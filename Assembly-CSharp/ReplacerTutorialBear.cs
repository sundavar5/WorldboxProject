using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006A2 RID: 1698
public class ReplacerTutorialBear : MonoBehaviour
{
	// Token: 0x06003660 RID: 13920 RVA: 0x0018B784 File Offset: 0x00189984
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
		if (this._asset == null)
		{
			this._asset = AssetManager.buildings.get("monolith");
		}
		if (this._asset.buildings.Count > 0)
		{
			this._target_icon.sprite = this.icon_civ;
			return;
		}
		this._target_icon.sprite = this.icon_animal;
	}

	// Token: 0x0400284B RID: 10315
	[SerializeField]
	private Image _target_icon;

	// Token: 0x0400284C RID: 10316
	public Sprite icon_animal;

	// Token: 0x0400284D RID: 10317
	public Sprite icon_civ;

	// Token: 0x0400284E RID: 10318
	private BuildingAsset _asset;
}
