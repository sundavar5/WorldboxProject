using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// Token: 0x02000615 RID: 1557
public class WindowToolbar : MonoBehaviour, IShakable
{
	// Token: 0x170002BF RID: 703
	// (get) Token: 0x06003314 RID: 13076 RVA: 0x00181B51 File Offset: 0x0017FD51
	public float shake_duration { get; } = 0.5f;

	// Token: 0x170002C0 RID: 704
	// (get) Token: 0x06003315 RID: 13077 RVA: 0x00181B59 File Offset: 0x0017FD59
	public float shake_strength { get; } = 8f;

	// Token: 0x170002C1 RID: 705
	// (get) Token: 0x06003316 RID: 13078 RVA: 0x00181B61 File Offset: 0x0017FD61
	// (set) Token: 0x06003317 RID: 13079 RVA: 0x00181B69 File Offset: 0x0017FD69
	public Tweener shake_tween { get; set; }

	// Token: 0x06003318 RID: 13080 RVA: 0x00181B74 File Offset: 0x0017FD74
	private void Awake()
	{
		WindowToolbar._instance = this;
		this._ui_mover = base.GetComponent<UiMover>();
		this._content = base.transform.FindRecursive("Content");
		this._canvas_group = base.GetComponent<CanvasGroup>();
		this._parent_rect = base.transform.parent.GetComponent<RectTransform>();
		ScrollWindow.addCallbackShow(delegate(string _)
		{
			this.toggleShow(true);
		});
		ScrollWindow.addCallbackClose(delegate
		{
			this.toggleShow(false);
		});
		OptionAsset tAsset = AssetManager.options_library.get("ui_size_windows");
		this._ui_size_min = (float)tAsset.min_value / 100f;
		this._ui_size_max = (float)tAsset.max_value / 100f;
		this._ui_mover.initPos.y = -6f;
		this._ui_mover.hidePos.y = -6f;
		Vector3 tToolbarPosition = base.transform.localPosition;
		tToolbarPosition.y = -6f;
		base.transform.localPosition = tToolbarPosition;
		foreach (PowerButton tButton in base.GetComponentsInChildren<PowerButton>())
		{
			if (tButton.type == PowerButtonType.Window)
			{
				this._window_buttons.Add(tButton.open_window_id, tButton.transform);
			}
		}
		ScrollWindow.addCallbackShow(new ScrollWindowNameAction(this.checkSelectOnWindowShow));
		ScrollWindow.addCallbackHide(new ScrollWindowNameAction(this.checkDeselectOnWindowHide));
	}

	// Token: 0x06003319 RID: 13081 RVA: 0x00181CD0 File Offset: 0x0017FED0
	private void Start()
	{
		CanvasMain.instance.addCallbackResize(new ResizeAction(this.onResize));
		CanvasMain.instance.addCallbackResizeUI(new ResizeUIAction(this.checkShow));
	}

	// Token: 0x0600331A RID: 13082 RVA: 0x00181CFE File Offset: 0x0017FEFE
	private void OnDisable()
	{
		((IShakable)this).killShakeTween();
	}

	// Token: 0x0600331B RID: 13083 RVA: 0x00181D08 File Offset: 0x0017FF08
	private void onResize(float pWidth, float pHeight)
	{
		float tUISize = (float)PlayerConfig.getOptionInt("ui_size_windows") / 100f;
		this.checkShow(tUISize);
	}

	// Token: 0x0600331C RID: 13084 RVA: 0x00181D2E File Offset: 0x0017FF2E
	private void checkShow(float pUISize)
	{
		if (this.isHideDistance(pUISize))
		{
			this.toggleShow(false);
			return;
		}
		if (!ScrollWindow.isWindowActive())
		{
			return;
		}
		this.toggleShow(true);
	}

	// Token: 0x0600331D RID: 13085 RVA: 0x00181D50 File Offset: 0x0017FF50
	public static void toggleActive(bool pState)
	{
		WindowToolbar._instance.toggleActiveInstance(pState);
	}

	// Token: 0x0600331E RID: 13086 RVA: 0x00181D60 File Offset: 0x0017FF60
	private void toggleActiveInstance(bool pState)
	{
		if (!pState)
		{
			this._ui_mover.setVisible(false, false, delegate
			{
				base.gameObject.SetActive(false);
			});
			return;
		}
		base.gameObject.SetActive(true);
		if (ScrollWindow.isWindowActive())
		{
			this.toggleShow(true);
			return;
		}
		this._ui_mover.setVisible(false, true, null);
	}

	// Token: 0x0600331F RID: 13087 RVA: 0x00181DB4 File Offset: 0x0017FFB4
	private void toggleShow(bool pState)
	{
		if (!base.gameObject.activeSelf)
		{
			return;
		}
		if (pState)
		{
			float tPercent = (float)PlayerConfig.getIntValue("ui_size_windows") / 100f;
			if (this.isHideDistance(tPercent))
			{
				pState = false;
			}
			else if (!AssetManager.window_library.get(ScrollWindow.getCurrentWindow().screen_id).window_toolbar_enabled)
			{
				pState = false;
			}
		}
		this._last_state = pState;
		this._canvas_group.blocksRaycasts = pState;
		if (pState)
		{
			this._content.gameObject.SetActive(true);
		}
		TweenCallback tCallback = null;
		if (!pState)
		{
			tCallback = delegate()
			{
				this._content.gameObject.SetActive(this._last_state);
			};
		}
		this._ui_mover.setVisible(pState, false, tCallback);
	}

	// Token: 0x06003320 RID: 13088 RVA: 0x00181E57 File Offset: 0x00180057
	public static void shake()
	{
		((IShakable)WindowToolbar._instance).shake();
	}

	// Token: 0x06003321 RID: 13089 RVA: 0x00181E63 File Offset: 0x00180063
	private bool isPortrait()
	{
		return false;
	}

	// Token: 0x06003322 RID: 13090 RVA: 0x00181E68 File Offset: 0x00180068
	private bool isHideDistance(float pUISize)
	{
		if (!ScrollWindow.isWindowActive())
		{
			return true;
		}
		float tUISizeRelative = Mathf.InverseLerp(this._ui_size_min, this._ui_size_max, pUISize);
		float tReferenceFix = (float)Screen.height / 1080f;
		float tDistance = Mathf.Lerp(225f, 440f, tUISizeRelative) * tReferenceFix;
		float xMin = this._windows_parent.WorldRect().xMin;
		float tToolbarParentLeftX = this._parent_rect.WorldRect().xMin;
		return xMin - tToolbarParentLeftX < tDistance;
	}

	// Token: 0x06003323 RID: 13091 RVA: 0x00181EE0 File Offset: 0x001800E0
	private void checkSelectOnWindowShow(string pWindowId)
	{
		Transform tButtonTransform;
		if (!this._window_buttons.TryGetValue(pWindowId, out tButtonTransform))
		{
			pWindowId = AssetManager.window_library.get(pWindowId).related_parent_window;
			if (string.IsNullOrEmpty(pWindowId))
			{
				return;
			}
			if (!this._window_buttons.TryGetValue(pWindowId, out tButtonTransform))
			{
				return;
			}
		}
		this._selector.SetParent(tButtonTransform);
		this._selector.localPosition = Vector3.zero;
		this._selector.localScale = Vector3.one;
		this.scrollToButton(tButtonTransform);
	}

	// Token: 0x06003324 RID: 13092 RVA: 0x00181F5C File Offset: 0x0018015C
	private void scrollToButton(Transform pButtonTransform)
	{
		Rect tRectOfScroll = this._scroll_rect.rectTransform.GetWorldRect();
		float tScrollTop = tRectOfScroll.yMax;
		Rect tRectOfContent = this._content_rect.GetWorldRect();
		float tContentHeight = tRectOfContent.height;
		Rect tButtonRect = ((RectTransform)pButtonTransform).GetWorldRect();
		float tButtonPosition = pButtonTransform.position.y - tRectOfContent.yMax + tScrollTop;
		Vector2 tError = new Vector2(0f, tButtonRect.height * 0.5f);
		if (tRectOfScroll.Contains(tButtonRect.position - tError) && tRectOfScroll.Contains(tButtonRect.max + tError))
		{
			return;
		}
		float tRequiredScrollValue = (tButtonPosition - tButtonRect.height / 0.5f) / tContentHeight;
		float tUISize = (float)PlayerConfig.getOptionInt("ui_size_windows") / 100f;
		float tUISizeRelative = 1f - Mathf.InverseLerp(this._ui_size_min, this._ui_size_max, tUISize);
		if (tRequiredScrollValue > 1f - (0.5f - tUISizeRelative))
		{
			tRequiredScrollValue = 1f;
		}
		else if (tRequiredScrollValue < 0.1f + tUISizeRelative)
		{
			tRequiredScrollValue = 0f;
		}
		DOTween.To(() => this._scroll_rect.verticalScrollbar.value, delegate(float pValue)
		{
			this._scroll_rect.verticalScrollbar.value = pValue;
		}, tRequiredScrollValue, 0.3f);
	}

	// Token: 0x06003325 RID: 13093 RVA: 0x0018209A File Offset: 0x0018029A
	private void checkDeselectOnWindowHide(string pWindowId)
	{
		this._selector.SetParent(this._selector_base_parent);
	}

	// Token: 0x06003327 RID: 13095 RVA: 0x001820D6 File Offset: 0x001802D6
	Transform IShakable.get_transform()
	{
		return base.transform;
	}

	// Token: 0x040026AC RID: 9900
	private const int REFERENCE_HEIGHT = 1080;

	// Token: 0x040026AD RID: 9901
	private const int HIDE_DISTANCE_MIN = 225;

	// Token: 0x040026AE RID: 9902
	private const int HIDE_DISTANCE_MAX = 440;

	// Token: 0x040026AF RID: 9903
	private const float FOCUS_SCROLL_TOP_ERROR = 0.5f;

	// Token: 0x040026B0 RID: 9904
	private const float FOCUS_SCROLL_BOTTOM_ERROR = 0.1f;

	// Token: 0x040026B1 RID: 9905
	private const float FOCUS_SCROLL_BUTTON_ERROR = 0.5f;

	// Token: 0x040026B2 RID: 9906
	private const float FOCUS_SCROLL_DURATION = 0.3f;

	// Token: 0x040026B3 RID: 9907
	private static WindowToolbar _instance;

	// Token: 0x040026B4 RID: 9908
	private UiMover _ui_mover;

	// Token: 0x040026B5 RID: 9909
	private Transform _content;

	// Token: 0x040026B6 RID: 9910
	private RectTransform _parent_rect;

	// Token: 0x040026B7 RID: 9911
	private CanvasGroup _canvas_group;

	// Token: 0x040026B8 RID: 9912
	[SerializeField]
	private ScrollRectExtended _scroll_rect;

	// Token: 0x040026B9 RID: 9913
	[SerializeField]
	private RectTransform _content_rect;

	// Token: 0x040026BA RID: 9914
	[SerializeField]
	private RectTransform _windows_parent;

	// Token: 0x040026BB RID: 9915
	[SerializeField]
	private Transform _selector_base_parent;

	// Token: 0x040026BC RID: 9916
	[SerializeField]
	private Transform _selector;

	// Token: 0x040026C0 RID: 9920
	private float _ui_size_min;

	// Token: 0x040026C1 RID: 9921
	private float _ui_size_max;

	// Token: 0x040026C2 RID: 9922
	private Dictionary<string, Transform> _window_buttons = new Dictionary<string, Transform>();

	// Token: 0x040026C3 RID: 9923
	public bool _last_state;
}
