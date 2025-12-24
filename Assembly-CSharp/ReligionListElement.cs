using System;
using UnityEngine.UI;

// Token: 0x0200073E RID: 1854
public class ReligionListElement : WindowListElementBase<Religion, ReligionData>
{
	// Token: 0x06003AE4 RID: 15076 RVA: 0x0019F820 File Offset: 0x0019DA20
	internal override void show(Religion pReligion)
	{
		base.show(pReligion);
		this.text_name.text = pReligion.name;
		this.text_name.color = pReligion.getColor().getColorText();
		this.text_age.setValue(pReligion.getAge(), "");
		this.text_population.setValue(pReligion.countUnits(), "");
		this.text_villages.setValue(pReligion.countCities(), "");
		this.text_kingdom.setValue(pReligion.countKingdoms(), "");
		this.text_renown.setValue(pReligion.getRenown(), "");
	}

	// Token: 0x06003AE5 RID: 15077 RVA: 0x0019F8C9 File Offset: 0x0019DAC9
	protected override void tooltipAction()
	{
		Tooltip.show(this, "religion", new TooltipData
		{
			religion = this.meta_object
		});
	}

	// Token: 0x06003AE6 RID: 15078 RVA: 0x0019F8E7 File Offset: 0x0019DAE7
	protected override ActorAsset getActorAsset()
	{
		return this.meta_object.getActorAsset();
	}

	// Token: 0x04002B83 RID: 11139
	public Text text_name;

	// Token: 0x04002B84 RID: 11140
	public CountUpOnClick text_age;

	// Token: 0x04002B85 RID: 11141
	public CountUpOnClick text_population;

	// Token: 0x04002B86 RID: 11142
	public CountUpOnClick text_renown;

	// Token: 0x04002B87 RID: 11143
	public CountUpOnClick text_villages;

	// Token: 0x04002B88 RID: 11144
	public CountUpOnClick text_kingdom;
}
