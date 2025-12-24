using System;

// Token: 0x0200027C RID: 636
public class DeadKingdom : Kingdom
{
	// Token: 0x060017C9 RID: 6089 RVA: 0x000E8678 File Offset: 0x000E6878
	public override void loadData(KingdomData pData)
	{
		this.setData(pData);
		this.data.load();
		ActorAsset tAsset = this.getActorAsset();
		this.asset = AssetManager.kingdoms.get(tAsset.kingdom_id_civilization);
	}

	// Token: 0x060017CA RID: 6090 RVA: 0x000E86B4 File Offset: 0x000E68B4
	public override int getAge()
	{
		int tStartYear = Date.getYear(this.data.created_time);
		return Date.getYear(this.data.died_time) - tStartYear;
	}

	// Token: 0x060017CB RID: 6091 RVA: 0x000E86E4 File Offset: 0x000E68E4
	public override string getMotto()
	{
		return this.data.motto;
	}
}
