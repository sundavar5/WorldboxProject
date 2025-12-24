using System;
using UnityEngine;

// Token: 0x02000548 RID: 1352
public class ActorDebugAnimationElement : BaseDebugAnimationElement<ActorAsset>
{
	// Token: 0x06002C14 RID: 11284 RVA: 0x0015B5A6 File Offset: 0x001597A6
	protected override void Start()
	{
		base.Start();
		this.adult.create();
		this.baby.create();
	}

	// Token: 0x06002C15 RID: 11285 RVA: 0x0015B5C4 File Offset: 0x001597C4
	public override void update()
	{
		if (!this.is_playing)
		{
			return;
		}
		this.adult.update(Time.deltaTime);
		if (this.asset.has_baby_form)
		{
			this.baby.update(Time.deltaTime);
		}
		this.frame_number_text.text = this.adult.currentFrameIndex.ToString();
	}

	// Token: 0x06002C16 RID: 11286 RVA: 0x0015B624 File Offset: 0x00159824
	public override void setData(ActorAsset pAsset)
	{
		base.setData(pAsset);
		if (!this.asset.has_baby_form)
		{
			this.baby.enabled = false;
			this.baby.image.sprite = null;
			this.baby.image.color = Color.clear;
		}
	}

	// Token: 0x06002C17 RID: 11287 RVA: 0x0015B678 File Offset: 0x00159878
	protected override void clear()
	{
		this.adult.enabled = true;
		this.adult.image.color = Color.white;
		this.adult.frames = Array.Empty<Sprite>();
		this.adult.resetAnim(0);
		this.baby.enabled = true;
		this.baby.image.color = Color.white;
		this.baby.frames = Array.Empty<Sprite>();
		this.baby.resetAnim(0);
	}

	// Token: 0x06002C18 RID: 11288 RVA: 0x0015B6FF File Offset: 0x001598FF
	public override void stopAnimations()
	{
		base.stopAnimations();
		this.adult.isOn = false;
		this.baby.isOn = false;
		this.frame_number_text.text = this.adult.currentFrameIndex.ToString();
	}

	// Token: 0x06002C19 RID: 11289 RVA: 0x0015B73A File Offset: 0x0015993A
	public override void startAnimations()
	{
		base.startAnimations();
		this.adult.isOn = true;
		this.baby.isOn = true;
	}

	// Token: 0x06002C1A RID: 11290 RVA: 0x0015B75C File Offset: 0x0015995C
	protected override void clickNextFrame()
	{
		if (this.is_playing)
		{
			return;
		}
		int tFramesCount = this.adult.frames.Length;
		this.adult.currentFrameIndex++;
		this.baby.currentFrameIndex++;
		if (this.adult.currentFrameIndex > tFramesCount - 1)
		{
			this.adult.currentFrameIndex = 0;
			this.baby.currentFrameIndex = 0;
		}
		this.frame_number_text.text = this.adult.currentFrameIndex.ToString();
		this.adult.updateFrame();
		this.baby.updateFrame();
	}

	// Token: 0x040021D7 RID: 8663
	public SpriteAnimation adult;

	// Token: 0x040021D8 RID: 8664
	public SpriteAnimation baby;
}
