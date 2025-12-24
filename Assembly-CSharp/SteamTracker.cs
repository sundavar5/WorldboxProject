using System;
using RSG;
using Steamworks;
using UnityEngine;

// Token: 0x02000586 RID: 1414
public class SteamTracker : MonoBehaviour, IRichTracker
{
	// Token: 0x06002EF0 RID: 12016 RVA: 0x0016BE58 File Offset: 0x0016A058
	private void Start()
	{
		this.instance = this;
		SteamSDK.steamInitialized.Then(delegate()
		{
			SteamTracker.init();
		}).Catch(delegate(Exception _)
		{
			Object.Destroy(this.instance);
		});
	}

	// Token: 0x06002EF1 RID: 12017 RVA: 0x0016BEA7 File Offset: 0x0016A0A7
	private void OnDestroy()
	{
		this.instance = null;
		PowerTracker.steamTracker = null;
	}

	// Token: 0x06002EF2 RID: 12018 RVA: 0x0016BEB6 File Offset: 0x0016A0B6
	private static bool init()
	{
		if (SteamSDK.steamInitialized != null && SteamSDK.steamInitialized.CurState == PromiseState.Resolved)
		{
			SteamTracker.steam_initialized = true;
		}
		return SteamTracker.steam_initialized;
	}

	// Token: 0x06002EF3 RID: 12019 RVA: 0x0016BED8 File Offset: 0x0016A0D8
	public void trackViewing(string pText)
	{
		if (!SteamTracker.steam_initialized && !SteamTracker.init())
		{
			return;
		}
		if (pText == "" || !LocalizedTextManager.stringExists(pText))
		{
			this.trackActivity("#Status_browsing");
			return;
		}
		SteamFriends.SetRichPresence("window", LocalizedTextManager.getText(pText, null, false));
		this.trackActivity("#Status_viewing");
	}

	// Token: 0x06002EF4 RID: 12020 RVA: 0x0016BF33 File Offset: 0x0016A133
	public void trackWatching()
	{
		this.trackActivity("#Status_watching");
	}

	// Token: 0x06002EF5 RID: 12021 RVA: 0x0016BF40 File Offset: 0x0016A140
	public void trackUsing(string pPower)
	{
		if (!SteamTracker.steam_initialized && !SteamTracker.init())
		{
			return;
		}
		SteamFriends.SetRichPresence("power", LocalizedTextManager.getText(pPower, null, false));
		this.trackActivity("#Status_using");
	}

	// Token: 0x06002EF6 RID: 12022 RVA: 0x0016BF70 File Offset: 0x0016A170
	public void updateUsing(int pAmount, string pPower = "")
	{
		if (!SteamTracker.steam_initialized && !SteamTracker.init())
		{
			return;
		}
		if (pPower != "")
		{
			SteamFriends.SetRichPresence("power", LocalizedTextManager.getText(pPower, null, false));
		}
		SteamFriends.SetRichPresence("amount", pAmount.ToString());
		this.trackActivity("#Status_using_num");
	}

	// Token: 0x06002EF7 RID: 12023 RVA: 0x0016BFC9 File Offset: 0x0016A1C9
	public void inspectKingdom(string pKingdom)
	{
		if (!SteamTracker.steam_initialized && !SteamTracker.init())
		{
			return;
		}
		SteamFriends.SetRichPresence("kingdom", pKingdom);
		this.trackActivity("#Status_kingdom");
	}

	// Token: 0x06002EF8 RID: 12024 RVA: 0x0016BFF1 File Offset: 0x0016A1F1
	public void inspectVillage(string pVillage)
	{
		if (!SteamTracker.steam_initialized && !SteamTracker.init())
		{
			return;
		}
		SteamFriends.SetRichPresence("village", pVillage);
		this.trackActivity("#Status_village");
	}

	// Token: 0x06002EF9 RID: 12025 RVA: 0x0016C019 File Offset: 0x0016A219
	public void inspectUnit(string pUnit)
	{
		if (!SteamTracker.steam_initialized && !SteamTracker.init())
		{
			return;
		}
		SteamFriends.SetRichPresence("unit", pUnit);
		this.trackActivity("#Status_unit");
	}

	// Token: 0x06002EFA RID: 12026 RVA: 0x0016C041 File Offset: 0x0016A241
	public void spectatingUnit(string pUnit)
	{
		if (!SteamTracker.steam_initialized && !SteamTracker.init())
		{
			return;
		}
		SteamFriends.SetRichPresence("unit", pUnit);
		this.trackActivity("#Status_spectating");
	}

	// Token: 0x06002EFB RID: 12027 RVA: 0x0016C06C File Offset: 0x0016A26C
	public void updateDetails(StatisticsAsset pStat)
	{
		if (!SteamTracker.steam_initialized && !SteamTracker.init())
		{
			return;
		}
		string tKey = pStat.getLocaleID();
		if (!string.IsNullOrEmpty(tKey))
		{
			SteamFriends.SetRichPresence("stat", tKey);
		}
		SteamFriends.SetRichPresence("value", pStat.last_value);
		this.trackActivity(pStat.steam_activity);
	}

	// Token: 0x06002EFC RID: 12028 RVA: 0x0016C0C0 File Offset: 0x0016A2C0
	public void trackActivity(string pText)
	{
		if (!SteamTracker.steam_initialized && !SteamTracker.init())
		{
			return;
		}
		SteamTracker.timer = 10f;
		try
		{
			if (pText.Substring(0, 1) != "#")
			{
				Debug.LogError(pText);
			}
			else
			{
				SteamFriends.SetRichPresence("steam_display", pText);
			}
		}
		catch (Exception message)
		{
			Debug.LogError("Could not set Steam Rich Presence (Steam not running, or game not run as Administrator)");
			Debug.LogError(message);
		}
	}

	// Token: 0x06002EFD RID: 12029 RVA: 0x0016C134 File Offset: 0x0016A334
	private void Update()
	{
		if (!SteamTracker.steam_initialized)
		{
			return;
		}
		if (SteamTracker.timer > 0f)
		{
			SteamTracker.timer -= Time.deltaTime;
			return;
		}
		SteamTracker.timer = 10f;
		try
		{
			this.updateDetails(PowerTracker.activeStat);
		}
		catch (Exception message)
		{
			Debug.Log("Steam Failed or Disabled");
			Debug.Log(message);
			Object.Destroy(this.instance);
		}
	}

	// Token: 0x040022FC RID: 8956
	private static bool steam_initialized = false;

	// Token: 0x040022FD RID: 8957
	private SteamTracker instance;

	// Token: 0x040022FE RID: 8958
	private static float timer = 10f;
}
