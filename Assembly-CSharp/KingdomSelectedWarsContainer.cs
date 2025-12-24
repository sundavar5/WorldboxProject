using System;

// Token: 0x020006F8 RID: 1784
public class KingdomSelectedWarsContainer : KingdomDiplomacyContainer<WarBanner, War, WarData>
{
	// Token: 0x0600393A RID: 14650 RVA: 0x00198246 File Offset: 0x00196446
	protected override void OnEnable()
	{
	}

	// Token: 0x0600393B RID: 14651 RVA: 0x00198248 File Offset: 0x00196448
	public void update(Kingdom pKingdom)
	{
		this.meta_object = pKingdom;
		this.clear();
		if (!base.kingdom.hasEnemies())
		{
			return;
		}
		using (ListPool<War> tList = new ListPool<War>(base.kingdom.getWars(false)))
		{
			this.track_objects.AddRange(tList);
			foreach (War ptr in tList)
			{
				War tWar = ptr;
				if (!tWar.isRekt())
				{
					WarBanner tElement = this.pool_elements.getNext();
					TipButton tTipButton = tElement.GetComponent<TipButton>();
					if (!tElement.HasComponent<DraggableLayoutElement>())
					{
						tElement.AddComponent<DraggableLayoutElement>();
					}
					tTipButton.showOnClick = true;
					tElement.buttons_enabled = true;
					tElement.load(tWar);
				}
			}
		}
	}
}
