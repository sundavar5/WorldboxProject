using System;

// Token: 0x020001B7 RID: 439
public class HistoryGroupLibrary : AssetLibrary<HistoryGroupAsset>
{
	// Token: 0x06000CC0 RID: 3264 RVA: 0x000B9118 File Offset: 0x000B7318
	public override void init()
	{
		base.init();
		this.add(new HistoryGroupAsset
		{
			id = "kings",
			icon_path = "ui/Icons/history_group/icon_kings"
		});
		this.add(new HistoryGroupAsset
		{
			id = "favorite_units",
			icon_path = "ui/Icons/iconFavoriteStar"
		});
		this.add(new HistoryGroupAsset
		{
			id = "cities",
			icon_path = "ui/Icons/iconCityList"
		});
		this.add(new HistoryGroupAsset
		{
			id = "kingdoms",
			icon_path = "ui/Icons/iconKingdomList"
		});
		this.add(new HistoryGroupAsset
		{
			id = "wars",
			icon_path = "ui/Icons/iconWarList"
		});
		this.add(new HistoryGroupAsset
		{
			id = "clans",
			icon_path = "ui/Icons/iconClanList"
		});
		this.add(new HistoryGroupAsset
		{
			id = "disasters",
			icon_path = "ui/Icons/worldrules/icon_disasters"
		});
	}

	// Token: 0x06000CC1 RID: 3265 RVA: 0x000B921C File Offset: 0x000B741C
	public override void post_init()
	{
		foreach (HistoryGroupAsset tAsset in this.list)
		{
			if (string.IsNullOrEmpty(tAsset.icon_path))
			{
				tAsset.icon_path = "ui/Icons/history_group/icon_" + tAsset.id;
			}
		}
	}

	// Token: 0x06000CC2 RID: 3266 RVA: 0x000B928C File Offset: 0x000B748C
	public override void editorDiagnosticLocales()
	{
		base.editorDiagnosticLocales();
		foreach (HistoryGroupAsset tAsset in this.list)
		{
			this.checkLocale(tAsset, tAsset.getLocaleID());
		}
	}
}
