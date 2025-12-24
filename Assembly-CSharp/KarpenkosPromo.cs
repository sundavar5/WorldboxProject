using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200046A RID: 1130
public class KarpenkosPromo : MonoBehaviour
{
	// Token: 0x0600269F RID: 9887 RVA: 0x0013A731 File Offset: 0x00138931
	private void Awake()
	{
		this.maxElements = this.sprites.Count;
	}

	// Token: 0x060026A0 RID: 9888 RVA: 0x0013A744 File Offset: 0x00138944
	private void OnEnable()
	{
		this.curImageIndex = 0;
		this.timerChange = this.intervalMainImage / 2f;
		this.setImage(this.image1, this.curImageIndex);
		this.curImageIndex++;
		this.setImage(this.image2, this.curImageIndex);
		this.imageCurrent = this.image1;
		this.imageTransition = this.image2;
		this.imageCurrent.GetComponent<CanvasGroup>().alpha = 1f;
		this.imageTransition.GetComponent<CanvasGroup>().alpha = 0f;
	}

	// Token: 0x060026A1 RID: 9889 RVA: 0x0013A7DE File Offset: 0x001389DE
	private void setImage(Image pImage, int pIndex)
	{
		pImage.sprite = this.sprites[pIndex];
	}

	// Token: 0x060026A2 RID: 9890 RVA: 0x0013A7F4 File Offset: 0x001389F4
	private void Update()
	{
		if (this.timerChange > 0f)
		{
			this.timerChange -= Time.deltaTime;
			return;
		}
		if (this.imageTransition.GetComponent<CanvasGroup>().alpha < 1f)
		{
			this.imageTransition.GetComponent<CanvasGroup>().alpha += Time.deltaTime * 2f;
			if (this.imageTransition.GetComponent<CanvasGroup>().alpha >= 1f)
			{
				this.imageTransition.GetComponent<CanvasGroup>().alpha = 0f;
				this.imageCurrent.sprite = this.imageTransition.sprite;
				this.timerChange = this.intervalChange;
				if (this.curImageIndex == 0)
				{
					this.timerChange = this.intervalMainImage;
				}
				this.curImageIndex++;
				if (this.curImageIndex >= this.maxElements)
				{
					this.curImageIndex = 0;
				}
				this.setImage(this.imageTransition, this.curImageIndex);
			}
		}
	}

	// Token: 0x04001D10 RID: 7440
	public List<Sprite> sprites = new List<Sprite>();

	// Token: 0x04001D11 RID: 7441
	public Image image1;

	// Token: 0x04001D12 RID: 7442
	public Image image2;

	// Token: 0x04001D13 RID: 7443
	private float intervalChange = 1f;

	// Token: 0x04001D14 RID: 7444
	private float intervalMainImage = 1.5f;

	// Token: 0x04001D15 RID: 7445
	private int maxElements;

	// Token: 0x04001D16 RID: 7446
	private int curImageIndex;

	// Token: 0x04001D17 RID: 7447
	private float timerChange;

	// Token: 0x04001D18 RID: 7448
	private Image imageTransition;

	// Token: 0x04001D19 RID: 7449
	private Image imageCurrent;
}
