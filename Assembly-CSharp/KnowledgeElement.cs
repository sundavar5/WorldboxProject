using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000704 RID: 1796
public class KnowledgeElement : MonoBehaviour
{
	// Token: 0x06003964 RID: 14692 RVA: 0x00198C35 File Offset: 0x00196E35
	private void OnEnable()
	{
		if (!this._initialized)
		{
			return;
		}
		this.resetBar();
	}

	// Token: 0x06003965 RID: 14693 RVA: 0x00198C46 File Offset: 0x00196E46
	private void Start()
	{
		this.init(this._asset);
		this.resetBar();
	}

	// Token: 0x06003966 RID: 14694 RVA: 0x00198C5A File Offset: 0x00196E5A
	public void setAsset(KnowledgeAsset pAsset)
	{
		this._asset = pAsset;
	}

	// Token: 0x06003967 RID: 14695 RVA: 0x00198C63 File Offset: 0x00196E63
	public void setCube(CubeOverview pBigCube, WindowMetaTab pCubeTab)
	{
		this._cube_overview_big = pBigCube;
		this._cube_tab = pCubeTab;
	}

	// Token: 0x06003968 RID: 14696 RVA: 0x00198C74 File Offset: 0x00196E74
	private void init(KnowledgeAsset pAsset)
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		this._asset = pAsset;
		this._localized_text.setKeyAndUpdate(this._asset.getLocaleID());
		Sprite tIcon = this._asset.getIcon();
		this._icon_left.sprite = tIcon;
		this._icon_left.GetComponentInParent<Button>().onClick.AddListener(delegate()
		{
			this._asset.click_icon_action(this._asset);
		});
		this._icon_right.GetComponentInParent<Button>().onClick.AddListener(delegate()
		{
			this._cube_overview_big.setFilterAsset(this._asset);
			this._cube_tab.tab_action.Invoke(this._cube_tab);
		});
		this._library = this._asset.get_library();
		foreach (BaseUnlockableAsset tAsset in this._library.elements_list)
		{
			if (tAsset.show_in_knowledge_window)
			{
				this._assets_list.Add(tAsset);
			}
		}
		this._assets_list.Shuffle<BaseUnlockableAsset>();
		this._running_icons.init(new RunningIconCallback(this.prevItem), new RunningIconCallback(this.nextItem));
		using (ListPool<Vector3> tPositions = new ListPool<Vector3>(this._running_icons.transform.childCount))
		{
			foreach (object obj in this._running_icons.transform)
			{
				Transform tChild = (Transform)obj;
				tPositions.Add(tChild.localPosition);
				Object.Destroy(tChild.gameObject);
			}
			Transform tPrefab = Resources.Load<Transform>(pAsset.button_prefab_path);
			foreach (Vector3 ptr in tPositions)
			{
				Vector3 tPosition = ptr;
				Transform tElement = Object.Instantiate<Transform>(tPrefab, this._running_icons.transform);
				tElement.transform.localPosition = tPosition;
				Transform transform = tElement;
				int items = this._items;
				this._items = items + 1;
				transform.SetSiblingIndex(items);
				if (!tElement.HasComponent<RunningIcon>())
				{
					tElement.AddComponent<RunningIcon>();
				}
				this._running_icons.addIcon(tElement.GetComponent<RunningIcon>());
				Button componentInChildren = tElement.GetComponentInChildren<Button>();
				componentInChildren.enabled = false;
				componentInChildren.OnHover(delegate()
				{
					this._running_icons.toggle(false);
				});
				componentInChildren.OnHoverOut(delegate()
				{
					this._running_icons.toggle(true);
				});
				DraggableLayoutElement tDraggable;
				if (tElement.TryGetComponent<DraggableLayoutElement>(out tDraggable))
				{
					tDraggable.enabled = false;
				}
				BaseUnlockableAsset tAsset2 = this.getNextAsset();
				this._asset.load_button(tElement, tAsset2);
				ButtonTipLoader tip_button_loader = this._asset.tip_button_loader;
				if (tip_button_loader != null)
				{
					tip_button_loader(tElement, tAsset2);
				}
			}
			this.checkEasterEggsSprite();
		}
	}

	// Token: 0x06003969 RID: 14697 RVA: 0x00198F94 File Offset: 0x00197194
	private void checkEasterEggsSprite()
	{
		if (string.IsNullOrEmpty(this._asset.path_icon_easter_egg))
		{
			this._icon_easter_left.gameObject.SetActive(false);
			this._icon_easter_right.gameObject.SetActive(false);
			return;
		}
		Sprite tIcon = SpriteTextureLoader.getSprite(this._asset.path_icon_easter_egg);
		this._icon_easter_left.main_image.sprite = tIcon;
		this._icon_easter_right.main_image.sprite = tIcon;
	}

	// Token: 0x0600396A RID: 14698 RVA: 0x0019900C File Offset: 0x0019720C
	private void resetBar()
	{
		int tCurrent = this._asset.countUnlockedByPlayer();
		int tMax = this._asset.countTotal();
		this._progress_bar.setBar((float)tCurrent, (float)tMax, "/" + tMax.ToText(), true, false, true, 0.3f);
	}

	// Token: 0x0600396B RID: 14699 RVA: 0x00199058 File Offset: 0x00197258
	private void nextItem(Transform pButton)
	{
		BaseUnlockableAsset tAsset = this.getNextAsset();
		this._asset.load_button(pButton, tAsset);
		ButtonTipLoader tip_button_loader = this._asset.tip_button_loader;
		if (tip_button_loader == null)
		{
			return;
		}
		tip_button_loader(pButton, tAsset);
	}

	// Token: 0x0600396C RID: 14700 RVA: 0x00199098 File Offset: 0x00197298
	private BaseUnlockableAsset getNextAsset()
	{
		this._running_icon_latest_index++;
		int tIndex = Toolbox.loopIndex(this._running_icon_latest_index, this._assets_list.Count);
		this._running_icon_latest_index = tIndex;
		return this._assets_list[tIndex];
	}

	// Token: 0x0600396D RID: 14701 RVA: 0x001990E0 File Offset: 0x001972E0
	private void prevItem(Transform pButton)
	{
		BaseUnlockableAsset tAsset = this.getPrevAsset();
		this._asset.load_button(pButton, tAsset);
		ButtonTipLoader tip_button_loader = this._asset.tip_button_loader;
		if (tip_button_loader == null)
		{
			return;
		}
		tip_button_loader(pButton, tAsset);
	}

	// Token: 0x0600396E RID: 14702 RVA: 0x00199120 File Offset: 0x00197320
	private BaseUnlockableAsset getPrevAsset()
	{
		this._running_icon_latest_index--;
		int tIndex = Toolbox.loopIndex(this._running_icon_latest_index, this._assets_list.Count);
		this._running_icon_latest_index = tIndex;
		int tPrevIndex = Toolbox.loopIndex(tIndex - this._items + 1, this._assets_list.Count);
		return this._assets_list[tPrevIndex];
	}

	// Token: 0x04002A4C RID: 10828
	[SerializeField]
	private LocalizedText _localized_text;

	// Token: 0x04002A4D RID: 10829
	[SerializeField]
	private Image _icon_left;

	// Token: 0x04002A4E RID: 10830
	[SerializeField]
	private Image _icon_right;

	// Token: 0x04002A4F RID: 10831
	[SerializeField]
	private EasterEggBanner _icon_easter_left;

	// Token: 0x04002A50 RID: 10832
	[SerializeField]
	private EasterEggBanner _icon_easter_right;

	// Token: 0x04002A51 RID: 10833
	[SerializeField]
	private StatBar _progress_bar;

	// Token: 0x04002A52 RID: 10834
	[SerializeField]
	private RunningIcons _running_icons;

	// Token: 0x04002A53 RID: 10835
	private CubeOverview _cube_overview_big;

	// Token: 0x04002A54 RID: 10836
	private WindowMetaTab _cube_tab;

	// Token: 0x04002A55 RID: 10837
	private KnowledgeAsset _asset;

	// Token: 0x04002A56 RID: 10838
	private int _running_icon_latest_index;

	// Token: 0x04002A57 RID: 10839
	private ILibraryWithUnlockables _library;

	// Token: 0x04002A58 RID: 10840
	private List<BaseUnlockableAsset> _assets_list = new List<BaseUnlockableAsset>();

	// Token: 0x04002A59 RID: 10841
	private int _items;

	// Token: 0x04002A5A RID: 10842
	private bool _initialized;
}
