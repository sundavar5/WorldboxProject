using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006F3 RID: 1779
public class KingdomListElement : WindowListElementBase<Kingdom, KingdomData>
{
	// Token: 0x06003916 RID: 14614 RVA: 0x00197B50 File Offset: 0x00195D50
	internal override void show(Kingdom pKingdom)
	{
		base.show(pKingdom);
		this.kingdomName.text = pKingdom.name;
		Color tColor = pKingdom.getColor().getColorText();
		this.kingdomName.color = tColor;
		this.avatarLoader.show(pKingdom.king);
		int tZones = 0;
		int tHouses = 0;
		int tCities = 0;
		foreach (City tCity in pKingdom.getCities())
		{
			tCities++;
			tZones += tCity.zones.Count;
			tHouses += tCity.buildings.Count;
		}
		this.textPopulation.setValue(pKingdom.getPopulationPeople(), "");
		this.textArmy.setValue(pKingdom.countTotalWarriors(), "");
		this.textZones.setValue(tZones, "");
		this.textHouses.setValue(tHouses, "");
		this.textCities.setValue(tCities, "/" + pKingdom.getMaxCities().ToString());
		this.textAge.setValue(pKingdom.getAge(), "");
	}

	// Token: 0x06003917 RID: 14615 RVA: 0x00197C90 File Offset: 0x00195E90
	protected override void tooltipAction()
	{
		Tooltip.show(this, "kingdom", new TooltipData
		{
			kingdom = this.meta_object
		});
	}

	// Token: 0x06003918 RID: 14616 RVA: 0x00197CAE File Offset: 0x00195EAE
	protected override ActorAsset getActorAsset()
	{
		return this.meta_object.getActorAsset();
	}

	// Token: 0x04002A34 RID: 10804
	public CountUpOnClick textAge;

	// Token: 0x04002A35 RID: 10805
	public CountUpOnClick textPopulation;

	// Token: 0x04002A36 RID: 10806
	public CountUpOnClick textArmy;

	// Token: 0x04002A37 RID: 10807
	public CountUpOnClick textCities;

	// Token: 0x04002A38 RID: 10808
	public CountUpOnClick textHouses;

	// Token: 0x04002A39 RID: 10809
	public CountUpOnClick textZones;

	// Token: 0x04002A3A RID: 10810
	public Text kingdomName;

	// Token: 0x04002A3B RID: 10811
	public GameObject buttonCapital;

	// Token: 0x04002A3C RID: 10812
	public GameObject buttonKing;

	// Token: 0x04002A3D RID: 10813
	public UiUnitAvatarElement avatarLoader;
}
