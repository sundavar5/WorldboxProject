using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityPools;

// Token: 0x0200013D RID: 317
public static class NameGeneratorTests
{
	// Token: 0x0600099A RID: 2458 RVA: 0x0008BAF9 File Offset: 0x00089CF9
	public static void runTests()
	{
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x0008BAFC File Offset: 0x00089CFC
	public static string testAllNamesForUniqueness()
	{
		string tRes = "";
		foreach (NameGeneratorAsset nameGenAsset in AssetManager.name_generator.list)
		{
			HashSet<string> tUniqueList = new HashSet<string>();
			for (int i = 0; i < 1000; i++)
			{
				string tInput = NameGenerator.generateNameFromTemplate(nameGenAsset, null, null, false, 0, null, null, false, null, ActorSex.None, false);
				if (!tUniqueList.Contains(tInput))
				{
					tUniqueList.Add(tInput);
				}
			}
			tRes = string.Concat(new string[]
			{
				tRes,
				"Unique names for asset ",
				nameGenAsset.id,
				": ",
				tUniqueList.Count.ToString(),
				"\n"
			});
		}
		return NameGeneratorTests.writeResults("name_test3_uniq", tRes);
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x0008BBF0 File Offset: 0x00089DF0
	public static string testAllNamesOutput()
	{
		string tRes = "";
		foreach (NameGeneratorAsset nameGenAsset in AssetManager.name_generator.list)
		{
			tRes = tRes + "\n--- asset name: " + nameGenAsset.id + " ---\n";
			tRes = tRes + NameGenerator.generateNamesFromTemplate(20, nameGenAsset, null, null, false, true) + "\n";
		}
		return NameGeneratorTests.writeResults("name_test3", tRes);
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0008BC80 File Offset: 0x00089E80
	public static string testNamesAlliances()
	{
		NameGeneratorTests.testNameStart();
		NameGeneratorTests.testName("alliance_name", 20);
		return NameGeneratorTests.testNameEnd();
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0008BC98 File Offset: 0x00089E98
	public static string testNamesWars()
	{
		NameGeneratorTests.testNameStart();
		NameGeneratorTests.testName("war_conquest", 20);
		NameGeneratorTests.testName("war_rebellion", 20);
		NameGeneratorTests.testName("war_spite", 20);
		NameGeneratorTests.testName("war_inspire", 20);
		NameGeneratorTests.testName("war_whisper", 20);
		return NameGeneratorTests.testNameEnd();
	}

	// Token: 0x0600099F RID: 2463 RVA: 0x0008BCEC File Offset: 0x00089EEC
	public static string testNamesItems()
	{
		NameGeneratorTests.testNameStart();
		NameGeneratorTests.testName("boots_name", 20);
		NameGeneratorTests.testName("armor_name", 20);
		NameGeneratorTests.testName("helmet_name", 20);
		NameGeneratorTests.testName("ring_name", 20);
		NameGeneratorTests.testName("amulet_name", 20);
		return NameGeneratorTests.testNameEnd();
	}

	// Token: 0x060009A0 RID: 2464 RVA: 0x0008BD40 File Offset: 0x00089F40
	public static string testNamesWeapons()
	{
		NameGeneratorTests.testNameStart();
		NameGeneratorTests.testName("sword_name", 20);
		NameGeneratorTests.testName("axe_name", 20);
		NameGeneratorTests.testName("hammer_name", 20);
		NameGeneratorTests.testName("stick_name", 20);
		NameGeneratorTests.testName("blaster_name", 20);
		NameGeneratorTests.testName("spear_name", 20);
		NameGeneratorTests.testName("bow_name", 20);
		NameGeneratorTests.testName("flame_sword_name", 20);
		NameGeneratorTests.testName("necromancer_staff_name", 20);
		NameGeneratorTests.testName("evil_staff_name", 20);
		NameGeneratorTests.testName("white_staff_name", 20);
		NameGeneratorTests.testName("plague_doctor_staff_name", 20);
		NameGeneratorTests.testName("druid_staff_name", 20);
		return NameGeneratorTests.testNameEnd();
	}

	// Token: 0x060009A1 RID: 2465 RVA: 0x0008BDF3 File Offset: 0x00089FF3
	public static void testNameStart()
	{
		NameGeneratorTests._test_string = "";
	}

	// Token: 0x060009A2 RID: 2466 RVA: 0x0008BDFF File Offset: 0x00089FFF
	public static string testNameEnd()
	{
		return NameGeneratorTests.writeResults("name_test2", NameGeneratorTests._test_string);
	}

	// Token: 0x060009A3 RID: 2467 RVA: 0x0008BE10 File Offset: 0x0008A010
	public static void testName(string pID, int pAmount = 20)
	{
		NameGeneratorTests._test_string = NameGeneratorTests._test_string + "\n--- " + pID + ":\n";
		NameGeneratorAsset tNameAsset = AssetManager.name_generator.get(pID);
		NameGeneratorTests._test_string = NameGeneratorTests._test_string + NameGenerator.generateNamesFromTemplate(100, tNameAsset, null, null, false, true) + "\n";
	}

	// Token: 0x060009A4 RID: 2468 RVA: 0x0008BE64 File Offset: 0x0008A064
	public static string testNamesBooks()
	{
		NameGeneratorTests.testNameStart();
		string result;
		using (ListPool<string> tBookNames = new ListPool<string>
		{
			"book_name_fable",
			"book_name_biology",
			"book_name_math",
			"book_name_diplomacy_manual",
			"book_name_love_story",
			"book_name_bad_story",
			"book_name_warfare_manual",
			"book_name_economy_manual",
			"book_name_stewardship_manual",
			"book_name_history"
		})
		{
			tBookNames.Shuffle<string>();
			foreach (string ptr in tBookNames)
			{
				NameGeneratorTests.testName(ptr, 20);
			}
			result = NameGeneratorTests.testNameEnd();
		}
		return result;
	}

	// Token: 0x060009A5 RID: 2469 RVA: 0x0008BF50 File Offset: 0x0008A150
	public static string testNamesDefault()
	{
		string tRes = "";
		tRes += "\n--- default - legacy:\n";
		for (int i = 0; i < 100; i++)
		{
			tRes = tRes + NameGenerator.getName("orc_unit", ActorSex.Male, true, null, null, false) + "\n";
		}
		tRes += "\n--- default_name - onomastics:\n";
		for (int j = 0; j < 100; j++)
		{
			tRes = tRes + NameGenerator.getName("orc_unit", ActorSex.Male, false, null, null, false) + "\n";
		}
		return NameGeneratorTests.writeResults("name_test_default", tRes);
	}

	// Token: 0x060009A6 RID: 2470 RVA: 0x0008BFE8 File Offset: 0x0008A1E8
	public static string testNamesClans()
	{
		string tRes = "";
		tRes += "\n--- human_clan name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("human_clan"), null, null, false, false) + "\n";
		tRes += "\n--- elf_clan name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("elf_clan"), null, null, false, false) + "\n";
		tRes += "\n--- dwarf_clan name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("dwarf_clan"), null, null, false, false) + "\n";
		tRes += "\n--- orc_clan name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("orc_clan"), null, null, false, false) + "\n";
		return NameGeneratorTests.writeResults("name_test2", tRes);
	}

	// Token: 0x060009A7 RID: 2471 RVA: 0x0008C0D0 File Offset: 0x0008A2D0
	public static string testNamesKingdoms()
	{
		string tRes = "";
		tRes += "\n--- human_kingdom name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("human_kingdom"), null, null, false, false) + "\n";
		tRes += "\n--- elf_kingdom name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("elf_kingdom"), null, null, false, false) + "\n";
		tRes += "\n--- dwarf_kingdom name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("dwarf_kingdom"), null, null, false, false) + "\n";
		tRes += "\n--- orc_kingdom name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("orc_kingdom"), null, null, false, false) + "\n";
		return NameGeneratorTests.writeResults("name_test2", tRes);
	}

	// Token: 0x060009A8 RID: 2472 RVA: 0x0008C1B8 File Offset: 0x0008A3B8
	public static string testNamesCities()
	{
		string tRes = "";
		tRes += "\n--- human_city name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("human_city"), null, null, false, false) + "\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("human_city"), null, null, true, false) + "\n";
		tRes += "\n--- elf_city name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("elf_city"), null, null, false, false) + "\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("elf_city"), null, null, true, false) + "\n";
		tRes += "\n--- dwarf_city name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("dwarf_city"), null, null, false, false) + "\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("dwarf_city"), null, null, true, false) + "\n";
		tRes += "\n--- orc_city name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("orc_city"), null, null, false, false) + "\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("orc_city"), null, null, true, false) + "\n";
		return NameGeneratorTests.writeResults("name_test2", tRes);
	}

	// Token: 0x060009A9 RID: 2473 RVA: 0x0008C338 File Offset: 0x0008A538
	public static string testNamesCulture()
	{
		string tRes = "";
		tRes += "\n--- elf_culture name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("elf_culture"), null, null, false, false) + "\n";
		tRes += "\n--- dwarf_culture name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("dwarf_culture"), null, null, false, false) + "\n";
		tRes += "\n--- orc_culture name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("orc_culture"), null, null, false, false) + "\n";
		tRes += "\n--- human_culture name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("human_culture"), null, null, false, false) + "\n";
		return NameGeneratorTests.writeResults("name_test2", tRes);
	}

	// Token: 0x060009AA RID: 2474 RVA: 0x0008C420 File Offset: 0x0008A620
	public static string testMottos()
	{
		string tRes = "";
		tRes += "\n--- Mottos:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(100, AssetManager.name_generator.get("clan_mottos"), null, null, false, false) + "\n";
		return NameGeneratorTests.writeResults("name_test_mottos", tRes);
	}

	// Token: 0x060009AB RID: 2475 RVA: 0x0008C470 File Offset: 0x0008A670
	public static string testNames()
	{
		string tRes = "";
		tRes += "\n--- elf name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("elf_unit"), null, null, false, false) + "\n";
		tRes += "\n--- elf City:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("elf_city"), null, null, false, false) + "\n";
		tRes += "\n--- elf Kingdom:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("elf_kingdom"), null, null, false, false) + "\n";
		tRes += "\n--- dwarf name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("dwarf_unit"), null, null, false, false) + "\n";
		tRes += "\n--- dwarf City:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("dwarf_city"), null, null, false, false) + "\n";
		tRes += "\n--- dwarf Kingdom:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("dwarf_kingdom"), null, null, false, false) + "\n";
		tRes += "\n--- orc name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("orc_unit"), null, null, false, false) + "\n";
		tRes += "\n--- orc City:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("orc_city"), null, null, false, false) + "\n";
		tRes += "\n--- orc Kingdom:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("orc_kingdom"), null, null, false, false) + "\n";
		tRes += "\n--- Human name:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("human_unit"), null, null, false, false) + "\n";
		tRes += "\n--- Human City:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("human_city"), null, null, false, false) + "\n";
		tRes += "\n--- Human Kingdom:\n";
		tRes = tRes + NameGenerator.generateNamesFromTemplate(20, AssetManager.name_generator.get("human_kingdom"), null, null, false, false) + "\n";
		return NameGeneratorTests.writeResults("name_test2", tRes);
	}

	// Token: 0x060009AC RID: 2476 RVA: 0x0008C6E8 File Offset: 0x0008A8E8
	public static string testShowOnomasticsVsLegacy()
	{
		string tRes = "";
		string tOno = "[<color=green>ONO</color>]";
		string tLeg = "[<color=orange>LEG</color>]";
		string tEmp = "[<color=red>---</color>]";
		string tDic = "[<color=yellow>DIC</color>]";
		foreach (NameGeneratorAsset tAsset in AssetManager.name_generator.list)
		{
			if ((string.IsNullOrEmpty("") || tAsset.id.Contains("")) && !"".Contains(tAsset.id))
			{
				string tItem = tEmp;
				string tItem2 = tEmp;
				string tItem3 = tEmp;
				string tCode = " ";
				string tCode2 = " ";
				if (tAsset.hasOnomastics())
				{
					tCode = "+";
					tItem2 = tOno;
				}
				if (tAsset.use_dictionary)
				{
					tItem = tDic;
				}
				List<string[]> templates = tAsset.templates;
				if (templates != null && templates.Count > 0)
				{
					tCode2 = "-";
					tItem3 = tLeg;
				}
				tRes = string.Concat(new string[]
				{
					tRes,
					tCode,
					tCode2,
					" ",
					tItem,
					" ",
					tItem2,
					" ",
					tItem3,
					" ",
					tAsset.id,
					"\n"
				});
				if (tAsset.hasOnomastics())
				{
					List<string[]> templates2 = tAsset.templates;
					if (templates2 != null && templates2.Count > 0)
					{
						tRes += NameGeneratorTests.compareOnomasticsVsLegacy(tAsset.id, 15000);
					}
				}
			}
		}
		return NameGeneratorTests.writeResults("name_test_ono", tRes);
	}

	// Token: 0x060009AD RID: 2477 RVA: 0x0008C8A0 File Offset: 0x0008AAA0
	public static string compareOnomasticsVsLegacy(string pNameAssetID, int pRuns)
	{
		string tRes = "";
		Randy.resetSeed(Randy.randomInt(1, 500));
		NameGeneratorAsset tAsset = AssetManager.name_generator.get(pNameAssetID);
		HashSet<string> tUniques = UnsafeCollectionPool<HashSet<string>, string>.Get();
		HashSet<string> tUniques2 = UnsafeCollectionPool<HashSet<string>, string>.Get();
		HashSet<string> tUniquesBoth = UnsafeCollectionPool<HashSet<string>, string>.Get();
		float tStart = Time.realtimeSinceStartup;
		for (int i = 0; i < pRuns; i++)
		{
			tUniques.Add(NameGenerator.generateNameFromTemplate(tAsset, null, null, true, 0, null, null, false, null, ActorSex.None, false).ToLowerInvariant());
		}
		float tEnd = Time.realtimeSinceStartup;
		float tStart2 = Time.realtimeSinceStartup;
		for (int j = 0; j < pRuns; j++)
		{
			tUniques2.Add(NameGenerator.generateNameFromTemplate(tAsset, null, null, false, 0, null, null, false, null, ActorSex.None, false).ToLowerInvariant());
		}
		float tEnd2 = Time.realtimeSinceStartup;
		int tUniqueCount = 0;
		int tUniqueCount2 = 0;
		int tUniqueCountBoth = 0;
		foreach (string tName in tUniques)
		{
			if (tUniques2.Contains(tName))
			{
				tUniqueCountBoth++;
				tUniquesBoth.Add(tName);
			}
			else
			{
				tUniqueCount++;
			}
		}
		foreach (string tName2 in tUniques2)
		{
			if (!tUniques.Contains(tName2))
			{
				tUniqueCount2++;
			}
		}
		Dictionary<int, int> tLengths = new Dictionary<int, int>();
		Dictionary<int, int> tLengths2 = new Dictionary<int, int>();
		int tMinLength = int.MaxValue;
		int tMaxLength = 0;
		foreach (string text in tUniques)
		{
			int tLen = text.Length;
			if (tLen < tMinLength)
			{
				tMinLength = tLen;
			}
			if (tLen > tMaxLength)
			{
				tMaxLength = tLen;
			}
		}
		int tMinLength2 = int.MaxValue;
		int tMaxLength2 = 0;
		foreach (string text2 in tUniques2)
		{
			int tLen2 = text2.Length;
			if (tLen2 < tMinLength2)
			{
				tMinLength2 = tLen2;
			}
			if (tLen2 > tMaxLength2)
			{
				tMaxLength2 = tLen2;
			}
		}
		int num = Mathf.Min(tMinLength, tMinLength2);
		int tMaxKeys = Mathf.Max(tMaxLength, tMaxLength2);
		for (int k = num; k <= tMaxKeys; k++)
		{
			tLengths[k] = 0;
			tLengths2[k] = 0;
		}
		foreach (string text3 in tUniques)
		{
			int tLen3 = text3.Length;
			Dictionary<int, int> dictionary = tLengths;
			int num2 = tLen3;
			int num3 = dictionary[num2];
			dictionary[num2] = num3 + 1;
		}
		foreach (string text4 in tUniques2)
		{
			int tLen4 = text4.Length;
			Dictionary<int, int> dictionary2 = tLengths2;
			int num3 = tLen4;
			int num2 = dictionary2[num3];
			dictionary2[num3] = num2 + 1;
		}
		float tLegacyOnlyPerc = 100f * (float)tUniqueCount / (float)tUniques.Count;
		float tOnoOnlyPerc = 100f * (float)tUniqueCount2 / (float)tUniques2.Count;
		float tBothPerc = 100f * (float)tUniqueCountBoth / (float)tUniques.Count;
		string tLegacyOnlyPercStr = (tLegacyOnlyPerc < 25f) ? ("<color=green>" + tLegacyOnlyPerc.ToString("F2") + "%</color>") : ((tLegacyOnlyPerc < 70f) ? ("<color=orange>" + tLegacyOnlyPerc.ToString("F2") + "%</color>") : ("<color=red>" + tLegacyOnlyPerc.ToString("F2") + "%</color>"));
		string tOnoOnlyPercStr = (tOnoOnlyPerc < 25f) ? ("<color=green>" + tOnoOnlyPerc.ToString("F2") + "%</color>") : ((tOnoOnlyPerc < 70f) ? ("<color=orange>" + tOnoOnlyPerc.ToString("F2") + "%</color>") : ("<color=red>" + tOnoOnlyPerc.ToString("F2") + "%</color>"));
		string tBothPercStr = (tBothPerc < 25f) ? ("<color=red>" + tBothPerc.ToString("F2") + "%</color>") : ((tBothPerc < 70f) ? ("<color=orange>" + tBothPerc.ToString("F2") + "%</color>") : ("<color=green>" + tBothPerc.ToString("F2") + "%</color>"));
		string result;
		using (ListPool<string[]> tRows = new ListPool<string[]>())
		{
			tRows.Add(new string[]
			{
				"Unique " + pNameAssetID + " :",
				pRuns.ToString() + " runs"
			});
			tRows.Add(new string[]
			{
				"Legacy :",
				tUniques.Count.ToString() ?? "",
				(100 * tUniques.Count / tUniques2.Count).ToString() + "%",
				(tEnd - tStart).ToString("F2") + "s"
			});
			tRows.Add(new string[]
			{
				"Ono :",
				tUniques2.Count.ToString() ?? "",
				(100 * tUniques2.Count / tUniques.Count).ToString() + "%",
				(tEnd2 - tStart2).ToString("F2") + "s"
			});
			tRows.Add(new string[]
			{
				"names only in legacy :",
				tUniqueCount.ToString() ?? "",
				tLegacyOnlyPercStr
			});
			tRows.Add(new string[]
			{
				"names only in ono :",
				tUniqueCount2.ToString() ?? "",
				tOnoOnlyPercStr
			});
			tRows.Add(new string[]
			{
				"names in both :",
				tUniqueCountBoth.ToString() ?? "",
				tBothPercStr
			});
			string tMinLengthStr = (tMinLength < tMinLength2) ? ("<color=red>" + tMinLength.ToString() + "</color>") : tMinLength.ToString();
			string tMinLength2Str = (tMinLength2 < tMinLength) ? ("<color=red>" + tMinLength2.ToString() + "</color>") : tMinLength2.ToString();
			string tMaxLengthStr = (tMaxLength > tMaxLength2) ? ("<color=red>" + tMaxLength.ToString() + "</color>") : tMaxLength.ToString();
			string tMaxLength2Str = (tMaxLength2 > tMaxLength) ? ("<color=red>" + tMaxLength2.ToString() + "</color>") : tMaxLength2.ToString();
			tRows.Add(new string[]
			{
				"min/max len legacy :",
				tMinLengthStr + "-" + tMaxLengthStr
			});
			tRows.Add(new string[]
			{
				"min/max len ono :",
				tMinLength2Str + "-" + tMaxLength2Str
			});
			tRes = tRes + "\n" + Toolbox.printRows(tRows, "right", false);
			tRows.Clear();
			string[] tKeysLens = (from p in tLengths
			select p.Key.ToString()).ToArray<string>();
			string[] tLegLens = (from p in tLengths
			select p.Value.ToString()).ToArray<string>();
			string[] tOnoLens = (from p in tLengths2
			select p.Value.ToString()).ToArray<string>();
			string[] tKeysComb = new string[]
			{
				"len dist"
			}.Concat(tKeysLens).ToArray<string>();
			string[] tLegComb = new string[]
			{
				"legacy :"
			}.Concat(tLegLens).ToArray<string>();
			string[] tOnoComb = new string[]
			{
				"ono :"
			}.Concat(tOnoLens).ToArray<string>();
			tRows.Add(tKeysComb);
			tRows.Add(tLegComb);
			tRows.Add(tOnoComb);
			tRes = tRes + "\n" + Toolbox.printRows(tRows, "right", false);
			HashSet<string> tOnlyU = UnsafeCollectionPool<HashSet<string>, string>.Get();
			tOnlyU.UnionWith(tUniques);
			tOnlyU.ExceptWith(tUniques2);
			using (ListPool<string> tU = new ListPool<string>(tOnlyU))
			{
				tU.Sort();
				using (ListPool<string> tPrint = new ListPool<string>(91))
				{
					using (ListPool<string> tPrint2 = new ListPool<string>(91))
					{
						using (ListPool<string> tPrintBoth = new ListPool<string>(91))
						{
							if (tU.Count > 0)
							{
								tPrint.Add("Legacy");
								ValueTuple<string, string> valueTuple = NameGeneratorTests.findShortestLongest(tU);
								string tShortest = valueTuple.Item1;
								string tLongest = valueTuple.Item2;
								for (int l = 0; l < Mathf.Min(tU.Count, 30); l++)
								{
									tPrint.Add(tU.Shift<string>());
								}
								for (int m = 0; m < Mathf.Min(tU.Count, 30); m++)
								{
									tPrint.Insert(Mathf.Min(31, tPrint.Count), tU.Pop<string>());
								}
								int tMiddleIndex = Mathf.Max(tU.Count / 2 - 15, 0);
								for (int n = 0; n < Mathf.Min(tU.Count, 30); n++)
								{
									tPrint.Insert(Mathf.Min(30 + n + 1, tPrint.Count), tU[n + tMiddleIndex]);
								}
								tPrint.Add(Toolbox.fillLeft("", tLongest.Length, '='));
								tPrint.Add("Min/Max");
								tPrint.Add(Toolbox.fillLeft("", tLongest.Length, '='));
								tPrint.Add(tLongest);
								tPrint.Add(tShortest);
							}
							HashSet<string> tOnlyU2 = UnsafeCollectionPool<HashSet<string>, string>.Get();
							tOnlyU2.UnionWith(tUniques2);
							tOnlyU2.ExceptWith(tUniques);
							using (ListPool<string> tU2 = new ListPool<string>(tOnlyU2))
							{
								tU2.Sort();
								if (tU2.Count > 0)
								{
									tPrint2.Add("Ono");
									ValueTuple<string, string> valueTuple2 = NameGeneratorTests.findShortestLongest(tU2);
									string tShortest2 = valueTuple2.Item1;
									string tLongest2 = valueTuple2.Item2;
									for (int i2 = 0; i2 < Mathf.Min(tU2.Count, 30); i2++)
									{
										tPrint2.Add(tU2.Shift<string>());
									}
									for (int i3 = 0; i3 < Mathf.Min(tU2.Count, 30); i3++)
									{
										tPrint2.Insert(Mathf.Min(31, tPrint2.Count), tU2.Pop<string>());
									}
									int tMiddleIndex2 = Mathf.Max(tU2.Count / 2 - 15, 0);
									for (int i4 = 0; i4 < Mathf.Min(tU2.Count, 30); i4++)
									{
										tPrint2.Insert(Mathf.Min(30 + i4 + 1, tPrint2.Count), tU2[i4 + tMiddleIndex2]);
									}
									tPrint2.Add(Toolbox.fillLeft("", tLongest2.Length, '='));
									tPrint2.Add("Min/Max");
									tPrint2.Add(Toolbox.fillLeft("", tLongest2.Length, '='));
									tPrint2.Add(tLongest2);
									tPrint2.Add(tShortest2);
								}
								using (ListPool<string> tBoth = new ListPool<string>(tUniquesBoth))
								{
									tBoth.Sort();
									if (tBoth.Count > 0)
									{
										tPrintBoth.Add("Both");
										ValueTuple<string, string> valueTuple3 = NameGeneratorTests.findShortestLongest(tBoth);
										string tShortest3 = valueTuple3.Item1;
										string tLongest3 = valueTuple3.Item2;
										for (int i5 = 0; i5 < Mathf.Min(tBoth.Count, 30); i5++)
										{
											tPrintBoth.Add(tBoth.Shift<string>());
										}
										for (int i6 = 0; i6 < Mathf.Min(tBoth.Count, 30); i6++)
										{
											tPrintBoth.Insert(Mathf.Min(31, tBoth.Count), tBoth.Pop<string>());
										}
										int tMiddleIndex3 = Mathf.Max(tBoth.Count / 2 - 15, 0);
										for (int i7 = 0; i7 < Mathf.Min(tBoth.Count, 30); i7++)
										{
											tPrintBoth.Insert(Mathf.Min(30 + i7 + 1, tBoth.Count), tBoth[i7 + tMiddleIndex3]);
										}
										tPrintBoth.Add(Toolbox.fillLeft("", tLongest3.Length, '='));
										tPrintBoth.Add("Min/Max");
										tPrintBoth.Add(Toolbox.fillLeft("", tLongest3.Length, '='));
										tPrintBoth.Add(tLongest3);
										tPrintBoth.Add(tShortest3);
									}
									tRes = tRes + "\n" + Toolbox.printColumns(new ListPool<string>[]
									{
										tPrint,
										tPrint2,
										tPrintBoth
									});
									UnsafeCollectionPool<HashSet<string>, string>.Release(tOnlyU);
									UnsafeCollectionPool<HashSet<string>, string>.Release(tOnlyU2);
									UnsafeCollectionPool<HashSet<string>, string>.Release(tUniques);
									UnsafeCollectionPool<HashSet<string>, string>.Release(tUniques2);
									UnsafeCollectionPool<HashSet<string>, string>.Release(tUniquesBoth);
									result = tRes;
								}
							}
						}
					}
				}
			}
		}
		return result;
	}

	// Token: 0x060009AE RID: 2478 RVA: 0x0008D6BC File Offset: 0x0008B8BC
	private static ValueTuple<string, string> findShortestLongest(ListPool<string> pHashSet)
	{
		string tLongest = null;
		string tShortest = null;
		int tMaxLength = int.MinValue;
		int tMinLength = int.MaxValue;
		foreach (string ptr in pHashSet)
		{
			string tString = ptr;
			int tLength = tString.Length;
			if (tLength > tMaxLength)
			{
				tMaxLength = tLength;
				tLongest = tString;
			}
			if (tLength < tMinLength)
			{
				tMinLength = tLength;
				tShortest = tString;
			}
		}
		return new ValueTuple<string, string>(tShortest, tLongest);
	}

	// Token: 0x060009AF RID: 2479 RVA: 0x0008D73C File Offset: 0x0008B93C
	public static string writeResults(string pFilename, string pResults)
	{
		File.WriteAllText(Application.persistentDataPath + "/" + pFilename, pResults);
		Debug.Log("Written result to " + pFilename + " in " + Application.persistentDataPath);
		return pResults;
	}

	// Token: 0x040009A2 RID: 2466
	private static string _test_string;
}
