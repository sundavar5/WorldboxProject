using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000041 RID: 65
public static class FPS
{
	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060002D7 RID: 727 RVA: 0x0001C5D4 File Offset: 0x0001A7D4
	public static int fps
	{
		get
		{
			return FPS._fps;
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x0001C5DC File Offset: 0x0001A7DC
	public static string getFPS()
	{
		if (FPS._fps >= 60)
		{
			return "<color=#75D53A>" + FPS._fps.ToString() + "</color>";
		}
		if (FPS._fps >= 30)
		{
			return "<color=#F4E700>" + FPS._fps.ToString() + "</color>";
		}
		return "<color=#DB2920>" + FPS._fps.ToString() + "</color>";
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0001C648 File Offset: 0x0001A848
	public static void update()
	{
		FPS._delta_time += Time.unscaledDeltaTime;
		FPS._frame_count += 1f;
		if (FPS._delta_time > 1f / FPS._update_rate)
		{
			FPS._fps = (int)(FPS._frame_count / FPS._delta_time);
			FPS._delta_time = 0f;
			FPS._frame_count = 0f;
			return;
		}
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0001C6B0 File Offset: 0x0001A8B0
	public static void debug_update()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (SmoothLoader.isLoading())
		{
			return;
		}
		if (FPS._last_fps.Count >= 5)
		{
			return;
		}
		if (FPS._fps >= 25)
		{
			FPS._last_fps.Clear();
		}
		FPS._last_fps.Add(FPS._fps);
		if (Config.editor_mastef && FPS._last_fps.Count >= 5)
		{
			DebugConfig.createTool(AssetManager.debug_tool_library.get("Benchmark All").id, 30, -10, -1);
			DebugConfig.createTool(AssetManager.debug_tool_library.get("Benchmark Actors").id, 200, -10, -1);
		}
	}

	// Token: 0x04000261 RID: 609
	private static float _delta_time;

	// Token: 0x04000262 RID: 610
	private static float _frame_count;

	// Token: 0x04000263 RID: 611
	private static float _update_rate = 3f;

	// Token: 0x04000264 RID: 612
	private static int _fps;

	// Token: 0x04000265 RID: 613
	private const int DEBUG_FPS_THRESHOLD = 25;

	// Token: 0x04000266 RID: 614
	private static List<int> _last_fps = new List<int>(5);
}
