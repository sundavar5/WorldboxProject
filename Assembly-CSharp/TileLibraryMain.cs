using System;
using System.Collections.Generic;

// Token: 0x0200007E RID: 126
public class TileLibraryMain<T> : AssetLibrary<T> where T : TileTypeBase
{
	// Token: 0x06000478 RID: 1144 RVA: 0x0002F1A8 File Offset: 0x0002D3A8
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (T t in this.list)
		{
			TileTypeBase tTileType = t;
			TileTypeBase tileTypeBase = tTileType;
			HashSet<BiomeTag> biome_tags = tTileType.biome_tags;
			tileTypeBase.has_biome_tags = (biome_tags != null && biome_tags.Count > 0);
			if (tTileType.color_hex != null)
			{
				tTileType.color = Toolbox.makeColor(tTileType.color_hex);
			}
			if (tTileType.edge_color_hex != null)
			{
				tTileType.edge_color = Toolbox.makeColor(tTileType.edge_color_hex);
			}
		}
	}
}
