using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x0200054D RID: 1357
public class BaseDebugAnimationElement<TAsset> : MonoBehaviour where TAsset : Asset
{
	// Token: 0x06002C3B RID: 11323 RVA: 0x0015C41F File Offset: 0x0015A61F
	protected virtual void Start()
	{
		this.play_pause_button.onClick.AddListener(new UnityAction(this.clickToggleState));
		this.frame_number_button.onClick.AddListener(new UnityAction(this.clickNextFrame));
	}

	// Token: 0x06002C3C RID: 11324 RVA: 0x0015C45A File Offset: 0x0015A65A
	public virtual void update()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06002C3D RID: 11325 RVA: 0x0015C461 File Offset: 0x0015A661
	public virtual void setData(TAsset pAsset)
	{
		this.asset = pAsset;
		this.clear();
		this.is_playing = true;
	}

	// Token: 0x06002C3E RID: 11326 RVA: 0x0015C477 File Offset: 0x0015A677
	protected virtual void clear()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06002C3F RID: 11327 RVA: 0x0015C47E File Offset: 0x0015A67E
	public virtual void stopAnimations()
	{
		this.is_playing = false;
		this.checkButtons();
	}

	// Token: 0x06002C40 RID: 11328 RVA: 0x0015C48D File Offset: 0x0015A68D
	public virtual void startAnimations()
	{
		this.is_playing = true;
		this.checkButtons();
	}

	// Token: 0x06002C41 RID: 11329 RVA: 0x0015C49C File Offset: 0x0015A69C
	private void clickToggleState()
	{
		this.is_playing = !this.is_playing;
		if (this.is_playing)
		{
			this.startAnimations();
			return;
		}
		this.stopAnimations();
	}

	// Token: 0x06002C42 RID: 11330 RVA: 0x0015C4C4 File Offset: 0x0015A6C4
	private void checkButtons()
	{
		if (this.is_playing)
		{
			this.play_pause_icon.sprite = this.sprite_pause;
			this.frame_number_button.interactable = false;
			return;
		}
		this.play_pause_icon.sprite = this.sprite_play;
		this.frame_number_button.interactable = true;
	}

	// Token: 0x06002C43 RID: 11331 RVA: 0x0015C514 File Offset: 0x0015A714
	protected virtual void clickNextFrame()
	{
		throw new NotImplementedException();
	}

	// Token: 0x040021EC RID: 8684
	protected TAsset asset;

	// Token: 0x040021ED RID: 8685
	public Button play_pause_button;

	// Token: 0x040021EE RID: 8686
	public Image play_pause_icon;

	// Token: 0x040021EF RID: 8687
	public Sprite sprite_play;

	// Token: 0x040021F0 RID: 8688
	public Sprite sprite_pause;

	// Token: 0x040021F1 RID: 8689
	public Button frame_number_button;

	// Token: 0x040021F2 RID: 8690
	public Text frame_number_text;

	// Token: 0x040021F3 RID: 8691
	protected bool is_playing;
}
