using System;
using System.Collections.Generic;
using UnityPools;

// Token: 0x0200040C RID: 1036
public class NameGenerator
{
	// Token: 0x060023D4 RID: 9172 RVA: 0x0012A912 File Offset: 0x00128B12
	public static void init()
	{
		if (NameGenerator._initiated)
		{
			return;
		}
		NameGenerator._initiated = true;
		Blacklist.init();
	}

	// Token: 0x060023D5 RID: 9173 RVA: 0x0012A928 File Offset: 0x00128B28
	public static string generateName(Actor pActor, MetaType pType, long pSeed, ActorSex pSex = ActorSex.None)
	{
		string tNameTemplate = null;
		int tSeedMeta = pType.GetHashCode();
		pSeed += (long)tSeedMeta;
		if (pActor.hasCulture())
		{
			OnomasticsData tOnomasticData = pActor.culture.getOnomasticData(pType, false);
			if (tOnomasticData != null)
			{
				return tOnomasticData.generateName(pSex, 0, new long?(pSeed));
			}
			tNameTemplate = pActor.culture.getNameTemplate(pType);
		}
		else
		{
			foreach (Actor tParent in pActor.getParents())
			{
				if (tParent.hasCulture())
				{
					OnomasticsData tOnomasticData2 = tParent.culture.getOnomasticData(pType, false);
					if (tOnomasticData2 != null)
					{
						return tOnomasticData2.generateName(pSex, 0, new long?(pSeed));
					}
					tNameTemplate = tParent.culture.getNameTemplate(pType);
					break;
				}
			}
		}
		if (string.IsNullOrEmpty(tNameTemplate))
		{
			tNameTemplate = pActor.asset.getNameTemplate(pType);
		}
		return NameGenerator.getName(tNameTemplate, pSex, false, null, new long?(pSeed), false);
	}

	// Token: 0x060023D6 RID: 9174 RVA: 0x0012AA24 File Offset: 0x00128C24
	public static string getName(string pAssetID, ActorSex pSex = ActorSex.Male, bool pForceLegacy = false, string pTemplate = null, long? pSeed = null, bool pIgnoreBlackList = false)
	{
		NameGenerator.init();
		NameGeneratorAsset tAsset = AssetManager.name_generator.get(pAssetID);
		NameGenerator._current_consonants = 0;
		NameGenerator._current_vowels = 0;
		string tName = NameGenerator.generateNameFromTemplate(tAsset, null, null, pForceLegacy, 0, pTemplate, null, false, pSeed, pSex, pIgnoreBlackList);
		if (!tAsset.hasOnomastics() && pSex == ActorSex.Female)
		{
			string lastLetter = tName.Substring(tName.Length - 1, 1);
			bool tFound = false;
			string[] vowels = tAsset.vowels;
			for (int i = 0; i < vowels.Length; i++)
			{
				if (vowels[i].CompareTo(lastLetter) == 0)
				{
					tFound = true;
					break;
				}
			}
			if (!tFound)
			{
				tName += Randy.getRandom<string>(tAsset.vowels);
			}
		}
		return tName;
	}

	// Token: 0x060023D7 RID: 9175 RVA: 0x0012AAC1 File Offset: 0x00128CC1
	private static string firstToUpper(string pString)
	{
		return pString.FirstToUpper();
	}

	// Token: 0x060023D8 RID: 9176 RVA: 0x0012AAC9 File Offset: 0x00128CC9
	private static string addVowel(string[] pList, bool pUppercase = false)
	{
		NameGenerator._current_consonants = 0;
		NameGenerator._current_vowels++;
		if (pUppercase)
		{
			return NameGenerator.firstToUpper(Randy.getRandom<string>(pList));
		}
		return Randy.getRandom<string>(pList);
	}

	// Token: 0x060023D9 RID: 9177 RVA: 0x0012AAF4 File Offset: 0x00128CF4
	private static string addEnding(NameGeneratorAsset pTemplate, string pName)
	{
		string tEnding = Randy.getRandom<string>(pTemplate.parts);
		if (NameGenerator.isConsonant(tEnding[0]) && NameGenerator.isConsonant(pName[pName.Length - 1]))
		{
			tEnding = NameGenerator.addVowel(pTemplate.vowels, false) + tEnding;
		}
		else if (!NameGenerator.isConsonant(tEnding[0]) && !NameGenerator.isConsonant(pName[pName.Length - 1]))
		{
			tEnding = NameGenerator.addConsonant(pTemplate.consonants, false) + tEnding;
		}
		return tEnding;
	}

	// Token: 0x060023DA RID: 9178 RVA: 0x0012AB7C File Offset: 0x00128D7C
	private static string addConsonant(string[] pList, bool pUppercase = false)
	{
		NameGenerator._current_consonants++;
		NameGenerator._current_vowels = 0;
		if (pUppercase)
		{
			return NameGenerator.firstToUpper(Randy.getRandom<string>(pList));
		}
		return Randy.getRandom<string>(pList);
	}

	// Token: 0x060023DB RID: 9179 RVA: 0x0012ABA8 File Offset: 0x00128DA8
	private static string addPart(string[] pArray, bool pUppercase = false)
	{
		string tPart = Randy.getRandom<string>(pArray);
		if (NameGenerator.isConsonant(tPart[tPart.Length - 1]))
		{
			NameGenerator._current_consonants++;
			NameGenerator._current_vowels = 0;
		}
		else
		{
			NameGenerator._current_consonants = 0;
			NameGenerator._current_vowels++;
		}
		if (pUppercase)
		{
			tPart = NameGenerator.firstToUpper(tPart);
		}
		return tPart;
	}

	// Token: 0x060023DC RID: 9180 RVA: 0x0012AC02 File Offset: 0x00128E02
	private static bool isConsonant(char pChar)
	{
		return NameGenerator.consonants_all.IndexOf(pChar) > -1;
	}

	// Token: 0x060023DD RID: 9181 RVA: 0x0012AC12 File Offset: 0x00128E12
	private static bool isVowel(char pChar)
	{
		return NameGenerator.vowels_all.IndexOf(pChar) > -1;
	}

	// Token: 0x060023DE RID: 9182 RVA: 0x0012AC24 File Offset: 0x00128E24
	public static string generateNameFromTemplate(string pAssetID, Actor pActor = null, Kingdom pKingdom = null, bool pForceLegacy = false)
	{
		return NameGenerator.generateNameFromTemplate(AssetManager.name_generator.get(pAssetID), pActor, pKingdom, pForceLegacy, 0, null, null, false, null, ActorSex.None, false);
	}

	// Token: 0x060023DF RID: 9183 RVA: 0x0012AC54 File Offset: 0x00128E54
	public static string generateNameFromOnomastics(NameGeneratorAsset pAsset, string pTemplate = null, Actor pActor = null, long? pSeed = null, ActorSex pSex = ActorSex.None)
	{
		OnomasticsData tOriginalData;
		if (!string.IsNullOrEmpty(pTemplate))
		{
			tOriginalData = OnomasticsCache.getOriginalData(pTemplate);
		}
		else
		{
			tOriginalData = OnomasticsCache.getOriginalData(pAsset.onomastics_templates.GetRandom<string>());
		}
		ActorSex tSex = pSex;
		if (pActor != null)
		{
			tSex = pActor.data.sex;
		}
		return tOriginalData.generateName(tSex, 0, pSeed);
	}

	// Token: 0x060023E0 RID: 9184 RVA: 0x0012ACA0 File Offset: 0x00128EA0
	public static string generateNamesFromTemplate(int pAmount, NameGeneratorAsset pAsset, Actor pActor = null, Kingdom pKingdom = null, bool pForceLegacy = false, bool pTestReplacer = false)
	{
		string tRes = "";
		HashSet<string> tUniques = new HashSet<string>();
		List<string> tNames = new List<string>();
		if (pAsset.hasOnomastics() && !pForceLegacy)
		{
			using (List<string>.Enumerator enumerator = pAsset.onomastics_templates.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string tTemplate = enumerator.Current;
					tUniques.Clear();
					tNames.Clear();
					for (int i = 0; i < 100; i++)
					{
						tUniques.Add(NameGenerator.generateNameFromTemplate(pAsset, pActor, pKingdom, false, 0, tTemplate, null, pTestReplacer, null, ActorSex.None, false));
					}
					tRes = tRes + "\n--- " + tTemplate;
					tRes = string.Concat(new string[]
					{
						tRes,
						"\n -- (",
						tUniques.Count.ToString(),
						" / ",
						100.ToString(),
						") \n"
					});
					tNames.AddRange(tUniques);
					tNames.Shuffle<string>();
					if (tNames.Count - pAmount > 0)
					{
						tNames.RemoveRange(pAmount, tNames.Count - pAmount);
					}
					tNames.Sort();
					foreach (string tName in tNames)
					{
						tRes = tRes + tName + "\n";
					}
				}
				return tRes;
			}
		}
		for (int j = 0; j < 100; j++)
		{
			tUniques.Add(NameGenerator.generateNameFromTemplate(pAsset, pActor, pKingdom, pForceLegacy, 0, null, null, pTestReplacer, null, ActorSex.None, false));
		}
		tRes += "\n--- Legacy";
		tRes = string.Concat(new string[]
		{
			tRes,
			"\n -- (",
			tUniques.Count.ToString(),
			" / ",
			100.ToString(),
			") \n"
		});
		tNames.AddRange(tUniques);
		tNames.Shuffle<string>();
		if (tNames.Count - pAmount > 0)
		{
			tNames.RemoveRange(pAmount, tNames.Count - pAmount);
		}
		tNames.Sort();
		foreach (string tName2 in tNames)
		{
			tRes = tRes + tName2 + "\n";
		}
		return tRes;
	}

	// Token: 0x060023E1 RID: 9185 RVA: 0x0012AF40 File Offset: 0x00129140
	public static string generateNameFromTemplate(NameGeneratorAsset pAsset, Actor pActor = null, Kingdom pKingdom = null, bool pForceLegacy = false, int pCalls = 0, string pOnomasticsTemplate = null, string[] pClassicTemplate = null, bool pTestReplacer = false, long? pSeed = null, ActorSex pSex = ActorSex.None, bool pIgnoreBlacklist = false)
	{
		if (pCalls > 50)
		{
			return string.Empty;
		}
		if (pAsset.hasOnomastics() && !pForceLegacy)
		{
			return NameGenerator.generateNameFromOnomastics(pAsset, pOnomasticsTemplate, pActor, pSeed, pSex);
		}
		NameGenerator._current_consonants = 0;
		NameGenerator._current_vowels = 0;
		string tName = "";
		string[] array = pClassicTemplate ?? pAsset.templates.GetRandom<string[]>();
		bool tAdditionAdded = false;
		foreach (string tStep in array)
		{
			string tMain;
			string tLastPart;
			if (tStep.Contains('#'))
			{
				string[] array3 = tStep.Split('#', StringSplitOptions.None);
				tMain = array3[0];
				tLastPart = array3[1];
			}
			else
			{
				tMain = tStep;
				tLastPart = "";
			}
			if (pAsset.use_dictionary)
			{
				if (tMain == "$comma$")
				{
					tName += ", ";
				}
				else
				{
					if (tMain.Contains(';'))
					{
						tMain = tMain.Split(';', StringSplitOptions.None).GetRandom<string>();
					}
					Dictionary<string, ListPool<string>> dict_splitted_items = NameGenerator._dict_splitted_items;
					if (dict_splitted_items == null || !dict_splitted_items.ContainsKey(tMain))
					{
						if (NameGenerator._dict_splitted_items == null)
						{
							NameGenerator._dict_splitted_items = UnsafeCollectionPool<Dictionary<string, ListPool<string>>, KeyValuePair<string, ListPool<string>>>.Get();
						}
						ListPool<string> tNewList = new ListPool<string>(pAsset.dict_parts[tMain].Split(',', StringSplitOptions.None));
						NameGenerator._dict_splitted_items.Add(tMain, tNewList);
					}
					NameGenerator._dict_splitted_items[tMain].ShuffleLast<string>();
					string tMottoPartId = NameGenerator._dict_splitted_items[tMain].Last<string>();
					if (NameGenerator._dict_splitted_items[tMain].Count > 1)
					{
						NameGenerator._dict_splitted_items[tMain].Pop<string>();
					}
					tName += tMottoPartId;
				}
			}
			else
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(tMain);
				if (num <= 2267396087U)
				{
					if (num <= 717708808U)
					{
						if (num <= 455613546U)
						{
							if (num != 34558099U)
							{
								if (num != 51335718U)
								{
									if (num != 455613546U)
									{
										goto IL_932;
									}
									if (!(tMain == "CONSONANT"))
									{
										goto IL_932;
									}
									tName += NameGenerator.addConsonant(pAsset.consonants, true);
									goto IL_932;
								}
								else
								{
									if (!(tMain == "special2"))
									{
										goto IL_932;
									}
									tName += pAsset.special2.GetRandom<string>();
									goto IL_932;
								}
							}
							else
							{
								if (!(tMain == "special1"))
								{
									goto IL_932;
								}
								tName += pAsset.special1.GetRandom<string>();
								goto IL_932;
							}
						}
						else if (num != 467038368U)
						{
							if (num != 621580159U)
							{
								if (num != 717708808U)
								{
									goto IL_932;
								}
								if (!(tMain == "Part_group"))
								{
									goto IL_932;
								}
								goto IL_806;
							}
							else if (!(tMain == " "))
							{
								goto IL_932;
							}
						}
						else
						{
							if (!(tMain == "number"))
							{
								goto IL_932;
							}
							tName += Randy.randomInt(0, 10).ToString();
							goto IL_932;
						}
					}
					else if (num <= 1417423114U)
					{
						if (num != 737585510U)
						{
							if (num != 894689925U)
							{
								if (num != 1417423114U)
								{
									goto IL_932;
								}
								if (!(tMain == "Letters"))
								{
									goto IL_932;
								}
								string[] tNumbers2 = tLastPart.Split('-', StringSplitOptions.None);
								tName += NameGenerator.addWord(pAsset, int.Parse(tNumbers2[0]), int.Parse(tNumbers2[1]), true);
								goto IL_932;
							}
							else if (!(tMain == "space"))
							{
								goto IL_932;
							}
						}
						else
						{
							if (!(tMain == "vowel"))
							{
								goto IL_932;
							}
							tName += NameGenerator.addVowel(pAsset.vowels, false);
							goto IL_932;
						}
					}
					else if (num != 1431138378U)
					{
						if (num != 2088252948U)
						{
							if (num != 2267396087U)
							{
								goto IL_932;
							}
							if (!(tMain == "removalchance"))
							{
								goto IL_932;
							}
							if (Randy.randomBool())
							{
								tName.Remove(tName.Length - 1);
								goto IL_932;
							}
							goto IL_932;
						}
						else
						{
							if (!(tMain == "part"))
							{
								goto IL_932;
							}
							tName += pAsset.parts.GetRandom<string>();
							goto IL_932;
						}
					}
					else
					{
						if (!(tMain == "consonant"))
						{
							goto IL_932;
						}
						tName += NameGenerator.addConsonant(pAsset.consonants, false);
						goto IL_932;
					}
					tName += " ";
					goto IL_932;
				}
				if (num <= 2524959326U)
				{
					if (num <= 2312965761U)
					{
						if (num != 2287153481U)
						{
							if (num != 2296188142U)
							{
								if (num != 2312965761U)
								{
									goto IL_932;
								}
								if (!(tMain == "part_group3"))
								{
									goto IL_932;
								}
								goto IL_7BC;
							}
							else if (!(tMain == "part_group2"))
							{
								goto IL_932;
							}
						}
						else
						{
							if (!(tMain == "addition_ending"))
							{
								goto IL_932;
							}
							if (!tAdditionAdded && Randy.randomChance(pAsset.add_addition_chance))
							{
								tName = tName + " " + pAsset.addition_ending.GetRandom<string>();
								tAdditionAdded = true;
								goto IL_932;
							}
							goto IL_932;
						}
					}
					else if (num != 2446939470U)
					{
						if (num != 2463717089U)
						{
							if (num != 2524959326U)
							{
								goto IL_932;
							}
							if (!(tMain == "vowelchance"))
							{
								goto IL_932;
							}
							if (Randy.randomBool())
							{
								tName += NameGenerator.addVowel(pAsset.vowels, false);
								goto IL_932;
							}
							goto IL_932;
						}
						else
						{
							if (!(tMain == "Part_group3"))
							{
								goto IL_932;
							}
							goto IL_8CF;
						}
					}
					else
					{
						if (!(tMain == "Part_group2"))
						{
							goto IL_932;
						}
						goto IL_86C;
					}
				}
				else if (num <= 3814285364U)
				{
					if (num != 3159231528U)
					{
						if (num != 3552634630U)
						{
							if (num != 3814285364U)
							{
								goto IL_932;
							}
							if (!(tMain == "Part"))
							{
								goto IL_932;
							}
							string tPart = pAsset.parts.GetRandom<string>();
							tPart = NameGenerator.firstToUpper(tPart);
							tName += tPart;
							goto IL_932;
						}
						else
						{
							if (!(tMain == "VOWEL"))
							{
								goto IL_932;
							}
							tName += NameGenerator.addVowel(pAsset.vowels, true);
							goto IL_932;
						}
					}
					else
					{
						if (!(tMain == "part_group"))
						{
							goto IL_932;
						}
						using (List<string>.Enumerator enumerator = pAsset.part_groups.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								string text = enumerator.Current;
								string[] tGroupParts = text.Split(',', StringSplitOptions.None);
								tName += tGroupParts.GetRandom<string>();
							}
							goto IL_932;
						}
					}
				}
				else if (num != 3890148115U)
				{
					if (num != 4018923290U)
					{
						if (num != 4153687146U)
						{
							goto IL_932;
						}
						if (!(tMain == "letters"))
						{
							goto IL_932;
						}
						string[] tNumbers3 = tLastPart.Split('-', StringSplitOptions.None);
						tName += NameGenerator.addWord(pAsset, int.Parse(tNumbers3[0]), int.Parse(tNumbers3[1]), false);
						goto IL_932;
					}
					else
					{
						if (!(tMain == "addition_start"))
						{
							goto IL_932;
						}
						if (!tAdditionAdded && Randy.randomChance(pAsset.add_addition_chance))
						{
							tName = tName + pAsset.addition_start.GetRandom<string>() + " ";
							tAdditionAdded = true;
							goto IL_932;
						}
						goto IL_932;
					}
				}
				else
				{
					if (!(tMain == "RANDOM_LETTER"))
					{
						goto IL_932;
					}
					if (Randy.randomBool())
					{
						tName += NameGenerator.addVowel(pAsset.vowels, true);
						goto IL_932;
					}
					tName += NameGenerator.addConsonant(pAsset.consonants, true);
					goto IL_932;
				}
				using (List<string>.Enumerator enumerator = pAsset.part_groups2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text2 = enumerator.Current;
						string[] tGroupParts2 = text2.Split(',', StringSplitOptions.None);
						tName += tGroupParts2.GetRandom<string>();
					}
					goto IL_932;
				}
				IL_7BC:
				using (List<string>.Enumerator enumerator = pAsset.part_groups3.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text3 = enumerator.Current;
						string[] tGroupParts3 = text3.Split(',', StringSplitOptions.None);
						tName += tGroupParts3.GetRandom<string>();
					}
					goto IL_932;
				}
				IL_806:
				bool tMakeUpper = true;
				using (List<string>.Enumerator enumerator = pAsset.part_groups.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text4 = enumerator.Current;
						string[] tGroupParts4 = text4.Split(',', StringSplitOptions.None);
						if (tMakeUpper)
						{
							tName += NameGenerator.firstToUpper(tGroupParts4.GetRandom<string>());
							tMakeUpper = false;
						}
						else
						{
							tName += tGroupParts4.GetRandom<string>();
						}
					}
					goto IL_932;
				}
				IL_86C:
				tMakeUpper = true;
				using (List<string>.Enumerator enumerator = pAsset.part_groups2.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text5 = enumerator.Current;
						string[] tGroupParts5 = text5.Split(',', StringSplitOptions.None);
						if (tMakeUpper)
						{
							tName += NameGenerator.firstToUpper(tGroupParts5.GetRandom<string>());
							tMakeUpper = false;
						}
						else
						{
							tName += tGroupParts5.GetRandom<string>();
						}
					}
					goto IL_932;
				}
				IL_8CF:
				tMakeUpper = true;
				foreach (string text6 in pAsset.part_groups3)
				{
					string[] tGroupParts6 = text6.Split(',', StringSplitOptions.None);
					if (tMakeUpper)
					{
						tName += NameGenerator.firstToUpper(tGroupParts6.GetRandom<string>());
						tMakeUpper = false;
					}
					else
					{
						tName += tGroupParts6.GetRandom<string>();
					}
				}
			}
			IL_932:;
		}
		if (tName.Contains('$'))
		{
			if (pTestReplacer)
			{
				NameGeneratorReplacers.replacer_debug(ref tName);
			}
			else
			{
				if (pAsset.replacer != null)
				{
					pAsset.replacer(ref tName, pActor);
				}
				if (pAsset.replacer_kingdom != null)
				{
					pAsset.replacer_kingdom(ref tName, pKingdom);
				}
			}
		}
		bool tRedo = false;
		if (string.IsNullOrEmpty(tName))
		{
			tRedo = true;
		}
		else if (!pAsset.use_dictionary && !pIgnoreBlacklist && Blacklist.checkBlackList(tName))
		{
			tRedo = true;
		}
		if (tRedo)
		{
			return NameGenerator.generateNameFromTemplate(pAsset, pActor, pKingdom, pForceLegacy, ++pCalls, pOnomasticsTemplate, pClassicTemplate, pTestReplacer, null, ActorSex.None, false);
		}
		tName = NameGenerator.firstToUpper(tName);
		if (pAsset.finalizer != null)
		{
			tName = pAsset.finalizer(tName);
		}
		if (NameGenerator._dict_splitted_items != null)
		{
			foreach (ListPool<string> listPool in NameGenerator._dict_splitted_items.Values)
			{
				listPool.Dispose();
			}
			NameGenerator._dict_splitted_items.Clear();
			UnsafeCollectionPool<Dictionary<string, ListPool<string>>, KeyValuePair<string, ListPool<string>>>.Release(NameGenerator._dict_splitted_items);
			NameGenerator._dict_splitted_items = null;
		}
		return tName;
	}

	// Token: 0x060023E2 RID: 9186 RVA: 0x0012B9F0 File Offset: 0x00129BF0
	private static string addWord(NameGeneratorAsset pAsset, int pMin, int pMax, bool pToUpperFirst = false)
	{
		string tName = "";
		int tWidth = Randy.randomInt(pMin, pMax);
		for (int i = 0; i < tWidth; i++)
		{
			if (NameGenerator._current_consonants >= pAsset.max_consonants_in_row)
			{
				tName += NameGenerator.addVowel(pAsset.vowels, pToUpperFirst);
				pToUpperFirst = false;
			}
			else if (NameGenerator._current_vowels >= pAsset.max_vowels_in_row)
			{
				tName += NameGenerator.addConsonant(pAsset.consonants, pToUpperFirst);
				pToUpperFirst = false;
			}
			else if (Randy.randomBool())
			{
				tName += NameGenerator.addVowel(pAsset.vowels, pToUpperFirst);
				pToUpperFirst = false;
			}
			else
			{
				tName += NameGenerator.addConsonant(pAsset.consonants, pToUpperFirst);
				pToUpperFirst = false;
			}
		}
		return tName;
	}

	// Token: 0x040019DA RID: 6618
	private static int _current_consonants = 0;

	// Token: 0x040019DB RID: 6619
	private static int _current_vowels = 0;

	// Token: 0x040019DC RID: 6620
	private static readonly char[] vowels_all = new char[]
	{
		'a',
		'e',
		'i',
		'o',
		'u',
		'y'
	};

	// Token: 0x040019DD RID: 6621
	private static readonly char[] consonants_all = new char[]
	{
		'b',
		'c',
		'd',
		'f',
		'g',
		'h',
		'j',
		'k',
		'l',
		'm',
		'n',
		'p',
		'q',
		'r',
		's',
		't',
		'v',
		'w',
		'x',
		'z'
	};

	// Token: 0x040019DE RID: 6622
	private static bool _initiated = false;

	// Token: 0x040019DF RID: 6623
	[ThreadStatic]
	private static Dictionary<string, ListPool<string>> _dict_splitted_items;
}
