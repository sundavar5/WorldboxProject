using System;
using System.Collections.Generic;

// Token: 0x0200028C RID: 652
[Serializable]
public class SentenceAsset : Asset
{
	// Token: 0x0600190C RID: 6412 RVA: 0x000EDC54 File Offset: 0x000EBE54
	public void addTemplate(params string[] pTemplates)
	{
		this._templates.Add(pTemplates);
	}

	// Token: 0x0600190D RID: 6413 RVA: 0x000EDC62 File Offset: 0x000EBE62
	public string[] getRandomTemplate()
	{
		if (this._templates.Count == 0)
		{
			return null;
		}
		return this._templates.GetRandom<string[]>();
	}

	// Token: 0x040013AE RID: 5038
	private List<string[]> _templates = new List<string[]>();
}
