using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200032A RID: 810
public class PossessionModeButton : MonoBehaviour
{
	// Token: 0x06001F44 RID: 8004 RVA: 0x0010F834 File Offset: 0x0010DA34
	public void updateGraphics(PossessionActionMode pCurrentSelectedMode)
	{
		if (this.mode == pCurrentSelectedMode)
		{
			this._image_icon.color = Color.white;
			this._image_background.color = Color.white;
			return;
		}
		this._image_icon.color = new Color(0.3f, 0.3f, 0.3f, 1f);
		this._image_background.color = Color.gray;
	}

	// Token: 0x040016D3 RID: 5843
	public PossessionActionMode mode;

	// Token: 0x040016D4 RID: 5844
	[SerializeField]
	private Image _image_icon;

	// Token: 0x040016D5 RID: 5845
	[SerializeField]
	private Image _image_background;
}
