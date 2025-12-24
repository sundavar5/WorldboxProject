using System;
using DG.Tweening;
using UnityEngine.UI;

// Token: 0x02000675 RID: 1653
public class CultureListElement : WindowListElementBase<Culture, CultureData>
{
	// Token: 0x0600354D RID: 13645 RVA: 0x001885C8 File Offset: 0x001867C8
	internal override void show(Culture pCulture)
	{
		base.show(pCulture);
		this.name.text = pCulture.data.name;
		this.name.color = pCulture.getColor().getColorText();
		this.textAge.setValue(pCulture.getAge(), "");
		this.textFollowers.setValue(pCulture.countUnits(), "");
		this.textRenown.setValue(pCulture.getRenown(), "");
		this.textCities.setValue(pCulture.countCities(), "");
		this.textBooks.setValue(pCulture.books.count(), "");
	}

	// Token: 0x0600354E RID: 13646 RVA: 0x0018867C File Offset: 0x0018687C
	protected override void OnDisable()
	{
		this.textFollowers.DOKill(false);
		this.textCities.DOKill(false);
		this.textRenown.DOKill(false);
		this.textAge.DOKill(false);
		this.textBooks.DOKill(false);
		base.OnDisable();
	}

	// Token: 0x0600354F RID: 13647 RVA: 0x001886D0 File Offset: 0x001868D0
	protected override void tooltipAction()
	{
		Tooltip.show(this, "culture", new TooltipData
		{
			culture = this.meta_object
		});
	}

	// Token: 0x06003550 RID: 13648 RVA: 0x001886EE File Offset: 0x001868EE
	protected override ActorAsset getActorAsset()
	{
		return this.meta_object.getActorAsset();
	}

	// Token: 0x040027D8 RID: 10200
	public new Text name;

	// Token: 0x040027D9 RID: 10201
	public CountUpOnClick textFollowers;

	// Token: 0x040027DA RID: 10202
	public CountUpOnClick textCities;

	// Token: 0x040027DB RID: 10203
	public CountUpOnClick textRenown;

	// Token: 0x040027DC RID: 10204
	public CountUpOnClick textAge;

	// Token: 0x040027DD RID: 10205
	public CountUpOnClick textBooks;
}
