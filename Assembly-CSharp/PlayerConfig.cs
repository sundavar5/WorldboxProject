using System;
using System.Collections.Generic;
using System.IO;
using Firebase.Analytics;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000478 RID: 1144
public class PlayerConfig
{
	// Token: 0x06002744 RID: 10052 RVA: 0x0013E5F9 File Offset: 0x0013C7F9
	public static void init()
	{
		if (PlayerConfig.instance != null)
		{
			return;
		}
		Debug.Log("INIT PlayerConfig");
		PlayerConfig.instance = new PlayerConfig();
		PlayerConfig.instance.create();
	}

	// Token: 0x06002745 RID: 10053 RVA: 0x0013E624 File Offset: 0x0013C824
	public void create()
	{
		this.rewardCheckTimer = this.rewardCheckTimerInterval;
		this.setNewDataPath();
		Debug.Log("Init PlayerConfig");
		if (File.Exists(this.dataPath))
		{
			try
			{
				this.loadData();
				return;
			}
			catch (Exception)
			{
				this.initNewSave();
				return;
			}
		}
		this.initNewSave();
	}

	// Token: 0x06002746 RID: 10054 RVA: 0x0013E684 File Offset: 0x0013C884
	internal void start()
	{
		AdButtonTimer.setAdTimer();
	}

	// Token: 0x06002747 RID: 10055 RVA: 0x0013E68B File Offset: 0x0013C88B
	internal void update()
	{
	}

	// Token: 0x06002748 RID: 10056 RVA: 0x0013E68D File Offset: 0x0013C88D
	private void setNewDataPath()
	{
		this.dataPath = Application.persistentDataPath + "/worldboxData";
	}

	// Token: 0x06002749 RID: 10057 RVA: 0x0013E6A4 File Offset: 0x0013C8A4
	private void initNewSave()
	{
		this.data = new PlayerConfigData();
		this.data.initData();
		PlayerConfig.dict["language"].stringVal = PlayerConfig.detectLanguage();
		Config.steam_language_allow_detect = true;
		if (Globals.specialAbstudio)
		{
			PlayerConfig.dict["language"].stringVal = "fa";
		}
		PlayerConfig.saveData();
	}

	// Token: 0x0600274A RID: 10058 RVA: 0x0013E70C File Offset: 0x0013C90C
	public static void setFirebaseProp(string pName, string pProp)
	{
		if (!Config.isMobile)
		{
			return;
		}
		if (!Config.firebase_available)
		{
			return;
		}
		try
		{
			FirebaseAnalytics.SetUserProperty(pName, pProp);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600274B RID: 10059 RVA: 0x0013E748 File Offset: 0x0013C948
	public static void toggleFullScreen()
	{
		PlayerConfig.setFullScreen(!PlayerConfig.dict["fullscreen"].boolVal, true);
	}

	// Token: 0x0600274C RID: 10060 RVA: 0x0013E768 File Offset: 0x0013C968
	public static void setFullScreen(bool pFullScreen, bool pSwitchScreen = true)
	{
		PlayerConfig.dict["fullscreen"].boolVal = pFullScreen;
		PlayerConfig.saveData();
		OptionAsset tAsset = AssetManager.options_library.get("fullscreen");
		tAsset.action(tAsset);
	}

	// Token: 0x0600274D RID: 10061 RVA: 0x0013E7AC File Offset: 0x0013C9AC
	public static string detectLanguage()
	{
		switch (Application.systemLanguage)
		{
		case SystemLanguage.Arabic:
			return "ar";
		case SystemLanguage.Czech:
			return "cs";
		case SystemLanguage.Danish:
			return "da";
		case SystemLanguage.Dutch:
			return "nl";
		case SystemLanguage.English:
			return "en";
		case SystemLanguage.Finnish:
			return "fn";
		case SystemLanguage.French:
			return "fr";
		case SystemLanguage.German:
			return "de";
		case SystemLanguage.Greek:
			return "gr";
		case SystemLanguage.Hebrew:
			return "he";
		case SystemLanguage.Hungarian:
			return "hu";
		case SystemLanguage.Indonesian:
			return "id";
		case SystemLanguage.Italian:
			return "it";
		case SystemLanguage.Japanese:
			return "ja";
		case SystemLanguage.Korean:
			return "ko";
		case SystemLanguage.Lithuanian:
			return "lt";
		case SystemLanguage.Norwegian:
			return "no";
		case SystemLanguage.Polish:
			return "pl";
		case SystemLanguage.Portuguese:
			return "pt";
		case SystemLanguage.Romanian:
			return "ro";
		case SystemLanguage.Russian:
			return "ru";
		case SystemLanguage.SerboCroatian:
			return "hr";
		case SystemLanguage.Slovak:
			return "sk";
		case SystemLanguage.Spanish:
			return "es";
		case SystemLanguage.Swedish:
			return "sv";
		case SystemLanguage.Thai:
			return "th";
		case SystemLanguage.Turkish:
			return "tr";
		case SystemLanguage.Ukrainian:
			return "ua";
		case SystemLanguage.Vietnamese:
			return "vn";
		case SystemLanguage.ChineseSimplified:
			return "cz";
		case SystemLanguage.ChineseTraditional:
			return "ch";
		}
		return "en";
	}

	// Token: 0x0600274E RID: 10062 RVA: 0x0013E9A4 File Offset: 0x0013CBA4
	public static void saveData()
	{
		string tJsonData = Toolbox.encode(PlayerConfig.instance.data.toJson());
		Toolbox.WriteSafely("Player Config", PlayerConfig.instance.dataPath, ref tJsonData);
		foreach (PlayerOptionData tData in PlayerConfig.dict.Values)
		{
			if (tData.boolVal)
			{
				PlayerConfig.setFirebaseProp("option_" + tData.name, tData.boolVal ? "on" : "off");
			}
		}
		PlayerConfig.setFirebaseProp("option_language", PlayerConfig.dict["language"].stringVal);
	}

	// Token: 0x0600274F RID: 10063 RVA: 0x0013EA70 File Offset: 0x0013CC70
	private void loadData()
	{
		if (!File.Exists(this.dataPath))
		{
			return;
		}
		string fileString = File.ReadAllText(this.dataPath);
		string decodedString = "";
		try
		{
			decodedString = Toolbox.decode(fileString);
		}
		catch (Exception)
		{
			decodedString = "";
		}
		if (string.IsNullOrEmpty(decodedString))
		{
			try
			{
				decodedString = Toolbox.decodeMobile(fileString);
			}
			catch (Exception)
			{
				decodedString = "";
			}
		}
		if (!string.IsNullOrEmpty(decodedString))
		{
			fileString = decodedString;
		}
		if (fileString.Contains("list"))
		{
			this.data = JsonConvert.DeserializeObject<PlayerConfigData>(fileString);
			this.data.initData();
			string tCurrentLanguage = this.data.get("language").stringVal;
			if (tCurrentLanguage == "boat" || tCurrentLanguage == "keys")
			{
				this.data.get("language").stringVal = PlayerConfig.detectLanguage();
			}
		}
		else
		{
			this.initNewSave();
		}
		if (this.data.fireworksCheck2025)
		{
			Config.EVERYTHING_FIREWORKS = true;
		}
		if (this.data.magicCheck2025)
		{
			Config.EVERYTHING_MAGIC_COLOR = true;
		}
		if (Config.isEditor && Config.editor_test_rewards_from_ads)
		{
			this.data.rewardedPowers.Clear();
		}
		if (this.data.premium)
		{
			Config.hasPremium = true;
		}
		bool tSaveData = false;
		if (this.moveTraitsAndAchievements())
		{
			tSaveData = true;
		}
		if (this.handleDebugOptions())
		{
			tSaveData = true;
		}
		if (tSaveData)
		{
			PlayerConfig.saveData();
		}
	}

	// Token: 0x06002750 RID: 10064 RVA: 0x0013EBDC File Offset: 0x0013CDDC
	internal static bool optionEnabled(string gameOptionName, OptionType pType)
	{
		foreach (PlayerOptionData tData in PlayerConfig.instance.data.list)
		{
			if (!(tData.name != gameOptionName) && pType == OptionType.Bool)
			{
				return tData.boolVal;
			}
		}
		return false;
	}

	// Token: 0x06002751 RID: 10065 RVA: 0x0013EC50 File Offset: 0x0013CE50
	public static int getIntValue(string pID)
	{
		PlayerOptionData tData = PlayerConfig.dict[pID];
		OptionAsset tOptionAsset = AssetManager.options_library.get(pID);
		if (tData.intVal != Mathf.Clamp(tData.intVal, tOptionAsset.min_value, tOptionAsset.max_value))
		{
			return tOptionAsset.default_int;
		}
		return tData.intVal;
	}

	// Token: 0x06002752 RID: 10066 RVA: 0x0013ECA1 File Offset: 0x0013CEA1
	public static bool optionBoolEnabled(string pName)
	{
		return PlayerConfig.dict[pName].boolVal;
	}

	// Token: 0x06002753 RID: 10067 RVA: 0x0013ECB3 File Offset: 0x0013CEB3
	public static int getOptionInt(string pName)
	{
		return PlayerConfig.dict[pName].intVal;
	}

	// Token: 0x06002754 RID: 10068 RVA: 0x0013ECC5 File Offset: 0x0013CEC5
	public static string getOptionString(string pName)
	{
		return PlayerConfig.dict[pName].stringVal;
	}

	// Token: 0x06002755 RID: 10069 RVA: 0x0013ECD7 File Offset: 0x0013CED7
	public static void setOptionBool(string pName, bool pVal)
	{
		PlayerConfig.dict[pName].boolVal = pVal;
	}

	// Token: 0x06002756 RID: 10070 RVA: 0x0013ECEA File Offset: 0x0013CEEA
	public static void setOptionInt(string pName, int pVal)
	{
		PlayerConfig.dict[pName].intVal = pVal;
	}

	// Token: 0x06002757 RID: 10071 RVA: 0x0013ECFD File Offset: 0x0013CEFD
	public static void setOptionString(string pName, string pVal)
	{
		PlayerConfig.dict[pName].stringVal = pVal;
	}

	// Token: 0x06002758 RID: 10072 RVA: 0x0013ED10 File Offset: 0x0013CF10
	[Obsolete]
	internal static void switchOption(string gameOptionName, OptionType pType)
	{
		foreach (PlayerOptionData tData in PlayerConfig.instance.data.list)
		{
			if (!(tData.name != gameOptionName) && pType == OptionType.Bool)
			{
				tData.boolVal = !tData.boolVal;
			}
		}
		PlayerConfig.checkSettings();
	}

	// Token: 0x06002759 RID: 10073 RVA: 0x0013ED8C File Offset: 0x0013CF8C
	public static void setVsync(bool vsyncEnabled)
	{
		if (!vsyncEnabled)
		{
			QualitySettings.vSyncCount = 0;
			if (Config.fps_lock_30)
			{
				if (Application.targetFrameRate != 30)
				{
					Application.targetFrameRate = 30;
					return;
				}
			}
			else if (Application.targetFrameRate != 60)
			{
				Application.targetFrameRate = 60;
			}
			return;
		}
		if (Screen.currentResolution.refreshRateRatio.value < 61.0)
		{
			QualitySettings.vSyncCount = 1;
			return;
		}
		if (Screen.currentResolution.refreshRateRatio.value < 121.0)
		{
			QualitySettings.vSyncCount = 2;
			return;
		}
		if (Screen.currentResolution.refreshRateRatio.value < 181.0)
		{
			QualitySettings.vSyncCount = 3;
			return;
		}
		QualitySettings.vSyncCount = 4;
	}

	// Token: 0x0600275A RID: 10074 RVA: 0x0013EE46 File Offset: 0x0013D046
	public static void turnOffAssetsPreloading()
	{
		PlayerConfig.setOptionBool("preload_units", false);
		PlayerConfig.setOptionBool("preload_buildings", false);
		PlayerConfig.setOptionBool("preload_quantum_sprites", false);
		PlayerConfig.setOptionBool("preload_windows", false);
	}

	// Token: 0x0600275B RID: 10075 RVA: 0x0013EE74 File Offset: 0x0013D074
	internal static void checkSettings()
	{
		if (SystemInfo.systemMemorySize < 3000 && !PlayerConfig._memory_check_done)
		{
			PlayerConfig._memory_check_done = true;
			Debug.Log("[RAM is MEH] SystemInfo.systemMemorySize: " + SystemInfo.systemMemorySize.ToString());
			PlayerConfig.turnOffAssetsPreloading();
		}
		foreach (OptionAsset tAsset in AssetManager.options_library.list)
		{
			if (!tAsset.computer_only || Config.isComputer)
			{
				if (tAsset.reset_to_default_on_launch)
				{
					PlayerConfig.setOptionBool(tAsset.id, tAsset.default_bool);
					PlayerConfig.setOptionInt(tAsset.id, tAsset.default_int);
					PlayerConfig.setOptionString(tAsset.id, tAsset.default_string);
				}
				ActionOptionAsset action = tAsset.action;
				if (action != null)
				{
					action(tAsset);
				}
			}
		}
	}

	// Token: 0x0600275C RID: 10076 RVA: 0x0013EF5C File Offset: 0x0013D15C
	public static int countRewards()
	{
		PlayerConfig playerConfig = PlayerConfig.instance;
		bool flag;
		if (playerConfig == null)
		{
			flag = (null != null);
		}
		else
		{
			PlayerConfigData playerConfigData = playerConfig.data;
			flag = (((playerConfigData != null) ? playerConfigData.rewardedPowers : null) != null);
		}
		if (flag)
		{
			return PlayerConfig.instance.data.rewardedPowers.Count;
		}
		return 0;
	}

	// Token: 0x0600275D RID: 10077 RVA: 0x0013EF93 File Offset: 0x0013D193
	public static void clearRewards()
	{
		PlayerConfig playerConfig = PlayerConfig.instance;
		if (playerConfig == null)
		{
			return;
		}
		PlayerConfigData playerConfigData = playerConfig.data;
		if (playerConfigData == null)
		{
			return;
		}
		List<RewardedPower> rewardedPowers = playerConfigData.rewardedPowers;
		if (rewardedPowers == null)
		{
			return;
		}
		rewardedPowers.Clear();
	}

	// Token: 0x0600275E RID: 10078 RVA: 0x0013EFB8 File Offset: 0x0013D1B8
	private bool moveTraitsAndAchievements()
	{
		bool tSave = false;
		List<string> achievements = this.data.achievements;
		if (achievements != null && achievements.Count > 0)
		{
			foreach (string pName in this.data.achievements)
			{
				GameProgress.unlockAchievement(pName);
			}
			this.data.achievements.Clear();
			tSave = true;
		}
		List<string> unlocked_traits = this.data.unlocked_traits;
		if (unlocked_traits != null && unlocked_traits.Count > 0)
		{
			foreach (string tID in this.data.unlocked_traits)
			{
				ActorTrait tTrait = AssetManager.traits.get(tID);
				if (tTrait != null)
				{
					tTrait.unlock(true);
				}
			}
			this.data.unlocked_traits.Clear();
			tSave = true;
		}
		return tSave;
	}

	// Token: 0x0600275F RID: 10079 RVA: 0x0013F0C4 File Offset: 0x0013D2C4
	private bool handleDebugOptions()
	{
		bool tSave = false;
		if (this.data.clearDebugOnStart)
		{
			DebugConfig.setOption(DebugOption.DisablePremium, false, true);
			DebugConfig.setOption(DebugOption.TestAds, false, true);
			this.data.clearDebugOnStart = false;
			this.data.premiumDisabled = false;
			this.data.testAds = false;
			tSave = true;
		}
		else
		{
			if (this.data.premiumDisabled)
			{
				DebugConfig.setOption(DebugOption.DisablePremium, true, true);
				this.data.clearDebugOnStart = true;
				tSave = true;
			}
			if (this.data.testAds)
			{
				DebugConfig.setOption(DebugOption.TestAds, true, true);
				this.data.clearDebugOnStart = true;
				tSave = true;
			}
		}
		return tSave;
	}

	// Token: 0x04001D82 RID: 7554
	public static PlayerConfig instance;

	// Token: 0x04001D83 RID: 7555
	public static Dictionary<string, PlayerOptionData> dict = new Dictionary<string, PlayerOptionData>();

	// Token: 0x04001D84 RID: 7556
	private string dataPath;

	// Token: 0x04001D85 RID: 7557
	internal PlayerConfigData data;

	// Token: 0x04001D86 RID: 7558
	private float rewardCheckTimer = 10f;

	// Token: 0x04001D87 RID: 7559
	private float rewardCheckTimerInterval = 60f;

	// Token: 0x04001D88 RID: 7560
	private static bool _memory_check_done = false;
}
