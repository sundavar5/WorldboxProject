using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace LayoutGroupExt
{
	// Token: 0x0200098A RID: 2442
	[DisallowMultipleComponent]
	[ExecuteAlways]
	[RequireComponent(typeof(RectTransform))]
	public abstract class LayoutGroupExtended : LayoutGroup
	{
		// Token: 0x06004736 RID: 18230 RVA: 0x001E308C File Offset: 0x001E128C
		protected new void SetChildAlongAxis(RectTransform rect, int axis, float pos)
		{
			if (rect == null)
			{
				return;
			}
			this.SetChildAlongAxisWithScale(rect, axis, pos, 1f);
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x001E30A8 File Offset: 0x001E12A8
		public override void CalculateLayoutInputHorizontal()
		{
			if (this._skip_frame == Time.frameCount)
			{
				base.SetDirty();
				return;
			}
			bool tFirst = base.rectChildren.Count == 0;
			base.rectChildren.Clear();
			List<Component> toIgnoreList = CollectionPool<List<Component>, Component>.Get();
			for (int i = 0; i < base.rectTransform.childCount; i++)
			{
				if (Application.isPlaying && tFirst && base.rectChildren.Count == this.delayItems)
				{
					this._skip_frame = Time.frameCount;
					base.SetDirty();
					break;
				}
				RectTransform rect = base.rectTransform.GetChild(i) as RectTransform;
				if (!(rect == null) && rect.gameObject.activeInHierarchy)
				{
					rect.GetComponents(typeof(ILayoutIgnorer), toIgnoreList);
					if (toIgnoreList.Count == 0)
					{
						base.rectChildren.Add(rect);
					}
					else
					{
						foreach (Component component in toIgnoreList)
						{
							ILayoutIgnorer ignorer = (ILayoutIgnorer)component;
							if (!ignorer.ignoreLayout && ((MonoBehaviour)ignorer).enabled)
							{
								base.rectChildren.Add(rect);
								break;
							}
						}
					}
				}
			}
			CollectionPool<List<Component>, Component>.Release(toIgnoreList);
			this.m_Tracker.Clear();
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x001E3200 File Offset: 0x001E1400
		protected new void SetChildAlongAxisWithScale(RectTransform rect, int axis, float pos, float scaleFactor)
		{
			if (rect == null)
			{
				return;
			}
			this.m_Tracker.Add(this, rect, DrivenTransformProperties.Anchors | ((axis == 0) ? DrivenTransformProperties.AnchoredPositionX : DrivenTransformProperties.AnchoredPositionY));
			rect.anchorMin = Vector2.up;
			rect.anchorMax = Vector2.up;
			Vector2 anchoredPosition;
			if (!this.m_Grid_Anchors.TryGetValue(rect, out anchoredPosition) || !Application.isPlaying)
			{
				anchoredPosition = rect.anchoredPosition;
				this.m_Grid_Anchors[rect] = anchoredPosition;
			}
			anchoredPosition[axis] = ((axis == 0) ? (pos + rect.sizeDelta[axis] * rect.pivot[axis] * scaleFactor) : (-pos - rect.sizeDelta[axis] * (1f - rect.pivot[axis]) * scaleFactor));
			this.SetPosition(rect, anchoredPosition, axis);
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x001E32D7 File Offset: 0x001E14D7
		protected new void SetChildAlongAxis(RectTransform rect, int axis, float pos, float size)
		{
			if (rect == null)
			{
				return;
			}
			this.SetChildAlongAxisWithScale(rect, axis, pos, size, 1f);
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x001E32F4 File Offset: 0x001E14F4
		protected new void SetChildAlongAxisWithScale(RectTransform rect, int axis, float pos, float size, float scaleFactor)
		{
			if (rect == null)
			{
				return;
			}
			this.m_Tracker.Add(this, rect, DrivenTransformProperties.Anchors | ((axis == 0) ? (DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.SizeDeltaX) : (DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.SizeDeltaY)));
			rect.anchorMin = Vector2.up;
			rect.anchorMax = Vector2.up;
			Vector2 sizeDelta = rect.sizeDelta;
			sizeDelta[axis] = size;
			rect.sizeDelta = sizeDelta;
			Vector2 anchoredPosition;
			if (!this.m_Grid_Anchors.TryGetValue(rect, out anchoredPosition) || !Application.isPlaying)
			{
				anchoredPosition = rect.anchoredPosition;
				this.m_Grid_Anchors[rect] = anchoredPosition;
			}
			anchoredPosition[axis] = ((axis == 0) ? (pos + size * rect.pivot[axis] * scaleFactor) : (-pos - size * (1f - rect.pivot[axis]) * scaleFactor));
			this.SetPosition(rect, anchoredPosition, axis);
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x001E33D4 File Offset: 0x001E15D4
		public void SetPosition(RectTransform rect, Vector2 pos, int axis)
		{
			if (!Application.isPlaying)
			{
				rect.anchoredPosition = pos;
				return;
			}
			if (!this.m_Axis[axis].Contains(rect))
			{
				if (this.m_Axis[axis].Count >= this.delayItems)
				{
					Vector2 tClosest = Vector2.zero;
					float tMinDist = float.MaxValue;
					for (int i = this.m_Axis[axis].Count - 1; i >= 0; i--)
					{
						Vector2 tNeighbour = this.m_Axis[axis][i].anchoredPosition;
						if (tNeighbour == Vector2.zero)
						{
							tNeighbour = this.m_Grid_Anchors[this.m_Axis[axis][i]];
						}
						float tDist = Vector2.Distance(tNeighbour, pos);
						if (tDist < tMinDist)
						{
							tMinDist = tDist;
							tClosest = tNeighbour;
						}
					}
					Vector2 tDirection = tClosest - pos;
					if (Mathf.Abs(tDirection.y) > Mathf.Abs(tDirection.x))
					{
						rect.anchoredPosition = tClosest + new Vector2(0f, 1f);
					}
					else
					{
						rect.anchoredPosition = tClosest - new Vector2(1f, 0f);
					}
				}
				else
				{
					rect.anchoredPosition = pos;
				}
				this.m_Axis[axis].Add(rect);
				if (!this.m_Children.Contains(rect))
				{
					this.m_Children.Add(rect);
				}
			}
			this.m_Grid_Anchors[rect] = pos;
			Vector2 tOldAnchor = rect.anchoredPosition;
			rect.anchoredPosition = pos;
			this.m_Grid_Positions[rect] = rect.position;
			rect.anchoredPosition = tOldAnchor;
			if (this.m_Children.Count != this.m_Positions.Length)
			{
				this.m_Positions = new Vector2[this.m_Children.Count];
				this.m_Sort = new RectTransform[this.m_Children.Count];
			}
			this.m_Children.Sort((RectTransform a, RectTransform b) => a.GetSiblingIndex().CompareTo(b.GetSiblingIndex()));
			for (int j = 0; j < this.m_Children.Count; j++)
			{
				Vector2 tNewPos = this.m_Grid_Positions[this.m_Children[j]];
				this.m_Positions[j] = tNewPos;
				this.m_Sort[j] = this.m_Children[j];
			}
			Dictionary<RectTransform, TweenerCore<Vector2, Vector2, VectorOptions>> dict;
			TweenerCore<Vector2, Vector2, VectorOptions> tween;
			if (axis != 0)
			{
				if (axis != 1)
				{
					return;
				}
				dict = this.RectPositionYTweens;
				TweenerCore<Vector2, Vector2, VectorOptions> oldTweenY;
				if (dict.TryGetValue(rect, out oldTweenY) && oldTweenY.IsActive())
				{
					if (Mathf.Approximately(oldTweenY.endValue.y, pos.y))
					{
						return;
					}
					oldTweenY.Kill(false);
				}
				if (Mathf.Approximately(rect.anchoredPosition.y, pos.y))
				{
					return;
				}
				tween = rect.DOAnchorPosY(pos.y, this.moveDuration, false);
			}
			else
			{
				dict = this.RectPositionXTweens;
				TweenerCore<Vector2, Vector2, VectorOptions> oldTweenX;
				if (dict.TryGetValue(rect, out oldTweenX) && oldTweenX.IsActive())
				{
					if (Mathf.Approximately(oldTweenX.endValue.x, pos.x))
					{
						return;
					}
					oldTweenX.Kill(false);
				}
				if (Mathf.Approximately(rect.anchoredPosition.x, pos.x))
				{
					return;
				}
				tween = rect.DOAnchorPosX(pos.x, this.moveDuration, false);
			}
			TweenerCore<Vector2, Vector2, VectorOptions> tween3 = tween;
			tween3.onKill = (TweenCallback)Delegate.Combine(tween3.onKill, new TweenCallback(delegate()
			{
				if (dict.ContainsKey(rect) && dict[rect] == tween)
				{
					dict.Remove(rect);
				}
			}));
			TweenerCore<Vector2, Vector2, VectorOptions> tween2 = tween;
			tween2.onComplete = (TweenCallback)Delegate.Combine(tween2.onComplete, new TweenCallback(delegate()
			{
				LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
				dict.Remove(rect);
			}));
			dict[rect] = tween;
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x001E381A File Offset: 0x001E1A1A
		protected override void OnEnable()
		{
			base.OnEnable();
			ScrollWindow.addCallbackShow(new ScrollWindowNameAction(this.setDirty));
			ScrollWindow.addCallbackShowFinished(new ScrollWindowNameAction(this.setDirty));
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x001E3844 File Offset: 0x001E1A44
		protected override void OnDisable()
		{
			base.OnDisable();
			ScrollWindow.removeCallbackShow(new ScrollWindowNameAction(this.setDirty));
			ScrollWindow.removeCallbackShowFinished(new ScrollWindowNameAction(this.setDirty));
			this._skip_frame = -1;
			this.m_Axis[0].Clear();
			this.m_Axis[1].Clear();
			this.m_Children.Clear();
			this.m_Grid_Positions.Clear();
			this.m_Grid_Anchors.Clear();
			base.rectChildren.Clear();
			using (global::ListPool<TweenerCore<Vector2, Vector2, VectorOptions>> tTweenPool = new global::ListPool<TweenerCore<Vector2, Vector2, VectorOptions>>(this.RectPositionXTweens.Count + this.RectPositionYTweens.Count))
			{
				tTweenPool.AddRange(this.RectPositionXTweens.Values);
				tTweenPool.AddRange(this.RectPositionYTweens.Values);
				this.RectPositionXTweens.Clear();
				this.RectPositionYTweens.Clear();
				foreach (TweenerCore<Vector2, Vector2, VectorOptions> ptr in tTweenPool)
				{
					ptr.Kill(false);
				}
			}
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x001E397C File Offset: 0x001E1B7C
		private void LateUpdate()
		{
			foreach (RectTransform tChild in this.m_Children)
			{
				if (!base.rectChildren.Contains(tChild) || !tChild.gameObject.activeInHierarchy)
				{
					LayoutGroupExtended._to_remove.Add(tChild);
				}
			}
			foreach (RectTransform tChild2 in LayoutGroupExtended._to_remove)
			{
				this.m_Children.Remove(tChild2);
				this.m_Axis[0].Remove(tChild2);
				this.m_Axis[1].Remove(tChild2);
				this.m_Grid_Positions.Remove(tChild2);
				this.m_Grid_Anchors.Remove(tChild2);
			}
			LayoutGroupExtended._to_remove.Clear();
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x001E3A80 File Offset: 0x001E1C80
		private void setDirty(string pWindowName)
		{
			base.SetDirty();
		}

		// Token: 0x06004740 RID: 18240 RVA: 0x001E3A88 File Offset: 0x001E1C88
		private void DebugInit()
		{
			if (LayoutGroupExtended._highlighter_prefab == null)
			{
				LayoutGroupExtended._highlighter_prefab = Object.Instantiate<RectTransform>(Resources.Load<RectTransform>("ui/selector"));
			}
			if (this._pool_highlighter == null)
			{
				this._pool_highlighter = new ObjectPoolGenericMono<RectTransform>(LayoutGroupExtended._highlighter_prefab, base.transform);
			}
			this._pool_highlighter.clear(true);
		}

		// Token: 0x06004741 RID: 18241 RVA: 0x001E3AE0 File Offset: 0x001E1CE0
		protected virtual void Update()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			if (DebugConfig.isOn(DebugOption.ShowLayoutGroupGrid))
			{
				this.DebugInit();
				for (int i = 0; i < this.m_Positions.Length; i++)
				{
					Vector2 position = this.m_Positions[i];
					RectTransform next = this._pool_highlighter.getNext();
					next.localScale = this.m_Children[0].localScale;
					next.GetChild(0).GetComponent<Image>().color = new Color(1f, 0f, 0f, 0.25f);
					Object gameObject = next.gameObject;
					string str = "m_positions ";
					string str2 = i.ToString();
					string str3 = " ";
					Vector2 vector = position;
					gameObject.name = str + str2 + str3 + vector.ToString();
					next.position = position;
				}
				for (int j = 0; j < this.m_Sort.Length; j++)
				{
					RectTransform tRect = this.m_Sort[j];
					Vector2 position2 = this.m_Grid_Anchors[tRect];
					RectTransform next2 = this._pool_highlighter.getNext();
					next2.localScale = this.m_Children[0].localScale;
					next2.GetChild(0).GetComponent<Image>().color = new Color(0f, 1f, 0f, 0.25f);
					Object gameObject2 = next2.gameObject;
					string str4 = "m_Grid_Anchors ";
					string str5 = j.ToString();
					string str6 = " ";
					Vector2 vector = position2;
					gameObject2.name = str4 + str5 + str6 + vector.ToString();
					next2.anchoredPosition = position2;
				}
				for (int k = 0; k < this.m_Sort.Length; k++)
				{
					RectTransform tRect2 = this.m_Sort[k];
					Vector2 position3 = this.m_Grid_Positions[tRect2];
					RectTransform next3 = this._pool_highlighter.getNext();
					next3.localScale = this.m_Children[0].localScale;
					next3.GetChild(0).GetComponent<Image>().color = new Color(0f, 0f, 1f, 0.25f);
					Object gameObject3 = next3.gameObject;
					string str7 = "m_Grid_Positions ";
					string str8 = k.ToString();
					string str9 = " ";
					Vector2 vector = position3;
					gameObject3.name = str7 + str8 + str9 + vector.ToString();
					next3.position = position3;
				}
				return;
			}
			ObjectPoolGenericMono<RectTransform> pool_highlighter = this._pool_highlighter;
			if (pool_highlighter == null)
			{
				return;
			}
			pool_highlighter.clear(true);
		}

		// Token: 0x04003215 RID: 12821
		[SerializeField]
		public float moveDuration = 0.15f;

		// Token: 0x04003216 RID: 12822
		[Tooltip("Will position the n items immediately, animating the next ones.")]
		[SerializeField]
		public int delayItems = 1;

		// Token: 0x04003217 RID: 12823
		private Dictionary<RectTransform, TweenerCore<Vector2, Vector2, VectorOptions>> RectPositionXTweens = new Dictionary<RectTransform, TweenerCore<Vector2, Vector2, VectorOptions>>();

		// Token: 0x04003218 RID: 12824
		private Dictionary<RectTransform, TweenerCore<Vector2, Vector2, VectorOptions>> RectPositionYTweens = new Dictionary<RectTransform, TweenerCore<Vector2, Vector2, VectorOptions>>();

		// Token: 0x04003219 RID: 12825
		private static List<RectTransform> _to_remove = new List<RectTransform>();

		// Token: 0x0400321A RID: 12826
		internal List<RectTransform> m_Children = new List<RectTransform>();

		// Token: 0x0400321B RID: 12827
		internal Dictionary<int, List<RectTransform>> m_Axis = new Dictionary<int, List<RectTransform>>
		{
			{
				0,
				new List<RectTransform>()
			},
			{
				1,
				new List<RectTransform>()
			}
		};

		// Token: 0x0400321C RID: 12828
		internal Vector2[] m_Positions = new Vector2[0];

		// Token: 0x0400321D RID: 12829
		internal RectTransform[] m_Sort = new RectTransform[0];

		// Token: 0x0400321E RID: 12830
		internal Dictionary<RectTransform, Vector2> m_Grid_Positions = new Dictionary<RectTransform, Vector2>();

		// Token: 0x0400321F RID: 12831
		internal Dictionary<RectTransform, Vector2> m_Grid_Anchors = new Dictionary<RectTransform, Vector2>();

		// Token: 0x04003220 RID: 12832
		private int _skip_frame = -1;

		// Token: 0x04003221 RID: 12833
		private static RectTransform _highlighter_prefab;

		// Token: 0x04003222 RID: 12834
		private ObjectPoolGenericMono<RectTransform> _pool_highlighter;
	}
}
