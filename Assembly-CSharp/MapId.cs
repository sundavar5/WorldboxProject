using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007AF RID: 1967
public class MapId : MonoBehaviour
{
	// Token: 0x170003A3 RID: 931
	// (get) Token: 0x06003E5B RID: 15963 RVA: 0x001B2698 File Offset: 0x001B0898
	public static string formattedMapId
	{
		get
		{
			if (!string.IsNullOrEmpty(MapId.mapId) && MapId.mapId.Length == 12)
			{
				return string.Concat(new string[]
				{
					"WB-",
					MapId.mapId.Substring(0, 4),
					"-",
					MapId.mapId.Substring(4, 4),
					"-",
					MapId.mapId.Substring(8, 4)
				});
			}
			return MapId.mapId;
		}
	}

	// Token: 0x04002D67 RID: 11623
	public Button continueButton;

	// Token: 0x04002D68 RID: 11624
	public InputField mapIdText;

	// Token: 0x04002D69 RID: 11625
	public Text statusText;

	// Token: 0x04002D6A RID: 11626
	public static string mapId;

	// Token: 0x04002D6B RID: 11627
	public static Map map;

	// Token: 0x04002D6C RID: 11628
	public Sprite buttonOn;

	// Token: 0x04002D6D RID: 11629
	public Sprite buttonOff;
}
