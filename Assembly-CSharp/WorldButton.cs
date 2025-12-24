using System;
using UnityEngine;

// Token: 0x02000616 RID: 1558
public class WorldButton : MonoBehaviour
{
	// Token: 0x0600332E RID: 13102 RVA: 0x0018213B File Offset: 0x0018033B
	private void Start()
	{
		this.initial_pos = base.transform.localPosition;
		if (this.mainButtonObject != null)
		{
			this.hide();
		}
	}

	// Token: 0x0600332F RID: 13103 RVA: 0x00182164 File Offset: 0x00180364
	public void onClickMain()
	{
		if (WorldButton.active_buttons != null && WorldButton.active_buttons != this)
		{
			WorldButton.active_buttons.hideChildren();
			WorldButton.active_buttons = null;
		}
		if (!this.lesser_buttons[0].gameObject.activeSelf)
		{
			WorldButton[] array = this.lesser_buttons;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].activate();
			}
			WorldButton.active_buttons = this;
			return;
		}
		this.hideChildren();
	}

	// Token: 0x06003330 RID: 13104 RVA: 0x001821DC File Offset: 0x001803DC
	public void hideChildren()
	{
		WorldButton[] array = this.lesser_buttons;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].hide();
		}
	}

	// Token: 0x06003331 RID: 13105 RVA: 0x00182206 File Offset: 0x00180406
	public void hide()
	{
		base.gameObject.SetActive(false);
		base.transform.localPosition = this.mainButtonObject.transform.position;
	}

	// Token: 0x06003332 RID: 13106 RVA: 0x0018222F File Offset: 0x0018042F
	public void activate()
	{
		base.gameObject.SetActive(true);
		base.transform.localPosition = this.initial_pos;
	}

	// Token: 0x040026C4 RID: 9924
	public static WorldButton active_buttons;

	// Token: 0x040026C5 RID: 9925
	public WorldButton mainButtonObject;

	// Token: 0x040026C6 RID: 9926
	public WorldButton[] lesser_buttons;

	// Token: 0x040026C7 RID: 9927
	private Vector3 initial_pos;
}
