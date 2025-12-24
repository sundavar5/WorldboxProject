using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000487 RID: 1159
public class SpriteAnimation : MonoBehaviour
{
	// Token: 0x060027C7 RID: 10183 RVA: 0x00141174 File Offset: 0x0013F374
	public void Awake()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		if (this.spriteRenderer == null)
		{
			this.image = base.GetComponent<Image>();
			this.useOnSpriteRenderer = false;
			return;
		}
		this.currentSpriteGraphic = this.spriteRenderer.sprite;
	}

	// Token: 0x060027C8 RID: 10184 RVA: 0x001411C0 File Offset: 0x0013F3C0
	public virtual void create()
	{
		this.spriteRenderer = base.GetComponent<SpriteRenderer>();
		this.nextFrameTime = this.timeBetweenFrames;
	}

	// Token: 0x060027C9 RID: 10185 RVA: 0x001411DA File Offset: 0x0013F3DA
	public void stopAnimations()
	{
		this.isOn = false;
	}

	// Token: 0x060027CA RID: 10186 RVA: 0x001411E3 File Offset: 0x0013F3E3
	internal void setFrames(Sprite[] pFrames)
	{
		if (this.frames == pFrames)
		{
			return;
		}
		this.frames = pFrames;
		if (this.currentFrameIndex >= this.frames.Length)
		{
			this.currentFrameIndex = 0;
		}
		this.updateFrame();
	}

	// Token: 0x060027CB RID: 10187 RVA: 0x00141213 File Offset: 0x0013F413
	public bool isLastFrame()
	{
		return this.currentFrameIndex >= this.frames.Length - 1;
	}

	// Token: 0x060027CC RID: 10188 RVA: 0x0014122A File Offset: 0x0013F42A
	public bool isFirstFrame()
	{
		return this.currentFrameIndex == 0;
	}

	// Token: 0x060027CD RID: 10189 RVA: 0x00141238 File Offset: 0x0013F438
	internal virtual void update(float pElapsed)
	{
		if (this.useNormalDeltaTime)
		{
			pElapsed = Time.deltaTime;
		}
		if (this.dirty)
		{
			this.dirty = false;
			this.forceUpdateFrame();
			return;
		}
		if (!this.isOn)
		{
			if (this.stopFrameTrigger)
			{
				this.stopFrameTrigger = false;
				this.updateFrame();
			}
			return;
		}
		if (World.world.isPaused() && !this.ignorePause)
		{
			return;
		}
		if (this.nextFrameTime > 0f)
		{
			this.nextFrameTime -= pElapsed;
			return;
		}
		this.nextFrameTime = this.timeBetweenFrames;
		if (this.playType == AnimPlayType.Forward)
		{
			if (this.currentFrameIndex + 1 >= this.frames.Length)
			{
				if (this.returnToPool)
				{
					base.GetComponent<BaseEffect>().kill();
					return;
				}
				if (!this.looped)
				{
					return;
				}
				this.currentFrameIndex = 0;
			}
			else
			{
				this.currentFrameIndex++;
			}
		}
		else if (this.currentFrameIndex - 1 < 0)
		{
			if (this.returnToPool)
			{
				base.GetComponent<BaseEffect>().kill();
				return;
			}
			if (!this.looped)
			{
				return;
			}
			this.currentFrameIndex = this.frames.Length - 1;
		}
		else
		{
			this.currentFrameIndex--;
		}
		this.updateFrame();
	}

	// Token: 0x060027CE RID: 10190 RVA: 0x00141363 File Offset: 0x0013F563
	public void stopAt(int pFrameId = 0, bool pNow = false)
	{
		this.isOn = false;
		this.currentFrameIndex = pFrameId;
		if (pNow)
		{
			this.updateFrame();
			return;
		}
		this.stopFrameTrigger = true;
	}

	// Token: 0x060027CF RID: 10191 RVA: 0x00141384 File Offset: 0x0013F584
	public void forceUpdateFrame()
	{
		if (this.frames.Length == 0)
		{
			return;
		}
		this.currentSpriteGraphic = this.frames[this.currentFrameIndex];
		this.applyCurrentSpriteGraphics(this.currentSpriteGraphic);
	}

	// Token: 0x060027D0 RID: 10192 RVA: 0x001413AF File Offset: 0x0013F5AF
	public void setRandomFrame()
	{
		this.currentFrameIndex = Randy.randomInt(0, this.frames.Length);
		this.updateFrame();
	}

	// Token: 0x060027D1 RID: 10193 RVA: 0x001413CB File Offset: 0x0013F5CB
	public void setFrameIndex(int pFrame)
	{
		this.currentFrameIndex = pFrame;
		this.updateFrame();
	}

	// Token: 0x060027D2 RID: 10194 RVA: 0x001413DC File Offset: 0x0013F5DC
	public void updateFrame()
	{
		if (this.frames.Length == 0 || this.currentFrameIndex >= this.frames.Length || this.currentSpriteGraphic == this.frames[this.currentFrameIndex])
		{
			return;
		}
		this.currentSpriteGraphic = this.frames[this.currentFrameIndex];
		this.applyCurrentSpriteGraphics(this.currentSpriteGraphic);
	}

	// Token: 0x060027D3 RID: 10195 RVA: 0x0014143C File Offset: 0x0013F63C
	internal void applyCurrentSpriteGraphics(Sprite pSprite)
	{
		if (this.useOnSpriteRenderer)
		{
			Sprite tFinalSprite = pSprite;
			if (this.phenotype != null)
			{
				tFinalSprite = DynamicSprites.getIconWithColors(pSprite, this.phenotype, null);
			}
			this.spriteRenderer.sprite = tFinalSprite;
			return;
		}
		this.image.sprite = pSprite;
	}

	// Token: 0x060027D4 RID: 10196 RVA: 0x00141482 File Offset: 0x0013F682
	public Sprite getCurrentGraphics()
	{
		return this.currentSpriteGraphic;
	}

	// Token: 0x060027D5 RID: 10197 RVA: 0x0014148A File Offset: 0x0013F68A
	public void resetAnim(int pFrameIndex = 0)
	{
		this.nextFrameTime = this.timeBetweenFrames;
		this.currentFrameIndex = pFrameIndex;
		this.updateFrame();
	}

	// Token: 0x04001DF2 RID: 7666
	public bool ignorePause = true;

	// Token: 0x04001DF3 RID: 7667
	public bool isOn = true;

	// Token: 0x04001DF4 RID: 7668
	public float timeBetweenFrames = 0.1f;

	// Token: 0x04001DF5 RID: 7669
	public float nextFrameTime;

	// Token: 0x04001DF6 RID: 7670
	public bool useNormalDeltaTime;

	// Token: 0x04001DF7 RID: 7671
	public AnimPlayType playType;

	// Token: 0x04001DF8 RID: 7672
	public int currentFrameIndex;

	// Token: 0x04001DF9 RID: 7673
	public bool looped = true;

	// Token: 0x04001DFA RID: 7674
	public bool returnToPool;

	// Token: 0x04001DFB RID: 7675
	public Sprite[] frames;

	// Token: 0x04001DFC RID: 7676
	public bool dirty;

	// Token: 0x04001DFD RID: 7677
	internal SpriteRenderer spriteRenderer;

	// Token: 0x04001DFE RID: 7678
	internal Image image;

	// Token: 0x04001DFF RID: 7679
	internal bool useOnSpriteRenderer = true;

	// Token: 0x04001E00 RID: 7680
	internal PhenotypeAsset phenotype;

	// Token: 0x04001E01 RID: 7681
	private bool stopFrameTrigger;

	// Token: 0x04001E02 RID: 7682
	internal Sprite currentSpriteGraphic;
}
