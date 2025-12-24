using System;

// Token: 0x020004A8 RID: 1192
public interface IWorldBoxAd
{
	// Token: 0x06002916 RID: 10518
	void Reset();

	// Token: 0x06002917 RID: 10519
	void RequestAd();

	// Token: 0x06002918 RID: 10520
	void KillAd();

	// Token: 0x06002919 RID: 10521
	bool IsReady();

	// Token: 0x0600291A RID: 10522
	void ShowAd();

	// Token: 0x0600291B RID: 10523
	bool HasAd();

	// Token: 0x0600291C RID: 10524
	bool IsInitialized();

	// Token: 0x0600291D RID: 10525
	string GetProviderName();

	// Token: 0x0600291E RID: 10526
	string GetColor();

	// Token: 0x1700022E RID: 558
	// (get) Token: 0x0600291F RID: 10527
	// (set) Token: 0x06002920 RID: 10528
	Action adResetCallback { get; set; }

	// Token: 0x1700022F RID: 559
	// (get) Token: 0x06002921 RID: 10529
	// (set) Token: 0x06002922 RID: 10530
	Action adFailedCallback { get; set; }

	// Token: 0x17000230 RID: 560
	// (get) Token: 0x06002923 RID: 10531
	// (set) Token: 0x06002924 RID: 10532
	Action adFinishedCallback { get; set; }

	// Token: 0x17000231 RID: 561
	// (get) Token: 0x06002925 RID: 10533
	// (set) Token: 0x06002926 RID: 10534
	Action adStartedCallback { get; set; }

	// Token: 0x17000232 RID: 562
	// (get) Token: 0x06002927 RID: 10535
	// (set) Token: 0x06002928 RID: 10536
	Action<string> logger { get; set; }
}
