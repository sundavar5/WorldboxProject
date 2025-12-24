using System;
using UnityEngine;
using UnityEngine.UI;

namespace LayoutGroupExt
{
	// Token: 0x02000989 RID: 2441
	[ExecuteAlways]
	public abstract class HorizontalOrVerticalLayoutGroupExtended : LayoutGroupExtended
	{
		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06004722 RID: 18210 RVA: 0x001E2A97 File Offset: 0x001E0C97
		// (set) Token: 0x06004723 RID: 18211 RVA: 0x001E2A9F File Offset: 0x001E0C9F
		public float spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<float>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06004724 RID: 18212 RVA: 0x001E2AAE File Offset: 0x001E0CAE
		// (set) Token: 0x06004725 RID: 18213 RVA: 0x001E2AB6 File Offset: 0x001E0CB6
		public bool childForceExpandWidth
		{
			get
			{
				return this.m_ChildForceExpandWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandWidth, value);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06004726 RID: 18214 RVA: 0x001E2AC5 File Offset: 0x001E0CC5
		// (set) Token: 0x06004727 RID: 18215 RVA: 0x001E2ACD File Offset: 0x001E0CCD
		public bool childForceExpandHeight
		{
			get
			{
				return this.m_ChildForceExpandHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildForceExpandHeight, value);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06004728 RID: 18216 RVA: 0x001E2ADC File Offset: 0x001E0CDC
		// (set) Token: 0x06004729 RID: 18217 RVA: 0x001E2AE4 File Offset: 0x001E0CE4
		public bool childControlWidth
		{
			get
			{
				return this.m_ChildControlWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildControlWidth, value);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600472A RID: 18218 RVA: 0x001E2AF3 File Offset: 0x001E0CF3
		// (set) Token: 0x0600472B RID: 18219 RVA: 0x001E2AFB File Offset: 0x001E0CFB
		public bool childControlHeight
		{
			get
			{
				return this.m_ChildControlHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildControlHeight, value);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x0600472C RID: 18220 RVA: 0x001E2B0A File Offset: 0x001E0D0A
		// (set) Token: 0x0600472D RID: 18221 RVA: 0x001E2B12 File Offset: 0x001E0D12
		public bool childScaleWidth
		{
			get
			{
				return this.m_ChildScaleWidth;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildScaleWidth, value);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x0600472E RID: 18222 RVA: 0x001E2B21 File Offset: 0x001E0D21
		// (set) Token: 0x0600472F RID: 18223 RVA: 0x001E2B29 File Offset: 0x001E0D29
		public bool childScaleHeight
		{
			get
			{
				return this.m_ChildScaleHeight;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ChildScaleHeight, value);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06004730 RID: 18224 RVA: 0x001E2B38 File Offset: 0x001E0D38
		// (set) Token: 0x06004731 RID: 18225 RVA: 0x001E2B40 File Offset: 0x001E0D40
		public bool reverseArrangement
		{
			get
			{
				return this.m_ReverseArrangement;
			}
			set
			{
				base.SetProperty<bool>(ref this.m_ReverseArrangement, value);
			}
		}

		// Token: 0x06004732 RID: 18226 RVA: 0x001E2B50 File Offset: 0x001E0D50
		protected void CalcAlongAxis(int axis, bool isVertical)
		{
			float combinedPadding = (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical);
			bool controlSize = (axis == 0) ? this.m_ChildControlWidth : this.m_ChildControlHeight;
			bool useScale = (axis == 0) ? this.m_ChildScaleWidth : this.m_ChildScaleHeight;
			bool childForceExpandSize = (axis == 0) ? this.m_ChildForceExpandWidth : this.m_ChildForceExpandHeight;
			float totalMin = combinedPadding;
			float totalPreferred = combinedPadding;
			float totalFlexible = 0f;
			bool alongOtherAxis = isVertical ^ axis == 1;
			int rectChildrenCount = base.rectChildren.Count;
			for (int i = 0; i < rectChildrenCount; i++)
			{
				RectTransform child = base.rectChildren[i];
				float min;
				float preferred;
				float flexible;
				this.GetChildSizes(child, axis, controlSize, childForceExpandSize, out min, out preferred, out flexible);
				if (useScale)
				{
					float scaleFactor = child.localScale[axis];
					min *= scaleFactor;
					preferred *= scaleFactor;
					flexible *= scaleFactor;
				}
				if (alongOtherAxis)
				{
					totalMin = Mathf.Max(min + combinedPadding, totalMin);
					totalPreferred = Mathf.Max(preferred + combinedPadding, totalPreferred);
					totalFlexible = Mathf.Max(flexible, totalFlexible);
				}
				else
				{
					totalMin += min + this.spacing;
					totalPreferred += preferred + this.spacing;
					totalFlexible += flexible;
				}
			}
			if (!alongOtherAxis && base.rectChildren.Count > 0)
			{
				totalMin -= this.spacing;
				totalPreferred -= this.spacing;
			}
			totalPreferred = Mathf.Max(totalMin, totalPreferred);
			base.SetLayoutInputForAxis(totalMin, totalPreferred, totalFlexible, axis);
		}

		// Token: 0x06004733 RID: 18227 RVA: 0x001E2CC4 File Offset: 0x001E0EC4
		protected void SetChildrenAlongAxis(int axis, bool isVertical)
		{
			float size = base.rectTransform.rect.size[axis];
			bool controlSize = (axis == 0) ? this.m_ChildControlWidth : this.m_ChildControlHeight;
			bool useScale = (axis == 0) ? this.m_ChildScaleWidth : this.m_ChildScaleHeight;
			bool childForceExpandSize = (axis == 0) ? this.m_ChildForceExpandWidth : this.m_ChildForceExpandHeight;
			float alignmentOnAxis = base.GetAlignmentOnAxis(axis);
			bool flag = isVertical ^ axis == 1;
			int startIndex = this.m_ReverseArrangement ? (base.rectChildren.Count - 1) : 0;
			int endIndex = this.m_ReverseArrangement ? 0 : base.rectChildren.Count;
			int increment = this.m_ReverseArrangement ? -1 : 1;
			if (flag)
			{
				float innerSize = size - (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical);
				int i = startIndex;
				while (this.m_ReverseArrangement ? (i >= endIndex) : (i < endIndex))
				{
					RectTransform child = base.rectChildren[i];
					float min;
					float preferred;
					float flexible;
					this.GetChildSizes(child, axis, controlSize, childForceExpandSize, out min, out preferred, out flexible);
					float scaleFactor = useScale ? child.localScale[axis] : 1f;
					float requiredSpace = Mathf.Clamp(innerSize, min, (flexible > 0f) ? size : preferred);
					float startOffset = base.GetStartOffset(axis, requiredSpace * scaleFactor);
					if (controlSize)
					{
						base.SetChildAlongAxisWithScale(child, axis, startOffset, requiredSpace, scaleFactor);
					}
					else
					{
						float offsetInCell = (requiredSpace - child.sizeDelta[axis]) * alignmentOnAxis;
						base.SetChildAlongAxisWithScale(child, axis, startOffset + offsetInCell, scaleFactor);
					}
					i += increment;
				}
				return;
			}
			float pos = (float)((axis == 0) ? base.padding.left : base.padding.top);
			float itemFlexibleMultiplier = 0f;
			float surplusSpace = size - base.GetTotalPreferredSize(axis);
			if (surplusSpace > 0f)
			{
				if (base.GetTotalFlexibleSize(axis) == 0f)
				{
					pos = base.GetStartOffset(axis, base.GetTotalPreferredSize(axis) - (float)((axis == 0) ? base.padding.horizontal : base.padding.vertical));
				}
				else if (base.GetTotalFlexibleSize(axis) > 0f)
				{
					itemFlexibleMultiplier = surplusSpace / base.GetTotalFlexibleSize(axis);
				}
			}
			float minMaxLerp = 0f;
			if (base.GetTotalMinSize(axis) != base.GetTotalPreferredSize(axis))
			{
				minMaxLerp = Mathf.Clamp01((size - base.GetTotalMinSize(axis)) / (base.GetTotalPreferredSize(axis) - base.GetTotalMinSize(axis)));
			}
			int j = startIndex;
			while (this.m_ReverseArrangement ? (j >= endIndex) : (j < endIndex))
			{
				RectTransform child2 = base.rectChildren[j];
				float min2;
				float preferred2;
				float flexible2;
				this.GetChildSizes(child2, axis, controlSize, childForceExpandSize, out min2, out preferred2, out flexible2);
				float scaleFactor2 = useScale ? child2.localScale[axis] : 1f;
				float childSize = Mathf.Lerp(min2, preferred2, minMaxLerp);
				childSize += flexible2 * itemFlexibleMultiplier;
				if (controlSize)
				{
					base.SetChildAlongAxisWithScale(child2, axis, pos, childSize, scaleFactor2);
				}
				else
				{
					float offsetInCell2 = (childSize - child2.sizeDelta[axis]) * alignmentOnAxis;
					base.SetChildAlongAxisWithScale(child2, axis, pos + offsetInCell2, scaleFactor2);
				}
				pos += childSize * scaleFactor2 + this.spacing;
				j += increment;
			}
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x001E3004 File Offset: 0x001E1204
		private void GetChildSizes(RectTransform child, int axis, bool controlSize, bool childForceExpand, out float min, out float preferred, out float flexible)
		{
			if (!controlSize)
			{
				min = child.sizeDelta[axis];
				preferred = min;
				flexible = 0f;
			}
			else
			{
				min = LayoutUtility.GetMinSize(child, axis);
				preferred = LayoutUtility.GetPreferredSize(child, axis);
				flexible = LayoutUtility.GetFlexibleSize(child, axis);
			}
			if (childForceExpand)
			{
				flexible = Mathf.Max(flexible, 1f);
			}
		}

		// Token: 0x0400320D RID: 12813
		[SerializeField]
		protected float m_Spacing;

		// Token: 0x0400320E RID: 12814
		[SerializeField]
		protected bool m_ChildForceExpandWidth = true;

		// Token: 0x0400320F RID: 12815
		[SerializeField]
		protected bool m_ChildForceExpandHeight = true;

		// Token: 0x04003210 RID: 12816
		[SerializeField]
		protected bool m_ChildControlWidth = true;

		// Token: 0x04003211 RID: 12817
		[SerializeField]
		protected bool m_ChildControlHeight = true;

		// Token: 0x04003212 RID: 12818
		[SerializeField]
		protected bool m_ChildScaleWidth;

		// Token: 0x04003213 RID: 12819
		[SerializeField]
		protected bool m_ChildScaleHeight;

		// Token: 0x04003214 RID: 12820
		[SerializeField]
		protected bool m_ReverseArrangement;
	}
}
