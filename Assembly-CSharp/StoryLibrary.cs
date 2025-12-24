using System;
using UnityEngine;

// Token: 0x0200028F RID: 655
public class StoryLibrary : AssetLibrary<StoryAsset>
{
	// Token: 0x06001914 RID: 6420 RVA: 0x000EDDF0 File Offset: 0x000EBFF0
	public override void init()
	{
		this.add(new StoryAsset
		{
			id = "story_1"
		});
		this.t.addTemplate(new string[]
		{
			"pron_obj",
			"word_concept",
			"comma",
			"word_concept",
			"word_action",
			"word_concept",
			"word_creature",
			"period",
			"pron_obj",
			"word_concept",
			"pron_poss_adj",
			"word_place",
			"question_mark",
			"pron_obj",
			"word_concept",
			"pron_poss_adj",
			"word_place",
			"exclamation_mark"
		});
	}

	// Token: 0x06001915 RID: 6421 RVA: 0x000EDEBF File Offset: 0x000EC0BF
	public override void editorDiagnostic()
	{
		base.editorDiagnostic();
		if (Config.editor_maxim)
		{
			this.test();
		}
	}

	// Token: 0x06001916 RID: 6422 RVA: 0x000EDED4 File Offset: 0x000EC0D4
	private void test()
	{
		string[] tTemplate = this.get("story_1").getRandomTemplate();
		for (int i = 0; i < 10; i++)
		{
			Language language = new Language();
			LanguageStructure tStructure = new LanguageStructure();
			language.data = new LanguageData();
			language.data.structure = tStructure;
			string t = StoryLibrary.generateExample(language, tTemplate);
			string t2 = StoryLibrary.generateExample(language, tTemplate);
			string t3 = StoryLibrary.generateExample(language, tTemplate);
			string tSyllables = string.Concat(new string[]
			{
				"S:",
				tStructure.syllables_start.AsString<string>(),
				", |M:",
				tStructure.syllables_mid.AsString<string>(),
				", |E:",
				tStructure.syllables_ends.AsString<string>()
			});
			Debug.Log("Example Language " + string.Format("{0} : ", i) + tSyllables);
			Debug.Log("Example " + 1.ToString() + ": " + t);
			Debug.Log("Example " + 2.ToString() + ": " + t2);
			Debug.Log("Example " + 3.ToString() + ": " + t3);
		}
	}

	// Token: 0x06001917 RID: 6423 RVA: 0x000EE010 File Offset: 0x000EC210
	public static string getTestText(Language pLanguage)
	{
		string[] tTemplate = AssetManager.story_library.get("story_1").getRandomTemplate();
		if (pLanguage.data.structure == null)
		{
			LanguageStructure tStructure = new LanguageStructure();
			pLanguage.data.structure = tStructure;
		}
		return StoryLibrary.generateExample(pLanguage, tTemplate);
	}

	// Token: 0x06001918 RID: 6424 RVA: 0x000EE058 File Offset: 0x000EC258
	private static string generateExample(Language pLanguage, string[] pTemplate)
	{
		LanguageStructure tStructure = pLanguage.data.structure;
		string result;
		using (ListPool<string> tList = new ListPool<string>())
		{
			LinguisticsAsset tLastAsset = null;
			for (int i = 0; i < pTemplate.Length; i++)
			{
				string tElementID = pTemplate[i];
				LinguisticsAsset tAssetLing = AssetManager.linguistics_library.getSimple(tElementID);
				if (tAssetLing != null)
				{
					if (tAssetLing.word_group)
					{
						if (i > 0 && tAssetLing.add_space)
						{
							tList.Add(" ");
						}
						string[] tArticleArray = null;
						int tWordType = (int)tAssetLing.word_type;
						if (tAssetLing.word_type != WordType.None && tStructure.settings_articles.enabled[tWordType])
						{
							tArticleArray = tStructure.settings_articles.sets[tWordType];
						}
						if (tArticleArray != null)
						{
							tList.Add(tArticleArray.GetRandom<string>());
							tList.Add(tStructure.settings_articles.separator[tWordType]);
						}
						string tWordID = tAssetLing.array.GetRandom<string>();
						string tWord = AssetManager.words_library.getSimple(tWordID).getWordInLanguage(tStructure, tAssetLing, 0);
						if (tLastAsset != null && tLastAsset.next_uppercase)
						{
							tWord = tWord.FirstToUpper();
						}
						tList.Add(tWord);
					}
					else if (!string.IsNullOrEmpty(tAssetLing.simple_text))
					{
						tList.Add(tAssetLing.simple_text);
					}
					else if (tAssetLing.symbols_around)
					{
						tList.Insert(tList.Count - 1, tAssetLing.symbols_around_left);
						tList.Insert(tList.Count, tAssetLing.symbols_around_right);
					}
					tLastAsset = tAssetLing;
				}
			}
			using (StringBuilderPool tSB = new StringBuilderPool())
			{
				foreach (string ptr in tList)
				{
					string tPart = ptr;
					tSB.Append(tPart);
				}
				result = tSB.ToString().FirstToUpper();
			}
		}
		return result;
	}
}
