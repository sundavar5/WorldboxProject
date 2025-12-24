using System;
using Beebyte.Obfuscator;

// Token: 0x0200005D RID: 93
[Skip]
public class MapSizeLibrary : AssetLibrary<MapSizeAsset>
{
	// Token: 0x06000363 RID: 867 RVA: 0x0001F335 File Offset: 0x0001D535
	public static string[] getSizes()
	{
		return MapSizeLibrary._mapSizes;
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0001F33C File Offset: 0x0001D53C
	public override void init()
	{
		base.init();
		this.add(new MapSizeAsset
		{
			id = "tiny",
			size = 2,
			path_icon = "actor_traits/iconTiny"
		});
		this.add(new MapSizeAsset
		{
			id = "small",
			size = 3,
			path_icon = "iconAntBlack"
		});
		this.add(new MapSizeAsset
		{
			id = "standard",
			size = 4,
			path_icon = "iconTileSand"
		});
		this.add(new MapSizeAsset
		{
			id = "large",
			size = 5,
			path_icon = "iconTileSoil",
			show_warning = true
		});
		this.add(new MapSizeAsset
		{
			id = "huge",
			size = 6,
			path_icon = "iconTileHighSoil",
			show_warning = true
		});
		this.add(new MapSizeAsset
		{
			id = "gigantic",
			size = 7,
			path_icon = "iconTileMountains",
			show_warning = true
		});
		this.add(new MapSizeAsset
		{
			id = "titanic",
			size = 8,
			path_icon = "iconTitanic",
			show_warning = true
		});
		this.add(new MapSizeAsset
		{
			id = "iceberg",
			size = 9,
			path_icon = "iconIceberg",
			show_warning = true
		});
	}

	// Token: 0x06000365 RID: 869 RVA: 0x0001F4BB File Offset: 0x0001D6BB
	public override void linkAssets()
	{
		base.linkAssets();
		this.convertToOldFormat();
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0001F4CC File Offset: 0x0001D6CC
	private void convertToOldFormat()
	{
		MapSizeLibrary._mapSizes = new string[this.list.Count];
		for (int i = 0; i < MapSizeLibrary._mapSizes.Length; i++)
		{
			MapSizeLibrary._mapSizes[i] = this.list[i].id;
		}
	}

	// Token: 0x06000367 RID: 871 RVA: 0x0001F518 File Offset: 0x0001D718
	public static int getSize(string pId)
	{
		MapSizeAsset tAsset = AssetManager.map_sizes.get(pId);
		if (tAsset == null)
		{
			return 2;
		}
		return tAsset.size;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x0001F53C File Offset: 0x0001D73C
	public static MapSizeAsset getPresetAsset(int pMapSize)
	{
		foreach (MapSizeAsset tAsset in AssetManager.map_sizes.list)
		{
			if (tAsset.size == pMapSize)
			{
				return tAsset;
			}
		}
		return null;
	}

	// Token: 0x06000369 RID: 873 RVA: 0x0001F59C File Offset: 0x0001D79C
	public static string getPreset(int pMapSize)
	{
		foreach (MapSizeAsset tAsset in AssetManager.map_sizes.list)
		{
			if (tAsset.size == pMapSize)
			{
				return tAsset.id;
			}
		}
		return null;
	}

	// Token: 0x0600036A RID: 874 RVA: 0x0001F604 File Offset: 0x0001D804
	public static bool isSizeValid(int pMapSize)
	{
		return MapSizeLibrary.getPresetAsset(pMapSize) != null && pMapSize <= MapSizeLibrary.getSize(Config.maxMapSize);
	}

	// Token: 0x0600036B RID: 875 RVA: 0x0001F620 File Offset: 0x0001D820
	public override void editorDiagnosticLocales()
	{
		foreach (MapSizeAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
		base.editorDiagnosticLocales();
	}

	// Token: 0x040002F3 RID: 755
	public const string tiny = "tiny";

	// Token: 0x040002F4 RID: 756
	public const string small = "small";

	// Token: 0x040002F5 RID: 757
	public const string standard = "standard";

	// Token: 0x040002F6 RID: 758
	public const string large = "large";

	// Token: 0x040002F7 RID: 759
	public const string huge = "huge";

	// Token: 0x040002F8 RID: 760
	public const string gigantic = "gigantic";

	// Token: 0x040002F9 RID: 761
	public const string titanic = "titanic";

	// Token: 0x040002FA RID: 762
	public const string iceberg = "iceberg";

	// Token: 0x040002FB RID: 763
	private static string[] _mapSizes;
}
