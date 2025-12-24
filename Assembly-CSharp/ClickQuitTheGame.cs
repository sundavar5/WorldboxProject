using System;
using db;
using UnityEngine;

// Token: 0x0200068C RID: 1676
public class ClickQuitTheGame : MonoBehaviour
{
	// Token: 0x060035BB RID: 13755 RVA: 0x0018988B File Offset: 0x00187A8B
	public void clickQuit()
	{
		DBManager.clearAndClose();
		Application.Quit();
	}
}
