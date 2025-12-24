using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Beebyte.Obfuscator;
using UnityEngine;
using UnityEngine.CrashReportHandler;

// Token: 0x020001C8 RID: 456
[ObfuscateLiterals]
public class Config
{
	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000D5F RID: 3423 RVA: 0x000BBDF8 File Offset: 0x000B9FF8
	public static bool worldLoading
	{
		get
		{
			return SmoothLoader.isLoading();
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000D60 RID: 3424 RVA: 0x000BBDFF File Offset: 0x000B9FFF
	// (set) Token: 0x06000D61 RID: 3425 RVA: 0x000BBE06 File Offset: 0x000BA006
	public static bool paused
	{
		get
		{
			return Config._paused;
		}
		set
		{
			Config._paused = value;
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000D63 RID: 3427 RVA: 0x000BBE16 File Offset: 0x000BA016
	// (set) Token: 0x06000D62 RID: 3426 RVA: 0x000BBE0E File Offset: 0x000BA00E
	public static bool hasPremium
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		get
		{
			return !Config.editor_test_rewards_from_ads && Config._hpr;
		}
		set
		{
			Config._hpr = value;
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000D64 RID: 3428 RVA: 0x000BBE26 File Offset: 0x000BA026
	// (set) Token: 0x06000D65 RID: 3429 RVA: 0x000BBE2D File Offset: 0x000BA02D
	public static string current_brush
	{
		get
		{
			return Config._current_brush;
		}
		set
		{
			Config._current_brush = value;
			Config.current_brush_data = Brush.get(value);
		}
	}

	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06000D66 RID: 3430 RVA: 0x000BBE40 File Offset: 0x000BA040
	// (set) Token: 0x06000D67 RID: 3431 RVA: 0x000BBE47 File Offset: 0x000BA047
	public static BrushData current_brush_data { get; private set; }

	// Token: 0x06000D68 RID: 3432 RVA: 0x000BBE4F File Offset: 0x000BA04F
	public static void setDraggingObject(IDraggable pGameObject)
	{
		Config.dragging_item_object = pGameObject;
		Config._dragging_item = true;
	}

	// Token: 0x06000D69 RID: 3433 RVA: 0x000BBE5D File Offset: 0x000BA05D
	public static bool isDraggingObject(IDraggable pGameObject)
	{
		return Config.dragging_item_object != null && Config.dragging_item_object == pGameObject;
	}

	// Token: 0x06000D6A RID: 3434 RVA: 0x000BBE73 File Offset: 0x000BA073
	public static IDraggable getDraggingObject()
	{
		return Config.dragging_item_object;
	}

	// Token: 0x06000D6B RID: 3435 RVA: 0x000BBE7A File Offset: 0x000BA07A
	public static void clearDraggingObject()
	{
		Config._dragging_item = false;
		Config.dragging_item_object = null;
	}

	// Token: 0x06000D6C RID: 3436 RVA: 0x000BBE88 File Offset: 0x000BA088
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isDraggingItem()
	{
		return Config._dragging_item;
	}

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06000D6D RID: 3437 RVA: 0x000BBE8F File Offset: 0x000BA08F
	public static bool joyControls
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06000D6E RID: 3438 RVA: 0x000BBE92 File Offset: 0x000BA092
	public static void setWorldSpeed(WorldTimeScaleAsset pAsset, bool pUpdateDebug = true)
	{
		if (!pAsset.sonic && pUpdateDebug)
		{
			DebugConfig.setOption(DebugOption.SonicSpeed, false, false);
		}
		Config.time_scale_asset = pAsset;
	}

	// Token: 0x06000D6F RID: 3439 RVA: 0x000BBEB0 File Offset: 0x000BA0B0
	public static void setWorldSpeed(string pID, bool pUpdateDebug = true)
	{
		Config.setWorldSpeed(AssetManager.time_scales.get(pID), pUpdateDebug);
	}

	// Token: 0x06000D70 RID: 3440 RVA: 0x000BBEC3 File Offset: 0x000BA0C3
	public static void nextWorldSpeed(bool pCycle = false)
	{
		Config.setWorldSpeed(Config.time_scale_asset.getNext(pCycle), true);
	}

	// Token: 0x06000D71 RID: 3441 RVA: 0x000BBED6 File Offset: 0x000BA0D6
	public static void prevWorldSpeed()
	{
		Config.setWorldSpeed(Config.time_scale_asset.getPrevious(false), true);
	}

	// Token: 0x06000D72 RID: 3442 RVA: 0x000BBEEC File Offset: 0x000BA0EC
	public static void setPortrait(bool pValue)
	{
		if (pValue)
		{
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
			Screen.autorotateToLandscapeLeft = false;
			Screen.autorotateToLandscapeRight = false;
			if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown)
			{
				Screen.orientation = ScreenOrientation.PortraitUpsideDown;
			}
			else
			{
				Screen.orientation = ScreenOrientation.Portrait;
			}
			Screen.orientation = ScreenOrientation.AutoRotation;
			World.world.camera.ResetAspect();
			return;
		}
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
		Screen.autorotateToLandscapeLeft = true;
		Screen.autorotateToLandscapeRight = true;
		if (Input.deviceOrientation == DeviceOrientation.LandscapeRight)
		{
			Screen.orientation = ScreenOrientation.LandscapeRight;
		}
		else
		{
			Screen.orientation = ScreenOrientation.LandscapeLeft;
		}
		Screen.orientation = ScreenOrientation.AutoRotation;
		World.world.camera.ResetAspect();
	}

	// Token: 0x06000D73 RID: 3443 RVA: 0x000BBF84 File Offset: 0x000BA184
	public static void setAutorotation(bool pValue)
	{
		OptionAsset tPortrait = AssetManager.options_library.get("portrait");
		if (pValue)
		{
			Screen.autorotateToPortrait = true;
			Screen.autorotateToPortraitUpsideDown = true;
			Screen.autorotateToLandscapeLeft = true;
			Screen.autorotateToLandscapeRight = true;
			ScreenOrientation orientation;
			switch (Input.deviceOrientation)
			{
			case DeviceOrientation.Portrait:
				orientation = ScreenOrientation.Portrait;
				break;
			case DeviceOrientation.PortraitUpsideDown:
				orientation = ScreenOrientation.PortraitUpsideDown;
				break;
			case DeviceOrientation.LandscapeLeft:
				orientation = ScreenOrientation.LandscapeLeft;
				break;
			case DeviceOrientation.LandscapeRight:
				orientation = ScreenOrientation.LandscapeRight;
				break;
			default:
				orientation = ScreenOrientation.Portrait;
				break;
			}
			Screen.orientation = orientation;
			Screen.orientation = ScreenOrientation.AutoRotation;
			tPortrait.interactable = false;
			World.world.camera.ResetAspect();
			return;
		}
		tPortrait.interactable = true;
		Config.setPortrait(PlayerConfig.optionBoolEnabled("portrait"));
	}

	// Token: 0x06000D74 RID: 3444 RVA: 0x000BC027 File Offset: 0x000BA227
	public static void enableAutoRotation(bool pValue)
	{
	}

	// Token: 0x06000D75 RID: 3445 RVA: 0x000BC02C File Offset: 0x000BA22C
	public static bool skipCrashMetadata()
	{
		return Config.MODDED || Config.experimental_mode || !(Config.gen ?? true);
	}

	// Token: 0x06000D76 RID: 3446 RVA: 0x000BC068 File Offset: 0x000BA268
	[Skip]
	[SkipRename]
	[DoNotFake]
	public static void updateCrashMetadata()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (SmoothLoader.isLoading())
		{
			return;
		}
		if (Config.skip)
		{
			return;
		}
		if (Config.timer > 0f)
		{
			Config.timer -= Time.fixedDeltaTime;
			return;
		}
		Config.timer = 30f;
		if (Config.skipCrashMetadata())
		{
			Config.skip = true;
			CrashReportHandler.enableCaptureExceptions = false;
			return;
		}
		CrashReportHandler.enableCaptureExceptions = false;
		try
		{
			if (!string.IsNullOrEmpty(Config.versionCodeText))
			{
				CrashReportHandler.SetUserMetadata("u_versionCodeText", Config.versionCodeText);
			}
			if (!string.IsNullOrEmpty(Config.gitCodeText))
			{
				CrashReportHandler.SetUserMetadata("u_gitCodeText", Config.gitCodeText);
			}
			CrashReportHandler.SetUserMetadata("c_MODDED", Config.MODDED.ToString());
			CrashReportHandler.SetUserMetadata("c_HAVE_PREMIUM", Config.hasPremium.ToString());
			CrashReportHandler.SetUserMetadata("c_game_speed", Config.time_scale_asset.id);
			CrashReportHandler.SetUserMetadata("c_sonic_speed", DebugConfig.isOn(DebugOption.SonicSpeed).ToString());
			CrashReportHandler.SetUserMetadata("c_show_map_names", Zones.showMapNames().ToString());
			if (DebugConfig.instance.debugButton.gameObject.activeSelf)
			{
				CrashReportHandler.SetUserMetadata("c_debug_button", "visible");
			}
			CrashReportHandler.SetUserMetadata("o_camera_lowRes", World.world.quality_changer.isLowRes().ToString());
			CrashReportHandler.SetUserMetadata("o_selected_power", World.world.getSelectedPowerID());
			CrashReportHandler.SetUserMetadata("c_map_mode", World.world.zone_calculator.getCurrentModeDebug().ToString());
			if (ScrollWindow.isWindowActive())
			{
				CrashReportHandler.SetUserMetadata("o_window_open", ScrollWindow.getCurrentWindow().screen_id);
			}
			else
			{
				CrashReportHandler.SetUserMetadata("o_window_open", "false");
			}
			string tPowerLog = "";
			for (int i = 0; i < Config._loggedSelectedPowers.Count; i++)
			{
				tPowerLog = tPowerLog + Config._loggedSelectedPowers[i] + ",";
			}
			CrashReportHandler.SetUserMetadata("o_power_history", tPowerLog);
			CrashReportHandler.SetUserMetadata("map_units", World.world.units.Count.ToString());
			CrashReportHandler.SetUserMetadata("map_buildings", World.world.buildings.Count.ToString());
			CrashReportHandler.SetUserMetadata("map_civ_kingdoms", World.world.kingdoms.Count.ToString());
			CrashReportHandler.SetUserMetadata("map_cultures", World.world.cultures.Count.ToString());
			CrashReportHandler.SetUserMetadata("map_layers", World.world.zone_calculator.zones.Count.ToString());
			CrashReportHandler.SetUserMetadata("map_chunks", World.world.map_chunk_manager.chunks.Length.ToString());
			CrashReportHandler.SetUserMetadata("map_drops_active", World.world.drop_manager.getActiveIndex().ToString());
			CrashReportHandler.SetUserMetadata("map_stackEffects_active", World.world.stack_effects.countActive().ToString());
			try
			{
				CrashReportHandler.SetUserMetadata("u_installMode", Application.installMode.ToString());
				CrashReportHandler.SetUserMetadata("u_sandboxType", Application.sandboxType.ToString());
				CrashReportHandler.SetUserMetadata("g_activeTier", Graphics.activeTier.ToString());
			}
			catch (Exception)
			{
			}
		}
		catch (Exception message)
		{
			Config.skip = true;
			Debug.LogError(message);
			throw;
		}
		CrashReportHandler.enableCaptureExceptions = true;
	}

	// Token: 0x06000D77 RID: 3447 RVA: 0x000BC41C File Offset: 0x000BA61C
	public static void logSelectedPower(GodPower pPower)
	{
		if (Config._loggedSelectedPowers.Count > 5)
		{
			Config._loggedSelectedPowers.RemoveAt(0);
		}
		Config._loggedSelectedPowers.Add(pPower.id);
	}

	// Token: 0x06000D78 RID: 3448 RVA: 0x000BC446 File Offset: 0x000BA646
	public static void scheduleGC(string pWhere, bool pUnloadResources = false)
	{
		Config._scheduledGC = true;
		if (pUnloadResources)
		{
			Config._scheduledGCUnload = true;
		}
	}

	// Token: 0x06000D79 RID: 3449 RVA: 0x000BC457 File Offset: 0x000BA657
	public static void checkGC()
	{
		if (Config._scheduledGC)
		{
			Config.forceGC("scheduled", Config._scheduledGCUnload);
		}
	}

	// Token: 0x06000D7A RID: 3450 RVA: 0x000BC46F File Offset: 0x000BA66F
	public static void forceGC(string pWhere, bool pUnloadResources = false)
	{
		if (pUnloadResources)
		{
			Resources.UnloadUnusedAssets();
			GC.Collect(1, GCCollectionMode.Optimized, false);
		}
		else
		{
			GC.Collect(0, GCCollectionMode.Optimized, false);
		}
		Config._scheduledGC = false;
		Config._scheduledGCUnload = false;
	}

	// Token: 0x06000D7B RID: 3451 RVA: 0x000BC498 File Offset: 0x000BA698
	public static float getScrollToGroupDelay()
	{
		if (!Config.isMobile)
		{
			return 0.4f;
		}
		return 0.55f;
	}

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x06000D7C RID: 3452 RVA: 0x000BC4AC File Offset: 0x000BA6AC
	public static string gs { get; } = "";

	// Token: 0x06000D7D RID: 3453 RVA: 0x000BC4B3 File Offset: 0x000BA6B3
	public static void fireworksCheck(bool pEnabled)
	{
		Config.EVERYTHING_FIREWORKS = pEnabled;
		PlayerConfig.instance.data.fireworksCheck2025 = pEnabled;
		PlayerConfig.saveData();
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x000BC4D0 File Offset: 0x000BA6D0
	public static void valCheck(bool pEnabled)
	{
		PlayerConfig.instance.data.valCheck2025 = pEnabled;
		PlayerConfig.saveData();
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x000BC4E7 File Offset: 0x000BA6E7
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void pCheck(bool value)
	{
		PlayerConfig.instance.data.pPossible0507 = value;
		PlayerConfig.saveData();
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x000BC4FE File Offset: 0x000BA6FE
	public static void magicCheck(bool pEnabled)
	{
		Config.EVERYTHING_MAGIC_COLOR = pEnabled;
		PlayerConfig.instance.data.magicCheck2025 = pEnabled;
		PlayerConfig.saveData();
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x000BC51B File Offset: 0x000BA71B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void givePremium()
	{
		InAppManager.activatePrem(false);
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x000BC523 File Offset: 0x000BA723
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static void removePremium()
	{
		Config.hasPremium = false;
		PlayerConfig.instance.data.premium = false;
		PlayerConfig.saveData();
		PremiumElementsChecker.checkElements();
	}

	// Token: 0x04000CB5 RID: 3253
	public static bool parallel_jobs_updater = true;

	// Token: 0x04000CB6 RID: 3254
	public static bool parallel_chunk_manager = true;

	// Token: 0x04000CB7 RID: 3255
	public static string versionCodeText = string.Empty;

	// Token: 0x04000CB8 RID: 3256
	public static string gitCodeText = string.Empty;

	// Token: 0x04000CB9 RID: 3257
	public static string versionCodeDate = string.Empty;

	// Token: 0x04000CBA RID: 3258
	public static string iname = string.Empty;

	// Token: 0x04000CBB RID: 3259
	internal static bool? gen = null;

	// Token: 0x04000CBC RID: 3260
	public static string testStreamingAssets = "test";

	// Token: 0x04000CBD RID: 3261
	public static bool ui_main_hidden = false;

	// Token: 0x04000CBE RID: 3262
	public static int WORLD_SAVE_VERSION = 17;

	// Token: 0x04000CBF RID: 3263
	public static string current_map_template = "continent";

	// Token: 0x04000CC0 RID: 3264
	public static string customMapSize = "standard";

	// Token: 0x04000CC1 RID: 3265
	public static int customZoneX = 0;

	// Token: 0x04000CC2 RID: 3266
	public static int customZoneY = 0;

	// Token: 0x04000CC3 RID: 3267
	public static int customPerlinScale = 10;

	// Token: 0x04000CC4 RID: 3268
	public static int customRandomShapes = 10;

	// Token: 0x04000CC5 RID: 3269
	public static int customWaterLevel = 10;

	// Token: 0x04000CC6 RID: 3270
	public static int ZONE_AMOUNT_X = 4;

	// Token: 0x04000CC7 RID: 3271
	public static int ZONE_AMOUNT_Y = 4;

	// Token: 0x04000CC8 RID: 3272
	public static string customMapSizeDefault = "standard";

	// Token: 0x04000CC9 RID: 3273
	public static string maxMapSize = "iceberg";

	// Token: 0x04000CCA RID: 3274
	public static int ZONE_AMOUNT_X_DEFAULT = 3;

	// Token: 0x04000CCB RID: 3275
	public static int ZONE_AMOUNT_Y_DEFAULT = 4;

	// Token: 0x04000CCC RID: 3276
	public const int MAP_BLOCK_SIZE = 64;

	// Token: 0x04000CCD RID: 3277
	public const int CHUNK_SIZE = 16;

	// Token: 0x04000CCE RID: 3278
	public const int TILES_IN_CHUNK = 256;

	// Token: 0x04000CCF RID: 3279
	public const int CITY_ZONE_SIZE = 8;

	// Token: 0x04000CD0 RID: 3280
	public const int CITY_ZONE_TILES = 64;

	// Token: 0x04000CD1 RID: 3281
	public const int TILES_IN_REGION = 256;

	// Token: 0x04000CD2 RID: 3282
	public const int PREVIEW_MAP_SIZE = 512;

	// Token: 0x04000CD3 RID: 3283
	public const float FOCUS_SCROLL_DELAY_PC = 0.4f;

	// Token: 0x04000CD4 RID: 3284
	public const float FOCUS_SCROLL_DELAY_PHONE = 0.55f;

	// Token: 0x04000CD5 RID: 3285
	public static WorldTimeScaleAsset time_scale_asset = null;

	// Token: 0x04000CD6 RID: 3286
	public static bool fps_lock_30 = false;

	// Token: 0x04000CD7 RID: 3287
	public static bool MODDED = false;

	// Token: 0x04000CD8 RID: 3288
	public static bool EVERYTHING_MAGIC_COLOR = false;

	// Token: 0x04000CD9 RID: 3289
	public static bool EVERYTHING_FIREWORKS = false;

	// Token: 0x04000CDA RID: 3290
	private static bool _paused = false;

	// Token: 0x04000CDB RID: 3291
	public static bool lockGameControls = false;

	// Token: 0x04000CDC RID: 3292
	internal static string steam_name;

	// Token: 0x04000CDD RID: 3293
	internal static string steam_id;

	// Token: 0x04000CDE RID: 3294
	internal static bool steam_language_allow_detect = true;

	// Token: 0x04000CDF RID: 3295
	internal static string discordId;

	// Token: 0x04000CE0 RID: 3296
	internal static string discordName;

	// Token: 0x04000CE1 RID: 3297
	internal static string discordDiscriminator;

	// Token: 0x04000CE2 RID: 3298
	public static bool testAds = false;

	// Token: 0x04000CE3 RID: 3299
	public static bool firebaseInitiating = false;

	// Token: 0x04000CE4 RID: 3300
	public static bool firebaseChecked = false;

	// Token: 0x04000CE5 RID: 3301
	public static bool firebaseEnabled = true;

	// Token: 0x04000CE6 RID: 3302
	public static bool authEnabled = false;

	// Token: 0x04000CE7 RID: 3303
	public const string firebaseDatabaseURL = "https://worldbox-g.firebaseio.com/";

	// Token: 0x04000CE8 RID: 3304
	public const string baseURL = "https://versions.superworldbox.com";

	// Token: 0x04000CE9 RID: 3305
	public const string currencyURL = "https://currency.superworldbox.com";

	// Token: 0x04000CEA RID: 3306
	public static bool adsInitialized = false;

	// Token: 0x04000CEB RID: 3307
	public static bool disable_dispose_logs = true;

	// Token: 0x04000CEC RID: 3308
	public static bool disable_loading_logs = false;

	// Token: 0x04000CED RID: 3309
	public static bool disable_discord = false;

	// Token: 0x04000CEE RID: 3310
	public static bool disable_steam = false;

	// Token: 0x04000CEF RID: 3311
	public static bool disable_db = false;

	// Token: 0x04000CF0 RID: 3312
	public static bool disable_startup_window = false;

	// Token: 0x04000CF1 RID: 3313
	public static bool disable_tutorial = false;

	// Token: 0x04000CF2 RID: 3314
	public static bool debug_log_meta_ranks = false;

	// Token: 0x04000CF3 RID: 3315
	public static string debug_last_selected_power_button;

	// Token: 0x04000CF4 RID: 3316
	public static string debug_last_window;

	// Token: 0x04000CF5 RID: 3317
	public static int debug_worlds_loaded;

	// Token: 0x04000CF6 RID: 3318
	public static WindowStats debug_window_stats;

	// Token: 0x04000CF7 RID: 3319
	public static bool load_random_test_map = false;

	// Token: 0x04000CF8 RID: 3320
	public static bool load_new_map = false;

	// Token: 0x04000CF9 RID: 3321
	public static bool load_dragon = false;

	// Token: 0x04000CFA RID: 3322
	public static bool load_save_on_start = false;

	// Token: 0x04000CFB RID: 3323
	public static bool load_save_from_path = false;

	// Token: 0x04000CFC RID: 3324
	public static string load_test_save_path = "";

	// Token: 0x04000CFD RID: 3325
	public static bool load_test_map = false;

	// Token: 0x04000CFE RID: 3326
	public static int load_save_on_start_slot = 1;

	// Token: 0x04000CFF RID: 3327
	public static string auto_test_on_start = null;

	// Token: 0x04000D00 RID: 3328
	public static float LOAD_TIME_INIT = 0f;

	// Token: 0x04000D01 RID: 3329
	public static float LOAD_TIME_CREATE = 0f;

	// Token: 0x04000D02 RID: 3330
	public static float LOAD_TIME_GENERATE = 0f;

	// Token: 0x04000D03 RID: 3331
	public static float LAST_LOAD_TIME = 0f;

	// Token: 0x04000D04 RID: 3332
	public static bool editor_test_rewards_from_ads = false;

	// Token: 0x04000D05 RID: 3333
	private static bool _hpr = false;

	// Token: 0x04000D06 RID: 3334
	public static bool sprite_animations_on = true;

	// Token: 0x04000D07 RID: 3335
	public static bool shadows_active = false;

	// Token: 0x04000D08 RID: 3336
	public static bool tooltips_active = true;

	// Token: 0x04000D09 RID: 3337
	public static bool preload_windows = true;

	// Token: 0x04000D0A RID: 3338
	public static bool preload_quantum_sprites = true;

	// Token: 0x04000D0B RID: 3339
	public static bool preload_buildings = true;

	// Token: 0x04000D0C RID: 3340
	public static bool preload_units = true;

	// Token: 0x04000D0D RID: 3341
	public static bool autosaves = true;

	// Token: 0x04000D0E RID: 3342
	public static bool graphs = true;

	// Token: 0x04000D0F RID: 3343
	public static bool experimental_mode = false;

	// Token: 0x04000D10 RID: 3344
	public static bool wbb_confirmed = false;

	// Token: 0x04000D11 RID: 3345
	public static bool full_screen = true;

	// Token: 0x04000D12 RID: 3346
	public static bool firebase_available = false;

	// Token: 0x04000D13 RID: 3347
	public static bool upload_available = false;

	// Token: 0x04000D14 RID: 3348
	public static bool game_loaded = false;

	// Token: 0x04000D15 RID: 3349
	public static bool show_console_on_start = false;

	// Token: 0x04000D16 RID: 3350
	public static bool show_console_on_error = true;

	// Token: 0x04000D17 RID: 3351
	public static bool editor_maxim = false;

	// Token: 0x04000D18 RID: 3352
	public static bool editor_mastef = false;

	// Token: 0x04000D19 RID: 3353
	public static bool editor_nikon = false;

	// Token: 0x04000D1A RID: 3354
	public static bool editor_devs = false;

	// Token: 0x04000D1B RID: 3355
	public static bool fmod_test_build = false;

	// Token: 0x04000D1C RID: 3356
	public static bool isEditor = false;

	// Token: 0x04000D1D RID: 3357
	public static bool isMobile = false;

	// Token: 0x04000D1E RID: 3358
	public static bool isIos = false;

	// Token: 0x04000D1F RID: 3359
	public static bool isAndroid = false;

	// Token: 0x04000D20 RID: 3360
	public static bool isComputer = true;

	// Token: 0x04000D21 RID: 3361
	public static bool grey_goo_damaged = false;

	// Token: 0x04000D22 RID: 3362
	public static GodPower power_to_unlock;

	// Token: 0x04000D23 RID: 3363
	private static string _current_brush = "circ_5";

	// Token: 0x04000D25 RID: 3365
	public static string selected_trait_editor = string.Empty;

	// Token: 0x04000D26 RID: 3366
	public static readonly SelectedObjectsGraph selected_objects_graph = new SelectedObjectsGraph();

	// Token: 0x04000D27 RID: 3367
	private static bool _dragging_item = false;

	// Token: 0x04000D28 RID: 3368
	public static IDraggable dragging_item_object = null;

	// Token: 0x04000D29 RID: 3369
	public static Kingdom whisper_A;

	// Token: 0x04000D2A RID: 3370
	public static Kingdom whisper_B;

	// Token: 0x04000D2B RID: 3371
	public static Kingdom unity_A;

	// Token: 0x04000D2C RID: 3372
	public static Kingdom unity_B;

	// Token: 0x04000D2D RID: 3373
	private static float timer = 30f;

	// Token: 0x04000D2E RID: 3374
	private static bool skip = false;

	// Token: 0x04000D2F RID: 3375
	private static List<string> _loggedSelectedPowers = new List<string>();

	// Token: 0x04000D30 RID: 3376
	private static bool _scheduledGC = false;

	// Token: 0x04000D31 RID: 3377
	private static bool _scheduledGCUnload = false;

	// Token: 0x04000D32 RID: 3378
	public static string gv;
}
