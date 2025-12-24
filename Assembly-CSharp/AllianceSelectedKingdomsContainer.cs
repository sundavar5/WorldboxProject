using System;

// Token: 0x02000629 RID: 1577
public class AllianceSelectedKingdomsContainer : AllianceKingdomsContainer
{
	// Token: 0x06003384 RID: 13188 RVA: 0x0018363B File Offset: 0x0018183B
	protected override void OnEnable()
	{
	}

	// Token: 0x06003385 RID: 13189 RVA: 0x00183640 File Offset: 0x00181840
	public void update(Alliance pAlliance)
	{
		this.meta_object = pAlliance;
		this.clear();
		using (ListPool<Kingdom> tList = new ListPool<Kingdom>(base.alliance.kingdoms_hashset))
		{
			this.track_objects.AddRange(tList);
			foreach (Kingdom ptr in tList)
			{
				Kingdom tKingdom = ptr;
				base.showBanner(tKingdom);
			}
		}
	}
}
