using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

// Token: 0x02000482 RID: 1154
public static class RectTransformExtensions
{
	// Token: 0x0600278C RID: 10124 RVA: 0x00140130 File Offset: 0x0013E330
	public static global::ListPool<RectTransform> getLayoutChildren(this RectTransform pRect)
	{
		List<Component> tToIgnoreList = CollectionPool<List<Component>, Component>.Get();
		global::ListPool<RectTransform> tChildren = new global::ListPool<RectTransform>();
		int i = 0;
		int tLen = pRect.childCount;
		while (i < tLen)
		{
			RectTransform tChild = pRect.GetChild(i) as RectTransform;
			if (!(tChild == null) && tChild.gameObject.activeInHierarchy)
			{
				if (!tChild.HasComponent<ILayoutIgnorer>())
				{
					tChildren.Add(tChild);
				}
				else
				{
					tChild.GetComponents(typeof(ILayoutIgnorer), tToIgnoreList);
					if (tToIgnoreList.Count == 0)
					{
						tChildren.Add(tChild);
					}
					else
					{
						for (int j = 0; j < tToIgnoreList.Count; j++)
						{
							if (!((ILayoutIgnorer)tToIgnoreList[j]).ignoreLayout)
							{
								tChildren.Add(tChild);
								break;
							}
						}
						tToIgnoreList.Clear();
					}
				}
			}
			i++;
		}
		CollectionPool<List<Component>, Component>.Release(tToIgnoreList);
		return tChildren;
	}

	// Token: 0x0600278D RID: 10125 RVA: 0x001401FF File Offset: 0x0013E3FF
	public static void SetLeft(this RectTransform pRectTransform, float pLeft)
	{
		pRectTransform.offsetMin = new Vector2(pLeft, pRectTransform.offsetMin.y);
	}

	// Token: 0x0600278E RID: 10126 RVA: 0x00140218 File Offset: 0x0013E418
	public static void SetRight(this RectTransform pRectTransform, float pRight)
	{
		pRectTransform.offsetMax = new Vector2(-pRight, pRectTransform.offsetMax.y);
	}

	// Token: 0x0600278F RID: 10127 RVA: 0x00140232 File Offset: 0x0013E432
	public static void SetTop(this RectTransform pRectTransform, float pTop)
	{
		pRectTransform.offsetMax = new Vector2(pRectTransform.offsetMax.x, -pTop);
	}

	// Token: 0x06002790 RID: 10128 RVA: 0x0014024C File Offset: 0x0013E44C
	public static void SetBottom(this RectTransform pRectTransform, float pBottom)
	{
		pRectTransform.offsetMin = new Vector2(pRectTransform.offsetMin.x, pBottom);
	}

	// Token: 0x06002791 RID: 10129 RVA: 0x00140268 File Offset: 0x0013E468
	public static void SetAnchor(this RectTransform pSource, AnchorPresets pAlign, float pOffsetX = 0f, float pOffsetY = 0f)
	{
		pSource.anchoredPosition = new Vector3(pOffsetX, pOffsetY, 0f);
		switch (pAlign)
		{
		case AnchorPresets.TopLeft:
			pSource.anchorMin = new Vector2(0f, 1f);
			pSource.anchorMax = new Vector2(0f, 1f);
			return;
		case AnchorPresets.TopCenter:
			pSource.anchorMin = new Vector2(0.5f, 1f);
			pSource.anchorMax = new Vector2(0.5f, 1f);
			return;
		case AnchorPresets.TopRight:
			pSource.anchorMin = new Vector2(1f, 1f);
			pSource.anchorMax = new Vector2(1f, 1f);
			return;
		case AnchorPresets.MiddleLeft:
			pSource.anchorMin = new Vector2(0f, 0.5f);
			pSource.anchorMax = new Vector2(0f, 0.5f);
			return;
		case AnchorPresets.MiddleCenter:
			pSource.anchorMin = new Vector2(0.5f, 0.5f);
			pSource.anchorMax = new Vector2(0.5f, 0.5f);
			return;
		case AnchorPresets.MiddleRight:
			pSource.anchorMin = new Vector2(1f, 0.5f);
			pSource.anchorMax = new Vector2(1f, 0.5f);
			return;
		case AnchorPresets.BottomLeft:
			pSource.anchorMin = new Vector2(0f, 0f);
			pSource.anchorMax = new Vector2(0f, 0f);
			return;
		case AnchorPresets.BottonCenter:
			pSource.anchorMin = new Vector2(0.5f, 0f);
			pSource.anchorMax = new Vector2(0.5f, 0f);
			return;
		case AnchorPresets.BottomRight:
			pSource.anchorMin = new Vector2(1f, 0f);
			pSource.anchorMax = new Vector2(1f, 0f);
			return;
		case AnchorPresets.BottomStretch:
			break;
		case AnchorPresets.VertStretchLeft:
			pSource.anchorMin = new Vector2(0f, 0f);
			pSource.anchorMax = new Vector2(0f, 1f);
			return;
		case AnchorPresets.VertStretchRight:
			pSource.anchorMin = new Vector2(1f, 0f);
			pSource.anchorMax = new Vector2(1f, 1f);
			return;
		case AnchorPresets.VertStretchCenter:
			pSource.anchorMin = new Vector2(0.5f, 0f);
			pSource.anchorMax = new Vector2(0.5f, 1f);
			return;
		case AnchorPresets.HorStretchTop:
			pSource.anchorMin = new Vector2(0f, 1f);
			pSource.anchorMax = new Vector2(1f, 1f);
			return;
		case AnchorPresets.HorStretchMiddle:
			pSource.anchorMin = new Vector2(0f, 0.5f);
			pSource.anchorMax = new Vector2(1f, 0.5f);
			return;
		case AnchorPresets.HorStretchBottom:
			pSource.anchorMin = new Vector2(0f, 0f);
			pSource.anchorMax = new Vector2(1f, 0f);
			return;
		case AnchorPresets.StretchAll:
			pSource.anchorMin = new Vector2(0f, 0f);
			pSource.anchorMax = new Vector2(1f, 1f);
			break;
		default:
			return;
		}
	}

	// Token: 0x06002792 RID: 10130 RVA: 0x00140588 File Offset: 0x0013E788
	public static void SetPivot(this RectTransform pSource, PivotPresets pPreset, bool pKeepPosition = false)
	{
		Vector2 tNewPivot = Vector2.zero;
		switch (pPreset)
		{
		case PivotPresets.TopLeft:
			tNewPivot = new Vector2(0f, 1f);
			break;
		case PivotPresets.TopCenter:
			tNewPivot = new Vector2(0.5f, 1f);
			break;
		case PivotPresets.TopRight:
			tNewPivot = new Vector2(1f, 1f);
			break;
		case PivotPresets.MiddleLeft:
			tNewPivot = new Vector2(0f, 0.5f);
			break;
		case PivotPresets.MiddleCenter:
			tNewPivot = new Vector2(0.5f, 0.5f);
			break;
		case PivotPresets.MiddleRight:
			tNewPivot = new Vector2(1f, 0.5f);
			break;
		case PivotPresets.BottomLeft:
			tNewPivot = new Vector2(0f, 0f);
			break;
		case PivotPresets.BottomCenter:
			tNewPivot = new Vector2(0.5f, 0f);
			break;
		case PivotPresets.BottomRight:
			tNewPivot = new Vector2(1f, 0f);
			break;
		}
		if (!pKeepPosition)
		{
			pSource.pivot = tNewPivot;
			return;
		}
		Vector3 tDeltaPosition = pSource.pivot - tNewPivot;
		tDeltaPosition.Scale(pSource.rect.size);
		tDeltaPosition.Scale(pSource.localScale);
		tDeltaPosition = pSource.rotation * tDeltaPosition;
		pSource.pivot = tNewPivot;
		pSource.localPosition -= tDeltaPosition;
	}

	// Token: 0x06002793 RID: 10131 RVA: 0x001406E4 File Offset: 0x0013E8E4
	public static Vector2 GetWorldCenter(this RectTransform pRectTransform)
	{
		return pRectTransform.TransformPoint(pRectTransform.rect.center);
	}

	// Token: 0x06002794 RID: 10132 RVA: 0x00140710 File Offset: 0x0013E910
	public static Rect GetWorldRect(this RectTransform pRectTransform)
	{
		Rect tLocalRect = pRectTransform.rect;
		return new Rect
		{
			min = pRectTransform.TransformPoint(tLocalRect.min),
			max = pRectTransform.TransformPoint(tLocalRect.max)
		};
	}

	// Token: 0x06002795 RID: 10133 RVA: 0x0014076C File Offset: 0x0013E96C
	public static bool Overlaps(this RectTransform a, RectTransform b)
	{
		return a.WorldRect().Overlaps(b.WorldRect());
	}

	// Token: 0x06002796 RID: 10134 RVA: 0x00140790 File Offset: 0x0013E990
	public static bool Overlaps(this RectTransform a, RectTransform b, bool allowInverse)
	{
		return a.WorldRect().Overlaps(b.WorldRect(), allowInverse);
	}

	// Token: 0x06002797 RID: 10135 RVA: 0x001407B4 File Offset: 0x0013E9B4
	public static Rect WorldRect(this RectTransform rectTransform)
	{
		Vector2 sizeDelta = rectTransform.sizeDelta;
		float rectTransformWidth = sizeDelta.x * rectTransform.lossyScale.x;
		float rectTransformHeight = sizeDelta.y * rectTransform.lossyScale.y;
		Vector3 vector = rectTransform.TransformPoint(rectTransform.rect.center);
		float x = vector.x - rectTransformWidth * 0.5f;
		float y = vector.y - rectTransformHeight * 0.5f;
		return new Rect(x, y, rectTransformWidth, rectTransformHeight);
	}
}
