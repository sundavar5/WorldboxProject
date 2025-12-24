using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000838 RID: 2104
public class PowerButton : MonoBehaviour, IPointerClickHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler, IScrollHandler
{
	// Token: 0x170003B7 RID: 951
	// (get) Token: 0x0600417F RID: 16767 RVA: 0x001BD4B0 File Offset: 0x001BB6B0
	private PowerButtonSelector _selected_buttons
	{
		get
		{
			return PowerButtonSelector.instance;
		}
	}

	// Token: 0x06004180 RID: 16768 RVA: 0x001BD4B8 File Offset: 0x001BB6B8
	public static PowerButton get(string pID)
	{
		for (int i = 0; i < PowerButton.power_buttons.Count; i++)
		{
			PowerButton tButton = PowerButton.power_buttons[i];
			if (tButton.gameObject.name == pID)
			{
				return tButton;
			}
		}
		return null;
	}

	// Token: 0x06004181 RID: 16769 RVA: 0x001BD4FC File Offset: 0x001BB6FC
	private void init()
	{
		this.rect_transform = base.GetComponent<RectTransform>();
		this._image = base.GetComponent<Image>();
		this._button = base.GetComponent<Button>();
		if (this.type == PowerButtonType.Special || this.type == PowerButtonType.Active)
		{
			PowerButton.power_buttons.Add(this);
			this.godPower = AssetManager.powers.get(base.gameObject.name);
		}
		else if (this.type == PowerButtonType.Shop)
		{
			this.godPower = AssetManager.powers.get(base.gameObject.name);
		}
		if (this.godPower != null)
		{
			if (this.godPower.disabled_on_mobile && Config.isMobile)
			{
				base.gameObject.SetActive(false);
				return;
			}
			if (this.type == PowerButtonType.Active)
			{
				GodPower.addPower(this.godPower, this);
			}
			if (this.godPower.toggle_action != null)
			{
				PowerButton.toggle_buttons.Add(this);
			}
			if (this.isActorSpawn())
			{
				ActorAsset tAsset = this.godPower.getActorAsset();
				if (!tAsset.isAvailable())
				{
					this.icon.color = Toolbox.color_black;
				}
				PowerButton.actor_spawn_buttons.TryAdd(tAsset, this);
			}
		}
	}

	// Token: 0x06004182 RID: 16770 RVA: 0x001BD620 File Offset: 0x001BB820
	private void OnEnable()
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		this.init();
		this._default_scale = base.transform.localScale;
		this._clicked_scale = this._default_scale * 0.9f;
		if (this.type == PowerButtonType.Active && this.godPower != null)
		{
			if (base.gameObject.name.Contains("Button"))
			{
				Color tColor = new Color(0.5f, 0.5f, 0.5f, 1f);
				this._image.color = tColor;
				this.icon.color = tColor;
			}
			else
			{
				this.godPower.id = base.gameObject.transform.name;
			}
		}
		if (!this.HasComponent<TipButton>())
		{
			this._button.OnHover(new UnityAction(this.showTooltip));
			this._button.OnHoverOut(new UnityAction(Tooltip.hideTooltip));
		}
	}

	// Token: 0x06004183 RID: 16771 RVA: 0x001BD718 File Offset: 0x001BB918
	public void OnPointerClick(PointerEventData pEventData)
	{
		if (this.draggingBarEnabled() && ScrollRectExtended.isAnyDragged())
		{
			return;
		}
		if (!InputHelpers.mouseSupported && !Tooltip.isShowingFor(this) && (this.type == PowerButtonType.Active || this.type == PowerButtonType.Special) && PowerButtonSelector.instance.selectedButton != this)
		{
			this.showTooltip();
		}
		this.clickButton();
	}

	// Token: 0x06004184 RID: 16772 RVA: 0x001BD774 File Offset: 0x001BB974
	internal void clickButton()
	{
		this.newClickAnimation();
		this.playSound();
		if ((this.type == PowerButtonType.Active || this.type == PowerButtonType.Library || this.type == PowerButtonType.Special) && (this.godPower == null || this.godPower.track_activity))
		{
			PowerTracker.trackPower(this.getText());
		}
		if (this.type == PowerButtonType.Active)
		{
			this.clickActivePower();
		}
		if (this.type == PowerButtonType.BrushSizeMain)
		{
			this.clickSizeMainTool();
		}
		if (this.type == PowerButtonType.TimeScale)
		{
			this.clickTimeScaleTool();
		}
		if (this.type == PowerButtonType.Shop)
		{
			this.clickShop();
		}
		if (this.type == PowerButtonType.Special)
		{
			this.clickSpecial();
		}
		if (this.type == PowerButtonType.Window)
		{
			this.clickOpenWindow();
		}
	}

	// Token: 0x06004185 RID: 16773 RVA: 0x001BD824 File Offset: 0x001BBA24
	private void showTooltip()
	{
		if (InputHelpers.mouseSupported && !Config.tooltips_active)
		{
			return;
		}
		if (this.godPower != null)
		{
			TooltipData tData = new TooltipData();
			if ((Config.isComputer || Config.isEditor) && this.godPower.multiple_spawn_tip && this.type != PowerButtonType.Shop)
			{
				tData.tip_description_2 = "hotkey_many_mod";
			}
			tData.tip_name = this.godPower.getLocaleID();
			tData.tip_description = this.godPower.getDescriptionID();
			PowerActionType powerActionType = this.godPower.type;
			if (powerActionType != PowerActionType.PowerSpawnActor)
			{
				if (powerActionType != PowerActionType.PowerSpawnSeeds)
				{
					Tooltip.show(this, "normal", tData);
				}
				else
				{
					tData.power = this.godPower;
					Tooltip.show(this, "biome_seed", tData);
				}
			}
			else
			{
				tData.power = this.godPower;
				Tooltip.show(this, "unit_spawn", tData);
			}
		}
		else
		{
			string tText = this.getText();
			string tDescription = this.getDescription();
			if (tText == "")
			{
				return;
			}
			if (tDescription != "")
			{
				Tooltip.show(this, "normal", new TooltipData
				{
					tip_name = tText,
					tip_description = tDescription
				});
			}
			else
			{
				Tooltip.show(this, "tip", new TooltipData
				{
					tip_name = tText
				});
			}
		}
		base.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
		base.transform.DOKill(false);
		base.transform.DOScale(1f, 0.1f).SetEase(Ease.InBack);
	}

	// Token: 0x06004186 RID: 16774 RVA: 0x001BD9A8 File Offset: 0x001BBBA8
	private string getText()
	{
		if (this.godPower != null)
		{
			return this.godPower.getLocaleID();
		}
		string tButtonName = this._button.name.Underscore();
		if (LocalizedTextManager.stringExists("button_" + tButtonName))
		{
			return "button_" + tButtonName;
		}
		if (LocalizedTextManager.stringExists(tButtonName))
		{
			return tButtonName;
		}
		return "";
	}

	// Token: 0x06004187 RID: 16775 RVA: 0x001BDA08 File Offset: 0x001BBC08
	private string getDescription()
	{
		if (this.godPower != null)
		{
			return this.godPower.getDescriptionID();
		}
		string tButtonName = this._button.name.Underscore();
		if (LocalizedTextManager.stringExists("button_" + tButtonName + "_description"))
		{
			return "button_" + tButtonName + "_description";
		}
		if (LocalizedTextManager.stringExists(tButtonName + "_description"))
		{
			return tButtonName + "_description";
		}
		return "";
	}

	// Token: 0x06004188 RID: 16776 RVA: 0x001BDA88 File Offset: 0x001BBC88
	private void clickOpenWindow()
	{
		if (this.open_window_id == "steam" && (Config.isComputer || Config.isEditor))
		{
			this.open_window_id = "steam_workshop_main";
		}
		if (ScrollWindow.isAnimationActive() && !ScrollWindow.isCurrentWindow(this.open_window_id))
		{
			ScrollWindow.finishAnimations();
		}
		PowerButtonSelector.instance.clearHighlightedButton();
		this.showWindow(this.open_window_id, this.block_same_window);
	}

	// Token: 0x06004189 RID: 16777 RVA: 0x001BDAF8 File Offset: 0x001BBCF8
	internal void clickSpecial()
	{
		Analytics.LogEvent("select_power", "powerID", this.godPower.id);
		if (this.godPower.id == "pause")
		{
			base.GetComponent<PauseButton>().press();
		}
		if (this.godPower.toggle_action != null)
		{
			this.godPower.toggle_action(this.godPower.id);
			PowerButtonSelector.instance.unselectAll();
			PowerButtonSelector.instance.checkToggleIcons();
		}
	}

	// Token: 0x0600418A RID: 16778 RVA: 0x001BDB80 File Offset: 0x001BBD80
	public void checkToggleIcon()
	{
		GodPower tPower = this.godPower;
		if (tPower == null)
		{
			return;
		}
		if (string.IsNullOrEmpty(tPower.toggle_name))
		{
			return;
		}
		OptionAsset tOptionAsset = this.godPower.option_asset;
		bool tActive = tOptionAsset.isActive();
		bool tZoneKingdom = tActive;
		bool tZoneCity = tActive;
		bool tZoneFluid = tActive;
		ToggleIcon tIconToggle0 = null;
		ToggleIcon tIconToggle = null;
		ToggleIcon tIconToggle2 = null;
		Transform transform = base.transform.Find("ToggleIcon");
		ToggleIcon tIconNormal = (transform != null) ? transform.GetComponent<ToggleIcon>() : null;
		if (tPower.multi_toggle)
		{
			int tIntValue = tOptionAsset.current_int_value;
			Transform transform2 = base.transform.Find("toggle_0");
			tIconToggle0 = ((transform2 != null) ? transform2.GetComponent<ToggleIcon>() : null);
			Transform transform3 = base.transform.Find("toggle_1");
			tIconToggle = ((transform3 != null) ? transform3.GetComponent<ToggleIcon>() : null);
			Transform transform4 = base.transform.Find("toggle_2");
			tIconToggle2 = ((transform4 != null) ? transform4.GetComponent<ToggleIcon>() : null);
			int max_value = tOptionAsset.max_value;
			if (max_value != 1)
			{
				if (max_value == 2)
				{
					tZoneKingdom = (tIntValue == 0);
					tZoneCity = (tIntValue == 1);
					tZoneFluid = (tIntValue == 2);
				}
				else
				{
					tZoneKingdom = tActive;
					tZoneCity = tActive;
					tZoneFluid = tActive;
				}
			}
			else
			{
				tZoneKingdom = (tIntValue == 0);
				tZoneFluid = (tIntValue == 1);
			}
		}
		if (tIconNormal != null)
		{
			tIconNormal.updateIcon(tActive);
		}
		if (tPower.multi_toggle)
		{
			if (tIconToggle0 != null)
			{
				tIconToggle0.updateIconMultiToggle(tActive, tZoneFluid);
			}
			if (tIconToggle != null)
			{
				tIconToggle.updateIconMultiToggle(tActive, tZoneKingdom);
			}
			if (tIconToggle2 != null)
			{
				tIconToggle2.updateIconMultiToggle(tActive, tZoneCity);
			}
		}
	}

	// Token: 0x0600418B RID: 16779 RVA: 0x001BDCD5 File Offset: 0x001BBED5
	private void playSound()
	{
		SoundBox.click();
	}

	// Token: 0x0600418C RID: 16780 RVA: 0x001BDCDC File Offset: 0x001BBEDC
	public void checkLockIcon()
	{
		if (this.type == PowerButtonType.Shop)
		{
			return;
		}
		if (this.godPower != null && this.godPower.requires_premium)
		{
			if (this._icon_lock == null)
			{
				this._icon_lock = Object.Instantiate<Image>(PrefabLibrary.instance.iconLock, base.transform);
				this._icon_lock.enabled = true;
			}
			if (this.buttonUnlocked == null)
			{
				this.buttonUnlocked = Object.Instantiate<GameObject>(PowerButtonSelector.instance.buttonUnlockedFlashNew, base.transform);
				this.buttonUnlocked.transform.position = base.transform.position;
				this.buttonUnlocked.SetActive(false);
				this.buttonUnlocked.transform.SetSiblingIndex(0);
				this.buttonUnlocked.GetComponent<RectTransform>().pivot = base.GetComponent<RectTransform>().pivot;
			}
			if (this.buttonUnlockedFlash == null)
			{
				this.buttonUnlockedFlash = Object.Instantiate<GameObject>(PowerButtonSelector.instance.buttonUnlockedFlash, base.transform);
				this.buttonUnlockedFlash.transform.position = base.transform.position;
				this.buttonUnlockedFlash.SetActive(false);
				this.buttonUnlockedFlash.transform.SetSiblingIndex(0);
				this.buttonUnlockedFlash.GetComponent<RectTransform>().pivot = base.GetComponent<RectTransform>().pivot;
			}
		}
		if (this._icon_lock == null)
		{
			return;
		}
		if (this.godPower == null)
		{
			this._icon_lock.enabled = false;
			return;
		}
		if (Config.hasPremium)
		{
			this._icon_lock.enabled = false;
			return;
		}
		this._icon_lock.enabled = true;
	}

	// Token: 0x0600418D RID: 16781 RVA: 0x001BDE7F File Offset: 0x001BC07F
	public void showOthers()
	{
		Debug.Log("other");
	}

	// Token: 0x0600418E RID: 16782 RVA: 0x001BDE8B File Offset: 0x001BC08B
	public bool isSelected()
	{
		return this._selected_buttons.isPowerSelected(this);
	}

	// Token: 0x0600418F RID: 16783 RVA: 0x001BDE99 File Offset: 0x001BC099
	public void cancelSelection()
	{
		this._selected_buttons.unselectAll();
	}

	// Token: 0x06004190 RID: 16784 RVA: 0x001BDEA8 File Offset: 0x001BC0A8
	public void unselectActivePower()
	{
		if (base.gameObject.activeInHierarchy)
		{
			base.StartCoroutine(this.angleToZero());
			return;
		}
		this.icon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
	}

	// Token: 0x06004191 RID: 16785 RVA: 0x001BDEF4 File Offset: 0x001BC0F4
	public void hideSizes()
	{
		this.sizeButtons.SetActive(false);
	}

	// Token: 0x06004192 RID: 16786 RVA: 0x001BDF02 File Offset: 0x001BC102
	private void clickSizeMainTool()
	{
		this.sizeButtons.SetActive(!this.sizeButtons.activeSelf);
	}

	// Token: 0x06004193 RID: 16787 RVA: 0x001BDF1D File Offset: 0x001BC11D
	public void clickTimeScaleTool()
	{
		this.sizeButtons.SetActive(false);
		Config.setWorldSpeed(base.transform.name, true);
		this.mainSizeButton.newClickAnimation();
		World.world.player_control.inspect_timer_click = 1f;
	}

	// Token: 0x06004194 RID: 16788 RVA: 0x001BDF5B File Offset: 0x001BC15B
	private void clickActivePower()
	{
		this._selected_buttons.clickPowerButton(this);
	}

	// Token: 0x06004195 RID: 16789 RVA: 0x001BDF69 File Offset: 0x001BC169
	public bool canSelect()
	{
		return this.is_selectable && (!this.isActorSpawn() || this.godPower.getActorAsset().isAvailable());
	}

	// Token: 0x06004196 RID: 16790 RVA: 0x001BDF94 File Offset: 0x001BC194
	private void clickShop()
	{
		WorldTip.showNow(LocalizedTextManager.getText(this.godPower.getLocaleID(), null, false) + "\n" + LocalizedTextManager.getText(this.godPower.getDescriptionID(), null, false), false, "top", 3f, "#F3961F");
	}

	// Token: 0x06004197 RID: 16791 RVA: 0x001BDFE4 File Offset: 0x001BC1E4
	public void setSelectedPower(PowerButton pLibraryButton, bool pAnim = false)
	{
		this.godPower = pLibraryButton.godPower;
	}

	// Token: 0x06004198 RID: 16792 RVA: 0x001BDFF2 File Offset: 0x001BC1F2
	public void newClickAnimation()
	{
		base.gameObject.transform.localScale = this._clicked_scale;
		base.gameObject.transform.DOScale(this._default_scale, 0.1f).SetEase(Ease.InOutBack);
	}

	// Token: 0x06004199 RID: 16793 RVA: 0x001BE02D File Offset: 0x001BC22D
	protected IEnumerator angleToZero()
	{
		while (this.icon.transform.localEulerAngles.z != 0f)
		{
			Vector3 tVec = this.icon.transform.localEulerAngles;
			tVec.z -= 100f * Time.deltaTime;
			if (tVec.z < 0f)
			{
				tVec.z = 0f;
			}
			this.icon.transform.localEulerAngles = tVec;
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600419A RID: 16794 RVA: 0x001BE03C File Offset: 0x001BC23C
	public void animate(float pElapsed)
	{
		if (this.icon.transform.localEulerAngles.z < 20f)
		{
			Vector3 tVec = this.icon.transform.localEulerAngles;
			tVec.z += 100f * pElapsed;
			if (tVec.z > 20f)
			{
				tVec.z = 20f;
			}
			this.icon.transform.localEulerAngles = tVec;
		}
	}

	// Token: 0x0600419B RID: 16795 RVA: 0x001BE0B4 File Offset: 0x001BC2B4
	public void destroyLockIcon()
	{
		if (this.buttonUnlocked != null)
		{
			Object.Destroy(this.buttonUnlocked);
		}
		if (this.buttonUnlockedFlash != null)
		{
			Object.Destroy(this.buttonUnlockedFlash);
		}
		if (this._icon_lock != null)
		{
			Object.Destroy(this._icon_lock.gameObject);
			return;
		}
		Transform tLockObject = base.transform.Find("IconLock");
		if (tLockObject != null)
		{
			Object.Destroy(tLockObject.gameObject);
			return;
		}
		tLockObject = base.transform.Find("IconLock(Clone)");
		if (tLockObject != null)
		{
			Object.Destroy(tLockObject.gameObject);
		}
	}

	// Token: 0x0600419C RID: 16796 RVA: 0x001BE15F File Offset: 0x001BC35F
	public void showWindow(string pID, bool pBlockSame)
	{
		if (ScrollWindow.isAnimationActive())
		{
			return;
		}
		ScrollWindow.showWindow(pID, false, pBlockSame);
	}

	// Token: 0x0600419D RID: 16797 RVA: 0x001BE171 File Offset: 0x001BC371
	public void showWindow(string pID)
	{
		ScrollWindow.showWindow(pID, false, false);
	}

	// Token: 0x0600419E RID: 16798 RVA: 0x001BE17C File Offset: 0x001BC37C
	public void selectPowerTab(TweenCallback pOnComplete = null)
	{
		if (!base.transform.parent.gameObject.activeInHierarchy)
		{
			Button tButton = PowerTabController.instance.getTabForTabGroup(base.transform.parent.name);
			if (tButton != null)
			{
				PowersTab.showTabFromButton(tButton, false);
			}
		}
		RectTransform tContent = base.transform.parent.parent.parent as RectTransform;
		float tContentWidth = tContent.rect.width;
		float tWidth = (float)Screen.width / CanvasMain.instance.canvas_ui.scaleFactor;
		float tTarget = -base.transform.localPosition.x + 32f + tWidth / 2f;
		float tMinX = 0f;
		if (tContentWidth > tWidth)
		{
			tMinX = -1f * (tContentWidth - tWidth);
		}
		tTarget = Mathf.Clamp(tTarget, tMinX, 0f);
		ScrollRectExtended tScrollRect = tContent.GetComponentInParent<ScrollRectExtended>();
		tScrollRect.movementType = ScrollRectExtended.MovementType.Clamped;
		if (pOnComplete == null)
		{
			tContent.DOLocalMoveX(tTarget, 1.5f, false).SetEase(Ease.OutBack).SetDelay(0.3f).OnComplete(delegate
			{
				tScrollRect.StopMovement();
				tScrollRect.movementType = ScrollRectExtended.MovementType.Elastic;
			});
			return;
		}
		tContent.DOLocalMoveX(tTarget, 0.125f, false).SetEase(Ease.OutBack).OnComplete(delegate
		{
			tScrollRect.StopMovement();
			tScrollRect.movementType = ScrollRectExtended.MovementType.Elastic;
			pOnComplete();
		});
	}

	// Token: 0x0600419F RID: 16799 RVA: 0x001BE2E0 File Offset: 0x001BC4E0
	internal void findNeighbours(List<PowerButton> pButtons, bool pCheckForActive = false)
	{
		Vector2 thisPosition = this.rect_transform.anchoredPosition;
		if (thisPosition.y == -2f)
		{
			thisPosition.y = 16f;
		}
		foreach (PowerButton tPowerButton in pButtons)
		{
			if (!(tPowerButton == this) && (!pCheckForActive || tPowerButton.gameObject.activeSelf))
			{
				Vector2 tPosition = tPowerButton.rect_transform.anchoredPosition;
				if (tPosition.y == -2f)
				{
					tPosition.y = 16f;
				}
				if (tPosition.y == thisPosition.y)
				{
					if (tPosition.x < thisPosition.x)
					{
						if (this.left == null)
						{
							this.left = tPowerButton;
						}
						else if (this.left.rect_transform.anchoredPosition.x < tPosition.x)
						{
							this.left = tPowerButton;
						}
					}
					if (tPosition.x > thisPosition.x)
					{
						if (this.right == null)
						{
							this.right = tPowerButton;
						}
						else if (this.right.rect_transform.anchoredPosition.x > tPosition.x)
						{
							this.right = tPowerButton;
						}
					}
				}
				if (tPosition.x == thisPosition.x)
				{
					if (tPosition.y < thisPosition.y)
					{
						if (this.down == null)
						{
							this.down = tPowerButton;
						}
						else if (this.down.rect_transform.anchoredPosition.y < tPosition.y)
						{
							this.down = tPowerButton;
						}
					}
					if (tPosition.y > thisPosition.y)
					{
						if (this.up == null)
						{
							this.up = tPowerButton;
						}
						else if (this.up.rect_transform.anchoredPosition.y > tPosition.y)
						{
							this.up = tPowerButton;
						}
					}
				}
			}
		}
		if (this.left == null)
		{
			foreach (PowerButton tPowerButton2 in pButtons)
			{
				if (!(tPowerButton2 == this) && (!pCheckForActive || tPowerButton2.gameObject.activeSelf))
				{
					Vector2 tPosition2 = tPowerButton2.rect_transform.anchoredPosition;
					if (tPosition2.y == -2f)
					{
						tPosition2.y = 16f;
					}
					if (tPosition2.y == thisPosition.y)
					{
						if (this.left == null)
						{
							this.left = tPowerButton2;
						}
						else if (this.left.rect_transform.anchoredPosition.x < tPosition2.x)
						{
							this.left = tPowerButton2;
						}
					}
				}
			}
		}
		if (this.right == null)
		{
			foreach (PowerButton tPowerButton3 in pButtons)
			{
				if (!(tPowerButton3 == this) && (!pCheckForActive || tPowerButton3.gameObject.activeSelf))
				{
					Vector2 tPosition3 = tPowerButton3.rect_transform.anchoredPosition;
					if (tPosition3.y == -2f)
					{
						tPosition3.y = 16f;
					}
					if (tPosition3.y == thisPosition.y)
					{
						if (this.right == null)
						{
							this.right = tPowerButton3;
						}
						else if (this.right.rect_transform.anchoredPosition.x > tPosition3.x)
						{
							this.right = tPowerButton3;
						}
					}
				}
			}
		}
	}

	// Token: 0x060041A0 RID: 16800 RVA: 0x001BE6C4 File Offset: 0x001BC8C4
	private bool isActorSpawn()
	{
		return this.godPower != null && this.godPower.type == PowerActionType.PowerSpawnActor;
	}

	// Token: 0x060041A1 RID: 16801 RVA: 0x001BE6E0 File Offset: 0x001BC8E0
	public static void checkActorSpawnButtons()
	{
		Color tColorNoPopulation = new Color(0.75f, 0.75f, 0.75f, 0.9f);
		foreach (KeyValuePair<ActorAsset, PowerButton> tPair in PowerButton.actor_spawn_buttons)
		{
			ActorAsset tAsset = tPair.Key;
			PowerButton tButton = tPair.Value;
			if (tAsset.isAvailable())
			{
				if (tAsset.countPopulation() > 0)
				{
					tButton._image.sprite = ToolbarButtons.getSpriteButtonUnitExists();
					tButton.icon.color = Toolbox.color_white;
				}
				else
				{
					tButton._image.sprite = ToolbarButtons.getSpriteButtonNormal();
					tButton.icon.color = tColorNoPopulation;
				}
			}
			else
			{
				tButton.icon.color = Toolbox.color_black;
			}
		}
	}

	// Token: 0x060041A2 RID: 16802 RVA: 0x001BE7C0 File Offset: 0x001BC9C0
	private bool draggingBarEnabled()
	{
		return this.drag_power_bar && InputHelpers.mouseSupported;
	}

	// Token: 0x060041A3 RID: 16803 RVA: 0x001BE7D1 File Offset: 0x001BC9D1
	public void OnBeginDrag(PointerEventData pEventData)
	{
		if (!this.draggingBarEnabled())
		{
			return;
		}
		ScrollRectExtended.SendMessageToAll("OnBeginDrag", pEventData);
	}

	// Token: 0x060041A4 RID: 16804 RVA: 0x001BE7E7 File Offset: 0x001BC9E7
	public void OnDrag(PointerEventData pEventData)
	{
		if (!this.draggingBarEnabled())
		{
			return;
		}
		ScrollRectExtended.SendMessageToAll("OnDrag", pEventData);
	}

	// Token: 0x060041A5 RID: 16805 RVA: 0x001BE7FD File Offset: 0x001BC9FD
	public void OnEndDrag(PointerEventData pEventData)
	{
		if (!this.draggingBarEnabled())
		{
			return;
		}
		ScrollRectExtended.SendMessageToAll("OnEndDrag", pEventData);
	}

	// Token: 0x060041A6 RID: 16806 RVA: 0x001BE813 File Offset: 0x001BCA13
	public void OnInitializePotentialDrag(PointerEventData pEventData)
	{
		if (!this.draggingBarEnabled())
		{
			return;
		}
		ScrollRectExtended.SendMessageToAll("OnInitializePotentialDrag", pEventData);
	}

	// Token: 0x060041A7 RID: 16807 RVA: 0x001BE829 File Offset: 0x001BCA29
	public void OnScroll(PointerEventData pEventData)
	{
		if (!this.draggingBarEnabled())
		{
			return;
		}
		ScrollRectExtended.SendMessageToAll("OnScroll", pEventData);
	}

	// Token: 0x060041A8 RID: 16808 RVA: 0x001BE840 File Offset: 0x001BCA40
	public void OnDestroy()
	{
		base.transform.DOKill(false);
		PowerButton.power_buttons.Remove(this);
		PowerButton.toggle_buttons.Remove(this);
		if (PowerButton.actor_spawn_buttons.ContainsValue(this))
		{
			ActorAsset tAsset = PowerButton.actor_spawn_buttons.FirstOrDefault((KeyValuePair<ActorAsset, PowerButton> x) => x.Value == this).Key;
			PowerButton.actor_spawn_buttons.Remove(tAsset);
		}
	}

	// Token: 0x04002FD6 RID: 12246
	public bool drag_power_bar;

	// Token: 0x04002FD7 RID: 12247
	public string open_window_id = string.Empty;

	// Token: 0x04002FD8 RID: 12248
	public bool block_same_window;

	// Token: 0x04002FD9 RID: 12249
	public Image icon;

	// Token: 0x04002FDA RID: 12250
	private Image _image;

	// Token: 0x04002FDB RID: 12251
	private Button _button;

	// Token: 0x04002FDC RID: 12252
	public PowerButtonType type;

	// Token: 0x04002FDD RID: 12253
	public GameObject sizeButtons;

	// Token: 0x04002FDE RID: 12254
	public PowerButton mainSizeButton;

	// Token: 0x04002FDF RID: 12255
	internal GodPower godPower;

	// Token: 0x04002FE0 RID: 12256
	private Image _icon_lock;

	// Token: 0x04002FE1 RID: 12257
	public GameObject buttonUnlocked;

	// Token: 0x04002FE2 RID: 12258
	public GameObject buttonUnlockedFlash;

	// Token: 0x04002FE3 RID: 12259
	public static List<PowerButton> power_buttons = new List<PowerButton>();

	// Token: 0x04002FE4 RID: 12260
	public static List<PowerButton> toggle_buttons = new List<PowerButton>();

	// Token: 0x04002FE5 RID: 12261
	public static Dictionary<ActorAsset, PowerButton> actor_spawn_buttons = new Dictionary<ActorAsset, PowerButton>();

	// Token: 0x04002FE6 RID: 12262
	internal RectTransform rect_transform;

	// Token: 0x04002FE7 RID: 12263
	internal PowerButton left;

	// Token: 0x04002FE8 RID: 12264
	internal PowerButton right;

	// Token: 0x04002FE9 RID: 12265
	internal PowerButton down;

	// Token: 0x04002FEA RID: 12266
	internal PowerButton up;

	// Token: 0x04002FEB RID: 12267
	private Vector3 _default_scale;

	// Token: 0x04002FEC RID: 12268
	private Vector3 _clicked_scale;

	// Token: 0x04002FED RID: 12269
	[HideInInspector]
	public bool is_selectable = true;

	// Token: 0x04002FEE RID: 12270
	private bool _initialized;
}
