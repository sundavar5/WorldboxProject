using System;

namespace EpPathFinding.cs
{
	// Token: 0x02000873 RID: 2163
	public abstract class ParamBase
	{
		// Token: 0x060043F7 RID: 17399 RVA: 0x001CBC60 File Offset: 0x001C9E60
		public ParamBase(BaseGrid iGrid, GridPos iStartPos, GridPos iEndPos, DiagonalMovement iDiagonalMovement, HeuristicMode iMode) : this(iGrid, iDiagonalMovement, iMode)
		{
			this.m_startNode = this.m_searchGrid.GetNodeAt(iStartPos.x, iStartPos.y);
			this.m_endNode = this.m_searchGrid.GetNodeAt(iEndPos.x, iEndPos.y);
			if (this.m_startNode == null)
			{
				this.m_startNode = new Node(iStartPos.x, iStartPos.y, new bool?(true));
			}
			if (this.m_endNode == null)
			{
				this.m_endNode = new Node(iEndPos.x, iEndPos.y, new bool?(true));
			}
		}

		// Token: 0x060043F8 RID: 17400 RVA: 0x001CBD08 File Offset: 0x001C9F08
		public void setGrid(BaseGrid iGrid, GridPos iStartPos, GridPos iEndPos)
		{
			this.m_searchGrid = iGrid;
			this.m_startNode = this.m_searchGrid.GetNodeAt(iStartPos.x, iStartPos.y);
			this.m_endNode = this.m_searchGrid.GetNodeAt(iEndPos.x, iEndPos.y);
			if (this.m_startNode == null)
			{
				this.m_startNode = new Node(iStartPos.x, iStartPos.y, new bool?(true));
			}
			if (this.m_endNode == null)
			{
				this.m_endNode = new Node(iEndPos.x, iEndPos.y, new bool?(true));
			}
		}

		// Token: 0x060043F9 RID: 17401 RVA: 0x001CBDAC File Offset: 0x001C9FAC
		public ParamBase(BaseGrid iGrid, DiagonalMovement iDiagonalMovement, HeuristicMode iMode)
		{
			this.SetHeuristic(iMode);
			this.m_searchGrid = iGrid;
			this.DiagonalMovement = iDiagonalMovement;
			this.m_startNode = null;
			this.m_endNode = null;
		}

		// Token: 0x060043FA RID: 17402 RVA: 0x001CBDD7 File Offset: 0x001C9FD7
		public ParamBase()
		{
		}

		// Token: 0x060043FB RID: 17403 RVA: 0x001CBDDF File Offset: 0x001C9FDF
		public ParamBase(ParamBase param)
		{
			this.m_searchGrid = param.m_searchGrid;
			this.DiagonalMovement = param.DiagonalMovement;
			this.m_startNode = param.m_startNode;
			this.m_endNode = param.m_endNode;
		}

		// Token: 0x060043FC RID: 17404
		internal abstract void _reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null);

		// Token: 0x060043FD RID: 17405 RVA: 0x001CBE18 File Offset: 0x001CA018
		public void Reset(GridPos iStartPos, GridPos iEndPos, BaseGrid iSearchGrid = null)
		{
			this._reset(iStartPos, iEndPos, iSearchGrid);
			this.m_startNode = null;
			this.m_endNode = null;
			if (iSearchGrid != null)
			{
				this.m_searchGrid = iSearchGrid;
			}
			this.m_searchGrid.Reset();
			this.m_startNode = this.m_searchGrid.GetNodeAt(iStartPos.x, iStartPos.y);
			this.m_endNode = this.m_searchGrid.GetNodeAt(iEndPos.x, iEndPos.y);
			if (this.m_startNode == null)
			{
				this.m_startNode = new Node(iStartPos.x, iStartPos.y, new bool?(true));
			}
			if (this.m_endNode == null)
			{
				this.m_endNode = new Node(iEndPos.x, iEndPos.y, new bool?(true));
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x060043FE RID: 17406 RVA: 0x001CBEE1 File Offset: 0x001CA0E1
		public BaseGrid SearchGrid
		{
			get
			{
				return this.m_searchGrid;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x060043FF RID: 17407 RVA: 0x001CBEE9 File Offset: 0x001CA0E9
		public Node StartNode
		{
			get
			{
				return this.m_startNode;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06004400 RID: 17408 RVA: 0x001CBEF1 File Offset: 0x001CA0F1
		public Node EndNode
		{
			get
			{
				return this.m_endNode;
			}
		}

		// Token: 0x06004401 RID: 17409 RVA: 0x001CBEF9 File Offset: 0x001CA0F9
		public void SetHeuristic(HeuristicMode iMode)
		{
			this.m_heuristicMode = iMode;
		}

		// Token: 0x04003137 RID: 12599
		internal BaseGrid m_searchGrid;

		// Token: 0x04003138 RID: 12600
		internal Node m_startNode;

		// Token: 0x04003139 RID: 12601
		internal Node m_endNode;

		// Token: 0x0400313A RID: 12602
		internal HeuristicMode m_heuristicMode;

		// Token: 0x0400313B RID: 12603
		public DiagonalMovement DiagonalMovement;
	}
}
