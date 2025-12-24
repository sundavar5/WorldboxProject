using System;

// Token: 0x02000055 RID: 85
[Serializable]
public class MapGenSettingsAsset : Asset, ILocalizedAsset
{
	// Token: 0x06000327 RID: 807 RVA: 0x0001DAA4 File Offset: 0x0001BCA4
	public string getLocaleID()
	{
		return this.id;
	}

	// Token: 0x040002AF RID: 687
	public bool is_switch;

	// Token: 0x040002B0 RID: 688
	public int min_value;

	// Token: 0x040002B1 RID: 689
	public int max_value;

	// Token: 0x040002B2 RID: 690
	public MapGenSettingsDelegateBool allowed_check;

	// Token: 0x040002B3 RID: 691
	public MapGenSettingsDelegate increase;

	// Token: 0x040002B4 RID: 692
	public MapGenSettingsDelegate decrease;

	// Token: 0x040002B5 RID: 693
	public MapGenSettingsDelegateSwitch action_switch;

	// Token: 0x040002B6 RID: 694
	public MapGenSettingsDelegateGet action_get;

	// Token: 0x040002B7 RID: 695
	public MapGenSettingsDelegateSet action_set;
}
