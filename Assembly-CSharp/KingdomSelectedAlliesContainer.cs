using System;

// Token: 0x020006F5 RID: 1781
public class KingdomSelectedAlliesContainer : KingdomDiplomacyContainer<KingdomBanner, Kingdom, KingdomData>
{
	// Token: 0x0600392F RID: 14639 RVA: 0x00198040 File Offset: 0x00196240
	protected override void OnEnable()
	{
	}

	// Token: 0x06003930 RID: 14640 RVA: 0x00198044 File Offset: 0x00196244
	public void update(Kingdom pKingdom)
	{
		this.meta_object = pKingdom;
		this.clear();
		using (ListPool<Kingdom> tList = World.world.wars.getNeutralKingdoms(base.kingdom, false, false))
		{
			if (base.kingdom.hasAlliance())
			{
				foreach (Kingdom tKingdom in base.kingdom.getAlliance().kingdoms_list)
				{
					if (tKingdom != base.kingdom && !tKingdom.isRekt())
					{
						tList.Add(tKingdom);
					}
				}
			}
			this.track_objects.AddRange(tList);
			if (tList.Count != 0)
			{
				foreach (Kingdom ptr in tList)
				{
					Kingdom tKingdom2 = ptr;
					if (!tKingdom2.isRekt())
					{
						KingdomBanner tElement = this.pool_elements.getNext();
						tElement.diplo_banner = true;
						tElement.GetComponent<TipButton>().showOnClick = true;
						tElement.GetComponentInChildren<RotateOnHover>().enabled = true;
						if (!tElement.HasComponent<DraggableLayoutElement>())
						{
							tElement.AddComponent<DraggableLayoutElement>();
						}
						tElement.load(tKingdom2);
					}
				}
			}
		}
	}
}
