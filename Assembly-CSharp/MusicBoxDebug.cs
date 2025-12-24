using System;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

// Token: 0x02000435 RID: 1077
public class MusicBoxDebug
{
	// Token: 0x06002585 RID: 9605 RVA: 0x001358AC File Offset: 0x00133AAC
	public void add(string pPath, float pX, float pY, EventInstance pInstance)
	{
		pX += Randy.randomFloat(-0.5f, 0.5f);
		pY += Randy.randomFloat(-0.5f, 0.5f);
		this.list.Add(new DebugMusicBoxData
		{
			timer = 3f,
			path = pPath,
			x = pX,
			y = pY,
			instance = pInstance
		});
	}

	// Token: 0x06002586 RID: 9606 RVA: 0x00135918 File Offset: 0x00133B18
	public void update()
	{
		for (int i = this.list.Count - 1; i >= 0; i--)
		{
			DebugMusicBoxData debugMusicBoxData = this.list[i];
			debugMusicBoxData.timer -= Time.deltaTime;
			if (debugMusicBoxData.timer <= 0f)
			{
				this.list.RemoveAt(i);
			}
		}
	}

	// Token: 0x06002587 RID: 9607 RVA: 0x00135973 File Offset: 0x00133B73
	public void clear()
	{
		this.list.Clear();
	}

	// Token: 0x04001C84 RID: 7300
	internal List<DebugMusicBoxData> list = new List<DebugMusicBoxData>();
}
