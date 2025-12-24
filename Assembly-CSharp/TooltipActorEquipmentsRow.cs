using System;

// Token: 0x02000784 RID: 1924
public class TooltipActorEquipmentsRow : TooltipItemsRow<TooltipOutlineItem>
{
	// Token: 0x06003D56 RID: 15702 RVA: 0x001ADF94 File Offset: 0x001AC194
	protected override void loadItems()
	{
		this.items_pool.clear(true);
		Actor tActor = this.tooltip_data.actor;
		if (!tActor.canUseItems() || tActor.equipment == null || !tActor.equipment.hasItems())
		{
			base.gameObject.SetActive(false);
			return;
		}
		bool tHasEquipment = false;
		foreach (ActorEquipmentSlot actorEquipmentSlot in tActor.equipment)
		{
			Item tEquipment = actorEquipmentSlot.getItem();
			if (tEquipment != null)
			{
				tHasEquipment = true;
				TooltipOutlineItem tIcon = this.items_pool.getNext();
				tIcon.image.sprite = tEquipment.getSprite();
				if (tEquipment.getQuality() == Rarity.R3_Legendary)
				{
					tIcon.outline.show(RarityLibrary.legendary.color_container);
				}
				else
				{
					tIcon.outline.gameObject.SetActive(false);
				}
			}
		}
		base.gameObject.SetActive(tHasEquipment);
	}
}
