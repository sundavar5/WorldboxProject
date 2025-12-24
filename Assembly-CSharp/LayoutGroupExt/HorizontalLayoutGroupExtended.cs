using System;
using UnityEngine;

namespace LayoutGroupExt
{
	// Token: 0x02000988 RID: 2440
	[AddComponentMenu("Layout/Horizontal Layout Group ( Extended )", 150)]
	public class HorizontalLayoutGroupExtended : HorizontalOrVerticalLayoutGroupExtended
	{
		// Token: 0x0600471D RID: 18205 RVA: 0x001E2A61 File Offset: 0x001E0C61
		protected HorizontalLayoutGroupExtended()
		{
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x001E2A69 File Offset: 0x001E0C69
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();
			base.CalcAlongAxis(0, false);
		}

		// Token: 0x0600471F RID: 18207 RVA: 0x001E2A79 File Offset: 0x001E0C79
		public override void CalculateLayoutInputVertical()
		{
			base.CalcAlongAxis(1, false);
		}

		// Token: 0x06004720 RID: 18208 RVA: 0x001E2A83 File Offset: 0x001E0C83
		public override void SetLayoutHorizontal()
		{
			base.SetChildrenAlongAxis(0, false);
		}

		// Token: 0x06004721 RID: 18209 RVA: 0x001E2A8D File Offset: 0x001E0C8D
		public override void SetLayoutVertical()
		{
			base.SetChildrenAlongAxis(1, false);
		}
	}
}
