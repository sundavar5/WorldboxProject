using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000842 RID: 2114
public class ToggleIcon : MonoBehaviour
{
	// Token: 0x0600423C RID: 16956 RVA: 0x001C099C File Offset: 0x001BEB9C
	private void Awake()
	{
		this.image = base.GetComponent<Image>();
	}

	// Token: 0x0600423D RID: 16957 RVA: 0x001C09AC File Offset: 0x001BEBAC
	internal void updateIcon(bool pEnabled)
	{
		if (this.image == null)
		{
			this.image = base.GetComponent<Image>();
		}
		if (pEnabled)
		{
			this.image.sprite = this.spriteON;
			return;
		}
		this.image.sprite = this.spriteOFF;
	}

	// Token: 0x0600423E RID: 16958 RVA: 0x001C09FC File Offset: 0x001BEBFC
	internal void updateIconMultiToggle(bool pActive, bool pEnabled)
	{
		if (this.image == null)
		{
			this.image = base.GetComponent<Image>();
		}
		if (pActive)
		{
			this.image.gameObject.SetActive(true);
		}
		else
		{
			this.image.gameObject.SetActive(false);
		}
		if (pActive)
		{
			this.image.color = Color.white;
		}
		else if (pEnabled)
		{
			this.image.color = Toolbox.color_grey;
		}
		else
		{
			this.image.color = Color.white;
		}
		if (pEnabled)
		{
			this.image.sprite = this.spriteON;
			return;
		}
		this.image.sprite = this.spriteOFF;
	}

	// Token: 0x04003056 RID: 12374
	public Sprite spriteON;

	// Token: 0x04003057 RID: 12375
	public Sprite spriteOFF;

	// Token: 0x04003058 RID: 12376
	private Image image;
}
