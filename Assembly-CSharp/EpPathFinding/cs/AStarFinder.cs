using System;
using System.Collections.Generic;

namespace EpPathFinding.cs
{
	// Token: 0x02000863 RID: 2147
	public static class AStarFinder
	{
		// Token: 0x06004340 RID: 17216 RVA: 0x001C8BB7 File Offset: 0x001C6DB7
		public static void backTracePath(Node pNode, List<WorldTile> pSavePath, bool pEndToStartPath = false)
		{
			pSavePath.Clear();
			pSavePath.Add(pNode.tile);
			while (pNode.parent != null)
			{
				pNode = pNode.parent;
				pSavePath.Add(pNode.tile);
			}
			if (!pEndToStartPath)
			{
				pSavePath.Reverse();
			}
		}

		// Token: 0x06004341 RID: 17217 RVA: 0x001C8BF4 File Offset: 0x001C6DF4
		public static void FindPath(AStarParam pParam, List<WorldTile> pSavePath)
		{
			AStarFinder._open_list.Clear();
			Node tStartNode;
			Node tEndNode;
			if (pParam.end_to_start_path)
			{
				tStartNode = pParam.EndNode;
				tEndNode = pParam.StartNode;
			}
			else
			{
				tStartNode = pParam.StartNode;
				tEndNode = pParam.EndNode;
			}
			StaticGrid tGrid = (StaticGrid)pParam.SearchGrid;
			DiagonalMovement tDiagonalMovement = pParam.DiagonalMovement;
			float tWeight = pParam.weight;
			bool tForBoat = pParam.boat;
			tStartNode.startToCurNodeLen = 0f;
			tStartNode.heuristicStartToEndLen = 0f;
			AStarFinder._open_list.Add(tStartNode);
			tStartNode.isOpened = true;
			AStarFinder.result_split_path = false;
			AStarFinder._last_global_region_id = 0;
			while (AStarFinder._open_list.Count != 0)
			{
				if (pParam.max_open_list != -1 && AStarFinder._open_list.Count > pParam.max_open_list)
				{
					return;
				}
				Node tNode = AStarFinder._open_list.DeleteMin();
				tNode.isClosed = true;
				tGrid.addToClosed(tNode);
				if (tNode == tEndNode)
				{
					AStarFinder.backTracePath(tEndNode, pSavePath, pParam.end_to_start_path);
					return;
				}
				WorldTile tCheckedTile = tNode.tile;
				if (pParam.use_global_path_lock)
				{
					if (tCheckedTile.region.region_path_id < AStarFinder._last_global_region_id && tCheckedTile.region.region_path_id != -1)
					{
						tNode.isClosed = true;
						tGrid.addToClosed(tNode);
						continue;
					}
					if (tCheckedTile.region.region_path_id > AStarFinder._last_global_region_id)
					{
						AStarFinder._last_global_region_id = tCheckedTile.region.region_path_id;
					}
				}
				WorldTile[] tNeighbourList;
				if (tDiagonalMovement == DiagonalMovement.Never)
				{
					tNeighbourList = tCheckedTile.neighbours;
				}
				else if (AStarFinder.isObstaclesAround(tCheckedTile))
				{
					tNeighbourList = tCheckedTile.neighbours;
				}
				else
				{
					tNeighbourList = tCheckedTile.neighboursAll;
				}
				foreach (WorldTile tNeighbourTile in tNeighbourList)
				{
					Node tNeighbour = tGrid.m_nodes[tNeighbourTile.x][tNeighbourTile.y];
					if (!tNeighbour.isClosed)
					{
						TileTypeBase tNeighbourType = tNeighbourTile.Type;
						if (pParam.block || !tNeighbourType.block)
						{
							if (pParam.use_global_path_lock)
							{
								if (tNeighbourTile.region.region_path_id < AStarFinder._last_global_region_id && tNeighbourTile.region.region_path_id != -1)
								{
									tNode.isClosed = true;
									tGrid.addToClosed(tNode);
									goto IL_45A;
								}
								if (tNeighbourTile.region.region_path_id > AStarFinder._last_global_region_id)
								{
									AStarFinder._last_global_region_id = tNeighbourTile.region.region_path_id;
								}
								if (!tNeighbourTile.region.used_by_path_lock)
								{
									tNode.isClosed = true;
									tGrid.addToClosed(tNode);
									goto IL_45A;
								}
							}
							if (tForBoat)
							{
								if (!tNeighbourTile.isGoodForBoat())
								{
									goto IL_45A;
								}
							}
							else
							{
								if (!pParam.lava && tNeighbourType.lava)
								{
									goto IL_45A;
								}
								if ((!pParam.block || !tNeighbourType.block) && (!pParam.lava || !tNeighbourType.lava))
								{
									if (pParam.ground && pParam.ocean)
									{
										if (!tNeighbourType.ground && !tNeighbourType.ocean)
										{
											goto IL_45A;
										}
									}
									else if ((pParam.ground && !tNeighbourType.ground) || (pParam.ocean && !tNeighbourType.ocean))
									{
										goto IL_45A;
									}
								}
							}
							float tCost = 1f;
							if (pParam.roads && tNeighbourType.road)
							{
								tCost = 0.01f;
							}
							if (AStarFinder._last_global_region_id >= 4 && tGrid.closed_list_count > 10)
							{
								AStarFinder.result_split_path = true;
								AStarFinder.backTracePath(tNode, pSavePath, pParam.end_to_start_path);
								return;
							}
							float tNodeLen = tNode.startToCurNodeLen + tCost * ((tNeighbour.x - tNode.x == 0 || tNeighbour.y - tNode.y == 0) ? 1f : 1.414f);
							if (!tNeighbour.isOpened || tNodeLen < tNeighbour.startToCurNodeLen)
							{
								tNeighbour.startToCurNodeLen = tNodeLen;
								if (tNeighbour.heuristicCurNodeToEndLen == null)
								{
									if (pParam.roads)
									{
										tNeighbour.heuristicCurNodeToEndLen = new float?(Heuristic.Euclidean(Math.Abs(tNeighbour.x - tEndNode.x), Math.Abs(tNeighbour.y - tEndNode.y)) * tWeight);
									}
									else
									{
										tNeighbour.heuristicCurNodeToEndLen = new float?((float)(Math.Abs(tNeighbour.x - tEndNode.x) + Math.Abs(tNeighbour.y - tEndNode.y)) * tWeight);
									}
								}
								tNeighbour.heuristicStartToEndLen = tNeighbour.startToCurNodeLen + tNeighbour.heuristicCurNodeToEndLen.Value;
								tNeighbour.parent = tNode;
								if (!tNeighbour.isOpened)
								{
									AStarFinder._open_list.Add(tNeighbour);
									tNeighbour.isOpened = true;
									tGrid.addToClosed(tNeighbour);
								}
							}
						}
					}
					IL_45A:;
				}
			}
		}

		// Token: 0x06004342 RID: 17218 RVA: 0x001C907B File Offset: 0x001C727B
		private static bool isObstaclesAround(WorldTile pTile)
		{
			return pTile.hasWallsAround();
		}

		// Token: 0x040030FD RID: 12541
		private static int _last_global_region_id;

		// Token: 0x040030FE RID: 12542
		public static bool result_split_path;

		// Token: 0x040030FF RID: 12543
		private static IntervalHeap<Node> _open_list = new IntervalHeap<Node>();
	}
}
