using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020003FE RID: 1022
public class DebugConfig : MonoBehaviour
{
	// Token: 0x0600234E RID: 9038 RVA: 0x001265CC File Offset: 0x001247CC
	private void Start()
	{
		DebugConfig.instance = this;
		if (DebugConfig.isOn(DebugOption.DisablePremium) || DebugConfig.isOn(DebugOption.TestAds))
		{
			this.debugButton.SetActive(true);
			if (DebugConfig.isOn(DebugOption.DisablePremium))
			{
				Debug.LogError("[DEBUG] Premium is disabled via debug menu!");
				Debug.Log("This option is only for beta testers. If you purchased premium, you won't have it until you disable it.");
				Debug.Log("Disable this option in the debug menu and restart the game.");
			}
			if (DebugConfig.isOn(DebugOption.TestAds))
			{
				Debug.LogError("[DEBUG] Test Ads are enabled via debug menu!");
				Debug.Log("This option is only for beta testers. You most likely don't want to play with it.");
				Debug.Log("Disable this option in the debug menu and restart the game.");
			}
		}
	}

	// Token: 0x0600234F RID: 9039 RVA: 0x00126648 File Offset: 0x00124848
	private static void enableDefaultOptions()
	{
		DebugConfig.setOption(DebugOption.GenerateNewMapOnMapLoadingError, true, true);
		DebugConfig.setOption(DebugOption.LavaGlow, true, true);
		DebugConfig.setOption(DebugOption.UseCameraAspect, true, true);
		DebugConfig.setOption(DebugOption.UseGlobalPathLock, true, true);
		DebugConfig.setOption(DebugOption.SystemBuildTick, true, true);
		DebugConfig.setOption(DebugOption.SystemCityPlaceFinder, true, true);
		DebugConfig.setOption(DebugOption.SystemProduceNewCitizens, true, true);
		DebugConfig.setOption(DebugOption.SystemUnitPathfinding, true, true);
		DebugConfig.setOption(DebugOption.SystemWorldBehaviours, true, true);
		DebugConfig.setOption(DebugOption.SystemZoneGrowth, true, true);
		DebugConfig.setOption(DebugOption.SystemCheckUnitAction, true, true);
		DebugConfig.setOption(DebugOption.SystemUpdateUnitAnimation, true, true);
		DebugConfig.setOption(DebugOption.SystemUpdateBuildings, true, true);
		DebugConfig.setOption(DebugOption.SystemUpdateUnits, true, true);
		DebugConfig.setOption(DebugOption.SystemUpdateCities, true, true);
		DebugConfig.setOption(DebugOption.SystemRedrawMap, true, true);
		DebugConfig.setOption(DebugOption.SystemUpdateDirtyChunks, true, true);
		DebugConfig.setOption(DebugOption.SystemCheckGoodForBoat, false, true);
		DebugConfig.setOption(DebugOption.SystemCityTasks, true, true);
		DebugConfig.setOption(DebugOption.InspectObjectsOnClick, true, true);
		DebugConfig.setOption(DebugOption.SystemMusic, false, true);
		DebugConfig.setOption(DebugOption.Greg, false, true);
		DebugConfig.setOption(DebugOption.MakeUnitsFollowCursor, false, true);
		DebugConfig.setOption(DebugOption.SystemSplitAstar, true, true);
		DebugConfig.setOption(DebugOption.UseCacheForRegionPath, true, true);
		DebugConfig.setOption(DebugOption.ParallelJobsUpdater, true, true);
		DebugConfig.setOption(DebugOption.ParallelChunks, true, true);
		DebugConfig.setOption(DebugOption.ScaleEffectEnabled, true, true);
		if (Config.editor_devs && !Config.editor_nikon)
		{
			DebugConfig.setOption(DebugOption.ExportAssetLibraries, true, true);
			DebugConfig.setOption(DebugOption.GenerateGameplayReport, true, true);
			DebugConfig.setOption(DebugOption.TesterLibs, true, true);
		}
		if (Config.isEditor)
		{
			DebugConfig.setOption(DebugOption.DebugButton, true, true);
		}
	}

	// Token: 0x06002350 RID: 9040 RVA: 0x001267B4 File Offset: 0x001249B4
	public static void initDict()
	{
		DebugConfig._dictionary = new Dictionary<DebugOption, bool>();
		foreach (object obj in Enum.GetValues(typeof(DebugOption)))
		{
			DebugConfig.setOption((DebugOption)obj, false, true);
		}
	}

	// Token: 0x06002351 RID: 9041 RVA: 0x00126820 File Offset: 0x00124A20
	public static void init()
	{
		if (DebugConfig._dictionary != null)
		{
			return;
		}
		DebugConfig.initDict();
		DebugConfig.enableDefaultOptions();
		if (PlayerConfig.instance != null)
		{
			if (PlayerConfig.instance.data.premiumDisabled)
			{
				DebugConfig.setOption(DebugOption.DisablePremium, true, true);
			}
			if (PlayerConfig.instance.data.testAds)
			{
				DebugConfig.setOption(DebugOption.TestAds, true, true);
			}
		}
		if (Config.fmod_test_build)
		{
			Config.disable_discord = true;
			Config.disable_steam = true;
		}
		if (Config.isEditor)
		{
			Config.show_console_on_error = false;
			Config.customMapSizeDefault = "standard";
			DebugConfig.setOption(DebugOption.Graphy, true, true);
		}
		if (Config.editor_maxim)
		{
			DebugConfig.editorMaximOptions();
		}
		if (Config.editor_mastef)
		{
			DebugConfig.editorMastefOptions();
		}
		if (Config.editor_nikon)
		{
			DebugConfig.editorNikonOptions();
		}
	}

	// Token: 0x06002352 RID: 9042 RVA: 0x001268D0 File Offset: 0x00124AD0
	private static void editorNikonOptions()
	{
		Config.show_console_on_error = false;
		Config.disable_discord = true;
		Config.disable_steam = true;
		Config.load_save_on_start = false;
		Config.load_save_on_start_slot = 8;
		Config.disable_startup_window = false;
		Config.disable_tutorial = true;
		MusicBox.debug_sounds = false;
		DebugConfig.setOption(DebugOption.DebugButton, true, true);
		DebugConfig.setOption(DebugOption.Graphy, false, true);
		DebugConfig.setOption(DebugOption.PauseOnStart, true, true);
		DebugConfig.setOption(DebugOption.ShowHiddenStats, true, true);
		DebugConfig.setOption(DebugOption.DebugWindowHotkeys, true, true);
		DebugConfig.setOption(DebugOption.DebugUnitHotkeys, true, true);
	}

	// Token: 0x06002353 RID: 9043 RVA: 0x00126950 File Offset: 0x00124B50
	internal static void debugToolMastefDefaults(DebugTool pTool)
	{
		pTool.sort_order_reversed = false;
		pTool.sort_by_values = true;
		pTool.show_averages = false;
		pTool.hide_zeroes = false;
		pTool.show_counter = true;
		pTool.show_max = false;
		pTool.paused = false;
		pTool.hide_zeroes = true;
		pTool.state = DebugToolState.Values;
	}

	// Token: 0x06002354 RID: 9044 RVA: 0x0012699C File Offset: 0x00124B9C
	private static void editorMastefOptions()
	{
		Plot.DEBUG_PLOTS = false;
		Config.show_console_on_error = false;
		Config.disable_loading_logs = true;
		Config.disable_discord = true;
		Config.disable_steam = true;
		MusicBox.debug_sounds = false;
		Config.disable_startup_window = true;
		Config.disable_dispose_logs = true;
		DebugConfig.setOption(DebugOption.OverlaySoundsActive, false, true);
		DebugConfig.setOption(DebugOption.ShowLayoutGroupGrid, false, true);
		DebugConfig.setOption(DebugOption.BenchAiEnabled, false, true);
		DebugConfig.setOption(DebugOption.DebugTooltipUI, false, true);
		DebugConfig.setOption(DebugOption.ParallelJobsUpdater, false, true);
		DebugConfig.setOption(DebugOption.ParallelChunks, false, true);
		DebugConfig.setOption(DebugOption.SonicSpeed, false, true);
		DebugConfig.setOption(DebugOption.Graphy, false, true);
		MapBox.on_world_loaded = (Action)Delegate.Combine(MapBox.on_world_loaded, new Action(delegate()
		{
		}));
	}

	// Token: 0x06002355 RID: 9045 RVA: 0x00126A68 File Offset: 0x00124C68
	private static void debugBoats()
	{
		DebugConfig.setOption(DebugOption.OverlayBoatTransport, true, true);
		DebugConfig.setOption(DebugOption.BoatPassengerLines, true, true);
		DebugConfig.setOption(DebugOption.ActorGizmosBoatTaxiRequestTargets, true, true);
		DebugConfig.setOption(DebugOption.ActorGizmosBoatTaxiTarget, true, true);
		DebugConfig.setOption(DebugOption.RenderIslandsTileCorners, true, true);
		DebugConfig.setOption(DebugOption.RegionNeighbours, true, true);
		DebugConfig.setOption(DebugOption.Region, true, true);
		DebugConfig.addDebugTool("boat");
		DebugConfig.addDebugTool("taxi");
		DebugConfig.addDebugTool("tile_info");
	}

	// Token: 0x06002356 RID: 9046 RVA: 0x00126AD6 File Offset: 0x00124CD6
	private static void debugReproduction()
	{
		DebugConfig.addDebugTool("reproduction_diagnostic_cursor");
		DebugConfig.addDebugTool("reproduction_diagnostic_total");
		DebugConfig.addDebugTool("city_jobs");
		DebugConfig.addDebugTool("city_storage");
	}

	// Token: 0x06002357 RID: 9047 RVA: 0x00126B00 File Offset: 0x00124D00
	private static void debugMapChunks()
	{
		DebugConfig.addDebugTool("map_chunks");
		DebugConfig.addDebugTool("benchmark_chunks");
	}

	// Token: 0x06002358 RID: 9048 RVA: 0x00126B18 File Offset: 0x00124D18
	private static void editorMaximOptions()
	{
		DebugConfig.setOption(DebugOption.GenerateNewMapOnMapLoadingError, false, true);
		Config.disable_discord = true;
		Config.disable_steam = true;
		Config.disable_startup_window = true;
		Config.show_console_on_error = false;
		Config.disable_tutorial = true;
		DebugConfig.setOption(DebugOption.ControlledUnitsAttackRaycast, true, true);
		DebugConfig.setOption(DebugOption.DebugWindowHotkeys, true, true);
		DebugConfig.setOption(DebugOption.CivDrawCityClaimZone, true, true);
		DebugConfig.setOption(DebugOption.PauseOnStart, true, true);
		DebugConfig.setOption(DebugOption.ShowHiddenStats, true, true);
		DebugConfig.setOption(DebugOption.DebugUnitHotkeys, true, true);
		DebugConfig.setOption(DebugOption.ShowWarriorsCityText, true, true);
		DebugConfig.setOption(DebugOption.ShowCityWeaponsText, true, true);
		DebugConfig.setOption(DebugOption.ShowFoodCityText, true, true);
		DebugConfig.setOption(DebugOption.CursorUnitAttackRange, true, true);
		DebugConfig.setOption(DebugOption.ShowAmountNearArmy, true, true);
		for (int i = 0; i < 100; i++)
		{
			Debug.Log("remember the cant");
		}
	}

	// Token: 0x06002359 RID: 9049 RVA: 0x00126BD5 File Offset: 0x00124DD5
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isOn(DebugOption pOption)
	{
		return DebugConfig._dictionary[pOption];
	}

	// Token: 0x0600235A RID: 9050 RVA: 0x00126BE4 File Offset: 0x00124DE4
	public static void switchOption(DebugOption pOption)
	{
		DebugConfig.setOption(pOption, !DebugConfig.isOn(pOption), true);
		if (pOption == DebugOption.DisablePremium && PlayerConfig.instance != null)
		{
			PlayerConfig.instance.data.premiumDisabled = DebugConfig.isOn(pOption);
			PlayerConfig.instance.data.clearDebugOnStart = false;
			PlayerConfig.saveData();
			PremiumElementsChecker.checkElements();
			DebugConfig.disablePremiumNotify();
		}
		if (pOption == DebugOption.TestAds && PlayerConfig.instance != null)
		{
			PlayerConfig.instance.data.testAds = DebugConfig.isOn(pOption);
			PlayerConfig.instance.data.clearDebugOnStart = false;
			PlayerConfig.saveData();
			PremiumElementsChecker.checkElements();
			DebugConfig.testAdsNotify();
		}
	}

	// Token: 0x0600235B RID: 9051 RVA: 0x00126C80 File Offset: 0x00124E80
	public static void disablePremiumNotify()
	{
		if (PlayerConfig.instance == null)
		{
			return;
		}
		if (DebugConfig.isOn(DebugOption.DisablePremium))
		{
			WorldTip.showNow("Premium is blocked! Even after restart!", false, "top", 10f, "#F3961F");
			return;
		}
		WorldTip.showNow("Premium is unblocked", false, "top", 3f, "#F3961F");
	}

	// Token: 0x0600235C RID: 9052 RVA: 0x00126CD4 File Offset: 0x00124ED4
	public static void testAdsNotify()
	{
		if (PlayerConfig.instance == null)
		{
			return;
		}
		if (DebugConfig.isOn(DebugOption.TestAds))
		{
			WorldTip.showNow("Test Ads are enabled! Even after restart!", false, "top", 10f, "#F3961F");
			return;
		}
		WorldTip.showNow("Test Ads disabled", false, "top", 3f, "#F3961F");
	}

	// Token: 0x0600235D RID: 9053 RVA: 0x00126D28 File Offset: 0x00124F28
	public static void setOption(DebugOption pOption, bool pVal, bool pUpdateSpecialSettings = true)
	{
		if (DebugConfig._dictionary == null)
		{
			DebugConfig.init();
		}
		DebugConfig._dictionary[pOption] = pVal;
		if (pUpdateSpecialSettings)
		{
			if (pOption == DebugOption.Graphy)
			{
				DebugConfig.checkGraphy();
			}
			if (pOption == DebugOption.SonicSpeed && Config.game_loaded)
			{
				if (pVal)
				{
					Config.setWorldSpeed("x40", false);
					return;
				}
				Config.setWorldSpeed("x1", false);
			}
		}
	}

	// Token: 0x0600235E RID: 9054 RVA: 0x00126D80 File Offset: 0x00124F80
	public static void checkSonicTimeScales()
	{
		if (DebugConfig.isOn(DebugOption.SonicSpeed))
		{
			Config.setWorldSpeed("x40", false);
			return;
		}
		Config.setWorldSpeed("x1", false);
	}

	// Token: 0x0600235F RID: 9055 RVA: 0x00126DA2 File Offset: 0x00124FA2
	public static void checkGraphy()
	{
		if (PrefabLibrary.instance != null)
		{
			PrefabLibrary.instance.graphy.gameObject.SetActive(DebugConfig.isOn(DebugOption.Graphy));
		}
	}

	// Token: 0x06002360 RID: 9056 RVA: 0x00126DCC File Offset: 0x00124FCC
	public static void createTool(string pID, int pX = 80, int pY = -10, int pWidth = -1)
	{
		Bench.bench_enabled = true;
		DebugToolAsset tAsset = AssetManager.debug_tool_library.get(pID);
		DebugTool tTool = Object.Instantiate<DebugTool>(PrefabLibrary.instance.debugTool, DebugConfig.instance.transform);
		tTool.setAsset(tAsset);
		tTool.populateOptions();
		for (int i = 0; i < tTool.dropdown.options.Count; i++)
		{
			if (tTool.dropdown.options[i].text == pID)
			{
				tTool.dropdown.value = i;
				tTool.dropdown.captionText.text = pID;
				break;
			}
		}
		tTool.transform.localPosition = new Vector3((float)pX, (float)pY);
		DebugConfig._pos_x += 128;
	}

	// Token: 0x170001F5 RID: 501
	// (get) Token: 0x06002361 RID: 9057 RVA: 0x00126E8E File Offset: 0x0012508E
	public static bool debug_enabled
	{
		get
		{
			return DebugConfig.instance.debugButton.gameObject.activeSelf;
		}
	}

	// Token: 0x06002362 RID: 9058 RVA: 0x00126EA4 File Offset: 0x001250A4
	private static void addDebugTool(string pID)
	{
		DebugConfig.default_debug_tools.Add(pID);
	}

	// Token: 0x0400198E RID: 6542
	public GameObject debugButton;

	// Token: 0x0400198F RID: 6543
	public static DebugConfig instance;

	// Token: 0x04001990 RID: 6544
	private static Dictionary<DebugOption, bool> _dictionary;

	// Token: 0x04001991 RID: 6545
	private static int _pos_x = 20;

	// Token: 0x04001992 RID: 6546
	public static List<string> default_debug_tools = new List<string>();
}
