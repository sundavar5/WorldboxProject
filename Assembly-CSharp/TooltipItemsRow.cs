using System;
using UnityEngine;

// Token: 0x02000789 RID: 1929
public class TooltipItemsRow<TComponent> : MonoBehaviour where TComponent : MonoBehaviour
{
	// Token: 0x06003D62 RID: 15714 RVA: 0x001AE1CE File Offset: 0x001AC3CE
	public void init(Tooltip pTooltip, TooltipData pData)
	{
		this.tooltip = pTooltip;
		this.tooltip_data = pData;
		if (this.items_pool == null)
		{
			this.items_pool = new ObjectPoolGenericMono<TComponent>(this.item, this.items_parent);
		}
		this.loadItems();
	}

	// Token: 0x06003D63 RID: 15715 RVA: 0x001AE203 File Offset: 0x001AC403
	protected virtual void loadItems()
	{
		throw new NotImplementedException();
	}

	// Token: 0x04002C7C RID: 11388
	public Transform items_parent;

	// Token: 0x04002C7D RID: 11389
	public TComponent item;

	// Token: 0x04002C7E RID: 11390
	protected Tooltip tooltip;

	// Token: 0x04002C7F RID: 11391
	protected TooltipData tooltip_data;

	// Token: 0x04002C80 RID: 11392
	protected ObjectPoolGenericMono<TComponent> items_pool;
}
