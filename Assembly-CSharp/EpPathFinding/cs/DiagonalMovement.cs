using System;

namespace EpPathFinding.cs
{
	// Token: 0x02000865 RID: 2149
	public enum DiagonalMovement
	{
		// Token: 0x0400310E RID: 12558
		Always,
		// Token: 0x0400310F RID: 12559
		Never,
		// Token: 0x04003110 RID: 12560
		IfAtLeastOneWalkable,
		// Token: 0x04003111 RID: 12561
		OnlyWhenNoObstacles
	}
}
