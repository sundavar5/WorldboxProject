using System;

// Token: 0x02000782 RID: 1922
public readonly struct TooltipOpinionInfo
{
	// Token: 0x06003D54 RID: 15700 RVA: 0x001ADF7A File Offset: 0x001AC17A
	public TooltipOpinionInfo(string pTranslationKey, int pValue)
	{
		this.translation_key = pTranslationKey;
		this.value = pValue;
	}

	// Token: 0x04002C77 RID: 11383
	public readonly string translation_key;

	// Token: 0x04002C78 RID: 11384
	public readonly int value;
}
