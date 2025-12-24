using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x02000866 RID: 2150
	public abstract class BaseGrid : IDisposable
	{
		// Token: 0x06004347 RID: 17223 RVA: 0x001C90F5 File Offset: 0x001C72F5
		public BaseGrid()
		{
			this.m_gridRect = new GridRect();
		}

		// Token: 0x06004348 RID: 17224 RVA: 0x001C9113 File Offset: 0x001C7313
		public BaseGrid(BaseGrid b)
		{
			this.m_gridRect = new GridRect(b.m_gridRect);
			this.width = b.width;
			this.height = b.height;
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x001C914F File Offset: 0x001C734F
		public GridRect gridRect
		{
			get
			{
				return this.m_gridRect;
			}
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600434A RID: 17226
		// (set) Token: 0x0600434B RID: 17227
		public abstract int width { get; protected set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600434C RID: 17228
		// (set) Token: 0x0600434D RID: 17229
		public abstract int height { get; protected set; }

		// Token: 0x0600434E RID: 17230
		public abstract Node GetNodeAt(int iX, int iY);

		// Token: 0x0600434F RID: 17231
		public abstract bool IsWalkableAt(int iX, int iY);

		// Token: 0x06004350 RID: 17232
		public abstract bool SetWalkableAt(int iX, int iY, bool iWalkable, int pCost = 1);

		// Token: 0x06004351 RID: 17233
		public abstract Node GetNodeAt(GridPos iPos);

		// Token: 0x06004352 RID: 17234
		public abstract bool IsWalkableAt(GridPos iPos);

		// Token: 0x06004353 RID: 17235
		public abstract bool SetWalkableAt(GridPos iPos, bool iWalkable);

		// Token: 0x06004354 RID: 17236 RVA: 0x001C9158 File Offset: 0x001C7358
		public List<Node> GetNeighbors(Node iNode, DiagonalMovement diagonalMovement)
		{
			int tX = iNode.x;
			int tY = iNode.y;
			List<Node> neighbors = new List<Node>();
			bool tS0 = false;
			bool tD0 = false;
			bool tS = false;
			bool tD = false;
			bool tS2 = false;
			bool tD2 = false;
			bool tS3 = false;
			bool tD3 = false;
			GridPos pos = new GridPos();
			if (this.IsWalkableAt(pos.Set(tX, tY - 1)))
			{
				neighbors.Add(this.GetNodeAt(pos));
				tS0 = true;
			}
			if (this.IsWalkableAt(pos.Set(tX + 1, tY)))
			{
				neighbors.Add(this.GetNodeAt(pos));
				tS = true;
			}
			if (this.IsWalkableAt(pos.Set(tX, tY + 1)))
			{
				neighbors.Add(this.GetNodeAt(pos));
				tS2 = true;
			}
			if (this.IsWalkableAt(pos.Set(tX - 1, tY)))
			{
				neighbors.Add(this.GetNodeAt(pos));
				tS3 = true;
			}
			switch (diagonalMovement)
			{
			case DiagonalMovement.Always:
				tD0 = true;
				tD = true;
				tD2 = true;
				tD3 = true;
				break;
			case DiagonalMovement.IfAtLeastOneWalkable:
				tD0 = (tS3 || tS0);
				tD = (tS0 || tS);
				tD2 = (tS || tS2);
				tD3 = (tS2 || tS3);
				break;
			case DiagonalMovement.OnlyWhenNoObstacles:
				tD0 = (tS3 && tS0);
				tD = (tS0 && tS);
				tD2 = (tS && tS2);
				tD3 = (tS2 && tS3);
				break;
			}
			if (tD0 && this.IsWalkableAt(pos.Set(tX - 1, tY - 1)))
			{
				neighbors.Add(this.GetNodeAt(pos));
			}
			if (tD && this.IsWalkableAt(pos.Set(tX + 1, tY - 1)))
			{
				neighbors.Add(this.GetNodeAt(pos));
			}
			if (tD2 && this.IsWalkableAt(pos.Set(tX + 1, tY + 1)))
			{
				neighbors.Add(this.GetNodeAt(pos));
			}
			if (tD3 && this.IsWalkableAt(pos.Set(tX - 1, tY + 1)))
			{
				neighbors.Add(this.GetNodeAt(pos));
			}
			return neighbors;
		}

		// Token: 0x06004355 RID: 17237 RVA: 0x001C931F File Offset: 0x001C751F
		public void addToClosed(Node pNode)
		{
			this.closedList.Add(pNode);
			this.closed_list_count++;
		}

		// Token: 0x06004356 RID: 17238
		public abstract void Reset();

		// Token: 0x06004357 RID: 17239
		public abstract BaseGrid Clone();

		// Token: 0x06004358 RID: 17240 RVA: 0x001C933C File Offset: 0x001C753C
		public virtual void Dispose()
		{
			foreach (Node node in this.closedList)
			{
				node.Dispose();
			}
			this.closedList.Clear();
			this.closed_list_count = 0;
			this.m_gridRect = null;
		}

		// Token: 0x04003112 RID: 12562
		public readonly List<Node> closedList = new List<Node>();

		// Token: 0x04003113 RID: 12563
		public int closed_list_count;

		// Token: 0x04003114 RID: 12564
		public const int CLOSED_LIST_MINIMUM_ELEMENTS = 10;

		// Token: 0x04003115 RID: 12565
		protected GridRect m_gridRect;
	}
}
