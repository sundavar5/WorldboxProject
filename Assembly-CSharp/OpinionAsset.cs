using System;
using System.Collections.Generic;

// Token: 0x02000066 RID: 102
[Serializable]
public class OpinionAsset : Asset, IMultiLocalesAsset
{
	// Token: 0x06000386 RID: 902 RVA: 0x00020206 File Offset: 0x0001E406
	public string getTranslationKey(int pValue)
	{
		if (pValue > 0)
		{
			return this.translation_key;
		}
		if (!string.IsNullOrEmpty(this.translation_key_negative))
		{
			return this.translation_key_negative;
		}
		return this.translation_key;
	}

	// Token: 0x06000387 RID: 903 RVA: 0x0002022D File Offset: 0x0001E42D
	public IEnumerable<string> getLocaleIDs()
	{
		yield return this.translation_key;
		if (!string.IsNullOrEmpty(this.translation_key_negative))
		{
			yield return this.translation_key_negative;
		}
		yield break;
	}

	// Token: 0x04000318 RID: 792
	public string translation_key;

	// Token: 0x04000319 RID: 793
	public string translation_key_negative;

	// Token: 0x0400031A RID: 794
	public OpinionDelegateCalc calc;
}
