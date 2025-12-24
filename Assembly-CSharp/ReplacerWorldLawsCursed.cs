using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006A4 RID: 1700
public class ReplacerWorldLawsCursed : MonoBehaviour
{
	// Token: 0x06003665 RID: 13925 RVA: 0x0018B84B File Offset: 0x00189A4B
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (WorldLawLibrary.world_law_cursed_world.isEnabled())
		{
			this._target_icon.sprite = this.icon_world_cursed;
			return;
		}
		this._target_icon.sprite = this.icon_normal;
	}

	// Token: 0x04002852 RID: 10322
	[SerializeField]
	private Image _target_icon;

	// Token: 0x04002853 RID: 10323
	public Sprite icon_normal;

	// Token: 0x04002854 RID: 10324
	public Sprite icon_world_cursed;
}
