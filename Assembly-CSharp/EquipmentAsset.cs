using System;
using UnityEngine;

// Token: 0x02000110 RID: 272
[Serializable]
public class EquipmentAsset : ItemAsset, IHandRenderer
{
	// Token: 0x0600086B RID: 2155 RVA: 0x0007547D File Offset: 0x0007367D
	public Sprite[] getSprites()
	{
		return this.gameplay_sprites;
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x0600086C RID: 2156 RVA: 0x00075485 File Offset: 0x00073685
	public bool is_colored
	{
		get
		{
			return this.colored;
		}
	}

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x0600086D RID: 2157 RVA: 0x0007548D File Offset: 0x0007368D
	public bool is_animated
	{
		get
		{
			return this.animated;
		}
	}
}
