using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000015 RID: 21
public class AchievementPopup : MonoBehaviour
{
	// Token: 0x060000F9 RID: 249 RVA: 0x00009A95 File Offset: 0x00007C95
	private void Awake()
	{
		AchievementPopup._instance = this;
		this.hide();
	}

	// Token: 0x060000FA RID: 250 RVA: 0x00009AA3 File Offset: 0x00007CA3
	internal static void show(string pAchievementID)
	{
		AchievementPopup._instance.showByID(pAchievementID);
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00009AB0 File Offset: 0x00007CB0
	internal static void show(Achievement pAchievement)
	{
		AchievementPopup._instance.showByID(pAchievement.id);
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00009AC2 File Offset: 0x00007CC2
	private void Update()
	{
		World.world.spawnCongratulationFireworks();
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00009AD0 File Offset: 0x00007CD0
	internal void showByID(string pAchievementID)
	{
		if (this._tween != null && this._tween.active)
		{
			return;
		}
		base.gameObject.SetActive(true);
		this.checkPool();
		Achievement tAchievement = AssetManager.achievements.get(pAchievementID);
		Sprite tSprite = tAchievement.getIcon();
		if (tSprite != null)
		{
			this._icon_left.sprite = tSprite;
			this._icon_right.sprite = tSprite;
		}
		this._popup_text.GetComponent<LocalizedText>().setKeyAndUpdate(tAchievement.getLocaleID());
		this._popup_description.GetComponent<LocalizedText>().setKeyAndUpdate(tAchievement.getDescriptionID());
		float tDiffY = ((float)Screen.height - Screen.safeArea.height) / CanvasMain.instance.canvas_ui.scaleFactor;
		this._tween = base.transform.DOLocalMoveY(0f - tDiffY, 1f, false).SetEase(Ease.OutBack).SetDelay(0.2f).OnComplete(new TweenCallback(this.tweenHide));
		if (tAchievement.unlocks_something)
		{
			foreach (BaseUnlockableAsset tAsset in tAchievement.unlock_assets)
			{
				if (tAsset.show_for_unlockables_ui)
				{
					this._goodie_pool.getNext().load(tAsset, true);
				}
			}
		}
	}

	// Token: 0x060000FE RID: 254 RVA: 0x00009C30 File Offset: 0x00007E30
	public void forceHide()
	{
		if (this._tween != null)
		{
			this._tween.Kill(false);
		}
		this._tween = base.transform.DOLocalMoveY(200f, 0.5f, false).SetEase(Ease.OutBack).OnComplete(new TweenCallback(this.hide));
	}

	// Token: 0x060000FF RID: 255 RVA: 0x00009C85 File Offset: 0x00007E85
	private void tweenHide()
	{
		this._tween = base.transform.DOLocalMoveY(200f, 1f, false).SetDelay(4f).SetEase(Ease.OutBack).OnComplete(new TweenCallback(this.hide));
	}

	// Token: 0x06000100 RID: 256 RVA: 0x00009CC5 File Offset: 0x00007EC5
	private void hide()
	{
		base.gameObject.SetActive(false);
		ObjectPoolGenericMono<AchievementGoodie> goodie_pool = this._goodie_pool;
		if (goodie_pool == null)
		{
			return;
		}
		goodie_pool.clear(true);
	}

	// Token: 0x06000101 RID: 257 RVA: 0x00009CE4 File Offset: 0x00007EE4
	private void checkPool()
	{
		if (this._goodie_pool == null)
		{
			this._goodie_pool = new ObjectPoolGenericMono<AchievementGoodie>(this._goodie_prefab, this._goodies_parent);
		}
	}

	// Token: 0x040000B6 RID: 182
	private static AchievementPopup _instance;

	// Token: 0x040000B7 RID: 183
	[SerializeField]
	private Image _icon_left;

	// Token: 0x040000B8 RID: 184
	[SerializeField]
	private Image _icon_right;

	// Token: 0x040000B9 RID: 185
	[SerializeField]
	private Text _popup_text;

	// Token: 0x040000BA RID: 186
	[SerializeField]
	private Text _popup_description;

	// Token: 0x040000BB RID: 187
	[SerializeField]
	private AchievementGoodie _goodie_prefab;

	// Token: 0x040000BC RID: 188
	[SerializeField]
	private Transform _goodies_parent;

	// Token: 0x040000BD RID: 189
	private ObjectPoolGenericMono<AchievementGoodie> _goodie_pool;

	// Token: 0x040000BE RID: 190
	private Tweener _tween;
}
