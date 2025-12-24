using System;

// Token: 0x02000689 RID: 1673
public class EquipmentGroupElement : AugmentationCategory<EquipmentAsset, EquipmentButton, EquipmentEditorButton>
{
	// Token: 0x060035B7 RID: 13751 RVA: 0x00189862 File Offset: 0x00187A62
	protected override bool isUnlocked(EquipmentButton pButton)
	{
		return pButton.getElementAsset().isAvailable();
	}
}
