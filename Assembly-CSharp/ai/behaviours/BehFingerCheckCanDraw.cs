using System;

namespace ai.behaviours
{
	// Token: 0x02000957 RID: 2391
	public class BehFingerCheckCanDraw : BehFingerDrawAction
	{
		// Token: 0x06004660 RID: 18016 RVA: 0x001DD290 File Offset: 0x001DB490
		protected override void setupErrorChecks()
		{
			this.check_has_target_tiles = true;
			this.check_current_tile_in_target_tiles = true;
			this.check_target_tile_in_target_tiles = false;
			base.setupErrorChecks();
		}
	}
}
