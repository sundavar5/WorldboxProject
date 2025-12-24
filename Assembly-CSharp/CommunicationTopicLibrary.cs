using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000D9 RID: 217
public class CommunicationTopicLibrary : AssetLibrary<CommunicationAsset>
{
	// Token: 0x0600065D RID: 1629 RVA: 0x0005F9A0 File Offset: 0x0005DBA0
	public override void init()
	{
		CommunicationAsset communicationAsset = new CommunicationAsset();
		communicationAsset.id = "emotions";
		communicationAsset.rate = 0.9f;
		communicationAsset.check = ((Actor pActor) => pActor.hasEmotions());
		communicationAsset.pot_fill = delegate(Actor pActor, ListPool<Sprite> pPotSprites)
		{
			Sprite tHappinessSprite = HappinessHelper.getSpriteBasedOnHappinessValue(pActor.getHappiness());
			for (int i = 0; i < 3; i++)
			{
				pPotSprites.Add(tHappinessSprite);
			}
			if (!pActor.hasHappinessHistory())
			{
				return;
			}
			foreach (HappinessHistory tHistoryEntity in pActor.happiness_change_history)
			{
				Sprite tSprite = tHistoryEntity.asset.getSprite();
				pPotSprites.Add(tSprite);
			}
		};
		this.add(communicationAsset);
		CommunicationAsset communicationAsset2 = new CommunicationAsset();
		communicationAsset2.id = "is_housed";
		communicationAsset2.rate = 0.2f;
		communicationAsset2.check = ((Actor pActor) => pActor.hasCity() && pActor.hasHouse());
		communicationAsset2.pot_fill = delegate(Actor _, ListPool<Sprite> pPotSprites)
		{
			pPotSprites.AddRange(this._cached_sprites_housed);
		};
		this.add(communicationAsset2);
		CommunicationAsset communicationAsset3 = new CommunicationAsset();
		communicationAsset3.id = "is_homeless";
		communicationAsset3.rate = 0.4f;
		communicationAsset3.check = ((Actor pActor) => pActor.hasCity() && !pActor.hasHouse());
		communicationAsset3.pot_fill = delegate(Actor _, ListPool<Sprite> pPotSprites)
		{
			pPotSprites.AddRange(this._cached_sprites_homeless);
		};
		this.add(communicationAsset3);
		CommunicationAsset communicationAsset4 = new CommunicationAsset();
		communicationAsset4.id = "favorite_food";
		communicationAsset4.rate = 0.4f;
		communicationAsset4.check = ((Actor pActor) => pActor.hasFavoriteFood());
		communicationAsset4.pot_fill = delegate(Actor pActor, ListPool<Sprite> pPotSprites)
		{
			Sprite tSprite = pActor.favorite_food_asset.getSpriteIcon();
			if (tSprite != null)
			{
				pPotSprites.Add(tSprite);
			}
		};
		this.add(communicationAsset4);
		CommunicationAsset communicationAsset5 = new CommunicationAsset();
		communicationAsset5.id = "religion";
		communicationAsset5.rate = 0.2f;
		communicationAsset5.check = ((Actor pActor) => pActor.hasReligion());
		communicationAsset5.pot_fill = delegate(Actor pActor, ListPool<Sprite> pPotSprites)
		{
			Sprite tSprite = pActor.religion.getTopicSprite();
			if (tSprite != null)
			{
				pPotSprites.Add(tSprite);
			}
			pPotSprites.AddRange(this._cached_sprites_religion);
		};
		this.add(communicationAsset5);
		CommunicationAsset communicationAsset6 = new CommunicationAsset();
		communicationAsset6.id = "culture";
		communicationAsset6.rate = 0.15f;
		communicationAsset6.check = ((Actor pActor) => pActor.hasCulture());
		communicationAsset6.pot_fill = delegate(Actor pActor, ListPool<Sprite> pPotSprites)
		{
			Sprite tSprite = pActor.culture.getTopicSprite();
			if (tSprite != null)
			{
				pPotSprites.Add(tSprite);
			}
			pPotSprites.AddRange(this._cached_sprites_culture);
		};
		this.add(communicationAsset6);
		CommunicationAsset communicationAsset7 = new CommunicationAsset();
		communicationAsset7.id = "equipment";
		communicationAsset7.rate = 0.2f;
		communicationAsset7.check = ((Actor pActor) => pActor.hasEquipment());
		communicationAsset7.pot_fill = delegate(Actor pActor, ListPool<Sprite> pPotSprites)
		{
			foreach (ActorEquipmentSlot actorEquipmentSlot in pActor.equipment)
			{
				Sprite tSprite = actorEquipmentSlot.getItem().getAsset().getSprite();
				if (tSprite != null)
				{
					pPotSprites.Add(tSprite);
				}
			}
		};
		this.add(communicationAsset7);
		CommunicationAsset communicationAsset8 = new CommunicationAsset();
		communicationAsset8.id = "language";
		communicationAsset8.rate = 0.15f;
		communicationAsset8.check = ((Actor pActor) => pActor.hasLanguage());
		communicationAsset8.pot_fill = delegate(Actor pActor, ListPool<Sprite> pPotSprites)
		{
			Sprite tSprite = pActor.language.getTopicSprite();
			if (tSprite != null)
			{
				pPotSprites.Add(tSprite);
			}
		};
		this.add(communicationAsset8);
		CommunicationAsset communicationAsset9 = new CommunicationAsset();
		communicationAsset9.id = "actor_traits";
		communicationAsset9.rate = 0.3f;
		communicationAsset9.check = ((Actor pActor) => pActor.hasTraits());
		communicationAsset9.pot_fill = delegate(Actor pActor, ListPool<Sprite> pPotSprites)
		{
			Sprite tSprite = pActor.getTopicSpriteTrait();
			if (tSprite != null)
			{
				pPotSprites.Add(tSprite);
			}
		};
		this.add(communicationAsset9);
		CommunicationAsset communicationAsset10 = new CommunicationAsset();
		communicationAsset10.id = "family";
		communicationAsset10.rate = 0.3f;
		communicationAsset10.check = ((Actor pActor) => pActor.hasFamily());
		communicationAsset10.pot_fill = delegate(Actor _, ListPool<Sprite> pPotSprites)
		{
			pPotSprites.AddRange(this._cached_sprites_family);
		};
		this.add(communicationAsset10);
		CommunicationAsset communicationAsset11 = new CommunicationAsset();
		communicationAsset11.id = "kingdom_civ";
		communicationAsset11.rate = 0.2f;
		communicationAsset11.check = ((Actor pActor) => pActor.isKingdomCiv());
		communicationAsset11.pot_fill = delegate(Actor pActor, ListPool<Sprite> pPotSprites)
		{
			Sprite tSprite = pActor.kingdom.getTopicSprite();
			if (tSprite != null)
			{
				pPotSprites.Add(tSprite);
			}
			pPotSprites.AddRange(this._cached_sprites_kingdom);
		};
		this.add(communicationAsset11);
		CommunicationAsset communicationAsset12 = new CommunicationAsset();
		communicationAsset12.id = "statuses";
		communicationAsset12.rate = 0.7f;
		communicationAsset12.check = ((Actor pActor) => pActor.hasAnyStatusEffect());
		communicationAsset12.pot_fill = delegate(Actor pActor, ListPool<Sprite> pPotSprites)
		{
			foreach (Status tData in pActor.getStatuses())
			{
				pPotSprites.Add(tData.asset.getSprite());
			}
		};
		this.add(communicationAsset12);
		CommunicationAsset communicationAsset13 = new CommunicationAsset();
		communicationAsset13.id = "city";
		communicationAsset13.rate = 0.3f;
		communicationAsset13.check = ((Actor pActor) => pActor.hasCity());
		communicationAsset13.pot_fill = delegate(Actor pActor, ListPool<Sprite> pPotSprites)
		{
			pPotSprites.AddRange(this._cached_sprites_city);
			if (pActor.city.hasStorages())
			{
				ResourceAsset tResourceAsset = pActor.city.storages.GetRandom<Building>().resources.getRandomFoodAsset();
				if (tResourceAsset != null)
				{
					Sprite tSprite = tResourceAsset.getSpriteIcon();
					if (tSprite != null)
					{
						pPotSprites.Add(tSprite);
					}
				}
			}
		};
		this.add(communicationAsset13);
		CommunicationAsset communicationAsset14 = new CommunicationAsset();
		communicationAsset14.id = "city_boats";
		communicationAsset14.rate = 0.1f;
		communicationAsset14.check = ((Actor pActor) => pActor.hasCity() && pActor.city.countBoats() > 0);
		communicationAsset14.pot_fill = delegate(Actor _, ListPool<Sprite> pPotSprites)
		{
			pPotSprites.AddRange(this._cached_sprites_boats_water);
		};
		this.add(communicationAsset14);
		CommunicationAsset communicationAsset15 = new CommunicationAsset();
		communicationAsset15.id = "clan";
		communicationAsset15.rate = 0.3f;
		communicationAsset15.check = ((Actor pActor) => pActor.hasClan());
		communicationAsset15.pot_fill = delegate(Actor pActor, ListPool<Sprite> pPotSprites)
		{
			Sprite tSprite = pActor.clan.getTopicSprite();
			if (tSprite != null)
			{
				pPotSprites.Add(tSprite);
			}
			pPotSprites.AddRange(this._cached_sprites_clan);
		};
		this.add(communicationAsset15);
		CommunicationAsset communicationAsset16 = new CommunicationAsset();
		communicationAsset16.id = "time_and_death";
		communicationAsset16.rate = 0.3f;
		communicationAsset16.check = ((Actor _) => true);
		communicationAsset16.pot_fill = delegate(Actor _, ListPool<Sprite> pPotSprites)
		{
			pPotSprites.AddRange(this._cached_sprites_time_and_death);
		};
		this.add(communicationAsset16);
		CommunicationAsset communicationAsset17 = new CommunicationAsset();
		communicationAsset17.id = "world_subspecies";
		communicationAsset17.rate = 0.1f;
		communicationAsset17.check = ((Actor _) => World.world.subspecies.hasAny());
		communicationAsset17.pot_fill = delegate(Actor _, ListPool<Sprite> pPotSprites)
		{
			pPotSprites.AddRange(this._cached_sprites_general_topics);
		};
		this.add(communicationAsset17);
		CommunicationAsset communicationAsset18 = new CommunicationAsset();
		communicationAsset18.id = "general_topics";
		communicationAsset18.rate = 1f;
		communicationAsset18.check = ((Actor _) => true);
		communicationAsset18.pot_fill = delegate(Actor _, ListPool<Sprite> pPotSprites)
		{
			pPotSprites.AddRange(this._cached_sprites_general_topics);
		};
		this.add(communicationAsset18);
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x00060061 File Offset: 0x0005E261
	public override void linkAssets()
	{
		this.cacheSpritesGeneralTopics();
		base.linkAssets();
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x00060070 File Offset: 0x0005E270
	public Sprite getTopicSprite(Actor pActor)
	{
		Sprite random;
		using (ListPool<Sprite> tPotSprites = new ListPool<Sprite>())
		{
			this.list.Shuffle<CommunicationAsset>();
			foreach (CommunicationAsset tAsset in this.list)
			{
				if (Randy.randomChance(tAsset.rate) && tAsset.check(pActor))
				{
					tAsset.pot_fill(pActor, tPotSprites);
					if (tPotSprites.Count > 10)
					{
						break;
					}
				}
			}
			random = tPotSprites.GetRandom<Sprite>();
		}
		return random;
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x00060124 File Offset: 0x0005E324
	private void cacheSpritesGeneralTopics()
	{
		this._cached_sprites_housed.Add(SpriteTextureLoader.getSprite("ui/Icons/iconHoused"));
		this._cached_sprites_homeless.Add(SpriteTextureLoader.getSprite("ui/Icons/iconHomeless"));
		this._cached_sprites_religion.Add(SpriteTextureLoader.getSprite("ui/Icons/iconReligion"));
		this._cached_sprites_religion.Add(SpriteTextureLoader.getSprite("ui/Icons/iconReligionList"));
		this._cached_sprites_culture.Add(SpriteTextureLoader.getSprite("ui/Icons/iconCulture"));
		this._cached_sprites_culture.Add(SpriteTextureLoader.getSprite("ui/Icons/iconCultureList"));
		this._cached_sprites_family.Add(SpriteTextureLoader.getSprite("ui/Icons/iconFamily"));
		this._cached_sprites_family.Add(SpriteTextureLoader.getSprite("ui/Icons/iconFamilyList"));
		this._cached_sprites_family.Add(SpriteTextureLoader.getSprite("ui/Icons/iconChildren"));
		this._cached_sprites_kingdom.Add(SpriteTextureLoader.getSprite("ui/Icons/iconKingdom"));
		this._cached_sprites_kingdom.Add(SpriteTextureLoader.getSprite("ui/Icons/iconKingdomList"));
		this._cached_sprites_kingdom.Add(SpriteTextureLoader.getSprite("ui/Icons/iconRebellion"));
		this._cached_sprites_kingdom.Add(SpriteTextureLoader.getSprite("ui/Icons/iconKings"));
		this._cached_sprites_city.Add(SpriteTextureLoader.getSprite("ui/Icons/iconCity"));
		this._cached_sprites_city.Add(SpriteTextureLoader.getSprite("ui/Icons/iconCityList"));
		this._cached_sprites_city.Add(SpriteTextureLoader.getSprite("ui/Icons/iconLeaders"));
		this._cached_sprites_clan.Add(SpriteTextureLoader.getSprite("ui/Icons/iconClan"));
		this._cached_sprites_clan.Add(SpriteTextureLoader.getSprite("ui/Icons/iconClanList"));
		this._cached_sprites_time_and_death.Add(SpriteTextureLoader.getSprite("ui/Icons/iconClock"));
		this._cached_sprites_time_and_death.Add(SpriteTextureLoader.getSprite("ui/Icons/iconDead"));
		this._cached_sprites_time_and_death.Add(SpriteTextureLoader.getSprite("ui/Icons/iconSkulls"));
		this._cached_sprites_time_and_death.Add(SpriteTextureLoader.getSprite("ui/Icons/iconKills"));
		this._cached_sprites_time_and_death.Add(SpriteTextureLoader.getSprite("ui/Icons/iconAge"));
		this._cached_sprites_time_and_death.Add(SpriteTextureLoader.getSprite("ui/Icons/iconRenown"));
		this._cached_sprites_general_topics.Add(SpriteTextureLoader.getSprite("ui/Icons/iconGodFinger"));
		this._cached_sprites_general_topics.Add(SpriteTextureLoader.getSprite("ui/Icons/iconBre"));
		this._cached_sprites_boats_water.Add(SpriteTextureLoader.getSprite("ui/Icons/iconBoat"));
		this._cached_sprites_boats_water.Add(SpriteTextureLoader.getSprite("ui/Icons/iconTileDeepOcean"));
		this._cached_sprites_boats_water.Add(SpriteTextureLoader.getSprite("ui/Icons/iconTileCloseOcean"));
	}

	// Token: 0x0400072C RID: 1836
	private List<Sprite> _cached_sprites_religion = new List<Sprite>();

	// Token: 0x0400072D RID: 1837
	private List<Sprite> _cached_sprites_culture = new List<Sprite>();

	// Token: 0x0400072E RID: 1838
	private List<Sprite> _cached_sprites_family = new List<Sprite>();

	// Token: 0x0400072F RID: 1839
	private List<Sprite> _cached_sprites_kingdom = new List<Sprite>();

	// Token: 0x04000730 RID: 1840
	private List<Sprite> _cached_sprites_city = new List<Sprite>();

	// Token: 0x04000731 RID: 1841
	private List<Sprite> _cached_sprites_clan = new List<Sprite>();

	// Token: 0x04000732 RID: 1842
	private List<Sprite> _cached_sprites_time_and_death = new List<Sprite>();

	// Token: 0x04000733 RID: 1843
	private List<Sprite> _cached_sprites_general_topics = new List<Sprite>();

	// Token: 0x04000734 RID: 1844
	private List<Sprite> _cached_sprites_boats_water = new List<Sprite>();

	// Token: 0x04000735 RID: 1845
	private List<Sprite> _cached_sprites_housed = new List<Sprite>();

	// Token: 0x04000736 RID: 1846
	private List<Sprite> _cached_sprites_homeless = new List<Sprite>();

	// Token: 0x04000737 RID: 1847
	private const int MAX_TOPIC_SPRITES = 10;
}
