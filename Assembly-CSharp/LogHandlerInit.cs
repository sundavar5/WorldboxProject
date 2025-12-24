using System;
using UnityEngine;

// Token: 0x0200053D RID: 1341
public class LogHandlerInit : MonoBehaviour
{
	// Token: 0x06002BE2 RID: 11234 RVA: 0x0015ACB0 File Offset: 0x00158EB0
	private void Awake()
	{
		try
		{
			LogHandler.init();
		}
		catch (Exception message)
		{
			Debug.LogError(message);
			throw;
		}
	}
}
