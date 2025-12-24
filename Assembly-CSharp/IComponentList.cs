using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007D4 RID: 2004
public interface IComponentList
{
	// Token: 0x06003F48 RID: 16200
	ListPool<NanoObject> getElements();

	// Token: 0x06003F49 RID: 16201
	void setShowAll();

	// Token: 0x06003F4A RID: 16202
	void setShowFavoritesOnly();

	// Token: 0x06003F4B RID: 16203
	void setShowDeadOnly();

	// Token: 0x06003F4C RID: 16204
	void setShowAliveOnly();

	// Token: 0x06003F4D RID: 16205
	void setDefault();

	// Token: 0x06003F4E RID: 16206
	void init(GameObject pNoItems, SortingTab pSortingTab, GameObject pListElementPrefab, Transform pListTransform, ScrollRect pScrollRect, Text pTitleCounter, Text pFavoritesCounter, Text pDeadCounter);
}
