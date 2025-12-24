using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200076A RID: 1898
public class SubspeciesListElement : WindowListElementBase<Subspecies, SubspeciesData>
{
	// Token: 0x06003C3D RID: 15421 RVA: 0x001A3248 File Offset: 0x001A1448
	internal override void show(Subspecies pSubspecies)
	{
		base.show(pSubspecies);
		this.text_name.text = pSubspecies.name;
		this.text_name.color = pSubspecies.getColor().getColorText();
		this.text_age.setValue(pSubspecies.getAge(), "");
		this.text_population.setValue(pSubspecies.countUnits(), "");
		this.text_deaths.setValue((int)pSubspecies.getTotalDeaths(), "");
		this.text_children.setValue(pSubspecies.countChildren(), "");
		this.text_family.setValue(pSubspecies.countCurrentFamilies(), "");
		string tTerm = pSubspecies.getActorAsset().getTranslatedName();
		this._subspecies_name.text = tTerm;
	}

	// Token: 0x06003C3E RID: 15422 RVA: 0x001A330A File Offset: 0x001A150A
	protected override void tooltipAction()
	{
		Tooltip.show(this, "subspecies", new TooltipData
		{
			subspecies = this.meta_object
		});
	}

	// Token: 0x06003C3F RID: 15423 RVA: 0x001A3328 File Offset: 0x001A1528
	protected override ActorAsset getActorAsset()
	{
		return this.meta_object.getActorAsset();
	}

	// Token: 0x04002BD8 RID: 11224
	public Text text_name;

	// Token: 0x04002BD9 RID: 11225
	public CountUpOnClick text_age;

	// Token: 0x04002BDA RID: 11226
	public CountUpOnClick text_population;

	// Token: 0x04002BDB RID: 11227
	public CountUpOnClick text_children;

	// Token: 0x04002BDC RID: 11228
	public CountUpOnClick text_deaths;

	// Token: 0x04002BDD RID: 11229
	public CountUpOnClick text_family;

	// Token: 0x04002BDE RID: 11230
	[SerializeField]
	private Text _subspecies_name;
}
