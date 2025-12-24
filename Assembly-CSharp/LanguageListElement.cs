using System;
using UnityEngine.UI;

// Token: 0x0200070E RID: 1806
public class LanguageListElement : WindowListElementBase<Language, LanguageData>
{
	// Token: 0x060039C0 RID: 14784 RVA: 0x0019AFD4 File Offset: 0x001991D4
	internal override void show(Language pLanguage)
	{
		base.show(pLanguage);
		this.text_name.text = pLanguage.name;
		this.text_name.color = pLanguage.getColor().getColorText();
		this.text_age.setValue(pLanguage.getAge(), "");
		this.text_population.setValue(pLanguage.countUnits(), "");
		this.text_villages.setValue(pLanguage.countCities(), "");
		this.text_kingdom.setValue(pLanguage.countKingdoms(), "");
		this.text_books.setValue(pLanguage.books.count(), "");
	}

	// Token: 0x060039C1 RID: 14785 RVA: 0x0019B082 File Offset: 0x00199282
	protected override void tooltipAction()
	{
		Tooltip.show(this, "language", new TooltipData
		{
			language = this.meta_object
		});
	}

	// Token: 0x060039C2 RID: 14786 RVA: 0x0019B0A0 File Offset: 0x001992A0
	protected override ActorAsset getActorAsset()
	{
		return this.meta_object.getActorAsset();
	}

	// Token: 0x04002AC8 RID: 10952
	public Text text_name;

	// Token: 0x04002AC9 RID: 10953
	public CountUpOnClick text_age;

	// Token: 0x04002ACA RID: 10954
	public CountUpOnClick text_population;

	// Token: 0x04002ACB RID: 10955
	public CountUpOnClick text_books;

	// Token: 0x04002ACC RID: 10956
	public CountUpOnClick text_villages;

	// Token: 0x04002ACD RID: 10957
	public CountUpOnClick text_kingdom;
}
