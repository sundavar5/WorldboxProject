using System;

// Token: 0x02000241 RID: 577
public class Beehive : BaseBuildingComponent
{
	// Token: 0x060015EB RID: 5611 RVA: 0x000E0F11 File Offset: 0x000DF111
	public void addHoney()
	{
		if (this.honey >= 10)
		{
			return;
		}
		this.honey++;
		if (this.honey == 10)
		{
			this.building.setHaveResourcesToCollect(true);
		}
	}

	// Token: 0x04001261 RID: 4705
	public int honey;
}
