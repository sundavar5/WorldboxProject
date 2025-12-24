using System;

// Token: 0x0200051F RID: 1311
public class Brush
{
	// Token: 0x06002B01 RID: 11009 RVA: 0x00155935 File Offset: 0x00153B35
	public static string getRandom()
	{
		return AssetManager.brush_library.list.GetRandom<BrushData>().id;
	}

	// Token: 0x06002B02 RID: 11010 RVA: 0x0015594C File Offset: 0x00153B4C
	public static string getRandom(int pMinSize, int pMaxSize = 50, Predicate<BrushData> pMatch = null)
	{
		foreach (BrushData tBrush in AssetManager.brush_library.list.LoopRandom<BrushData>())
		{
			if ((pMatch == null || pMatch(tBrush)) && tBrush.sqr_size >= pMinSize && tBrush.sqr_size <= pMaxSize)
			{
				return tBrush.id;
			}
		}
		return "circ_1";
	}

	// Token: 0x06002B03 RID: 11011 RVA: 0x001559CC File Offset: 0x00153BCC
	public static BrushData get(int pSize, string pID = "circ_")
	{
		string tID = pID + pSize.ToString();
		BrushData tAsset = AssetManager.brush_library.get(tID);
		if (tAsset != null)
		{
			return tAsset;
		}
		tAsset = AssetManager.brush_library.clone(tID, pID + "1");
		tAsset.size = pSize;
		AssetManager.brush_library.post_init();
		return tAsset;
	}

	// Token: 0x06002B04 RID: 11012 RVA: 0x00155A21 File Offset: 0x00153C21
	public static BrushData get(string pID)
	{
		return AssetManager.brush_library.get(pID);
	}
}
