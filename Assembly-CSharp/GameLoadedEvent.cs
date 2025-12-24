using System;
using Proyecto26;
using UnityEngine;

// Token: 0x020001CC RID: 460
public class GameLoadedEvent : BaseMapObject
{
	// Token: 0x06000DA4 RID: 3492 RVA: 0x000BD68D File Offset: 0x000BB88D
	private void Awake()
	{
		LogText.log("GameLoadedEvent", "Awake", "st");
		LogText.log("GameLoadedEvent", "Awake", "en");
		this.setVersionData();
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x000BD6C0 File Offset: 0x000BB8C0
	private void setVersionData()
	{
		TextAsset tTextAsset = Resources.Load("texts/build_info") as TextAsset;
		try
		{
			Config.versionCodeText = tTextAsset.text.Split('$', StringSplitOptions.None)[0];
			Config.versionCodeDate = tTextAsset.text.Split('$', StringSplitOptions.None)[1];
		}
		catch (Exception)
		{
			if (tTextAsset != null)
			{
				Config.versionCodeText = tTextAsset.text;
				Config.versionCodeDate = "";
			}
		}
		try
		{
			RestClient.DefaultRequestHeaders["wb-build"] = (Config.versionCodeText ?? "na");
		}
		catch (Exception)
		{
		}
		try
		{
			TextAsset gitAsset = Resources.Load("texts/git_info") as TextAsset;
			if (gitAsset != null)
			{
				Config.gitCodeText = gitAsset.text;
			}
			try
			{
				RestClient.DefaultRequestHeaders["wb-git"] = (Config.gitCodeText ?? "na");
			}
			catch (Exception)
			{
			}
		}
		catch (Exception message)
		{
			Debug.Log(message);
		}
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x000BD7D4 File Offset: 0x000BB9D4
	internal override void create()
	{
		base.create();
		Config.LOAD_TIME_INIT = Time.realtimeSinceStartup;
		LogText.log("GameLoadedEvent", "create", "");
		LocalizedTextManager.instance.setLanguage(PlayerConfig.dict["language"].stringVal);
		if (Globals.TRAILER_MODE)
		{
			TrailerModeSettings.startEvent();
		}
		World.world.startTheGame(false);
		GodPower.diagnostic();
		Config.updateCrashMetadata();
	}
}
