using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200080B RID: 2059
public class WorkshopMapElement : MonoBehaviour
{
	// Token: 0x06004085 RID: 16517 RVA: 0x001B9FCC File Offset: 0x001B81CC
	public void load(WorkshopMapData pData)
	{
		this.data = pData;
		this.textName.text = this.data.meta_data_map.mapStats.name;
		this.textKingdoms.text = this.data.meta_data_map.kingdoms.ToString();
		this.textCities.text = this.data.meta_data_map.cities.ToString();
		this.textPopulation.text = this.data.meta_data_map.population.ToString();
		this.textMobs.text = this.data.meta_data_map.mobs.ToString();
		this.textUpvotes.text = this.data.workshop_item.VotesUp.ToString();
		this.textComments.text = this.data.workshop_item.NumComments.ToString();
		this.image.sprite = this.data.sprite_small_preview;
		if (this.data.workshop_item.Owner.Id.ToString() == Config.steam_id)
		{
			this.textName.color = Toolbox.makeColor("#3DDEFF");
			this.ayeIcon.gameObject.SetActive(true);
		}
		else
		{
			this.textName.color = Toolbox.makeColor("#FF9B1C");
			this.ayeIcon.gameObject.SetActive(false);
		}
		base.gameObject.name = "WorkshopMapElement " + this.data.meta_data_map.mapStats.name;
	}

	// Token: 0x06004086 RID: 16518 RVA: 0x001BA183 File Offset: 0x001B8383
	public void clickWorkshopMap()
	{
		SaveManager.currentWorkshopMapData = this.data;
		ScrollWindow.showWindow("steam_workshop_play_world");
	}

	// Token: 0x04002EB8 RID: 11960
	private WorkshopMapData data;

	// Token: 0x04002EB9 RID: 11961
	public Image image;

	// Token: 0x04002EBA RID: 11962
	public Text textName;

	// Token: 0x04002EBB RID: 11963
	public Text textKingdoms;

	// Token: 0x04002EBC RID: 11964
	public Text textCities;

	// Token: 0x04002EBD RID: 11965
	public Text textPopulation;

	// Token: 0x04002EBE RID: 11966
	public Text textMobs;

	// Token: 0x04002EBF RID: 11967
	public Text textUpvotes;

	// Token: 0x04002EC0 RID: 11968
	public Text textComments;

	// Token: 0x04002EC1 RID: 11969
	public Image mainBackground;

	// Token: 0x04002EC2 RID: 11970
	public Image ayeIcon;
}
