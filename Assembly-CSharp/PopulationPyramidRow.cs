using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000730 RID: 1840
public class PopulationPyramidRow : MonoBehaviour
{
	// Token: 0x06003A9A RID: 15002 RVA: 0x0019E50F File Offset: 0x0019C70F
	private void Start()
	{
		base.gameObject.AddOrGetComponent<Button>().onClick.AddListener(new UnityAction(this.animateBars));
		this.setupTooltip();
	}

	// Token: 0x06003A9B RID: 15003 RVA: 0x0019E538 File Offset: 0x0019C738
	private void setupTooltip()
	{
		TipButton tTipButton;
		if (!base.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		tTipButton.setHoverAction(new TooltipAction(this.showTooltip), false);
	}

	// Token: 0x06003A9C RID: 15004 RVA: 0x0019E564 File Offset: 0x0019C764
	private void showTooltip()
	{
		CustomDataContainer<string> tCustomDataString = new CustomDataContainer<string>();
		tCustomDataString["age_range"] = this._age_group_min.ToString() + " - " + this._age_group_max.ToString();
		CustomDataContainer<int> tCustomDataInt = new CustomDataContainer<int>();
		tCustomDataInt["males"] = this._left_item.getCount();
		tCustomDataInt["females"] = this._right_item.getCount();
		Tooltip.show(base.gameObject, "gender_data", new TooltipData
		{
			custom_data_string = tCustomDataString,
			custom_data_int = tCustomDataInt
		});
	}

	// Token: 0x06003A9D RID: 15005 RVA: 0x0019E5F7 File Offset: 0x0019C7F7
	private void animateBars()
	{
		this._left_item.animateBar();
		this._right_item.animateBar();
	}

	// Token: 0x06003A9E RID: 15006 RVA: 0x0019E610 File Offset: 0x0019C810
	internal void setAgeGroup(int pAgeGroup, int pAgeGroupMax)
	{
		this._age_group_min = pAgeGroup;
		this._age_group_max = pAgeGroupMax;
		this._text.text = pAgeGroup.ToString();
		float tOpacity = 0.75f + (float)pAgeGroup / 400f;
		tOpacity = Mathf.Clamp(tOpacity, 0.75f, 1f);
		this._left_item.setOpacity(tOpacity);
		this._right_item.setOpacity(tOpacity);
	}

	// Token: 0x06003A9F RID: 15007 RVA: 0x0019E678 File Offset: 0x0019C878
	internal void setColorTextBasedOnAmount(int pAmount)
	{
		if (pAmount == 0)
		{
			this._text.color = new Color(1f, 1f, 1f, 0.3f);
			return;
		}
		this._text.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x06003AA0 RID: 15008 RVA: 0x0019E6D4 File Offset: 0x0019C8D4
	internal void setMaleCount(int pCount, int pMax)
	{
		this._left_item.setCount(pCount, pMax);
		if (pCount == 0)
		{
			this._left_icon.color = new Color(1f, 1f, 1f, 0.3f);
			return;
		}
		this._left_icon.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x06003AA1 RID: 15009 RVA: 0x0019E73C File Offset: 0x0019C93C
	internal void setFemaleCount(int pCount, int pMax)
	{
		this._right_item.setCount(pCount, pMax);
		if (pCount == 0)
		{
			this._right_icon.color = new Color(1f, 1f, 1f, 0.3f);
			return;
		}
		this._right_icon.color = new Color(1f, 1f, 1f, 1f);
	}

	// Token: 0x04002B44 RID: 11076
	[SerializeField]
	private Image _left_icon;

	// Token: 0x04002B45 RID: 11077
	[SerializeField]
	private Image _right_icon;

	// Token: 0x04002B46 RID: 11078
	[SerializeField]
	private PopulationPyramidItem _left_item;

	// Token: 0x04002B47 RID: 11079
	[SerializeField]
	private PopulationPyramidItem _right_item;

	// Token: 0x04002B48 RID: 11080
	[SerializeField]
	private Text _text;

	// Token: 0x04002B49 RID: 11081
	private int _age_group_min;

	// Token: 0x04002B4A RID: 11082
	private int _age_group_max;
}
