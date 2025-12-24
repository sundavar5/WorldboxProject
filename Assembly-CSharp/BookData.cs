using System;
using System.ComponentModel;

// Token: 0x0200022F RID: 559
[Serializable]
public class BookData : BaseSystemData
{
	// Token: 0x040011DE RID: 4574
	public string book_type;

	// Token: 0x040011DF RID: 4575
	public string path_cover;

	// Token: 0x040011E0 RID: 4576
	public string path_icon;

	// Token: 0x040011E1 RID: 4577
	public string author_name;

	// Token: 0x040011E2 RID: 4578
	[DefaultValue(-1L)]
	public long author_id = -1L;

	// Token: 0x040011E3 RID: 4579
	public string author_clan_name;

	// Token: 0x040011E4 RID: 4580
	[DefaultValue(-1L)]
	public long author_clan_id = -1L;

	// Token: 0x040011E5 RID: 4581
	public string author_kingdom_name;

	// Token: 0x040011E6 RID: 4582
	[DefaultValue(-1L)]
	public long author_kingdom_id = -1L;

	// Token: 0x040011E7 RID: 4583
	public string author_city_name;

	// Token: 0x040011E8 RID: 4584
	[DefaultValue(-1L)]
	public long author_city_id = -1L;

	// Token: 0x040011E9 RID: 4585
	[DefaultValue(-1L)]
	public long language_id = -1L;

	// Token: 0x040011EA RID: 4586
	public string language_name;

	// Token: 0x040011EB RID: 4587
	[DefaultValue(-1L)]
	public long culture_id = -1L;

	// Token: 0x040011EC RID: 4588
	public string culture_name;

	// Token: 0x040011ED RID: 4589
	[DefaultValue(-1L)]
	public long religion_id = -1L;

	// Token: 0x040011EE RID: 4590
	public string religion_name;

	// Token: 0x040011EF RID: 4591
	public int times_read;

	// Token: 0x040011F0 RID: 4592
	public double timestamp_read_last_time;

	// Token: 0x040011F1 RID: 4593
	public string trait_id_actor = string.Empty;

	// Token: 0x040011F2 RID: 4594
	public string trait_id_language = string.Empty;

	// Token: 0x040011F3 RID: 4595
	public string trait_id_culture = string.Empty;

	// Token: 0x040011F4 RID: 4596
	public string trait_id_religion = string.Empty;

	// Token: 0x040011F5 RID: 4597
	[DefaultValue(-1L)]
	public long building_id = -1L;
}
