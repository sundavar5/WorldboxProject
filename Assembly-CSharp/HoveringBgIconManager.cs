using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

// Token: 0x020005DB RID: 1499
public class HoveringBgIconManager : MonoBehaviour
{
	// Token: 0x0600314F RID: 12623 RVA: 0x00179B80 File Offset: 0x00177D80
	private void Awake()
	{
		if (this._pool_icons != null)
		{
			return;
		}
		HoveringBgIconManager._instance = this;
		this._rect = base.GetComponent<RectTransform>();
		this._canvas_group = base.GetComponent<CanvasGroup>();
		this._pool_icons = new ObjectPoolGenericMono<HoveringIcon>(this._icon_prefab, this._icon_pool);
		for (int i = 0; i < this._icons.childCount; i++)
		{
			RectTransform tChild = this._icons.GetChild(i) as RectTransform;
			this._places.Add(tChild);
			tChild.gameObject.name = "Placing " + i.ToString();
		}
	}

	// Token: 0x06003150 RID: 12624 RVA: 0x00179C1B File Offset: 0x00177E1B
	private void OnDisable()
	{
		this._pool_icons.clear(true);
	}

	// Token: 0x06003151 RID: 12625 RVA: 0x00179C2C File Offset: 0x00177E2C
	public void fadeIn()
	{
		this._icons.gameObject.SetActive(true);
		this._canvas_group.DOFade(1f, 0.2f);
		this._canvas_group.interactable = true;
		this._canvas_group.blocksRaycasts = true;
	}

	// Token: 0x06003152 RID: 12626 RVA: 0x00179C78 File Offset: 0x00177E78
	public void fadeOut()
	{
		this._canvas_group.interactable = false;
		this._canvas_group.blocksRaycasts = false;
		this._canvas_group.DOFade(0f, 0.2f);
		this.clear();
		this.resetPlaces();
		this._icons.gameObject.SetActive(false);
	}

	// Token: 0x06003153 RID: 12627 RVA: 0x00179CD0 File Offset: 0x00177ED0
	private void resetPlaces()
	{
		if (Randy.randomBool())
		{
			return;
		}
		float tCenterX = this._rect.rect.width / 2f;
		float tCenterY = this._rect.rect.height / 2f;
		Vector3 tCenter = new Vector3(tCenterX, tCenterY, 0f);
		for (int i = 0; i < this._places.Count; i++)
		{
			RectTransform rectTransform = this._places[i];
			rectTransform.DOKill(false);
			rectTransform.anchoredPosition = tCenter;
		}
	}

	// Token: 0x06003154 RID: 12628 RVA: 0x00179D64 File Offset: 0x00177F64
	private void shufflePlaces()
	{
		this.resetPlaces();
		float tMaxX = this._rect.rect.width;
		float tMaxY = this._rect.rect.height;
		for (int i = 0; i < this._places.Count; i++)
		{
			RectTransform target = this._places[i];
			float tDuration = Randy.randomFloat(0.15f, 0.35f);
			target.DOAnchorPos(new Vector3(Randy.randomFloat(0f, tMaxX), Randy.randomFloat(0f, tMaxY), 0f), tDuration, false);
		}
	}

	// Token: 0x06003155 RID: 12629 RVA: 0x00179E00 File Offset: 0x00178000
	public void animate(WindowAsset pWindowAsset)
	{
		this.clear();
		this.shufflePlaces();
		float tStartAngle = Randy.randomFloat(0f, 360f);
		string tMainPath = "ui/Icons/";
		using (ListPool<string> tResultList = new ListPool<string>(16))
		{
			Delegate[] invocationList = pWindowAsset.get_hovering_icons.GetInvocationList();
			for (int j = 0; j < invocationList.Length; j++)
			{
				foreach (string tPath in ((HoveringBGIconsGetter)invocationList[j])(pWindowAsset))
				{
					if (tPath.EndsWith("/"))
					{
						Sprite[] tSprites = SpriteTextureLoader.getSpriteList(tMainPath + tPath, false);
						for (int i = 0; i < tSprites.Length; i++)
						{
							string tResultString = tMainPath + tPath + tSprites[i].name;
							tResultList.Add(tResultString);
						}
					}
					else
					{
						string tResultString2 = tMainPath + tPath;
						tResultList.Add(tResultString2);
					}
				}
			}
			foreach (RectTransform tPlace in this._places)
			{
				string tIconToLoadPath = tResultList.GetRandom<string>();
				HoveringIcon tIcon = this._pool_icons.getNext();
				tIcon.clear();
				tIcon.transform.SetParent(tPlace, false);
				tIcon.rect.anchoredPosition = Vector3.zero;
				tIcon.transform.rotation = Quaternion.identity;
				tIcon.image.sprite = SpriteTextureLoader.getSprite(tIconToLoadPath);
				if (this._random_scale)
				{
					float tScale = Randy.randomFloat(0.4f, 1f);
					tIcon.transform.localScale = new Vector3(tScale, tScale, tScale);
				}
				else
				{
					tIcon.transform.localScale = tPlace.localScale;
				}
				Vector3 tCurScale = tIcon.transform.localScale;
				tIcon.image.color = new Color(tCurScale.x, tCurScale.x, tCurScale.x, 1f);
				tStartAngle += Randy.randomFloat(20f, 130f);
				tIcon.transform.eulerAngles = new Vector3(0f, 0f, tStartAngle);
				tIcon.init();
			}
		}
	}

	// Token: 0x06003156 RID: 12630 RVA: 0x0017A09C File Offset: 0x0017829C
	public static void show()
	{
		HoveringBgIconManager._instance.fadeIn();
	}

	// Token: 0x06003157 RID: 12631 RVA: 0x0017A0A8 File Offset: 0x001782A8
	public static void hide()
	{
		HoveringBgIconManager._instance.fadeOut();
	}

	// Token: 0x06003158 RID: 12632 RVA: 0x0017A0B4 File Offset: 0x001782B4
	public static void showWindow(WindowAsset pWindowAsset)
	{
		HoveringBgIconManager._instance.animate(pWindowAsset);
	}

	// Token: 0x06003159 RID: 12633 RVA: 0x0017A0C4 File Offset: 0x001782C4
	public static void dropAll()
	{
		foreach (HoveringIcon tIcon in HoveringBgIconManager._instance._pool_icons.getListTotal())
		{
			if (tIcon.gameObject.activeSelf)
			{
				UiCreature tCreature = tIcon.GetComponent<UiCreature>();
				if (!tCreature.dropped)
				{
					tCreature.click();
				}
			}
		}
	}

	// Token: 0x0600315A RID: 12634 RVA: 0x0017A138 File Offset: 0x00178338
	public static void randomDrop()
	{
		using (ListPool<UiCreature> tList = new ListPool<UiCreature>(HoveringBgIconManager._instance._pool_icons.countActive()))
		{
			foreach (HoveringIcon tIcon in HoveringBgIconManager._instance._pool_icons.getListTotal())
			{
				if (tIcon.gameObject.activeSelf)
				{
					UiCreature tCreature = tIcon.GetComponent<UiCreature>();
					if (!tCreature.dropped)
					{
						tList.Add(tCreature);
					}
				}
			}
			if (tList.Count != 0)
			{
				tList.GetRandom<UiCreature>().click();
			}
		}
	}

	// Token: 0x0600315B RID: 12635 RVA: 0x0017A1EC File Offset: 0x001783EC
	private void clear()
	{
		this._pool_icons.clear(true);
		this._pool_icons.resetParent();
	}

	// Token: 0x0400253D RID: 9533
	[SerializeField]
	private HoveringIcon _icon_prefab;

	// Token: 0x0400253E RID: 9534
	private ObjectPoolGenericMono<HoveringIcon> _pool_icons;

	// Token: 0x0400253F RID: 9535
	private CanvasGroup _canvas_group;

	// Token: 0x04002540 RID: 9536
	private RectTransform _rect;

	// Token: 0x04002541 RID: 9537
	private List<RectTransform> _places = new List<RectTransform>();

	// Token: 0x04002542 RID: 9538
	[SerializeField]
	public bool _random_scale = true;

	// Token: 0x04002543 RID: 9539
	[SerializeField]
	private Transform _icon_pool;

	// Token: 0x04002544 RID: 9540
	[SerializeField]
	private Transform _icons;

	// Token: 0x04002545 RID: 9541
	private static HoveringBgIconManager _instance;
}
