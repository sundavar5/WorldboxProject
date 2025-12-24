using System;

namespace ai.behaviours
{
	// Token: 0x0200089E RID: 2206
	public class BehCitizenActionCity : BehCityActor
	{
		// Token: 0x06004489 RID: 17545 RVA: 0x001CE2A5 File Offset: 0x001CC4A5
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.uses_cities = true;
		}
	}
}
