using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000654 RID: 1620
public class CityElementNeutralHandler : CityElement
{
	// Token: 0x06003487 RID: 13447 RVA: 0x001860D7 File Offset: 0x001842D7
	private void checkNeutralElements()
	{
		if (this.meta_object.isNeutral())
		{
			this._layout_element_content_meta.SetActive(false);
			this._layout_element_wants.SetActive(false);
			this._layout_element_ruler.SetActive(false);
		}
	}

	// Token: 0x06003488 RID: 13448 RVA: 0x0018610A File Offset: 0x0018430A
	protected override IEnumerator showContent()
	{
		this.checkNeutralElements();
		return base.showContent();
	}

	// Token: 0x04002799 RID: 10137
	[SerializeField]
	private GameObject _layout_element_content_meta;

	// Token: 0x0400279A RID: 10138
	[SerializeField]
	private GameObject _layout_element_wants;

	// Token: 0x0400279B RID: 10139
	[SerializeField]
	private GameObject _layout_element_ruler;
}
