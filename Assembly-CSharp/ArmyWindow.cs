using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000638 RID: 1592
public class ArmyWindow : WindowMetaGeneric<Army, ArmyData>
{
	// Token: 0x170002CD RID: 717
	// (get) Token: 0x060033D8 RID: 13272 RVA: 0x0018419C File Offset: 0x0018239C
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Army;
		}
	}

	// Token: 0x170002CE RID: 718
	// (get) Token: 0x060033D9 RID: 13273 RVA: 0x001841A0 File Offset: 0x001823A0
	protected override Army meta_object
	{
		get
		{
			return SelectedMetas.selected_army;
		}
	}

	// Token: 0x060033DA RID: 13274 RVA: 0x001841A8 File Offset: 0x001823A8
	protected override void showTopPartInformation()
	{
		base.showTopPartInformation();
		Army tArmy = this.meta_object;
		if (tArmy == null)
		{
			return;
		}
		this._race_top_left.sprite = tArmy.getSpriteIcon();
		this._race_top_right.sprite = tArmy.getSpriteIcon();
	}

	// Token: 0x060033DB RID: 13275 RVA: 0x001841E8 File Offset: 0x001823E8
	internal override void showStatsRows()
	{
		Army tArmy = this.meta_object;
		if (tArmy == null)
		{
			return;
		}
		base.tryShowPastNames();
		base.showStatRow("founded", tArmy.getFoundedDate(), MetaType.None, -1L, "iconAge", null, null);
		base.showStatRow("males", tArmy.countMales(), MetaType.None, -1L, "iconMale", null, null);
		base.showStatRow("females", tArmy.countFemales(), MetaType.None, -1L, "iconFemale", null, null);
		base.showStatRow("deaths", tArmy.getTotalDeaths(), MetaType.None, -1L, "iconDead", null, null);
		base.showStatRow("kills", tArmy.getTotalKills(), MetaType.None, -1L, "iconKills", null, null);
		base.tryToShowActor("captain", -1L, null, tArmy.getCaptain(), "iconCaptain");
		this.tryShowPastCaptains();
		base.tryToShowMetaCity("village", -1L, null, tArmy.getCity(), "iconCity");
		base.tryToShowMetaKingdom("kingdom", -1L, null, tArmy.getKingdom());
	}

	// Token: 0x060033DC RID: 13276 RVA: 0x001842F4 File Offset: 0x001824F4
	private void tryShowPastCaptains()
	{
		List<LeaderEntry> past_captains = this.meta_object.data.past_captains;
		if (past_captains != null && past_captains.Count > 1)
		{
			base.showStatRow("past_captains", this.meta_object.data.past_captains.Count, MetaType.None, -1L, "iconCaptain", "past_rulers", new TooltipDataGetter(this.getTooltipPastCaptains));
		}
	}

	// Token: 0x060033DD RID: 13277 RVA: 0x00184361 File Offset: 0x00182561
	private TooltipData getTooltipPastCaptains()
	{
		return new TooltipData
		{
			tip_name = "past_captains",
			meta_type = MetaType.Army,
			past_rulers = new ListPool<LeaderEntry>(this.meta_object.data.past_captains)
		};
	}

	// Token: 0x04002738 RID: 10040
	[SerializeField]
	private Image _race_top_left;

	// Token: 0x04002739 RID: 10041
	[SerializeField]
	private Image _race_top_right;
}
