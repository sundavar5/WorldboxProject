using System;
using System.ComponentModel;

// Token: 0x02000030 RID: 48
[Serializable]
public class CitizenJobAsset : Asset
{
	// Token: 0x040001C9 RID: 457
	public string path_icon;

	// Token: 0x040001CA RID: 458
	public int priority;

	// Token: 0x040001CB RID: 459
	public int priority_no_food;

	// Token: 0x040001CC RID: 460
	[DefaultValue(true)]
	public bool common_job = true;

	// Token: 0x040001CD RID: 461
	[DefaultValue(true)]
	public bool ok_for_king = true;

	// Token: 0x040001CE RID: 462
	[DefaultValue(true)]
	public bool ok_for_leader = true;

	// Token: 0x040001CF RID: 463
	public bool only_leaders;

	// Token: 0x040001D0 RID: 464
	public CitizenJobCondition should_be_assigned;

	// Token: 0x040001D1 RID: 465
	public string unit_job_default;

	// Token: 0x040001D2 RID: 466
	public DebugOption debug_option;
}
