using System;
using System.Collections.Generic;
using UnityEngine.UI;

// Token: 0x0200078F RID: 1935
public class TooltipTraitsRow<TTrait> : TooltipItemsRow<Image> where TTrait : BaseTrait<TTrait>
{
	// Token: 0x06003D70 RID: 15728 RVA: 0x001AE2E0 File Offset: 0x001AC4E0
	protected override void loadItems()
	{
		this.items_pool.clear(true);
		IReadOnlyCollection<TTrait> tTraits = this.traits_hashset;
		if (tTraits == null || tTraits.Count == 0)
		{
			base.gameObject.SetActive(false);
			return;
		}
		base.gameObject.SetActive(true);
		foreach (TTrait tTrait in this.traits_hashset)
		{
			this.items_pool.getNext().sprite = tTrait.getSprite();
		}
	}

	// Token: 0x1700039D RID: 925
	// (get) Token: 0x06003D71 RID: 15729 RVA: 0x001AE378 File Offset: 0x001AC578
	protected virtual IReadOnlyCollection<TTrait> traits_hashset
	{
		get
		{
			throw new NotImplementedException();
		}
	}
}
