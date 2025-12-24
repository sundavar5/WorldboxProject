using System;
using UnityEngine;

// Token: 0x02000477 RID: 1143
public class PlatformRemover : MonoBehaviour
{
	// Token: 0x06002742 RID: 10050 RVA: 0x0013E564 File Offset: 0x0013C764
	private void Awake()
	{
		if (this.removeOnGlobalVersion)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (this.removeOnEditor && Config.isEditor)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (this.removeOnPC && Config.isComputer)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (this.removeOnAndroid && Config.isAndroid)
		{
			Object.Destroy(base.gameObject);
			return;
		}
		if (this.removeOnIOS && Config.isIos)
		{
			Object.Destroy(base.gameObject);
			return;
		}
	}

	// Token: 0x04001D7B RID: 7547
	public bool removeOnIOS;

	// Token: 0x04001D7C RID: 7548
	public bool removeOnAndroid;

	// Token: 0x04001D7D RID: 7549
	public bool removeOnPC;

	// Token: 0x04001D7E RID: 7550
	public bool removeOnEditor;

	// Token: 0x04001D7F RID: 7551
	public bool removeOnGlobalVersion;

	// Token: 0x04001D80 RID: 7552
	public bool removeOnChineseVersion;

	// Token: 0x04001D81 RID: 7553
	public bool removeOnNonPremiumVersion;
}
