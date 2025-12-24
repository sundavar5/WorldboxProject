using System;
using UnityEngine;

// Token: 0x02000717 RID: 1815
public class EmptyLogElement : MonoBehaviour
{
	// Token: 0x060039E3 RID: 14819 RVA: 0x0019B3A9 File Offset: 0x001995A9
	public void load(WorldLogMessage pMessage)
	{
		this._message = pMessage;
	}

	// Token: 0x060039E4 RID: 14820 RVA: 0x0019B3B2 File Offset: 0x001995B2
	public void setElement(WorldLogElement pElement)
	{
		this._element = pElement;
		if (this._element == null)
		{
			return;
		}
		this._element.showMessage(this._message);
		pElement.transform.SetParent(base.transform);
	}

	// Token: 0x060039E5 RID: 14821 RVA: 0x0019B3EC File Offset: 0x001995EC
	public WorldLogElement getElement()
	{
		return this._element;
	}

	// Token: 0x04002ACF RID: 10959
	private WorldLogElement _log_element;

	// Token: 0x04002AD0 RID: 10960
	public RectTransform rect_transform;

	// Token: 0x04002AD1 RID: 10961
	private WorldLogElement _element;

	// Token: 0x04002AD2 RID: 10962
	private WorldLogMessage _message;
}
