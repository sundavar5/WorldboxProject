using System;
using System.Collections.Generic;

// Token: 0x0200012B RID: 299
[Serializable]
public class ListWindowAsset : Asset, IMultiLocalesAsset
{
	// Token: 0x06000919 RID: 2329 RVA: 0x00081E38 File Offset: 0x00080038
	public IEnumerable<string> getLocaleIDs()
	{
		yield return this.no_items_locale;
		if (!string.IsNullOrEmpty(this.no_dead_items_locale))
		{
			yield return this.no_dead_items_locale;
		}
		yield break;
	}

	// Token: 0x04000944 RID: 2372
	public string no_items_locale;

	// Token: 0x04000945 RID: 2373
	public string no_dead_items_locale;

	// Token: 0x04000946 RID: 2374
	public string art_path;

	// Token: 0x04000947 RID: 2375
	public string icon_path;

	// Token: 0x04000948 RID: 2376
	public MetaType meta_type;

	// Token: 0x04000949 RID: 2377
	public ListComponentSetter set_list_component;
}
