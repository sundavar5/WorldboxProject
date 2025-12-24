using System;
using UnityEngine;

// Token: 0x020005C2 RID: 1474
[Serializable]
public class QueueItem
{
	// Token: 0x040024C9 RID: 9417
	public object timestamp;

	// Token: 0x040024CA RID: 9418
	public string salt = RequestHelper.salt;

	// Token: 0x040024CB RID: 9419
	public string version = Application.version;

	// Token: 0x040024CC RID: 9420
	public string identifier = Application.identifier;

	// Token: 0x040024CD RID: 9421
	public string language = LocalizedTextManager.instance.language;

	// Token: 0x040024CE RID: 9422
	public string platform = Application.platform.ToString();

	// Token: 0x040024CF RID: 9423
	public int progress;
}
