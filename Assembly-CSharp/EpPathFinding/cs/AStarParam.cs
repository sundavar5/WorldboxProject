using System;

namespace EpPathFinding.cs
{
	// Token: 0x02000864 RID: 2148
	public class AStarParam : ParamBase
	{
		// Token: 0x06004345 RID: 17221 RVA: 0x001C90A0 File Offset: 0x001C72A0
		public void resetParam()
		{
			this.swamp = false;
			this.roads = false;
			this.ocean = false;
			this.lava = false;
			this.ground = false;
			this.use_global_path_lock = false;
			this.boat = false;
			this.limit = false;
			this.fire = false;
			this.end_to_start_path = false;
		}

		// Token: 0x06004346 RID: 17222 RVA: 0x001C90F3 File Offset: 0x001C72F3
		internal override void _reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null)
		{
		}

		// Token: 0x04003100 RID: 12544
		internal float weight;

		// Token: 0x04003101 RID: 12545
		internal int max_open_list = -1;

		// Token: 0x04003102 RID: 12546
		internal bool roads;

		// Token: 0x04003103 RID: 12547
		internal bool use_global_path_lock;

		// Token: 0x04003104 RID: 12548
		internal bool boat;

		// Token: 0x04003105 RID: 12549
		internal bool limit;

		// Token: 0x04003106 RID: 12550
		internal bool swamp;

		// Token: 0x04003107 RID: 12551
		internal bool ocean;

		// Token: 0x04003108 RID: 12552
		internal bool lava;

		// Token: 0x04003109 RID: 12553
		internal bool fire;

		// Token: 0x0400310A RID: 12554
		internal bool block;

		// Token: 0x0400310B RID: 12555
		internal bool ground;

		// Token: 0x0400310C RID: 12556
		internal bool end_to_start_path;
	}
}
