using System;
using System.Collections.Generic;

// Token: 0x0200004E RID: 78
[Serializable]
public class LoyaltyAsset : Asset, IMultiLocalesAsset
{
	// Token: 0x0600030D RID: 781 RVA: 0x0001D160 File Offset: 0x0001B360
	public IEnumerable<string> getLocaleIDs()
	{
		yield return this.translation_key;
		if (!string.IsNullOrEmpty(this.translation_key_negative))
		{
			yield return this.translation_key_negative;
		}
		yield break;
	}

	// Token: 0x0600030E RID: 782 RVA: 0x0001D170 File Offset: 0x0001B370
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

	// Token: 0x040002AC RID: 684
	public string translation_key;

	// Token: 0x040002AD RID: 685
	public string translation_key_negative;

	// Token: 0x040002AE RID: 686
	public LoyaltyDelegateCalc calc;
}
