using System;
using ai.behaviours;

// Token: 0x020003B9 RID: 953
public class BehActorBuildingTarget : BehaviourActionActor
{
	// Token: 0x0600222C RID: 8748 RVA: 0x0011F918 File Offset: 0x0011DB18
	protected override void setupErrorChecks()
	{
		base.setupErrorChecks();
		this.null_check_building_target = true;
	}
}
