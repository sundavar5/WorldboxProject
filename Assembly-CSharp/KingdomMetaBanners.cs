using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006F4 RID: 1780
public class KingdomMetaBanners : KingdomElement, IBaseMetaBanners
{
	// Token: 0x0600391A RID: 14618 RVA: 0x00197CC4 File Offset: 0x00195EC4
	protected override void Awake()
	{
		base.Awake();
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_city,
			check = (() => base.kingdom.hasCapital()),
			nano = (() => base.kingdom.capital)
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_clan,
			check = (() => base.kingdom.getKingClan() != null),
			nano = (() => base.kingdom.getKingClan())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_alliance,
			check = (() => base.kingdom.hasAlliance()),
			nano = (() => base.kingdom.getAlliance())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_language,
			check = (() => base.kingdom.hasLanguage()),
			nano = (() => base.kingdom.getLanguage())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_culture,
			check = (() => base.kingdom.hasCulture()),
			nano = (() => base.kingdom.getCulture())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_religion,
			check = (() => base.kingdom.hasReligion()),
			nano = (() => base.kingdom.getReligion())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_subspecies,
			check = (() => base.kingdom.getMainSubspecies() != null),
			nano = (() => base.kingdom.getMainSubspecies())
		});
		((IBaseMetaBanners)this).enableClickAnimation();
	}

	// Token: 0x0600391B RID: 14619 RVA: 0x00197E9D File Offset: 0x0019609D
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

	// Token: 0x0600391C RID: 14620 RVA: 0x00197EAC File Offset: 0x001960AC
	protected override void clear()
	{
		base.clear();
		foreach (MetaBannerElement tBannerAsset in this.banners)
		{
			this.metaBannerHide(tBannerAsset);
		}
		this.visible_banners = 0;
	}

	// Token: 0x0600391D RID: 14621 RVA: 0x00197F0C File Offset: 0x0019610C
	public void metaBannerShow(MetaBannerElement pAsset)
	{
		pAsset.banner.gameObject.SetActive(true);
		pAsset.banner.load(pAsset.nano());
		this.visible_banners++;
	}

	// Token: 0x0600391E RID: 14622 RVA: 0x00197F43 File Offset: 0x00196143
	public void metaBannerHide(MetaBannerElement pAsset)
	{
		if (!pAsset.banner.gameObject.activeSelf)
		{
			return;
		}
		pAsset.banner.gameObject.SetActive(false);
	}

	// Token: 0x0600391F RID: 14623 RVA: 0x00197F69 File Offset: 0x00196169
	public IReadOnlyCollection<MetaBannerElement> getBanners()
	{
		return this.banners;
	}

	// Token: 0x04002A3E RID: 10814
	[SerializeField]
	private CityBanner _banner_city;

	// Token: 0x04002A3F RID: 10815
	[SerializeField]
	private CultureBanner _banner_culture;

	// Token: 0x04002A40 RID: 10816
	[SerializeField]
	private AllianceBanner _banner_alliance;

	// Token: 0x04002A41 RID: 10817
	[SerializeField]
	private LanguageBanner _banner_language;

	// Token: 0x04002A42 RID: 10818
	[SerializeField]
	private ReligionBanner _banner_religion;

	// Token: 0x04002A43 RID: 10819
	[SerializeField]
	private ClanBanner _banner_clan;

	// Token: 0x04002A44 RID: 10820
	[SerializeField]
	private SubspeciesBanner _banner_subspecies;

	// Token: 0x04002A45 RID: 10821
	protected List<MetaBannerElement> banners = new List<MetaBannerElement>();

	// Token: 0x04002A46 RID: 10822
	private const float DELAY = 0.025f;

	// Token: 0x04002A47 RID: 10823
	protected int visible_banners;
}
