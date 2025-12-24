using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000788 RID: 1928
public class TooltipIconsRow : TooltipItemsRow<Image>
{
	// Token: 0x06003D5E RID: 15710 RVA: 0x001AE0E0 File Offset: 0x001AC2E0
	protected override void loadItems()
	{
		this.items_pool.clear(true);
		if (this._icons.Count == 0)
		{
			base.gameObject.SetActive(false);
			return;
		}
		base.gameObject.SetActive(true);
		foreach (ValueTuple<Sprite, Color> tIcon in this._icons)
		{
			Image next = this.items_pool.getNext();
			next.sprite = tIcon.Item1;
			next.color = tIcon.Item2;
		}
		this.clearIcons();
	}

	// Token: 0x06003D5F RID: 15711 RVA: 0x001AE188 File Offset: 0x001AC388
	public void addIcon(Sprite pIcon, string pColor = "#FFFFFF")
	{
		Color tColor = Toolbox.makeColor(pColor);
		this._icons.Add(new ValueTuple<Sprite, Color>(pIcon, tColor));
	}

	// Token: 0x06003D60 RID: 15712 RVA: 0x001AE1AE File Offset: 0x001AC3AE
	private void clearIcons()
	{
		this._icons.Clear();
	}

	// Token: 0x04002C7B RID: 11387
	private List<ValueTuple<Sprite, Color>> _icons = new List<ValueTuple<Sprite, Color>>();
}
