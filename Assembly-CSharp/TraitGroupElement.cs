using System;

// Token: 0x02000795 RID: 1941
public class TraitGroupElement<TTrait, TTraitButton, TTraitEditorButton> : AugmentationCategory<TTrait, TTraitButton, TTraitEditorButton> where TTrait : BaseTrait<TTrait> where TTraitButton : TraitButton<TTrait> where TTraitEditorButton : TraitEditorButton<TTraitButton, TTrait>
{
	// Token: 0x06003D89 RID: 15753 RVA: 0x001AE542 File Offset: 0x001AC742
	protected override bool isUnlocked(TTraitButton pButton)
	{
		return pButton.getElementAsset().isAvailable();
	}
}
