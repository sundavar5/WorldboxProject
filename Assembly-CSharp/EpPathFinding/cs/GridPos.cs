using System;

namespace EpPathFinding.cs
{
	// Token: 0x0200086D RID: 2157
	public class GridPos : IEquatable<GridPos>
	{
		// Token: 0x060043D4 RID: 17364 RVA: 0x001CB801 File Offset: 0x001C9A01
		public GridPos()
		{
			this.x = 0;
			this.y = 0;
		}

		// Token: 0x060043D5 RID: 17365 RVA: 0x001CB817 File Offset: 0x001C9A17
		public GridPos(int iX, int iY)
		{
			this.x = iX;
			this.y = iY;
		}

		// Token: 0x060043D6 RID: 17366 RVA: 0x001CB82D File Offset: 0x001C9A2D
		public GridPos(GridPos b)
		{
			this.x = b.x;
			this.y = b.y;
		}

		// Token: 0x060043D7 RID: 17367 RVA: 0x001CB84D File Offset: 0x001C9A4D
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		// Token: 0x060043D8 RID: 17368 RVA: 0x001CB85C File Offset: 0x001C9A5C
		public override bool Equals(object obj)
		{
			GridPos p = (GridPos)obj;
			return p != null && this.x == p.x && this.y == p.y;
		}

		// Token: 0x060043D9 RID: 17369 RVA: 0x001CB893 File Offset: 0x001C9A93
		public bool Equals(GridPos p)
		{
			return p != null && this.x == p.x && this.y == p.y;
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x001CB8B8 File Offset: 0x001C9AB8
		public static bool operator ==(GridPos a, GridPos b)
		{
			return a == b || (a != null && b != null && a.x == b.x && a.y == b.y);
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x001CB8E8 File Offset: 0x001C9AE8
		public static bool operator !=(GridPos a, GridPos b)
		{
			return !(a == b);
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x001CB8F4 File Offset: 0x001C9AF4
		public GridPos Set(int iX, int iY)
		{
			this.x = iX;
			this.y = iY;
			return this;
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x001CB905 File Offset: 0x001C9B05
		public override string ToString()
		{
			return string.Format("({0},{1})", this.x, this.y);
		}

		// Token: 0x0400312C RID: 12588
		public int x;

		// Token: 0x0400312D RID: 12589
		public int y;
	}
}
