using System;
using System.Collections.Generic;

// Token: 0x02000133 RID: 307
[Serializable]
public class LocaleGroupAsset : Asset
{
	// Token: 0x0400095D RID: 2397
	public string[] libraries;

	// Token: 0x0400095E RID: 2398
	public List<string> contains = new List<string>();

	// Token: 0x0400095F RID: 2399
	public List<string> starts_with_priority = new List<string>();

	// Token: 0x04000960 RID: 2400
	public List<string> starts_with = new List<string>();

	// Token: 0x04000961 RID: 2401
	public List<string> matches = new List<string>();

	// Token: 0x04000962 RID: 2402
	public LocaleGroupChecker checker;

	// Token: 0x04000963 RID: 2403
	public Dictionary<string, string> locales = new Dictionary<string, string>();
}
