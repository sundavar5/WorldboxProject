using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020004AF RID: 1199
public class RewardAnimation : MonoBehaviour
{
	// Token: 0x0600298A RID: 10634 RVA: 0x001487A4 File Offset: 0x001469A4
	private void Awake()
	{
		this._icon_transform = this.rewardedPowerIcon.transform;
		this._text_transform = this.rewardTexts.transform;
		this._rotation_animation = this.boxSprite.GetComponent<IconRotationAnimation>();
		this._sprite_animation = this.boxSprite.GetComponent<SpriteAnimation>();
		this._sprite_animation.Awake();
		if (this._original_pos == Vector3.zero)
		{
			this._original_pos = this._icon_transform.localPosition;
		}
	}

	// Token: 0x0600298B RID: 10635 RVA: 0x00148824 File Offset: 0x00146A24
	public void OnEnable()
	{
		if (this._original_pos == Vector3.zero)
		{
			this._original_pos = this._icon_transform.localPosition;
		}
		this.bottomButtonText.key = "free_power_button_open_in";
		this.bottomButtonText.updateText(true);
		this.resetAnim();
	}

	// Token: 0x0600298C RID: 10636 RVA: 0x00148876 File Offset: 0x00146A76
	private void OnDisable()
	{
		this._icon_scale_tween.Kill(false);
		this._icon_move_tween.Kill(false);
		this._text_tween.Kill(false);
	}

	// Token: 0x0600298D RID: 10637 RVA: 0x0014889C File Offset: 0x00146A9C
	public void resetAnim()
	{
		this.state = RewardAnimationState.Idle;
		this._sprite_animation.resetAnim(3);
		this._rotation_animation.enabled = true;
		this._icon_transform.DOKill(false);
		this.rewardedPowerIcon.SetActive(false);
		this.rewardTexts.SetActive(false);
		this.Text_free_power_unlocked.gameObject.SetActive(false);
		this.Text_free_power_tap_to_unlock.gameObject.SetActive(true);
	}

	// Token: 0x0600298E RID: 10638 RVA: 0x00148910 File Offset: 0x00146B10
	private void Update()
	{
		if (this.quickReward && this._sprite_animation.currentFrameIndex < 7)
		{
			this._sprite_animation.currentFrameIndex = 7;
			this.showRewards(false);
			this.moveStageThree();
		}
		if (this.state == RewardAnimationState.Play || this.state == RewardAnimationState.Open)
		{
			this._sprite_animation.update(Time.deltaTime);
			if (this._sprite_animation.currentFrameIndex > 6 && this.state != RewardAnimationState.Open)
			{
				this.showRewards(true);
			}
		}
	}

	// Token: 0x0600298F RID: 10639 RVA: 0x0014898C File Offset: 0x00146B8C
	private void showRewards(bool pStart = true)
	{
		this.state = RewardAnimationState.Open;
		this.rewardedPowerIcon.SetActive(true);
		this._text_tween.Kill(false);
		this._text_transform.localScale = new Vector3(0.5f, 0.5f);
		this._text_tween = this._text_transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f).SetEase(Ease.OutBack);
		this.rewardTexts.gameObject.SetActive(true);
		this.Text_free_power_unlocked.gameObject.SetActive(true);
		this.Text_free_power_tap_to_unlock.gameObject.SetActive(false);
		this.bottomButtonText.key = "get_it";
		this.bottomButtonText.updateText(true);
		this._icon_transform.DOKill(false);
		this._icon_transform.localPosition = this._original_pos;
		this._icon_transform.localScale = new Vector3(0.02f, 0.1f, 1f);
		if (pStart)
		{
			Vector3 tPunchPos = new Vector3(this._original_pos.x, this._original_pos.y, 0f);
			tPunchPos.y += 22f;
			this._icon_move_tween = this._icon_transform.DOLocalMove(tPunchPos, this.moveTime1, false).SetEase(Ease.OutCirc).OnComplete(new TweenCallback(this.moveStageTwo));
		}
		this._icon_scale_tween = this._icon_transform.DOScale(new Vector3(0.75f, 0.75f, 1f), this.rewardedPowerScaleTime).SetEase(Ease.Flash).OnComplete(new TweenCallback(this.scaleStageTwo));
	}

	// Token: 0x06002990 RID: 10640 RVA: 0x00148B38 File Offset: 0x00146D38
	private void moveStageTwo()
	{
		this._icon_move_tween.Kill(false);
		this._icon_move_tween = this._icon_transform.DOLocalMove(this._original_pos, this.moveTime2, false).SetEase(Ease.InOutQuad).OnComplete(new TweenCallback(this.moveStageThree));
	}

	// Token: 0x06002991 RID: 10641 RVA: 0x00148B88 File Offset: 0x00146D88
	private void moveStageThree()
	{
		this._icon_move_tween.Kill(false);
		Vector3 tVec = new Vector3(this._original_pos.x, this._original_pos.y, 1f);
		tVec.y += 3f;
		this._icon_move_tween = this._icon_transform.DOLocalMove(tVec, this.moveTime3, false).SetEase(Ease.InOutQuad).OnComplete(new TweenCallback(this.moveStageFour));
	}

	// Token: 0x06002992 RID: 10642 RVA: 0x00148C04 File Offset: 0x00146E04
	private void moveStageFour()
	{
		this._icon_move_tween.Kill(false);
		this._icon_move_tween = this._icon_transform.DOLocalMove(this._original_pos, this.moveTime4, false).SetEase(Ease.InOutQuad).OnComplete(new TweenCallback(this.moveStageThree));
	}

	// Token: 0x06002993 RID: 10643 RVA: 0x00148C52 File Offset: 0x00146E52
	private void scaleStageTwo()
	{
	}

	// Token: 0x06002994 RID: 10644 RVA: 0x00148C54 File Offset: 0x00146E54
	public void clickAnimation()
	{
		if (this._sprite_animation.currentFrameIndex > 5)
		{
			return;
		}
		this._sprite_animation.resetAnim(0);
		this._rotation_animation.enabled = false;
		this._rotation_animation.transform.localScale = new Vector3(1f, 1f, 1f);
		if (this.state != RewardAnimationState.Idle)
		{
			this.resetAnim();
		}
		this.state = RewardAnimationState.Play;
	}

	// Token: 0x04001EF8 RID: 7928
	public Image boxSprite;

	// Token: 0x04001EF9 RID: 7929
	public GameObject rewardTexts;

	// Token: 0x04001EFA RID: 7930
	public Text Text_free_power_unlocked;

	// Token: 0x04001EFB RID: 7931
	public Text Text_free_power_tap_to_unlock;

	// Token: 0x04001EFC RID: 7932
	private IconRotationAnimation _rotation_animation;

	// Token: 0x04001EFD RID: 7933
	public GameObject rewardedPowerIcon;

	// Token: 0x04001EFE RID: 7934
	private SpriteAnimation _sprite_animation;

	// Token: 0x04001EFF RID: 7935
	internal RewardAnimationState state;

	// Token: 0x04001F00 RID: 7936
	public LocalizedText bottomButtonText;

	// Token: 0x04001F01 RID: 7937
	private Vector3 _original_pos = Vector3.zero;

	// Token: 0x04001F02 RID: 7938
	public bool quickReward;

	// Token: 0x04001F03 RID: 7939
	private Tweener _icon_move_tween;

	// Token: 0x04001F04 RID: 7940
	private Tweener _icon_scale_tween;

	// Token: 0x04001F05 RID: 7941
	private Tweener _text_tween;

	// Token: 0x04001F06 RID: 7942
	public float rewardedPowerScaleTime = 0.45f;

	// Token: 0x04001F07 RID: 7943
	public float moveTime1 = 0.25f;

	// Token: 0x04001F08 RID: 7944
	public float moveTime2 = 0.25f;

	// Token: 0x04001F09 RID: 7945
	public float moveTime3 = 1.5f;

	// Token: 0x04001F0A RID: 7946
	public float moveTime4 = 1.5f;

	// Token: 0x04001F0B RID: 7947
	private Transform _icon_transform;

	// Token: 0x04001F0C RID: 7948
	private Transform _text_transform;
}
