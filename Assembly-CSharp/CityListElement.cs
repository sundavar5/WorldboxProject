using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000657 RID: 1623
public class CityListElement : WindowListElementBase<City, CityData>
{
	// Token: 0x06003499 RID: 13465 RVA: 0x00186370 File Offset: 0x00184570
	internal override void show(City pCity)
	{
		base.show(pCity);
		this.avatarLoader.show(pCity.leader);
		this._loyalty_element.setCity(pCity);
		this.text_name.text = pCity.name;
		this.text_name.color = pCity.kingdom.getColor().getColorText();
		this.population.setValue(pCity.getPopulationPeople(), "");
		this.army.setValue(pCity.countWarriors(), "");
		this.zones.setValue(pCity.zones.Count, "");
		int tLoyalty = pCity.getLoyalty(true);
		this._loyalty.setValue(tLoyalty, "");
		if (tLoyalty < 0)
		{
			this._loyalty.getText().color = Toolbox.color_negative_RGBA;
		}
		else
		{
			this._loyalty.getText().color = Toolbox.color_positive_RGBA;
		}
		this.age.setValue(pCity.getAge(), "");
		this.toggleCapital(pCity.isCapitalCity());
	}

	// Token: 0x0600349A RID: 13466 RVA: 0x0018647F File Offset: 0x0018467F
	protected override void initMonoFields()
	{
	}

	// Token: 0x0600349B RID: 13467 RVA: 0x00186481 File Offset: 0x00184681
	protected override void loadBanner()
	{
		this.city_banner.load(this.meta_object);
	}

	// Token: 0x0600349C RID: 13468 RVA: 0x00186494 File Offset: 0x00184694
	protected override void tooltipAction()
	{
		Tooltip.show(this, "city", new TooltipData
		{
			city = this.meta_object
		});
	}

	// Token: 0x0600349D RID: 13469 RVA: 0x001864B2 File Offset: 0x001846B2
	private void toggleCapital(bool pState)
	{
		GameObject icon_capital = this._icon_capital;
		if (icon_capital == null)
		{
			return;
		}
		icon_capital.SetActive(pState);
	}

	// Token: 0x0600349E RID: 13470 RVA: 0x001864C5 File Offset: 0x001846C5
	protected override ActorAsset getActorAsset()
	{
		return this.meta_object.getActorAsset();
	}

	// Token: 0x0400279E RID: 10142
	public Text text_name;

	// Token: 0x0400279F RID: 10143
	public CountUpOnClick population;

	// Token: 0x040027A0 RID: 10144
	public CountUpOnClick army;

	// Token: 0x040027A1 RID: 10145
	public CountUpOnClick zones;

	// Token: 0x040027A2 RID: 10146
	[SerializeField]
	private CountUpOnClick _loyalty;

	// Token: 0x040027A3 RID: 10147
	public CountUpOnClick age;

	// Token: 0x040027A4 RID: 10148
	public UiUnitAvatarElement avatarLoader;

	// Token: 0x040027A5 RID: 10149
	public CityBanner city_banner;

	// Token: 0x040027A6 RID: 10150
	[SerializeField]
	private GameObject _icon_capital;

	// Token: 0x040027A7 RID: 10151
	[SerializeField]
	private CityLoyaltyElement _loyalty_element;
}
