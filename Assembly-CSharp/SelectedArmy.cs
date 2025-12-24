using System;

// Token: 0x0200074C RID: 1868
public class SelectedArmy : SelectedMetaWithUnit<Army, ArmyData>
{
	// Token: 0x1700035E RID: 862
	// (get) Token: 0x06003B19 RID: 15129 RVA: 0x0019FF1A File Offset: 0x0019E11A
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Army;
		}
	}

	// Token: 0x1700035F RID: 863
	// (get) Token: 0x06003B1A RID: 15130 RVA: 0x0019FF1E File Offset: 0x0019E11E
	public override string unit_title_locale_key
	{
		get
		{
			return "titled_captain";
		}
	}

	// Token: 0x06003B1B RID: 15131 RVA: 0x0019FF25 File Offset: 0x0019E125
	public override bool hasUnit()
	{
		return !this.nano_object.getCaptain().isRekt();
	}

	// Token: 0x06003B1C RID: 15132 RVA: 0x0019FF3A File Offset: 0x0019E13A
	public override Actor getUnit()
	{
		return this.nano_object.getCaptain();
	}

	// Token: 0x06003B1D RID: 15133 RVA: 0x0019FF47 File Offset: 0x0019E147
	protected override string getPowerTabAssetID()
	{
		return "selected_army";
	}

	// Token: 0x06003B1E RID: 15134 RVA: 0x0019FF50 File Offset: 0x0019E150
	protected override void showStatsGeneral(Army pArmy)
	{
		base.showStatsGeneral(pArmy);
		base.setIconValue("i_army_size", (float)pArmy.countUnits(), null, "", false, "", '/');
		base.setIconValue("i_money", (float)pArmy.countTotalMoney(), null, "", false, "", '/');
		base.setIconValue("i_melee", (float)pArmy.countMelee(), null, "", false, "", '/');
		base.setIconValue("i_range", (float)pArmy.countRange(), null, "", false, "", '/');
	}
}
