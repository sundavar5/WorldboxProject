using System;
using UnityEngine;

namespace LayoutGroupExt
{
	// Token: 0x0200098B RID: 2443
	[AddComponentMenu("Layout/Vertical Layout Group ( Extended )", 151)]
	public class VerticalLayoutGroupExtended : HorizontalOrVerticalLayoutGroupExtended
	{
		// Token: 0x06004744 RID: 18244 RVA: 0x001E3DE6 File Offset: 0x001E1FE6
		protected VerticalLayoutGroupExtended()
		{
		}

		// Token: 0x06004745 RID: 18245 RVA: 0x001E3DEE File Offset: 0x001E1FEE
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, true);
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x001E3DFE File Offset: 0x001E1FFE
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, true);
		}

		// Token: 0x06004747 RID: 18247 RVA: 0x001E3E08 File Offset: 0x001E2008
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, true);
		}

		// Token: 0x06004748 RID: 18248 RVA: 0x001E3E12 File Offset: 0x001E2012
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, true);
		}
	}
}
