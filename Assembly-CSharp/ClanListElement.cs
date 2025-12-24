using System;
using UnityEngine.UI;

// Token: 0x02000667 RID: 1639
public class ClanListElement : WindowListElementBase<Clan, ClanData>
{
	// Token: 0x0600350F RID: 13583 RVA: 0x00187CC0 File Offset: 0x00185EC0
	internal override void show(Clan pClan)
	{
		base.show(pClan);
		Actor tChief = pClan.getChief();
		if (tChief.isRekt())
		{
			this.avatarLoader.gameObject.SetActive(false);
		}
		else
		{
			this.avatarLoader.gameObject.SetActive(true);
			this.avatarLoader.show(tChief);
		}
		this.text_name.text = pClan.name;
		this.text_name.color = pClan.getColor().getColorText();
		this.members.setValue(pClan.countUnits(), "");
		this.renown.setValue(pClan.getRenown(), "");
		int tAges = pClan.getAge();
		this.age.setValue(tAges, "");
		this.dead.setValue((int)pClan.getTotalDeaths(), "");
	}

	// Token: 0x06003510 RID: 13584 RVA: 0x00187D95 File Offset: 0x00185F95
	protected override void tooltipAction()
	{
		Tooltip.show(this, "clan", new TooltipData
		{
			clan = this.meta_object
		});
	}

	// Token: 0x06003511 RID: 13585 RVA: 0x00187DB3 File Offset: 0x00185FB3
	protected override ActorAsset getActorAsset()
	{
		return this.meta_object.getActorAsset();
	}

	// Token: 0x040027CA RID: 10186
	public Text text_name;

	// Token: 0x040027CB RID: 10187
	public CountUpOnClick members;

	// Token: 0x040027CC RID: 10188
	public CountUpOnClick dead;

	// Token: 0x040027CD RID: 10189
	public CountUpOnClick age;

	// Token: 0x040027CE RID: 10190
	public CountUpOnClick renown;

	// Token: 0x040027CF RID: 10191
	public UiUnitAvatarElement avatarLoader;
}
