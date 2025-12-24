using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000633 RID: 1587
public class ArmyListElement : WindowListElementBase<Army, ArmyData>
{
	// Token: 0x060033B7 RID: 13239 RVA: 0x00183CB4 File Offset: 0x00181EB4
	internal override void show(Army pArmy)
	{
		base.show(pArmy);
		this._text_name.text = pArmy.name;
		Color tColor = pArmy.getColor().getColorText();
		this._text_name.color = tColor;
		bool tHasCaptain = pArmy.hasCaptain();
		this._captain.gameObject.SetActive(tHasCaptain);
		if (tHasCaptain)
		{
			this._captain.show(pArmy.getCaptain());
		}
		this._amount.setValue(pArmy.countUnits(), "");
		this._age.setValue(pArmy.getAge(), "");
		this._renown.setValue(pArmy.getRenown(), "");
		this._kills.setValue((int)pArmy.getTotalKills(), "");
		this._deaths.setValue((int)pArmy.getTotalDeaths(), "");
	}

	// Token: 0x060033B8 RID: 13240 RVA: 0x00183D8D File Offset: 0x00181F8D
	protected override void initMonoFields()
	{
	}

	// Token: 0x060033B9 RID: 13241 RVA: 0x00183D8F File Offset: 0x00181F8F
	protected override void loadBanner()
	{
		this._army_banner.load(this.meta_object);
	}

	// Token: 0x060033BA RID: 13242 RVA: 0x00183DA2 File Offset: 0x00181FA2
	protected override void tooltipAction()
	{
		Tooltip.show(this, "army", new TooltipData
		{
			army = this.meta_object
		});
	}

	// Token: 0x060033BB RID: 13243 RVA: 0x00183DC0 File Offset: 0x00181FC0
	protected override ActorAsset getActorAsset()
	{
		return this.meta_object.getActorAsset();
	}

	// Token: 0x0400271F RID: 10015
	[SerializeField]
	private Text _text_name;

	// Token: 0x04002720 RID: 10016
	[SerializeField]
	private CountUpOnClick _amount;

	// Token: 0x04002721 RID: 10017
	[SerializeField]
	private CountUpOnClick _age;

	// Token: 0x04002722 RID: 10018
	[SerializeField]
	private CountUpOnClick _renown;

	// Token: 0x04002723 RID: 10019
	[SerializeField]
	private CountUpOnClick _kills;

	// Token: 0x04002724 RID: 10020
	[SerializeField]
	private CountUpOnClick _deaths;

	// Token: 0x04002725 RID: 10021
	[SerializeField]
	private UiUnitAvatarElement _captain;

	// Token: 0x04002726 RID: 10022
	[SerializeField]
	private ArmyBanner _army_banner;
}
