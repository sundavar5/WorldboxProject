using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine.Scripting;

// Token: 0x0200031A RID: 794
[Serializable]
public class CityData : MetaObjectData
{
	// Token: 0x170001D2 RID: 466
	// (get) Token: 0x06001E90 RID: 7824 RVA: 0x0010CA53 File Offset: 0x0010AC53
	// (set) Token: 0x06001E91 RID: 7825 RVA: 0x0010CA5B File Offset: 0x0010AC5B
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string original_actor_asset { get; set; }

	// Token: 0x170001D3 RID: 467
	// (get) Token: 0x06001E92 RID: 7826 RVA: 0x0010CA64 File Offset: 0x0010AC64
	// (set) Token: 0x06001E93 RID: 7827 RVA: 0x0010CA6C File Offset: 0x0010AC6C
	[DefaultValue(-1L)]
	public long kingdomID { get; set; } = -1L;

	// Token: 0x170001D4 RID: 468
	// (get) Token: 0x06001E94 RID: 7828 RVA: 0x0010CA75 File Offset: 0x0010AC75
	// (set) Token: 0x06001E95 RID: 7829 RVA: 0x0010CA7D File Offset: 0x0010AC7D
	[DefaultValue(-1L)]
	public long leaderID { get; set; } = -1L;

	// Token: 0x170001D5 RID: 469
	// (get) Token: 0x06001E96 RID: 7830 RVA: 0x0010CA86 File Offset: 0x0010AC86
	// (set) Token: 0x06001E97 RID: 7831 RVA: 0x0010CA8E File Offset: 0x0010AC8E
	[DefaultValue(-1L)]
	public long founder_id { get; set; } = -1L;

	// Token: 0x170001D6 RID: 470
	// (get) Token: 0x06001E98 RID: 7832 RVA: 0x0010CA97 File Offset: 0x0010AC97
	// (set) Token: 0x06001E99 RID: 7833 RVA: 0x0010CA9F File Offset: 0x0010AC9F
	public string founder_name { get; set; }

	// Token: 0x170001D7 RID: 471
	// (get) Token: 0x06001E9A RID: 7834 RVA: 0x0010CAA8 File Offset: 0x0010ACA8
	// (set) Token: 0x06001E9B RID: 7835 RVA: 0x0010CAB0 File Offset: 0x0010ACB0
	[DefaultValue(-1L)]
	public long last_leader_id { get; set; } = -1L;

	// Token: 0x170001D8 RID: 472
	// (get) Token: 0x06001E9C RID: 7836 RVA: 0x0010CAB9 File Offset: 0x0010ACB9
	// (set) Token: 0x06001E9D RID: 7837 RVA: 0x0010CAC1 File Offset: 0x0010ACC1
	[DefaultValue(-1L)]
	public long last_kingdom_id { get; set; } = -1L;

	// Token: 0x170001D9 RID: 473
	// (get) Token: 0x06001E9E RID: 7838 RVA: 0x0010CACA File Offset: 0x0010ACCA
	// (set) Token: 0x06001E9F RID: 7839 RVA: 0x0010CAD2 File Offset: 0x0010ACD2
	[DefaultValue(-1L)]
	public long id_culture { get; set; } = -1L;

	// Token: 0x170001DA RID: 474
	// (get) Token: 0x06001EA0 RID: 7840 RVA: 0x0010CADB File Offset: 0x0010ACDB
	// (set) Token: 0x06001EA1 RID: 7841 RVA: 0x0010CAE3 File Offset: 0x0010ACE3
	[DefaultValue(-1L)]
	public long id_language { get; set; } = -1L;

	// Token: 0x170001DB RID: 475
	// (get) Token: 0x06001EA2 RID: 7842 RVA: 0x0010CAEC File Offset: 0x0010ACEC
	// (set) Token: 0x06001EA3 RID: 7843 RVA: 0x0010CAF4 File Offset: 0x0010ACF4
	[DefaultValue(-1L)]
	public long id_religion { get; set; } = -1L;

	// Token: 0x170001DC RID: 476
	// (get) Token: 0x06001EA4 RID: 7844 RVA: 0x0010CAFD File Offset: 0x0010ACFD
	// (set) Token: 0x06001EA5 RID: 7845 RVA: 0x0010CB05 File Offset: 0x0010AD05
	public long left { get; set; }

	// Token: 0x170001DD RID: 477
	// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x0010CB0E File Offset: 0x0010AD0E
	// (set) Token: 0x06001EA7 RID: 7847 RVA: 0x0010CB16 File Offset: 0x0010AD16
	public long joined { get; set; }

	// Token: 0x170001DE RID: 478
	// (get) Token: 0x06001EA8 RID: 7848 RVA: 0x0010CB1F File Offset: 0x0010AD1F
	// (set) Token: 0x06001EA9 RID: 7849 RVA: 0x0010CB27 File Offset: 0x0010AD27
	public long moved { get; set; }

	// Token: 0x170001DF RID: 479
	// (get) Token: 0x06001EAA RID: 7850 RVA: 0x0010CB30 File Offset: 0x0010AD30
	// (set) Token: 0x06001EAB RID: 7851 RVA: 0x0010CB38 File Offset: 0x0010AD38
	public long migrated { get; set; }

	// Token: 0x170001E0 RID: 480
	// (set) Token: 0x06001EAC RID: 7852 RVA: 0x0010CB41 File Offset: 0x0010AD41
	[Preserve]
	[Obsolete("use .name instead", true)]
	public string cityName
	{
		set
		{
			if (!string.IsNullOrEmpty(base.name))
			{
				return;
			}
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			base.name = value;
		}
	}

	// Token: 0x170001E1 RID: 481
	// (set) Token: 0x06001EAD RID: 7853 RVA: 0x0010CB61 File Offset: 0x0010AD61
	[Preserve]
	[Obsolete("use .id instead", true)]
	public long cityID
	{
		set
		{
			if (!value.hasValue())
			{
				return;
			}
			if (base.id.hasValue())
			{
				return;
			}
			base.id = value;
		}
	}

	// Token: 0x170001E2 RID: 482
	// (set) Token: 0x06001EAE RID: 7854 RVA: 0x0010CB81 File Offset: 0x0010AD81
	[Preserve]
	[Obsolete("use .original_actor_asset instead", true)]
	public string race
	{
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.original_actor_asset))
			{
				return;
			}
			this.original_actor_asset = value;
		}
	}

	// Token: 0x06001EAF RID: 7855 RVA: 0x0010CBA4 File Offset: 0x0010ADA4
	public override void Dispose()
	{
		base.Dispose();
		this.zones.Clear();
		List<LeaderEntry> list = this.past_rulers;
		if (list != null)
		{
			list.Clear();
		}
		this.past_rulers = null;
		CityEquipment cityEquipment = this.equipment;
		if (cityEquipment != null)
		{
			cityEquipment.Dispose();
		}
		this.equipment = null;
	}

	// Token: 0x0400166D RID: 5741
	public CityEquipment equipment;

	// Token: 0x04001674 RID: 5748
	public List<ZoneData> zones = new List<ZoneData>();

	// Token: 0x04001678 RID: 5752
	public int total_food_consumed;

	// Token: 0x04001679 RID: 5753
	public float timer_supply;

	// Token: 0x0400167A RID: 5754
	public float timer_trade;

	// Token: 0x0400167B RID: 5755
	public List<LeaderEntry> past_rulers;

	// Token: 0x0400167C RID: 5756
	[DefaultValue(0)]
	public int total_leaders;

	// Token: 0x0400167D RID: 5757
	public double timestamp_kingdom;
}
