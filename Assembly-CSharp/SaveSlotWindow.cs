using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000736 RID: 1846
public class SaveSlotWindow : MonoBehaviour
{
	// Token: 0x06003AC0 RID: 15040 RVA: 0x0019F0DC File Offset: 0x0019D2DC
	private void checkChildren()
	{
		if (this.previews.Count > 0)
		{
			return;
		}
		BoxPreview[] tPreviews = this.buttonsContainer.GetComponentsInChildren<BoxPreview>();
		this.previews.AddRange(tPreviews);
	}

	// Token: 0x06003AC1 RID: 15041 RVA: 0x0019F110 File Offset: 0x0019D310
	private void OnEnable()
	{
		this.checkChildren();
		this.prepareLoadPreviews();
	}

	// Token: 0x06003AC2 RID: 15042 RVA: 0x0019F120 File Offset: 0x0019D320
	private void prepareLoadPreviews()
	{
		SaveManager.clearCurrentSelectedWorld();
		for (int i = 0; i < this.previews.Count; i++)
		{
			this.previews[i].setSlot(i + 1);
		}
	}

	// Token: 0x04002B6E RID: 11118
	public GameObject buttonsContainer;

	// Token: 0x04002B6F RID: 11119
	private List<BoxPreview> previews = new List<BoxPreview>();

	// Token: 0x04002B70 RID: 11120
	public GameObject slotButtonPrefabNew;

	// Token: 0x04002B71 RID: 11121
	public ScrollRect scroll_rect;
}
