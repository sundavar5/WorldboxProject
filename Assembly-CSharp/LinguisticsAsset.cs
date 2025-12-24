using System;
using System.Collections.Generic;

// Token: 0x0200028A RID: 650
[Serializable]
public class LinguisticsAsset : Asset
{
	// Token: 0x060018FF RID: 6399 RVA: 0x000ECF79 File Offset: 0x000EB179
	public void addPattern(int pRate, params string[] pPattern)
	{
		this._pot_patterns.AddTimes(pRate, pPattern);
	}

	// Token: 0x06001900 RID: 6400 RVA: 0x000ECF88 File Offset: 0x000EB188
	public string getRandom()
	{
		return this.array.GetRandom<string>();
	}

	// Token: 0x06001901 RID: 6401 RVA: 0x000ECF95 File Offset: 0x000EB195
	public string[] getRandomPattern()
	{
		if (this._pot_patterns.Count == 0)
		{
			return null;
		}
		return this._pot_patterns.GetRandom<string[]>();
	}

	// Token: 0x06001902 RID: 6402 RVA: 0x000ECFB1 File Offset: 0x000EB1B1
	public string getLocaleID()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001903 RID: 6403 RVA: 0x000ECFB8 File Offset: 0x000EB1B8
	public string getDescriptionID()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06001904 RID: 6404 RVA: 0x000ECFBF File Offset: 0x000EB1BF
	public string getDescriptionID2()
	{
		throw new NotImplementedException();
	}

	// Token: 0x040013A2 RID: 5026
	public string simple_text;

	// Token: 0x040013A3 RID: 5027
	public WordType word_type = WordType.None;

	// Token: 0x040013A4 RID: 5028
	public bool symbols_around;

	// Token: 0x040013A5 RID: 5029
	public string symbols_around_left;

	// Token: 0x040013A6 RID: 5030
	public string symbols_around_right;

	// Token: 0x040013A7 RID: 5031
	public bool add_space;

	// Token: 0x040013A8 RID: 5032
	public bool next_uppercase;

	// Token: 0x040013A9 RID: 5033
	public string[] array;

	// Token: 0x040013AA RID: 5034
	public bool word_group;

	// Token: 0x040013AB RID: 5035
	private List<string[]> _pot_patterns = new List<string[]>();
}
