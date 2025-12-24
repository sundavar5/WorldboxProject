using System;
using UnityEngine;

// Token: 0x02000539 RID: 1337
public class GraphyCaller : MonoBehaviour
{
	// Token: 0x06002BBB RID: 11195 RVA: 0x0015A064 File Offset: 0x00158264
	public void click()
	{
		this.clicked++;
		if (this.clicked > 10)
		{
			bool tIsOn = DebugConfig.isOn(DebugOption.DebugButton);
			DebugConfig.setOption(DebugOption.DebugButton, !tIsOn, true);
			DebugConfig.instance.debugButton.SetActive(!tIsOn);
		}
	}

	// Token: 0x06002BBC RID: 11196 RVA: 0x0015A0B6 File Offset: 0x001582B6
	public void clickConsole()
	{
		this.clicked++;
		if (this.clicked > 10)
		{
			World.world.console.Show();
		}
	}

	// Token: 0x06002BBD RID: 11197 RVA: 0x0015A0DF File Offset: 0x001582DF
	private void OnEnable()
	{
		this.clicked = 0;
	}

	// Token: 0x0400219C RID: 8604
	private int clicked;
}
