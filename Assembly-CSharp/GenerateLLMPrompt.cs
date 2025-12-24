using System;

// Token: 0x020006D9 RID: 1753
public static class GenerateLLMPrompt
{
	// Token: 0x06003849 RID: 14409 RVA: 0x001943E4 File Offset: 0x001925E4
	public static string getText(Actor pActor)
	{
		string result;
		using (StringBuilderPool tPool = new StringBuilderPool())
		{
			tPool.AppendLine("World Name: " + World.world.map_stats.name);
			tPool.AppendLine(string.Format("World Year: {0}", Date.getCurrentYear()));
			tPool.AppendLine("World Age: " + World.world.era_manager.getCurrentAge().id);
			tPool.AppendLine(string.Format("Deaths: {0}, Population: {1}, Mobs: {2}", World.world.map_stats.deaths, World.world.map_stats.population, World.world.map_stats.current_mobs));
			tPool.AppendLine("God Architector Name: " + World.world.map_stats.player_name);
			tPool.AppendLine();
			tPool.AppendLine(string.Format("World has subspecies: {0}, families: {1}, languages: {2}, religions: {3}, items: {4}, buildings: {5}, cultures: {6}, kingdoms: {7}, cities: {8}, clans: {9}, units: {10}", new object[]
			{
				World.world.subspecies.Count,
				World.world.families.Count,
				World.world.languages.Count,
				World.world.religions.Count,
				World.world.items.Count,
				World.world.buildings.Count,
				World.world.cultures.Count,
				World.world.kingdoms.Count,
				World.world.cities.Count,
				World.world.clans.Count,
				World.world.units.Count
			}));
			tPool.AppendLine(string.Format("World has islands: {0}", World.world.islands_calculator.countLandIslands()));
			tPool.AppendLine("Unit Name: " + pActor.name);
			tPool.AppendLine(string.Format("Age: {0}, Species: {1}, Sex: {2}, Level: {3}", new object[]
			{
				pActor.getAge(),
				pActor.asset.id,
				pActor.data.sex,
				pActor.data.level
			}));
			tPool.AppendLine(string.Format("Births: {0}, Kills: {1}, Generation: {2}", pActor.data.births, pActor.data.kills, pActor.data.generation));
			tPool.AppendLine("Actor Traits: " + pActor.getTraitsAsLocalizedString());
			if (pActor.hasSubspecies())
			{
				tPool.AppendLine(string.Format("Subspecies: {0}, Age: {1}", pActor.subspecies.name, pActor.subspecies.getAge()));
				tPool.AppendLine("Subspecies traits: " + pActor.subspecies.getTraitsAsLocalizedString());
			}
			if (pActor.hasKingdom() && pActor.isKingdomCiv())
			{
				tPool.AppendLine("Kingdom: " + pActor.kingdom.name);
				tPool.AppendLine(string.Format("Kingdom Age: {0}, Population: {1}, Children: {2}, Warriors: {3}", new object[]
				{
					pActor.kingdom.getAge(),
					pActor.kingdom.getPopulationPeople(),
					pActor.kingdom.countChildren(),
					pActor.kingdom.countTotalWarriors()
				}));
				if (pActor.kingdom.hasKing())
				{
					tPool.AppendLine(string.Format("King: {0}, Age: {1}", pActor.kingdom.king.name, pActor.kingdom.king.getAge()));
					tPool.AppendLine(string.Format("Births: {0}, Kills: {1}, Level: {2}", pActor.kingdom.king.data.births, pActor.kingdom.king.data.kills, pActor.kingdom.king.data.level));
				}
			}
			int tParentCount = 0;
			foreach (Actor tParent in pActor.getParents())
			{
				tPool.AppendLine(string.Format("Parent {0}: {1}, Age: {2}", ++tParentCount, tParent.name, tParent.getAge()));
			}
			if (pActor.hasCity())
			{
				tPool.AppendLine("City: " + pActor.city.name);
				tPool.AppendLine(string.Format("City Age: {0}, Population: {1}, Children: {2}, Warriors: {3}", new object[]
				{
					pActor.city.getAge(),
					pActor.city.getPopulationPeople(),
					pActor.city.countPopulationChildren(),
					pActor.city.countWarriors()
				}));
			}
			if (pActor.hasClan())
			{
				tPool.AppendLine(string.Format("Bloodline Clan is: {0}, Members: {1}, Age: {2} years", pActor.clan.name, pActor.clan.countUnits(), pActor.clan.getAge()));
				tPool.AppendLine("Clan traits: " + pActor.clan.getTraitsAsLocalizedString());
			}
			if (pActor.hasFamily())
			{
				tPool.AppendLine(string.Format("Family: {0}, Members: {1}, Age: {2} years", pActor.family.name, pActor.family.countUnits(), pActor.family.getAge()));
			}
			if (pActor.hasCulture())
			{
				tPool.AppendLine(string.Format("Culture: {0}, Followers: {1}, Age: {2} years", pActor.culture.name, pActor.culture.countUnits(), pActor.culture.getAge()));
				tPool.AppendLine("Culture traits: " + pActor.culture.getTraitsAsLocalizedString());
			}
			if (pActor.hasLanguage())
			{
				tPool.AppendLine(string.Format("Language: {0}, Users: {1}, Age: {2} years", pActor.language.name, pActor.language.countUnits(), pActor.language.getAge()));
				tPool.AppendLine("Language traits: " + pActor.language.getTraitsAsLocalizedString());
			}
			if (pActor.hasReligion())
			{
				tPool.AppendLine(string.Format("Religion: {0}, Followers: {1}, age {2} years", pActor.religion.name, pActor.religion.countUnits(), pActor.religion.getAge()));
				tPool.AppendLine("Religion traits: " + pActor.religion.getTraitsAsLocalizedString());
			}
			if (pActor.hasLover())
			{
				tPool.AppendLine(string.Format("Lover: {0}, {1}, level: {2}, Age: {3}, money: {4}, kills: {5}", new object[]
				{
					pActor.lover.name,
					pActor.data.sex,
					pActor.data.level,
					pActor.lover.getAge(),
					pActor.lover.data.money,
					pActor.lover.data.kills
				}));
			}
			if (pActor.hasBestFriend())
			{
				tPool.AppendLine(string.Format("Best Friend: {0}. Age of friendship: {1}", pActor.getBestFriend().name, pActor.getBestFriend().getAge()));
				if (pActor.getBestFriend().hasLover())
				{
					tPool.AppendLine(string.Format("Best Friend's Lover: {0}. Of age {1}", pActor.getBestFriend().lover.name, pActor.getBestFriend().lover.getAge()));
				}
			}
			if (pActor.hasWeapon())
			{
				tPool.AppendLine(string.Format("Weapon: {0}. Rarity: {1}. Age is {2} years", pActor.getWeapon().getName(true), pActor.getWeapon().getQuality(), pActor.getWeapon().getAge()));
			}
			tPool.AppendLine(string.Format("Happiness: {0}/{1}", pActor.getHappiness(), pActor.getMaxHappiness()));
			tPool.AppendLine(string.Format("Health: {0}/{1}", pActor.data.health, pActor.getMaxHealth()));
			tPool.AppendLine(string.Format("Stamina: {0}/{1}", pActor.data.stamina, pActor.getMaxStamina()));
			tPool.AppendLine(string.Format("Nutrition: {0}/{1}", pActor.data.nutrition, pActor.getMaxNutrition()));
			tPool.AppendLine(string.Format("Mana: {0}/{1}", pActor.data.mana, pActor.getMaxMana()));
			tPool.AppendLine(string.Format("Money: {0}", pActor.data.money));
			tPool.AppendLine();
			tPool.AppendLine("He lives in a fantasy simulated world. Write a story about his life, his thoughts, and his adventures.");
			tPool.AppendLine("Make it as though it were from a classic fantasy tale like Lord of the Rings or a D&D campaign.");
			tPool.AppendLine("Make it epic, dramatic, and full of lore. Infuse it with light and darkness, fun and sadness.");
			tPool.AppendLine("Give it character and heart, and make it unforgettable.");
			tPool.AppendLine("Reply should be in this language: " + LocalizedTextManager.instance.language);
			result = tPool.ToString();
		}
		return result;
	}
}
