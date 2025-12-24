using System;

// Token: 0x02000751 RID: 1873
public class SelectedFamily : SelectedMetaWithUnit<Family, FamilyData>
{
	// Token: 0x17000365 RID: 869
	// (get) Token: 0x06003B40 RID: 15168 RVA: 0x001A0358 File Offset: 0x0019E558
	protected override MetaType meta_type
	{
		get
		{
			return MetaType.Family;
		}
	}

	// Token: 0x17000366 RID: 870
	// (get) Token: 0x06003B41 RID: 15169 RVA: 0x001A035B File Offset: 0x0019E55B
	public override string unit_title_locale_key
	{
		get
		{
			return "titled_alpha";
		}
	}

	// Token: 0x06003B42 RID: 15170 RVA: 0x001A0362 File Offset: 0x0019E562
	public override bool hasUnit()
	{
		return !this.nano_object.getAlpha().isRekt();
	}

	// Token: 0x06003B43 RID: 15171 RVA: 0x001A0377 File Offset: 0x0019E577
	public override Actor getUnit()
	{
		return this.nano_object.getAlpha();
	}

	// Token: 0x06003B44 RID: 15172 RVA: 0x001A0384 File Offset: 0x0019E584
	protected override string getPowerTabAssetID()
	{
		return "selected_family";
	}

	// Token: 0x06003B45 RID: 15173 RVA: 0x001A038B File Offset: 0x0019E58B
	public void openPeopleTab()
	{
		ScrollWindow.showWindow(base.window_id);
		ScrollWindow.getCurrentWindow().tabs.showTab("People");
	}
}
