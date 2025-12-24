using System;
using System.Collections.Generic;
using Newtonsoft.Json;

// Token: 0x020000A4 RID: 164
[Serializable]
public class BaseLibraryWithUnlockables<T> : AssetLibrary<T>, ILibraryWithUnlockables where T : BaseUnlockableAsset
{
	// Token: 0x17000019 RID: 25
	// (get) Token: 0x0600056B RID: 1387 RVA: 0x00053006 File Offset: 0x00051206
	[JsonIgnore]
	public IEnumerable<BaseUnlockableAsset> elements_list
	{
		get
		{
			return this.list;
		}
	}

	// Token: 0x0600056C RID: 1388 RVA: 0x00053010 File Offset: 0x00051210
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (T t in this.list)
		{
			BaseUnlockableAsset tAsset = t;
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
	}
}
