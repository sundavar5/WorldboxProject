using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000300 RID: 768
public static class DebugHighlight
{
	// Token: 0x06001D1E RID: 7454 RVA: 0x00105028 File Offset: 0x00103228
	public static void updateDebugHighlights()
	{
		if (DebugHighlight.hashset.Count == 0)
		{
			return;
		}
		DebugHighlight.to_remove.Clear();
		foreach (DebugHighlightContainer tCont in DebugHighlight.hashset)
		{
			tCont.timer -= World.world.delta_time;
			if (tCont.timer < 0f)
			{
				DebugHighlight.to_remove.Add(tCont);
			}
		}
		foreach (DebugHighlightContainer tCont2 in DebugHighlight.to_remove)
		{
			DebugHighlight.hashset.Remove(tCont2);
		}
	}

	// Token: 0x06001D1F RID: 7455 RVA: 0x00105100 File Offset: 0x00103300
	public static void newHighlightList(Color pColor, List<TileZone> pZones, float pTime = 3f)
	{
		foreach (TileZone iZone in pZones)
		{
			DebugHighlight.newHighlight(pColor, iZone, pTime);
		}
	}

	// Token: 0x06001D20 RID: 7456 RVA: 0x00105150 File Offset: 0x00103350
	public static void newHighlightList(Color pColor, List<MapChunk> pChunks, float pTime = 3f)
	{
		foreach (MapChunk tChunk in pChunks)
		{
			DebugHighlight.newHighlight(pColor, tChunk, pTime);
		}
	}

	// Token: 0x06001D21 RID: 7457 RVA: 0x001051A0 File Offset: 0x001033A0
	public static void clear()
	{
		DebugHighlight.hashset.Clear();
	}

	// Token: 0x06001D22 RID: 7458 RVA: 0x001051AC File Offset: 0x001033AC
	public static void newHighlight(Color pColor, MapChunk pChunk, float pTime = 3f)
	{
		DebugHighlightContainer tCont = new DebugHighlightContainer();
		tCont.chunk = pChunk;
		tCont.color = pColor;
		tCont.setTimer(pTime);
		DebugHighlight.hashset.Add(tCont);
	}

	// Token: 0x06001D23 RID: 7459 RVA: 0x001051E0 File Offset: 0x001033E0
	public static void newHighlight(Color pColor, TileZone pZone, float pTime = 3f)
	{
		DebugHighlightContainer tCont = new DebugHighlightContainer();
		tCont.zone = pZone;
		tCont.color = pColor;
		tCont.setTimer(pTime);
		DebugHighlight.hashset.Add(tCont);
	}

	// Token: 0x040015FB RID: 5627
	public static HashSet<DebugHighlightContainer> hashset = new HashSet<DebugHighlightContainer>();

	// Token: 0x040015FC RID: 5628
	private static List<DebugHighlightContainer> to_remove = new List<DebugHighlightContainer>();
}
