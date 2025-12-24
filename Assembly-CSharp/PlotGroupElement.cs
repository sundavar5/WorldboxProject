using System;

// Token: 0x02000727 RID: 1831
public class PlotGroupElement : AugmentationCategory<PlotAsset, PlotButton, PlotEditorButton>
{
	// Token: 0x06003A60 RID: 14944 RVA: 0x0019D93F File Offset: 0x0019BB3F
	protected override bool isUnlocked(PlotButton pButton)
	{
		return pButton.getElementAsset().isAvailable();
	}
}
