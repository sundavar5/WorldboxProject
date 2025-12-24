using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005F1 RID: 1521
public class PauseButton : MonoBehaviour
{
	// Token: 0x060031B8 RID: 12728 RVA: 0x0017BBFB File Offset: 0x00179DFB
	private void Start()
	{
		this.icon = base.transform.Find("Icon").GetComponent<Image>();
	}

	// Token: 0x060031B9 RID: 12729 RVA: 0x0017BC18 File Offset: 0x00179E18
	private void Update()
	{
		this.updateSprite();
	}

	// Token: 0x060031BA RID: 12730 RVA: 0x0017BC20 File Offset: 0x00179E20
	internal void press()
	{
		Config.paused = !Config.paused;
		if (Config.paused)
		{
			WorldTip.instance.setText("game_paused", false);
			return;
		}
		WorldTip.instance.setText("game_unpaused", false);
	}

	// Token: 0x060031BB RID: 12731 RVA: 0x0017BC57 File Offset: 0x00179E57
	private void updateSprite()
	{
		if (Config.paused)
		{
			this.icon.sprite = this.play;
			return;
		}
		this.icon.sprite = this.pause;
	}

	// Token: 0x04002599 RID: 9625
	public Sprite pause;

	// Token: 0x0400259A RID: 9626
	public Sprite play;

	// Token: 0x0400259B RID: 9627
	private Image icon;
}
