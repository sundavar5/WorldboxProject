using System;
using UnityEngine.UI;

// Token: 0x020007D5 RID: 2005
public interface ISapientListComponent
{
	// Token: 0x06003F4F RID: 16207
	void setSapientCounter(Text pCounter);

	// Token: 0x06003F50 RID: 16208
	void setNonSapientCounter(Text pCounter);

	// Token: 0x06003F51 RID: 16209
	void setShowSapientOnly();

	// Token: 0x06003F52 RID: 16210
	void setShowNonSapientOnly();

	// Token: 0x06003F53 RID: 16211
	void setDefault();
}
