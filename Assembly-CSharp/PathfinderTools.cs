using System;
using System.Collections.Generic;
using EpPathFinding.cs;
using UnityEngine;

// Token: 0x0200043A RID: 1082
public class PathfinderTools
{
	// Token: 0x060025A5 RID: 9637 RVA: 0x00136848 File Offset: 0x00134A48
	public static List<WorldTile> raycast(WorldTile pFrom, WorldTile pTarget, float pMod = 0.99f)
	{
		List<WorldTile> tList = PathfinderTools._raycast_result;
		tList.Clear();
		float tDist = Toolbox.DistTile(pFrom, pTarget) * pMod;
		Vector2 tVecA = new Vector2(pFrom.posV3.x, pFrom.posV3.y);
		Vector2 tVecB = new Vector2(pTarget.posV3.x, pTarget.posV3.y);
		WorldTile tPrevTile = null;
		int i = 0;
		while ((float)i <= tDist)
		{
			Vector2 vector = Vector2.Lerp(tVecA, tVecB, (float)i / tDist);
			int tX = Mathf.FloorToInt(vector.x);
			int tY = Mathf.FloorToInt(vector.y);
			WorldTile tTile = World.world.GetTile(tX, tY);
			if (tTile != null && tTile != tPrevTile)
			{
				tList.Add(tTile);
				tPrevTile = tTile;
			}
			i++;
		}
		if (tPrevTile != pTarget)
		{
			tList.Add(pTarget);
		}
		return tList;
	}

	// Token: 0x060025A6 RID: 9638 RVA: 0x00136914 File Offset: 0x00134B14
	public static List<WorldTile> raycast(Vector2 pFrom, Vector2 pTarget, float pMod = 0.99f)
	{
		List<WorldTile> tList = PathfinderTools._raycast_result;
		tList.Clear();
		float tDist = Toolbox.DistVec2Float(pFrom, pTarget) * pMod;
		WorldTile tPrevTile = null;
		int i = 0;
		while ((float)i <= tDist)
		{
			Vector2 vector = Vector2.Lerp(pFrom, pTarget, (float)i / tDist);
			int tX = Mathf.FloorToInt(vector.x);
			int tY = Mathf.FloorToInt(vector.y);
			WorldTile tTile = World.world.GetTile(tX, tY);
			if (tTile != null && tTile != tPrevTile)
			{
				tList.Add(tTile);
				tPrevTile = tTile;
			}
			i++;
		}
		if (tPrevTile.pos != pTarget)
		{
			WorldTile tMainTile = World.world.GetTile((int)pTarget.x, (int)pTarget.y);
			if (tMainTile != null)
			{
				tList.Add(tMainTile);
			}
		}
		return tList;
	}

	// Token: 0x060025A7 RID: 9639 RVA: 0x001369C8 File Offset: 0x00134BC8
	public static bool tryToGetSimplePath(WorldTile pFrom, WorldTile pTargetTile, List<WorldTile> pPathToFill, ActorAsset pAsset, AStarParam pParam, int pTileLimit = 0)
	{
		PathfinderTools.raycast(pFrom, pTargetTile, 0.99f);
		List<WorldTile> tResults = PathfinderTools._raycast_result;
		int i = 0;
		int tLenTiles = tResults.Count;
		while (i < tLenTiles)
		{
			WorldTile tTile = tResults[i];
			if (!pParam.block && tTile.Type.block)
			{
				return false;
			}
			if (!pParam.lava && tTile.Type.lava)
			{
				return false;
			}
			if (!pParam.fire && tTile.isOnFire())
			{
				return false;
			}
			if (!pParam.ocean && tTile.Type.ocean)
			{
				return false;
			}
			if (!pParam.ground && tTile.Type.ground)
			{
				return false;
			}
			if (pParam.boat && !tTile.isGoodForBoat())
			{
				return false;
			}
			if (tTile.hasWallsAround())
			{
				return false;
			}
			i++;
		}
		return true;
	}

	// Token: 0x060025A8 RID: 9640 RVA: 0x00136A9C File Offset: 0x00134C9C
	public static WorldTile raycastTileForUnitToEmbark(WorldTile pFromGround, WorldTile pTargetOcean)
	{
		List<WorldTile> tRaycastResult = PathfinderTools.raycast(pTargetOcean, pFromGround, 0.99f);
		WorldTile tResult = null;
		TileIsland tIsland = pFromGround.region.island;
		for (int i = 0; i < tRaycastResult.Count; i++)
		{
			WorldTile tTile = tRaycastResult[i];
			if (tTile.region.island == tIsland)
			{
				tResult = tTile;
				break;
			}
		}
		tRaycastResult.Clear();
		return tResult;
	}

	// Token: 0x060025A9 RID: 9641 RVA: 0x00136AF9 File Offset: 0x00134CF9
	public static List<WorldTile> getLastRaycastResult()
	{
		return PathfinderTools._raycast_result;
	}

	// Token: 0x060025AA RID: 9642 RVA: 0x00136B00 File Offset: 0x00134D00
	public static WorldTile raycastTileForUnitLandingFromOcean(WorldTile pFromOcean, WorldTile pTargetGround)
	{
		List<WorldTile> tRaycastResult = PathfinderTools.raycast(pFromOcean, pTargetGround, 0.99f);
		WorldTile tResult = null;
		TileIsland tIsland = pTargetGround.region.island;
		for (int i = 0; i < tRaycastResult.Count; i++)
		{
			WorldTile tTile = tRaycastResult[i];
			if (tTile.region.island == tIsland)
			{
				tResult = tTile;
				break;
			}
		}
		tRaycastResult.Clear();
		return tResult;
	}

	// Token: 0x04001C92 RID: 7314
	private static readonly List<WorldTile> _raycast_result = new List<WorldTile>();
}
