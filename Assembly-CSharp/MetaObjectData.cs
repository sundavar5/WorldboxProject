using System;
using System.ComponentModel;
using Newtonsoft.Json;

// Token: 0x02000225 RID: 549
public class MetaObjectData : BaseSystemData
{
	// Token: 0x17000107 RID: 263
	// (get) Token: 0x0600143E RID: 5182 RVA: 0x000DAE73 File Offset: 0x000D9073
	// (set) Token: 0x0600143F RID: 5183 RVA: 0x000DAE7B File Offset: 0x000D907B
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	public int color_id { get; set; }

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06001440 RID: 5184 RVA: 0x000DAE84 File Offset: 0x000D9084
	// (set) Token: 0x06001441 RID: 5185 RVA: 0x000DAE8C File Offset: 0x000D908C
	[DefaultValue(-1)]
	public int original_color_id { get; set; } = -1;

	// Token: 0x17000109 RID: 265
	// (get) Token: 0x06001442 RID: 5186 RVA: 0x000DAE95 File Offset: 0x000D9095
	// (set) Token: 0x06001443 RID: 5187 RVA: 0x000DAE9D File Offset: 0x000D909D
	public long total_deaths { get; set; }

	// Token: 0x1700010A RID: 266
	// (get) Token: 0x06001444 RID: 5188 RVA: 0x000DAEA6 File Offset: 0x000D90A6
	// (set) Token: 0x06001445 RID: 5189 RVA: 0x000DAEAE File Offset: 0x000D90AE
	public long total_births { get; set; }

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06001446 RID: 5190 RVA: 0x000DAEB7 File Offset: 0x000D90B7
	// (set) Token: 0x06001447 RID: 5191 RVA: 0x000DAEBF File Offset: 0x000D90BF
	public long total_kills { get; set; }

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06001448 RID: 5192 RVA: 0x000DAEC8 File Offset: 0x000D90C8
	// (set) Token: 0x06001449 RID: 5193 RVA: 0x000DAED0 File Offset: 0x000D90D0
	public long deaths_natural { get; set; }

	// Token: 0x1700010D RID: 269
	// (get) Token: 0x0600144A RID: 5194 RVA: 0x000DAED9 File Offset: 0x000D90D9
	// (set) Token: 0x0600144B RID: 5195 RVA: 0x000DAEE1 File Offset: 0x000D90E1
	public long deaths_hunger { get; set; }

	// Token: 0x1700010E RID: 270
	// (get) Token: 0x0600144C RID: 5196 RVA: 0x000DAEEA File Offset: 0x000D90EA
	// (set) Token: 0x0600144D RID: 5197 RVA: 0x000DAEF2 File Offset: 0x000D90F2
	public long deaths_eaten { get; set; }

	// Token: 0x1700010F RID: 271
	// (get) Token: 0x0600144E RID: 5198 RVA: 0x000DAEFB File Offset: 0x000D90FB
	// (set) Token: 0x0600144F RID: 5199 RVA: 0x000DAF03 File Offset: 0x000D9103
	public long deaths_plague { get; set; }

	// Token: 0x17000110 RID: 272
	// (get) Token: 0x06001450 RID: 5200 RVA: 0x000DAF0C File Offset: 0x000D910C
	// (set) Token: 0x06001451 RID: 5201 RVA: 0x000DAF14 File Offset: 0x000D9114
	public long deaths_poison { get; set; }

	// Token: 0x17000111 RID: 273
	// (get) Token: 0x06001452 RID: 5202 RVA: 0x000DAF1D File Offset: 0x000D911D
	// (set) Token: 0x06001453 RID: 5203 RVA: 0x000DAF25 File Offset: 0x000D9125
	public long deaths_infection { get; set; }

	// Token: 0x17000112 RID: 274
	// (get) Token: 0x06001454 RID: 5204 RVA: 0x000DAF2E File Offset: 0x000D912E
	// (set) Token: 0x06001455 RID: 5205 RVA: 0x000DAF36 File Offset: 0x000D9136
	public long deaths_tumor { get; set; }

	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06001456 RID: 5206 RVA: 0x000DAF3F File Offset: 0x000D913F
	// (set) Token: 0x06001457 RID: 5207 RVA: 0x000DAF47 File Offset: 0x000D9147
	public long deaths_acid { get; set; }

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06001458 RID: 5208 RVA: 0x000DAF50 File Offset: 0x000D9150
	// (set) Token: 0x06001459 RID: 5209 RVA: 0x000DAF58 File Offset: 0x000D9158
	public long deaths_fire { get; set; }

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x0600145A RID: 5210 RVA: 0x000DAF61 File Offset: 0x000D9161
	// (set) Token: 0x0600145B RID: 5211 RVA: 0x000DAF69 File Offset: 0x000D9169
	public long deaths_divine { get; set; }

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x0600145C RID: 5212 RVA: 0x000DAF72 File Offset: 0x000D9172
	// (set) Token: 0x0600145D RID: 5213 RVA: 0x000DAF7A File Offset: 0x000D917A
	public long deaths_weapon { get; set; }

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x0600145E RID: 5214 RVA: 0x000DAF83 File Offset: 0x000D9183
	// (set) Token: 0x0600145F RID: 5215 RVA: 0x000DAF8B File Offset: 0x000D918B
	public long deaths_gravity { get; set; }

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06001460 RID: 5216 RVA: 0x000DAF94 File Offset: 0x000D9194
	// (set) Token: 0x06001461 RID: 5217 RVA: 0x000DAF9C File Offset: 0x000D919C
	public long deaths_drowning { get; set; }

	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06001462 RID: 5218 RVA: 0x000DAFA5 File Offset: 0x000D91A5
	// (set) Token: 0x06001463 RID: 5219 RVA: 0x000DAFAD File Offset: 0x000D91AD
	public long deaths_water { get; set; }

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06001464 RID: 5220 RVA: 0x000DAFB6 File Offset: 0x000D91B6
	// (set) Token: 0x06001465 RID: 5221 RVA: 0x000DAFBE File Offset: 0x000D91BE
	public long deaths_explosion { get; set; }

	// Token: 0x1700011B RID: 283
	// (get) Token: 0x06001466 RID: 5222 RVA: 0x000DAFC7 File Offset: 0x000D91C7
	// (set) Token: 0x06001467 RID: 5223 RVA: 0x000DAFCF File Offset: 0x000D91CF
	public long deaths_other { get; set; }

	// Token: 0x1700011C RID: 284
	// (get) Token: 0x06001468 RID: 5224 RVA: 0x000DAFD8 File Offset: 0x000D91D8
	// (set) Token: 0x06001469 RID: 5225 RVA: 0x000DAFE0 File Offset: 0x000D91E0
	public long metamorphosis { get; set; }

	// Token: 0x1700011D RID: 285
	// (get) Token: 0x0600146A RID: 5226 RVA: 0x000DAFE9 File Offset: 0x000D91E9
	// (set) Token: 0x0600146B RID: 5227 RVA: 0x000DAFF1 File Offset: 0x000D91F1
	public long evolutions { get; set; }

	// Token: 0x1700011E RID: 286
	// (get) Token: 0x0600146C RID: 5228 RVA: 0x000DAFFA File Offset: 0x000D91FA
	// (set) Token: 0x0600146D RID: 5229 RVA: 0x000DB002 File Offset: 0x000D9202
	public int renown { get; set; }

	// Token: 0x0600146E RID: 5230 RVA: 0x000DB00B File Offset: 0x000D920B
	public void setColorID(int pColorID)
	{
		this.color_id = pColorID;
		if (this.original_color_id == -1)
		{
			this.original_color_id = this.color_id;
		}
	}
}
