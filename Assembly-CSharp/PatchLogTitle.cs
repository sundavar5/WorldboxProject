using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005F0 RID: 1520
public class PatchLogTitle : MonoBehaviour
{
	// Token: 0x060031B5 RID: 12725 RVA: 0x0017BBCD File Offset: 0x00179DCD
	public void setUnfolded()
	{
		this._background.sprite = this._bg_unfolded;
	}

	// Token: 0x060031B6 RID: 12726 RVA: 0x0017BBE0 File Offset: 0x00179DE0
	public void setFolded()
	{
		this._background.sprite = this._bg_folded;
	}

	// Token: 0x04002593 RID: 9619
	[SerializeField]
	private Image _background;

	// Token: 0x04002594 RID: 9620
	[SerializeField]
	private Sprite _bg_folded;

	// Token: 0x04002595 RID: 9621
	[SerializeField]
	private Sprite _bg_unfolded;

	// Token: 0x04002596 RID: 9622
	public Image icon_left;

	// Token: 0x04002597 RID: 9623
	public Image icon_right;

	// Token: 0x04002598 RID: 9624
	public Text title;
}
