using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020005D4 RID: 1492
[RequireComponent(typeof(RectTransform))]
public class CanvasNotch : MonoBehaviour
{
	// Token: 0x0600310B RID: 12555 RVA: 0x00178550 File Offset: 0x00176750
	private void Awake()
	{
		this._canvas = base.gameObject.transform.GetComponentInParent<Canvas>();
		this.safeAreaTransform = base.GetComponent<RectTransform>();
		if (!this.screenChangeVarsInitialized)
		{
			this.lastOrientation = Screen.orientation;
			this.lastResolution.x = (float)Screen.width;
			this.lastResolution.y = (float)Screen.height;
			this.lastSafeArea = Screen.safeArea;
			this.screenChangeVarsInitialized = true;
		}
	}

	// Token: 0x0600310C RID: 12556 RVA: 0x001785C6 File Offset: 0x001767C6
	private void Start()
	{
		this.ApplySafeArea();
	}

	// Token: 0x0600310D RID: 12557 RVA: 0x001785D0 File Offset: 0x001767D0
	private void Update()
	{
		if (Application.isMobilePlatform && Screen.orientation != this.lastOrientation)
		{
			this.OrientationChanged();
		}
		if (Screen.safeArea != this.lastSafeArea)
		{
			this.SafeAreaChanged();
		}
		if (this._canvas != null && this._canvas.pixelRect != this.lastCanvasRect)
		{
			this.CanvasChanged();
		}
		if ((float)Screen.width != this.lastResolution.x || (float)Screen.height != this.lastResolution.y)
		{
			this.ResolutionChanged();
		}
		if (!this.ranFirstTime)
		{
			this.ApplySafeArea();
		}
	}

	// Token: 0x0600310E RID: 12558 RVA: 0x00178678 File Offset: 0x00176878
	private void ApplySafeArea()
	{
		if (this._canvas == null)
		{
			return;
		}
		if (this.safeAreaTransform == null)
		{
			return;
		}
		this.ranFirstTime = true;
		Rect safeArea = Screen.safeArea;
		Rect tScreen = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		Vector2 tMinDiff = safeArea.min - tScreen.min;
		Vector2 tMaxDiff = safeArea.max - tScreen.max;
		safeArea.min -= tMaxDiff;
		safeArea.max -= tMinDiff;
		Vector2 anchorMin = safeArea.position;
		Vector2 anchorMax = safeArea.position + safeArea.size;
		anchorMin.x /= this._canvas.pixelRect.width;
		anchorMin.y /= this._canvas.pixelRect.height;
		anchorMax.x /= this._canvas.pixelRect.width;
		anchorMax.y /= this._canvas.pixelRect.height;
		this.safeAreaTransform.anchorMin = anchorMin;
		this.safeAreaTransform.anchorMax = anchorMax;
	}

	// Token: 0x0600310F RID: 12559 RVA: 0x001787CF File Offset: 0x001769CF
	private void OrientationChanged()
	{
		this.lastOrientation = Screen.orientation;
		this.lastResolution.x = (float)Screen.width;
		this.lastResolution.y = (float)Screen.height;
		this.ApplySafeArea();
	}

	// Token: 0x06003110 RID: 12560 RVA: 0x00178804 File Offset: 0x00176A04
	private void ResolutionChanged()
	{
		this.lastResolution.x = (float)Screen.width;
		this.lastResolution.y = (float)Screen.height;
		this.ApplySafeArea();
	}

	// Token: 0x06003111 RID: 12561 RVA: 0x0017882E File Offset: 0x00176A2E
	private void SafeAreaChanged()
	{
		this.lastSafeArea = Screen.safeArea;
		this.ApplySafeArea();
	}

	// Token: 0x06003112 RID: 12562 RVA: 0x00178841 File Offset: 0x00176A41
	private void CanvasChanged()
	{
		this.lastCanvasRect = this._canvas.pixelRect;
		this.ApplySafeArea();
	}

	// Token: 0x06003113 RID: 12563 RVA: 0x0017885C File Offset: 0x00176A5C
	private void debugConsole()
	{
		Dictionary<string, Rect> sizes = new Dictionary<string, Rect>();
		Debug.Log("amount of cutouts: " + Screen.cutouts.Length.ToString());
		sizes["screen"] = new Rect(0f, 0f, (float)Screen.width, (float)Screen.height);
		sizes["safearea"] = Screen.safeArea;
		foreach (string screenId in sizes.Keys)
		{
			Debug.Log(string.Concat(new string[]
			{
				"[o] ",
				screenId,
				": x:",
				sizes[screenId].x.ToString(),
				", y:",
				sizes[screenId].y.ToString(),
				", w:",
				sizes[screenId].width.ToString(),
				", h:",
				sizes[screenId].height.ToString()
			}));
		}
		if (this._canvas == null)
		{
			Debug.Log("canvas not ready");
			return;
		}
		foreach (string screenId2 in sizes.Keys)
		{
			Debug.Log(string.Concat(new string[]
			{
				"[c] ",
				screenId2,
				": x:",
				(sizes[screenId2].x / this._canvas.scaleFactor).ToString(),
				", y:",
				(sizes[screenId2].y / this._canvas.scaleFactor).ToString(),
				", w:",
				(sizes[screenId2].width / this._canvas.scaleFactor).ToString(),
				", h:",
				(sizes[screenId2].height / this._canvas.scaleFactor).ToString()
			}));
		}
	}

	// Token: 0x0400250A RID: 9482
	private bool screenChangeVarsInitialized;

	// Token: 0x0400250B RID: 9483
	private bool ranFirstTime;

	// Token: 0x0400250C RID: 9484
	private ScreenOrientation lastOrientation = ScreenOrientation.AutoRotation;

	// Token: 0x0400250D RID: 9485
	private Vector2 lastResolution = Vector2.zero;

	// Token: 0x0400250E RID: 9486
	private Rect lastSafeArea = Rect.zero;

	// Token: 0x0400250F RID: 9487
	private Rect lastCanvasRect = Rect.zero;

	// Token: 0x04002510 RID: 9488
	private RectTransform safeAreaTransform;

	// Token: 0x04002511 RID: 9489
	private Canvas _canvas;
}
