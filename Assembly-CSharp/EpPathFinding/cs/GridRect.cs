using System;

namespace EpPathFinding.cs
{
	// Token: 0x0200086E RID: 2158
	public class GridRect
	{
		// Token: 0x060043DE RID: 17374 RVA: 0x001CB927 File Offset: 0x001C9B27
		public GridRect()
		{
			this.minX = 0;
			this.minY = 0;
			this.maxX = 0;
			this.maxY = 0;
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x001CB94B File Offset: 0x001C9B4B
		public GridRect(int iMinX, int iMinY, int iMaxX, int iMaxY)
		{
			this.minX = iMinX;
			this.minY = iMinY;
			this.maxX = iMaxX;
			this.maxY = iMaxY;
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x001CB970 File Offset: 0x001C9B70
		public GridRect(GridRect b)
		{
			this.minX = b.minX;
			this.minY = b.minY;
			this.maxX = b.maxX;
			this.maxY = b.maxY;
		}

		// Token: 0x060043E1 RID: 17377 RVA: 0x001CB9A8 File Offset: 0x001C9BA8
		public override int GetHashCode()
		{
			return this.minX ^ this.minY ^ this.maxX ^ this.maxY;
		}

		// Token: 0x060043E2 RID: 17378 RVA: 0x001CB9C8 File Offset: 0x001C9BC8
		public override bool Equals(object obj)
		{
			GridRect p = (GridRect)obj;
			return p != null && (this.minX == p.minX && this.minY == p.minY && this.maxX == p.maxX) && this.maxY == p.maxY;
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x001CBA1C File Offset: 0x001C9C1C
		public bool Equals(GridRect p)
		{
			return p != null && (this.minX == p.minX && this.minY == p.minY && this.maxX == p.maxX) && this.maxY == p.maxY;
		}

		// Token: 0x060043E4 RID: 17380 RVA: 0x001CBA68 File Offset: 0x001C9C68
		public static bool operator ==(GridRect a, GridRect b)
		{
			return a == b || (a != null && b != null && (a.minX == b.minX && a.minY == b.minY && a.maxX == b.maxX) && a.maxY == b.maxY);
		}

		// Token: 0x060043E5 RID: 17381 RVA: 0x001CBABF File Offset: 0x001C9CBF
		public static bool operator !=(GridRect a, GridRect b)
		{
			return !(a == b);
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x001CBACB File Offset: 0x001C9CCB
		public GridRect Set(int iMinX, int iMinY, int iMaxX, int iMaxY)
		{
			this.minX = iMinX;
			this.minY = iMinY;
			this.maxX = iMaxX;
			this.maxY = iMaxY;
			return this;
		}

		// Token: 0x0400312E RID: 12590
		public int minX;

		// Token: 0x0400312F RID: 12591
		public int minY;

		// Token: 0x04003130 RID: 12592
		public int maxX;

		// Token: 0x04003131 RID: 12593
		public int maxY;
	}
}
