using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x02000286 RID: 646
public class LanguageData : MetaObjectData
{
	// Token: 0x0400137A RID: 4986
	public int banner_background_id;

	// Token: 0x0400137B RID: 4987
	public int banner_icon_id;

	// Token: 0x0400137C RID: 4988
	public int books_written;

	// Token: 0x0400137D RID: 4989
	public string creator_name = string.Empty;

	// Token: 0x0400137E RID: 4990
	[DefaultValue(-1L)]
	public long creator_id = -1L;

	// Token: 0x0400137F RID: 4991
	public string creator_subspecies_name = string.Empty;

	// Token: 0x04001380 RID: 4992
	[DefaultValue(-1L)]
	public long creator_subspecies_id = -1L;

	// Token: 0x04001381 RID: 4993
	public string creator_species_id = string.Empty;

	// Token: 0x04001382 RID: 4994
	[DefaultValue(-1L)]
	public long creator_kingdom_id = -1L;

	// Token: 0x04001383 RID: 4995
	public string creator_kingdom_name = string.Empty;

	// Token: 0x04001384 RID: 4996
	[DefaultValue(-1L)]
	public long creator_clan_id = -1L;

	// Token: 0x04001385 RID: 4997
	public string creator_clan_name = string.Empty;

	// Token: 0x04001386 RID: 4998
	[DefaultValue(-1L)]
	public long creator_city_id = -1L;

	// Token: 0x04001387 RID: 4999
	public string creator_city_name = string.Empty;

	// Token: 0x04001388 RID: 5000
	public List<string> saved_traits;

	// Token: 0x04001389 RID: 5001
	public int speakers_new;

	// Token: 0x0400138A RID: 5002
	public int speakers_converted;

	// Token: 0x0400138B RID: 5003
	public int speakers_lost;

	// Token: 0x0400138C RID: 5004
	public LanguageStructure structure;
}
