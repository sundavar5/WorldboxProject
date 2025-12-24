using System;

namespace EpPathFinding.cs
{
	// Token: 0x0200086A RID: 2154
	public class Node : IComparable<Node>, IDisposable
	{
		// Token: 0x060043A5 RID: 17317 RVA: 0x001CB008 File Offset: 0x001C9208
		public Node(int iX, int iY, bool? iWalkable = null)
		{
			this.x = iX;
			this.y = iY;
			this.heuristicStartToEndLen = 0f;
			this.startToCurNodeLen = 0f;
			this.heuristicCurNodeToEndLen = null;
			this.isOpened = false;
			this.isClosed = false;
			this.parent = null;
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x001CB060 File Offset: 0x001C9260
		public Node(Node b)
		{
			this.x = b.x;
			this.y = b.y;
			this.heuristicStartToEndLen = b.heuristicStartToEndLen;
			this.startToCurNodeLen = b.startToCurNodeLen;
			this.heuristicCurNodeToEndLen = b.heuristicCurNodeToEndLen;
			this.isOpened = b.isOpened;
			this.isClosed = b.isClosed;
			this.parent = b.parent;
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x001CB0D3 File Offset: 0x001C92D3
		public void Reset(bool? iWalkable = null)
		{
			this.heuristicStartToEndLen = 0f;
			this.startToCurNodeLen = 0f;
			this.heuristicCurNodeToEndLen = null;
			this.isOpened = false;
			this.isClosed = false;
			this.parent = null;
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x001CB10C File Offset: 0x001C930C
		public int CompareTo(Node iObj)
		{
			float result = this.heuristicStartToEndLen - iObj.heuristicStartToEndLen;
			if (result > 0f)
			{
				return 1;
			}
			if (result == 0f)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x001CB13C File Offset: 0x001C933C
		public override int GetHashCode()
		{
			return this.x ^ this.y;
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x001CB14C File Offset: 0x001C934C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Node p = obj as Node;
			return p != null && this.x == p.x && this.y == p.y;
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x001CB188 File Offset: 0x001C9388
		public bool Equals(Node p)
		{
			return p != null && this.x == p.x && this.y == p.y;
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x001CB1AD File Offset: 0x001C93AD
		public static bool operator ==(Node a, Node b)
		{
			return a == b || (a != null && b != null && a.x == b.x && a.y == b.y);
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x001CB1DB File Offset: 0x001C93DB
		public static bool operator !=(Node a, Node b)
		{
			return !(a == b);
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x001CB1E7 File Offset: 0x001C93E7
		public void Dispose()
		{
			this.tile = null;
			this.parent = null;
		}

		// Token: 0x0400311F RID: 12575
		public WorldTile tile;

		// Token: 0x04003120 RID: 12576
		public readonly int x;

		// Token: 0x04003121 RID: 12577
		public readonly int y;

		// Token: 0x04003122 RID: 12578
		public float heuristicStartToEndLen;

		// Token: 0x04003123 RID: 12579
		public float startToCurNodeLen;

		// Token: 0x04003124 RID: 12580
		public float? heuristicCurNodeToEndLen;

		// Token: 0x04003125 RID: 12581
		public bool isOpened;

		// Token: 0x04003126 RID: 12582
		public bool isClosed;

		// Token: 0x04003127 RID: 12583
		public Node parent;
	}
}
