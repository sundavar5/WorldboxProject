using System;

// Token: 0x02000113 RID: 275
public abstract class ItemAssetLibrary<T> : BaseLibraryWithUnlockables<T> where T : ItemAsset
{
	// Token: 0x06000883 RID: 2179 RVA: 0x00075784 File Offset: 0x00073984
	public override T add(T pAsset)
	{
		T tNewAsset = base.add(pAsset);
		if (tNewAsset.base_stats == null)
		{
			tNewAsset.base_stats = new BaseStats();
		}
		return tNewAsset;
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x000757B8 File Offset: 0x000739B8
	public override void editorDiagnosticLocales()
	{
		foreach (T t in this.list)
		{
			ItemAsset tAsset = t;
			if (tAsset.has_locales)
			{
				string tName = tAsset.getLocaleID();
				this.checkLocale(tAsset, tName);
				if (!tAsset.isMod())
				{
					string tDescription = tAsset.getDescriptionID();
					this.checkLocale(tAsset, tDescription);
					if (tAsset.material != "basic")
					{
						this.checkLocale(tAsset, tAsset.getMaterialID());
					}
					foreach (object obj in Enum.GetValues(typeof(Rarity)))
					{
						Rarity tRarity = (Rarity)obj;
						string tResult = tAsset.getLocaleRarity(tRarity);
						this.checkLocale(tAsset, tResult);
					}
				}
			}
		}
	}
}
