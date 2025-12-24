using System;

// Token: 0x020003BA RID: 954
public class BehActorUsableBuildingTarget : BehActorBuildingTarget
{
	// Token: 0x0600222E RID: 8750 RVA: 0x0011F92F File Offset: 0x0011DB2F
	protected override void setupErrorChecks()
	{
		base.setupErrorChecks();
		this.check_building_target_non_usable = true;
	}
}
