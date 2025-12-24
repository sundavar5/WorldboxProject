using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000659 RID: 1625
public class CityMetaBanners : CityElement, IBaseMetaBanners
{
	// Token: 0x060034A5 RID: 13477 RVA: 0x00186578 File Offset: 0x00184778
	protected override void Awake()
	{
		base.Awake();
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_kingdom,
			check = (() => !base.city.kingdom.isRekt() && !base.city.kingdom.isNeutral()),
			nano = (() => base.city.kingdom)
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_clan,
			check = (() => base.city.hasLeader() && base.city.leader.hasClan()),
			nano = (() => base.city.leader.clan)
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_alliance,
			check = (() => base.city.kingdom.hasAlliance()),
			nano = (() => base.city.kingdom.getAlliance())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_language,
			check = (() => base.city.hasLanguage()),
			nano = (() => base.city.getLanguage())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_culture,
			check = (() => base.city.hasCulture()),
			nano = (() => base.city.getCulture())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_religion,
			check = (() => base.city.hasReligion()),
			nano = (() => base.city.getReligion())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_subspecies,
			check = (() => !base.city.getMainSubspecies().isRekt()),
			nano = (() => base.city.getMainSubspecies())
		});
		this.banners.Add(new MetaBannerElement
		{
			banner = this._banner_army,
			check = (() => base.city.hasArmy()),
			nano = (() => base.city.getArmy())
		});
		((IBaseMetaBanners)this).enableClickAnimation();
	}

	// Token: 0x060034A6 RID: 13478 RVA: 0x00186791 File Offset: 0x00184991
	protected override IEnumerator showContent()
	{
		this.banners.Sort((MetaBannerElement x, MetaBannerElement y) => x.banner.transform.GetSiblingIndex().CompareTo(y.banner.transform.GetSiblingIndex()));
		yield return new WaitForSecondsRealtime(0.025f);
		if (base.city.kingdom.isNeutral())
		{
			yield break;
		}
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

	// Token: 0x060034A7 RID: 13479 RVA: 0x001867A0 File Offset: 0x001849A0
	protected override void clear()
	{
		base.clear();
		foreach (MetaBannerElement tBannerAsset in this.banners)
		{
			this.metaBannerHide(tBannerAsset);
		}
		this.visible_banners = 0;
	}

	// Token: 0x060034A8 RID: 13480 RVA: 0x00186800 File Offset: 0x00184A00
	public void metaBannerShow(MetaBannerElement pAsset)
	{
		pAsset.banner.gameObject.SetActive(true);
		pAsset.banner.load(pAsset.nano());
		this.visible_banners++;
	}

	// Token: 0x060034A9 RID: 13481 RVA: 0x00186837 File Offset: 0x00184A37
	public void metaBannerHide(MetaBannerElement pAsset)
	{
		if (!pAsset.banner.gameObject.activeSelf)
		{
			return;
		}
		pAsset.banner.gameObject.SetActive(false);
	}

	// Token: 0x060034AA RID: 13482 RVA: 0x0018685D File Offset: 0x00184A5D
	public IReadOnlyCollection<MetaBannerElement> getBanners()
	{
		return this.banners;
	}

	// Token: 0x040027AA RID: 10154
	[SerializeField]
	private KingdomBanner _banner_kingdom;

	// Token: 0x040027AB RID: 10155
	[SerializeField]
	private ClanBanner _banner_clan;

	// Token: 0x040027AC RID: 10156
	[SerializeField]
	private AllianceBanner _banner_alliance;

	// Token: 0x040027AD RID: 10157
	[SerializeField]
	private LanguageBanner _banner_language;

	// Token: 0x040027AE RID: 10158
	[SerializeField]
	private CultureBanner _banner_culture;

	// Token: 0x040027AF RID: 10159
	[SerializeField]
	private ReligionBanner _banner_religion;

	// Token: 0x040027B0 RID: 10160
	[SerializeField]
	private SubspeciesBanner _banner_subspecies;

	// Token: 0x040027B1 RID: 10161
	[SerializeField]
	private ArmyBanner _banner_army;

	// Token: 0x040027B2 RID: 10162
	protected List<MetaBannerElement> banners = new List<MetaBannerElement>();

	// Token: 0x040027B3 RID: 10163
	private const float DELAY = 0.025f;

	// Token: 0x040027B4 RID: 10164
	protected int visible_banners;
}
