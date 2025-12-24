using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200081A RID: 2074
public class WorldLawsTextInsult : MonoBehaviour
{
	// Token: 0x060040F7 RID: 16631 RVA: 0x001BBA81 File Offset: 0x001B9C81
	public static void removeInsultTimeout()
	{
		WorldLawsTextInsult._global_wait_timeout = 0f;
	}

	// Token: 0x060040F8 RID: 16632 RVA: 0x001BBA90 File Offset: 0x001B9C90
	public void Update()
	{
		if (!this.shouldInsultNow())
		{
			return;
		}
		if (this._wait_time > 0f)
		{
			this._wait_time -= Time.deltaTime;
			return;
		}
		if (WorldLawsTextInsult._global_wait_timeout > 0f && !this.isTweening())
		{
			WorldLawsTextInsult._global_wait_timeout -= Time.deltaTime;
			return;
		}
		if (!this.isTweening())
		{
			this.startNewTween();
		}
	}

	// Token: 0x060040F9 RID: 16633 RVA: 0x001BBAFC File Offset: 0x001B9CFC
	private void startNewTween()
	{
		this.killTweens();
		if (WorldLawLibrary.world_law_cursed_world.isEnabled())
		{
			WorldLawsTextInsult._global_wait_timeout = 0.6f + Randy.randomFloat(0f, 2f);
		}
		else
		{
			WorldLawsTextInsult._global_wait_timeout = 2f + Randy.randomFloat(0f, 3f);
		}
		Vector3 tRotation = new Vector3(0f, 0f, Randy.randomFloat(-30f, 30f));
		this._size_parent.localRotation = Quaternion.Euler(tRotation);
		this._text.text = this.getInsultText();
		this._text.transform.position = this._follow_object.position + new Vector3(0f, (float)Randy.randomInt(8, 12), 0f);
		this._text.fontSize = Randy.randomInt(7, 9);
		Vector3 tTarget = this._text.transform.position + new Vector3(0f, Randy.randomFloat(30f, 60f), 0f);
		this._text_tweener = this._text.transform.DOMove(tTarget, 6f, false).SetEase(Ease.OutCubic);
		this._text.DOColor(Color.white, 2f).onComplete = new TweenCallback(this.doTextFade);
	}

	// Token: 0x060040FA RID: 16634 RVA: 0x001BBC5C File Offset: 0x001B9E5C
	private string getInsultText()
	{
		string tInsultText;
		if (Randy.randomChance(0.005f))
		{
			tInsultText = this._insults_rare.GetRandom<string>();
		}
		else
		{
			tInsultText = InsultStringGenerator.getRandomText(4, 9, false);
		}
		return tInsultText;
	}

	// Token: 0x060040FB RID: 16635 RVA: 0x001BBC8E File Offset: 0x001B9E8E
	private void doTextFade()
	{
		this._text.DOFade(0f, 2f).onComplete = new TweenCallback(this.doWait);
	}

	// Token: 0x060040FC RID: 16636 RVA: 0x001BBCB6 File Offset: 0x001B9EB6
	private bool shouldInsultNow()
	{
		return CursedSacrifice.isWorldReadyForCURSE() || WorldLawLibrary.world_law_cursed_world.isEnabled();
	}

	// Token: 0x060040FD RID: 16637 RVA: 0x001BBCCB File Offset: 0x001B9ECB
	private bool isTweening()
	{
		return this._text_tweener.IsActive();
	}

	// Token: 0x060040FE RID: 16638 RVA: 0x001BBCD8 File Offset: 0x001B9ED8
	private void OnEnable()
	{
		this.doWait();
	}

	// Token: 0x060040FF RID: 16639 RVA: 0x001BBCE0 File Offset: 0x001B9EE0
	private void doWait()
	{
		this.killTweens();
		this._wait_time = Randy.randomFloat(0f, 7f);
		this._text.color = Toolbox.color_white_transparent;
	}

	// Token: 0x06004100 RID: 16640 RVA: 0x001BBD0D File Offset: 0x001B9F0D
	private void killTweens()
	{
		Tweener text_tweener = this._text_tweener;
		if (text_tweener != null)
		{
			text_tweener.Kill(false);
		}
		this._text.DOKill(false);
	}

	// Token: 0x04002F2A RID: 12074
	[SerializeField]
	private Transform _follow_object;

	// Token: 0x04002F2B RID: 12075
	[SerializeField]
	private RectTransform _size_parent;

	// Token: 0x04002F2C RID: 12076
	[SerializeField]
	private Text _text;

	// Token: 0x04002F2D RID: 12077
	private static float _global_wait_timeout;

	// Token: 0x04002F2E RID: 12078
	private const float RARE_INSULT_CHANCE = 0.005f;

	// Token: 0x04002F2F RID: 12079
	private float _wait_time;

	// Token: 0x04002F30 RID: 12080
	private Tweener _text_tweener;

	// Token: 0x04002F31 RID: 12081
	private string[] _insults_rare = new string[]
	{
		"UPDATE?",
		"WHEN",
		"GEB",
		"BRE",
		"REBR",
		"MODERN?",
		"HELP",
		"CAKE",
		"BRURSE",
		"MAXIM",
		"MASTEF",
		"HUGO",
		"NIKON",
		"JECO"
	};
}
