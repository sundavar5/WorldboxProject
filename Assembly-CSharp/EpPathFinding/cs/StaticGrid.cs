using System;
using System.Runtime.CompilerServices;

namespace EpPathFinding.cs
{
	// Token: 0x0200086C RID: 2156
	public class StaticGrid : BaseGrid
	{
		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x060043C0 RID: 17344 RVA: 0x001CB4CB File Offset: 0x001C96CB
		// (set) Token: 0x060043C1 RID: 17345 RVA: 0x001CB4D3 File Offset: 0x001C96D3
		public override int width { get; protected set; }

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x060043C2 RID: 17346 RVA: 0x001CB4DC File Offset: 0x001C96DC
		// (set) Token: 0x060043C3 RID: 17347 RVA: 0x001CB4E4 File Offset: 0x001C96E4
		public override int height { get; protected set; }

		// Token: 0x060043C4 RID: 17348 RVA: 0x001CB4F0 File Offset: 0x001C96F0
		public StaticGrid(int iWidth, int iHeight, bool[][] iMatrix = null)
		{
			this.width = iWidth;
			this.height = iHeight;
			this.m_gridRect.minX = 0;
			this.m_gridRect.minY = 0;
			this.m_gridRect.maxX = iWidth - 1;
			this.m_gridRect.maxY = iHeight - 1;
			this.m_nodes = this.buildNodes(iWidth, iHeight, iMatrix);
		}

		// Token: 0x060043C5 RID: 17349 RVA: 0x001CB554 File Offset: 0x001C9754
		public StaticGrid(StaticGrid b) : base(b)
		{
			bool[][] tMatrix = new bool[b.width][];
			for (int widthTrav = 0; widthTrav < b.width; widthTrav++)
			{
				tMatrix[widthTrav] = new bool[b.height];
				for (int heightTrav = 0; heightTrav < b.height; heightTrav++)
				{
					if (b.IsWalkableAt(widthTrav, heightTrav))
					{
						tMatrix[widthTrav][heightTrav] = true;
					}
					else
					{
						tMatrix[widthTrav][heightTrav] = false;
					}
				}
			}
			this.m_nodes = this.buildNodes(b.width, b.height, tMatrix);
		}

		// Token: 0x060043C6 RID: 17350 RVA: 0x001CB5D8 File Offset: 0x001C97D8
		private Node[][] buildNodes(int iWidth, int iHeight, bool[][] iMatrix)
		{
			Node[][] tNodes = new Node[iWidth][];
			for (int widthTrav = 0; widthTrav < iWidth; widthTrav++)
			{
				tNodes[widthTrav] = new Node[iHeight];
				for (int heightTrav = 0; heightTrav < iHeight; heightTrav++)
				{
					tNodes[widthTrav][heightTrav] = new Node(widthTrav, heightTrav, null);
				}
			}
			return tNodes;
		}

		// Token: 0x060043C7 RID: 17351 RVA: 0x001CB623 File Offset: 0x001C9823
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override Node GetNodeAt(int iX, int iY)
		{
			return this.m_nodes[iX][iY];
		}

		// Token: 0x060043C8 RID: 17352 RVA: 0x001CB62F File Offset: 0x001C982F
		public override bool IsWalkableAt(int iX, int iY)
		{
			return this.isInside(iX, iY);
		}

		// Token: 0x060043C9 RID: 17353 RVA: 0x001CB639 File Offset: 0x001C9839
		protected bool isInside(int iX, int iY)
		{
			return iX >= 0 && iX < this.width && iY >= 0 && iY < this.height;
		}

		// Token: 0x060043CA RID: 17354 RVA: 0x001CB659 File Offset: 0x001C9859
		public override bool SetWalkableAt(int iX, int iY, bool iWalkable, int pCost = 1)
		{
			return true;
		}

		// Token: 0x060043CB RID: 17355 RVA: 0x001CB65C File Offset: 0x001C985C
		public void SetTileNode(int iX, int iY, WorldTile pTile)
		{
			this.m_nodes[iX][iY].tile = pTile;
		}

		// Token: 0x060043CC RID: 17356 RVA: 0x001CB66E File Offset: 0x001C986E
		protected bool isInside(GridPos iPos)
		{
			return this.isInside(iPos.x, iPos.y);
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x001CB682 File Offset: 0x001C9882
		public override Node GetNodeAt(GridPos iPos)
		{
			return this.GetNodeAt(iPos.x, iPos.y);
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x001CB696 File Offset: 0x001C9896
		public override bool IsWalkableAt(GridPos iPos)
		{
			return this.IsWalkableAt(iPos.x, iPos.y);
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x001CB6AA File Offset: 0x001C98AA
		public override bool SetWalkableAt(GridPos iPos, bool iWalkable)
		{
			return this.SetWalkableAt(iPos.x, iPos.y, iWalkable, 1);
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x001CB6C0 File Offset: 0x001C98C0
		public override void Reset()
		{
			this.Reset(null);
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x001CB6CC File Offset: 0x001C98CC
		public void Reset(bool[][] iMatrix)
		{
			foreach (Node node in this.closedList)
			{
				node.Reset(null);
			}
			this.closedList.Clear();
			this.closed_list_count = 0;
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x001CB738 File Offset: 0x001C9938
		public override BaseGrid Clone()
		{
			int tWidth = this.width;
			int tHeight = this.height;
			Node[][] nodes = this.m_nodes;
			StaticGrid tNewGrid = new StaticGrid(tWidth, tHeight, null);
			Node[][] tNewNodes = new Node[tWidth][];
			for (int widthTrav = 0; widthTrav < tWidth; widthTrav++)
			{
				tNewNodes[widthTrav] = new Node[tHeight];
				for (int heightTrav = 0; heightTrav < tHeight; heightTrav++)
				{
					tNewNodes[widthTrav][heightTrav] = new Node(widthTrav, heightTrav, new bool?(false));
				}
			}
			tNewGrid.m_nodes = tNewNodes;
			return tNewGrid;
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x001CB7B4 File Offset: 0x001C99B4
		public override void Dispose()
		{
			Node[][] tNodes = this.m_nodes;
			for (int widthTrav = 0; widthTrav < this.width; widthTrav++)
			{
				for (int heightTrav = 0; heightTrav < this.height; heightTrav++)
				{
					tNodes[widthTrav][heightTrav].Dispose();
				}
			}
			this.m_nodes = null;
			base.Dispose();
		}

		// Token: 0x0400312B RID: 12587
		public Node[][] m_nodes;
	}
}
