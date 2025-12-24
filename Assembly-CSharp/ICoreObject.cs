using System;

// Token: 0x020001D1 RID: 465
public interface ICoreObject
{
	// Token: 0x17000070 RID: 112
	// (get) Token: 0x06000DB4 RID: 3508
	string name { get; }

	// Token: 0x06000DB5 RID: 3509
	long getID();

	// Token: 0x06000DB6 RID: 3510
	int getAge();

	// Token: 0x06000DB7 RID: 3511
	bool isAlive();

	// Token: 0x06000DB8 RID: 3512
	bool isFavorite();

	// Token: 0x06000DB9 RID: 3513
	void switchFavorite();
}
