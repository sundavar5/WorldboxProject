using System;

// Token: 0x0200018C RID: 396
[Serializable]
public class BaseCategoryLibrary<T> : AssetLibrary<T> where T : BaseCategoryAsset
{
	// Token: 0x06000BBA RID: 3002 RVA: 0x000AAC54 File Offset: 0x000A8E54
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (T t in this.list)
		{
			BaseCategoryAsset tAsset = t;
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
	}
}
