using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200047E RID: 1150
public class PowersTab : MonoBehaviour
{
	// Token: 0x0600276D RID: 10093 RVA: 0x0013F500 File Offset: 0x0013D700
	private void Start()
	{
		this.parentObj = base.transform.parent.parent.gameObject;
		this._selected_nano = base.GetComponent<SelectedNanoBase>();
		if (PowerTabController.instance.tab_main == this)
		{
			this.setActive(null);
			PowersTab._main_tab = PowerTabController.instance.tab_main;
		}
		else
		{
			this.hideTab();
		}
		foreach (PowerButton tPowerButton in base.GetComponentsInChildren<PowerButton>())
		{
			if (!(tPowerButton == null) && !(tPowerButton.rect_transform == null))
			{
				this._power_buttons.Add(tPowerButton);
			}
		}
		this.findNeighbours(false);
		this._asset = AssetManager.power_tab_library.get(base.gameObject.name);
		if (this._asset == null)
		{
			Debug.LogError("No Power_Tab_library found for " + base.gameObject.name);
		}
	}

	// Token: 0x0600276E RID: 10094 RVA: 0x0013F5E4 File Offset: 0x0013D7E4
	public void findNeighbours(bool pCheckForActive = false)
	{
		foreach (PowerButton powerButton in this._power_buttons)
		{
			powerButton.up = null;
			powerButton.down = null;
			powerButton.left = null;
			powerButton.right = null;
		}
		foreach (PowerButton powerButton2 in this._power_buttons)
		{
			powerButton2.findNeighbours(this._power_buttons, pCheckForActive);
		}
	}

	// Token: 0x0600276F RID: 10095 RVA: 0x0013F690 File Offset: 0x0013D890
	public void update()
	{
		if (this._selected_nano != null && !ScrollWindow.isWindowActive() && !ScrollWindow.isAnimationActive())
		{
			this._selected_nano.update();
		}
		int tActiveChildren = base.transform.CountChildren((Transform pChild) => pChild.gameObject.activeSelf && !pChild.name.StartsWith("ButtonSelection"));
		if (this._children != tActiveChildren)
		{
			this._children = tActiveChildren;
			this.sortButtons();
			this.setNewWidth();
			if (this._asset != null)
			{
				this._asset.last_scroll_position = 0f;
			}
		}
	}

	// Token: 0x06002770 RID: 10096 RVA: 0x0013F724 File Offset: 0x0013D924
	public PowerTabAsset getAsset()
	{
		return this._asset;
	}

	// Token: 0x06002771 RID: 10097 RVA: 0x0013F72C File Offset: 0x0013D92C
	private void selectButton(PowerButton pButton)
	{
		World.world.selected_buttons.unselectAll();
		if (pButton == null)
		{
			World.world.selected_buttons.clearHighlightedButton();
			return;
		}
		this._last_selected_button = pButton;
		if (pButton.godPower != null && pButton.godPower.activate_on_hotkey_select)
		{
			World.world.selected_buttons.clickPowerButton(pButton);
		}
		else
		{
			World.world.selected_buttons.highlightButton(pButton);
		}
		float tWidth = (float)Screen.width / CanvasMain.instance.canvas_ui.scaleFactor;
		if (pButton.rect_transform.position.x > 0f && pButton.rect_transform.position.x + 32f < (float)Screen.width)
		{
			return;
		}
		float tTarget = -pButton.transform.localPosition.x - 96f + tWidth;
		if (tTarget > 0f)
		{
			tTarget = 0f;
		}
		pButton.transform.parent.parent.parent.transform.DOLocalMoveX(tTarget, 0.25f, false);
	}

	// Token: 0x06002772 RID: 10098 RVA: 0x0013F83C File Offset: 0x0013DA3C
	internal int currentPowerIndex()
	{
		PowerButton tSelectedButton = World.world.selected_buttons.selectedButton;
		if (tSelectedButton != null && tSelectedButton != this._last_selected_button)
		{
			if (this._last_selected_button == null)
			{
				this._last_selected_button = tSelectedButton;
			}
			else if (this._power_buttons.IndexOf(tSelectedButton) >= 0)
			{
				this._last_selected_button = tSelectedButton;
			}
		}
		int tIndex = this._power_buttons.IndexOf(this._last_selected_button);
		if (tIndex < 0)
		{
			tIndex = 0;
		}
		return tIndex;
	}

	// Token: 0x06002773 RID: 10099 RVA: 0x0013F8B8 File Offset: 0x0013DAB8
	internal PowerButton getActiveButton()
	{
		return this._power_buttons[this.currentPowerIndex()];
	}

	// Token: 0x06002774 RID: 10100 RVA: 0x0013F8CC File Offset: 0x0013DACC
	internal void leftButton()
	{
		PowerButton tFindButton = this.getActiveButton();
		while (tFindButton.left != null)
		{
			tFindButton = tFindButton.left;
			if (tFindButton.canSelect() && tFindButton.isActiveAndEnabled)
			{
				break;
			}
		}
		this.selectButton(tFindButton);
	}

	// Token: 0x06002775 RID: 10101 RVA: 0x0013F910 File Offset: 0x0013DB10
	internal void rightButton()
	{
		PowerButton tFindButton = this.getActiveButton();
		while (tFindButton.right != null)
		{
			tFindButton = tFindButton.right;
			if (tFindButton.canSelect() && tFindButton.isActiveAndEnabled)
			{
				break;
			}
		}
		this.selectButton(tFindButton);
	}

	// Token: 0x06002776 RID: 10102 RVA: 0x0013F954 File Offset: 0x0013DB54
	internal void upButton()
	{
		PowerButton tButton = this.getActiveButton();
		if (tButton.up != null && tButton.up.isActiveAndEnabled && tButton.up.canSelect())
		{
			this.selectButton(tButton.up);
			return;
		}
		if (tButton.down != null && tButton.down.isActiveAndEnabled && tButton.down.canSelect())
		{
			this.selectButton(tButton.down);
		}
	}

	// Token: 0x06002777 RID: 10103 RVA: 0x0013F9D4 File Offset: 0x0013DBD4
	internal void downButton()
	{
		PowerButton tButton = this.getActiveButton();
		if (tButton.down != null && tButton.down.isActiveAndEnabled && tButton.down.canSelect())
		{
			this.selectButton(tButton.down);
			return;
		}
		if (tButton.up != null && tButton.up.isActiveAndEnabled && tButton.up.canSelect())
		{
			this.selectButton(tButton.up);
		}
	}

	// Token: 0x06002778 RID: 10104 RVA: 0x0013FA51 File Offset: 0x0013DC51
	public static void showTabFromButton(Button pButtonTab, bool pHideTooltips = false)
	{
		pButtonTab.onClick.Invoke();
		if (pHideTooltips)
		{
			Tooltip.hideTooltipNow();
		}
	}

	// Token: 0x06002779 RID: 10105 RVA: 0x0013FA66 File Offset: 0x0013DC66
	public static PowersTab getActiveTab()
	{
		if (!PowersTab.isTabSelected())
		{
			return PowersTab._main_tab;
		}
		return PowersTab._current_tab;
	}

	// Token: 0x0600277A RID: 10106 RVA: 0x0013FA7A File Offset: 0x0013DC7A
	public static bool isTabSelected()
	{
		return PowersTab._current_tab != null;
	}

	// Token: 0x0600277B RID: 10107 RVA: 0x0013FA87 File Offset: 0x0013DC87
	public static void unselect()
	{
		PowersTab current_tab = PowersTab._current_tab;
		if (current_tab != null)
		{
			current_tab.hideTab();
		}
		PowersTab._main_tab.setActive(null);
		Tooltip.hideTooltip();
	}

	// Token: 0x0600277C RID: 10108 RVA: 0x0013FAA9 File Offset: 0x0013DCA9
	public bool isCurrentPowerTabSelected()
	{
		return PowersTab._current_tab == this;
	}

	// Token: 0x0600277D RID: 10109 RVA: 0x0013FAB6 File Offset: 0x0013DCB6
	public void tryToShowTab()
	{
		if (this.isCurrentPowerTabSelected())
		{
			return;
		}
		this.showTab(null);
	}

	// Token: 0x0600277E RID: 10110 RVA: 0x0013FAC8 File Offset: 0x0013DCC8
	public void showTab(Button pTabButton)
	{
		bool tSame = false;
		if (PowersTab._current_tab != null)
		{
			if (this.isCurrentPowerTabSelected())
			{
				tSame = true;
			}
			PowersTab._current_tab.hideTab();
			PowersTab._current_tab = null;
		}
		if (tSame)
		{
			PowersTab._main_tab.setActive(null);
			return;
		}
		PowerTabController.instance.tab_main.hideTab();
		PowersTab._current_tab = this;
		PowersTab._current_tab_button = pTabButton;
		this.setActive(pTabButton);
		if (pTabButton != null)
		{
			string tTipKey = pTabButton.gameObject.GetComponent<TipButton>().textOnClick;
			WorldTip.instance.showToolbarText(LocalizedTextManager.getText(tTipKey, null, false));
		}
		MusicBox.playSoundUI("event:/SFX/UI/ThumbnailsSlide");
	}

	// Token: 0x0600277F RID: 10111 RVA: 0x0013FB68 File Offset: 0x0013DD68
	private void setActive(Button pTabButton = null)
	{
		if (pTabButton != null)
		{
			pTabButton.image.sprite = this.image_selected;
		}
		base.gameObject.SetActive(true);
		base.gameObject.transform.localPosition = new Vector3(0f, -16f);
		this.setNewWidth();
		if (this._asset != null)
		{
			PowerTabController.loadScrollPosition(this._asset.last_scroll_position);
			if (this._asset.tab_type_main)
			{
				SelectedTabsHistory.clear();
			}
		}
		TabCenterer tCentered = base.GetComponent<TabCenterer>();
		tCentered == null;
		base.gameObject.transform.localScale = new Vector3(0.2f, 0.9f, 0.9f);
		base.gameObject.transform.DOScale(1f, PowersTab.scale_time).SetEase(Ease.OutCubic);
		if (this._asset != null && this._asset.tab_type_main)
		{
			foreach (PowerTabAsset tAsset in AssetManager.power_tab_library.list)
			{
				PowerTabAction on_main_tab_select = tAsset.on_main_tab_select;
				if (on_main_tab_select != null)
				{
					on_main_tab_select(tAsset);
				}
			}
		}
	}

	// Token: 0x06002780 RID: 10112 RVA: 0x0013FCAC File Offset: 0x0013DEAC
	private bool setNewWidth()
	{
		int tChildren = base.transform.childCount;
		RectTransform tRectTransform = this.parentObj.GetComponent<RectTransform>();
		float tOldWidth = tRectTransform.sizeDelta.x;
		float tMaxX = 0f;
		for (int i = 0; i < tChildren; i++)
		{
			GameObject tChild = base.transform.GetChild(i).gameObject;
			if (tChild.activeSelf)
			{
				RectTransform tChildRect = tChild.GetComponent<RectTransform>();
				float tNewPos = tChildRect.localPosition.x + tChildRect.sizeDelta.x + tChildRect.rect.x;
				if (tNewPos > tMaxX)
				{
					tMaxX = tNewPos;
				}
			}
		}
		tRectTransform.sizeDelta = new Vector2(tMaxX + 5f, tRectTransform.sizeDelta.y);
		PowerTabController.instance.resetToStartScrollPosition();
		return tOldWidth != tMaxX;
	}

	// Token: 0x06002781 RID: 10113 RVA: 0x0013FD83 File Offset: 0x0013DF83
	public bool recalc()
	{
		return this.setNewWidth();
	}

	// Token: 0x06002782 RID: 10114 RVA: 0x0013FD8B File Offset: 0x0013DF8B
	public void hideTab()
	{
		this.saveScrollPosition();
		this.completeHide();
		PowersTab._current_tab = null;
		if (PowersTab._current_tab_button != null)
		{
			PowersTab._current_tab_button.image.sprite = this.image_normal;
			PowersTab._current_tab_button = null;
		}
	}

	// Token: 0x06002783 RID: 10115 RVA: 0x0013FDC7 File Offset: 0x0013DFC7
	private void saveScrollPosition()
	{
		if (this._asset == null)
		{
			return;
		}
		this._asset.last_scroll_position = PowerTabController.currentScrollPosition();
	}

	// Token: 0x06002784 RID: 10116 RVA: 0x0013FDE2 File Offset: 0x0013DFE2
	private void completeHide()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x06002785 RID: 10117 RVA: 0x0013FDF0 File Offset: 0x0013DFF0
	private void OnDisable()
	{
		if (Config.isDraggingItem())
		{
			Config.getDraggingObject().KillDrag();
		}
	}

	// Token: 0x06002786 RID: 10118 RVA: 0x0013FE03 File Offset: 0x0013E003
	private void prepareButtonPosition(RectTransform pRect)
	{
		if (pRect == null)
		{
			return;
		}
		pRect.SetAnchor(AnchorPresets.TopLeft, 0f, 0f);
		pRect.SetPivot(PivotPresets.TopLeft, false);
	}

	// Token: 0x06002787 RID: 10119 RVA: 0x0013FE28 File Offset: 0x0013E028
	private void restoreButtonPosition(RectTransform pRect)
	{
		if (pRect == null)
		{
			return;
		}
		pRect.SetPivot(PivotPresets.MiddleCenter, true);
	}

	// Token: 0x06002788 RID: 10120 RVA: 0x0013FE3C File Offset: 0x0013E03C
	public void sortButtons()
	{
		Transform tParent = base.gameObject.transform;
		float tStepHalf = 9.6f;
		int iX = 0;
		int tButtons = 0;
		float tCurOffset = 20f;
		for (int i = 0; i < tParent.childCount; i++)
		{
			GameObject tChild = tParent.GetChild(i).gameObject;
			tChild.transform.DOKill(true);
			if (tChild.activeSelf)
			{
				RectTransform tRect;
				tChild.TryGetComponent<RectTransform>(out tRect);
				if (tChild.name.StartsWith("_space_half"))
				{
					if (tButtons > 0)
					{
						tCurOffset += 36f;
						tButtons = 0;
					}
					tCurOffset += tStepHalf;
				}
				else if (tChild.name.StartsWith("_line") || tChild.name.StartsWith("element_"))
				{
					if (tButtons > 0)
					{
						tCurOffset += 36f;
						tButtons = 0;
					}
					float tYPosition = 32f;
					if (tChild.name.StartsWith("_line"))
					{
						tYPosition = 37.2f;
					}
					this.prepareButtonPosition(tRect);
					tChild.transform.localPosition = new Vector3(tCurOffset, tYPosition, 0f);
					tCurOffset += ((RectTransform)tChild.transform).rect.width;
					tCurOffset += 4f;
					this.restoreButtonPosition(tRect);
				}
				else
				{
					PowerButton tButton;
					tChild.TryGetComponent<PowerButton>(out tButton);
					if (tChild.name.Contains("_space") || (!(tButton == null) && tButton.isActiveAndEnabled))
					{
						bool tXIncreased = false;
						int iY;
						if (tButtons % 2 == 0)
						{
							iX++;
							iY = 0;
							tXIncreased = (tButtons > 0);
						}
						else
						{
							iY = 1;
						}
						tButtons++;
						if (tChild.name.StartsWith("_space"))
						{
							if (tXIncreased)
							{
								tCurOffset += 36f;
							}
						}
						else
						{
							if (tXIncreased)
							{
								tCurOffset += 36f;
							}
							if (!(tButton == null) && tButton.isActiveAndEnabled)
							{
								float yOffset;
								if (iY == 0)
								{
									yOffset = 32f;
								}
								else
								{
									yOffset = -4f;
								}
								if (tChild.name.Contains("tab_back_button"))
								{
									yOffset = 32f;
								}
								this.prepareButtonPosition(tRect);
								tButton.transform.localPosition = new Vector3(tCurOffset, yOffset, 0f);
								this.restoreButtonPosition(tRect);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x04001DB1 RID: 7601
	private const float END_SPACE = 5f;

	// Token: 0x04001DB2 RID: 7602
	private static PowersTab _current_tab;

	// Token: 0x04001DB3 RID: 7603
	private static Button _current_tab_button;

	// Token: 0x04001DB4 RID: 7604
	private static PowersTab _main_tab;

	// Token: 0x04001DB5 RID: 7605
	private GameObject parentObj;

	// Token: 0x04001DB6 RID: 7606
	public Sprite image_normal;

	// Token: 0x04001DB7 RID: 7607
	public Sprite image_selected;

	// Token: 0x04001DB8 RID: 7608
	public Button powerButton;

	// Token: 0x04001DB9 RID: 7609
	public static float scale_time = 0.2f;

	// Token: 0x04001DBA RID: 7610
	public static float buttonScaleTime = 0.1f;

	// Token: 0x04001DBB RID: 7611
	private List<PowerButton> _power_buttons = new List<PowerButton>();

	// Token: 0x04001DBC RID: 7612
	private PowerButton _last_selected_button;

	// Token: 0x04001DBD RID: 7613
	private PowerTabAsset _asset;

	// Token: 0x04001DBE RID: 7614
	private SelectedNanoBase _selected_nano;

	// Token: 0x04001DBF RID: 7615
	private int _children;

	// Token: 0x04001DC0 RID: 7616
	public const int BACK_BUTTON_Y = -2;

	// Token: 0x04001DC1 RID: 7617
	public const int TOP_BUTTON_Y = 16;

	// Token: 0x04001DC2 RID: 7618
	private const int Y_TOP_ITEM = 32;

	// Token: 0x04001DC3 RID: 7619
	private const int Y_BOTTOM_ITEM = -4;

	// Token: 0x04001DC4 RID: 7620
	private const float Y_LINE_ITEM = 37.2f;

	// Token: 0x04001DC5 RID: 7621
	private const int SIDE_SPACING = 2;

	// Token: 0x04001DC6 RID: 7622
	private const int BUTTON_WIDTH = 32;
}
