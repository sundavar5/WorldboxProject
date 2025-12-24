using System;
using System.Collections.Generic;
using System.ComponentModel;
using Newtonsoft.Json;
using UnityEngine.Scripting;

// Token: 0x02000280 RID: 640
[Serializable]
public class KingdomData : MetaObjectData
{
	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06001874 RID: 6260 RVA: 0x000EB3D2 File Offset: 0x000E95D2
	// (set) Token: 0x06001875 RID: 6261 RVA: 0x000EB3DA File Offset: 0x000E95DA
	[DefaultValue(-1L)]
	public long allianceID { get; set; } = -1L;

	// Token: 0x17000170 RID: 368
	// (get) Token: 0x06001876 RID: 6262 RVA: 0x000EB3E3 File Offset: 0x000E95E3
	// (set) Token: 0x06001877 RID: 6263 RVA: 0x000EB3EB File Offset: 0x000E95EB
	[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
	public string original_actor_asset { get; set; }

	// Token: 0x17000171 RID: 369
	// (get) Token: 0x06001878 RID: 6264 RVA: 0x000EB3F4 File Offset: 0x000E95F4
	// (set) Token: 0x06001879 RID: 6265 RVA: 0x000EB3FC File Offset: 0x000E95FC
	[DefaultValue(-1L)]
	public long royal_clan_id { get; set; } = -1L;

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x0600187A RID: 6266 RVA: 0x000EB405 File Offset: 0x000E9605
	// (set) Token: 0x0600187B RID: 6267 RVA: 0x000EB40D File Offset: 0x000E960D
	public string motto { get; set; }

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x0600187C RID: 6268 RVA: 0x000EB416 File Offset: 0x000E9616
	// (set) Token: 0x0600187D RID: 6269 RVA: 0x000EB41E File Offset: 0x000E961E
	[DefaultValue(-1L)]
	public long id_culture { get; set; } = -1L;

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x0600187E RID: 6270 RVA: 0x000EB427 File Offset: 0x000E9627
	// (set) Token: 0x0600187F RID: 6271 RVA: 0x000EB42F File Offset: 0x000E962F
	[DefaultValue(-1L)]
	public long id_language { get; set; } = -1L;

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06001880 RID: 6272 RVA: 0x000EB438 File Offset: 0x000E9638
	// (set) Token: 0x06001881 RID: 6273 RVA: 0x000EB440 File Offset: 0x000E9640
	[DefaultValue(-1L)]
	public long id_religion { get; set; } = -1L;

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06001882 RID: 6274 RVA: 0x000EB449 File Offset: 0x000E9649
	// (set) Token: 0x06001883 RID: 6275 RVA: 0x000EB451 File Offset: 0x000E9651
	[DefaultValue(-1L)]
	public long kingID { get; set; } = -1L;

	// Token: 0x17000177 RID: 375
	// (get) Token: 0x06001884 RID: 6276 RVA: 0x000EB45A File Offset: 0x000E965A
	// (set) Token: 0x06001885 RID: 6277 RVA: 0x000EB462 File Offset: 0x000E9662
	[DefaultValue(-1L)]
	public long capitalID { get; set; } = -1L;

	// Token: 0x17000178 RID: 376
	// (get) Token: 0x06001886 RID: 6278 RVA: 0x000EB46B File Offset: 0x000E966B
	// (set) Token: 0x06001887 RID: 6279 RVA: 0x000EB473 File Offset: 0x000E9673
	[DefaultValue(-1L)]
	public long last_capital_id { get; set; } = -1L;

	// Token: 0x17000179 RID: 377
	// (get) Token: 0x06001888 RID: 6280 RVA: 0x000EB47C File Offset: 0x000E967C
	// (set) Token: 0x06001889 RID: 6281 RVA: 0x000EB484 File Offset: 0x000E9684
	[DefaultValue(1)]
	public int last_army_id { get; set; } = 1;

	// Token: 0x1700017A RID: 378
	// (set) Token: 0x0600188A RID: 6282 RVA: 0x000EB48D File Offset: 0x000E968D
	[Preserve]
	[Obsolete("use .color_id instead", true)]
	public int colorId
	{
		set
		{
			if (value == -1)
			{
				return;
			}
			if (base.color_id != 0)
			{
				return;
			}
			base.setColorID(value);
		}
	}

	// Token: 0x1700017B RID: 379
	// (set) Token: 0x0600188B RID: 6283 RVA: 0x000EB4A4 File Offset: 0x000E96A4
	[Preserve]
	[Obsolete("use .original_actor_asset instead", true)]
	public string raceId
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

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x0600188C RID: 6284 RVA: 0x000EB4C4 File Offset: 0x000E96C4
	// (set) Token: 0x0600188D RID: 6285 RVA: 0x000EB4CC File Offset: 0x000E96CC
	[DefaultValue(0)]
	public int banner_background_id { get; set; }

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x0600188E RID: 6286 RVA: 0x000EB4D5 File Offset: 0x000E96D5
	// (set) Token: 0x0600188F RID: 6287 RVA: 0x000EB4DD File Offset: 0x000E96DD
	[DefaultValue(0)]
	public int banner_icon_id { get; set; }

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x06001890 RID: 6288 RVA: 0x000EB4E6 File Offset: 0x000E96E6
	// (set) Token: 0x06001891 RID: 6289 RVA: 0x000EB4EE File Offset: 0x000E96EE
	public long left { get; set; }

	// Token: 0x1700017F RID: 383
	// (get) Token: 0x06001892 RID: 6290 RVA: 0x000EB4F7 File Offset: 0x000E96F7
	// (set) Token: 0x06001893 RID: 6291 RVA: 0x000EB4FF File Offset: 0x000E96FF
	public long joined { get; set; }

	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06001894 RID: 6292 RVA: 0x000EB508 File Offset: 0x000E9708
	// (set) Token: 0x06001895 RID: 6293 RVA: 0x000EB510 File Offset: 0x000E9710
	public long moved { get; set; }

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x06001896 RID: 6294 RVA: 0x000EB519 File Offset: 0x000E9719
	// (set) Token: 0x06001897 RID: 6295 RVA: 0x000EB521 File Offset: 0x000E9721
	public long migrated { get; set; }

	// Token: 0x06001898 RID: 6296 RVA: 0x000EB52A File Offset: 0x000E972A
	public override void Dispose()
	{
		base.Dispose();
		List<string> list = this.saved_traits;
		if (list != null)
		{
			list.Clear();
		}
		this.saved_traits = null;
		List<LeaderEntry> list2 = this.past_rulers;
		if (list2 != null)
		{
			list2.Clear();
		}
		this.past_rulers = null;
	}

	// Token: 0x04001352 RID: 4946
	[DefaultValue(-1.0)]
	public double timestamp_alliance = -1.0;

	// Token: 0x04001353 RID: 4947
	public List<string> saved_traits;

	// Token: 0x04001354 RID: 4948
	[DefaultValue(-1.0)]
	public double timestamp_last_war = -1.0;

	// Token: 0x04001355 RID: 4949
	[DefaultValue(-1.0)]
	public double timestamp_new_conquest = -1.0;

	// Token: 0x04001356 RID: 4950
	[DefaultValue(-1.0)]
	public double timestamp_king_rule = -1.0;

	// Token: 0x04001359 RID: 4953
	public float timer_new_king = 10f;

	// Token: 0x04001360 RID: 4960
	public List<LeaderEntry> past_rulers;

	// Token: 0x04001361 RID: 4961
	[DefaultValue(0)]
	public int total_kings;
}
