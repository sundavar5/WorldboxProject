using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x02000868 RID: 2152
	public class DynamicGridWPool : BaseGrid
	{
		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x0600436A RID: 17258 RVA: 0x001C9954 File Offset: 0x001C7B54
		// (set) Token: 0x0600436B RID: 17259 RVA: 0x001C997D File Offset: 0x001C7B7D
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

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600436C RID: 17260 RVA: 0x001C997F File Offset: 0x001C7B7F
		// (set) Token: 0x0600436D RID: 17261 RVA: 0x001C99A8 File Offset: 0x001C7BA8
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

		// Token: 0x0600436E RID: 17262 RVA: 0x001C99AC File Offset: 0x001C7BAC
		public DynamicGridWPool(NodePool iNodePool)
		{
			this.m_gridRect = new GridRect();
			this.m_gridRect.minX = 0;
			this.m_gridRect.minY = 0;
			this.m_gridRect.maxX = 0;
			this.m_gridRect.maxY = 0;
			this.m_notSet = true;
			this.m_nodePool = iNodePool;
		}

		// Token: 0x0600436F RID: 17263 RVA: 0x001C9A08 File Offset: 0x001C7C08
		public DynamicGridWPool(DynamicGridWPool b) : base(b)
		{
			this.m_notSet = b.m_notSet;
			this.m_nodePool = b.m_nodePool;
		}

		// Token: 0x06004370 RID: 17264 RVA: 0x001C9A2C File Offset: 0x001C7C2C
		public override Node GetNodeAt(int iX, int iY)
		{
			GridPos pos = new GridPos(iX, iY);
			return this.GetNodeAt(pos);
		}

		// Token: 0x06004371 RID: 17265 RVA: 0x001C9A48 File Offset: 0x001C7C48
		public override bool IsWalkableAt(int iX, int iY)
		{
			GridPos pos = new GridPos(iX, iY);
			return this.IsWalkableAt(pos);
		}

		// Token: 0x06004372 RID: 17266 RVA: 0x001C9A64 File Offset: 0x001C7C64
		private void setBoundingBox()
		{
			this.m_notSet = true;
			foreach (KeyValuePair<GridPos, Node> pair in this.m_nodePool.Nodes)
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

		// Token: 0x06004373 RID: 17267 RVA: 0x001C9BBC File Offset: 0x001C7DBC
		public override bool SetWalkableAt(int iX, int iY, bool iWalkable, int pCost = 1)
		{
			GridPos pos = new GridPos(iX, iY);
			this.m_nodePool.SetNode(pos, new bool?(iWalkable));
			if (iWalkable)
			{
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
			}
			else if (iX == this.m_gridRect.minX || iX == this.m_gridRect.maxX || iY == this.m_gridRect.minY || iY == this.m_gridRect.maxY)
			{
				this.m_notSet = true;
			}
			return true;
		}

		// Token: 0x06004374 RID: 17268 RVA: 0x001C9CB4 File Offset: 0x001C7EB4
		public override Node GetNodeAt(GridPos iPos)
		{
			return this.m_nodePool.GetNode(iPos);
		}

		// Token: 0x06004375 RID: 17269 RVA: 0x001C9CC2 File Offset: 0x001C7EC2
		public override bool IsWalkableAt(GridPos iPos)
		{
			return this.m_nodePool.Nodes.ContainsKey(iPos);
		}

		// Token: 0x06004376 RID: 17270 RVA: 0x001C9CD5 File Offset: 0x001C7ED5
		public override bool SetWalkableAt(GridPos iPos, bool iWalkable)
		{
			return this.SetWalkableAt(iPos.x, iPos.y, iWalkable, 1);
		}

		// Token: 0x06004377 RID: 17271 RVA: 0x001C9CEC File Offset: 0x001C7EEC
		public override void Reset()
		{
			foreach (KeyValuePair<GridPos, Node> keyValue in this.m_nodePool.Nodes)
			{
				keyValue.Value.Reset(null);
			}
		}

		// Token: 0x06004378 RID: 17272 RVA: 0x001C9D54 File Offset: 0x001C7F54
		public override BaseGrid Clone()
		{
			return new DynamicGridWPool(this.m_nodePool);
		}

		// Token: 0x04003118 RID: 12568
		private bool m_notSet;

		// Token: 0x04003119 RID: 12569
		private NodePool m_nodePool;
	}
}
