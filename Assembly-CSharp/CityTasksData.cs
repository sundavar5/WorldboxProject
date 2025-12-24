using System;

// Token: 0x02000320 RID: 800
[Serializable]
public class CityTasksData
{
	// Token: 0x06001EE8 RID: 7912 RVA: 0x0010DCB0 File Offset: 0x0010BEB0
	public void clear()
	{
		this.trees = 0;
		this.minerals = 0;
		this.bushes = 0;
		this.plants = 0;
		this.hives = 0;
		this.ruins = 0;
		this.poops = 0;
		this.farm_fields = 0;
		this.roads = 0;
		this.wheats = 0;
		this.fire = 0;
		this.farms_total = 0;
	}

	// Token: 0x040016A1 RID: 5793
	public int trees;

	// Token: 0x040016A2 RID: 5794
	public int minerals;

	// Token: 0x040016A3 RID: 5795
	public int bushes;

	// Token: 0x040016A4 RID: 5796
	public int plants;

	// Token: 0x040016A5 RID: 5797
	public int hives;

	// Token: 0x040016A6 RID: 5798
	public int farm_fields;

	// Token: 0x040016A7 RID: 5799
	public int farms_total;

	// Token: 0x040016A8 RID: 5800
	public int wheats;

	// Token: 0x040016A9 RID: 5801
	public int ruins;

	// Token: 0x040016AA RID: 5802
	public int poops;

	// Token: 0x040016AB RID: 5803
	public int roads;

	// Token: 0x040016AC RID: 5804
	public int fire;
}
