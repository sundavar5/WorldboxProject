using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002C RID: 44
public class BrushLibrary : AssetLibrary<BrushData>
{
	// Token: 0x06000216 RID: 534 RVA: 0x00012EAC File Offset: 0x000110AC
	public override void init()
	{
		base.init();
		BrushData brushData = new BrushData();
		brushData.id = "circ_0";
		brushData.size = 0;
		brushData.group = BrushGroup.Circles;
		brushData.fast_spawn = true;
		brushData.continuous = true;
		brushData.localized_key = "brush_circ";
		brushData.generate_action = delegate(BrushData pAsset)
		{
			pAsset.pos = new BrushPixelData[]
			{
				new BrushPixelData(0, 0, 0)
			};
		};
		this.add(brushData);
		this.t.show_in_brush_window = true;
		this.clone("circ_1", "circ_0");
		this.t.size = 1;
		this.t.fast_spawn = false;
		this.t.show_in_brush_window = true;
		this.t.generate_action = delegate(BrushData pAsset)
		{
			int tRadius = pAsset.size;
			int tX = 0;
			int tY = tRadius;
			int tDiameter = 1 - tRadius;
			HashSet<BrushPixelData> tTempList = new HashSet<BrushPixelData>();
			while (tX <= tY)
			{
				for (int i = -tX; i <= tX; i++)
				{
					int tDist = (int)Toolbox.Dist(0, 0, i, tY);
					tTempList.Add(new BrushPixelData(i, tY, tDist));
					tTempList.Add(new BrushPixelData(i, -tY, tDist));
				}
				for (int j = -tY; j <= tY; j++)
				{
					int tDist2 = (int)Toolbox.Dist(0, 0, tX, j);
					tTempList.Add(new BrushPixelData(j, tX, tDist2));
					tTempList.Add(new BrushPixelData(j, -tX, tDist2));
				}
				if (tDiameter < 0)
				{
					tDiameter += 2 * tX + 3;
				}
				else
				{
					tDiameter += 2 * (tX - tY) + 5;
					tY--;
				}
				tX++;
			}
			pAsset.pos = tTempList.ToArray<BrushPixelData>();
		};
		this.clone("circ_2", "circ_1");
		this.t.size = 2;
		this.t.show_in_brush_window = true;
		this.clone("circ_3", "circ_1");
		this.t.size = 3;
		this.t.show_in_brush_window = true;
		this.clone("circ_4", "circ_1");
		this.t.size = 4;
		this.t.continuous = false;
		this.t.show_in_brush_window = true;
		this.clone("circ_5", "circ_4");
		this.t.drops = 2;
		this.t.size = 5;
		this.t.show_in_brush_window = true;
		this.clone("circ_6", "circ_5");
		this.t.size = 6;
		this.t.show_in_brush_window = true;
		this.clone("circ_7", "circ_5");
		this.t.size = 7;
		this.t.show_in_brush_window = true;
		this.clone("circ_8", "circ_5");
		this.t.size = 8;
		this.clone("circ_9", "circ_5");
		this.t.size = 9;
		this.clone("circ_10", "circ_5");
		this.t.size = 10;
		this.t.show_in_brush_window = true;
		this.t.ui_scale = new Vector2(0.9f, 0.9f);
		this.clone("circ_11", "circ_5");
		this.t.size = 11;
		this.clone("circ_12", "circ_5");
		this.t.size = 12;
		this.clone("circ_15", "circ_5");
		this.t.size = 15;
		this.t.show_in_brush_window = true;
		this.t.ui_scale = new Vector2(0.9f, 0.9f);
		this.clone("circ_20", "circ_5");
		this.t.size = 20;
		this.clone("circ_30", "circ_5");
		this.t.size = 30;
		this.clone("circ_70", "circ_5");
		this.t.drops = 3;
		this.t.size = 70;
		BrushData brushData2 = new BrushData();
		brushData2.id = "sqr_0";
		brushData2.size = 0;
		brushData2.group = BrushGroup.Squares;
		brushData2.continuous = true;
		brushData2.fast_spawn = true;
		brushData2.localized_key = "brush_sqr";
		brushData2.generate_action = delegate(BrushData pAsset)
		{
			pAsset.pos = new BrushPixelData[]
			{
				new BrushPixelData(0, 0, 0)
			};
		};
		this.add(brushData2);
		this.add(new BrushData
		{
			id = "sqr_1",
			size = 1,
			group = BrushGroup.Squares,
			continuous = true,
			fast_spawn = false,
			localized_key = "brush_sqr"
		});
		this.t.show_in_brush_window = true;
		this.t.generate_action = delegate(BrushData pAsset)
		{
			int tSize = pAsset.size;
			Vector2Int tPos = new Vector2Int(tSize / 2, tSize / 2);
			using (ListPool<BrushPixelData> tTempList = new ListPool<BrushPixelData>())
			{
				for (int x = -tSize; x <= tSize; x++)
				{
					for (int y = -tSize; y <= tSize; y++)
					{
						int tDist = (int)Toolbox.Dist(x, y, tPos.x, tPos.y);
						tTempList.Add(new BrushPixelData(x, y, tDist));
					}
				}
				pAsset.pos = tTempList.ToArray<BrushPixelData>();
			}
		};
		this.clone("sqr_2", "sqr_1");
		this.t.size = 2;
		this.t.continuous = false;
		this.t.show_in_brush_window = true;
		this.clone("sqr_3", "sqr_1");
		this.t.size = 3;
		this.t.continuous = false;
		this.clone("sqr_4", "sqr_1");
		this.t.size = 4;
		this.t.continuous = false;
		this.t.show_in_brush_window = true;
		this.clone("sqr_5", "sqr_1");
		this.t.size = 5;
		this.t.continuous = false;
		this.clone("sqr_10", "sqr_1");
		this.t.size = 10;
		this.t.drops = 2;
		this.t.continuous = false;
		this.t.show_in_brush_window = true;
		this.t.ui_scale = new Vector2(0.8f, 0.8f);
		this.clone("sqr_15", "sqr_10");
		this.t.size = 15;
		this.t.continuous = false;
		this.t.show_in_brush_window = true;
		this.t.ui_scale = new Vector2(0.8f, 0.8f);
		this.add(new BrushData
		{
			id = "diamond_1",
			continuous = true,
			group = BrushGroup.Diamonds,
			localized_key = "brush_diamond",
			size = 1,
			fast_spawn = false
		});
		this.t.show_in_brush_window = true;
		this.t.generate_action = delegate(BrushData pAsset)
		{
			string tFileName = "brush_" + pAsset.id;
			Texture2D tTexture = Resources.Load<Texture2D>("ui/Icons/brushes/" + tFileName);
			int width = tTexture.width;
			int height = tTexture.height;
			int num = width / 2;
			Vector2Int tPos = new Vector2Int(width / 2, height / 2);
			using (ListPool<BrushPixelData> tTempList = new ListPool<BrushPixelData>())
			{
				for (int x = 0; x < width; x++)
				{
					for (int y = 0; y < height; y++)
					{
						if (!(tTexture.GetPixel(x, y) != Color.white))
						{
							int tX = tPos.x - x;
							int tY = tPos.y - y;
							int tDist = (int)Toolbox.Dist(tX, tY, tPos.x, tPos.y);
							tTempList.Add(new BrushPixelData(tX, tY, tDist));
						}
					}
				}
				pAsset.pos = tTempList.ToArray<BrushPixelData>();
			}
		};
		this.clone("diamond_2", "diamond_1");
		this.t.size = 2;
		this.t.show_in_brush_window = true;
		this.t.ui_scale = new Vector2(0.8f, 0.8f);
		this.clone("diamond_4", "diamond_1");
		this.t.size = 4;
		this.t.continuous = false;
		this.t.show_in_brush_window = true;
		this.t.ui_scale = new Vector2(0.7f, 0.7f);
		this.clone("diamond_5", "diamond_1");
		this.t.drops = 2;
		this.t.size = 5;
		this.t.continuous = false;
		this.t.show_in_brush_window = true;
		this.t.ui_scale = new Vector2(0.7f, 0.7f);
		this.clone("diamond_7", "diamond_5");
		this.t.size = 7;
		this.t.continuous = false;
		this.t.show_in_brush_window = true;
		this.t.ui_scale = new Vector2(0.9f, 0.9f);
		this.add(new BrushData
		{
			id = "special_1",
			continuous = true,
			group = BrushGroup.Special,
			localized_key = "brush_special",
			size = 1,
			fast_spawn = false
		});
		this.t.generate_action = this.get("diamond_1").generate_action;
		this.t.show_in_brush_window = true;
		this.t.ui_size = new Vector2(14f, 14f);
		this.clone("special_2", "special_1");
		this.t.size = 2;
		this.t.show_in_brush_window = true;
		this.t.ui_size = new Vector2(17.93f, 17.93f);
		this.clone("special_3", "special_1");
		this.t.size = 3;
		this.t.continuous = false;
		this.t.show_in_brush_window = true;
		this.t.ui_size = new Vector2(21.4f, 21.4f);
		this.clone("special_4", "special_1");
		this.t.drops = 2;
		this.t.size = 4;
		this.t.continuous = false;
		this.t.show_in_brush_window = true;
		this.t.ui_size = new Vector2(23.55f, 23.55f);
		this.clone("special_5", "special_4");
		this.t.drops = 2;
		this.t.size = 5;
		this.t.continuous = false;
		this.t.show_in_brush_window = true;
		this.t.ui_size = new Vector2(28.28f, 28.28f);
	}

	// Token: 0x06000217 RID: 535 RVA: 0x000137B8 File Offset: 0x000119B8
	public override void post_init()
	{
		base.post_init();
		foreach (BrushData tAsset in this.list)
		{
			if (tAsset.show_in_brush_window)
			{
				BrushLibrary._available_brushes.Add(tAsset.id);
			}
			BrushPixelData[] pos = tAsset.pos;
			if (pos == null || pos.Length == 0)
			{
				tAsset.generate_action(tAsset);
				int tMinX = int.MaxValue;
				int tMaxX = int.MinValue;
				int tMinY = int.MaxValue;
				int tMaxY = int.MinValue;
				for (int i = 0; i < tAsset.pos.Length; i++)
				{
					BrushPixelData tPixel = tAsset.pos[i];
					if (tPixel.x < tMinX)
					{
						tMinX = tPixel.x;
					}
					if (tPixel.x > tMaxX)
					{
						tMaxX = tPixel.x;
					}
					if (tPixel.y < tMinY)
					{
						tMinY = tPixel.y;
					}
					if (tPixel.y > tMaxY)
					{
						tMaxY = tPixel.y;
					}
				}
				tAsset.width = Mathf.Abs(tMaxX - tMinX) + 1;
				tAsset.height = Mathf.Abs(tMaxY - tMinY) + 1;
				tAsset.sqr_size = tAsset.width * tAsset.height;
			}
		}
	}

	// Token: 0x06000218 RID: 536 RVA: 0x00013918 File Offset: 0x00011B18
	public override void linkAssets()
	{
		base.linkAssets();
		foreach (BrushData pAsset in this.list)
		{
			BrushLibrary.shuffleBrush(pAsset);
		}
	}

	// Token: 0x06000219 RID: 537 RVA: 0x00013970 File Offset: 0x00011B70
	public static void shuffleBrush(BrushData pAsset)
	{
		pAsset.pos.Shuffle<BrushPixelData>();
		int tIndexOfCenter = 0;
		for (int i = 0; i < pAsset.pos.Length; i++)
		{
			BrushPixelData tPixel = pAsset.pos[i];
			if (tPixel.x == 0 && tPixel.y == 0)
			{
				tIndexOfCenter = i;
				break;
			}
		}
		BrushPixelData tFirstElement = pAsset.pos[0];
		BrushPixelData tCenterElement = pAsset.pos[tIndexOfCenter];
		pAsset.pos[0] = tCenterElement;
		pAsset.pos[tIndexOfCenter] = tFirstElement;
	}

	// Token: 0x0600021A RID: 538 RVA: 0x000139F8 File Offset: 0x00011BF8
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (BrushData tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
	}

	// Token: 0x0600021B RID: 539 RVA: 0x00013A58 File Offset: 0x00011C58
	public override BrushData clone(string pNew, string pFrom)
	{
		BrushData brushData = base.clone(pNew, pFrom);
		brushData.show_in_brush_window = false;
		return brushData;
	}

	// Token: 0x0600021C RID: 540 RVA: 0x00013A69 File Offset: 0x00011C69
	internal static void nextBrush()
	{
		Config.current_brush = BrushLibrary.getPrevious(Config.current_brush);
	}

	// Token: 0x0600021D RID: 541 RVA: 0x00013A7A File Offset: 0x00011C7A
	internal static void previousBrush()
	{
		Config.current_brush = BrushLibrary.getNext(Config.current_brush);
	}

	// Token: 0x0600021E RID: 542 RVA: 0x00013A8C File Offset: 0x00011C8C
	private static string getNext(string pBrushName)
	{
		bool tNext = false;
		for (int i = 0; i < BrushLibrary._available_brushes.Count; i++)
		{
			string tKey = BrushLibrary._available_brushes[i];
			if (tKey == pBrushName)
			{
				tNext = true;
			}
			else if (tNext)
			{
				return tKey;
			}
		}
		return pBrushName;
	}

	// Token: 0x0600021F RID: 543 RVA: 0x00013AD0 File Offset: 0x00011CD0
	private static string getPrevious(string pBrushName)
	{
		bool tNext = false;
		for (int i = BrushLibrary._available_brushes.Count - 1; i >= 0; i--)
		{
			string tKey = BrushLibrary._available_brushes[i];
			if (tKey == pBrushName)
			{
				tNext = true;
			}
			else if (tNext)
			{
				return tKey;
			}
		}
		return pBrushName;
	}

	// Token: 0x040001BE RID: 446
	[NonSerialized]
	private static readonly List<string> _available_brushes = new List<string>();
}
