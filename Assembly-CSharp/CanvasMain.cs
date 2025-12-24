using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005D3 RID: 1491
public class CanvasMain : MonoBehaviour
{
	// Token: 0x060030F7 RID: 12535 RVA: 0x001781C0 File Offset: 0x001763C0
	private void Awake()
	{
		CanvasMain.instance = this;
		this.scaler_main_ui = this.canvas_ui.GetComponent<CanvasScaler>();
		this.scaler_windows_ui = this.canvas_windows.GetComponent<CanvasScaler>();
		this.scaler_tooltip = this.canvas_tooltip.GetComponent<CanvasScaler>();
		this.scaler_map_names = this.canvas_map_names.GetComponent<CanvasScaler>();
	}

	// Token: 0x060030F8 RID: 12536 RVA: 0x00178217 File Offset: 0x00176417
	public bool setMainUiEnabled(bool pEnabled)
	{
		if (this.canvas_ui.enabled == pEnabled)
		{
			return false;
		}
		this.canvas_ui.enabled = pEnabled;
		return true;
	}

	// Token: 0x060030F9 RID: 12537 RVA: 0x00178236 File Offset: 0x00176436
	public float getLastWidth()
	{
		return this.last_width;
	}

	// Token: 0x060030FA RID: 12538 RVA: 0x0017823E File Offset: 0x0017643E
	public float getLastHeight()
	{
		return this.last_height;
	}

	// Token: 0x060030FB RID: 12539 RVA: 0x00178246 File Offset: 0x00176446
	public void addCallbackResize(ResizeAction pAction)
	{
		this._on_resize = (ResizeAction)Delegate.Combine(this._on_resize, pAction);
	}

	// Token: 0x060030FC RID: 12540 RVA: 0x0017825F File Offset: 0x0017645F
	public void removeCallbackResize(ResizeAction pAction)
	{
		this._on_resize = (ResizeAction)Delegate.Remove(this._on_resize, pAction);
	}

	// Token: 0x060030FD RID: 12541 RVA: 0x00178278 File Offset: 0x00176478
	public void addCallbackResizeUI(ResizeUIAction pAction)
	{
		this._on_resize_ui = (ResizeUIAction)Delegate.Combine(this._on_resize_ui, pAction);
	}

	// Token: 0x060030FE RID: 12542 RVA: 0x00178291 File Offset: 0x00176491
	public void removeCallbackResizeUI(ResizeUIAction pAction)
	{
		this._on_resize_ui = (ResizeUIAction)Delegate.Remove(this._on_resize_ui, pAction);
	}

	// Token: 0x060030FF RID: 12543 RVA: 0x001782AA File Offset: 0x001764AA
	private void checkResize(float pWidth, float pHeight)
	{
		this.last_width = pWidth;
		this.last_height = pHeight;
		this.screenOrientation = Screen.orientation;
		this.resizeMainUI();
		this.resizeWindowsUI();
		this.resizeTooltipUI();
		ResizeAction on_resize = this._on_resize;
		if (on_resize == null)
		{
			return;
		}
		on_resize(pWidth, pHeight);
	}

	// Token: 0x06003100 RID: 12544 RVA: 0x001782EC File Offset: 0x001764EC
	public void resizeWindowsUI()
	{
		this.changeCanvasSize(this.scaler_windows_ui, "ui_size_windows", 285f, 420f);
		float tPercent = (float)PlayerConfig.getIntValue("ui_size_windows") / 100f;
		ResizeUIAction on_resize_ui = this._on_resize_ui;
		if (on_resize_ui == null)
		{
			return;
		}
		on_resize_ui(tPercent);
	}

	// Token: 0x06003101 RID: 12545 RVA: 0x00178337 File Offset: 0x00176537
	public void resizeTooltipUI()
	{
		this.changeCanvasSize(this.scaler_tooltip, "ui_size_tooltips", 285f, 420f);
	}

	// Token: 0x06003102 RID: 12546 RVA: 0x00178354 File Offset: 0x00176554
	public void resizeMapNames()
	{
		this.changeCanvasSize(this.scaler_map_names, "ui_size_map_names", 285f, 420f);
	}

	// Token: 0x06003103 RID: 12547 RVA: 0x00178374 File Offset: 0x00176574
	public void resizeMainUI()
	{
		float tReferenceRes;
		if (Screen.height > Screen.width)
		{
			tReferenceRes = 360f;
		}
		else
		{
			tReferenceRes = 500f;
		}
		this.changeCanvasSize(this.scaler_main_ui, "ui_size_main", 285f, tReferenceRes);
	}

	// Token: 0x06003104 RID: 12548 RVA: 0x001783B4 File Offset: 0x001765B4
	private void changeCanvasSize(CanvasScaler pScaler, string pSizeOption, float pReferenceWidth, float pReferenceHeight)
	{
		pScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
		float tPercent = (float)PlayerConfig.getIntValue(pSizeOption) / 100f;
		float tScale = 2f - tPercent;
		pScaler.referenceResolution = new Vector2(pReferenceWidth, pReferenceHeight * tScale);
	}

	// Token: 0x06003105 RID: 12549 RVA: 0x001783F4 File Offset: 0x001765F4
	private void Start()
	{
		this.screenOrientation = Screen.orientation;
	}

	// Token: 0x06003106 RID: 12550 RVA: 0x00178404 File Offset: 0x00176604
	private void Update()
	{
		if (CanvasMain.tooltip_show_timeout > 0f)
		{
			CanvasMain.tooltip_show_timeout -= Time.deltaTime;
		}
		if ((float)Screen.width != this.last_width || (float)Screen.height != this.last_height)
		{
			this.checkResize((float)Screen.width, (float)Screen.height);
		}
		if (this.screenOrientation != Screen.orientation)
		{
			this.screenOrientation = Screen.orientation;
			if (ScrollWindow.isWindowActive())
			{
				ScrollWindow.hideAllEvent(true);
			}
		}
		if (!Config.lockGameControls)
		{
			MapBox world = World.world;
			if (!(((world != null) ? world.stack_effects : null) != null) || !World.world.stack_effects.isLocked())
			{
				this.blocker.gameObject.SetActive(false);
				return;
			}
		}
		this.blocker.gameObject.SetActive(true);
	}

	// Token: 0x06003107 RID: 12551 RVA: 0x001784D4 File Offset: 0x001766D4
	public static void addTooltipShowTimeout(float pTime)
	{
		CanvasMain.tooltip_show_timeout = pTime;
		Tooltip.hideTooltipNow();
	}

	// Token: 0x06003108 RID: 12552 RVA: 0x001784E1 File Offset: 0x001766E1
	public static bool isBottomBarShowing()
	{
		return !ScrollWindow.isWindowActive() && !ControllableUnit.isControllingUnit() && !MoveCamera.inSpectatorMode() && !Config.ui_main_hidden && !SmoothLoader.isLoading() && !SaveManager.isLoadingSaveAnimationActive();
	}

	// Token: 0x06003109 RID: 12553 RVA: 0x0017851A File Offset: 0x0017671A
	public static bool isNameplatesAllowed()
	{
		return !SmoothLoader.isLoading() && !SaveManager.isLoadingSaveAnimationActive();
	}

	// Token: 0x040024F7 RID: 9463
	public static CanvasMain instance;

	// Token: 0x040024F8 RID: 9464
	public static float tooltip_show_timeout;

	// Token: 0x040024F9 RID: 9465
	public Canvas canvas_ui;

	// Token: 0x040024FA RID: 9466
	public Canvas canvas_windows;

	// Token: 0x040024FB RID: 9467
	public Canvas canvas_map_names;

	// Token: 0x040024FC RID: 9468
	public Canvas canvas_tooltip;

	// Token: 0x040024FD RID: 9469
	public Image blocker;

	// Token: 0x040024FE RID: 9470
	private ScreenOrientation screenOrientation;

	// Token: 0x040024FF RID: 9471
	private CanvasScaler scaler_main_ui;

	// Token: 0x04002500 RID: 9472
	private CanvasScaler scaler_windows_ui;

	// Token: 0x04002501 RID: 9473
	private CanvasScaler scaler_tooltip;

	// Token: 0x04002502 RID: 9474
	private CanvasScaler scaler_map_names;

	// Token: 0x04002503 RID: 9475
	public Transform transformWindows;

	// Token: 0x04002504 RID: 9476
	private float last_width = -1f;

	// Token: 0x04002505 RID: 9477
	private float last_height = -1f;

	// Token: 0x04002506 RID: 9478
	private const int REFERENCE_SIZE_X = 285;

	// Token: 0x04002507 RID: 9479
	private const int REFERENCE_SIZE_Y = 420;

	// Token: 0x04002508 RID: 9480
	private ResizeAction _on_resize;

	// Token: 0x04002509 RID: 9481
	private ResizeUIAction _on_resize_ui;
}
