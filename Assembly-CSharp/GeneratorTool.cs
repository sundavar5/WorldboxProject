using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x02000404 RID: 1028
public class GeneratorTool : ScriptableObject
{
	// Token: 0x06002377 RID: 9079 RVA: 0x00127500 File Offset: 0x00125700
	internal static void Setup(WorldTile[,] pTilesMap)
	{
		GeneratorTool._tiles_map = pTilesMap;
	}

	// Token: 0x06002378 RID: 9080 RVA: 0x00127508 File Offset: 0x00125708
	public static void Init()
	{
		GeneratorTool.LoadGenShapeTextures();
	}

	// Token: 0x06002379 RID: 9081 RVA: 0x00127510 File Offset: 0x00125710
	internal static void applyTemplate(string pID, float pMod = 1f)
	{
		Texture2D tTexture = Resources.LoadAll<Texture2D>("map_gen/" + pID).GetRandom<Texture2D>();
		tTexture = TextureRotator.Rotate(tTexture, Randy.randomInt(0, 360), new Color32(byte.MaxValue, byte.MaxValue, byte.MaxValue, byte.MaxValue));
		TextureScale.Bilinear(tTexture, MapBox.width, MapBox.height);
		float tHeightMod = 255f * pMod;
		for (int x = 0; x < tTexture.width; x++)
		{
			for (int y = 0; y < tTexture.height; y++)
			{
				WorldTile tTile = World.world.GetTile(x, y);
				if (tTile != null)
				{
					tTexture.GetPixel(x, y);
					int tVal = (int)((1f - tTexture.GetPixel(x, y).g) * tHeightMod);
					tTile.Height += tVal;
				}
			}
		}
	}

	// Token: 0x0600237A RID: 9082 RVA: 0x001275DC File Offset: 0x001257DC
	internal static void ApplyRandomShape(string pWhat = "height", float tDistMax = 2f, float pMod = 0.7f, bool pSubtract = false)
	{
		Texture2D texture = Object.Instantiate<Texture2D>(GeneratorTool._textures.GetRandom<Texture2D>());
		texture.name = "random_shape";
		int newW = (int)((float)texture.width * Randy.randomFloat(0.3f, 2f));
		int newH = (int)((float)texture.height * Randy.randomFloat(0.3f, 2f));
		texture = TextureRotator.Rotate(texture, Randy.randomInt(0, 360), new Color32(0, 0, 0, 0));
		TextureScale.Bilinear(texture, newW, newH);
		newW = texture.width;
		newH = texture.height;
		int tPosX = MapBox.width / 2 - newW / 2 - (int)Randy.randomFloat((float)(-(float)newW) * tDistMax, (float)newW * tDistMax);
		int tPosY = MapBox.height / 2 - newH / 2 - (int)Randy.randomFloat((float)(-(float)newH) * tDistMax, (float)newH * tDistMax);
		if (tPosX < 0)
		{
			tPosX = 0;
		}
		if (tPosY < 0)
		{
			tPosY = 0;
		}
		if (tPosX + newW > MapBox.width)
		{
			tPosX = MapBox.width - newW;
		}
		if (tPosY + newH > MapBox.height)
		{
			tPosY = MapBox.height - newH;
		}
		float tHeightMod = 255f * pMod;
		for (int x = 0; x < texture.width; x++)
		{
			for (int y = 0; y < texture.height; y++)
			{
				WorldTile tTile = World.world.GetTile(tPosX + x, tPosY + y);
				if (tTile != null)
				{
					int tVal = (int)(texture.GetPixel(x, y).a * tHeightMod);
					if (pSubtract)
					{
						tVal = -tVal;
					}
					tTile.Height += tVal;
				}
			}
		}
		Object.Destroy(texture);
	}

	// Token: 0x0600237B RID: 9083 RVA: 0x00127759 File Offset: 0x00125959
	private static void LoadGenShapeTextures()
	{
		if (GeneratorTool._textures != null)
		{
			return;
		}
		GeneratorTool._textures = Resources.LoadAll<Texture2D>("gen_shapes");
	}

	// Token: 0x0600237C RID: 9084 RVA: 0x00127774 File Offset: 0x00125974
	public static void ApplyWaterLevel(WorldTile[,] tilesMap, int width, int height, int pVal)
	{
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				tilesMap[x, y].Height -= pVal;
			}
		}
	}

	// Token: 0x0600237D RID: 9085 RVA: 0x001277B0 File Offset: 0x001259B0
	public static void ApplyPerlinNoise(WorldTile[,] tilesMap, int width, int height, float pPosX, float pPosY, float pAlphaMod, float pScaleMod, bool pSubtract = false, GeneratorTarget pTarget = GeneratorTarget.Height)
	{
		float tAlphaMod = 255f * pAlphaMod;
		float tScaleX = 1f;
		float tScaleY = 1f;
		if (width > height)
		{
			tScaleX = (float)(width / height);
		}
		else
		{
			tScaleY = (float)(height / width);
		}
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				float num = (pPosX + (float)x) / (float)width;
				float tY = (pPosY + (float)y) / (float)height;
				int tValue = (int)(Mathf.PerlinNoise(num * pScaleMod * tScaleX, tY * pScaleMod * tScaleY) * tAlphaMod);
				if (pSubtract)
				{
					tValue = -tValue;
				}
				if (pTarget == GeneratorTarget.Height)
				{
					tilesMap[x, y].Height += tValue;
				}
			}
		}
	}

	// Token: 0x0600237E RID: 9086 RVA: 0x0012784C File Offset: 0x00125A4C
	public static void ApplyPerlinReplace(PerlinReplaceContainer pContainer)
	{
		float pPosX = (float)Randy.randomInt(0, 15000);
		float pPosY = (float)Randy.randomInt(0, 15000);
		int width = MapBox.width;
		int height = MapBox.height;
		float pScaleMod = (float)pContainer.scale;
		float tMaxHeight = 255f;
		float tScaleX = 1f;
		float tScaleY = 1f;
		if (width > height)
		{
			tScaleX = (float)(width / height);
		}
		else
		{
			tScaleY = (float)(height / width);
		}
		WorldTile[,] tTilesMap = GeneratorTool._tiles_map;
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				WorldTile tTile = tTilesMap[x, y];
				float num = (pPosX + (float)x) / (float)width;
				float tY = (pPosY + (float)y) / (float)height;
				int tValue = (int)(Mathf.PerlinNoise(num * pScaleMod * tScaleX, tY * pScaleMod * tScaleY) * tMaxHeight);
				for (int i = 0; i < pContainer.options.Count; i++)
				{
					PerlinReplaceOption tOption = pContainer.options[i];
					if (tValue > tOption.replace_height_value && tTile.main_type.IsType(tOption.from))
					{
						tTile.setTileType(tOption.to);
					}
				}
			}
		}
	}

	// Token: 0x0600237F RID: 9087 RVA: 0x00127974 File Offset: 0x00125B74
	public static void UpdateTileTypes(bool pGeneratorStage = false, int pStartIndex = 0, int pAmount = 0)
	{
		int tMax = pStartIndex + pAmount;
		for (int i = pStartIndex; i < tMax; i++)
		{
			WorldTile tTile = World.world.tiles_list[i];
			TileType tType = AssetManager.tiles.getTypeByDepth(tTile);
			tTile.setTileType(tType, true);
		}
	}

	// Token: 0x06002380 RID: 9088 RVA: 0x001279B4 File Offset: 0x00125BB4
	public static void GenerateTileNeighbours(WorldTile[] pTilesList)
	{
		int tCount = pTilesList.Length;
		for (int i = 0; i < tCount; i++)
		{
			GeneratorTool.generateTileNeighbours(pTilesList[i]);
		}
	}

	// Token: 0x06002381 RID: 9089 RVA: 0x001279DC File Offset: 0x00125BDC
	public static void generateTileNeighbours(WorldTile pTile)
	{
		WorldTile tNeighbour = GeneratorTool.getTile(pTile.x - 1, pTile.y);
		pTile.addNeighbour(tNeighbour, TileDirection.Left, GeneratorTool._neighbours, GeneratorTool._neighbours_all, false);
		tNeighbour = GeneratorTool.getTile(pTile.x + 1, pTile.y);
		pTile.addNeighbour(tNeighbour, TileDirection.Right, GeneratorTool._neighbours, GeneratorTool._neighbours_all, false);
		tNeighbour = GeneratorTool.getTile(pTile.x, pTile.y - 1);
		pTile.addNeighbour(tNeighbour, TileDirection.Down, GeneratorTool._neighbours, GeneratorTool._neighbours_all, false);
		tNeighbour = GeneratorTool.getTile(pTile.x, pTile.y + 1);
		pTile.addNeighbour(tNeighbour, TileDirection.Up, GeneratorTool._neighbours, GeneratorTool._neighbours_all, false);
		tNeighbour = GeneratorTool.getTile(pTile.x - 1, pTile.y - 1);
		pTile.addNeighbour(tNeighbour, TileDirection.Null, GeneratorTool._neighbours, GeneratorTool._neighbours_all, true);
		tNeighbour = GeneratorTool.getTile(pTile.x - 1, pTile.y + 1);
		pTile.addNeighbour(tNeighbour, TileDirection.Null, GeneratorTool._neighbours, GeneratorTool._neighbours_all, true);
		tNeighbour = GeneratorTool.getTile(pTile.x + 1, pTile.y - 1);
		pTile.addNeighbour(tNeighbour, TileDirection.Null, GeneratorTool._neighbours, GeneratorTool._neighbours_all, true);
		tNeighbour = GeneratorTool.getTile(pTile.x + 1, pTile.y + 1);
		pTile.addNeighbour(tNeighbour, TileDirection.Null, GeneratorTool._neighbours, GeneratorTool._neighbours_all, true);
		pTile.neighbours = GeneratorTool._neighbours.ToArray();
		pTile.neighboursAll = GeneratorTool._neighbours_all.ToArray();
		GeneratorTool._neighbours.Clear();
		GeneratorTool._neighbours_all.Clear();
	}

	// Token: 0x06002382 RID: 9090 RVA: 0x00127B60 File Offset: 0x00125D60
	public static void ApplyRingEffect()
	{
		WorldTile[,] tTilesMap = GeneratorTool._tiles_map;
		for (int x = 0; x < MapBox.width; x++)
		{
			for (int y = 0; y < MapBox.height; y++)
			{
				for (int i = 0; i < AssetManager.tiles.list.Count; i++)
				{
					TileType tType = AssetManager.tiles.list[i];
					if (tType.additional_height != null)
					{
						bool found = false;
						for (int j = 0; j < tType.additional_height.Length; j++)
						{
							WorldTile tWorldTile = tTilesMap[x, y];
							if (tWorldTile.Height == tType.height_min - tType.additional_height[j])
							{
								tWorldTile.Height = tType.height_min;
								found = true;
								break;
							}
						}
						if (found)
						{
							break;
						}
					}
				}
			}
		}
	}

	// Token: 0x06002383 RID: 9091 RVA: 0x00127C2E File Offset: 0x00125E2E
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	private static WorldTile getTile(int pX, int pY)
	{
		if (pX < 0 || pX >= MapBox.width)
		{
			return null;
		}
		if (pY < 0 || pY >= MapBox.height)
		{
			return null;
		}
		return GeneratorTool._tiles_map[pX, pY];
	}

	// Token: 0x040019AB RID: 6571
	private static WorldTile[,] _tiles_map;

	// Token: 0x040019AC RID: 6572
	private static Texture2D[] _textures;

	// Token: 0x040019AD RID: 6573
	private static List<WorldTile> _neighbours = new List<WorldTile>(4);

	// Token: 0x040019AE RID: 6574
	private static List<WorldTile> _neighbours_all = new List<WorldTile>(8);
}
