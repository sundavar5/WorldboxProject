using System;
using db;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001F2 RID: 498
public static class WorldLogMessageExtensions
{
	// Token: 0x06000E98 RID: 3736 RVA: 0x000C2790 File Offset: 0x000C0990
	public static void clear(this WorldLogMessage pMessage)
	{
		pMessage.unit = null;
	}

	// Token: 0x06000E99 RID: 3737 RVA: 0x000C2799 File Offset: 0x000C0999
	public static void add(this WorldLogMessage pMessage)
	{
		HistoryHud.instance.newHistory(pMessage);
		DBInserter.insertLog(pMessage);
	}

	// Token: 0x06000E9A RID: 3738 RVA: 0x000C27AC File Offset: 0x000C09AC
	public static string getFormatedText(this WorldLogMessage pMessage, Text pTextField)
	{
		WorldLogAsset tAsset = pMessage.getAsset();
		string tText;
		if (tAsset.random_ids > 0)
		{
			int tRandomID = pMessage.timestamp % tAsset.random_ids + 1;
			tText = tAsset.getLocaleID(tRandomID);
		}
		else
		{
			tText = tAsset.getLocaleID();
		}
		string tTranslationText = LocalizedTextManager.getText(tText, null, false);
		if (tAsset.text_replacer != null)
		{
			tAsset.text_replacer(pMessage, ref tTranslationText);
		}
		pTextField.color = tAsset.color;
		return tTranslationText;
	}

	// Token: 0x06000E9B RID: 3739 RVA: 0x000C2816 File Offset: 0x000C0A16
	public static bool followLocation(this WorldLogMessage pMessage)
	{
		if (pMessage.hasFollowLocation())
		{
			WorldLog.locationFollow(pMessage.unit);
			return true;
		}
		return false;
	}

	// Token: 0x06000E9C RID: 3740 RVA: 0x000C2830 File Offset: 0x000C0A30
	public static void jumpToLocation(this WorldLogMessage pMessage)
	{
		if (pMessage.followLocation())
		{
			return;
		}
		Vector3 tLocation = pMessage.getLocation();
		if (tLocation != Vector3.zero && Toolbox.inMapBorder(ref tLocation))
		{
			WorldLog.locationJump(tLocation);
		}
	}

	// Token: 0x06000E9D RID: 3741 RVA: 0x000C2869 File Offset: 0x000C0A69
	public static bool hasLocation(this WorldLogMessage pMessage)
	{
		return pMessage.getLocation() != Vector3.zero;
	}

	// Token: 0x06000E9E RID: 3742 RVA: 0x000C287B File Offset: 0x000C0A7B
	public static bool hasFollowLocation(this WorldLogMessage pMessage)
	{
		return pMessage.unit != null && pMessage.unit.isAlive();
	}

	// Token: 0x06000E9F RID: 3743 RVA: 0x000C2898 File Offset: 0x000C0A98
	public static Vector3 getLocation(this WorldLogMessage pMessage)
	{
		if (pMessage.unit != null && pMessage.unit.isAlive())
		{
			return pMessage.unit.current_position;
		}
		int? num = pMessage.x;
		int num2 = -1;
		if (!(num.GetValueOrDefault() == num2 & num != null))
		{
			num = pMessage.y;
			num2 = -1;
			if (!(num.GetValueOrDefault() == num2 & num != null))
			{
				Vector2 tLocation = pMessage.location;
				if (Toolbox.inMapBorder(ref tLocation))
				{
					return pMessage.location;
				}
			}
		}
		return Vector3.zero;
	}

	// Token: 0x06000EA0 RID: 3744 RVA: 0x000C2926 File Offset: 0x000C0B26
	public static WorldLogAsset getAsset(this WorldLogMessage pMessage)
	{
		return AssetManager.world_log_library.get(pMessage.asset_id);
	}

	// Token: 0x06000EA1 RID: 3745 RVA: 0x000C2938 File Offset: 0x000C0B38
	public static string getSpecial(this WorldLogMessage pMessage, int pSpecialId)
	{
		switch (pSpecialId)
		{
		case 1:
			return Toolbox.coloredText(pMessage.special1, pMessage.color_special_1, false);
		case 2:
			return Toolbox.coloredText(pMessage.special2, pMessage.color_special_2, false);
		case 3:
			return Toolbox.coloredText(pMessage.special3, pMessage.color_special_3, false);
		default:
			return "";
		}
	}
}
