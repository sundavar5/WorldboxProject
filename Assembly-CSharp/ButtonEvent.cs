using System;
using UnityEngine;

// Token: 0x020005CC RID: 1484
public class ButtonEvent : MonoBehaviour
{
	// Token: 0x060030B9 RID: 12473 RVA: 0x00177780 File Offset: 0x00175980
	public void clickGenerateMap(string pValue)
	{
		World.world.clickGenerateNewMap();
	}

	// Token: 0x060030BA RID: 12474 RVA: 0x0017778C File Offset: 0x0017598C
	public void clickPremiumButton()
	{
		ScrollWindow.showWindow("steam");
	}

	// Token: 0x060030BB RID: 12475 RVA: 0x00177798 File Offset: 0x00175998
	public void clickPossess()
	{
		using (ListPool<Actor> tList = new ListPool<Actor>(SelectedUnit.getAllSelected().Count))
		{
			foreach (Actor tActor in SelectedUnit.getAllSelected())
			{
				if (tActor.asset.allow_possession)
				{
					tList.Add(tActor);
				}
			}
			if (tList.Count != 0)
			{
				ControllableUnit.setControllableCreatures(tList);
				ScrollWindow.hideAllEvent(true);
			}
		}
	}

	// Token: 0x060030BC RID: 12476 RVA: 0x00177838 File Offset: 0x00175A38
	public void openUnitTabTraitsEditor()
	{
		ActionLibrary.openUnitWindow(SelectedUnit.unit);
		ScrollWindow.getCurrentWindow().tabs.showTab("Traits");
	}

	// Token: 0x060030BD RID: 12477 RVA: 0x00177858 File Offset: 0x00175A58
	public void openUnitTabEquipmentEditor()
	{
		ActionLibrary.openUnitWindow(SelectedUnit.unit);
		ScrollWindow.getCurrentWindow().tabs.showTab("Equipment");
	}

	// Token: 0x060030BE RID: 12478 RVA: 0x00177878 File Offset: 0x00175A78
	public void openUnitTabMind()
	{
		ActionLibrary.openUnitWindow(SelectedUnit.unit);
		ScrollWindow.getCurrentWindow().tabs.showTab("Mind");
	}

	// Token: 0x060030BF RID: 12479 RVA: 0x00177898 File Offset: 0x00175A98
	public void openUnitTabGenealogy()
	{
		ActionLibrary.openUnitWindow(SelectedUnit.unit);
		ScrollWindow.getCurrentWindow().tabs.showTab("Genealogy");
	}

	// Token: 0x060030C0 RID: 12480 RVA: 0x001778B8 File Offset: 0x00175AB8
	public void openUnitTabPlot()
	{
		ActionLibrary.openUnitWindow(SelectedUnit.unit);
		ScrollWindow.getCurrentWindow().tabs.showTab("Plots");
	}

	// Token: 0x060030C1 RID: 12481 RVA: 0x001778D8 File Offset: 0x00175AD8
	public void openUnitSpectate()
	{
		World.world.followUnit(SelectedUnit.unit);
		ScrollWindow.hideAllEvent(true);
	}

	// Token: 0x060030C2 RID: 12482 RVA: 0x001778EF File Offset: 0x00175AEF
	public void openSettings()
	{
		ScrollWindow.showWindow("settings");
	}

	// Token: 0x060030C3 RID: 12483 RVA: 0x001778FB File Offset: 0x00175AFB
	public void openSavesList()
	{
		ScrollWindow.showWindow("saves_list");
	}

	// Token: 0x060030C4 RID: 12484 RVA: 0x00177907 File Offset: 0x00175B07
	public void openPremiumHelp()
	{
		ButtonEvent.premium_restore_opened++;
		ScrollWindow.showWindow("premium_help");
	}

	// Token: 0x060030C5 RID: 12485 RVA: 0x0017791F File Offset: 0x00175B1F
	public void openPremiumHelpFaq()
	{
		ButtonEvent.premium_more_help_pressed++;
		if (Config.isAndroid || Config.isEditor)
		{
			Application.OpenURL("https://www.superworldbox.com/faq#i-purchased-the-premium-on-android-but-haven-t-received-it-or-you-trying-to-play-on-new-another-android-device-with-the-same-account");
			return;
		}
		if (Config.isIos)
		{
			Application.OpenURL("https://www.superworldbox.com/faq#i-purchased-the-premium-on-ios-and-later-got-a-new-apple-device-how-do-i-restore-premium");
		}
	}

	// Token: 0x060030C6 RID: 12486 RVA: 0x00177957 File Offset: 0x00175B57
	public void openPatchNotes()
	{
		ScrollWindow.showWindow("patch_log");
		Analytics.LogEvent("open_link_changelog", true, true);
	}

	// Token: 0x060030C7 RID: 12487 RVA: 0x0017796F File Offset: 0x00175B6F
	public void clickRewardAds()
	{
		if (ScrollWindow.isCurrentWindow("reward_ads"))
		{
			return;
		}
		ScrollWindow.showWindow("reward_ads");
	}

	// Token: 0x060030C8 RID: 12488 RVA: 0x00177988 File Offset: 0x00175B88
	public void showWindow(string pID)
	{
		ScrollWindow.showWindow(pID);
	}

	// Token: 0x060030C9 RID: 12489 RVA: 0x00177990 File Offset: 0x00175B90
	public void locateSelectedVillage()
	{
		World.world.locateSelectedVillage();
	}

	// Token: 0x060030CA RID: 12490 RVA: 0x0017799C File Offset: 0x00175B9C
	public void locateSelectedUnit()
	{
		World.world.followUnit(SelectedUnit.unit);
		ScrollWindow.hideAllEvent(true);
	}

	// Token: 0x060030CB RID: 12491 RVA: 0x001779B4 File Offset: 0x00175BB4
	public void locateSelectedArmy()
	{
		Army tArmy = SelectedMetas.selected_army;
		Actor tToFollow;
		if (tArmy.hasCaptain())
		{
			tToFollow = tArmy.getCaptain();
		}
		else
		{
			tToFollow = tArmy.units.GetRandom<Actor>();
		}
		World.world.followUnit(tToFollow);
		ScrollWindow.hideAllEvent(true);
	}

	// Token: 0x060030CC RID: 12492 RVA: 0x001779F5 File Offset: 0x00175BF5
	public void startLoadSaveSlot()
	{
		AutoSaveManager.autoSave(true, false);
		World.world.save_manager.startLoadSlot();
	}

	// Token: 0x060030CD RID: 12493 RVA: 0x00177A0D File Offset: 0x00175C0D
	public void clickSaveSlot()
	{
		AutoSaveManager.resetAutoSaveTimer();
		World.world.save_manager.clickSaveSlot();
	}

	// Token: 0x060030CE RID: 12494 RVA: 0x00177A23 File Offset: 0x00175C23
	public void confirmDeleteWorld()
	{
		SaveManager.deleteCurrentSave();
	}

	// Token: 0x060030CF RID: 12495 RVA: 0x00177A2A File Offset: 0x00175C2A
	public void startTutorialBear()
	{
		World.world.tutorial.startTutorial();
	}

	// Token: 0x060030D0 RID: 12496 RVA: 0x00177A3B File Offset: 0x00175C3B
	public void showRewardedAd()
	{
		PlayerConfig.instance.data.powerReward = string.Empty;
		if (!Config.isMobile && !Config.isEditor)
		{
			return;
		}
		RewardedAds.instance.ShowRewardedAd("gift");
	}

	// Token: 0x060030D1 RID: 12497 RVA: 0x00177A6F File Offset: 0x00175C6F
	public void showRewardedSaveSlotAd()
	{
		PlayerConfig.instance.data.powerReward = "saveslots";
		if (!Config.isMobile && !Config.isEditor)
		{
			return;
		}
		RewardedAds.instance.ShowRewardedAd("save_slot");
	}

	// Token: 0x060030D2 RID: 12498 RVA: 0x00177AA4 File Offset: 0x00175CA4
	public void hideRewardWindowAndHighlightPower()
	{
		if (ScrollWindow.isCurrentWindow("reward_ads_received"))
		{
			ScrollWindow.get("reward_ads_received").clickHide("right");
			if (PlayerConfig.instance.data.lastReward != string.Empty)
			{
				if (PlayerConfig.instance.data.lastReward.StartsWith("saveslots", StringComparison.Ordinal))
				{
					ScrollWindow.showWindow("saves_list");
				}
				else
				{
					PowerButton tButton = PowerButton.get(PlayerConfig.instance.data.lastReward);
					if (tButton == null)
					{
						return;
					}
					tButton.selectPowerTab(null);
				}
				PlayerConfig.instance.data.lastReward = string.Empty;
			}
		}
	}

	// Token: 0x060030D3 RID: 12499 RVA: 0x00177B51 File Offset: 0x00175D51
	public void clickUnHideUI()
	{
		Config.ui_main_hidden = false;
	}

	// Token: 0x060030D4 RID: 12500 RVA: 0x00177B59 File Offset: 0x00175D59
	public void closeActivePowerBar()
	{
		PowersTab.unselect();
	}

	// Token: 0x060030D5 RID: 12501 RVA: 0x00177B60 File Offset: 0x00175D60
	public void clickOpenMain()
	{
		PowerTabAsset tAsset = PowersTab.getActiveTab().getAsset();
		tAsset.on_main_info_click(tAsset);
	}

	// Token: 0x060030D6 RID: 12502 RVA: 0x00177B84 File Offset: 0x00175D84
	public void clickBackTab()
	{
		if (!SelectedTabsHistory.showPreviousTab())
		{
			PowerTabController.showMainTab();
		}
	}

	// Token: 0x060030D7 RID: 12503 RVA: 0x00177B92 File Offset: 0x00175D92
	public void restorePurchases()
	{
		ButtonEvent.premium_restore_action_pressed++;
		InAppManager.instance.RestorePurchases();
	}

	// Token: 0x060030D8 RID: 12504 RVA: 0x00177BAA File Offset: 0x00175DAA
	public void debugUnlockAll()
	{
		GameProgress.instance.debugUnlockAll();
		PowerButton.checkActorSpawnButtons();
	}

	// Token: 0x060030D9 RID: 12505 RVA: 0x00177BBB File Offset: 0x00175DBB
	public void debugClearAllProgress()
	{
		GameProgress.instance.debugClearAll();
		PowerButton.checkActorSpawnButtons();
	}

	// Token: 0x060030DA RID: 12506 RVA: 0x00177BCC File Offset: 0x00175DCC
	public void debugClearAchievements()
	{
		GameProgress.instance.debugClearAllAchievements();
		PowerButton.checkActorSpawnButtons();
	}

	// Token: 0x060030DB RID: 12507 RVA: 0x00177BDD File Offset: 0x00175DDD
	public void debugUnlockAllAchievements()
	{
		GameProgress.instance.unlockAllAchievements();
		PowerButton.checkActorSpawnButtons();
	}

	// Token: 0x060030DC RID: 12508 RVA: 0x00177BF0 File Offset: 0x00175DF0
	public void debugClearBannedSignals()
	{
		foreach (SignalAsset signalAsset in AssetManager.signals.list)
		{
			signalAsset.unban();
		}
	}

	// Token: 0x040024E3 RID: 9443
	public static int premium_restore_opened;

	// Token: 0x040024E4 RID: 9444
	public static int premium_restore_action_pressed;

	// Token: 0x040024E5 RID: 9445
	public static int premium_more_help_pressed;
}
