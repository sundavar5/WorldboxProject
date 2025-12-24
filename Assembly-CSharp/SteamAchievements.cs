using System;
using System.Collections.Generic;
using RSG;
using Steamworks;
using Steamworks.Data;
using UnityEngine;

// Token: 0x020005AD RID: 1453
internal static class SteamAchievements
{
	// Token: 0x0600301C RID: 12316 RVA: 0x00174410 File Offset: 0x00172610
	public static void InitAchievements()
	{
		SteamSDK.steamInitialized.Then(delegate()
		{
			foreach (Steamworks.Data.Achievement a in SteamUserStats.Achievements)
			{
				if (a.State)
				{
					SteamAchievements.unlockAchievement(a.Identifier);
					if (!AchievementLibrary.isUnlocked(a.Identifier))
					{
						Debug.Log("Was unlocked in Steam already, unlocking in the game: " + a.Identifier);
						AchievementLibrary.unlock(a.Identifier);
					}
				}
				if (!a.State && AchievementLibrary.isUnlocked(a.Identifier))
				{
					Debug.Log("Was not unlocked in Steam yet, unlocking: " + a.Identifier);
					SteamAchievements.TriggerAchievement(a.Identifier);
				}
			}
			SteamAchievements.initialized.Resolve();
		}).Catch(delegate(Exception err)
		{
			Debug.Log("Error happened while getting Steam Achievement");
			Debug.Log(err);
			SteamAchievements.initialized.Reject(new Exception("Steam Achievements not available"));
		});
	}

	// Token: 0x0600301D RID: 12317 RVA: 0x0017446C File Offset: 0x0017266C
	public static void TriggerAchievement(string id)
	{
		if (SteamAchievements.isSteamAchievementUnlocked(id))
		{
			return;
		}
		SteamAchievements.initialized.Then(delegate()
		{
			if (SteamAchievements.isSteamAchievementUnlocked(id))
			{
				return;
			}
			Debug.Log("Unlocking in Steam: " + id);
			Steamworks.Data.Achievement ach = new Steamworks.Data.Achievement(id);
			ach.Trigger(true);
			SteamAchievements.unlockAchievement(id);
		});
	}

	// Token: 0x0600301E RID: 12318 RVA: 0x001744AB File Offset: 0x001726AB
	public static void unlockAchievement(string pName)
	{
		SteamAchievements.achievements_hashset.Add(pName);
	}

	// Token: 0x0600301F RID: 12319 RVA: 0x001744B9 File Offset: 0x001726B9
	public static bool isSteamAchievementUnlocked(string pName)
	{
		return SteamAchievements.achievements_hashset.Contains(pName);
	}

	// Token: 0x04002447 RID: 9287
	private static Promise initialized = new Promise();

	// Token: 0x04002448 RID: 9288
	private static HashSet<string> achievements_hashset = new HashSet<string>();
}
