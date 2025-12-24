using System;

namespace EpPathFinding.cs
{
	// Token: 0x02000874 RID: 2164
	public class Util
	{
		// Token: 0x06004402 RID: 17410 RVA: 0x001CBF02 File Offset: 0x001CA102
		public static DiagonalMovement GetDiagonalMovement(bool iCrossCorners, bool iCrossAdjacentPoint)
		{
			if (iCrossCorners && iCrossAdjacentPoint)
			{
				return DiagonalMovement.Always;
			}
			if (iCrossCorners)
			{
				return DiagonalMovement.IfAtLeastOneWalkable;
			}
			return DiagonalMovement.OnlyWhenNoObstacles;
		}
	}
}
