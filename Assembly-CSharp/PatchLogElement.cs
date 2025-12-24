using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005ED RID: 1517
public class PatchLogElement : MonoBehaviour
{
	// Token: 0x060031A6 RID: 12710 RVA: 0x0017B570 File Offset: 0x00179770
	public void fold()
	{
		this._folded = true;
		this.title.setFolded();
		this.texts.gameObject.SetActive(false);
	}

	// Token: 0x060031A7 RID: 12711 RVA: 0x0017B595 File Offset: 0x00179795
	public void unfold()
	{
		this._folded = false;
		this.title.setUnfolded();
		this.texts.gameObject.SetActive(true);
	}

	// Token: 0x060031A8 RID: 12712 RVA: 0x0017B5BA File Offset: 0x001797BA
	public void toggleState()
	{
		if (this._folded)
		{
			this.unfold();
			return;
		}
		this.fold();
	}

	// Token: 0x04002581 RID: 9601
	public PatchLogTitle title;

	// Token: 0x04002582 RID: 9602
	public Text date;

	// Token: 0x04002583 RID: 9603
	public Text date_ago;

	// Token: 0x04002584 RID: 9604
	public Image background;

	// Token: 0x04002585 RID: 9605
	public GameObject texts;

	// Token: 0x04002586 RID: 9606
	public bool _folded;
}
