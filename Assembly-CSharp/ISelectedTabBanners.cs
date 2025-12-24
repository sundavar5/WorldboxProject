using System;

// Token: 0x02000749 RID: 1865
public interface ISelectedTabBanners<T> where T : NanoObject
{
	// Token: 0x06003B0C RID: 15116
	void update(T pNano);

	// Token: 0x06003B0D RID: 15117
	int countVisibleBanners();
}
