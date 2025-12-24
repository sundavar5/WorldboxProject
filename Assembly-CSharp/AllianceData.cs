using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x0200020F RID: 527
public class AllianceData : MetaObjectData
{
	// Token: 0x170000EB RID: 235
	// (get) Token: 0x060012F1 RID: 4849 RVA: 0x000D5FB4 File Offset: 0x000D41B4
	// (set) Token: 0x060012F2 RID: 4850 RVA: 0x000D5FBC File Offset: 0x000D41BC
	public string motto { get; set; }

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x060012F3 RID: 4851 RVA: 0x000D5FC5 File Offset: 0x000D41C5
	// (set) Token: 0x060012F4 RID: 4852 RVA: 0x000D5FCD File Offset: 0x000D41CD
	public int banner_background_id { get; set; }

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x060012F5 RID: 4853 RVA: 0x000D5FD6 File Offset: 0x000D41D6
	// (set) Token: 0x060012F6 RID: 4854 RVA: 0x000D5FDE File Offset: 0x000D41DE
	public int banner_icon_id { get; set; }

	// Token: 0x170000EE RID: 238
	// (get) Token: 0x060012F7 RID: 4855 RVA: 0x000D5FE7 File Offset: 0x000D41E7
	// (set) Token: 0x060012F8 RID: 4856 RVA: 0x000D5FEF File Offset: 0x000D41EF
	public string founder_actor_name { get; set; }

	// Token: 0x170000EF RID: 239
	// (get) Token: 0x060012F9 RID: 4857 RVA: 0x000D5FF8 File Offset: 0x000D41F8
	// (set) Token: 0x060012FA RID: 4858 RVA: 0x000D6000 File Offset: 0x000D4200
	[DefaultValue(-1L)]
	public long founder_actor_id { get; set; } = -1L;

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x060012FB RID: 4859 RVA: 0x000D6009 File Offset: 0x000D4209
	// (set) Token: 0x060012FC RID: 4860 RVA: 0x000D6011 File Offset: 0x000D4211
	public string founder_kingdom_name { get; set; }

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x060012FD RID: 4861 RVA: 0x000D601A File Offset: 0x000D421A
	// (set) Token: 0x060012FE RID: 4862 RVA: 0x000D6022 File Offset: 0x000D4222
	[DefaultValue(-1L)]
	public long founder_kingdom_id { get; set; } = -1L;

	// Token: 0x0400114F RID: 4431
	public List<long> kingdoms;

	// Token: 0x04001154 RID: 4436
	public double timestamp_member_joined;

	// Token: 0x04001155 RID: 4437
	[DefaultValue(AllianceType.Normal)]
	public AllianceType alliance_type;
}
