using System;
using System.Collections.Generic;

// Token: 0x0200019C RID: 412
public class LanguageTraitLibrary : BaseTraitLibrary<LanguageTrait>
{
	// Token: 0x06000C17 RID: 3095 RVA: 0x000AE2F2 File Offset: 0x000AC4F2
	protected override List<string> getDefaultTraitsForMeta(ActorAsset pAsset)
	{
		return pAsset.default_language_traits;
	}

	// Token: 0x06000C18 RID: 3096 RVA: 0x000AE2FC File Offset: 0x000AC4FC
	public override void init()
	{
		base.init();
		this.add(new LanguageTrait
		{
			id = "melodic",
			value = 3f,
			group_id = "harmony"
		});
		this.add(new LanguageTrait
		{
			id = "stylish_writing",
			value = 3f,
			group_id = "harmony"
		});
		this.add(new LanguageTrait
		{
			id = "scribble",
			group_id = "knowledge"
		});
		this.t.addOpposite("nicely_structured_grammar");
		this.add(new LanguageTrait
		{
			id = "nicely_structured_grammar",
			value = 2f,
			group_id = "knowledge"
		});
		this.t.addOpposite("scribble");
		this.add(new LanguageTrait
		{
			id = "beautiful_calligraphy",
			value = 2f,
			group_id = "harmony"
		});
		this.add(new LanguageTrait
		{
			id = "magic_words",
			group_id = "spirit"
		});
		this.t.setUnlockedWithAchievement("achievementTraitExplorerLanguage");
		LanguageTrait languageTrait = new LanguageTrait();
		languageTrait.id = "words_of_madness";
		languageTrait.value = 0.1f;
		languageTrait.group_id = "chaos";
		languageTrait.spawn_random_trait_allowed = false;
		languageTrait.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (a.hasTrait("evil"))
			{
				return;
			}
			if (a.hasTrait("blessed"))
			{
				return;
			}
			if (Randy.randomChance(pTrait.value))
			{
				a.addTrait("madness", false);
			}
		};
		this.add(languageTrait);
		this.t.setUnlockedWithAchievement("achievementCursedWorld");
		LanguageTrait languageTrait2 = new LanguageTrait();
		languageTrait2.id = "cursed_font";
		languageTrait2.value = 0.2f;
		languageTrait2.group_id = "chaos";
		languageTrait2.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (a.hasTrait("evil"))
			{
				return;
			}
			if (Randy.randomChance(pTrait.value))
			{
				a.addStatusEffect("cursed", 0f, true);
			}
		};
		this.add(languageTrait2);
		this.t.setUnlockedWithAchievement("achievementTheCorruptedTrees");
		LanguageTrait languageTrait3 = new LanguageTrait();
		languageTrait3.id = "font_of_gods";
		languageTrait3.value = 0.2f;
		languageTrait3.group_id = "spirit";
		languageTrait3.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (Randy.randomChance(pTrait.value))
			{
				a.addStatusEffect("enchanted", 0f, true);
			}
		};
		this.add(languageTrait3);
		LanguageTrait languageTrait4 = new LanguageTrait();
		languageTrait4.id = "chilly_font";
		languageTrait4.value = 0.4f;
		languageTrait4.group_id = "chaos";
		languageTrait4.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (Randy.randomChance(pTrait.value))
			{
				a.addStatusEffect("frozen", 0f, true);
			}
		};
		this.add(languageTrait4);
		LanguageTrait languageTrait5 = new LanguageTrait();
		languageTrait5.id = "ancient_runes";
		languageTrait5.value = 0.4f;
		languageTrait5.group_id = "spirit";
		languageTrait5.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (Randy.randomChance(pTrait.value))
			{
				a.addStatusEffect("spell_boost", 0f, true);
			}
		};
		this.add(languageTrait5);
		this.t.setUnlockedWithAchievement("achievementPlotsExplorer");
		LanguageTrait languageTrait6 = new LanguageTrait();
		languageTrait6.id = "repeated_sentences";
		languageTrait6.value = 0.1f;
		languageTrait6.group_id = "miscellaneous";
		languageTrait6.spawn_random_trait_allowed = false;
		languageTrait6.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (!Randy.randomChance(pTrait.value))
			{
				return;
			}
			if (!a.hasCity())
			{
				return;
			}
			if (!a.asset.can_be_cloned)
			{
				return;
			}
			if (!a.city.hasFreeHouseSlots())
			{
				return;
			}
			if (a.city.hasReachedWorldLawLimit())
			{
				return;
			}
			if (a.hasSubspecies() && a.subspecies.hasReachedPopulationLimit())
			{
				return;
			}
			World.world.units.cloneUnit(a, null);
		};
		this.add(languageTrait6);
		this.t.setUnlockedWithAchievement("achievementCloneWars");
		LanguageTrait languageTrait7 = new LanguageTrait();
		languageTrait7.id = "spooky_language";
		languageTrait7.value = 0.2f;
		languageTrait7.group_id = "chaos";
		languageTrait7.spawn_random_trait_allowed = false;
		languageTrait7.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (Randy.randomChance(pTrait.value))
			{
				if (!a.hasCity())
				{
					return;
				}
				World.world.units.spawnNewUnit("ghost", a.current_tile.neighboursAll.GetRandom<WorldTile>(), false, false, 6f, null, false, false);
			}
		};
		this.add(languageTrait7);
		this.t.setUnlockedWithAchievement("achievementChildNamedToto");
		LanguageTrait languageTrait8 = new LanguageTrait();
		languageTrait8.id = "powerful_words";
		languageTrait8.value = 0.2f;
		languageTrait8.group_id = "spirit";
		languageTrait8.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (Randy.randomChance(pTrait.value))
			{
				a.addStatusEffect("powerup", 0f, true);
			}
		};
		this.add(languageTrait8);
		LanguageTrait languageTrait9 = new LanguageTrait();
		languageTrait9.id = "confusing_semantics";
		languageTrait9.value = 0.2f;
		languageTrait9.group_id = "chaos";
		languageTrait9.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (Randy.randomChance(pTrait.value))
			{
				a.makeStunned(5f);
				if (a.hasLanguage())
				{
					a.joinLanguage(null);
				}
			}
		};
		this.add(languageTrait9);
		LanguageTrait languageTrait10 = new LanguageTrait();
		languageTrait10.id = "raging_paragraphs";
		languageTrait10.value = 0.2f;
		languageTrait10.group_id = "chaos";
		languageTrait10.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (Randy.randomChance(pTrait.value))
			{
				a.addStatusEffect("rage", 0f, true);
			}
		};
		this.add(languageTrait10);
		LanguageTrait languageTrait11 = new LanguageTrait();
		languageTrait11.id = "mortal_tongue";
		languageTrait11.value = 0.2f;
		languageTrait11.group_id = "chaos";
		languageTrait11.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (a.hasTrait("evil"))
			{
				return;
			}
			if (a.hasTrait("blessed"))
			{
				return;
			}
			if (Randy.randomChance(pTrait.value))
			{
				a.getHitFullHealth(AttackType.Divine);
			}
		};
		this.add(languageTrait11);
		this.t.setUnlockedWithAchievement("achievementCursedWorld");
		LanguageTrait languageTrait12 = new LanguageTrait();
		languageTrait12.id = "scorching_words";
		languageTrait12.value = 0.5f;
		languageTrait12.group_id = "chaos";
		languageTrait12.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (Randy.randomChance(pTrait.value))
			{
				a.addStatusEffect("burning", 0f, true);
			}
		};
		this.add(languageTrait12);
		LanguageTrait languageTrait13 = new LanguageTrait();
		languageTrait13.id = "doomed_glyphs";
		languageTrait13.value = 0.1f;
		languageTrait13.group_id = "chaos";
		languageTrait13.spawn_random_trait_allowed = false;
		languageTrait13.read_book_trait_action = delegate(Actor a, LanguageTrait pTrait, Book _)
		{
			if (a.hasTrait("lucky"))
			{
				return;
			}
			if (!WorldLawLibrary.world_law_disasters_nature.isEnabled())
			{
				return;
			}
			if (Randy.randomChance(pTrait.value))
			{
				Meteorite.spawnMeteoriteDisaster(a.current_tile, a);
			}
		};
		this.add(languageTrait13);
		this.t.setUnlockedWithAchievement("achievementMultiplySpoken");
		this.add(new LanguageTrait
		{
			id = "enlightening_script",
			group_id = "knowledge"
		});
		this.t.base_stats["intelligence"] = 10f;
		this.add(new LanguageTrait
		{
			id = "foolish_glyphs",
			group_id = "knowledge"
		});
		this.t.base_stats["intelligence"] = -5f;
		this.add(new LanguageTrait
		{
			id = "eternal_text",
			group_id = "miscellaneous",
			spawn_random_trait_allowed = false
		});
		this.t.setUnlockedWithAchievement("achievementPie");
		this.t.base_stats["lifespan"] = 500f;
		this.add(new LanguageTrait
		{
			id = "elegant_words",
			group_id = "spirit"
		});
		this.t.base_stats["offspring"] = 4f;
		this.add(new LanguageTrait
		{
			id = "strict_spelling",
			group_id = "knowledge"
		});
		this.t.base_stats["warfare"] = 10f;
		this.add(new LanguageTrait
		{
			id = "divine_encryption",
			group_id = "special",
			can_be_given = false,
			can_be_removed = false,
			can_be_in_book = false,
			spawn_random_trait_allowed = false
		});
		this.add(new LanguageTrait
		{
			id = "grin_mark",
			group_id = "fate",
			spawn_random_trait_allowed = false,
			priority = -100
		});
		this.t.setTraitInfoToGrinMark();
		this.t.setUnlockedWithAchievement("achievementCreaturesExplorer");
	}

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000C19 RID: 3097 RVA: 0x000AEABA File Offset: 0x000ACCBA
	protected override string icon_path
	{
		get
		{
			return "ui/Icons/language_traits/";
		}
	}

	// Token: 0x06000C1A RID: 3098 RVA: 0x000AEAC1 File Offset: 0x000ACCC1
	public static float getValueFloat(string pID)
	{
		return AssetManager.language_traits.get(pID).value;
	}

	// Token: 0x06000C1B RID: 3099 RVA: 0x000AEAD3 File Offset: 0x000ACCD3
	public static int getValue(string pID)
	{
		return (int)AssetManager.language_traits.get(pID).value;
	}
}
