using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000844 RID: 2116
public static class WindowPreloader
{
	// Token: 0x06004242 RID: 16962 RVA: 0x001C0ADC File Offset: 0x001BECDC
	public static void addWindowPreloadResources()
	{
		if (WindowPreloader._windows_preloaded)
		{
			return;
		}
		if (!Config.preload_windows)
		{
			return;
		}
		foreach (WindowAsset tAsset in AssetManager.window_library.list)
		{
			if (tAsset.preload)
			{
				WindowPreloader._windows_preload_list.Add(tAsset.id);
			}
		}
		SmoothLoader.add(delegate
		{
			foreach (string pWindowID in WindowPreloader._windows_preload_list)
			{
				WindowPreloader.preloadWindowPrefab(pWindowID);
			}
		}, WindowPreloader.c("Preloading windows", 1, 5), true, 0.001f, false);
	}

	// Token: 0x06004243 RID: 16963 RVA: 0x001C0B8C File Offset: 0x001BED8C
	public static void addWaitForWindowResources()
	{
		if (WindowPreloader._windows_preloaded)
		{
			return;
		}
		SmoothLoader.add(new MapLoaderAction(WindowPreloader.finishPreloadingWindowsResources), WindowPreloader.c("Preloading windows", 2, 5), true, 0.001f, false);
	}

	// Token: 0x06004244 RID: 16964 RVA: 0x001C0BBC File Offset: 0x001BEDBC
	private static void finishPreloadingWindowsResources()
	{
		using (List<ResourceRequest>.Enumerator enumerator = WindowPreloader._windows_resources_requests.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (!enumerator.Current.isDone)
				{
					WindowPreloader.addWaitForWindowResources();
					return;
				}
			}
		}
		WindowPreloader.addInstantiateWindows();
	}

	// Token: 0x06004245 RID: 16965 RVA: 0x001C0C1C File Offset: 0x001BEE1C
	private static void addInstantiateWindows()
	{
		SmoothLoader.add(delegate
		{
			foreach (string pWindowID in WindowPreloader._windows_preload_list)
			{
				WindowPreloader.prepareWindowPrefab(pWindowID);
			}
		}, WindowPreloader.c("Preloading windows", 3, 5), true, 0.001f, false);
		int i = 0;
		int tTotal = WindowPreloader._windows_preload_list.Count;
		using (List<string>.Enumerator enumerator = WindowPreloader._windows_preload_list.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				string tWindowID = enumerator.Current;
				SmoothLoader.add(delegate
				{
					WindowPreloader.instantiateWindow(tWindowID);
				}, WindowPreloader.c(WindowPreloader.c("Preloading windows", 4, 5), ++i, tTotal), false, 0.001f, false);
			}
		}
		SmoothLoader.add(new MapLoaderAction(WindowPreloader.finishPreloadingWindows), WindowPreloader.c("Preloading windows", 5, 5), true, 0.001f, true);
	}

	// Token: 0x06004246 RID: 16966 RVA: 0x001C0D08 File Offset: 0x001BEF08
	private static void finishPreloadingWindows()
	{
		foreach (AsyncInstantiateOperation<ScrollWindow> tRequest in WindowPreloader._windows_preloading_operations.Values)
		{
			if (!tRequest.isDone)
			{
				tRequest.WaitForCompletion();
			}
		}
		foreach (string tWindowID in WindowPreloader._windows_preloading_operations.Keys)
		{
			ScrollWindow tWindow = WindowPreloader._windows_preloading_operations[tWindowID].Result.First<ScrollWindow>();
			WindowPreloader.finishPreloadingWindow(tWindowID, tWindow);
			WindowPreloader.restoreWindowPrefab(tWindowID);
		}
		WindowPreloader.finalizeWindowsPreloading();
	}

	// Token: 0x06004247 RID: 16967 RVA: 0x001C0DD0 File Offset: 0x001BEFD0
	public static bool TryGetPreloadedWindow(string pWindowID, out ScrollWindow tScrollWindow)
	{
		if (WindowPreloader._preloaded_windows.TryGetValue(pWindowID, out tScrollWindow))
		{
			WindowPreloader._preloaded_windows.Remove(pWindowID);
			return true;
		}
		return false;
	}

	// Token: 0x06004248 RID: 16968 RVA: 0x001C0DF0 File Offset: 0x001BEFF0
	private static void preloadWindowPrefab(string pWindowID)
	{
		string tCheckWindowID = ScrollWindow.checkWindowID(pWindowID);
		if (!WindowPreloader._preloaded_windows.ContainsKey(pWindowID))
		{
			ResourceRequest tRequest = Resources.LoadAsync("windows/" + tCheckWindowID, typeof(ScrollWindow));
			WindowPreloader._windows_resources_requests.Add(tRequest);
		}
	}

	// Token: 0x06004249 RID: 16969 RVA: 0x001C0E38 File Offset: 0x001BF038
	private static ScrollWindow getWindowPrefab(string pWindowID)
	{
		string tCheckWindowID = ScrollWindow.checkWindowID(pWindowID);
		ScrollWindow tWindowPrefab = (ScrollWindow)Resources.Load("windows/" + tCheckWindowID, typeof(ScrollWindow));
		if (tWindowPrefab == null)
		{
			Debug.LogError("Window with id " + tCheckWindowID + " not found!");
			tWindowPrefab = (ScrollWindow)Resources.Load("windows/not_found", typeof(ScrollWindow));
		}
		return tWindowPrefab;
	}

	// Token: 0x0600424A RID: 16970 RVA: 0x001C0EA8 File Offset: 0x001BF0A8
	private static void prepareWindowPrefab(string pWindowID)
	{
		ScrollWindow tWindowPrefab = WindowPreloader.getWindowPrefab(pWindowID);
		tWindowPrefab.gameObject.SetActive(false);
		WindowPreloader._windows_tabs_objects[pWindowID] = ScrollWindow.disableTabsInPrefab(tWindowPrefab);
	}

	// Token: 0x0600424B RID: 16971 RVA: 0x001C0EDC File Offset: 0x001BF0DC
	private static void instantiateWindow(string pWindowID)
	{
		AsyncInstantiateOperation<ScrollWindow> tOperation = Object.InstantiateAsync<ScrollWindow>(WindowPreloader.getWindowPrefab(pWindowID), CanvasMain.instance.transformWindows);
		WindowPreloader._windows_preloading_operations.Add(pWindowID, tOperation);
	}

	// Token: 0x0600424C RID: 16972 RVA: 0x001C0F0B File Offset: 0x001BF10B
	private static void restoreWindowPrefab(string pWindowID)
	{
		Component windowPrefab = WindowPreloader.getWindowPrefab(pWindowID);
		ScrollWindow.enableTabsInPrefab(WindowPreloader._windows_tabs_objects[pWindowID]);
		windowPrefab.gameObject.SetActive(true);
	}

	// Token: 0x0600424D RID: 16973 RVA: 0x001C0F2E File Offset: 0x001BF12E
	private static void finishPreloadingWindow(string pWindowID, ScrollWindow pWindow)
	{
		pWindow.gameObject.name = pWindowID;
		pWindow.init();
		WindowPreloader._preloaded_windows.Add(pWindowID, pWindow);
	}

	// Token: 0x0600424E RID: 16974 RVA: 0x001C0F4E File Offset: 0x001BF14E
	private static void finalizeWindowsPreloading()
	{
		WindowPreloader._windows_preloaded = true;
		WindowPreloader._windows_tabs_objects.Clear();
		WindowPreloader._windows_preloading_operations.Clear();
		WindowPreloader._windows_resources_requests.Clear();
		WindowPreloader._windows_preload_list.Clear();
	}

	// Token: 0x0600424F RID: 16975 RVA: 0x001C0F7E File Offset: 0x001BF17E
	private static string c(string pString, int pStep, int pMax)
	{
		return string.Format("{0} ({1}/{2})", pString, pStep, pMax);
	}

	// Token: 0x04003059 RID: 12377
	private static bool _windows_preloaded;

	// Token: 0x0400305A RID: 12378
	private static readonly Dictionary<string, ScrollWindow> _preloaded_windows = new Dictionary<string, ScrollWindow>();

	// Token: 0x0400305B RID: 12379
	private static readonly List<ResourceRequest> _windows_resources_requests = new List<ResourceRequest>();

	// Token: 0x0400305C RID: 12380
	private static readonly Dictionary<string, AsyncInstantiateOperation<ScrollWindow>> _windows_preloading_operations = new Dictionary<string, AsyncInstantiateOperation<ScrollWindow>>();

	// Token: 0x0400305D RID: 12381
	private static readonly Dictionary<string, ListPool<GameObject>> _windows_tabs_objects = new Dictionary<string, ListPool<GameObject>>();

	// Token: 0x0400305E RID: 12382
	private static readonly List<string> _windows_preload_list = new List<string>();
}
