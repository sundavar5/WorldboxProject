using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000729 RID: 1833
public class PlotListElement : WindowListElementBase<Plot, PlotData>
{
	// Token: 0x06003A69 RID: 14953 RVA: 0x0019DA54 File Offset: 0x0019BC54
	internal override void show(Plot pPlot)
	{
		base.show(pPlot);
		Actor tActor = pPlot.getAuthor();
		this._avatar_loader.show(tActor);
		ColorAsset tColorAsset = null;
		if (tActor != null)
		{
			tColorAsset = tActor.kingdom.getColor();
		}
		if (tColorAsset != null)
		{
			this._text_name.color = tActor.kingdom.getColor().getColorText();
		}
		else
		{
			this._text_name.color = Toolbox.color_white;
		}
		this._text_name.text = pPlot.data.name;
		this._members.setValue(pPlot.getSupporters(), "");
		this._progress.setValue((int)pPlot.getProgress(), "/" + pPlot.getProgressMax().ToText());
		float tCurrentProgress = pPlot.getProgress();
		float tNeededProgress = pPlot.getProgressMax();
		this._bar.setBar(tCurrentProgress, tNeededProgress, "/" + tNeededProgress.ToText(), true, true, true, 0.3f);
		this._age.setValue(pPlot.getAge(), "");
		if (pPlot.getAsset().isAvailable())
		{
			this._locked_effect.gameObject.SetActive(false);
			return;
		}
		this._locked_effect.gameObject.SetActive(true);
	}

	// Token: 0x06003A6A RID: 14954 RVA: 0x0019DB88 File Offset: 0x0019BD88
	protected override void tooltipAction()
	{
		Tooltip.show(this, "plot", new TooltipData
		{
			plot = this.meta_object
		});
	}

	// Token: 0x04002B1C RID: 11036
	[SerializeField]
	private Text _text_name;

	// Token: 0x04002B1D RID: 11037
	[SerializeField]
	private CountUpOnClick _members;

	// Token: 0x04002B1E RID: 11038
	[SerializeField]
	private CountUpOnClick _age;

	// Token: 0x04002B1F RID: 11039
	[SerializeField]
	private CountUpOnClick _progress;

	// Token: 0x04002B20 RID: 11040
	[SerializeField]
	private UiUnitAvatarElement _avatar_loader;

	// Token: 0x04002B21 RID: 11041
	[SerializeField]
	private StatBar _bar;

	// Token: 0x04002B22 RID: 11042
	[SerializeField]
	private GameObject _locked_effect;
}
