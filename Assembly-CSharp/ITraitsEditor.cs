using System;
using UnityEngine;

// Token: 0x02000792 RID: 1938
public interface ITraitsEditor<TTrait> : IAugmentationsEditor where TTrait : BaseTrait<TTrait>
{
	// Token: 0x06003D7A RID: 15738
	ITraitsOwner<TTrait> getTraitsOwner();

	// Token: 0x06003D7B RID: 15739
	void scrollToGroupStarter(GameObject pTraitButton);

	// Token: 0x06003D7C RID: 15740
	void scrollToGroupStarter(GameObject pTraitButton, bool pIgnoreTooltipCheck);

	// Token: 0x06003D7D RID: 15741
	WindowMetaTab getEditorTab();
}
