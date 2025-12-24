using System;
using System.Collections.Generic;

// Token: 0x0200059B RID: 1435
[Serializable]
public class UploadMapQueue : QueueItem
{
	// Token: 0x04002406 RID: 9222
	public string username;

	// Token: 0x04002407 RID: 9223
	public string userId;

	// Token: 0x04002408 RID: 9224
	public string reason;

	// Token: 0x04002409 RID: 9225
	public string error;

	// Token: 0x0400240A RID: 9226
	public string status;

	// Token: 0x0400240B RID: 9227
	public string mapName;

	// Token: 0x0400240C RID: 9228
	public string mapDescription;

	// Token: 0x0400240D RID: 9229
	public List<string> mapTags;

	// Token: 0x0400240E RID: 9230
	public string mapFileName;

	// Token: 0x0400240F RID: 9231
	public string mapPreviewName;

	// Token: 0x04002410 RID: 9232
	public string mapId;

	// Token: 0x04002411 RID: 9233
	public MapMetaData mapMeta;
}
