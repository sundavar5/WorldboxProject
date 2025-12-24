using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020006E4 RID: 1764
public class UnitMetaBanners : UnitElement, IBaseMetaBanners
{
	// Token: 0x17000322 RID: 802
	// (get) Token: 0x06003888 RID: 14472 RVA: 0x001957A4 File Offset: 0x001939A4
	public int visible_banners
	{
		get
		{
			return this._visible_banners;
		}
	}

	// Token: 0x06003889 RID: 14473 RVA: 0x001957AC File Offset: 0x001939AC
	protected override void Awake()
	{
		base.Awake();
		this._banners.Add(new MetaBannerElement
		{
			banner = this._banner_kingdom,
			check = (() => this.actor.isKingdomCiv()),
			nano = (() => this.actor.kingdom)
		});
		this._banners.Add(new MetaBannerElement
		{
			banner = this._banner_clan,
			check = (() => this.actor.hasClan()),
			nano = (() => this.actor.clan)
		});
		this._banners.Add(new MetaBannerElement
		{
			banner = this._banner_alliance,
			check = (() => this.actor.kingdom.hasAlliance()),
			nano = (() => this.actor.kingdom.getAlliance())
		});
		this._banners.Add(new MetaBannerElement
		{
			banner = this._banner_language,
			check = (() => this.actor.hasLanguage()),
			nano = (() => this.actor.language)
		});
		this._banners.Add(new MetaBannerElement
		{
			banner = this._banner_culture,
			check = (() => this.actor.hasCulture()),
			nano = (() => this.actor.culture)
		});
		this._banners.Add(new MetaBannerElement
		{
			banner = this._banner_religion,
			check = (() => this.actor.hasReligion()),
			nano = (() => this.actor.religion)
		});
		this._banners.Add(new MetaBannerElement
		{
			banner = this._banner_subspecies,
			check = (() => this.actor.hasSubspecies()),
			nano = (() => this.actor.subspecies)
		});
		this._banners.Add(new MetaBannerElement
		{
			banner = this._banner_family,
			check = (() => this.actor.hasFamily()),
			nano = (() => this.actor.family)
		});
		this._banners.Add(new MetaBannerElement
		{
			banner = this._banner_plot,
			check = (() => this.actor.hasPlot()),
			nano = (() => this.actor.plot)
		});
		this._banners.Add(new MetaBannerElement
		{
			banner = this._banner_city,
			check = (() => this.actor.hasCity()),
			nano = (() => this.actor.getCity())
		});
		this._banners.Add(new MetaBannerElement
		{
			banner = this._banner_army,
			check = (() => this.actor.hasArmy()),
			nano = (() => this.actor.army)
		});
		((IBaseMetaBanners)this).enableClickAnimation();
	}

	// Token: 0x0600388A RID: 14474 RVA: 0x00195A85 File Offset: 0x00193C85
	protected override IEnumerator showContent()
	{
		this._banners.Sort((MetaBannerElement x, MetaBannerElement y) => x.banner.transform.GetSiblingIndex().CompareTo(y.banner.transform.GetSiblingIndex()));
		yield return new WaitForSecondsRealtime(0.025f);
		using (List<MetaBannerElement>.Enumerator enumerator = this._banners.GetEnumerator())
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

	// Token: 0x0600388B RID: 14475 RVA: 0x00195A94 File Offset: 0x00193C94
	protected override void clear()
	{
		base.clear();
		this._visible_banners = 0;
		foreach (MetaBannerElement tBannerAsset in this._banners)
		{
			this.metaBannerHide(tBannerAsset);
		}
	}

	// Token: 0x0600388C RID: 14476 RVA: 0x00195AF4 File Offset: 0x00193CF4
	public void metaBannerShow(MetaBannerElement pAsset)
	{
		pAsset.banner.gameObject.SetActive(true);
		pAsset.banner.load(pAsset.nano());
		this._visible_banners++;
	}

	// Token: 0x0600388D RID: 14477 RVA: 0x00195B2B File Offset: 0x00193D2B
	public void metaBannerHide(MetaBannerElement pAsset)
	{
		if (!pAsset.banner.gameObject.activeSelf)
		{
			return;
		}
		pAsset.banner.gameObject.SetActive(false);
	}

	// Token: 0x0600388E RID: 14478 RVA: 0x00195B51 File Offset: 0x00193D51
	public IReadOnlyCollection<MetaBannerElement> getBanners()
	{
		return this._banners;
	}

	// Token: 0x040029F8 RID: 10744
	[SerializeField]
	private CityBanner _banner_city;

	// Token: 0x040029F9 RID: 10745
	[SerializeField]
	private CultureBanner _banner_culture;

	// Token: 0x040029FA RID: 10746
	[SerializeField]
	private AllianceBanner _banner_alliance;

	// Token: 0x040029FB RID: 10747
	[SerializeField]
	private LanguageBanner _banner_language;

	// Token: 0x040029FC RID: 10748
	[SerializeField]
	private ReligionBanner _banner_religion;

	// Token: 0x040029FD RID: 10749
	[SerializeField]
	private ClanBanner _banner_clan;

	// Token: 0x040029FE RID: 10750
	[SerializeField]
	private KingdomBanner _banner_kingdom;

	// Token: 0x040029FF RID: 10751
	[SerializeField]
	private SubspeciesBanner _banner_subspecies;

	// Token: 0x04002A00 RID: 10752
	[SerializeField]
	private FamilyBanner _banner_family;

	// Token: 0x04002A01 RID: 10753
	[SerializeField]
	private PlotBanner _banner_plot;

	// Token: 0x04002A02 RID: 10754
	[SerializeField]
	private ArmyBanner _banner_army;

	// Token: 0x04002A03 RID: 10755
	protected List<MetaBannerElement> _banners = new List<MetaBannerElement>();

	// Token: 0x04002A04 RID: 10756
	private const float DELAY = 0.025f;

	// Token: 0x04002A05 RID: 10757
	private int _visible_banners;
}
