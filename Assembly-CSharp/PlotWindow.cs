using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200072B RID: 1835
public class PlotWindow : WindowMetaGeneric<Plot, PlotData>
{
	// Token: 0x17000349 RID: 841
	// (get) Token: 0x06003A71 RID: 14961 RVA: 0x0019DC0E File Offset: 0x0019BE0E
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Plot;
		}
	}

	// Token: 0x1700034A RID: 842
	// (get) Token: 0x06003A72 RID: 14962 RVA: 0x0019DC12 File Offset: 0x0019BE12
	protected override Plot meta_object
	{
		get
		{
			return SelectedMetas.selected_plot;
		}
	}

	// Token: 0x06003A73 RID: 14963 RVA: 0x0019DC19 File Offset: 0x0019BE19
	public override void startShowingWindow()
	{
		base.startShowingWindow();
		AchievementLibrary.plots_explorer.check(null);
	}

	// Token: 0x06003A74 RID: 14964 RVA: 0x0019DC30 File Offset: 0x0019BE30
	protected override void showTopPartInformation()
	{
		Plot tPlot = this.meta_object;
		if (tPlot == null)
		{
			return;
		}
		if (tPlot.getAsset().needs_to_be_explored)
		{
			tPlot.getAsset().unlock(true);
		}
		float tCurrentProgress = tPlot.getProgress();
		float tNeededProgress = tPlot.getProgressMax();
		this.bar.setBar(tCurrentProgress, tNeededProgress, "/" + tNeededProgress.ToText(), true, true, true, 0.3f);
		this.text_description.text = tPlot.getAsset().get_formatted_description(tPlot);
		this.text_description.GetComponent<LocalizedText>().checkTextFont(null);
		this.text_description.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		this.showAuthor();
	}

	// Token: 0x06003A75 RID: 14965 RVA: 0x0019DCDC File Offset: 0x0019BEDC
	private void showAuthor()
	{
		Actor tAuthor = this.meta_object.getAuthor();
		if (tAuthor.isRekt())
		{
			this.content_author.SetActive(false);
			return;
		}
		this.content_author.SetActive(true);
		this.author_element.show(tAuthor);
	}

	// Token: 0x06003A76 RID: 14966 RVA: 0x0019DD24 File Offset: 0x0019BF24
	internal override void showStatsRows()
	{
		Plot tPlot = this.meta_object;
		if (tPlot == null)
		{
			return;
		}
		base.tryShowPastNames();
		base.showStatRow("started_by", tPlot.data.founder_name, MetaType.Unit, tPlot.data.founder_id, null, null, null);
		base.showStatRow("started_at", tPlot.getFoundedDate(), MetaType.None, -1L, null, null, null);
	}

	// Token: 0x04002B26 RID: 11046
	[SerializeField]
	private GameObject content_author;

	// Token: 0x04002B27 RID: 11047
	[SerializeField]
	private PrefabUnitElement author_element;

	// Token: 0x04002B28 RID: 11048
	public StatsIcon text_info_age;

	// Token: 0x04002B29 RID: 11049
	public StatsIcon text_info_members;

	// Token: 0x04002B2A RID: 11050
	public StatsIcon text_info_power;

	// Token: 0x04002B2B RID: 11051
	public StatsIcon text_info_dead;

	// Token: 0x04002B2C RID: 11052
	public Text text_description;

	// Token: 0x04002B2D RID: 11053
	public StatBar bar;
}
