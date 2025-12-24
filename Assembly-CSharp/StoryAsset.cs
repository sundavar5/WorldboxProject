using System;
using System.Collections.Generic;

// Token: 0x0200028E RID: 654
[Serializable]
public class StoryAsset : Asset
{
	// Token: 0x06001911 RID: 6417 RVA: 0x000EDDB0 File Offset: 0x000EBFB0
	public void addTemplate(params string[] pTemplates)
	{
		this._templates.Add(pTemplates);
	}

	// Token: 0x06001912 RID: 6418 RVA: 0x000EDDBE File Offset: 0x000EBFBE
	public string[] getRandomTemplate()
	{
		if (this._templates.Count == 0)
		{
			return null;
		}
		return this._templates.GetRandom<string[]>();
	}

	// Token: 0x040013AF RID: 5039
	private List<string[]> _templates = new List<string[]>();
}
