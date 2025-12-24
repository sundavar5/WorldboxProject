using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000778 RID: 1912
public class LegendaryBG : MonoBehaviour
{
	// Token: 0x06003C89 RID: 15497 RVA: 0x001A403B File Offset: 0x001A223B
	private void Awake()
	{
		this.img = base.GetComponent<Image>();
		this.max_frames = this.spriteArray.Length;
	}

	// Token: 0x06003C8A RID: 15498 RVA: 0x001A4057 File Offset: 0x001A2257
	private void OnEnable()
	{
		this.timer = 0.2f;
		this.currentFrame = this.max_frames - 1;
	}

	// Token: 0x06003C8B RID: 15499 RVA: 0x001A4074 File Offset: 0x001A2274
	private void Update()
	{
		if (this.timer > 0f)
		{
			this.timer -= Time.deltaTime;
			return;
		}
		this.timer = 0.07f;
		this.currentFrame++;
		if (this.currentFrame >= this.max_frames)
		{
			this.currentFrame = 0;
		}
		else if (this.currentFrame == this.max_frames - 1)
		{
			this.timer = 2.4f;
		}
		this.img.sprite = this.spriteArray[this.currentFrame];
	}

	// Token: 0x04002BF9 RID: 11257
	public Sprite[] spriteArray;

	// Token: 0x04002BFA RID: 11258
	private Image img;

	// Token: 0x04002BFB RID: 11259
	private int max_frames = 9;

	// Token: 0x04002BFC RID: 11260
	private int currentFrame;

	// Token: 0x04002BFD RID: 11261
	private float timer = 0.07f;
}
