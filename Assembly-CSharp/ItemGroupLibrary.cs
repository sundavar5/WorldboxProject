using System;

// Token: 0x02000115 RID: 277
public class ItemGroupLibrary : BaseCategoryLibrary<ItemGroupAsset>
{
	// Token: 0x06000887 RID: 2183 RVA: 0x000758D8 File Offset: 0x00073AD8
	public override void init()
	{
		base.init();
		this.add(new ItemGroupAsset
		{
			id = "helmet",
			name = "equipment_group_helmet",
			color = "#BAFFC2"
		});
		this.add(new ItemGroupAsset
		{
			id = "armor",
			name = "equipment_group_armor",
			color = "#BAFFC2"
		});
		this.add(new ItemGroupAsset
		{
			id = "boots",
			name = "equipment_group_boots",
			color = "#BAFFC2"
		});
		this.add(new ItemGroupAsset
		{
			id = "ring",
			name = "equipment_group_ring",
			color = "#BAFFC2"
		});
		this.add(new ItemGroupAsset
		{
			id = "amulet",
			name = "equipment_group_amulet",
			color = "#BAFFC2"
		});
		this.add(new ItemGroupAsset
		{
			id = "sword",
			name = "equipment_group_sword",
			color = "#BAFFC2"
		});
		this.add(new ItemGroupAsset
		{
			id = "axe",
			name = "equipment_group_axe",
			color = "#BAFFC2"
		});
		this.add(new ItemGroupAsset
		{
			id = "hammer",
			name = "equipment_group_hammer",
			color = "#BAFFC2"
		});
		this.add(new ItemGroupAsset
		{
			id = "spear",
			name = "equipment_group_spear",
			color = "#BAFFC2"
		});
		this.add(new ItemGroupAsset
		{
			id = "bow",
			name = "equipment_group_bow",
			color = "#BAFFC2"
		});
		this.add(new ItemGroupAsset
		{
			id = "staff",
			name = "equipment_group_staff",
			color = "#BAFFC2"
		});
		this.add(new ItemGroupAsset
		{
			id = "firearm",
			name = "equipment_group_firearm",
			color = "#BAFFC2"
		});
	}
}
