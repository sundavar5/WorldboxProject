using System;
using System.Collections.Generic;
using ai.behaviours;

// Token: 0x020004C1 RID: 1217
public class TesterBehFillWorld : BehaviourActionTester
{
	// Token: 0x060029D5 RID: 10709 RVA: 0x00149C23 File Offset: 0x00147E23
	public TesterBehFillWorld(string pType)
	{
		this.type = pType;
	}

	// Token: 0x060029D6 RID: 10710 RVA: 0x00149C34 File Offset: 0x00147E34
	public override BehResult execute(AutoTesterBot pObject)
	{
		if (TesterBehFillWorld.tiles.Count == 0)
		{
			foreach (TileType tTileType in AssetManager.tiles.list)
			{
				if (tTileType.can_be_autotested)
				{
					TesterBehFillWorld.tiles.Add(tTileType);
				}
			}
			foreach (TopTileType tTileType2 in AssetManager.top_tiles.list)
			{
				if (tTileType2.can_be_autotested)
				{
					TesterBehFillWorld.top_tiles.Add(tTileType2);
				}
			}
		}
		TopTileType tTopType = null;
		TileType tType;
		if (this.type == "random")
		{
			tTopType = TesterBehFillWorld.top_tiles.GetRandom<TopTileType>();
			if (tTopType.is_biome)
			{
				tType = (Randy.randomBool() ? TileLibrary.soil_high : TileLibrary.soil_low);
			}
			else
			{
				tType = TesterBehFillWorld.tiles.GetRandom<TileType>();
			}
		}
		else
		{
			tType = AssetManager.tiles.get(this.type);
		}
		for (int i = 0; i < 3; i++)
		{
			WorldTile[] tTiles = BehaviourActionBase<AutoTesterBot>.world.map_chunk_manager.chunks.GetRandom<MapChunk>().tiles;
			int tCount = tTiles.Length;
			for (int j = 0; j < tCount; j++)
			{
				WorldTile tTile = tTiles[j];
				MapAction.terraformMain(tTile, tType, TerraformLibrary.destroy_no_flash, false);
				if (tTopType != null)
				{
					MapAction.terraformTop(tTile, tTopType, TerraformLibrary.destroy_no_flash, false);
				}
			}
		}
		return base.execute(pObject);
	}

	// Token: 0x04001F2E RID: 7982
	private static List<TileType> tiles = new List<TileType>();

	// Token: 0x04001F2F RID: 7983
	private static List<TopTileType> top_tiles = new List<TopTileType>();

	// Token: 0x04001F30 RID: 7984
	private string type;
}
