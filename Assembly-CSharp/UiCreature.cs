using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000608 RID: 1544
public class UiCreature : MonoBehaviour
{
	// Token: 0x060032A6 RID: 12966 RVA: 0x0017FE14 File Offset: 0x0017E014
	private void Awake()
	{
		this._original_parent = base.transform.parent;
		this._init_scale = base.transform.localScale;
		this._initial_pos = base.transform.localPosition;
		this._initial_rotation = base.transform.rotation;
	}

	// Token: 0x060032A7 RID: 12967 RVA: 0x0017FE65 File Offset: 0x0017E065
	private void Start()
	{
		if (!base.gameObject.HasComponent<Button>())
		{
			base.gameObject.AddComponent<Button>().onClick.AddListener(new UnityAction(this.click));
		}
	}

	// Token: 0x060032A8 RID: 12968 RVA: 0x0017FE95 File Offset: 0x0017E095
	private void killTweens(bool pComplete = false)
	{
		if (pComplete)
		{
			this._forced_complete = true;
		}
		this.tweener_scale.Kill(pComplete);
		this.tweener_rotation.Kill(pComplete);
		this.tweener_move.Kill(pComplete);
		this._forced_complete = false;
	}

	// Token: 0x060032A9 RID: 12969 RVA: 0x0017FECC File Offset: 0x0017E0CC
	internal void resetPosition()
	{
		this.killTweens(false);
		this.dropped = false;
		base.transform.rotation = this._initial_rotation;
		base.transform.localPosition = this._initial_pos;
		base.transform.localScale = this._init_scale;
		base.gameObject.SetActive(true);
	}

	// Token: 0x060032AA RID: 12970 RVA: 0x0017FF28 File Offset: 0x0017E128
	public void click()
	{
		if (this.dropped)
		{
			return;
		}
		this.killTweens(false);
		if (this.HasComponent<HoveringIcon>())
		{
			base.GetComponent<HoveringIcon>().clear();
		}
		if (this.HasComponent<LivingIcon>())
		{
			base.GetComponent<LivingIcon>().kill();
		}
		if (!string.IsNullOrEmpty(this.achievement))
		{
			AchievementLibrary.unlock(this.achievement);
		}
		if (this.doPlayPunch)
		{
			MusicBox.playSound("event:/SFX/OTHER/Punch", -1f, -1f, false, false);
		}
		if (this.doSfx != "none" && !string.IsNullOrEmpty(this.doSfx) && this.doSfx.Contains("event:"))
		{
			MusicBox.playSound(this.doSfx, -1f, -1f, false, false);
		}
		if (this.doScale)
		{
			Vector3 tScale = this._init_scale * 1.2f;
			base.transform.localScale = tScale;
			this.tweener_scale = base.transform.DOScale(this._init_scale, 0.3f).SetEase(Ease.OutBack);
		}
		if (this.doFall)
		{
			this.fall();
		}
		if (this.doFly)
		{
			this.flyAway();
		}
		if (this.doRotate)
		{
			if (Randy.randomBool())
			{
				this.tweener_rotation = base.transform.DORotate(new Vector3(0f, 0f, Randy.randomFloat(90f, 180f)), 1f, RotateMode.Fast).SetEase(Ease.OutCubic);
				return;
			}
			this.tweener_rotation = base.transform.DORotate(new Vector3(0f, 0f, Randy.randomFloat(-180f, -90f)), 1f, RotateMode.Fast).SetEase(Ease.OutCubic);
		}
	}

	// Token: 0x060032AB RID: 12971 RVA: 0x001800D8 File Offset: 0x0017E2D8
	private void flyAway()
	{
		this.dropped = true;
		if (this.changeParent)
		{
			base.transform.parent = CanvasMain.instance.canvas_tooltip.transform;
		}
		Vector3 tVec = new Vector3(base.transform.position.x + Randy.randomFloat(-200f, 200f), 1000f, 0f);
		this.tweener_move = base.transform.DOMove(tVec, 0.6f, false).SetEase(Ease.InQuad).OnComplete(new TweenCallback(this.completeFly));
	}

	// Token: 0x060032AC RID: 12972 RVA: 0x00180170 File Offset: 0x0017E370
	private void fall()
	{
		this.dropped = true;
		if (this.changeParent)
		{
			base.transform.SetParent(CanvasMain.instance.canvas_tooltip.transform);
		}
		Vector3 tVec = new Vector3(base.transform.position.x + Randy.randomFloat(-4f, 4f), base.transform.position.y - (float)Screen.height, 0f);
		this.tweener_move = base.transform.DOMove(tVec, 0.6f, false).SetEase(Ease.InQuad).OnComplete(new TweenCallback(this.completeFall));
	}

	// Token: 0x060032AD RID: 12973 RVA: 0x00180218 File Offset: 0x0017E418
	private void completeFly()
	{
		base.transform.SetParent(this._original_parent);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060032AE RID: 12974 RVA: 0x00180238 File Offset: 0x0017E438
	private void completeFall()
	{
		if (this._forced_complete)
		{
			base.gameObject.SetActive(false);
			return;
		}
		base.transform.SetParent(this._original_parent);
		MusicBox.playSound("event:/SFX/HIT/HitStone", -1f, -1f, false, false);
		base.gameObject.SetActive(false);
	}

	// Token: 0x060032AF RID: 12975 RVA: 0x0018028D File Offset: 0x0017E48D
	private void OnEnable()
	{
		this.resetPosition();
	}

	// Token: 0x060032B0 RID: 12976 RVA: 0x00180295 File Offset: 0x0017E495
	private void OnDisable()
	{
		this.killTweens(true);
	}

	// Token: 0x04002642 RID: 9794
	public bool doFall;

	// Token: 0x04002643 RID: 9795
	public bool doRotate;

	// Token: 0x04002644 RID: 9796
	public bool doScale = true;

	// Token: 0x04002645 RID: 9797
	public bool doFly;

	// Token: 0x04002646 RID: 9798
	public bool doPlayPunch;

	// Token: 0x04002647 RID: 9799
	public bool changeParent = true;

	// Token: 0x04002648 RID: 9800
	public string doSfx = "none";

	// Token: 0x04002649 RID: 9801
	private Tweener tweener_scale;

	// Token: 0x0400264A RID: 9802
	private Tweener tweener_rotation;

	// Token: 0x0400264B RID: 9803
	private Tweener tweener_move;

	// Token: 0x0400264C RID: 9804
	private Vector3 _init_scale = Vector3.one;

	// Token: 0x0400264D RID: 9805
	internal bool dropped;

	// Token: 0x0400264E RID: 9806
	public string achievement = "";

	// Token: 0x0400264F RID: 9807
	private Vector3 _initial_pos;

	// Token: 0x04002650 RID: 9808
	private Quaternion _initial_rotation;

	// Token: 0x04002651 RID: 9809
	private Transform _original_parent;

	// Token: 0x04002652 RID: 9810
	private bool _forced_complete;
}
