using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x020002DB RID: 731
public class ReligionData : MetaObjectData
{
	// Token: 0x06001B20 RID: 6944 RVA: 0x000FB940 File Offset: 0x000F9B40
	public override void Dispose()
	{
		base.Dispose();
		List<string> list = this.saved_traits;
		if (list != null)
		{
			list.Clear();
		}
		this.saved_traits = null;
	}

	// Token: 0x040014F2 RID: 5362
	public int banner_background_id;

	// Token: 0x040014F3 RID: 5363
	public int banner_icon_id;

	// Token: 0x040014F4 RID: 5364
	public string creator_city_name = string.Empty;

	// Token: 0x040014F5 RID: 5365
	[DefaultValue(-1L)]
	public long creator_city_id = -1L;

	// Token: 0x040014F6 RID: 5366
	public string creator_name = string.Empty;

	// Token: 0x040014F7 RID: 5367
	[DefaultValue(-1L)]
	public long creator_id = -1L;

	// Token: 0x040014F8 RID: 5368
	public string creator_species_id = string.Empty;

	// Token: 0x040014F9 RID: 5369
	[DefaultValue(-1L)]
	public long creator_subspecies_id = -1L;

	// Token: 0x040014FA RID: 5370
	public string creator_subspecies_name = string.Empty;

	// Token: 0x040014FB RID: 5371
	[DefaultValue(-1L)]
	public long creator_kingdom_id = -1L;

	// Token: 0x040014FC RID: 5372
	public string creator_kingdom_name = string.Empty;

	// Token: 0x040014FD RID: 5373
	[DefaultValue(-1L)]
	public long creator_clan_id = -1L;

	// Token: 0x040014FE RID: 5374
	public string creator_clan_name = string.Empty;

	// Token: 0x040014FF RID: 5375
	public List<string> saved_traits;
}
