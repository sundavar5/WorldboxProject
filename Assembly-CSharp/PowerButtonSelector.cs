using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000839 RID: 2105
public class PowerButtonSelector : MonoBehaviour
{
	// Token: 0x060041AC RID: 16812 RVA: 0x001BE8F3 File Offset: 0x001BCAF3
	private void Awake()
	{
		PowerButtonSelector.instance = this;
	}

	// Token: 0x060041AD RID: 16813 RVA: 0x001BE8FC File Offset: 0x001BCAFC
	private void Start()
	{
		this.clockButtMover.setVisible(false, true, null);
		this.sizeButtMover.setVisible(false, true, null);
		this.cancelButtMover.setVisible(false, true, null);
		this.cancelUnitSelectedMover.setVisible(false, true, null);
		this.toggleBottomElements(false, true);
		this.spectateUnitMover.setVisible(false, true, null);
		this.resetToolbarTempShow();
	}

	// Token: 0x060041AE RID: 16814 RVA: 0x001BE960 File Offset: 0x001BCB60
	internal void checkToggleIcons()
	{
		Color tColorDisabled = new Color(0.7f, 0.7f, 0.7f, 1f);
		foreach (PowerButton tButton in PowerButton.toggle_buttons)
		{
			tButton.checkToggleIcon();
			if (tButton.godPower.option_asset.isActive())
			{
				tButton.icon.color = Color.white;
			}
			else
			{
				tButton.icon.color = tColorDisabled;
			}
		}
	}

	// Token: 0x060041AF RID: 16815 RVA: 0x001BE9FC File Offset: 0x001BCBFC
	public virtual void setSelectedPower(PowerButton pButton, GodPower pPower, bool pAnim = false)
	{
		if (this.selectedButton == null)
		{
			return;
		}
		GodPower godPower = this.selectedButton.godPower;
		this.selectedButton.setSelectedPower(pButton, false);
		this.selectedButton.newClickAnimation();
	}

	// Token: 0x060041B0 RID: 16816 RVA: 0x001BEA34 File Offset: 0x001BCC34
	public void setPower(PowerButton pButton)
	{
		this.selectedButton = pButton;
		if (this.selectedButton != null && this.selectedButton.godPower != null)
		{
			Config.debug_last_selected_power_button = this.selectedButton.godPower.id;
			if (this.selectedButton.godPower.type == PowerActionType.PowerSpawnActor)
			{
				ActorAsset tActorAsset = this.selectedButton.godPower.getActorAsset();
				if (tActorAsset.has_sound_spawn)
				{
					MusicBox.playSoundUI(tActorAsset.sound_spawn);
				}
			}
		}
		if (this.selectedButton != null)
		{
			this.cancelButton.setIconFrom(this.selectedButton);
			LogText.log("Power Selected", pButton.godPower.id, "");
		}
		if (pButton == null)
		{
			PowerTracker.setPower(null);
			return;
		}
		PowerTracker.setPower(pButton.godPower);
	}

	// Token: 0x060041B1 RID: 16817 RVA: 0x001BEB03 File Offset: 0x001BCD03
	public void unselectTabs()
	{
		if (PowersTab.isTabSelected())
		{
			SelectedObjects.unselectNanoObject();
			PowersTab.unselect();
			SelectedTabsHistory.clear();
		}
	}

	// Token: 0x060041B2 RID: 16818 RVA: 0x001BEB1C File Offset: 0x001BCD1C
	public void unselectAll()
	{
		if (this.selectedButton != null)
		{
			this.selectedButton.unselectActivePower();
			this.setPower(null);
			this.clearHighlightedButton();
			WorldTip.instance.startHide();
		}
		if (ControllableUnit.isControllingUnit())
		{
			ControllableUnit.clear(true);
		}
		if (MoveCamera.hasFocusUnit())
		{
			MoveCamera.clearFocusUnitOnly();
		}
	}

	// Token: 0x060041B3 RID: 16819 RVA: 0x001BEB72 File Offset: 0x001BCD72
	public bool isPowerSelected()
	{
		return this.selectedButton != null;
	}

	// Token: 0x060041B4 RID: 16820 RVA: 0x001BEB80 File Offset: 0x001BCD80
	public bool isPowerSelected(PowerButton pButton)
	{
		return this.selectedButton == pButton;
	}

	// Token: 0x060041B5 RID: 16821 RVA: 0x001BEB90 File Offset: 0x001BCD90
	public void clickPowerButton(PowerButton pButton)
	{
		if (!pButton.canSelect())
		{
			if (!InputHelpers.mouseSupported)
			{
				this.showToolbarText(pButton);
			}
			return;
		}
		if (this.selectedButton == pButton)
		{
			this.unselectAll();
			return;
		}
		if (this.selectedButton != null)
		{
			this.selectedButton.unselectActivePower();
		}
		if (pButton.godPower.select_button_action != null && pButton.godPower.select_button_action(pButton.godPower.id))
		{
			return;
		}
		this.setPower(pButton);
		if (this.selectedButton != null)
		{
			this.highlightButton(this.selectedButton);
			if (this.selectedButton.godPower != null)
			{
				Config.logSelectedPower(this.selectedButton.godPower);
			}
		}
		if (InputHelpers.mouseSupported)
		{
			this.showToolbarText(pButton);
		}
		Analytics.LogEvent("select_power", "powerID", pButton.godPower.id);
	}

	// Token: 0x060041B6 RID: 16822 RVA: 0x001BEC73 File Offset: 0x001BCE73
	public void showToolbarText(PowerButton pButton)
	{
		WorldTip.instance.showToolbarText(pButton.godPower, true);
	}

	// Token: 0x060041B7 RID: 16823 RVA: 0x001BEC88 File Offset: 0x001BCE88
	internal void highlightButton(PowerButton pButton)
	{
		if (pButton == null)
		{
			return;
		}
		this.buttonSelectionSprite.SetActive(true);
		RectTransform rectTransform = (RectTransform)this.buttonSelectionSprite.transform;
		rectTransform.position = pButton.transform.position;
		rectTransform.SetParent(pButton.transform.parent);
		rectTransform.localScale = Vector3.one;
		rectTransform.sizeDelta = pButton.rect_transform.sizeDelta;
	}

	// Token: 0x060041B8 RID: 16824 RVA: 0x001BECF8 File Offset: 0x001BCEF8
	internal void clearHighlightedButton()
	{
		this.buttonSelectionSprite.SetActive(false);
	}

	// Token: 0x060041B9 RID: 16825 RVA: 0x001BED06 File Offset: 0x001BCF06
	private void Update()
	{
		this.updateSelectedPowerButtons();
		this.updateHideUiButton();
		this.updateSelectedUnitCancelButton();
	}

	// Token: 0x060041BA RID: 16826 RVA: 0x001BED1A File Offset: 0x001BCF1A
	private bool isSpecialTabActive()
	{
		return SelectedUnit.isSet() || SelectedObjects.isNanoObjectSet();
	}

	// Token: 0x060041BB RID: 16827 RVA: 0x001BED2F File Offset: 0x001BCF2F
	private void updateSelectedUnitCancelButton()
	{
		if (!World.world.isAnyPowerSelected() && this.isSpecialTabActive() && !ScrollWindow.isWindowActive())
		{
			this.cancelUnitSelectedMover.setVisible(true, false, null);
			return;
		}
		this.cancelUnitSelectedMover.setVisible(false, false, null);
	}

	// Token: 0x060041BC RID: 16828 RVA: 0x001BED69 File Offset: 0x001BCF69
	private void updateHideUiButton()
	{
		if (Config.ui_main_hidden)
		{
			this.unhideButtonMover.setVisible(true, false, null);
			return;
		}
		this.unhideButtonMover.setVisible(false, false, null);
	}

	// Token: 0x060041BD RID: 16829 RVA: 0x001BED90 File Offset: 0x001BCF90
	private void updateSelectedPowerButtons()
	{
		PowerButton tCurrentSelectedPowerButton = this.selectedButton;
		GodPower tCurrentGodPower = (tCurrentSelectedPowerButton != null) ? tCurrentSelectedPowerButton.godPower : null;
		if (tCurrentSelectedPowerButton == null)
		{
			this.cancelButtMover.setVisible(false, false, null);
		}
		else if (ScrollWindow.isWindowActive())
		{
			this.cancelButtMover.setVisible(false, false, null);
		}
		else
		{
			this.cancelButtMover.setVisible(true, false, null);
		}
		bool tInSpectatorMode = MoveCamera.inSpectatorMode();
		if (tCurrentSelectedPowerButton == null || ScrollWindow.isWindowActive() || tInSpectatorMode)
		{
			this.sizeButtMover.setVisible(false, false, null);
			this.sizeButton.hideSizes();
			if (tInSpectatorMode)
			{
				this.clockButtMover.setVisible(true, false, null);
			}
			else
			{
				this.clockButtMover.setVisible(false, false, null);
				this.clockButton.hideSizes();
			}
		}
		else
		{
			tCurrentSelectedPowerButton.animate(Time.deltaTime);
			if (tCurrentGodPower.show_tool_sizes)
			{
				this.sizeButtMover.setVisible(true, false, null);
			}
			else
			{
				this.sizeButtMover.setVisible(false, false, null);
				this.sizeButton.hideSizes();
			}
			if (this.selectedButton.godPower.id == "clock")
			{
				this.clockButtMover.setVisible(true, false, null);
			}
			else
			{
				this.clockButtMover.setVisible(false, false, null);
				this.clockButton.hideSizes();
			}
		}
		if (CanvasMain.isBottomBarShowing())
		{
			this.toggleBottomElements(true, false);
			if (this._is_toolbar_temp_showed)
			{
				this.resetToolbarTempShow();
			}
		}
		else if (!this._is_toolbar_temp_showed)
		{
			this.toggleBottomElements(false, false);
		}
		this.pauseButtonMover.setVisible(tInSpectatorMode, false, null);
		this.spectateUnitMover.setVisible(tInSpectatorMode, false, null);
		this.updateTopButtons();
	}

	// Token: 0x060041BE RID: 16830 RVA: 0x001BEF34 File Offset: 0x001BD134
	private void updateTopButtons()
	{
		if (this.bottomElementsMover.visible)
		{
			this.cancelButton.goDown = false;
			this.cancelButton.goUp = false;
			this.cancelButton.gameObject.SetActive(true);
			PremiumElementsChecker.checkElements();
			bool tEnableDebugButton = DebugConfig.isOn(DebugOption.DebugButton);
			DebugConfig.instance.debugButton.SetActive(tEnableDebugButton);
			this.clockButton.GetComponent<CancelButton>().goDown = false;
			this.joy_control_cancel_button.SetActive(false);
			return;
		}
		if (ControllableUnit.isControllingUnit())
		{
			this.cancelButton.gameObject.SetActive(false);
			PremiumElementsChecker.toggleActive(false);
			DebugConfig.instance.debugButton.SetActive(false);
			this.joy_control_cancel_button.SetActive(true);
			Sprite tSprite = ControllableUnit.getControllableUnit().getActorAsset().getSpriteIcon();
			this._joy_control_cancel_button_icon.sprite = tSprite;
		}
		bool tInSpectatorMode = MoveCamera.inSpectatorMode();
		this.cancelButton.goDown = (tInSpectatorMode || (ControllableUnit.isControllingUnit() && !Config.joyControls));
		this.clockButton.GetComponent<CancelButton>().goDown = tInSpectatorMode;
	}

	// Token: 0x060041BF RID: 16831 RVA: 0x001BF048 File Offset: 0x001BD248
	private void toggleBottomElements(bool pState, bool pNow = false)
	{
		TweenCallback tCompleteCallback = null;
		if (!pState)
		{
			tCompleteCallback = delegate()
			{
				this.buttons.SetActive(false);
				this.resetToolbarCanvasGroup();
			};
		}
		else
		{
			this.buttons.SetActive(true);
		}
		this.bottomElementsMover.setVisible(pState, pNow, tCompleteCallback);
	}

	// Token: 0x060041C0 RID: 16832 RVA: 0x001BF084 File Offset: 0x001BD284
	public void showBarTemporary()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (this._toolbar_hide_routine != null)
		{
			base.StopCoroutine(this._toolbar_hide_routine);
		}
		this._is_toolbar_temp_showed = true;
		this._toolbar_canvas_group.blocksRaycasts = false;
		this._toolbar_canvas_group.alpha = 0.5f;
		this.toggleBottomElements(true, false);
		this._toolbar_hide_routine = base.StartCoroutine(this.toolbarHideRoutine());
	}

	// Token: 0x060041C1 RID: 16833 RVA: 0x001BF0EA File Offset: 0x001BD2EA
	private IEnumerator toolbarHideRoutine()
	{
		yield return new WaitForSeconds(1f);
		this.toggleBottomElements(false, false);
		this._is_toolbar_temp_showed = false;
		yield break;
	}

	// Token: 0x060041C2 RID: 16834 RVA: 0x001BF0F9 File Offset: 0x001BD2F9
	private void resetToolbarCanvasGroup()
	{
		this._toolbar_canvas_group.blocksRaycasts = true;
		this._toolbar_canvas_group.alpha = 1f;
	}

	// Token: 0x060041C3 RID: 16835 RVA: 0x001BF117 File Offset: 0x001BD317
	private void resetToolbarTempShow()
	{
		this.resetToolbarCanvasGroup();
		this._is_toolbar_temp_showed = false;
		if (this._toolbar_hide_routine != null)
		{
			base.StopCoroutine(this._toolbar_hide_routine);
		}
	}

	// Token: 0x04002FEF RID: 12271
	private const float TOOLBAR_TEMP_SHOW_DURATION = 1f;

	// Token: 0x04002FF0 RID: 12272
	private const float TOOLBAR_TEMP_SHOW_ALPHA = 0.5f;

	// Token: 0x04002FF1 RID: 12273
	public GameObject buttonSelectionSprite;

	// Token: 0x04002FF2 RID: 12274
	public GameObject buttonUnlocked;

	// Token: 0x04002FF3 RID: 12275
	public GameObject buttonUnlockedFlash;

	// Token: 0x04002FF4 RID: 12276
	public GameObject buttonUnlockedFlashNew;

	// Token: 0x04002FF5 RID: 12277
	[SerializeField]
	private CanvasGroup _toolbar_canvas_group;

	// Token: 0x04002FF6 RID: 12278
	public UiMover cancelUnitSelectedMover;

	// Token: 0x04002FF7 RID: 12279
	public UiMover cancelButtMover;

	// Token: 0x04002FF8 RID: 12280
	public UiMover sizeButtMover;

	// Token: 0x04002FF9 RID: 12281
	public UiMover clockButtMover;

	// Token: 0x04002FFA RID: 12282
	public UiMover bottomElementsMover;

	// Token: 0x04002FFB RID: 12283
	public UiMover spectateUnitMover;

	// Token: 0x04002FFC RID: 12284
	public UiMover pauseButtonMover;

	// Token: 0x04002FFD RID: 12285
	public UiMover unhideButtonMover;

	// Token: 0x04002FFE RID: 12286
	public PowerButton clockButton;

	// Token: 0x04002FFF RID: 12287
	public PowerButton sizeButton;

	// Token: 0x04003000 RID: 12288
	public CancelButton cancelButton;

	// Token: 0x04003001 RID: 12289
	public PowerButton pauseButton;

	// Token: 0x04003002 RID: 12290
	public PowerButton pauseButton2;

	// Token: 0x04003003 RID: 12291
	public PowerButton cityInfo;

	// Token: 0x04003004 RID: 12292
	public PowerButton cityZones;

	// Token: 0x04003005 RID: 12293
	public PowerButton boatMarks;

	// Token: 0x04003006 RID: 12294
	public PowerButton kingsAndLeaders;

	// Token: 0x04003007 RID: 12295
	public PowerButton historyLog;

	// Token: 0x04003008 RID: 12296
	public PowerButton followUnit;

	// Token: 0x04003009 RID: 12297
	public GameObject joy_control_cancel_button;

	// Token: 0x0400300A RID: 12298
	[SerializeField]
	private Image _joy_control_cancel_button_icon;

	// Token: 0x0400300B RID: 12299
	private Coroutine _toolbar_hide_routine;

	// Token: 0x0400300C RID: 12300
	private bool _is_toolbar_temp_showed;

	// Token: 0x0400300D RID: 12301
	internal PowerButton selectedButton;

	// Token: 0x0400300E RID: 12302
	public GameObject buttons;

	// Token: 0x0400300F RID: 12303
	internal static PowerButtonSelector instance;
}
