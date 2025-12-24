using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace LayoutGroupExt
{
	// Token: 0x02000987 RID: 2439
	[AddComponentMenu("Layout/Grid Layout Group ( Extended )", 152)]
	public class GridLayoutGroupExtended : LayoutGroupExtended
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06004708 RID: 18184 RVA: 0x001E2204 File Offset: 0x001E0404
		// (set) Token: 0x06004709 RID: 18185 RVA: 0x001E220C File Offset: 0x001E040C
		public GridLayoutGroupExtended.Corner startCorner
		{
			get
			{
				return this.m_StartCorner;
			}
			set
			{
				base.SetProperty<GridLayoutGroupExtended.Corner>(ref this.m_StartCorner, value);
			}
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x0600470A RID: 18186 RVA: 0x001E221B File Offset: 0x001E041B
		// (set) Token: 0x0600470B RID: 18187 RVA: 0x001E2223 File Offset: 0x001E0423
		public GridLayoutGroupExtended.Axis startAxis
		{
			get
			{
				return this.m_StartAxis;
			}
			set
			{
				base.SetProperty<GridLayoutGroupExtended.Axis>(ref this.m_StartAxis, value);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x0600470C RID: 18188 RVA: 0x001E2232 File Offset: 0x001E0432
		// (set) Token: 0x0600470D RID: 18189 RVA: 0x001E223A File Offset: 0x001E043A
		public Vector2 cellSize
		{
			get
			{
				return this.m_CellSize;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_CellSize, value);
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600470E RID: 18190 RVA: 0x001E2249 File Offset: 0x001E0449
		// (set) Token: 0x0600470F RID: 18191 RVA: 0x001E2251 File Offset: 0x001E0451
		public Vector2 spacing
		{
			get
			{
				return this.m_Spacing;
			}
			set
			{
				base.SetProperty<Vector2>(ref this.m_Spacing, value);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06004710 RID: 18192 RVA: 0x001E2260 File Offset: 0x001E0460
		// (set) Token: 0x06004711 RID: 18193 RVA: 0x001E2268 File Offset: 0x001E0468
		public GridLayoutGroupExtended.Constraint constraint
		{
			get
			{
				return this.m_Constraint;
			}
			set
			{
				base.SetProperty<GridLayoutGroupExtended.Constraint>(ref this.m_Constraint, value);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06004712 RID: 18194 RVA: 0x001E2277 File Offset: 0x001E0477
		// (set) Token: 0x06004713 RID: 18195 RVA: 0x001E227F File Offset: 0x001E047F
		public int constraintCount
		{
			get
			{
				return this.m_ConstraintCount;
			}
			set
			{
				base.SetProperty<int>(ref this.m_ConstraintCount, Mathf.Max(1, value));
			}
		}

		// Token: 0x06004714 RID: 18196 RVA: 0x001E2294 File Offset: 0x001E0494
		protected GridLayoutGroupExtended()
		{
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x001E22D0 File Offset: 0x001E04D0
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			int minColumns;
			int preferredColumns;
			if (this.m_Constraint == GridLayoutGroupExtended.Constraint.FixedColumnCount)
			{
				preferredColumns = (minColumns = this.m_ConstraintCount);
			}
			else if (this.m_Constraint == GridLayoutGroupExtended.Constraint.FixedRowCount)
			{
				preferredColumns = (minColumns = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f));
			}
			else
			{
				minColumns = 1;
				preferredColumns = Mathf.CeilToInt(Mathf.Sqrt((float)base.rectChildren.Count));
			}
			base.SetLayoutInputForAxis((float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)minColumns - this.spacing.x, (float)base.padding.horizontal + (this.cellSize.x + this.spacing.x) * (float)preferredColumns - this.spacing.x, -1f, 0);
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x001E23B4 File Offset: 0x001E05B4
		public override void CalculateLayoutInputVertical()
		{
			int minRows;
			if (this.m_Constraint == GridLayoutGroupExtended.Constraint.FixedColumnCount)
			{
				minRows = Mathf.CeilToInt((float)base.rectChildren.Count / (float)this.m_ConstraintCount - 0.001f);
			}
			else if (this.m_Constraint == GridLayoutGroupExtended.Constraint.FixedRowCount)
			{
				minRows = this.m_ConstraintCount;
			}
			else
			{
				float width = base.rectTransform.rect.width;
				int cellCountX = Mathf.Max(1, Mathf.FloorToInt((width - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				minRows = Mathf.CeilToInt((float)base.rectChildren.Count / (float)cellCountX);
			}
			float minSpace = (float)base.padding.vertical + (this.cellSize.y + this.spacing.y) * (float)minRows - this.spacing.y;
			this.TweenLayoutInputForAxis(minSpace, minSpace, -1f, 1);
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x001E24B4 File Offset: 0x001E06B4
		private void TweenLayoutInputForAxis(float totalMin, float totalPreferred, float totalFlexible, int axis)
		{
			if (!Application.isPlaying)
			{
				base.SetLayoutInputForAxis(totalMin, totalPreferred, totalFlexible, axis);
				return;
			}
			Vector3 tTarget = new Vector3(totalMin, totalPreferred, totalFlexible);
			Vector3 tCurrent = new Vector3(base.GetTotalMinSize(axis), base.GetTotalPreferredSize(axis), base.GetTotalFlexibleSize(axis));
			if (tTarget == tCurrent)
			{
				return;
			}
			TweenerCore<Vector3, Vector3, VectorOptions> tTween = this._axis_tween[axis];
			if (tTween.IsActive())
			{
				if (tTween.endValue == tTarget)
				{
					return;
				}
				tTween.Kill(false);
			}
			this._axis_tween[axis] = this.DOPreferredSize(tTarget, this.moveDuration * 0.5f, axis);
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x001E2550 File Offset: 0x001E0750
		private TweenerCore<Vector3, Vector3, VectorOptions> DOPreferredSize(Vector3 endValue, float duration, int axis)
		{
			TweenerCore<Vector3, Vector3, VectorOptions> tweenerCore = DOTween.To(() => new Vector3(this.GetTotalMinSize(axis), this.GetTotalPreferredSize(axis), this.GetTotalFlexibleSize(axis)), delegate(Vector3 x)
			{
				this.SetLayoutInputForAxis(x.x, x.y, x.z, axis);
			}, endValue, duration);
			tweenerCore.OnUpdate(delegate
			{
				this.SetDirty();
			});
			tweenerCore.OnComplete(delegate
			{
				this.SetDirty();
			});
			tweenerCore.SetTarget(this);
			return tweenerCore;
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x001E25BE File Offset: 0x001E07BE
		protected override void OnDisable()
		{
			this._axis_tween[0].Kill(false);
			this._axis_tween[1].Kill(false);
			base.OnDisable();
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x001E25E2 File Offset: 0x001E07E2
		public override void SetLayoutHorizontal()
		{
			this.SetCellsAlongAxis(0);
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x001E25EB File Offset: 0x001E07EB
		public override void SetLayoutVertical()
		{
			this.SetCellsAlongAxis(1);
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x001E25F4 File Offset: 0x001E07F4
		private void SetCellsAlongAxis(int axis)
		{
			int rectChildrenCount = base.rectChildren.Count;
			if (axis == 0)
			{
				for (int i = 0; i < rectChildrenCount; i++)
				{
					RectTransform rect = base.rectChildren[i];
					this.m_Tracker.Add(this, rect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
					rect.anchorMin = Vector2.up;
					rect.anchorMax = Vector2.up;
					rect.sizeDelta = this.cellSize;
				}
				return;
			}
			float width = base.rectTransform.rect.size.x;
			float height = base.rectTransform.rect.size.y;
			int cellCountX = 1;
			int cellCountY = 1;
			if (this.m_Constraint == GridLayoutGroupExtended.Constraint.FixedColumnCount)
			{
				cellCountX = this.m_ConstraintCount;
				if (rectChildrenCount > cellCountX)
				{
					cellCountY = rectChildrenCount / cellCountX + ((rectChildrenCount % cellCountX > 0) ? 1 : 0);
				}
			}
			else if (this.m_Constraint == GridLayoutGroupExtended.Constraint.FixedRowCount)
			{
				cellCountY = this.m_ConstraintCount;
				if (rectChildrenCount > cellCountY)
				{
					cellCountX = rectChildrenCount / cellCountY + ((rectChildrenCount % cellCountY > 0) ? 1 : 0);
				}
			}
			else
			{
				if (this.cellSize.x + this.spacing.x <= 0f)
				{
					cellCountX = int.MaxValue;
				}
				else
				{
					cellCountX = Mathf.Max(1, Mathf.FloorToInt((width - (float)base.padding.horizontal + this.spacing.x + 0.001f) / (this.cellSize.x + this.spacing.x)));
				}
				if (this.cellSize.y + this.spacing.y <= 0f)
				{
					cellCountY = int.MaxValue;
				}
				else
				{
					cellCountY = Mathf.Max(1, Mathf.FloorToInt((height - (float)base.padding.vertical + this.spacing.y + 0.001f) / (this.cellSize.y + this.spacing.y)));
				}
			}
			int cornerX = (int)(this.startCorner % GridLayoutGroupExtended.Corner.LowerLeft);
			int cornerY = (int)(this.startCorner / GridLayoutGroupExtended.Corner.LowerLeft);
			int cellsPerMainAxis;
			int actualCellCountX;
			int actualCellCountY;
			if (this.startAxis == GridLayoutGroupExtended.Axis.Horizontal)
			{
				cellsPerMainAxis = cellCountX;
				actualCellCountX = Mathf.Clamp(cellCountX, 1, rectChildrenCount);
				if (this.m_Constraint == GridLayoutGroupExtended.Constraint.FixedRowCount)
				{
					actualCellCountY = Mathf.Min(cellCountY, rectChildrenCount);
				}
				else
				{
					actualCellCountY = Mathf.Clamp(cellCountY, 1, Mathf.CeilToInt((float)rectChildrenCount / (float)cellsPerMainAxis));
				}
			}
			else
			{
				cellsPerMainAxis = cellCountY;
				actualCellCountY = Mathf.Clamp(cellCountY, 1, rectChildrenCount);
				if (this.m_Constraint == GridLayoutGroupExtended.Constraint.FixedColumnCount)
				{
					actualCellCountX = Mathf.Min(cellCountX, rectChildrenCount);
				}
				else
				{
					actualCellCountX = Mathf.Clamp(cellCountX, 1, Mathf.CeilToInt((float)rectChildrenCount / (float)cellsPerMainAxis));
				}
			}
			Vector2 requiredSpace = new Vector2((float)actualCellCountX * this.cellSize.x + (float)(actualCellCountX - 1) * this.spacing.x, (float)actualCellCountY * this.cellSize.y + (float)(actualCellCountY - 1) * this.spacing.y);
			Vector2 startOffset = new Vector2(base.GetStartOffset(0, requiredSpace.x), base.GetStartOffset(1, requiredSpace.y));
			int childrenToMove = 0;
			if (rectChildrenCount > this.m_ConstraintCount && Mathf.CeilToInt((float)rectChildrenCount / (float)cellsPerMainAxis) < this.m_ConstraintCount)
			{
				childrenToMove = this.m_ConstraintCount - Mathf.CeilToInt((float)rectChildrenCount / (float)cellsPerMainAxis);
				childrenToMove += Mathf.FloorToInt((float)childrenToMove / ((float)cellsPerMainAxis - 1f));
				if (rectChildrenCount % cellsPerMainAxis == 1)
				{
					childrenToMove++;
				}
			}
			for (int j = 0; j < rectChildrenCount; j++)
			{
				int positionX;
				int positionY;
				if (this.startAxis == GridLayoutGroupExtended.Axis.Horizontal)
				{
					if (this.m_Constraint == GridLayoutGroupExtended.Constraint.FixedRowCount && rectChildrenCount - j <= childrenToMove)
					{
						positionX = 0;
						positionY = this.m_ConstraintCount - (rectChildrenCount - j);
					}
					else
					{
						positionX = j % cellsPerMainAxis;
						positionY = j / cellsPerMainAxis;
					}
				}
				else if (this.m_Constraint == GridLayoutGroupExtended.Constraint.FixedColumnCount && rectChildrenCount - j <= childrenToMove)
				{
					positionX = this.m_ConstraintCount - (rectChildrenCount - j);
					positionY = 0;
				}
				else
				{
					positionX = j / cellsPerMainAxis;
					positionY = j % cellsPerMainAxis;
				}
				if (cornerX == 1)
				{
					positionX = actualCellCountX - 1 - positionX;
				}
				if (cornerY == 1)
				{
					positionY = actualCellCountY - 1 - positionY;
				}
				base.SetChildAlongAxis(base.rectChildren[j], 1, startOffset.y + (this.cellSize[1] + this.spacing[1]) * (float)positionY, this.cellSize[1]);
				base.SetChildAlongAxis(base.rectChildren[j], 0, startOffset.x + (this.cellSize[0] + this.spacing[0]) * (float)positionX, this.cellSize[0]);
			}
		}

		// Token: 0x04003206 RID: 12806
		private TweenerCore<Vector3, Vector3, VectorOptions>[] _axis_tween = new TweenerCore<Vector3, Vector3, VectorOptions>[2];

		// Token: 0x04003207 RID: 12807
		[SerializeField]
		protected GridLayoutGroupExtended.Corner m_StartCorner;

		// Token: 0x04003208 RID: 12808
		[SerializeField]
		protected GridLayoutGroupExtended.Axis m_StartAxis;

		// Token: 0x04003209 RID: 12809
		[SerializeField]
		protected Vector2 m_CellSize = new Vector2(100f, 100f);

		// Token: 0x0400320A RID: 12810
		[SerializeField]
		protected Vector2 m_Spacing = Vector2.zero;

		// Token: 0x0400320B RID: 12811
		[SerializeField]
		protected GridLayoutGroupExtended.Constraint m_Constraint;

		// Token: 0x0400320C RID: 12812
		[SerializeField]
		protected int m_ConstraintCount = 2;

		// Token: 0x02000B45 RID: 2885
		public enum Corner
		{
			// Token: 0x04003D22 RID: 15650
			UpperLeft,
			// Token: 0x04003D23 RID: 15651
			UpperRight,
			// Token: 0x04003D24 RID: 15652
			LowerLeft,
			// Token: 0x04003D25 RID: 15653
			LowerRight
		}

		// Token: 0x02000B46 RID: 2886
		public enum Axis
		{
			// Token: 0x04003D27 RID: 15655
			Horizontal,
			// Token: 0x04003D28 RID: 15656
			Vertical
		}

		// Token: 0x02000B47 RID: 2887
		public enum Constraint
		{
			// Token: 0x04003D2A RID: 15658
			Flexible,
			// Token: 0x04003D2B RID: 15659
			FixedColumnCount,
			// Token: 0x04003D2C RID: 15660
			FixedRowCount
		}
	}
}
