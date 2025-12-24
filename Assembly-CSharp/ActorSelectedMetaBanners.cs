using System;

// Token: 0x020006D2 RID: 1746
public class ActorSelectedMetaBanners : UnitMetaBanners, ISelectedTabBanners<Actor>
{
	// Token: 0x06003803 RID: 14339 RVA: 0x00193154 File Offset: 0x00191354
	public void update(Actor pActor)
	{
		this.setActor(pActor);
		this.clear();
		foreach (MetaBannerElement tBannerAsset in this._banners)
		{
			if (tBannerAsset.check())
			{
				base.metaBannerShow(tBannerAsset);
			}
		}
	}

	// Token: 0x06003804 RID: 14340 RVA: 0x001931C4 File Offset: 0x001913C4
	protected override void checkSetActor()
	{
	}

	// Token: 0x06003805 RID: 14341 RVA: 0x001931C6 File Offset: 0x001913C6
	protected override void OnEnable()
	{
	}

	// Token: 0x06003806 RID: 14342 RVA: 0x001931C8 File Offset: 0x001913C8
	protected override void checkSetWindow()
	{
	}

	// Token: 0x06003807 RID: 14343 RVA: 0x001931CA File Offset: 0x001913CA
	public int countVisibleBanners()
	{
		return base.visible_banners;
	}
}
