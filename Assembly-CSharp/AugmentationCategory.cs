using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200063C RID: 1596
public class AugmentationCategory<TAugmentation, TAugmentationButton, TAugmentationEditorButton> : MonoBehaviour where TAugmentation : BaseAugmentationAsset where TAugmentationButton : AugmentationButton<TAugmentation> where TAugmentationEditorButton : AugmentationEditorButton<TAugmentationButton, TAugmentation>
{
	// Token: 0x06003406 RID: 13318 RVA: 0x00184818 File Offset: 0x00182A18
	public void clearDebug()
	{
		for (int i = 0; i < this.augmentation_buttons_transform.childCount; i++)
		{
			Object.Destroy(this.augmentation_buttons_transform.GetChild(i).gameObject);
		}
	}

	// Token: 0x06003407 RID: 13319 RVA: 0x00184851 File Offset: 0x00182A51
	public void hideCounter()
	{
		this.counter.text = "";
		this.counter.gameObject.SetActive(false);
	}

	// Token: 0x06003408 RID: 13320 RVA: 0x00184874 File Offset: 0x00182A74
	public void updateCounter()
	{
		int tCounterTotalSelected = 0;
		using (List<TAugmentationEditorButton>.Enumerator enumerator = this.augmentation_buttons.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.augmentation_button.isSelected())
				{
					tCounterTotalSelected++;
				}
			}
		}
		string tTotalButtonsString = this.augmentation_buttons.Count.ToString();
		this.counter.text = string.Format("{0}/{1}", tCounterTotalSelected, tTotalButtonsString);
	}

	// Token: 0x06003409 RID: 13321 RVA: 0x0018490C File Offset: 0x00182B0C
	protected virtual bool isUnlocked(TAugmentationButton pButton)
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600340A RID: 13322 RVA: 0x00184913 File Offset: 0x00182B13
	private void LateUpdate()
	{
		this.updateValues();
	}

	// Token: 0x0600340B RID: 13323 RVA: 0x0018491C File Offset: 0x00182B1C
	private void updateValues()
	{
		Vector2 tSize = this.height.sizeDelta;
		tSize.y = this.augmentation_buttons_transform.GetComponent<RectTransform>().sizeDelta.y + 15f;
		this.height.sizeDelta = tSize;
	}

	// Token: 0x0600340C RID: 13324 RVA: 0x00184964 File Offset: 0x00182B64
	public int countActiveButtons()
	{
		int tResult = 0;
		using (List<TAugmentationEditorButton>.Enumerator enumerator = this.augmentation_buttons.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.gameObject.activeSelf)
				{
					tResult++;
				}
			}
		}
		return tResult;
	}

	// Token: 0x0600340D RID: 13325 RVA: 0x001849C8 File Offset: 0x00182BC8
	public bool hasAugmentation(TAugmentation pTrait)
	{
		using (List<TAugmentationEditorButton>.Enumerator enumerator = this.augmentation_buttons.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.augmentation_button.getElementAsset() == pTrait)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x04002746 RID: 10054
	public Text title;

	// Token: 0x04002747 RID: 10055
	public Text counter;

	// Token: 0x04002748 RID: 10056
	public RectTransform height;

	// Token: 0x04002749 RID: 10057
	public Transform augmentation_buttons_transform;

	// Token: 0x0400274A RID: 10058
	public BaseCategoryAsset asset;

	// Token: 0x0400274B RID: 10059
	public List<TAugmentationEditorButton> augmentation_buttons = new List<TAugmentationEditorButton>();
}
