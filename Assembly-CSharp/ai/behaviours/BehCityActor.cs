using System;

namespace ai.behaviours
{
	// Token: 0x0200089F RID: 2207
	public class BehCityActor : BehaviourActionActor
	{
		// Token: 0x0600448B RID: 17547 RVA: 0x001CE2BC File Offset: 0x001CC4BC
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_city = true;
		}
	}
}
