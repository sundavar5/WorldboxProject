using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000078 RID: 120
public static class SmoothLoader
{
	// Token: 0x0600044B RID: 1099 RVA: 0x0002D760 File Offset: 0x0002B960
	public static void prepare()
	{
		SmoothLoader._actions.Clear();
		SmoothLoader._index = 0;
		SmoothLoader._started = false;
		SmoothLoader.finish();
		SmoothLoader.latest_called_id = string.Empty;
		SmoothLoader.latest_time = string.Empty;
		PlayerConfig.checkSettings();
		PreloadHelpers.init();
		SmoothLoader.add(delegate
		{
			ScrollWindow.hideAllEvent(false);
			CanvasMain.instance.setMainUiEnabled(false);
		}, "Close windows", true, 0.01f, false);
		SmoothLoader.add(delegate
		{
			QuantumSpriteManager.hideAll();
		}, "Quantum Clean", true, 0.01f, false);
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x0002D808 File Offset: 0x0002BA08
	public static void add(MapLoaderAction pAction, string pId, bool pSkipFrame = false, float pNewWaitTimerValue = 0.001f, bool pToEnd = false)
	{
		MapLoaderContainer tNewActionContainer = new MapLoaderContainer(pAction, pId, true, pNewWaitTimerValue);
		SmoothLoader._has_actions = true;
		if (SmoothLoader._started && !pToEnd)
		{
			if (pSkipFrame)
			{
				SmoothLoader._actions.Insert(SmoothLoader._index + 1 + SmoothLoader._added++, new MapLoaderContainer(new MapLoaderAction(SmoothLoader.skipFrame), "skipFrame", false, 0.001f));
			}
			SmoothLoader._actions.Insert(SmoothLoader._index + 1 + SmoothLoader._added++, tNewActionContainer);
			return;
		}
		if (pSkipFrame)
		{
			SmoothLoader._actions.Add(new MapLoaderContainer(new MapLoaderAction(SmoothLoader.skipFrame), "skipFrame", false, 0.001f));
		}
		SmoothLoader._actions.Add(tNewActionContainer);
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x0002D8C4 File Offset: 0x0002BAC4
	public static void setWaitTimer()
	{
		float tNewTimer = 0.001f;
		if (SmoothLoader._last_action != null)
		{
			tNewTimer = SmoothLoader._last_action.new_timer_value;
		}
		if (tNewTimer > SmoothLoader._current_timer)
		{
			SmoothLoader._current_timer = tNewTimer;
		}
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x0002D8F7 File Offset: 0x0002BAF7
	public static void skipFrame()
	{
		SmoothLoader._skip_frame = true;
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x0002D8FF File Offset: 0x0002BAFF
	public static bool isLoading()
	{
		return SmoothLoader._has_actions;
	}

	// Token: 0x06000450 RID: 1104 RVA: 0x0002D906 File Offset: 0x0002BB06
	private static void finish()
	{
		SmoothLoader._last_action = null;
		SmoothLoader._has_actions = false;
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x0002D914 File Offset: 0x0002BB14
	public static void update(float pElapsed)
	{
		if (!SmoothLoader._has_actions)
		{
			return;
		}
		if (SmoothLoader._current_timer > 0f)
		{
			SmoothLoader._current_timer -= pElapsed;
			return;
		}
		if (SmoothLoader._actions.Count == 0)
		{
			SmoothLoader.finish();
			return;
		}
		if (SmoothLoader._last_i == SmoothLoader._index)
		{
			SmoothLoader.openConsole();
			SmoothLoader.finish();
			MapBox.instance.startTheGame(true);
			return;
		}
		SmoothLoader._last_i = SmoothLoader._index;
		SmoothLoader._skip_frame = false;
		float tStartTime = Time.realtimeSinceStartup;
		SmoothLoader.doActions();
		while (Time.realtimeSinceStartup - tStartTime < 0.1f && !SmoothLoader._skip_frame && SmoothLoader._actions.Count != 0)
		{
			SmoothLoader.doActions();
		}
		SmoothLoader.setWaitTimer();
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x0002D9BE File Offset: 0x0002BBBE
	private static void openConsole()
	{
		if (World.world != null && World.world.console != null && !SmoothLoader._toggled_console)
		{
			SmoothLoader._toggled_console = true;
			World.world.console.Show();
		}
	}

	// Token: 0x06000453 RID: 1107 RVA: 0x0002D9FC File Offset: 0x0002BBFC
	private static void doActions()
	{
		if (!SmoothLoader._started)
		{
			Bench.bench("SmoothLoader", "loading", true);
		}
		SmoothLoader._started = true;
		SmoothLoader._added = 0;
		MapLoaderContainer mapLoaderContainer = SmoothLoader._actions[SmoothLoader._index];
		MapLoaderAction tAction = mapLoaderContainer.action;
		SmoothLoader._last_action = mapLoaderContainer;
		Bench.bench(mapLoaderContainer.id, "loading", true);
		tAction();
		Bench.benchEnd(mapLoaderContainer.id, "loading", false, 0L, true);
		SmoothLoader.setWaitTimer();
		SmoothLoader.checkDebugText();
		SmoothLoader._index++;
		if (SmoothLoader._index > SmoothLoader._actions.Count - 1)
		{
			SmoothLoader._actions.Clear();
			double tTotalBenchResult = Bench.benchEnd("SmoothLoader", "loading", false, 0L, true);
			if (SmoothLoader.logsEnabled)
			{
				int num = ++SmoothLoader._index;
				Debug.Log(SmoothLoader.fillRight(SmoothLoader.fillLeft(num.ToString(), 3, '0') + ": Loading finished", 40, ' ') + " = " + tTotalBenchResult.ToString("F4"));
			}
		}
	}

	// Token: 0x06000454 RID: 1108 RVA: 0x0002DB10 File Offset: 0x0002BD10
	private static void checkDebugText()
	{
		if (SmoothLoader._last_action == null)
		{
			return;
		}
		if (!SmoothLoader._last_action.debug_log)
		{
			return;
		}
		SmoothLoader.latest_time = Bench.getBenchResultAsDouble(SmoothLoader._last_action.id, "loading", false).ToString("F4");
		SmoothLoader.latest_called_id = SmoothLoader.fillLeft(SmoothLoader._index.ToString(), 3, '0') + ": " + SmoothLoader._last_action.id;
		if (SmoothLoader.logsEnabled)
		{
			string debugText = SmoothLoader.fillRight(SmoothLoader.latest_called_id, 40, ' ') + " = " + SmoothLoader.latest_time;
			double latest_result = Bench.getBenchResultAsDouble(SmoothLoader._last_action.id, "loading", false);
			if (latest_result > 1.0)
			{
				debugText = "<color=red>" + debugText + "</color>";
			}
			else if (latest_result > 0.5)
			{
				debugText = "<color=yellow>" + debugText + "</color>";
			}
			Debug.Log(debugText);
		}
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x06000455 RID: 1109 RVA: 0x0002DC01 File Offset: 0x0002BE01
	private static bool logsEnabled
	{
		get
		{
			return !Config.disable_loading_logs;
		}
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x0002DC0B File Offset: 0x0002BE0B
	private static string fillLeft(string pString, int pSize = 1, char pFill = ' ')
	{
		return Toolbox.fillLeft(pString, pSize, pFill);
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x0002DC15 File Offset: 0x0002BE15
	private static string fillRight(string pString, int pSize = 1, char pFill = ' ')
	{
		return Toolbox.fillRight(pString, pSize, pFill);
	}

	// Token: 0x04000388 RID: 904
	public const float DEFAULT_TIMER_VALUE = 0.001f;

	// Token: 0x04000389 RID: 905
	public const int MAX_LOG_LENGTH = 40;

	// Token: 0x0400038A RID: 906
	private static int _index = 0;

	// Token: 0x0400038B RID: 907
	private static float _current_timer = 0f;

	// Token: 0x0400038C RID: 908
	private static bool _skip_frame = false;

	// Token: 0x0400038D RID: 909
	private static int _added = 0;

	// Token: 0x0400038E RID: 910
	private static bool _started = false;

	// Token: 0x0400038F RID: 911
	private static bool _has_actions = false;

	// Token: 0x04000390 RID: 912
	private static List<MapLoaderContainer> _actions = new List<MapLoaderContainer>();

	// Token: 0x04000391 RID: 913
	public static string latest_called_id = string.Empty;

	// Token: 0x04000392 RID: 914
	public static string latest_time = string.Empty;

	// Token: 0x04000393 RID: 915
	private static MapLoaderContainer _last_action;

	// Token: 0x04000394 RID: 916
	private static bool _toggled_console = false;

	// Token: 0x04000395 RID: 917
	private static int _last_i = -1;
}
