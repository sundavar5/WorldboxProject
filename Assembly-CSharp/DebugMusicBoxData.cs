using System;
using FMOD.Studio;

// Token: 0x02000434 RID: 1076
public class DebugMusicBoxData
{
	// Token: 0x06002583 RID: 9603 RVA: 0x00135878 File Offset: 0x00133A78
	public bool isPlaying()
	{
		PLAYBACK_STATE tState;
		this.instance.getPlaybackState(out tState);
		return tState == PLAYBACK_STATE.PLAYING;
	}

	// Token: 0x04001C7E RID: 7294
	public const float INTERVAL = 3f;

	// Token: 0x04001C7F RID: 7295
	public float timer = 3f;

	// Token: 0x04001C80 RID: 7296
	public string path;

	// Token: 0x04001C81 RID: 7297
	public float x;

	// Token: 0x04001C82 RID: 7298
	public float y;

	// Token: 0x04001C83 RID: 7299
	public EventInstance instance;
}
