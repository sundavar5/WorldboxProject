using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000644 RID: 1604
public class RainSwitcherButton : MonoBehaviour
{
	// Token: 0x06003445 RID: 13381 RVA: 0x0018563C File Offset: 0x0018383C
	public void toggleState(bool pState)
	{
		if (pState)
		{
			this._background.sprite = this._enabled;
			this._icon.color = ColorStyleLibrary.m.favorite_selected;
			return;
		}
		this._background.sprite = this._disabled;
		this._icon.color = ColorStyleLibrary.m.favorite_not_selected;
	}

	// Token: 0x06003446 RID: 13382 RVA: 0x00185699 File Offset: 0x00183899
	public Button getButton()
	{
		return this._button;
	}

	// Token: 0x0400276C RID: 10092
	[SerializeField]
	private Image _icon;

	// Token: 0x0400276D RID: 10093
	[SerializeField]
	private Image _background;

	// Token: 0x0400276E RID: 10094
	[SerializeField]
	private Button _button;

	// Token: 0x0400276F RID: 10095
	[SerializeField]
	private Sprite _enabled;

	// Token: 0x04002770 RID: 10096
	[SerializeField]
	private Sprite _disabled;
}
