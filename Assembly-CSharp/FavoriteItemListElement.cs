using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000698 RID: 1688
public class FavoriteItemListElement : WindowListElementBase<Item, ItemData>
{
	// Token: 0x06003609 RID: 13833 RVA: 0x0018A62C File Offset: 0x0018882C
	internal override void show(Item pItem)
	{
		base.show(pItem);
		this.clear();
		this.name_text.text = pItem.getName(true);
		this.name_text.color = Toolbox.makeColor(pItem.getQualityColor());
		this.kills_text.setValue(pItem.data.kills, "");
		this.age_text.setValue(pItem.getAge(), "");
		this.damage_text.setValue((int)pItem.getFullStats()["damage"], "");
		this.armor_text.setValue((int)pItem.getFullStats()["armor"], "");
		this.durability_text.setValue(pItem.getDurabilityCurrent(), "");
		if (pItem.hasActor())
		{
			this._unit_avatar_element.gameObject.SetActive(true);
			this._unit_avatar_element.show(pItem.getActor());
			return;
		}
		if (pItem.hasCity())
		{
			this._banner_city.gameObject.SetActive(true);
			this._banner_city.load(pItem.getCity());
			return;
		}
		this._ownerless.SetActive(true);
	}

	// Token: 0x0600360A RID: 13834 RVA: 0x0018A759 File Offset: 0x00188959
	protected override void tooltipAction()
	{
		Tooltip.show(this, "equipment", new TooltipData
		{
			item = this.meta_object
		});
	}

	// Token: 0x0600360B RID: 13835 RVA: 0x0018A777 File Offset: 0x00188977
	private void clear()
	{
		this._unit_avatar_element.gameObject.SetActive(false);
		this._banner_city.gameObject.SetActive(false);
		this._ownerless.SetActive(false);
	}

	// Token: 0x04002810 RID: 10256
	public Text name_text;

	// Token: 0x04002811 RID: 10257
	public CountUpOnClick kills_text;

	// Token: 0x04002812 RID: 10258
	public CountUpOnClick age_text;

	// Token: 0x04002813 RID: 10259
	public CountUpOnClick owners_text;

	// Token: 0x04002814 RID: 10260
	public CountUpOnClick damage_text;

	// Token: 0x04002815 RID: 10261
	public CountUpOnClick armor_text;

	// Token: 0x04002816 RID: 10262
	public CountUpOnClick durability_text;

	// Token: 0x04002817 RID: 10263
	[SerializeField]
	private UiUnitAvatarElement _unit_avatar_element;

	// Token: 0x04002818 RID: 10264
	[SerializeField]
	private CityBanner _banner_city;

	// Token: 0x04002819 RID: 10265
	[SerializeField]
	private GameObject _ownerless;

	// Token: 0x0400281A RID: 10266
	private IconOutline _outline;
}
