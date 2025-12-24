using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000692 RID: 1682
public class FamilyListElement : WindowListElementBase<Family, FamilyData>
{
	// Token: 0x060035D8 RID: 13784 RVA: 0x00189BE8 File Offset: 0x00187DE8
	internal override void show(Family pFamily)
	{
		base.show(pFamily);
		this.text_name.text = pFamily.name;
		this.text_name.color = pFamily.getColor().getColorText();
		this.text_age.setValue(pFamily.getAge(), "");
		this.text_population.setValue(pFamily.countUnits(), "");
		this.text_adults.setValue(pFamily.countAdults(), "");
		this.text_children.setValue(pFamily.countChildren(), "");
		this.text_dead.setValue((int)pFamily.getTotalDeaths(), "");
		string tTerm = LocalizedTextManager.getText(pFamily.getActorAsset().getCollectiveTermID(), null, false);
		this._collective_term.text = tTerm;
	}

	// Token: 0x060035D9 RID: 13785 RVA: 0x00189CB1 File Offset: 0x00187EB1
	protected override void tooltipAction()
	{
		Tooltip.show(this, "family", new TooltipData
		{
			family = this.meta_object
		});
	}

	// Token: 0x060035DA RID: 13786 RVA: 0x00189CCF File Offset: 0x00187ECF
	protected override ActorAsset getActorAsset()
	{
		return this.meta_object.getActorAsset();
	}

	// Token: 0x040027FB RID: 10235
	public Text text_name;

	// Token: 0x040027FC RID: 10236
	public CountUpOnClick text_age;

	// Token: 0x040027FD RID: 10237
	public CountUpOnClick text_population;

	// Token: 0x040027FE RID: 10238
	public CountUpOnClick text_adults;

	// Token: 0x040027FF RID: 10239
	public CountUpOnClick text_children;

	// Token: 0x04002800 RID: 10240
	public CountUpOnClick text_dead;

	// Token: 0x04002801 RID: 10241
	[SerializeField]
	private Text _collective_term;
}
