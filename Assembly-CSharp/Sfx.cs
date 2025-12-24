using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000442 RID: 1090
public class Sfx : MonoBehaviour
{
	// Token: 0x060025CB RID: 9675 RVA: 0x001372AB File Offset: 0x001354AB
	[Obsolete("Sound system moved to MusicBox")]
	public static void timeout(string pName)
	{
		Debug.LogWarning("Don't call SFX.timeout");
	}

	// Token: 0x060025CC RID: 9676 RVA: 0x001372B7 File Offset: 0x001354B7
	[Obsolete("Check out MusicBox.playSound instead")]
	public static void play(string pName, bool pRestart = true, float pX = -1f, float pY = -1f)
	{
		Debug.LogWarning("Don't call SFX.play");
	}

	// Token: 0x060025CD RID: 9677 RVA: 0x001372C3 File Offset: 0x001354C3
	[Obsolete("Sound system moved to MusicBox")]
	public static void fadeOut(string pName)
	{
		bool boolVal = PlayerConfig.dict["sound"].boolVal;
	}

	// Token: 0x060025CE RID: 9678 RVA: 0x001372DA File Offset: 0x001354DA
	private void Start()
	{
	}

	// Token: 0x060025CF RID: 9679 RVA: 0x001372DC File Offset: 0x001354DC
	private void Update()
	{
	}

	// Token: 0x04001CB7 RID: 7351
	private static Dictionary<string, List<SoundController>> dict;

	// Token: 0x04001CB8 RID: 7352
	private static List<SoundController> listAll;
}
