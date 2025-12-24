using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000628 RID: 1576
public class AllianceListElement : WindowListElementBase<Alliance, AllianceData>
{
	// Token: 0x0600337F RID: 13183 RVA: 0x00183488 File Offset: 0x00181688
	internal override void show(Alliance pAlliance)
	{
		base.show(pAlliance);
		this.text_name.text = this.meta_object.name;
		this.text_name.color = this.meta_object.getColor().getColorText();
		this.age.setValue(this.meta_object.getAge(), "");
		this.population.setValue(this.meta_object.countPopulation(), "");
		this.warriors.setValue(this.meta_object.countWarriors(), "");
		this.villages.setValue(this.meta_object.countCities(), "");
		this.kingdoms.setValue(this.meta_object.countKingdoms(), "");
		this.showKingdomBanners(this.meta_object.kingdoms_list);
	}

	// Token: 0x06003380 RID: 13184 RVA: 0x00183568 File Offset: 0x00181768
	public void showKingdomBanners(List<Kingdom> pList)
	{
		if (this.pool_mini_banners == null)
		{
			this.pool_mini_banners = new ObjectPoolGenericMono<KingdomBanner>(this.prefabMiniKingdomBanner, this.grid.transform);
		}
		this.pool_mini_banners.clear(true);
		foreach (Kingdom tKingdom in pList)
		{
			KingdomBanner next = this.pool_mini_banners.getNext();
			next.load(tKingdom);
			next.GetComponentInChildren<RotateOnHover>().enabled = false;
		}
	}

	// Token: 0x06003381 RID: 13185 RVA: 0x001835FC File Offset: 0x001817FC
	protected override void tooltipAction()
	{
		Tooltip.show(this, "alliance", new TooltipData
		{
			alliance = this.meta_object
		});
	}

	// Token: 0x06003382 RID: 13186 RVA: 0x0018361A File Offset: 0x0018181A
	protected override void OnDisable()
	{
		base.OnDisable();
		ObjectPoolGenericMono<KingdomBanner> objectPoolGenericMono = this.pool_mini_banners;
		if (objectPoolGenericMono == null)
		{
			return;
		}
		objectPoolGenericMono.clear(true);
	}

	// Token: 0x04002706 RID: 9990
	public Text text_name;

	// Token: 0x04002707 RID: 9991
	public CountUpOnClick age;

	// Token: 0x04002708 RID: 9992
	public CountUpOnClick population;

	// Token: 0x04002709 RID: 9993
	public CountUpOnClick warriors;

	// Token: 0x0400270A RID: 9994
	public CountUpOnClick villages;

	// Token: 0x0400270B RID: 9995
	public CountUpOnClick kingdoms;

	// Token: 0x0400270C RID: 9996
	public Text level;

	// Token: 0x0400270D RID: 9997
	public KingdomBanner prefabMiniKingdomBanner;

	// Token: 0x0400270E RID: 9998
	public GameObject grid;

	// Token: 0x0400270F RID: 9999
	private ObjectPoolGenericMono<KingdomBanner> pool_mini_banners;
}
