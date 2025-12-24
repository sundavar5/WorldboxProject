using System;
using UnityEngine;

// Token: 0x02000408 RID: 1032
public class MapEdges
{
	// Token: 0x0600239F RID: 9119 RVA: 0x001285A4 File Offset: 0x001267A4
	internal static void AddEdgeGradientCircle(WorldTile[,] pMap, string pWhat)
	{
		WorldTile tCenter = pMap[MapBox.width / 2, MapBox.height / 2];
		float tMaxMod = 0.99f;
		float tGradientMod = 0.85f;
		float tMaxCenter = (float)(MapBox.width / 2) * tMaxMod;
		float tGradient = (float)(MapBox.width / 2) * tGradientMod;
		float tDiff = tMaxCenter - tGradient;
		foreach (WorldTile tTile in World.world.tiles_list)
		{
			float tDist = Toolbox.DistTile(tTile, tCenter);
			if (tDist > tMaxCenter)
			{
				tTile.Height = 0;
			}
			else if (tDist < tMaxCenter && tDist > tGradient)
			{
				float tMod = (tMaxCenter - tDist) / tDiff;
				int tNewHeight = (int)((float)tTile.Height * tMod);
				tTile.Height = tNewHeight;
			}
		}
	}

	// Token: 0x060023A0 RID: 9120 RVA: 0x0012865C File Offset: 0x0012685C
	internal static void AddEdgeSquare(WorldTile[,] pMap, string pWhat)
	{
		MapEdges.edgeSize = 64;
		if (MapEdges.textureLeft == null)
		{
			MapEdges.textureLeft = (Texture2D)Resources.Load("edges/edge100xLeft");
			MapEdges.textureRight = (Texture2D)Resources.Load("edges/edge100xRight");
			MapEdges.textureUp = (Texture2D)Resources.Load("edges/edge100xUp");
			MapEdges.textureDown = (Texture2D)Resources.Load("edges/edge100xDown");
			MapEdges.textureTempUp = (Texture2D)Resources.Load("edges/edgeTempUp");
			MapEdges.textureTempDown = (Texture2D)Resources.Load("edges/edgeTempDown");
		}
		int tCountWidth = (int)((float)MapBox.width / (float)MapEdges.edgeSize) + 1;
		int tCountHeight = (int)((float)MapBox.height / (float)MapEdges.edgeSize) + 1;
		if (pWhat == "temperature")
		{
			for (int iX = 0; iX < tCountWidth; iX++)
			{
				MapEdges.fill(iX, 0, MapEdges.textureTempDown, pMap, pWhat);
			}
			for (int iX2 = 0; iX2 < tCountWidth; iX2++)
			{
				MapEdges.fill(iX2, tCountHeight - 2, MapEdges.textureTempUp, pMap, pWhat);
			}
			return;
		}
		for (int iY = 0; iY < tCountHeight; iY++)
		{
			MapEdges.fill(0, iY, MapEdges.textureLeft, pMap, pWhat);
		}
		for (int iY2 = 0; iY2 < tCountHeight; iY2++)
		{
			MapEdges.fill(tCountWidth - 2, iY2, MapEdges.textureRight, pMap, pWhat);
		}
		for (int iX3 = 0; iX3 < tCountWidth; iX3++)
		{
			MapEdges.fill(iX3, 0, MapEdges.textureDown, pMap, pWhat);
		}
		for (int iX4 = 0; iX4 < tCountWidth; iX4++)
		{
			MapEdges.fill(iX4, tCountHeight - 2, MapEdges.textureUp, pMap, pWhat);
		}
	}

	// Token: 0x060023A1 RID: 9121 RVA: 0x001287DC File Offset: 0x001269DC
	internal static void fill(int pX, int pY, Texture2D pTexture, WorldTile[,] tilesMap, string pWhat)
	{
		for (int y = 0; y < pTexture.height; y++)
		{
			for (int x = 0; x < pTexture.width; x++)
			{
				int tHeight = (int)(pTexture.GetPixel(x, y).a * 255f);
				int tX = x + pX * MapEdges.edgeSize;
				int tY = y + pY * MapEdges.edgeSize;
				if (tX < MapBox.width && tY < MapBox.height)
				{
					WorldTile tWorldTile = tilesMap[tX, tY];
					if (tWorldTile != null && pWhat == "height")
					{
						tWorldTile.Height -= tHeight;
					}
				}
			}
		}
	}

	// Token: 0x040019B9 RID: 6585
	private static Texture2D textureLeft;

	// Token: 0x040019BA RID: 6586
	private static Texture2D textureRight;

	// Token: 0x040019BB RID: 6587
	private static Texture2D textureUp;

	// Token: 0x040019BC RID: 6588
	private static Texture2D textureDown;

	// Token: 0x040019BD RID: 6589
	private static Texture2D textureTempUp;

	// Token: 0x040019BE RID: 6590
	private static Texture2D textureTempDown;

	// Token: 0x040019BF RID: 6591
	private static int edgeSize;
}
