using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000634 RID: 1588
public class ArmyMetaBanners : ArmyElement, IBaseMetaBanners
{
	// Token: 0x170002CC RID: 716
	// (get) Token: 0x060033BD RID: 13245 RVA: 0x00183DD5 File Offset: 0x00181FD5
	public int visible_banners
	{
		get
		{
			return this._visible_banners;
		}
	}

	// Token: 0x060033BE RID: 13246 RVA: 0x00183DE0 File Offset: 0x00181FE0
	protected override void Awake()
	{
		base.Awake();
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_kingdom,
			check = (() => base.army.hasKingdom()),
			nano = (() => base.army.getKingdom())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_alliance,
			check = (() => base.army.hasKingdom() && base.army.getKingdom().hasAlliance()),
			nano = (() => base.army.getKingdom().getAlliance())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_city,
			check = (() => base.army.hasCity()),
			nano = (() => base.army.getCity())
		});
		((IBaseMetaBanners)this).enableClickAnimation();
	}

	// Token: 0x060033BF RID: 13247 RVA: 0x00183EB9 File Offset: 0x001820B9
	protected override IEnumerator showContent()
	{
		this.banners.Sort((MetaBannerElement x, MetaBannerElement y) => x.banner.transform.GetSiblingIndex().CompareTo(y.banner.transform.GetSiblingIndex()));
		yield return new WaitForSecondsRealtime(0.025f);
		using (List<MetaBannerElement>.Enumerator enumerator = this.banners.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				MetaBannerElement tBannerAsset = enumerator.Current;
				if (tBannerAsset.check())
				{
					this.track_objects.Add(tBannerAsset.nano());
					this.metaBannerShow(tBannerAsset);
				}
			}
			yield break;
		}
		yield break;
	}

	// Token: 0x060033C0 RID: 13248 RVA: 0x00183EC8 File Offset: 0x001820C8
	protected override void clear()
	{
		base.clear();
		this._visible_banners = 0;
		foreach (MetaBannerElement tBannerAsset in this.banners)
		{
			this.metaBannerHide(tBannerAsset);
		}
	}

	// Token: 0x060033C1 RID: 13249 RVA: 0x00183F28 File Offset: 0x00182128
	public void metaBannerShow(MetaBannerElement pAsset)
	{
		pAsset.banner.gameObject.SetActive(true);
		pAsset.banner.load(pAsset.nano());
		this._visible_banners++;
	}

	// Token: 0x060033C2 RID: 13250 RVA: 0x00183F5F File Offset: 0x0018215F
	public void metaBannerHide(MetaBannerElement pAsset)
	{
		if (!pAsset.banner.gameObject.activeSelf)
		{
			return;
		}
		pAsset.banner.gameObject.SetActive(false);
	}

	// Token: 0x060033C3 RID: 13251 RVA: 0x00183F85 File Offset: 0x00182185
	public IReadOnlyCollection<MetaBannerElement> getBanners()
	{
		return this.banners;
	}

	// Token: 0x04002727 RID: 10023
	[SerializeField]
	private CityBanner _banner_city;

	// Token: 0x04002728 RID: 10024
	[SerializeField]
	private AllianceBanner _banner_alliance;

	// Token: 0x04002729 RID: 10025
	[SerializeField]
	private KingdomBanner _banner_kingdom;

	// Token: 0x0400272A RID: 10026
	protected List<MetaBannerElement> banners = new List<MetaBannerElement>();

	// Token: 0x0400272B RID: 10027
	private const float DELAY = 0.025f;

	// Token: 0x0400272C RID: 10028
	private int _visible_banners;
}
