using System;
using Newtonsoft.Json;

// Token: 0x02000228 RID: 552
public readonly struct NameEntry
{
	// Token: 0x060014AA RID: 5290 RVA: 0x000DBEB5 File Offset: 0x000DA0B5
	public NameEntry(string pName, bool pCustom)
	{
		this.name = pName;
		this.color_id = -1;
		this.timestamp = (double)((int)World.world.getCurWorldTime());
		this.custom = pCustom;
	}

	// Token: 0x060014AB RID: 5291 RVA: 0x000DBEDE File Offset: 0x000DA0DE
	public NameEntry(string pName, bool pCustom, int pColorId)
	{
		this.name = pName;
		this.color_id = pColorId;
		this.timestamp = (double)((int)World.world.getCurWorldTime());
		this.custom = pCustom;
	}

	// Token: 0x060014AC RID: 5292 RVA: 0x000DBF07 File Offset: 0x000DA107
	public NameEntry(string pName, bool pCustom, double pTimestamp)
	{
		this.name = pName;
		this.color_id = -1;
		this.timestamp = (double)((int)pTimestamp);
		this.custom = pCustom;
	}

	// Token: 0x060014AD RID: 5293 RVA: 0x000DBF27 File Offset: 0x000DA127
	public NameEntry(string pName, bool pCustom, int pColorId, double pTimestamp)
	{
		this.name = pName;
		this.color_id = pColorId;
		this.timestamp = (double)((int)pTimestamp);
		this.custom = pCustom;
	}

	// Token: 0x040011C9 RID: 4553
	[JsonProperty]
	public readonly int color_id;

	// Token: 0x040011CA RID: 4554
	[JsonProperty]
	public readonly string name;

	// Token: 0x040011CB RID: 4555
	[JsonProperty]
	public readonly double timestamp;

	// Token: 0x040011CC RID: 4556
	[JsonProperty]
	public readonly bool custom;
}
