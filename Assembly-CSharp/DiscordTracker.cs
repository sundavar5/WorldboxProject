using System;
using Discord;
using Proyecto26;
using UnityEngine;

// Token: 0x0200057D RID: 1405
public class DiscordTracker : MonoBehaviour, IRichTracker
{
	// Token: 0x06002E8B RID: 11915 RVA: 0x00166A74 File Offset: 0x00164C74
	private void Start()
	{
		if (this._initiated)
		{
			return;
		}
		this._initiated = true;
		bool tDestroy = false;
		try
		{
			DiscordTracker._instance = this;
			DiscordTracker._discord = new Discord(816251591299432468L, 1UL);
			DiscordTracker._activity_manager = DiscordTracker._discord.GetActivityManager();
			Activity activity = default(Activity);
			activity.State = LocalizedTextManager.getText("discord_browsing", null, false);
			activity.Assets.LargeImage = "worldboxlogo";
			activity.Assets.LargeText = "WorldBox";
			activity.Timestamps.Start = (long)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
			activity.Instance = true;
			DiscordTracker._activity = activity;
			ActivityManager activity_manager = DiscordTracker._activity_manager;
			if (activity_manager != null)
			{
				activity_manager.UpdateActivity(DiscordTracker._activity, delegate(Result pRes)
				{
					if (pRes != Result.Ok)
					{
						Debug.Log("Disabling Discord");
						Debug.Log(pRes);
						Object.Destroy(DiscordTracker._instance);
					}
				});
			}
		}
		catch (ResultException message)
		{
			Debug.Log("Disabling Discord Integration (Discord not running, or game not run as Administrator)");
			Debug.Log(message);
			tDestroy = true;
		}
		catch (Exception message2)
		{
			Debug.Log("Disabling Discord Integration (Discord not running, or game not run as Administrator)");
			Debug.Log(message2);
			tDestroy = true;
		}
		if (tDestroy)
		{
			Object.Destroy(DiscordTracker._instance);
		}
	}

	// Token: 0x06002E8C RID: 11916 RVA: 0x00166BBC File Offset: 0x00164DBC
	private static void tryGetUser()
	{
		try
		{
			DiscordTracker._user_tries--;
			User tUser = DiscordTracker._discord.GetUserManager().GetCurrentUser();
			string tUserID = tUser.Id.ToString();
			if (!string.IsNullOrEmpty(tUserID))
			{
				Config.discordId = tUserID;
				RestClient.DefaultRequestHeaders["wb-dsc"] = tUserID;
				DiscordTracker._have_user = true;
				Debug.Log("D:" + Config.discordId);
			}
			else
			{
				Debug.Log("D:nf");
			}
			string tUsername = tUser.Username;
			if (!string.IsNullOrEmpty(tUsername))
			{
				Config.discordName = tUsername;
			}
			string tUserDiscriminator = tUser.Discriminator;
			if (!string.IsNullOrEmpty(tUserDiscriminator))
			{
				Config.discordDiscriminator = tUserDiscriminator;
			}
			VersionCheck.checkVersion();
		}
		catch (Exception)
		{
			Debug.Log("D:F");
		}
	}

	// Token: 0x06002E8D RID: 11917 RVA: 0x00166C84 File Offset: 0x00164E84
	private void Update()
	{
		if (!this._initiated)
		{
			return;
		}
		try
		{
			DiscordTracker._discord.RunCallbacks();
		}
		catch (Exception message)
		{
			Debug.Log("Disabling Discord");
			Debug.Log(message);
			Object.Destroy(DiscordTracker._instance);
			return;
		}
		if (DiscordTracker._timer > 0f)
		{
			DiscordTracker._timer -= Time.deltaTime;
			return;
		}
		DiscordTracker._timer = 10f;
		try
		{
			if (!DiscordTracker._have_user && DiscordTracker._user_tries > 0)
			{
				DiscordTracker.tryGetUser();
			}
			this.updateDetails(PowerTracker.activeStat);
		}
		catch (Exception message2)
		{
			Debug.Log("Disabling Discord");
			Debug.Log(message2);
			Object.Destroy(DiscordTracker._instance);
		}
	}

	// Token: 0x06002E8E RID: 11918 RVA: 0x00166D44 File Offset: 0x00164F44
	private void OnDisable()
	{
		Discord discord = DiscordTracker._discord;
		if (discord == null)
		{
			return;
		}
		discord.Dispose();
	}

	// Token: 0x06002E8F RID: 11919 RVA: 0x00166D55 File Offset: 0x00164F55
	private void OnDestroy()
	{
		DiscordTracker._instance = null;
		DiscordTracker._activity_manager = null;
		PowerTracker.discordTracker = null;
	}

	// Token: 0x06002E90 RID: 11920 RVA: 0x00166D6C File Offset: 0x00164F6C
	public void trackViewing(string pString)
	{
		if (DiscordTracker._instance == null)
		{
			return;
		}
		if (pString != "" && LocalizedTextManager.stringExists(pString))
		{
			pString = LocalizedTextManager.getText("discord_viewing", null, false).Replace("$window$", LocalizedTextManager.getText(pString, null, false));
		}
		else
		{
			if (pString != "")
			{
				Debug.Log("Missing translation for " + pString);
			}
			pString = LocalizedTextManager.getText("discord_browsing", null, false);
		}
		this.trackActivity(pString);
	}

	// Token: 0x06002E91 RID: 11921 RVA: 0x00166DF0 File Offset: 0x00164FF0
	public void trackWatching()
	{
		if (DiscordTracker._instance == null)
		{
			return;
		}
		this.trackActivity(LocalizedTextManager.getText("discord_watching", null, false));
	}

	// Token: 0x06002E92 RID: 11922 RVA: 0x00166E12 File Offset: 0x00165012
	public void trackUsing(string pPower)
	{
		if (DiscordTracker._instance == null)
		{
			return;
		}
		this.trackActivity(LocalizedTextManager.getText("discord_using", null, false).Replace("$power$", LocalizedTextManager.getText(pPower, null, false)));
	}

	// Token: 0x06002E93 RID: 11923 RVA: 0x00166E46 File Offset: 0x00165046
	public void updateUsing(int pAmount, string pPower = "")
	{
		this.trackActivity(LocalizedTextManager.getText(pPower, null, false) + " (" + pAmount.ToString() + ")");
	}

	// Token: 0x06002E94 RID: 11924 RVA: 0x00166E6C File Offset: 0x0016506C
	public void inspectKingdom(string pKingdom)
	{
		this.trackActivity(LocalizedTextManager.getText("village_statistics_kingdom", null, false) + ": " + pKingdom);
	}

	// Token: 0x06002E95 RID: 11925 RVA: 0x00166E8B File Offset: 0x0016508B
	public void inspectVillage(string pVillage)
	{
		this.trackActivity(LocalizedTextManager.getText("village", null, false) + ": " + pVillage);
	}

	// Token: 0x06002E96 RID: 11926 RVA: 0x00166EAA File Offset: 0x001650AA
	public void inspectUnit(string pUnit)
	{
		this.trackActivity("inspect".Localize() + ": " + pUnit);
	}

	// Token: 0x06002E97 RID: 11927 RVA: 0x00166EC7 File Offset: 0x001650C7
	public void spectatingUnit(string pUnit)
	{
		this.trackActivity(LocalizedTextManager.getText("tip_following_unit", null, false).Replace("$name$", pUnit));
	}

	// Token: 0x06002E98 RID: 11928 RVA: 0x00166EE8 File Offset: 0x001650E8
	public void trackActivity(string pState = "")
	{
		if (DiscordTracker._instance == null)
		{
			return;
		}
		DiscordTracker._activity.State = pState;
		ActivityManager activity_manager = DiscordTracker._activity_manager;
		if (activity_manager == null)
		{
			return;
		}
		activity_manager.UpdateActivity(DiscordTracker._activity, delegate(Result _)
		{
		});
	}

	// Token: 0x06002E99 RID: 11929 RVA: 0x00166F44 File Offset: 0x00165144
	public void updateDetails(StatisticsAsset pStat)
	{
		if (DiscordTracker._instance == null)
		{
			return;
		}
		string tKey = pStat.getLocaleID();
		if (!string.IsNullOrEmpty(tKey))
		{
			DiscordTracker._activity.Details = LocalizedTextManager.getText(tKey, null, false) + ": " + pStat.last_value;
		}
		else
		{
			DiscordTracker._activity.Details = pStat.last_value;
		}
		ActivityManager activity_manager = DiscordTracker._activity_manager;
		if (activity_manager == null)
		{
			return;
		}
		activity_manager.UpdateActivity(DiscordTracker._activity, delegate(Result _)
		{
		});
	}

	// Token: 0x040022D5 RID: 8917
	private const long DISCORD_GAME_ID = 816251591299432468L;

	// Token: 0x040022D6 RID: 8918
	private const ulong DISCORD_FLAGS = 1UL;

	// Token: 0x040022D7 RID: 8919
	private static Discord _discord;

	// Token: 0x040022D8 RID: 8920
	private static ActivityManager _activity_manager;

	// Token: 0x040022D9 RID: 8921
	private bool _initiated;

	// Token: 0x040022DA RID: 8922
	private static DiscordTracker _instance;

	// Token: 0x040022DB RID: 8923
	private static Activity _activity;

	// Token: 0x040022DC RID: 8924
	private static bool _have_user = false;

	// Token: 0x040022DD RID: 8925
	private static int _user_tries = 10;

	// Token: 0x040022DE RID: 8926
	private static float _timer = 10f;
}
