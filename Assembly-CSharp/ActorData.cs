using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine.Scripting;

// Token: 0x0200020B RID: 523
[Preserve]
[Serializable]
public class ActorData : BaseObjectData
{
	// Token: 0x170000BD RID: 189
	// (get) Token: 0x06001263 RID: 4707 RVA: 0x000D4CC0 File Offset: 0x000D2EC0
	// (set) Token: 0x06001264 RID: 4708 RVA: 0x000D4CC8 File Offset: 0x000D2EC8
	[DefaultValue(-1L)]
	public long cityID { get; set; } = -1L;

	// Token: 0x170000BE RID: 190
	// (get) Token: 0x06001265 RID: 4709 RVA: 0x000D4CD1 File Offset: 0x000D2ED1
	// (set) Token: 0x06001266 RID: 4710 RVA: 0x000D4CD9 File Offset: 0x000D2ED9
	[DefaultValue(-1L)]
	public long civ_kingdom_id { get; set; } = -1L;

	// Token: 0x170000BF RID: 191
	// (set) Token: 0x06001267 RID: 4711 RVA: 0x000D4CE2 File Offset: 0x000D2EE2
	[Preserve]
	[Obsolete("use .id instead", true)]
	public long actorID
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

	// Token: 0x170000C0 RID: 192
	// (set) Token: 0x06001268 RID: 4712 RVA: 0x000D4D02 File Offset: 0x000D2F02
	[Preserve]
	[Obsolete("use .name instead", true)]
	public string firstName
	{
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			if (!string.IsNullOrEmpty(base.name))
			{
				return;
			}
			base.name = value;
		}
	}

	// Token: 0x170000C1 RID: 193
	// (set) Token: 0x06001269 RID: 4713 RVA: 0x000D4D22 File Offset: 0x000D2F22
	[Preserve]
	[Obsolete("use .asset_id instead", true)]
	public string statsID
	{
		set
		{
			if (string.IsNullOrEmpty(value))
			{
				return;
			}
			if (!string.IsNullOrEmpty(this.asset_id))
			{
				return;
			}
			this.asset_id = value;
		}
	}

	// Token: 0x170000C2 RID: 194
	// (get) Token: 0x0600126A RID: 4714 RVA: 0x000D4D42 File Offset: 0x000D2F42
	// (set) Token: 0x0600126B RID: 4715 RVA: 0x000D4D4A File Offset: 0x000D2F4A
	[DefaultValue("")]
	public string favorite_food { get; set; } = "";

	// Token: 0x170000C3 RID: 195
	// (get) Token: 0x0600126C RID: 4716 RVA: 0x000D4D53 File Offset: 0x000D2F53
	// (set) Token: 0x0600126D RID: 4717 RVA: 0x000D4D5B File Offset: 0x000D2F5B
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	[DefaultValue(ActorSex.Male)]
	public ActorSex sex { get; set; }

	// Token: 0x170000C4 RID: 196
	// (get) Token: 0x0600126E RID: 4718 RVA: 0x000D4D64 File Offset: 0x000D2F64
	// (set) Token: 0x0600126F RID: 4719 RVA: 0x000D4D6C File Offset: 0x000D2F6C
	[DefaultValue(-1)]
	public int head { get; set; } = -1;

	// Token: 0x170000C5 RID: 197
	// (get) Token: 0x06001270 RID: 4720 RVA: 0x000D4D75 File Offset: 0x000D2F75
	// (set) Token: 0x06001271 RID: 4721 RVA: 0x000D4D7D File Offset: 0x000D2F7D
	[DefaultValue(-1L)]
	public long culture { get; set; } = -1L;

	// Token: 0x170000C6 RID: 198
	// (get) Token: 0x06001272 RID: 4722 RVA: 0x000D4D86 File Offset: 0x000D2F86
	// (set) Token: 0x06001273 RID: 4723 RVA: 0x000D4D8E File Offset: 0x000D2F8E
	[DefaultValue(-1L)]
	public long clan { get; set; } = -1L;

	// Token: 0x170000C7 RID: 199
	// (get) Token: 0x06001274 RID: 4724 RVA: 0x000D4D97 File Offset: 0x000D2F97
	// (set) Token: 0x06001275 RID: 4725 RVA: 0x000D4D9F File Offset: 0x000D2F9F
	[DefaultValue(-1L)]
	public long subspecies { get; set; } = -1L;

	// Token: 0x170000C8 RID: 200
	// (get) Token: 0x06001276 RID: 4726 RVA: 0x000D4DA8 File Offset: 0x000D2FA8
	// (set) Token: 0x06001277 RID: 4727 RVA: 0x000D4DB0 File Offset: 0x000D2FB0
	[DefaultValue(-1L)]
	public long language { get; set; } = -1L;

	// Token: 0x170000C9 RID: 201
	// (get) Token: 0x06001278 RID: 4728 RVA: 0x000D4DB9 File Offset: 0x000D2FB9
	// (set) Token: 0x06001279 RID: 4729 RVA: 0x000D4DC1 File Offset: 0x000D2FC1
	[DefaultValue(-1L)]
	public long plot { get; set; } = -1L;

	// Token: 0x170000CA RID: 202
	// (get) Token: 0x0600127A RID: 4730 RVA: 0x000D4DCA File Offset: 0x000D2FCA
	// (set) Token: 0x0600127B RID: 4731 RVA: 0x000D4DD2 File Offset: 0x000D2FD2
	[DefaultValue(-1L)]
	public long religion { get; set; } = -1L;

	// Token: 0x170000CB RID: 203
	// (get) Token: 0x0600127C RID: 4732 RVA: 0x000D4DDB File Offset: 0x000D2FDB
	// (set) Token: 0x0600127D RID: 4733 RVA: 0x000D4DE3 File Offset: 0x000D2FE3
	[DefaultValue(-1L)]
	public long family { get; set; } = -1L;

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x0600127E RID: 4734 RVA: 0x000D4DEC File Offset: 0x000D2FEC
	// (set) Token: 0x0600127F RID: 4735 RVA: 0x000D4DF4 File Offset: 0x000D2FF4
	[DefaultValue(-1L)]
	public long army { get; set; } = -1L;

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x06001280 RID: 4736 RVA: 0x000D4DFD File Offset: 0x000D2FFD
	// (set) Token: 0x06001281 RID: 4737 RVA: 0x000D4E05 File Offset: 0x000D3005
	[DefaultValue(-1L)]
	public long lover { get; set; } = -1L;

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x06001282 RID: 4738 RVA: 0x000D4E0E File Offset: 0x000D300E
	// (set) Token: 0x06001283 RID: 4739 RVA: 0x000D4E16 File Offset: 0x000D3016
	[DefaultValue(-1L)]
	public long best_friend_id { get; set; } = -1L;

	// Token: 0x170000CF RID: 207
	// (get) Token: 0x06001284 RID: 4740 RVA: 0x000D4E1F File Offset: 0x000D301F
	// (set) Token: 0x06001285 RID: 4741 RVA: 0x000D4E27 File Offset: 0x000D3027
	[DefaultValue(0)]
	public int renown { get; set; }

	// Token: 0x170000D0 RID: 208
	// (get) Token: 0x06001286 RID: 4742 RVA: 0x000D4E30 File Offset: 0x000D3030
	// (set) Token: 0x06001287 RID: 4743 RVA: 0x000D4E38 File Offset: 0x000D3038
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string asset_id { get; set; }

	// Token: 0x170000D1 RID: 209
	// (get) Token: 0x06001288 RID: 4744 RVA: 0x000D4E41 File Offset: 0x000D3041
	// (set) Token: 0x06001289 RID: 4745 RVA: 0x000D4E49 File Offset: 0x000D3049
	[DefaultValue(0)]
	public int kills { get; set; }

	// Token: 0x170000D2 RID: 210
	// (get) Token: 0x0600128A RID: 4746 RVA: 0x000D4E52 File Offset: 0x000D3052
	// (set) Token: 0x0600128B RID: 4747 RVA: 0x000D4E5A File Offset: 0x000D305A
	[DefaultValue(0)]
	public int food_consumed { get; set; }

	// Token: 0x170000D3 RID: 211
	// (get) Token: 0x0600128C RID: 4748 RVA: 0x000D4E63 File Offset: 0x000D3063
	// (set) Token: 0x0600128D RID: 4749 RVA: 0x000D4E6B File Offset: 0x000D306B
	[DefaultValue(1)]
	public int age_overgrowth { get; set; } = 1;

	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x0600128E RID: 4750 RVA: 0x000D4E74 File Offset: 0x000D3074
	// (set) Token: 0x0600128F RID: 4751 RVA: 0x000D4E7C File Offset: 0x000D307C
	[DefaultValue(0)]
	public int births { get; set; }

	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x06001290 RID: 4752 RVA: 0x000D4E85 File Offset: 0x000D3085
	// (set) Token: 0x06001291 RID: 4753 RVA: 0x000D4E8D File Offset: 0x000D308D
	[DefaultValue(-1L)]
	public long parent_id_1 { get; set; } = -1L;

	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x06001292 RID: 4754 RVA: 0x000D4E96 File Offset: 0x000D3096
	// (set) Token: 0x06001293 RID: 4755 RVA: 0x000D4E9E File Offset: 0x000D309E
	[DefaultValue(-1L)]
	public long parent_id_2 { get; set; } = -1L;

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x06001294 RID: 4756 RVA: 0x000D4EA7 File Offset: 0x000D30A7
	// (set) Token: 0x06001295 RID: 4757 RVA: 0x000D4EAF File Offset: 0x000D30AF
	[DefaultValue(-1L)]
	public long ancestor_family { get; set; } = -1L;

	// Token: 0x170000D8 RID: 216
	// (get) Token: 0x06001296 RID: 4758 RVA: 0x000D4EB8 File Offset: 0x000D30B8
	// (set) Token: 0x06001297 RID: 4759 RVA: 0x000D4EC0 File Offset: 0x000D30C0
	[DefaultValue(1)]
	public int generation { get; set; } = 1;

	// Token: 0x170000D9 RID: 217
	// (get) Token: 0x06001298 RID: 4760 RVA: 0x000D4EC9 File Offset: 0x000D30C9
	// (set) Token: 0x06001299 RID: 4761 RVA: 0x000D4ED1 File Offset: 0x000D30D1
	[DefaultValue(0)]
	public int pollen { get; set; }

	// Token: 0x170000DA RID: 218
	// (get) Token: 0x0600129A RID: 4762 RVA: 0x000D4EDA File Offset: 0x000D30DA
	// (set) Token: 0x0600129B RID: 4763 RVA: 0x000D4EE2 File Offset: 0x000D30E2
	[DefaultValue(0)]
	public int loot { get; set; }

	// Token: 0x170000DB RID: 219
	// (get) Token: 0x0600129C RID: 4764 RVA: 0x000D4EEB File Offset: 0x000D30EB
	// (set) Token: 0x0600129D RID: 4765 RVA: 0x000D4EF3 File Offset: 0x000D30F3
	[DefaultValue(0)]
	public int money { get; set; }

	// Token: 0x170000DC RID: 220
	// (get) Token: 0x0600129E RID: 4766 RVA: 0x000D4EFC File Offset: 0x000D30FC
	// (set) Token: 0x0600129F RID: 4767 RVA: 0x000D4F04 File Offset: 0x000D3104
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	[DefaultValue(0)]
	public int nutrition { get; set; }

	// Token: 0x170000DD RID: 221
	// (get) Token: 0x060012A0 RID: 4768 RVA: 0x000D4F0D File Offset: 0x000D310D
	// (set) Token: 0x060012A1 RID: 4769 RVA: 0x000D4F15 File Offset: 0x000D3115
	[DefaultValue(0)]
	public int happiness { get; set; }

	// Token: 0x170000DE RID: 222
	// (get) Token: 0x060012A2 RID: 4770 RVA: 0x000D4F1E File Offset: 0x000D311E
	// (set) Token: 0x060012A3 RID: 4771 RVA: 0x000D4F26 File Offset: 0x000D3126
	[DefaultValue(0)]
	public int stamina { get; set; }

	// Token: 0x170000DF RID: 223
	// (get) Token: 0x060012A4 RID: 4772 RVA: 0x000D4F2F File Offset: 0x000D312F
	// (set) Token: 0x060012A5 RID: 4773 RVA: 0x000D4F37 File Offset: 0x000D3137
	[DefaultValue(0)]
	public int mana { get; set; }

	// Token: 0x170000E0 RID: 224
	// (get) Token: 0x060012A6 RID: 4774 RVA: 0x000D4F40 File Offset: 0x000D3140
	// (set) Token: 0x060012A7 RID: 4775 RVA: 0x000D4F48 File Offset: 0x000D3148
	[DefaultValue(1)]
	public int level { get; set; } = 1;

	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x060012A8 RID: 4776 RVA: 0x000D4F51 File Offset: 0x000D3151
	// (set) Token: 0x060012A9 RID: 4777 RVA: 0x000D4F59 File Offset: 0x000D3159
	[DefaultValue(0)]
	public int experience { get; set; }

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x060012AA RID: 4778 RVA: 0x000D4F62 File Offset: 0x000D3162
	// (set) Token: 0x060012AB RID: 4779 RVA: 0x000D4F6A File Offset: 0x000D316A
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	[DefaultValue(0)]
	public int phenotype_shade { get; set; }

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x060012AC RID: 4780 RVA: 0x000D4F73 File Offset: 0x000D3173
	// (set) Token: 0x060012AD RID: 4781 RVA: 0x000D4F7B File Offset: 0x000D317B
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	[DefaultValue(0)]
	public int phenotype_index { get; set; }

	// Token: 0x170000E4 RID: 228
	// (set) Token: 0x060012AE RID: 4782 RVA: 0x000D4F84 File Offset: 0x000D3184
	[Preserve]
	[Obsolete("use .nutrition instead", true)]
	public int hunger
	{
		set
		{
			if (this.nutrition != 0)
			{
				return;
			}
			if (value == 0)
			{
				return;
			}
			this.nutrition = value;
		}
	}

	// Token: 0x170000E5 RID: 229
	// (set) Token: 0x060012AF RID: 4783 RVA: 0x000D4F9A File Offset: 0x000D319A
	[Preserve]
	[Obsolete("use .saved_traits instead", true)]
	public List<string> traits
	{
		set
		{
			if (value == null || value.Count == 0)
			{
				return;
			}
			List<string> list = this.saved_traits;
			if (list != null && list.Count > 0)
			{
				return;
			}
			this.saved_traits = value;
		}
	}

	// Token: 0x170000E6 RID: 230
	// (set) Token: 0x060012B0 RID: 4784 RVA: 0x000D4FC7 File Offset: 0x000D31C7
	[Preserve]
	[Obsolete("use .sex instead", true)]
	public ActorGender gender
	{
		set
		{
			if (this.sex != ActorSex.Male)
			{
				return;
			}
			if (value == ActorGender.Male)
			{
				this.sex = ActorSex.Male;
				return;
			}
			if (value != ActorGender.Female)
			{
				this.sex = ActorSex.Male;
				return;
			}
			this.sex = ActorSex.Female;
		}
	}

	// Token: 0x060012B1 RID: 4785 RVA: 0x000D4FF3 File Offset: 0x000D31F3
	public int getAge()
	{
		return Date.getYearsSince(base.created_time) + this.age_overgrowth;
	}

	// Token: 0x04001117 RID: 4375
	public List<long> saved_items;

	// Token: 0x04001118 RID: 4376
	[DefaultValue(null)]
	public ActorBag inventory;

	// Token: 0x0400111B RID: 4379
	public int x;

	// Token: 0x0400111C RID: 4380
	public int y;

	// Token: 0x0400111E RID: 4382
	[DefaultValue(-1L)]
	public long transportID = -1L;

	// Token: 0x0400112C RID: 4396
	[DefaultValue(-1L)]
	public long homeBuildingID = -1L;

	// Token: 0x0400112E RID: 4398
	[DefaultValue(UnitProfession.Nothing)]
	public UnitProfession profession;

	// Token: 0x04001142 RID: 4418
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	public List<string> saved_traits;
}
