using System;
using System.Collections;

// Token: 0x02000626 RID: 1574
public class AllianceKingdomsContainer : AllianceBannersContainer<KingdomBanner, Kingdom, KingdomData>
{
	// Token: 0x06003373 RID: 13171 RVA: 0x001832D0 File Offset: 0x001814D0
	protected override IEnumerator showContent()
	{
		AllianceKingdomsContainer.<showContent>d__0 <showContent>d__ = new AllianceKingdomsContainer.<showContent>d__0(0);
		<showContent>d__.<>4__this = this;
		return <showContent>d__;
	}

	// Token: 0x06003374 RID: 13172 RVA: 0x001832DF File Offset: 0x001814DF
	protected void showBanner(Kingdom pKingdom)
	{
		KingdomBanner next = this.pool_elements.getNext();
		next.load(pKingdom);
		next.AddComponent<DraggableLayoutElement>();
	}
}
