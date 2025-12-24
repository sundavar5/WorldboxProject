using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x02000867 RID: 2151
	public class DynamicGrid : BaseGrid
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06004359 RID: 17241 RVA: 0x001C93A8 File Offset: 0x001C75A8
		// (set) Token: 0x0600435A RID: 17242 RVA: 0x001C93D1 File Offset: 0x001C75D1
		public override int width
		{
			get
			{
				if (this.m_notSet)
				{
					this.setBoundingBox();
				}
				return this.m_gridRect.maxX - this.m_gridRect.minX + 1;
			}
			protected set
			{
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x0600435B RID: 17243 RVA: 0x001C93D3 File Offset: 0x001C75D3
		// (set) Token: 0x0600435C RID: 17244 RVA: 0x001C93FC File Offset: 0x001C75FC
		public override int height
		{
			get
			{
				if (this.m_notSet)
				{
					this.setBoundingBox();
				}
				return this.m_gridRect.maxY - this.m_gridRect.minY + 1;
			}
			protected set
			{
			}
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x001C9400 File Offset: 0x001C7600
		public DynamicGrid(List<GridPos> iWalkableGridList = null)
		{
			this.m_gridRect = new GridRect();
			this.m_gridRect.minX = 0;
			this.m_gridRect.minY = 0;
			this.m_gridRect.maxX = 0;
			this.m_gridRect.maxY = 0;
			this.m_notSet = true;
			this.buildNodes(iWalkableGridList);
		}

		// Token: 0x0600435E RID: 17246 RVA: 0x001C945C File Offset: 0x001C765C
		public DynamicGrid(DynamicGrid b) : base(b)
		{
			this.m_notSet = b.m_notSet;
			this.m_nodes = new Dictionary<GridPos, Node>(b.m_nodes);
		}

		// Token: 0x0600435F RID: 17247 RVA: 0x001C9484 File Offset: 0x001C7684
		protected void buildNodes(List<GridPos> iWalkableGridList)
		{
			this.m_nodes = new Dictionary<GridPos, Node>();
			if (iWalkableGridList == null)
			{
				return;
			}
			foreach (GridPos gridPos in iWalkableGridList)
			{
				this.SetWalkableAt(gridPos.x, gridPos.y, true, 1);
			}
		}

		// Token: 0x06004360 RID: 17248 RVA: 0x001C94F0 File Offset: 0x001C76F0
		public override Node GetNodeAt(int iX, int iY)
		{
			GridPos pos = new GridPos(iX, iY);
			return this.GetNodeAt(pos);
		}

		// Token: 0x06004361 RID: 17249 RVA: 0x001C950C File Offset: 0x001C770C
		public override bool IsWalkableAt(int iX, int iY)
		{
			GridPos pos = new GridPos(iX, iY);
			return this.IsWalkableAt(pos);
		}

		// Token: 0x06004362 RID: 17250 RVA: 0x001C9528 File Offset: 0x001C7728
		private void setBoundingBox()
		{
			this.m_notSet = true;
			foreach (KeyValuePair<GridPos, Node> pair in this.m_nodes)
			{
				if (pair.Key.x < this.m_gridRect.minX || this.m_notSet)
				{
					this.m_gridRect.minX = pair.Key.x;
				}
				if (pair.Key.x > this.m_gridRect.maxX || this.m_notSet)
				{
					this.m_gridRect.maxX = pair.Key.x;
				}
				if (pair.Key.y < this.m_gridRect.minY || this.m_notSet)
				{
					this.m_gridRect.minY = pair.Key.y;
				}
				if (pair.Key.y > this.m_gridRect.maxY || this.m_notSet)
				{
					this.m_gridRect.maxY = pair.Key.y;
				}
				this.m_notSet = false;
			}
			this.m_notSet = false;
		}

		// Token: 0x06004363 RID: 17251 RVA: 0x001C967C File Offset: 0x001C787C
		public override bool SetWalkableAt(int iX, int iY, bool iWalkable, int pCost = 1)
		{
			GridPos pos = new GridPos(iX, iY);
			if (iWalkable)
			{
				if (this.m_nodes.ContainsKey(pos))
				{
					return true;
				}
				if (iX < this.m_gridRect.minX || this.m_notSet)
				{
					this.m_gridRect.minX = iX;
				}
				if (iX > this.m_gridRect.maxX || this.m_notSet)
				{
					this.m_gridRect.maxX = iX;
				}
				if (iY < this.m_gridRect.minY || this.m_notSet)
				{
					this.m_gridRect.minY = iY;
				}
				if (iY > this.m_gridRect.maxY || this.m_notSet)
				{
					this.m_gridRect.maxY = iY;
				}
				this.m_nodes.Add(new GridPos(pos.x, pos.y), new Node(pos.x, pos.y, new bool?(iWalkable)));
			}
			else if (this.m_nodes.ContainsKey(pos))
			{
				this.m_nodes.Remove(pos);
				if (iX == this.m_gridRect.minX || iX == this.m_gridRect.maxX || iY == this.m_gridRect.minY || iY == this.m_gridRect.maxY)
				{
					this.m_notSet = true;
				}
			}
			return true;
		}

		// Token: 0x06004364 RID: 17252 RVA: 0x001C97BF File Offset: 0x001C79BF
		public override Node GetNodeAt(GridPos iPos)
		{
			if (this.m_nodes.ContainsKey(iPos))
			{
				return this.m_nodes[iPos];
			}
			return null;
		}

		// Token: 0x06004365 RID: 17253 RVA: 0x001C97DD File Offset: 0x001C79DD
		public override bool IsWalkableAt(GridPos iPos)
		{
			return this.m_nodes.ContainsKey(iPos);
		}

		// Token: 0x06004366 RID: 17254 RVA: 0x001C97EB File Offset: 0x001C79EB
		public override bool SetWalkableAt(GridPos iPos, bool iWalkable)
		{
			return this.SetWalkableAt(iPos.x, iPos.y, iWalkable, 1);
		}

		// Token: 0x06004367 RID: 17255 RVA: 0x001C9801 File Offset: 0x001C7A01
		public override void Reset()
		{
			this.Reset(null);
		}

		// Token: 0x06004368 RID: 17256 RVA: 0x001C980C File Offset: 0x001C7A0C
		public void Reset(List<GridPos> iWalkableGridList)
		{
			foreach (KeyValuePair<GridPos, Node> keyValue in this.m_nodes)
			{
				keyValue.Value.Reset(null);
			}
			if (iWalkableGridList == null)
			{
				return;
			}
			foreach (KeyValuePair<GridPos, Node> keyValue2 in this.m_nodes)
			{
				if (iWalkableGridList.Contains(keyValue2.Key))
				{
					this.SetWalkableAt(keyValue2.Key, true);
				}
				else
				{
					this.SetWalkableAt(keyValue2.Key, false);
				}
			}
		}

		// Token: 0x06004369 RID: 17257 RVA: 0x001C98DC File Offset: 0x001C7ADC
		public override BaseGrid Clone()
		{
			DynamicGrid tNewGrid = new DynamicGrid(null);
			foreach (KeyValuePair<GridPos, Node> keyValue in this.m_nodes)
			{
				tNewGrid.SetWalkableAt(keyValue.Key.x, keyValue.Key.y, true, 1);
			}
			return tNewGrid;
		}

		// Token: 0x04003116 RID: 12566
		protected Dictionary<GridPos, Node> m_nodes;

		// Token: 0x04003117 RID: 12567
		private bool m_notSet;
	}
}
