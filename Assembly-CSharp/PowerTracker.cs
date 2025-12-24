using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200057E RID: 1406
internal class PowerTracker : MonoBehaviour
{
	// Token: 0x17000265 RID: 613
	// (get) Token: 0x06002E9C RID: 11932 RVA: 0x00166FF6 File Offset: 0x001651F6
	private static List<StatisticsAsset> rotateStats
	{
		get
		{
			return StatisticsLibrary.power_tracker_pool;
		}
	}

	// Token: 0x06002E9D RID: 11933 RVA: 0x00166FFD File Offset: 0x001651FD
	private void Start()
	{
		PowerTracker.instance = this;
		if (!Config.disable_discord)
		{
			PowerTracker.discordTracker = base.gameObject.AddComponent<DiscordTracker>();
		}
		if (!Config.disable_steam)
		{
			PowerTracker.steamTracker = base.gameObject.AddComponent<SteamTracker>();
		}
		PowerTracker.initiated = true;
	}

	// Token: 0x06002E9E RID: 11934 RVA: 0x0016703C File Offset: 0x0016523C
	internal static void PlusOne(GodPower pPower = null)
	{
		if (PowerTracker.instance == null)
		{
			return;
		}
		if (MoveCamera.hasFocusUnit())
		{
			return;
		}
		if (pPower == null)
		{
			return;
		}
		PowerTracker.frameDone = true;
		PowerTracker.amount++;
		DiscordTracker discordTracker = PowerTracker.discordTracker;
		if (discordTracker != null)
		{
			discordTracker.updateUsing(PowerTracker.amount, pPower.getLocaleID());
		}
		SteamTracker steamTracker = PowerTracker.steamTracker;
		if (steamTracker == null)
		{
			return;
		}
		steamTracker.updateUsing(PowerTracker.amount, pPower.getLocaleID());
	}

	// Token: 0x06002E9F RID: 11935 RVA: 0x001670AC File Offset: 0x001652AC
	internal static void trackPower(string pString = "")
	{
		if (PowerTracker.instance == null)
		{
			return;
		}
		if (MoveCamera.hasFocusUnit())
		{
			return;
		}
		if (pString == "Button Close")
		{
			return;
		}
		if (pString == "ButtonLeader")
		{
			return;
		}
		if (pString == "ButtonKingdom")
		{
			return;
		}
		if (pString == "ButtonCapital")
		{
			return;
		}
		PowerTracker.amount = 0;
		if (PowerTracker.frameDone)
		{
			return;
		}
		SteamTracker steamTracker = PowerTracker.steamTracker;
		if (steamTracker != null)
		{
			steamTracker.trackViewing(pString);
		}
		DiscordTracker discordTracker = PowerTracker.discordTracker;
		if (discordTracker == null)
		{
			return;
		}
		discordTracker.trackViewing(pString);
	}

	// Token: 0x06002EA0 RID: 11936 RVA: 0x00167138 File Offset: 0x00165338
	internal static void setPower(GodPower pPower)
	{
		if (PowerTracker.instance == null)
		{
			return;
		}
		if (MoveCamera.hasFocusUnit())
		{
			return;
		}
		PowerTracker.frameDone = true;
		if (pPower == null)
		{
			PowerTracker.trackWatching();
		}
		else
		{
			if (!pPower.track_activity)
			{
				return;
			}
			if (LocalizedTextManager.stringExists(pPower.getLocaleID()))
			{
				DiscordTracker discordTracker = PowerTracker.discordTracker;
				if (discordTracker != null)
				{
					discordTracker.trackUsing(pPower.getLocaleID());
				}
				SteamTracker steamTracker = PowerTracker.steamTracker;
				if (steamTracker != null)
				{
					steamTracker.trackUsing(pPower.getLocaleID());
				}
			}
		}
		PowerTracker.amount = 0;
	}

	// Token: 0x06002EA1 RID: 11937 RVA: 0x001671B3 File Offset: 0x001653B3
	internal static void trackWatching()
	{
		if (PowerTracker.instance == null)
		{
			return;
		}
		DiscordTracker discordTracker = PowerTracker.discordTracker;
		if (discordTracker != null)
		{
			discordTracker.trackWatching();
		}
		SteamTracker steamTracker = PowerTracker.steamTracker;
		if (steamTracker == null)
		{
			return;
		}
		steamTracker.trackWatching();
	}

	// Token: 0x06002EA2 RID: 11938 RVA: 0x001671E2 File Offset: 0x001653E2
	internal static void spectatingUnit(string pUnit)
	{
		if (PowerTracker.instance == null)
		{
			return;
		}
		PowerTracker.frameDone = true;
		SteamTracker steamTracker = PowerTracker.steamTracker;
		if (steamTracker != null)
		{
			steamTracker.spectatingUnit(pUnit);
		}
		DiscordTracker discordTracker = PowerTracker.discordTracker;
		if (discordTracker == null)
		{
			return;
		}
		discordTracker.spectatingUnit(pUnit);
	}

	// Token: 0x06002EA3 RID: 11939 RVA: 0x0016721C File Offset: 0x0016541C
	internal static void trackWindow(string screen_id, ScrollWindow pWindow)
	{
		if (PowerTracker.instance == null)
		{
			return;
		}
		PowerTracker.frameDone = true;
		if (screen_id == "")
		{
			return;
		}
		if (!(screen_id == "kingdom"))
		{
			if (!(screen_id == "city"))
			{
				if (!(screen_id == "unit"))
				{
					Transform title = pWindow.transform.Find("Background/Title");
					if (title == null)
					{
						title = pWindow.transform.FindRecursive("Title");
					}
					if (title != null && title.HasComponent<LocalizedText>() && title.GetComponent<LocalizedText>().key != "??????")
					{
						SteamTracker steamTracker = PowerTracker.steamTracker;
						if (steamTracker != null)
						{
							steamTracker.trackViewing(title.GetComponent<LocalizedText>().key);
						}
						DiscordTracker discordTracker = PowerTracker.discordTracker;
						if (discordTracker == null)
						{
							return;
						}
						discordTracker.trackViewing(title.GetComponent<LocalizedText>().key);
						return;
					}
					else
					{
						Debug.Log("[PT] Not found " + screen_id);
						SteamTracker steamTracker2 = PowerTracker.steamTracker;
						if (steamTracker2 != null)
						{
							steamTracker2.trackViewing(screen_id);
						}
						DiscordTracker discordTracker2 = PowerTracker.discordTracker;
						if (discordTracker2 == null)
						{
							return;
						}
						discordTracker2.trackViewing(screen_id);
						return;
					}
				}
				else
				{
					SteamTracker steamTracker3 = PowerTracker.steamTracker;
					if (steamTracker3 != null)
					{
						steamTracker3.inspectUnit(SelectedUnit.unit.getName());
					}
					DiscordTracker discordTracker3 = PowerTracker.discordTracker;
					if (discordTracker3 == null)
					{
						return;
					}
					discordTracker3.inspectUnit(SelectedUnit.unit.getName());
					return;
				}
			}
			else
			{
				SteamTracker steamTracker4 = PowerTracker.steamTracker;
				if (steamTracker4 != null)
				{
					steamTracker4.inspectVillage(SelectedMetas.selected_city.name);
				}
				DiscordTracker discordTracker4 = PowerTracker.discordTracker;
				if (discordTracker4 == null)
				{
					return;
				}
				discordTracker4.inspectVillage(SelectedMetas.selected_city.name);
				return;
			}
		}
		else
		{
			SteamTracker steamTracker5 = PowerTracker.steamTracker;
			if (steamTracker5 != null)
			{
				steamTracker5.inspectKingdom(SelectedMetas.selected_kingdom.name);
			}
			DiscordTracker discordTracker5 = PowerTracker.discordTracker;
			if (discordTracker5 == null)
			{
				return;
			}
			discordTracker5.inspectKingdom(SelectedMetas.selected_kingdom.name);
			return;
		}
	}

	// Token: 0x06002EA4 RID: 11940 RVA: 0x001673CD File Offset: 0x001655CD
	private static void resetTimer()
	{
		PowerTracker.timer = 9f;
	}

	// Token: 0x06002EA5 RID: 11941 RVA: 0x001673D9 File Offset: 0x001655D9
	private static void nextStat()
	{
		PowerTracker.currentIndex = Randy.randomInt(0, PowerTracker.rotateStats.Count);
		if (PowerTracker.currentIndex >= PowerTracker.rotateStats.Count)
		{
			PowerTracker.currentIndex = 0;
		}
	}

	// Token: 0x06002EA6 RID: 11942 RVA: 0x00167408 File Offset: 0x00165608
	private void updateStat()
	{
		if (PowerTracker.instance == null)
		{
			return;
		}
		StatisticsAsset tAsset = PowerTracker.rotateStats[PowerTracker.currentIndex];
		string tValue = tAsset.string_action(tAsset);
		if (tValue != "0" && !string.IsNullOrEmpty(tValue))
		{
			tAsset.last_value = tValue;
			PowerTracker.activeStat = tAsset;
			return;
		}
		PowerTracker.nextStat();
		this.updateStat();
	}

	// Token: 0x06002EA7 RID: 11943 RVA: 0x0016746E File Offset: 0x0016566E
	private void OnDestroy()
	{
		PowerTracker.instance = null;
	}

	// Token: 0x06002EA8 RID: 11944 RVA: 0x00167478 File Offset: 0x00165678
	private void Update()
	{
		if (PowerTracker.initiated && PowerTracker.discordTracker == null && PowerTracker.steamTracker == null)
		{
			Object.Destroy(this);
			Debug.Log("[PT] Destroying...");
			return;
		}
		if (PowerTracker.secTimer > 0f)
		{
			PowerTracker.secTimer -= Time.deltaTime;
		}
		else
		{
			PowerTracker.secTimer = 1f;
			this.updateStat();
		}
		if (PowerTracker.timer > 0f)
		{
			PowerTracker.timer -= Time.deltaTime;
			return;
		}
		PowerTracker.resetTimer();
		PowerTracker.nextStat();
	}

	// Token: 0x06002EA9 RID: 11945 RVA: 0x0016750C File Offset: 0x0016570C
	private void LateUpdate()
	{
		PowerTracker.frameDone = false;
	}

	// Token: 0x040022DF RID: 8927
	private static int amount = 0;

	// Token: 0x040022E0 RID: 8928
	private static PowerTracker instance;

	// Token: 0x040022E1 RID: 8929
	private static bool frameDone = false;

	// Token: 0x040022E2 RID: 8930
	internal static SteamTracker steamTracker;

	// Token: 0x040022E3 RID: 8931
	internal static DiscordTracker discordTracker;

	// Token: 0x040022E4 RID: 8932
	private static bool initiated = false;

	// Token: 0x040022E5 RID: 8933
	internal static StatisticsAsset activeStat;

	// Token: 0x040022E6 RID: 8934
	internal static string statValue = "";

	// Token: 0x040022E7 RID: 8935
	private static float timer = 10f;

	// Token: 0x040022E8 RID: 8936
	private static float secTimer = 1f;

	// Token: 0x040022E9 RID: 8937
	private static int currentIndex = 0;
}
