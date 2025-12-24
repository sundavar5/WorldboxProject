using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200083E RID: 2110
public class ScrollWindow : MonoBehaviour, IShakable
{
	// Token: 0x060041DD RID: 16861 RVA: 0x001BF35B File Offset: 0x001BD55B
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isWindowActive()
	{
		return ScrollWindow._is_window_active;
	}

	// Token: 0x060041DE RID: 16862 RVA: 0x001BF362 File Offset: 0x001BD562
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static bool isAnimationActive()
	{
		return ScrollWindow._animations_list.Count > 0;
	}

	// Token: 0x170003B8 RID: 952
	// (get) Token: 0x060041DF RID: 16863 RVA: 0x001BF371 File Offset: 0x001BD571
	public float shake_duration { get; } = 0.5f;

	// Token: 0x170003B9 RID: 953
	// (get) Token: 0x060041E0 RID: 16864 RVA: 0x001BF379 File Offset: 0x001BD579
	public float shake_strength { get; } = 8f;

	// Token: 0x170003BA RID: 954
	// (get) Token: 0x060041E1 RID: 16865 RVA: 0x001BF381 File Offset: 0x001BD581
	// (set) Token: 0x060041E2 RID: 16866 RVA: 0x001BF389 File Offset: 0x001BD589
	public Tweener shake_tween { get; set; }

	// Token: 0x060041E3 RID: 16867 RVA: 0x001BF392 File Offset: 0x001BD592
	private void Awake()
	{
		this.init();
	}

	// Token: 0x060041E4 RID: 16868 RVA: 0x001BF39C File Offset: 0x001BD59C
	public void init()
	{
		if (this._initialized)
		{
			return;
		}
		this._asset = AssetManager.window_library.get(this.screen_id);
		this._canvas_group = base.gameObject.GetComponent<CanvasGroup>();
		this._bg_rect = base.gameObject.transform.GetChild(0).GetComponent<RectTransform>();
		if (this.destroyOnAwake != null)
		{
			GameObject[] array = this.destroyOnAwake;
			for (int i = 0; i < array.Length; i++)
			{
				Object.Destroy(array[i]);
			}
		}
		this.initComponents();
	}

	// Token: 0x060041E5 RID: 16869 RVA: 0x001BF420 File Offset: 0x001BD620
	private void OnEnable()
	{
		WorldTip.hideNow();
	}

	// Token: 0x060041E6 RID: 16870 RVA: 0x001BF427 File Offset: 0x001BD627
	private void Start()
	{
		if (this._canvas == null)
		{
			this.create(true);
		}
		this.toggleScrollbar(false);
	}

	// Token: 0x060041E7 RID: 16871 RVA: 0x001BF445 File Offset: 0x001BD645
	private void Update()
	{
		this.updateScrollbar();
		this.updateRightClickBack();
	}

	// Token: 0x060041E8 RID: 16872 RVA: 0x001BF453 File Offset: 0x001BD653
	private void updateRightClickBack()
	{
		if (InputHelpers.GetMouseButtonDown(1) && this.canGoBackWithRightClick())
		{
			WindowHistory.clickBack();
		}
	}

	// Token: 0x060041E9 RID: 16873 RVA: 0x001BF46A File Offset: 0x001BD66A
	private bool canGoBackWithRightClick()
	{
		return this.historyActionEnabled && !Config.isDraggingItem() && InputHelpers.mouseSupported && !World.world.isOverUiButton();
	}

	// Token: 0x060041EA RID: 16874 RVA: 0x001BF498 File Offset: 0x001BD698
	private void updateScrollbar()
	{
		Scrollbar tBar = this.scrollRect.verticalScrollbar;
		bool tCurrentState = !Mathf.Approximately(tBar.size, 1f);
		if (this._scrollbar_cached_state == tCurrentState)
		{
			return;
		}
		this._scrollbar_cached_state = tCurrentState;
		this.toggleScrollbar(true);
		this.checkGradient();
		this._scrollbar_tweener_color.Kill(false);
		Color tColor;
		if (this._scrollbar_cached_state)
		{
			tColor = Toolbox.makeColor("#E75340");
		}
		else
		{
			tColor = Toolbox.makeColor("#545454");
		}
		this._scrollbar_tweener_color = tBar.image.DOColor(tColor, 0.35f);
		if (!this._scrollbar_cached_state)
		{
			this._scrollbar_tweener_color.SetDelay(0.125f);
		}
		if (this._scrollbar_routine != null)
		{
			base.StopCoroutine(this._scrollbar_routine);
		}
		this._scrollbar_routine = base.StartCoroutine(this.toggleScrollbarRoutine());
	}

	// Token: 0x060041EB RID: 16875 RVA: 0x001BF565 File Offset: 0x001BD765
	private IEnumerator toggleScrollbarRoutine()
	{
		yield return new WaitForSecondsRealtime(0.25f);
		if (this.scrollRect.verticalScrollbar.size < 1f)
		{
			yield break;
		}
		this.toggleScrollbar(false);
		yield break;
	}

	// Token: 0x060041EC RID: 16876 RVA: 0x001BF574 File Offset: 0x001BD774
	private void toggleScrollbar(bool pState)
	{
		Scrollbar tVerticalBar = this.scrollRect.verticalScrollbar;
		this._scrollbar_tweener.Kill(false);
		float tPositionX = pState ? 0f : -17.2f;
		this._scrollbar_tweener = tVerticalBar.transform.DOLocalMoveX(tPositionX, 0.35f, false).SetEase(Ease.InOutCubic);
	}

	// Token: 0x060041ED RID: 16877 RVA: 0x001BF5C8 File Offset: 0x001BD7C8
	public void resetScroll()
	{
		Vector3 tVec = this.transform_content.localPosition;
		tVec.y = 0f;
		this.transform_content.localPosition = tVec;
	}

	// Token: 0x060041EE RID: 16878 RVA: 0x001BF5FC File Offset: 0x001BD7FC
	internal void create(bool pHide = false)
	{
		this._canvas = CanvasMain.instance.canvas_windows;
		if (this.historyActionEnabled)
		{
			this._back_button.onClick.AddListener(new UnityAction(WindowHistory.clickBack));
		}
		else
		{
			this._back_button_container.gameObject.SetActive(false);
		}
		if (pHide)
		{
			this.hide("right", false);
			this.finishTween();
		}
		this.checkGradient();
	}

	// Token: 0x060041EF RID: 16879 RVA: 0x001BF66B File Offset: 0x001BD86B
	private void checkGradient()
	{
		if (this.force_gradient || this._scrollbar_cached_state)
		{
			this.scrollingGradient.gameObject.SetActive(true);
			return;
		}
		this.scrollingGradient.gameObject.SetActive(false);
	}

	// Token: 0x060041F0 RID: 16880 RVA: 0x001BF6A0 File Offset: 0x001BD8A0
	public void clickBack()
	{
		WindowHistory.clickBack();
	}

	// Token: 0x060041F1 RID: 16881 RVA: 0x001BF6A8 File Offset: 0x001BD8A8
	private static void setCurrentWindow(ScrollWindow pWindow)
	{
		if (pWindow == null)
		{
			return;
		}
		ScrollWindow._current_window = pWindow;
		ScrollWindow._is_window_active = true;
		if (!ScrollWindow._is_any_window_active)
		{
			ScrollWindowNameAction open_callback = ScrollWindow._open_callback;
			if (open_callback != null)
			{
				open_callback(pWindow.screen_id);
			}
		}
		ScrollWindow._is_any_window_active = true;
		ScrollWindowNameAction show_callback = ScrollWindow._show_callback;
		if (show_callback == null)
		{
			return;
		}
		show_callback(pWindow.screen_id);
	}

	// Token: 0x060041F2 RID: 16882 RVA: 0x001BF704 File Offset: 0x001BD904
	private static void clearCurrentWindow(ScrollWindow pWindow)
	{
		if (ScrollWindow._current_window == null)
		{
			return;
		}
		ScrollWindowNameAction hide_callback = ScrollWindow._hide_callback;
		if (hide_callback != null)
		{
			hide_callback(ScrollWindow._current_window.screen_id);
		}
		ScrollWindow._current_window = null;
		ScrollWindow._is_window_active = false;
		Config.debug_window_stats.setCurrent(null);
	}

	// Token: 0x060041F3 RID: 16883 RVA: 0x001BF750 File Offset: 0x001BD950
	public static void queueWindow(string pWindowID)
	{
		ScrollWindow._queued_window = pWindowID;
	}

	// Token: 0x060041F4 RID: 16884 RVA: 0x001BF758 File Offset: 0x001BD958
	public static void clearQueue()
	{
		if (ScrollWindow._queued_window != "")
		{
			string queued_window = ScrollWindow._queued_window;
			ScrollWindow._queued_window = "";
			ScrollWindow.hideAllEvent(false);
			ScrollWindow.showWindow(queued_window);
		}
	}

	// Token: 0x060041F5 RID: 16885 RVA: 0x001BF785 File Offset: 0x001BD985
	public static void showWindow(string pWindowID)
	{
		ScrollWindow.showWindow(pWindowID, false, false);
	}

	// Token: 0x060041F6 RID: 16886 RVA: 0x001BF790 File Offset: 0x001BD990
	public static void showWindow(string pWindowID, bool pSkipAnimation = false, bool pBlockSame = false)
	{
		World.world.selected_buttons.clearHighlightedButton();
		if (ScrollWindow.isAnimationActive())
		{
			return;
		}
		bool tJustCreated = ScrollWindow.checkWindowExist(pWindowID);
		ScrollWindow tWindow = ScrollWindow._all_windows[pWindowID];
		if (pBlockSame && pWindowID == ScrollWindow._current_window.screen_id)
		{
			((IShakable)tWindow).shake();
			WindowToolbar.shake();
			ScrollWindow.randomDropHoveringIcon(3, 6);
			return;
		}
		tWindow.clickShow(pSkipAnimation, tJustCreated);
	}

	// Token: 0x060041F7 RID: 16887 RVA: 0x001BF7F8 File Offset: 0x001BD9F8
	public static void randomDropHoveringIcon(int pMin, int pMax)
	{
		int tDropAmount = Randy.randomInt(pMin, pMax);
		for (int i = 0; i < tDropAmount; i++)
		{
			HoveringBgIconManager.randomDrop();
		}
	}

	// Token: 0x060041F8 RID: 16888 RVA: 0x001BF81E File Offset: 0x001BDA1E
	public static string checkWindowID(string pWindowID)
	{
		if (pWindowID.StartsWith("worldnet", StringComparison.Ordinal))
		{
			return "not_found";
		}
		return pWindowID;
	}

	// Token: 0x060041F9 RID: 16889 RVA: 0x001BF838 File Offset: 0x001BDA38
	public static bool windowLoaded(string pWindowID)
	{
		string tCheckWindowID = ScrollWindow.checkWindowID(pWindowID);
		return ScrollWindow._all_windows.ContainsKey(tCheckWindowID);
	}

	// Token: 0x060041FA RID: 16890 RVA: 0x001BF858 File Offset: 0x001BDA58
	public static bool checkWindowExist(string pWindowID)
	{
		string tCheckWindowID = ScrollWindow.checkWindowID(pWindowID);
		bool tJustCreated = false;
		if (!ScrollWindow._all_windows.ContainsKey(pWindowID))
		{
			tJustCreated = true;
			string tPath = "windows/" + tCheckWindowID;
			ScrollWindow tWindow;
			if (!WindowPreloader.TryGetPreloadedWindow(pWindowID, out tWindow))
			{
				ScrollWindow tWindowPrefab = (ScrollWindow)Resources.Load(tPath, typeof(ScrollWindow));
				if (tWindowPrefab == null)
				{
					Debug.LogError("Window with id " + tCheckWindowID + " not found!");
					tWindowPrefab = (ScrollWindow)Resources.Load("windows/not_found", typeof(ScrollWindow));
				}
				ListPool<GameObject> tTabsObjects = ScrollWindow.disableTabsInPrefab(tWindowPrefab);
				tWindow = Object.Instantiate<ScrollWindow>(tWindowPrefab, CanvasMain.instance.transformWindows);
				ScrollWindow.enableTabsInPrefab(tTabsObjects);
			}
			if (!ScrollWindow._all_windows.ContainsKey(pWindowID))
			{
				ScrollWindow._all_windows.Add(pWindowID, tWindow);
			}
			tWindow.screen_id = pWindowID;
			tWindow.name = pWindowID;
			tWindow.create(false);
		}
		return tJustCreated;
	}

	// Token: 0x060041FB RID: 16891 RVA: 0x001BF938 File Offset: 0x001BDB38
	public static ListPool<GameObject> disableTabsInPrefab(ScrollWindow pPrefab)
	{
		ListPool<GameObject> tGameObjects = new ListPool<GameObject>();
		if (pPrefab.tabs != null)
		{
			WindowMetaTab[] array = pPrefab.tabs.transform.FindAllRecursive<WindowMetaTab>();
			for (int i = 0; i < array.Length; i++)
			{
				foreach (Transform tTabElement in array[i].tab_elements)
				{
					if (!(tTabElement == null) && tTabElement.gameObject.activeSelf)
					{
						tTabElement.gameObject.SetActive(false);
						tGameObjects.Add(tTabElement.gameObject);
					}
				}
			}
		}
		return tGameObjects;
	}

	// Token: 0x060041FC RID: 16892 RVA: 0x001BF9F0 File Offset: 0x001BDBF0
	public static void enableTabsInPrefab(ListPool<GameObject> pTabsObjects)
	{
		foreach (GameObject ptr in pTabsObjects)
		{
			ptr.gameObject.SetActive(true);
		}
		pTabsObjects.Dispose();
	}

	// Token: 0x060041FD RID: 16893 RVA: 0x001BFA48 File Offset: 0x001BDC48
	public static ScrollWindow get(string pWindowID)
	{
		ScrollWindow.checkWindowExist(pWindowID);
		return ScrollWindow._all_windows[pWindowID];
	}

	// Token: 0x060041FE RID: 16894 RVA: 0x001BFA5C File Offset: 0x001BDC5C
	public void clickShow(bool pSkipAnimation = false, bool pJustCreated = false)
	{
		if (ScrollWindow.isAnimationActive())
		{
			return;
		}
		LogText.log("Window Opened", this.screen_id, "");
		if (ScrollWindow._current_window == this)
		{
			this.showSameWindow();
			return;
		}
		ScrollWindow.moveAllToLeftAndRemove(true);
		this.show("right", "right", pSkipAnimation, pJustCreated);
	}

	// Token: 0x060041FF RID: 16895 RVA: 0x001BFAB4 File Offset: 0x001BDCB4
	public void clickShowLeft()
	{
		if (ScrollWindow.isAnimationActive())
		{
			return;
		}
		LogText.log("Window Opened", this.screen_id, "");
		if (ScrollWindow._current_window == this)
		{
			this.showSameWindow();
			return;
		}
		ScrollWindow.moveAllToRightAndRemove(true);
		this.show("left", "left", false, false);
	}

	// Token: 0x06004200 RID: 16896 RVA: 0x001BFB0A File Offset: 0x001BDD0A
	public void forceShow()
	{
		this.show("right", "right", true, false);
	}

	// Token: 0x06004201 RID: 16897 RVA: 0x001BFB20 File Offset: 0x001BDD20
	public void show(string pDistPosition = "right", string pStartPosition = "right", bool pSkipAnimation = false, bool pJustCreated = false)
	{
		this.setActive(true, pDistPosition, pStartPosition, pSkipAnimation, pJustCreated);
		CanvasMain.addTooltipShowTimeout(0.01f);
		if (this.screen_id == "PremiumPurchaseError")
		{
			Analytics.LogEvent("purchase_premium_error", true, true);
		}
		Analytics.trackWindow(this.screen_id);
		this.historyAction();
		PowerTracker.trackWindow(this.screen_id, this);
		MusicBox.playSoundUI("event:/SFX/UI/WindowWhoosh");
	}

	// Token: 0x06004202 RID: 16898 RVA: 0x001BFB88 File Offset: 0x001BDD88
	private void historyAction()
	{
		if (!this.historyActionEnabled)
		{
			return;
		}
		if (WindowHistory.hasHistory())
		{
			this._back_button_container.SetActive(true);
			Sprite tIcon = WindowHistory.list.Last<WindowHistoryData>().window._asset.getSprite();
			this.previous_window_icon.sprite = tIcon;
			this._close_button_tip.text_description_2 = "";
		}
		else
		{
			this._back_button_container.SetActive(false);
			this._close_button_tip.text_description_2 = "hotkey_cancel";
		}
		WindowHistory.addIntoHistory(this);
	}

	// Token: 0x06004203 RID: 16899 RVA: 0x001BFC0C File Offset: 0x001BDE0C
	public static void moveAllToLeftAndRemove(bool pWithAnimation = true)
	{
		if (ScrollWindow._current_window != null)
		{
			if (pWithAnimation)
			{
				ScrollWindow._current_window.moveToLeft(true);
			}
			else
			{
				ScrollWindow._current_window.activeToFalse();
			}
			ScrollWindowNameAction hide_callback = ScrollWindow._hide_callback;
			if (hide_callback != null)
			{
				hide_callback(ScrollWindow._current_window.screen_id);
			}
			ScrollWindow._current_window = null;
		}
		ScrollWindow._is_window_active = false;
		Config.debug_window_stats.setCurrent(null);
	}

	// Token: 0x06004204 RID: 16900 RVA: 0x001BFC71 File Offset: 0x001BDE71
	public static bool isCurrentWindow(string pWindowID)
	{
		return !(ScrollWindow._current_window == null) && ScrollWindow._current_window.screen_id == pWindowID;
	}

	// Token: 0x06004205 RID: 16901 RVA: 0x001BFC92 File Offset: 0x001BDE92
	public static ScrollWindow getCurrentWindow()
	{
		if (!ScrollWindow.isWindowActive())
		{
			return null;
		}
		return ScrollWindow._current_window;
	}

	// Token: 0x06004206 RID: 16902 RVA: 0x001BFCA2 File Offset: 0x001BDEA2
	public static void setPreviousWindowSprite(Sprite pSprite)
	{
		if (!ScrollWindow.isWindowActive())
		{
			return;
		}
		ScrollWindow.getCurrentWindow().previous_window_icon.sprite = pSprite;
	}

	// Token: 0x06004207 RID: 16903 RVA: 0x001BFCBC File Offset: 0x001BDEBC
	public static void moveAllToRightAndRemove(bool pWithAnimation = true)
	{
		if (ScrollWindow._current_window != null)
		{
			if (pWithAnimation)
			{
				ScrollWindow._current_window.moveToRight(true);
			}
			else
			{
				ScrollWindow._current_window.activeToFalse();
			}
			ScrollWindowNameAction hide_callback = ScrollWindow._hide_callback;
			if (hide_callback != null)
			{
				hide_callback(ScrollWindow._current_window.screen_id);
			}
			ScrollWindow._current_window = null;
		}
		ScrollWindow._is_window_active = false;
		Config.debug_window_stats.setCurrent(null);
	}

	// Token: 0x06004208 RID: 16904 RVA: 0x001BFD24 File Offset: 0x001BDF24
	public void moveToLeft(bool pRemove = false)
	{
		ScrollWindow.setCanvasGroupEnabled(this._canvas_group, false);
		if (pRemove)
		{
			float tDestinationX = base.transform.localPosition.x + this.getHidePosLeft();
			this.moveTween(tDestinationX, new TweenCallback(this.activeToFalse), false);
		}
	}

	// Token: 0x06004209 RID: 16905 RVA: 0x001BFD6C File Offset: 0x001BDF6C
	public void moveToRight(bool pRemove = false)
	{
		ScrollWindow.setCanvasGroupEnabled(this._canvas_group, false);
		if (pRemove)
		{
			float tDestinationX = base.transform.localPosition.x - this.getHidePosLeft();
			this.moveTween(tDestinationX, new TweenCallback(this.activeToFalse), false);
		}
	}

	// Token: 0x0600420A RID: 16906 RVA: 0x001BFDB4 File Offset: 0x001BDFB4
	public static void setCanvasGroupEnabled(CanvasGroup pGroup, bool pValue)
	{
		pGroup.interactable = pValue;
		pGroup.blocksRaycasts = pValue;
	}

	// Token: 0x0600420B RID: 16907 RVA: 0x001BFDC4 File Offset: 0x001BDFC4
	public void showSameWindow()
	{
		this.sameWindowTween(0f, new TweenCallback(this.finishTween));
		this.historyAction();
		ScrollWindow.clearCurrentWindow(this);
		ScrollWindowNameAction show_started_callback = ScrollWindow._show_started_callback;
		if (show_started_callback != null)
		{
			show_started_callback(this.screen_id);
		}
		base.gameObject.SetActive(false);
		base.gameObject.SetActive(true);
		ScrollWindow.setCurrentWindow(this);
		Tooltip.hideTooltipNow();
		this.resetScroll();
	}

	// Token: 0x0600420C RID: 16908 RVA: 0x001BFE34 File Offset: 0x001BE034
	public void setActive(bool pActive, string pDistPosition = "right", string pStartPosition = "right", bool pSkipAnimation = false, bool pJustCreated = false)
	{
		if (ScrollWindow.skip_worldtip_hide)
		{
			ScrollWindow.skip_worldtip_hide = false;
		}
		else
		{
			WorldTip.hideNow();
		}
		Tooltip.hideTooltipNow();
		if (this.unselectPower && World.world.selected_buttons.selectedButton != null && World.world.selected_buttons.selectedButton.godPower.unselect_when_window)
		{
			World.world.selected_buttons.unselectAll();
		}
		if (pActive)
		{
			ScrollWindow.setCanvasGroupEnabled(this._canvas_group, true);
			ScrollWindowNameAction show_started_callback = ScrollWindow._show_started_callback;
			if (show_started_callback != null)
			{
				show_started_callback(this.screen_id);
			}
			base.gameObject.SetActive(true);
			ScrollWindow.setCurrentWindow(this);
			this.resetScroll();
			if (!(pStartPosition == "right"))
			{
				if (pStartPosition == "left")
				{
					base.transform.localPosition = new Vector3(this.getHidePosLeft(), -6f, base.transform.localPosition.z);
				}
			}
			else
			{
				base.transform.localPosition = new Vector3(this.getHidePosRight(), -6f, base.transform.localPosition.z);
			}
			if (pSkipAnimation)
			{
				this.finishTween();
				base.transform.localPosition = new Vector3(0f, 0f, 0f);
				return;
			}
			this.moveTween(0f, new TweenCallback(this.finishTween), pJustCreated);
			return;
		}
		else
		{
			ScrollWindow.clearCurrentWindow(this);
			if (pDistPosition == "left")
			{
				float tDestinationX = this.getHidePosLeft();
				this.moveTween(tDestinationX, new TweenCallback(this.activeToFalse), pJustCreated);
				return;
			}
			this.moveTween(this.getHidePosRight(), new TweenCallback(this.activeToFalse), pJustCreated);
			return;
		}
	}

	// Token: 0x0600420D RID: 16909 RVA: 0x001BFFE8 File Offset: 0x001BE1E8
	protected void moveTween(float pToX = 0f, TweenCallback pCompleteCallback = null, bool pJustCreated = false)
	{
		float tTime = 0.35f;
		Ease tEase = Ease.OutBack;
		if (pCompleteCallback == null)
		{
			pCompleteCallback = new TweenCallback(this.finishTween);
		}
		if (pCompleteCallback == new TweenCallback(this.activeToFalse))
		{
			tTime = 0.1f;
			tEase = Ease.Linear;
		}
		Vector3 tTarget = new Vector3(pToX, -6f, base.transform.localPosition.z);
		this._animation_tween.Kill(true);
		float tDelay = 0.02f;
		if (pJustCreated)
		{
			tDelay = 0.1f;
		}
		this._animation_tween = base.transform.DOLocalMove(tTarget, tTime, false).SetDelay(tDelay).SetEase(tEase).OnComplete(delegate
		{
			pCompleteCallback();
			ScrollWindow._animations_list.Remove(this._animation_tween);
		});
		ScrollWindow._animations_list.Add(this._animation_tween);
	}

	// Token: 0x0600420E RID: 16910 RVA: 0x001C00CC File Offset: 0x001BE2CC
	protected void sameWindowTween(float pToX = 0f, TweenCallback pCompleteCallback = null)
	{
		float tTime = 0.09f;
		if (pCompleteCallback == null)
		{
			pCompleteCallback = new TweenCallback(this.finishTween);
		}
		base.transform.localPosition = new Vector3(0f, -4f, 0f);
		Vector3 tTarget = new Vector3(pToX, -6f, base.gameObject.transform.localPosition.z);
		this._animation_tween.Kill(true);
		this._animation_tween = base.transform.DOLocalMove(tTarget, tTime, false).SetEase(Ease.InOutSine).OnComplete(delegate
		{
			pCompleteCallback();
			ScrollWindow._animations_list.Remove(this._animation_tween);
		});
		ScrollWindow._animations_list.Add(this._animation_tween);
	}

	// Token: 0x0600420F RID: 16911 RVA: 0x001C0198 File Offset: 0x001BE398
	public static void hideAllEvent(bool pWithAnimation = true)
	{
		if (ScrollWindow.isWindowActive())
		{
			ScrollWindow._should_clear = true;
			ScrollWindowAction close_callback = ScrollWindow._close_callback;
			if (close_callback != null)
			{
				close_callback();
			}
			ScrollWindow.moveAllToLeftAndRemove(pWithAnimation);
		}
		Tooltip.hideTooltipNow();
		World.world.player_control.controls_lock_timer = 0.1f;
		PowerTracker.trackWatching();
	}

	// Token: 0x06004210 RID: 16912 RVA: 0x001C01E8 File Offset: 0x001BE3E8
	public void clickCloseButton(string pDirection = "right")
	{
		this.clickHide(pDirection);
		this._current_background_sprite_index++;
		if (this._current_background_sprite_index >= this.close_sprites.Count)
		{
			this._current_background_sprite_index = this.close_sprites.Count - 1;
		}
		this.close_background.sprite = this.close_sprites[this._current_background_sprite_index];
	}

	// Token: 0x06004211 RID: 16913 RVA: 0x001C024C File Offset: 0x001BE44C
	public void clickHide(string pDirection = "right")
	{
		if (!ScrollWindow.canClickHide())
		{
			return;
		}
		this.hide(pDirection, true);
		World.world.selected_buttons.gameObject.SetActive(true);
		ScrollWindow._should_clear = true;
		ScrollWindowAction close_callback = ScrollWindow._close_callback;
		if (close_callback != null)
		{
			close_callback();
		}
		World.world.player_control.controls_lock_timer = 0.3f;
		PowerTracker.trackWatching();
	}

	// Token: 0x06004212 RID: 16914 RVA: 0x001C02B0 File Offset: 0x001BE4B0
	internal static void checkElements()
	{
		if (!ScrollWindow.isWindowActive())
		{
			return;
		}
		ScrollWindow tWindow = ScrollWindow.getCurrentWindow();
		bool tShouldClose = tWindow.shouldClose();
		bool tShouldRefresh = false;
		if (!tShouldClose)
		{
			tShouldRefresh = tWindow.shouldRefresh();
		}
		if (tShouldClose || tShouldRefresh)
		{
			TabbedWindow tTabbedWindow;
			if (tWindow.TryGetComponent<TabbedWindow>(out tTabbedWindow))
			{
				tTabbedWindow.StopAllCoroutines();
			}
			WindowMetaElementBase[] componentsInChildren = tWindow.GetComponentsInChildren<WindowMetaElementBase>(false);
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].StopAllCoroutines();
			}
		}
		if (tShouldClose)
		{
			ScrollWindow.hideAllEvent(false);
			return;
		}
		if (tShouldRefresh)
		{
			WindowHistory.popHistory();
			tWindow.showSameWindow();
		}
	}

	// Token: 0x06004213 RID: 16915 RVA: 0x001C0334 File Offset: 0x001BE534
	public bool shouldClose()
	{
		TabbedWindow tTabbedWindow;
		return base.TryGetComponent<TabbedWindow>(out tTabbedWindow) && tTabbedWindow.checkCancelWindow();
	}

	// Token: 0x06004214 RID: 16916 RVA: 0x001C0358 File Offset: 0x001BE558
	public bool shouldRefresh()
	{
		IShouldRefreshWindow[] componentsInChildren = base.GetComponentsInChildren<IShouldRefreshWindow>(false);
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			if (componentsInChildren[i].checkRefreshWindow())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06004215 RID: 16917 RVA: 0x001C0388 File Offset: 0x001BE588
	private void OnDisable()
	{
		if (ScrollWindow._should_clear)
		{
			ScrollWindow._should_clear = false;
			WindowHistory.clear();
			ScrollWindow.clear();
			ScrollWindow._is_any_window_active = false;
			Config.debug_window_stats.setCurrent(null);
		}
		((IShakable)this).killShakeTween();
	}

	// Token: 0x06004216 RID: 16918 RVA: 0x001C03B8 File Offset: 0x001BE5B8
	internal static void clear()
	{
		foreach (MetaTypeAsset metaTypeAsset in AssetManager.meta_type_library.list)
		{
			MetaTypeAction window_action_clear = metaTypeAsset.window_action_clear;
			if (window_action_clear != null)
			{
				window_action_clear();
			}
		}
		NanoObject tNano = SelectedObjects.getSelectedNanoObject();
		if (!tNano.isRekt())
		{
			AssetManager.meta_type_library.getAsset(tNano.getMetaType()).selectAndInspect(tNano, false, true, true);
		}
	}

	// Token: 0x06004217 RID: 16919 RVA: 0x001C0440 File Offset: 0x001BE640
	public static bool canClickHide()
	{
		return !WorkshopUploadingWorldWindow.uploading;
	}

	// Token: 0x06004218 RID: 16920 RVA: 0x001C044C File Offset: 0x001BE64C
	public void hide(string pDirection = "right", bool pPlaySound = true)
	{
		LogText.log("Window Hide", this.screen_id, "");
		this.setActive(false, pDirection, "right", false, false);
		CanvasMain.addTooltipShowTimeout(0.01f);
		ScrollWindow.setCanvasGroupEnabled(this._canvas_group, false);
		if (pPlaySound)
		{
			MusicBox.playSoundUI("event:/SFX/UI/WindowClose");
		}
		Analytics.hideWindow();
	}

	// Token: 0x06004219 RID: 16921 RVA: 0x001C04A8 File Offset: 0x001BE6A8
	public static void finishAnimations()
	{
		using (ListPool<Tweener> tList = new ListPool<Tweener>(ScrollWindow._animations_list))
		{
			tList.Sort(delegate(Tweener p1, Tweener p2)
			{
				float t = p1.Duration(true) * p1.ElapsedPercentage(true);
				float t2 = p2.Duration(true) * p2.ElapsedPercentage(true);
				return t.CompareTo(t2);
			});
			foreach (Tweener ptr in tList)
			{
				ptr.Kill(true);
			}
		}
	}

	// Token: 0x0600421A RID: 16922 RVA: 0x001C053C File Offset: 0x001BE73C
	public void finishTween()
	{
		ScrollWindowNameAction show_finished_callback = ScrollWindow._show_finished_callback;
		if (show_finished_callback == null)
		{
			return;
		}
		show_finished_callback(this.screen_id);
	}

	// Token: 0x0600421B RID: 16923 RVA: 0x001C0553 File Offset: 0x001BE753
	public void activeToFalse()
	{
		base.gameObject.SetActive(false);
	}

	// Token: 0x0600421C RID: 16924 RVA: 0x001C0564 File Offset: 0x001BE764
	public float getHidePosRight()
	{
		return this._canvas.GetComponent<RectTransform>().sizeDelta.x / 2f + this._bg_rect.sizeDelta.x / 2f + this._bg_rect.sizeDelta.x * 0.2f;
	}

	// Token: 0x0600421D RID: 16925 RVA: 0x001C05BA File Offset: 0x001BE7BA
	public float getHidePosLeft()
	{
		return this.getHidePosRight() * -1f;
	}

	// Token: 0x0600421E RID: 16926 RVA: 0x001C05C8 File Offset: 0x001BE7C8
	public float getHidePosLeftHalf()
	{
		float sideDif = (this._canvas.GetComponent<RectTransform>().sizeDelta.x - this._bg_rect.sizeDelta.x) / 2f;
		return this.getHidePosLeft() + sideDif / 2f;
	}

	// Token: 0x0600421F RID: 16927 RVA: 0x001C0610 File Offset: 0x001BE810
	public float getDistBetweenWindows()
	{
		return this.getHidePosLeftHalf();
	}

	// Token: 0x06004220 RID: 16928 RVA: 0x001C0618 File Offset: 0x001BE818
	public void openConsole()
	{
		World.world.console.Show();
	}

	// Token: 0x06004221 RID: 16929 RVA: 0x001C0629 File Offset: 0x001BE829
	public static void addCallbackOpen(ScrollWindowNameAction pAction)
	{
		ScrollWindow._open_callback = (ScrollWindowNameAction)Delegate.Combine(ScrollWindow._open_callback, pAction);
	}

	// Token: 0x06004222 RID: 16930 RVA: 0x001C0640 File Offset: 0x001BE840
	public static void removeCallbackOpen(ScrollWindowNameAction pAction)
	{
		ScrollWindow._open_callback = (ScrollWindowNameAction)Delegate.Remove(ScrollWindow._open_callback, pAction);
	}

	// Token: 0x06004223 RID: 16931 RVA: 0x001C0657 File Offset: 0x001BE857
	public static void addCallbackShowStarted(ScrollWindowNameAction pAction)
	{
		ScrollWindow._show_started_callback = (ScrollWindowNameAction)Delegate.Combine(ScrollWindow._show_started_callback, pAction);
	}

	// Token: 0x06004224 RID: 16932 RVA: 0x001C066E File Offset: 0x001BE86E
	public static void removeCallbackShowStarted(ScrollWindowNameAction pAction)
	{
		ScrollWindow._show_started_callback = (ScrollWindowNameAction)Delegate.Remove(ScrollWindow._show_started_callback, pAction);
	}

	// Token: 0x06004225 RID: 16933 RVA: 0x001C0685 File Offset: 0x001BE885
	public static void addCallbackShow(ScrollWindowNameAction pAction)
	{
		ScrollWindow._show_callback = (ScrollWindowNameAction)Delegate.Combine(ScrollWindow._show_callback, pAction);
	}

	// Token: 0x06004226 RID: 16934 RVA: 0x001C069C File Offset: 0x001BE89C
	public static void removeCallbackShow(ScrollWindowNameAction pAction)
	{
		ScrollWindow._show_callback = (ScrollWindowNameAction)Delegate.Remove(ScrollWindow._show_callback, pAction);
	}

	// Token: 0x06004227 RID: 16935 RVA: 0x001C06B3 File Offset: 0x001BE8B3
	public static void addCallbackShowFinished(ScrollWindowNameAction pAction)
	{
		ScrollWindow._show_finished_callback = (ScrollWindowNameAction)Delegate.Combine(ScrollWindow._show_finished_callback, pAction);
	}

	// Token: 0x06004228 RID: 16936 RVA: 0x001C06CA File Offset: 0x001BE8CA
	public static void removeCallbackShowFinished(ScrollWindowNameAction pAction)
	{
		ScrollWindow._show_finished_callback = (ScrollWindowNameAction)Delegate.Remove(ScrollWindow._show_finished_callback, pAction);
	}

	// Token: 0x06004229 RID: 16937 RVA: 0x001C06E1 File Offset: 0x001BE8E1
	public static void addCallbackHide(ScrollWindowNameAction pAction)
	{
		ScrollWindow._hide_callback = (ScrollWindowNameAction)Delegate.Combine(ScrollWindow._hide_callback, pAction);
	}

	// Token: 0x0600422A RID: 16938 RVA: 0x001C06F8 File Offset: 0x001BE8F8
	public static void removeCallbackHide(ScrollWindowNameAction pAction)
	{
		ScrollWindow._hide_callback = (ScrollWindowNameAction)Delegate.Remove(ScrollWindow._hide_callback, pAction);
	}

	// Token: 0x0600422B RID: 16939 RVA: 0x001C070F File Offset: 0x001BE90F
	public static void addCallbackClose(ScrollWindowAction pAction)
	{
		ScrollWindow._close_callback = (ScrollWindowAction)Delegate.Combine(ScrollWindow._close_callback, pAction);
	}

	// Token: 0x0600422C RID: 16940 RVA: 0x001C0726 File Offset: 0x001BE926
	public static void removeCallbackClose(ScrollWindowAction pAction)
	{
		ScrollWindow._close_callback = (ScrollWindowAction)Delegate.Remove(ScrollWindow._close_callback, pAction);
	}

	// Token: 0x0600422D RID: 16941 RVA: 0x001C073D File Offset: 0x001BE93D
	private void initComponents()
	{
	}

	// Token: 0x0600422E RID: 16942 RVA: 0x001C0740 File Offset: 0x001BE940
	public static void debug(DebugTool pTool)
	{
		if (ScrollWindow.isWindowActive())
		{
			pTool.setText("currentWindow:", ScrollWindow.getCurrentWindow().screen_id, 0f, false, 0L, false, false, "");
			pTool.setText("historyActionEnabled:", ScrollWindow.getCurrentWindow().historyActionEnabled, 0f, false, 0L, false, false, "");
		}
		pTool.setText("_is_window_active:", ScrollWindow._is_window_active, 0f, false, 0L, false, false, "");
		pTool.setText("_is_any_window_active:", ScrollWindow._is_any_window_active, 0f, false, 0L, false, false, "");
		pTool.setText("isAnimationActive:", ScrollWindow.isAnimationActive(), 0f, false, 0L, false, false, "");
		pTool.setText("queuedWindow:", ScrollWindow._queued_window, 0f, false, 0L, false, false, "");
	}

	// Token: 0x06004231 RID: 16945 RVA: 0x001C0895 File Offset: 0x001BEA95
	Transform IShakable.get_transform()
	{
		return base.transform;
	}

	// Token: 0x04003017 RID: 12311
	public const int WINDOW_POSITION_Y = -6;

	// Token: 0x04003018 RID: 12312
	private const float SCROLLBAR_POSITION_SHOW = 0f;

	// Token: 0x04003019 RID: 12313
	private const float SCROLLBAR_POSITION_HIDE = -17.2f;

	// Token: 0x0400301A RID: 12314
	private const float SCROLLBAR_ANIMATION_DURATION = 0.35f;

	// Token: 0x0400301B RID: 12315
	private const float SCROLLBAR_ANIMATION_DURATION_COLOR = 0.35f;

	// Token: 0x0400301C RID: 12316
	private const float SCROLLBAR_ANIMATION_DELAY = 0.25f;

	// Token: 0x0400301D RID: 12317
	private const float SCROLLBAR_ANIMATION_DELAY_COLOR = 0.125f;

	// Token: 0x0400301E RID: 12318
	private const string SCROLLBAR_ACTIVE_COLOR = "#E75340";

	// Token: 0x0400301F RID: 12319
	private const string SCROLLBAR_INACTIVE_COLOR = "#545454";

	// Token: 0x04003020 RID: 12320
	private static ScrollWindowNameAction _open_callback;

	// Token: 0x04003021 RID: 12321
	private static ScrollWindowNameAction _show_started_callback;

	// Token: 0x04003022 RID: 12322
	private static ScrollWindowNameAction _show_callback;

	// Token: 0x04003023 RID: 12323
	private static ScrollWindowNameAction _show_finished_callback;

	// Token: 0x04003024 RID: 12324
	private static ScrollWindowNameAction _hide_callback;

	// Token: 0x04003025 RID: 12325
	private static ScrollWindowAction _close_callback;

	// Token: 0x04003026 RID: 12326
	private static ScrollWindow _current_window = null;

	// Token: 0x04003027 RID: 12327
	private static Dictionary<string, ScrollWindow> _all_windows = new Dictionary<string, ScrollWindow>();

	// Token: 0x04003028 RID: 12328
	private static bool _is_window_active = false;

	// Token: 0x04003029 RID: 12329
	private static bool _is_any_window_active = false;

	// Token: 0x0400302A RID: 12330
	public string screen_id = "screen";

	// Token: 0x0400302B RID: 12331
	public bool unselectPower = true;

	// Token: 0x0400302C RID: 12332
	private Canvas _canvas;

	// Token: 0x0400302D RID: 12333
	private CanvasGroup _canvas_group;

	// Token: 0x0400302E RID: 12334
	private RectTransform _bg_rect;

	// Token: 0x0400302F RID: 12335
	public Text titleText;

	// Token: 0x04003030 RID: 12336
	[SerializeField]
	private GameObject _back_button_container;

	// Token: 0x04003031 RID: 12337
	[SerializeField]
	private Button _back_button;

	// Token: 0x04003032 RID: 12338
	public Image previous_window_icon;

	// Token: 0x04003033 RID: 12339
	private static string _queued_window = "";

	// Token: 0x04003034 RID: 12340
	public ScrollRect scrollRect;

	// Token: 0x04003035 RID: 12341
	public Image scrollingGradient;

	// Token: 0x04003036 RID: 12342
	public RectTransform transform_content;

	// Token: 0x04003037 RID: 12343
	public RectTransform transform_viewport;

	// Token: 0x04003038 RID: 12344
	public RectTransform transform_scrollRect;

	// Token: 0x04003039 RID: 12345
	public GameObject[] destroyOnAwake;

	// Token: 0x0400303A RID: 12346
	public bool force_gradient;

	// Token: 0x0400303B RID: 12347
	public bool historyActionEnabled = true;

	// Token: 0x0400303C RID: 12348
	private static bool _should_clear;

	// Token: 0x0400303D RID: 12349
	public List<Sprite> close_sprites;

	// Token: 0x0400303E RID: 12350
	public Image close_background;

	// Token: 0x0400303F RID: 12351
	private int _current_background_sprite_index;

	// Token: 0x04003040 RID: 12352
	[SerializeField]
	private TipButton _close_button_tip;

	// Token: 0x04003041 RID: 12353
	public WindowMetaTabButtonsContainer tabs;

	// Token: 0x04003042 RID: 12354
	public static bool skip_worldtip_hide;

	// Token: 0x04003046 RID: 12358
	private static List<Tweener> _animations_list = new List<Tweener>();

	// Token: 0x04003047 RID: 12359
	private Tweener _animation_tween;

	// Token: 0x04003048 RID: 12360
	private WindowAsset _asset;

	// Token: 0x04003049 RID: 12361
	private bool _scrollbar_cached_state;

	// Token: 0x0400304A RID: 12362
	private Tweener _scrollbar_tweener;

	// Token: 0x0400304B RID: 12363
	private Tweener _scrollbar_tweener_color;

	// Token: 0x0400304C RID: 12364
	private Coroutine _scrollbar_routine;

	// Token: 0x0400304D RID: 12365
	private bool _initialized;
}
