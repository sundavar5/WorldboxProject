using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x02000871 RID: 2161
	public class NodePool
	{
		// Token: 0x060043EB RID: 17387 RVA: 0x001CBB25 File Offset: 0x001C9D25
		public NodePool()
		{
			this.m_nodes = new Dictionary<GridPos, Node>();
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x060043EC RID: 17388 RVA: 0x001CBB38 File Offset: 0x001C9D38
		public Dictionary<GridPos, Node> Nodes
		{
			get
			{
				return this.m_nodes;
			}
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x001CBB40 File Offset: 0x001C9D40
		public Node GetNode(int iX, int iY)
		{
			GridPos pos = new GridPos(iX, iY);
			return this.GetNode(pos);
		}

		// Token: 0x060043EE RID: 17390 RVA: 0x001CBB5C File Offset: 0x001C9D5C
		public Node GetNode(GridPos iPos)
		{
			Node retVal = null;
			this.m_nodes.TryGetValue(iPos, out retVal);
			return retVal;
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x001CBB7C File Offset: 0x001C9D7C
		public Node SetNode(int iX, int iY, bool? iWalkable = null)
		{
			GridPos pos = new GridPos(iX, iY);
			return this.SetNode(pos, iWalkable);
		}

		// Token: 0x060043F0 RID: 17392 RVA: 0x001CBB9C File Offset: 0x001C9D9C
		public Node SetNode(GridPos iPos, bool? iWalkable = null)
		{
			if (iWalkable == null)
			{
				Node newNode = new Node(iPos.x, iPos.y, new bool?(true));
				this.m_nodes.Add(iPos, newNode);
				return newNode;
			}
			if (!iWalkable.Value)
			{
				this.removeNode(iPos);
				return null;
			}
			Node retVal = null;
			if (this.m_nodes.TryGetValue(iPos, out retVal))
			{
				return retVal;
			}
			Node newNode2 = new Node(iPos.x, iPos.y, iWalkable);
			this.m_nodes.Add(iPos, newNode2);
			return newNode2;
		}

		// Token: 0x060043F1 RID: 17393 RVA: 0x001CBC24 File Offset: 0x001C9E24
		protected void removeNode(int iX, int iY)
		{
			GridPos pos = new GridPos(iX, iY);
			this.removeNode(pos);
		}

		// Token: 0x060043F2 RID: 17394 RVA: 0x001CBC40 File Offset: 0x001C9E40
		protected void removeNode(GridPos iPos)
		{
			if (this.m_nodes.ContainsKey(iPos))
			{
				this.m_nodes.Remove(iPos);
			}
		}

		// Token: 0x04003136 RID: 12598
		protected Dictionary<GridPos, Node> m_nodes;
	}
}
