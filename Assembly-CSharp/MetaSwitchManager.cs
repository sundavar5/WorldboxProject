using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000836 RID: 2102
public class MetaSwitchManager : MonoBehaviour
{
	// Token: 0x06004160 RID: 16736 RVA: 0x001BCE84 File Offset: 0x001BB084
	private void Awake()
	{
		MetaSwitchManager._instance = this;
		ScrollWindow.addCallbackOpen(delegate(string _)
		{
			this._was_just_opened = true;
			this.enable(true, true);
		});
		ScrollWindow.addCallbackShow(delegate(string _)
		{
			if (this._was_just_opened)
			{
				this._was_just_opened = false;
				return;
			}
			this.enable(false, true);
		});
		ScrollWindow.addCallbackClose(delegate
		{
			this.disable(true, true);
		});
		this._button_left.init(MetaSwitchManager.Direction.Left, new SwitchWindowsAction(MetaSwitchManager.switchWindowsWithCheck));
		this._button_right.init(MetaSwitchManager.Direction.Right, new SwitchWindowsAction(MetaSwitchManager.switchWindowsWithCheck));
	}

	// Token: 0x06004161 RID: 16737 RVA: 0x001BCEFA File Offset: 0x001BB0FA
	private void Start()
	{
		CanvasMain.instance.addCallbackResize(delegate(float _, float _)
		{
			if (!this._is_enabled)
			{
				this.enable(false, false);
				return;
			}
			this.refresh(false, false);
		});
	}

	// Token: 0x06004162 RID: 16738 RVA: 0x001BCF14 File Offset: 0x001BB114
	private void enable(bool pOpen, bool pCompleteOnDisable = true)
	{
		this._is_enabled = true;
		ScrollWindow currentWindow = ScrollWindow.getCurrentWindow();
		StatsWindow tStatsWindow = (currentWindow != null) ? currentWindow.GetComponent<StatsWindow>() : null;
		if (tStatsWindow == this._stats_window && this._stats_window != null)
		{
			this.updateShowingData();
			return;
		}
		if (tStatsWindow == null)
		{
			this.disable(!pOpen, true);
			return;
		}
		this._stats_window = tStatsWindow;
		this._meta_type_asset = AssetManager.meta_type_library.getAsset(this._stats_window.meta_type);
		this.refresh(true, pCompleteOnDisable);
	}

	// Token: 0x06004163 RID: 16739 RVA: 0x001BCF9C File Offset: 0x001BB19C
	private void disable(bool pAnimated = true, bool pCompleteTween = true)
	{
		this._is_enabled = false;
		if (pAnimated)
		{
			this.toggleControlsPosition(false, pCompleteTween);
		}
		else
		{
			this.toggleControls(false);
		}
		this._stats_window = null;
		ListPool<NanoObject> list = this._list;
		if (list != null)
		{
			list.Dispose();
		}
		this._list = null;
	}

	// Token: 0x06004164 RID: 16740 RVA: 0x001BCFD8 File Offset: 0x001BB1D8
	public static void checkAndRefresh()
	{
		MetaSwitchManager._instance.checkRefresh();
	}

	// Token: 0x06004165 RID: 16741 RVA: 0x001BCFE4 File Offset: 0x001BB1E4
	public static void refresh()
	{
		MetaSwitchManager._instance.refresh(true, true);
	}

	// Token: 0x06004166 RID: 16742 RVA: 0x001BCFF2 File Offset: 0x001BB1F2
	public static void refreshWithoutComplete()
	{
		MetaSwitchManager._instance.refresh(false, true);
	}

	// Token: 0x06004167 RID: 16743 RVA: 0x001BD000 File Offset: 0x001BB200
	private void checkRefresh()
	{
		if (!this._is_enabled)
		{
			return;
		}
		this.refresh(false, true);
	}

	// Token: 0x06004168 RID: 16744 RVA: 0x001BD014 File Offset: 0x001BB214
	internal void refresh(bool pCompleteTween = true, bool pCompleteOnDisable = true)
	{
		int tSize = PlayerConfig.getOptionInt("ui_size_windows");
		if ((float)tSize > 100f)
		{
			float tRatio = Mathf.Lerp(1.275f, 1.45f, 1f - Mathf.InverseLerp(100f, 115f, (float)tSize));
			float tScreenRatio = (float)Screen.width / (float)Screen.height * tRatio;
			if ((float)tSize * tScreenRatio > 100f)
			{
				this.disable(true, pCompleteOnDisable);
				return;
			}
		}
		ListPool<NanoObject> list = this._list;
		if (list != null)
		{
			list.Dispose();
		}
		this._list = this._meta_type_asset.getSortedList();
		bool tEnabled = this._list.Count >= 2;
		this._is_switching_enabled = tEnabled;
		this.toggleControlsPosition(tEnabled, pCompleteTween);
		if (tEnabled)
		{
			this.updateShowingData();
		}
	}

	// Token: 0x06004169 RID: 16745 RVA: 0x001BD0CC File Offset: 0x001BB2CC
	private static void switchWindowsWithCheck(MetaSwitchManager.Direction pDirection)
	{
		if (!ScrollWindow.isWindowActive())
		{
			return;
		}
		if (ScrollWindow.isAnimationActive())
		{
			return;
		}
		MetaSwitchManager.switchWindows(pDirection);
	}

	// Token: 0x0600416A RID: 16746 RVA: 0x001BD0E4 File Offset: 0x001BB2E4
	public static void switchWindows(MetaSwitchManager.Direction pDirection)
	{
		MetaSwitchManager._instance.switchWindow(pDirection);
	}

	// Token: 0x0600416B RID: 16747 RVA: 0x001BD0F4 File Offset: 0x001BB2F4
	private int getCurrentMetaIndex()
	{
		NanoObject tSelected = this._meta_type_asset.get_selected();
		int tIndex = this._list.IndexOf(tSelected);
		if (tIndex == -1)
		{
			this._list.Add(tSelected);
			tIndex = this._list.IndexOf(tSelected);
		}
		return tIndex;
	}

	// Token: 0x0600416C RID: 16748 RVA: 0x001BD140 File Offset: 0x001BB340
	private void switchWindow(MetaSwitchManager.Direction pDirection)
	{
		if (!this._is_switching_enabled)
		{
			return;
		}
		if (this._stats_window == null)
		{
			return;
		}
		if (this._list.Count < 2)
		{
			return;
		}
		NanoObject tNextElement = this.getElement(pDirection);
		this._meta_type_asset.set_selected(tNextElement);
		WindowHistory.popHistory();
		ScrollWindow.showWindow(this._meta_type_asset.window_name);
		this.updateShowingData();
	}

	// Token: 0x0600416D RID: 16749 RVA: 0x001BD1A8 File Offset: 0x001BB3A8
	private void updateShowingData()
	{
		this.updateWindowNumber();
		this.showBannersAndNames();
	}

	// Token: 0x0600416E RID: 16750 RVA: 0x001BD1B8 File Offset: 0x001BB3B8
	private void updateWindowNumber()
	{
		if (this._list == null)
		{
			this._window_number_current.text = "";
			this._window_number_total.text = "";
			return;
		}
		int tNumber = this.getCurrentMetaIndex() + 1;
		int tCount = this._list.Count;
		this._window_number_current.text = string.Format("{0}", tNumber);
		this._window_number_total.text = string.Format("{0}", tCount);
	}

	// Token: 0x0600416F RID: 16751 RVA: 0x001BD239 File Offset: 0x001BB439
	private void showBannersAndNames()
	{
		this.clear();
		this.showBanner(this.getIndex(MetaSwitchManager.Direction.Left), this._button_left);
		this.showBanner(this.getIndex(MetaSwitchManager.Direction.Right), this._button_right);
	}

	// Token: 0x06004170 RID: 16752 RVA: 0x001BD26C File Offset: 0x001BB46C
	private IBanner showBanner(int pIndex, MetaSwitchButton pButton)
	{
		NanoObject tObject = this._list[pIndex];
		IBanner tBanner = pButton.getPool().getNext(tObject);
		tBanner.load(tObject);
		Button tButton;
		if (tBanner.gameObject.TryGetComponent<Button>(out tButton))
		{
			tButton.enabled = false;
		}
		pButton.setBanner(tBanner);
		Transform transform = tBanner.transform;
		Transform parent = transform.parent;
		parent.localPosition = Vector3.zero;
		parent.localScale = Vector3.one;
		transform.localPosition = Vector3.zero;
		ColorAsset tColorAsset = tObject.getColor();
		if (tColorAsset != null)
		{
			string tColor = tColorAsset.color_text;
			pButton.meta_name.text = tObject.name.ColorHex(tColor, false);
		}
		return tBanner;
	}

	// Token: 0x06004171 RID: 16753 RVA: 0x001BD310 File Offset: 0x001BB510
	private void toggleControlsPosition(bool pState, bool pCompleteTween = true)
	{
		this._tweener.Kill(pCompleteTween);
		float tPositionY = pState ? 0f : -44f;
		if (pState)
		{
			this.toggleControls(true);
		}
		if (Mathf.Approximately(base.transform.localPosition.y, tPositionY))
		{
			return;
		}
		this._tweener = base.transform.DOLocalMoveY(tPositionY, 0.35f, false).SetEase(Ease.InOutCubic).OnComplete(delegate
		{
			if (!pState)
			{
				this.toggleControls(false);
			}
			this.checkRefresh();
		});
	}

	// Token: 0x06004172 RID: 16754 RVA: 0x001BD3AA File Offset: 0x001BB5AA
	private void toggleControls(bool pState)
	{
		this._container.SetActive(pState);
	}

	// Token: 0x06004173 RID: 16755 RVA: 0x001BD3B8 File Offset: 0x001BB5B8
	private void clear()
	{
		this._button_left.clear();
		this._button_right.clear();
	}

	// Token: 0x06004174 RID: 16756 RVA: 0x001BD3D0 File Offset: 0x001BB5D0
	private NanoObject getElement(MetaSwitchManager.Direction pDirection)
	{
		int tIndex = this.getIndex(pDirection);
		return this._list[tIndex];
	}

	// Token: 0x06004175 RID: 16757 RVA: 0x001BD3F4 File Offset: 0x001BB5F4
	private int getIndex(MetaSwitchManager.Direction pDirection)
	{
		int tIndex = this.getCurrentMetaIndex();
		return Toolbox.loopIndex((pDirection == MetaSwitchManager.Direction.Left) ? (tIndex - 1) : (tIndex + 1), this._list.Count);
	}

	// Token: 0x06004176 RID: 16758 RVA: 0x001BD423 File Offset: 0x001BB623
	public static bool isAnimationActive()
	{
		return MetaSwitchManager._instance._tweener.IsActive();
	}

	// Token: 0x06004177 RID: 16759 RVA: 0x001BD434 File Offset: 0x001BB634
	public static bool isSwitcherEnabled()
	{
		return MetaSwitchManager._instance._is_enabled;
	}

	// Token: 0x06004178 RID: 16760 RVA: 0x001BD440 File Offset: 0x001BB640
	public static MetaSwitchButton getLeftbutton()
	{
		return MetaSwitchManager._instance._button_left;
	}

	// Token: 0x06004179 RID: 16761 RVA: 0x001BD44C File Offset: 0x001BB64C
	public static MetaSwitchButton getRightButton()
	{
		return MetaSwitchManager._instance._button_right;
	}

	// Token: 0x04002FB7 RID: 12215
	private const float POSITION_SHOW = 0f;

	// Token: 0x04002FB8 RID: 12216
	private const float POSITION_HIDE = -44f;

	// Token: 0x04002FB9 RID: 12217
	private const float WINDOW_MAX_SIZE_PERCENT = 100f;

	// Token: 0x04002FBA RID: 12218
	private const float WINDOW_SIZE_PORTRAIT_START = 100f;

	// Token: 0x04002FBB RID: 12219
	private const float WINDOW_SIZE_PORTRAIT_END = 115f;

	// Token: 0x04002FBC RID: 12220
	private const float WINDOW_SIZE_PORTRAIT_RATIO_MIN = 1.275f;

	// Token: 0x04002FBD RID: 12221
	private const float WINDOW_SIZE_PORTRAIT_RATIO_MAX = 1.45f;

	// Token: 0x04002FBE RID: 12222
	private const float ANIMATION_DURATION = 0.35f;

	// Token: 0x04002FBF RID: 12223
	[SerializeField]
	private MetaSwitchButton _button_left;

	// Token: 0x04002FC0 RID: 12224
	[SerializeField]
	private MetaSwitchButton _button_right;

	// Token: 0x04002FC1 RID: 12225
	[SerializeField]
	private Text _window_number_current;

	// Token: 0x04002FC2 RID: 12226
	[SerializeField]
	private Text _window_number_total;

	// Token: 0x04002FC3 RID: 12227
	[SerializeField]
	private GameObject _container;

	// Token: 0x04002FC4 RID: 12228
	private StatsWindow _stats_window;

	// Token: 0x04002FC5 RID: 12229
	private MetaTypeAsset _meta_type_asset;

	// Token: 0x04002FC6 RID: 12230
	private ListPool<NanoObject> _list;

	// Token: 0x04002FC7 RID: 12231
	private static MetaSwitchManager _instance;

	// Token: 0x04002FC8 RID: 12232
	private bool _is_switching_enabled;

	// Token: 0x04002FC9 RID: 12233
	private bool _was_just_opened;

	// Token: 0x04002FCA RID: 12234
	private bool _is_enabled;

	// Token: 0x04002FCB RID: 12235
	private Tweener _tweener;

	// Token: 0x02000B24 RID: 2852
	public enum Direction
	{
		// Token: 0x04003CC6 RID: 15558
		Left,
		// Token: 0x04003CC7 RID: 15559
		Right
	}
}
