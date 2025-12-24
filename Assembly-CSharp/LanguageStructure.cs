using System;
using System.Text.RegularExpressions;

// Token: 0x02000288 RID: 648
[Serializable]
public class LanguageStructure
{
	// Token: 0x060018F7 RID: 6391 RVA: 0x000EC9B3 File Offset: 0x000EABB3
	public LanguageStructure()
	{
		this.generateSyllableSets();
	}

	// Token: 0x060018F8 RID: 6392 RVA: 0x000EC9C4 File Offset: 0x000EABC4
	public void generateSyllableSets()
	{
		if (this.syllables_start != null)
		{
			return;
		}
		this.generateMainParts();
		this.generatePatterns();
		int tArticleMinSize = Randy.randomInt(1, 2);
		int tArticleMaxSize = Randy.randomInt(1, 3);
		this.settings_articles = new ArticleSettings();
		this.settings_articles.create(this, tArticleMinSize, tArticleMaxSize);
		this.settings_prefixes = new PrefixesSettings();
		this.settings_prefixes.create(this, 0, 4);
		this.settings_suffixes = new SuffixesSettings();
		this.settings_suffixes.create(this, 0, 4);
		this.syllables_start = this.generateSyllables("syllable_starts", Randy.randomInt(2, 10));
		this.syllables_mid = this.generateSyllables("syllable_mids", Randy.randomInt(2, 10));
		this.syllables_ends = this.generateSyllables("syllable_ends", Randy.randomInt(2, 10));
	}

	// Token: 0x060018F9 RID: 6393 RVA: 0x000ECA8C File Offset: 0x000EAC8C
	private void generatePatterns()
	{
		int tTotal = Randy.randomInt(3, 10);
		this.word_patterns = new string[tTotal];
		this.word_weights = new float[tTotal];
		for (int i = 0; i < tTotal; i++)
		{
			this.word_patterns[i] = LanguageStructureHelpers.possible_word_patterns.GetRandom<string>();
			this.word_weights[i] = Randy.randomFloat(0.05f, 1f);
		}
	}

	// Token: 0x060018FA RID: 6394 RVA: 0x000ECAF0 File Offset: 0x000EACF0
	private void generateMainParts()
	{
		this.sets_consonants = this.generateParts("consonant", 5);
		this.sets_vowels = this.generateParts("vowel", 5);
		this.sets_onset_1 = this.generateParts("onset1", 5);
		this.sets_onset_2 = this.generateParts("onset2", 5);
		this.sets_codas_1 = this.generateParts("coda1", 5);
		this.sets_codas_2 = this.generateParts("coda2", 5);
		this.sets_diphthongs = this.generateParts("diphthongs", 5);
	}

	// Token: 0x060018FB RID: 6395 RVA: 0x000ECB7C File Offset: 0x000EAD7C
	private string[] generateParts(string pID, int pAmount)
	{
		LinguisticsAsset tLinAsset = AssetManager.linguistics_library.get(pID);
		string[] tResultParts = new string[pAmount];
		for (int i = 0; i < pAmount; i++)
		{
			tResultParts[i] = tLinAsset.getRandom();
		}
		return tResultParts;
	}

	// Token: 0x060018FC RID: 6396 RVA: 0x000ECBB4 File Offset: 0x000EADB4
	private string[] generateSyllables(string pID, int pAmount)
	{
		string[] tResultSyllables = new string[pAmount];
		LinguisticsAsset tLinAsset = AssetManager.linguistics_library.get(pID);
		for (int iAmount = 0; iAmount < pAmount; iAmount++)
		{
			string[] tPattern = tLinAsset.getRandomPattern();
			string tPatternMerged = string.Join("", tPattern);
			using (new StringBuilderPool())
			{
				string tPartOnset = string.Empty;
				string tPartNucleus = string.Empty;
				string tPartCoda = string.Empty;
				if (tPatternMerged.StartsWith("CC"))
				{
					tPartOnset = this.sets_onset_2.GetRandom<string>();
				}
				else if (tPatternMerged.StartsWith("C"))
				{
					tPartOnset = this.sets_onset_1.GetRandom<string>();
				}
				tPartNucleus = ((tPatternMerged.Contains("VV") || Randy.randomChance(0.2f)) ? this.sets_diphthongs.GetRandom<string>() : this.sets_vowels.GetRandom<string>());
				if (tPatternMerged.EndsWith("CC"))
				{
					tPartCoda = this.sets_codas_2.GetRandom<string>();
				}
				else if (tPatternMerged.EndsWith("C"))
				{
					tPartCoda = this.sets_codas_1.GetRandom<string>();
				}
				string tResult = tPartOnset + tPartNucleus + tPartCoda;
				tResultSyllables[iAmount] = tResult;
			}
		}
		return tResultSyllables;
	}

	// Token: 0x060018FD RID: 6397 RVA: 0x000ECCF4 File Offset: 0x000EAEF4
	private string fixOrthography(string pSyllable)
	{
		if (string.IsNullOrEmpty(pSyllable))
		{
			return pSyllable;
		}
		string tResult = Regex.Replace(pSyllable, "([bcdfghjklmnpqrstvwxyz])\\1{2,}", "$1$1");
		tResult = tResult.Replace("ck", "ck");
		tResult = tResult.Replace("kk", "ck");
		tResult = tResult.Replace("cc", "ck");
		tResult = Regex.Replace(tResult, "qw|qv", "qu");
		tResult = tResult.Replace("q", "qu");
		tResult = Regex.Replace(tResult, "aa+", "a");
		tResult = Regex.Replace(tResult, "ii+", "i");
		tResult = Regex.Replace(tResult, "uu+", "u");
		if (tResult.StartsWith("x"))
		{
			tResult = "z" + tResult.Substring(1);
		}
		tResult = Regex.Replace(tResult, "([bcdfghjklmnpqrstvwxyz])\\1\\1+", "$1$1");
		tResult = tResult.Replace("tch", "ch");
		tResult = tResult.Replace("dge", "ge");
		if (tResult.Length > 2)
		{
			string start = tResult.Substring(0, 2).ToLower();
			foreach (string cluster in new string[]
			{
				"kg",
				"pn",
				"gn",
				"kn",
				"wr",
				"mn",
				"ps"
			})
			{
				if (start == cluster)
				{
					tResult = tResult.Substring(1);
					break;
				}
			}
		}
		return tResult;
	}

	// Token: 0x0400138F RID: 5007
	public string[] sets_vowels;

	// Token: 0x04001390 RID: 5008
	public string[] sets_consonants;

	// Token: 0x04001391 RID: 5009
	public string[] sets_onset_1;

	// Token: 0x04001392 RID: 5010
	public string[] sets_onset_2;

	// Token: 0x04001393 RID: 5011
	public string[] sets_codas_1;

	// Token: 0x04001394 RID: 5012
	public string[] sets_codas_2;

	// Token: 0x04001395 RID: 5013
	public string[] sets_diphthongs;

	// Token: 0x04001396 RID: 5014
	public string[] syllables_start;

	// Token: 0x04001397 RID: 5015
	public string[] syllables_mid;

	// Token: 0x04001398 RID: 5016
	public string[] syllables_ends;

	// Token: 0x04001399 RID: 5017
	public string[] word_patterns;

	// Token: 0x0400139A RID: 5018
	public float[] word_weights;

	// Token: 0x0400139B RID: 5019
	public ArticleSettings settings_articles;

	// Token: 0x0400139C RID: 5020
	public PrefixesSettings settings_prefixes;

	// Token: 0x0400139D RID: 5021
	public SuffixesSettings settings_suffixes;
}
