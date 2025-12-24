using System;
using Beebyte.Obfuscator;
using SQLite;
using UnityEngine;

// Token: 0x020001F1 RID: 497
[Preserve]
[Skip]
[Serializable]
public class WorldLogMessage
{
	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000E75 RID: 3701 RVA: 0x000C24F9 File Offset: 0x000C06F9
	// (set) Token: 0x06000E76 RID: 3702 RVA: 0x000C2501 File Offset: 0x000C0701
	[NotNull]
	public string asset_id { get; set; }

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000E77 RID: 3703 RVA: 0x000C250A File Offset: 0x000C070A
	// (set) Token: 0x06000E78 RID: 3704 RVA: 0x000C2512 File Offset: 0x000C0712
	[NotNull]
	public int timestamp { get; set; }

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x06000E79 RID: 3705 RVA: 0x000C251B File Offset: 0x000C071B
	// (set) Token: 0x06000E7A RID: 3706 RVA: 0x000C2523 File Offset: 0x000C0723
	public string special1 { get; set; }

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x06000E7B RID: 3707 RVA: 0x000C252C File Offset: 0x000C072C
	// (set) Token: 0x06000E7C RID: 3708 RVA: 0x000C2534 File Offset: 0x000C0734
	public string special2 { get; set; }

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x06000E7D RID: 3709 RVA: 0x000C253D File Offset: 0x000C073D
	// (set) Token: 0x06000E7E RID: 3710 RVA: 0x000C2545 File Offset: 0x000C0745
	public string special3 { get; set; }

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000E7F RID: 3711 RVA: 0x000C254E File Offset: 0x000C074E
	// (set) Token: 0x06000E80 RID: 3712 RVA: 0x000C2556 File Offset: 0x000C0756
	public string color_special_1 { get; set; }

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x06000E81 RID: 3713 RVA: 0x000C255F File Offset: 0x000C075F
	// (set) Token: 0x06000E82 RID: 3714 RVA: 0x000C2567 File Offset: 0x000C0767
	public string color_special_2 { get; set; }

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x06000E83 RID: 3715 RVA: 0x000C2570 File Offset: 0x000C0770
	// (set) Token: 0x06000E84 RID: 3716 RVA: 0x000C2578 File Offset: 0x000C0778
	public string color_special_3 { get; set; }

	// Token: 0x1700007C RID: 124
	// (get) Token: 0x06000E85 RID: 3717 RVA: 0x000C2581 File Offset: 0x000C0781
	// (set) Token: 0x06000E86 RID: 3718 RVA: 0x000C2589 File Offset: 0x000C0789
	public long unit_id { get; set; } = -1L;

	// Token: 0x1700007D RID: 125
	// (get) Token: 0x06000E87 RID: 3719 RVA: 0x000C2592 File Offset: 0x000C0792
	// (set) Token: 0x06000E88 RID: 3720 RVA: 0x000C259A File Offset: 0x000C079A
	public long kingdom_id { get; set; } = -1L;

	// Token: 0x1700007E RID: 126
	// (get) Token: 0x06000E89 RID: 3721 RVA: 0x000C25A3 File Offset: 0x000C07A3
	// (set) Token: 0x06000E8A RID: 3722 RVA: 0x000C25AB File Offset: 0x000C07AB
	public int? x { get; set; }

	// Token: 0x1700007F RID: 127
	// (get) Token: 0x06000E8B RID: 3723 RVA: 0x000C25B4 File Offset: 0x000C07B4
	// (set) Token: 0x06000E8C RID: 3724 RVA: 0x000C25BC File Offset: 0x000C07BC
	public int? y { get; set; }

	// Token: 0x17000080 RID: 128
	// (get) Token: 0x06000E8E RID: 3726 RVA: 0x000C25EC File Offset: 0x000C07EC
	// (set) Token: 0x06000E8D RID: 3725 RVA: 0x000C25C5 File Offset: 0x000C07C5
	[Ignore]
	public Vector2 location
	{
		get
		{
			if (this.x == null || this.y == null)
			{
				return new Vector2(-1f, -1f);
			}
			return new Vector2((float)this.x.Value, (float)this.y.Value);
		}
		set
		{
			this.x = new int?((int)value.x);
			this.y = new int?((int)value.y);
		}
	}

	// Token: 0x17000081 RID: 129
	// (get) Token: 0x06000E90 RID: 3728 RVA: 0x000C2661 File Offset: 0x000C0861
	// (set) Token: 0x06000E8F RID: 3727 RVA: 0x000C264C File Offset: 0x000C084C
	[Ignore]
	public Actor unit
	{
		get
		{
			return World.world.units.get(this.unit_id);
		}
		set
		{
			this.unit_id = ((value != null) ? value.getID() : -1L);
		}
	}

	// Token: 0x17000082 RID: 130
	// (get) Token: 0x06000E92 RID: 3730 RVA: 0x000C268D File Offset: 0x000C088D
	// (set) Token: 0x06000E91 RID: 3729 RVA: 0x000C2678 File Offset: 0x000C0878
	[Ignore]
	public Kingdom kingdom
	{
		get
		{
			return World.world.kingdoms.get(this.kingdom_id);
		}
		set
		{
			this.kingdom_id = ((value != null) ? value.getID() : -1L);
		}
	}

	// Token: 0x17000083 RID: 131
	// (set) Token: 0x06000E93 RID: 3731 RVA: 0x000C26A4 File Offset: 0x000C08A4
	[Ignore]
	public Color color_special1
	{
		set
		{
			this.color_special_1 = Toolbox.colorToHex(value, false);
		}
	}

	// Token: 0x17000084 RID: 132
	// (set) Token: 0x06000E94 RID: 3732 RVA: 0x000C26B8 File Offset: 0x000C08B8
	[Ignore]
	public Color color_special2
	{
		set
		{
			this.color_special_2 = Toolbox.colorToHex(value, false);
		}
	}

	// Token: 0x17000085 RID: 133
	// (set) Token: 0x06000E95 RID: 3733 RVA: 0x000C26CC File Offset: 0x000C08CC
	[Ignore]
	public Color color_special3
	{
		set
		{
			this.color_special_3 = Toolbox.colorToHex(value, false);
		}
	}

	// Token: 0x06000E96 RID: 3734 RVA: 0x000C26E0 File Offset: 0x000C08E0
	public WorldLogMessage()
	{
	}

	// Token: 0x06000E97 RID: 3735 RVA: 0x000C26F8 File Offset: 0x000C08F8
	public WorldLogMessage(WorldLogAsset pAsset, string pSpecial1 = null, string pSpecial2 = null, string pSpecial3 = null)
	{
		this.asset_id = pAsset.id;
		this.special1 = pSpecial1;
		this.special2 = pSpecial2;
		this.special3 = pSpecial3;
		this.x = null;
		this.y = null;
		this.unit_id = -1L;
		this.color_special_1 = null;
		this.color_special_2 = null;
		this.color_special_3 = null;
		this.timestamp = (int)World.world.getCurWorldTime();
		this.unit = null;
	}
}
