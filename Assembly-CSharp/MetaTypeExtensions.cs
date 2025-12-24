using System;
using UnityEngine;

// Token: 0x0200005F RID: 95
public static class MetaTypeExtensions
{
	// Token: 0x0600036D RID: 877 RVA: 0x0001F688 File Offset: 0x0001D888
	public static MetaTypeAsset getAsset(this MetaType pType)
	{
		return AssetManager.meta_type_library.getAsset(pType);
	}

	// Token: 0x0600036E RID: 878 RVA: 0x0001F695 File Offset: 0x0001D895
	public static bool isNone(this MetaType pType)
	{
		return pType == MetaType.None;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0001F69B File Offset: 0x0001D89B
	public static int getZoneState(this MetaType pType)
	{
		return AssetManager.meta_type_library.getAsset(pType).getZoneOptionState();
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0001F6B0 File Offset: 0x0001D8B0
	public static string AsString(this MetaType pType)
	{
		switch (pType)
		{
		case MetaType.None:
			return "none";
		case MetaType.Subspecies:
			return "subspecies";
		case MetaType.Family:
			return "family";
		case MetaType.Language:
			return "language";
		case MetaType.Culture:
			return "culture";
		case MetaType.Religion:
			return "religion";
		case MetaType.Clan:
			return "clan";
		case MetaType.City:
			return "city";
		case MetaType.Kingdom:
			return "kingdom";
		case MetaType.Alliance:
			return "alliance";
		case MetaType.War:
			return "war";
		case MetaType.Plot:
			return "plot";
		case MetaType.Unit:
			return "unit";
		case MetaType.Building:
			return "building";
		case MetaType.Item:
			return "item";
		case MetaType.World:
			return "world";
		case MetaType.Special:
			return "special";
		case MetaType.Army:
			return "army";
		default:
			Debug.LogError("MetaTypeExtensions.AsString missing option for : " + pType.ToString());
			return pType.ToString().ToLower();
		}
	}
}
