using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200060D RID: 1549
public class UiPanelInfo : MonoBehaviour
{
	// Token: 0x060032C5 RID: 12997 RVA: 0x0018066C File Offset: 0x0017E86C
	private void OnEnable()
	{
		this.population_name = this.population.transform.Find("Name").GetComponent<Text>();
		this.population_value = this.population.transform.Find("Value").GetComponent<Text>();
		this.beasts_name = this.beasts.transform.Find("Name").GetComponent<Text>();
		this.beasts_value = this.beasts.transform.Find("Value").GetComponent<Text>();
		this.infected_name = this.infected.transform.Find("Name").GetComponent<Text>();
		this.infected_value = this.infected.transform.Find("Value").GetComponent<Text>();
		this.deaths_name = this.deaths.transform.Find("Name").GetComponent<Text>();
		this.deaths_value = this.deaths.transform.Find("Value").GetComponent<Text>();
		this.buildings_name = this.buildings.transform.Find("Name").GetComponent<Text>();
		this.buildings_value = this.buildings.transform.Find("Value").GetComponent<Text>();
		this.vegetation_name = this.vegetations.transform.Find("Name").GetComponent<Text>();
		this.vegetation_value = this.vegetations.transform.Find("Value").GetComponent<Text>();
	}

	// Token: 0x060032C6 RID: 12998 RVA: 0x001807FC File Offset: 0x0017E9FC
	private void Update()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		if (World.world == null)
		{
			return;
		}
		if (World.world.map_stats == null)
		{
			return;
		}
		if (World.world.game_stats == null)
		{
			return;
		}
		if (this.timer > 0f)
		{
			this.timer -= Time.deltaTime;
			return;
		}
		this.timer = this.interval;
		if (LocalizedTextManager.current_language.isRTL() != this.lastRTL)
		{
			this.lastRTL = LocalizedTextManager.current_language.isRTL();
			if (this.lastRTL)
			{
				this.population_value.alignment = TextAnchor.MiddleLeft;
				this.beasts_value.alignment = TextAnchor.MiddleLeft;
				this.infected_value.alignment = TextAnchor.MiddleLeft;
				this.deaths_value.alignment = TextAnchor.MiddleLeft;
				this.buildings_value.alignment = TextAnchor.MiddleLeft;
				this.vegetation_value.alignment = TextAnchor.MiddleLeft;
				this.population_name.alignment = TextAnchor.MiddleRight;
				this.beasts_name.alignment = TextAnchor.MiddleRight;
				this.infected_name.alignment = TextAnchor.MiddleRight;
				this.deaths_name.alignment = TextAnchor.MiddleRight;
				this.buildings_name.alignment = TextAnchor.MiddleRight;
				this.vegetation_name.alignment = TextAnchor.MiddleRight;
			}
			else
			{
				this.population_value.alignment = TextAnchor.MiddleRight;
				this.beasts_value.alignment = TextAnchor.MiddleRight;
				this.infected_value.alignment = TextAnchor.MiddleRight;
				this.deaths_value.alignment = TextAnchor.MiddleRight;
				this.buildings_value.alignment = TextAnchor.MiddleRight;
				this.vegetation_value.alignment = TextAnchor.MiddleRight;
				this.population_name.alignment = TextAnchor.MiddleLeft;
				this.beasts_name.alignment = TextAnchor.MiddleLeft;
				this.infected_name.alignment = TextAnchor.MiddleLeft;
				this.deaths_name.alignment = TextAnchor.MiddleLeft;
				this.buildings_name.alignment = TextAnchor.MiddleLeft;
				this.vegetation_name.alignment = TextAnchor.MiddleLeft;
			}
		}
		this.population_value.text = (World.world.getCivWorldPopulation().ToString() ?? "");
		this.beasts_value.text = (World.world.map_stats.current_mobs.ToString() ?? "");
		this.infected_value.text = (World.world.map_stats.current_infected.ToString() ?? "");
		this.deaths_value.text = (World.world.map_stats.deaths.ToString() ?? "");
		this.buildings_value.text = (World.world.map_stats.current_houses.ToString() ?? "");
		this.vegetation_value.text = (World.world.map_stats.current_vegetation.ToString() ?? "");
		this.population_value.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		this.beasts_value.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		this.infected_value.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		this.deaths_value.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		this.buildings_value.GetComponent<LocalizedText>().checkSpecialLanguages(null);
		this.vegetation_value.GetComponent<LocalizedText>().checkSpecialLanguages(null);
	}

	// Token: 0x04002661 RID: 9825
	public GameObject population;

	// Token: 0x04002662 RID: 9826
	public GameObject beasts;

	// Token: 0x04002663 RID: 9827
	public GameObject deaths;

	// Token: 0x04002664 RID: 9828
	public GameObject infected;

	// Token: 0x04002665 RID: 9829
	public GameObject buildings;

	// Token: 0x04002666 RID: 9830
	public GameObject vegetations;

	// Token: 0x04002667 RID: 9831
	private float interval = 0.2f;

	// Token: 0x04002668 RID: 9832
	private float timer;

	// Token: 0x04002669 RID: 9833
	private Text population_name;

	// Token: 0x0400266A RID: 9834
	private Text population_value;

	// Token: 0x0400266B RID: 9835
	private Text beasts_name;

	// Token: 0x0400266C RID: 9836
	private Text beasts_value;

	// Token: 0x0400266D RID: 9837
	private Text deaths_name;

	// Token: 0x0400266E RID: 9838
	private Text deaths_value;

	// Token: 0x0400266F RID: 9839
	private Text infected_name;

	// Token: 0x04002670 RID: 9840
	private Text infected_value;

	// Token: 0x04002671 RID: 9841
	private Text buildings_name;

	// Token: 0x04002672 RID: 9842
	private Text buildings_value;

	// Token: 0x04002673 RID: 9843
	private Text vegetation_name;

	// Token: 0x04002674 RID: 9844
	private Text vegetation_value;

	// Token: 0x04002675 RID: 9845
	private bool lastRTL;
}
