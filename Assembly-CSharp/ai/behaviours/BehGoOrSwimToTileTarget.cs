using System;

namespace ai.behaviours
{
	// Token: 0x020008DD RID: 2269
	public class BehGoOrSwimToTileTarget : BehGoToTileTarget
	{
		// Token: 0x06004527 RID: 17703 RVA: 0x001D0EFC File Offset: 0x001CF0FC
		protected override void setupErrorChecks()
		{
			base.setupErrorChecks();
			this.null_check_tile_target = true;
			this.walk_on_water = true;
		}
	}
}
