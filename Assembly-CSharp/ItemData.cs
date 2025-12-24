using System;
using System.ComponentModel;

// Token: 0x0200027A RID: 634
[Serializable]
public class ItemData : BaseSystemData
{
	// Token: 0x17000164 RID: 356
	// (get) Token: 0x060017B2 RID: 6066 RVA: 0x000E7ED8 File Offset: 0x000E60D8
	// (set) Token: 0x060017B3 RID: 6067 RVA: 0x000E7EE0 File Offset: 0x000E60E0
	[DefaultValue(-1L)]
	public long creator_id { get; set; } = -1L;

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x060017B4 RID: 6068 RVA: 0x000E7EE9 File Offset: 0x000E60E9
	// (set) Token: 0x060017B5 RID: 6069 RVA: 0x000E7EF1 File Offset: 0x000E60F1
	[DefaultValue(-1L)]
	public long creator_kingdom_id { get; set; } = -1L;

	// Token: 0x060017B6 RID: 6070 RVA: 0x000E7EFA File Offset: 0x000E60FA
	public bool ShouldSerializemodifiers()
	{
		return this.modifiers.Count > 0;
	}

	// Token: 0x060017B7 RID: 6071 RVA: 0x000E7F0A File Offset: 0x000E610A
	public override void Dispose()
	{
		this.modifiers.Dispose();
		base.Dispose();
	}

	// Token: 0x04001330 RID: 4912
	public int durability = 100;

	// Token: 0x04001331 RID: 4913
	public bool created_by_player;

	// Token: 0x04001332 RID: 4914
	[DefaultValue("")]
	public string by = string.Empty;

	// Token: 0x04001334 RID: 4916
	internal string byColor = string.Empty;

	// Token: 0x04001336 RID: 4918
	[DefaultValue("")]
	public string from = string.Empty;

	// Token: 0x04001337 RID: 4919
	internal string fromColor = string.Empty;

	// Token: 0x04001338 RID: 4920
	[DefaultValue(0)]
	public int kills;

	// Token: 0x04001339 RID: 4921
	public string asset_id = string.Empty;

	// Token: 0x0400133A RID: 4922
	public string material = string.Empty;

	// Token: 0x0400133B RID: 4923
	public readonly ListPool<string> modifiers = new ListPool<string>();
}
