using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x020002FB RID: 763
[Serializable]
public class WarData : MetaObjectData
{
	// Token: 0x170001C2 RID: 450
	// (get) Token: 0x06001CED RID: 7405 RVA: 0x00104491 File Offset: 0x00102691
	// (set) Token: 0x06001CEE RID: 7406 RVA: 0x00104499 File Offset: 0x00102699
	[DefaultValue(-1L)]
	public long main_attacker { get; set; } = -1L;

	// Token: 0x170001C3 RID: 451
	// (get) Token: 0x06001CEF RID: 7407 RVA: 0x001044A2 File Offset: 0x001026A2
	// (set) Token: 0x06001CF0 RID: 7408 RVA: 0x001044AA File Offset: 0x001026AA
	[DefaultValue(-1L)]
	public long main_defender { get; set; } = -1L;

	// Token: 0x170001C4 RID: 452
	// (get) Token: 0x06001CF1 RID: 7409 RVA: 0x001044B3 File Offset: 0x001026B3
	// (set) Token: 0x06001CF2 RID: 7410 RVA: 0x001044BB File Offset: 0x001026BB
	public int dead_attackers { get; set; }

	// Token: 0x170001C5 RID: 453
	// (get) Token: 0x06001CF3 RID: 7411 RVA: 0x001044C4 File Offset: 0x001026C4
	// (set) Token: 0x06001CF4 RID: 7412 RVA: 0x001044CC File Offset: 0x001026CC
	public int dead_defenders { get; set; }

	// Token: 0x170001C6 RID: 454
	// (get) Token: 0x06001CF5 RID: 7413 RVA: 0x001044D5 File Offset: 0x001026D5
	// (set) Token: 0x06001CF6 RID: 7414 RVA: 0x001044DD File Offset: 0x001026DD
	public string started_by_actor_name { get; set; } = string.Empty;

	// Token: 0x170001C7 RID: 455
	// (get) Token: 0x06001CF7 RID: 7415 RVA: 0x001044E6 File Offset: 0x001026E6
	// (set) Token: 0x06001CF8 RID: 7416 RVA: 0x001044EE File Offset: 0x001026EE
	[DefaultValue(-1L)]
	public long started_by_actor_id { get; set; } = -1L;

	// Token: 0x170001C8 RID: 456
	// (get) Token: 0x06001CF9 RID: 7417 RVA: 0x001044F7 File Offset: 0x001026F7
	// (set) Token: 0x06001CFA RID: 7418 RVA: 0x001044FF File Offset: 0x001026FF
	public string started_by_kingdom_name { get; set; } = string.Empty;

	// Token: 0x170001C9 RID: 457
	// (get) Token: 0x06001CFB RID: 7419 RVA: 0x00104508 File Offset: 0x00102708
	// (set) Token: 0x06001CFC RID: 7420 RVA: 0x00104510 File Offset: 0x00102710
	[DefaultValue(-1L)]
	public long started_by_kingdom_id { get; set; } = -1L;

	// Token: 0x170001CA RID: 458
	// (get) Token: 0x06001CFD RID: 7421 RVA: 0x00104519 File Offset: 0x00102719
	// (set) Token: 0x06001CFE RID: 7422 RVA: 0x00104521 File Offset: 0x00102721
	[DefaultValue("normal")]
	public string war_type { get; set; } = "normal";

	// Token: 0x06001CFF RID: 7423 RVA: 0x0010452A File Offset: 0x0010272A
	public override void Dispose()
	{
		this.list_attackers.Clear();
		this.list_defenders.Clear();
		base.Dispose();
	}

	// Token: 0x040015DF RID: 5599
	public List<long> list_attackers = new List<long>();

	// Token: 0x040015E0 RID: 5600
	public List<long> list_defenders = new List<long>();

	// Token: 0x040015E7 RID: 5607
	public List<long> past_attackers = new List<long>();

	// Token: 0x040015E8 RID: 5608
	public List<long> past_defenders = new List<long>();

	// Token: 0x040015E9 RID: 5609
	public List<long> died_attackers = new List<long>();

	// Token: 0x040015EA RID: 5610
	public List<long> died_defenders = new List<long>();

	// Token: 0x040015EC RID: 5612
	[DefaultValue(WarWinner.Nobody)]
	public WarWinner winner;
}
