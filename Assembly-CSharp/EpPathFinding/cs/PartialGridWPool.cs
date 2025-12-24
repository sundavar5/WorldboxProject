using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x0200086B RID: 2155
	public class PartialGridWPool : BaseGrid
	{
		// Token: 0x170003CF RID: 975
		// (get) Token: 0x060043AF RID: 17327 RVA: 0x001CB1F7 File Offset: 0x001C93F7
		// (set) Token: 0x060043B0 RID: 17328 RVA: 0x001CB212 File Offset: 0x001C9412
		public override int width
		{
			get
			{
				return this.m_gridRect.maxX - this.m_gridRect.minX + 1;
			}
			protected set
			{
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x060043B1 RID: 17329 RVA: 0x001CB214 File Offset: 0x001C9414
		// (set) Token: 0x060043B2 RID: 17330 RVA: 0x001CB22F File Offset: 0x001C942F
		public override int height
		{
			get
			{
				return this.m_gridRect.maxY - this.m_gridRect.minY + 1;
			}
			protected set
			{
			}
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x001CB231 File Offset: 0x001C9431
		public PartialGridWPool(NodePool iNodePool, GridRect iGridRect = null)
		{
			if (iGridRect == null)
			{
				this.m_gridRect = new GridRect();
			}
			else
			{
				this.m_gridRect = iGridRect;
			}
			this.m_nodePool = iNodePool;
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x001CB25D File Offset: 0x001C945D
		public PartialGridWPool(PartialGridWPool b) : base(b)
		{
			this.m_nodePool = b.m_nodePool;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x001CB272 File Offset: 0x001C9472
		public void SetGridRect(GridRect iGridRect)
		{
			this.m_gridRect = iGridRect;
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x001CB27B File Offset: 0x001C947B
		public bool IsInside(int iX, int iY)
		{
			return iX >= this.m_gridRect.minX && iX <= this.m_gridRect.maxX && iY >= this.m_gridRect.minY && iY <= this.m_gridRect.maxY;
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x001CB2B8 File Offset: 0x001C94B8
		public override Node GetNodeAt(int iX, int iY)
		{
			GridPos pos = new GridPos(iX, iY);
			return this.GetNodeAt(pos);
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x001CB2D4 File Offset: 0x001C94D4
		public override bool IsWalkableAt(int iX, int iY)
		{
			GridPos pos = new GridPos(iX, iY);
			return this.IsWalkableAt(pos);
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x001CB2F0 File Offset: 0x001C94F0
		public override bool SetWalkableAt(int iX, int iY, bool iWalkable, int pCost = 1)
		{
			if (!this.IsInside(iX, iY))
			{
				return false;
			}
			GridPos pos = new GridPos(iX, iY);
			this.m_nodePool.SetNode(pos, new bool?(iWalkable));
			return true;
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x001CB325 File Offset: 0x001C9525
		public bool IsInside(GridPos iPos)
		{
			return this.IsInside(iPos.x, iPos.y);
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x001CB339 File Offset: 0x001C9539
		public override Node GetNodeAt(GridPos iPos)
		{
			if (!this.IsInside(iPos))
			{
				return null;
			}
			return this.m_nodePool.GetNode(iPos);
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x001CB352 File Offset: 0x001C9552
		public override bool IsWalkableAt(GridPos iPos)
		{
			return this.IsInside(iPos) && this.m_nodePool.Nodes.ContainsKey(iPos);
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x001CB370 File Offset: 0x001C9570
		public override bool SetWalkableAt(GridPos iPos, bool iWalkable)
		{
			return this.SetWalkableAt(iPos.x, iPos.y, iWalkable, 1);
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x001CB388 File Offset: 0x001C9588
		public override void Reset()
		{
			int rectCount = (this.m_gridRect.maxX - this.m_gridRect.minX) * (this.m_gridRect.maxY - this.m_gridRect.minY);
			if (this.m_nodePool.Nodes.Count > rectCount)
			{
				GridPos travPos = new GridPos(0, 0);
				for (int xTrav = this.m_gridRect.minX; xTrav <= this.m_gridRect.maxX; xTrav++)
				{
					travPos.x = xTrav;
					for (int yTrav = this.m_gridRect.minY; yTrav <= this.m_gridRect.maxY; yTrav++)
					{
						travPos.y = yTrav;
						Node curNode = this.m_nodePool.GetNode(travPos);
						if (curNode != null)
						{
							curNode.Reset(null);
						}
					}
				}
				return;
			}
			foreach (KeyValuePair<GridPos, Node> keyValue in this.m_nodePool.Nodes)
			{
				keyValue.Value.Reset(null);
			}
		}

		// Token: 0x060043BF RID: 17343 RVA: 0x001CB4B8 File Offset: 0x001C96B8
		public override BaseGrid Clone()
		{
			return new PartialGridWPool(this.m_nodePool, this.m_gridRect);
		}

		// Token: 0x04003128 RID: 12584
		private NodePool m_nodePool;
	}
}
