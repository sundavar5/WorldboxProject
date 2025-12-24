using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006D5 RID: 1749
public class SelectedMultipleUnitsTab : SelectedNano<Actor>, ISelectedMetaWithUnit
{
	// Token: 0x1700031B RID: 795
	// (get) Token: 0x06003813 RID: 14355 RVA: 0x00193664 File Offset: 0x00191864
	protected override Actor nano_object
	{
		get
		{
			return SelectedUnit.unit;
		}
	}

	// Token: 0x1700031C RID: 796
	// (get) Token: 0x06003814 RID: 14356 RVA: 0x0019366B File Offset: 0x0019186B
	public SelectedMetaUnitElement unit_element
	{
		get
		{
			return this._unit_element;
		}
	}

	// Token: 0x1700031D RID: 797
	// (get) Token: 0x06003815 RID: 14357 RVA: 0x00193673 File Offset: 0x00191873
	public GameObject unit_element_separator
	{
		get
		{
			return this._unit_element_separator;
		}
	}

	// Token: 0x1700031E RID: 798
	// (get) Token: 0x06003816 RID: 14358 RVA: 0x0019367B File Offset: 0x0019187B
	private ISelectedMetaWithUnit as_meta_with_unit
	{
		get
		{
			return this;
		}
	}

	// Token: 0x1700031F RID: 799
	// (get) Token: 0x06003817 RID: 14359 RVA: 0x0019367E File Offset: 0x0019187E
	// (set) Token: 0x06003818 RID: 14360 RVA: 0x00193686 File Offset: 0x00191886
	public int last_dirty_stats_unit { get; set; }

	// Token: 0x17000320 RID: 800
	// (get) Token: 0x06003819 RID: 14361 RVA: 0x0019368F File Offset: 0x0019188F
	// (set) Token: 0x0600381A RID: 14362 RVA: 0x00193697 File Offset: 0x00191897
	public Actor last_unit { get; set; }

	// Token: 0x17000321 RID: 801
	// (get) Token: 0x0600381B RID: 14363 RVA: 0x001936A0 File Offset: 0x001918A0
	public string unit_title_locale_key
	{
		get
		{
			return null;
		}
	}

	// Token: 0x0600381C RID: 14364 RVA: 0x001936A3 File Offset: 0x001918A3
	public bool hasUnit()
	{
		return SelectedUnit.isSet();
	}

	// Token: 0x0600381D RID: 14365 RVA: 0x001936AA File Offset: 0x001918AA
	public Actor getUnit()
	{
		return SelectedUnit.unit;
	}

	// Token: 0x0600381E RID: 14366 RVA: 0x001936B1 File Offset: 0x001918B1
	protected override void Awake()
	{
		base.Awake();
		this._pool_avatars = new ObjectPoolGenericMono<UiUnitAvatarElement>(this._avatar_prefab, this._avatars_container);
		this._unfolder.setCallback(delegate(bool _)
		{
			this.showAvatars(this.getOffset(), this.getNextAmount());
		});
	}

	// Token: 0x0600381F RID: 14367 RVA: 0x001936E7 File Offset: 0x001918E7
	private void Start()
	{
		SelectedUnit.subscribeClearEvent(new SelectedUnitClearEvent(this.clearLastObject));
	}

	// Token: 0x06003820 RID: 14368 RVA: 0x001936FC File Offset: 0x001918FC
	protected override void updateElementsAlways(Actor pNano)
	{
		base.updateElementsAlways(pNano);
		bool flag = this.as_meta_with_unit.checkUnitElement();
		if (this.hasUnit())
		{
			this._unit_element.updateBarAndTask(this.getUnit());
		}
		if (flag)
		{
			this.updateAvatars();
			return;
		}
		List<Actor> tActors = SelectedUnit.getAllSelectedList();
		using (ListPool<int> tToRemove = new ListPool<int>())
		{
			for (int i = 0; i < this._showing_avatars.Count; i++)
			{
				UiUnitAvatarElement tAvatar = this._showing_avatars[i];
				if (i > tActors.Count - 1)
				{
					tToRemove.Add(i);
				}
				else
				{
					Actor tActor = tActors[i];
					int tPrevStatsVersion = this._stats_version[i];
					int tCurrentStatsVersion = tActor.getStatsDirtyVersion();
					if (!tActor.isRekt())
					{
						if (tCurrentStatsVersion != tPrevStatsVersion || tActor != tAvatar.getActor() || tAvatar.avatarLoader.actorStateChanged())
						{
							this._stats_version[i] = tCurrentStatsVersion;
							tAvatar.load(tActor);
						}
						else
						{
							tAvatar.updateTileSprite();
						}
					}
				}
			}
			tToRemove.Sort();
			tToRemove.Reverse();
			for (int j = 0; j < tToRemove.Count; j++)
			{
				int tIndex = tToRemove[j];
				UiUnitAvatarElement tAvatar2 = this._showing_avatars[tIndex];
				this._showing_avatars.RemoveAt(tIndex);
				this._stats_version.RemoveAt(tIndex);
				this._pool_avatars.release(tAvatar2, true);
			}
			this.updateUnfolderButton();
			if (tToRemove.Count > 0)
			{
				base.recalcTabSize();
			}
		}
	}

	// Token: 0x06003821 RID: 14369 RVA: 0x0019388C File Offset: 0x00191A8C
	protected override void updateElementsOnChange(Actor pNano)
	{
		base.updateElementsOnChange(pNano);
		this.updateAvatars();
		this.updateStatuses(pNano);
		this.updateEquipment(pNano);
	}

	// Token: 0x06003822 RID: 14370 RVA: 0x001938A9 File Offset: 0x00191AA9
	private void updateStatuses(Actor pActor)
	{
		this._container_status.update(pActor);
	}

	// Token: 0x06003823 RID: 14371 RVA: 0x001938B7 File Offset: 0x00191AB7
	private void updateEquipment(Actor pActor)
	{
		this._container_equipment.update(pActor);
	}

	// Token: 0x06003824 RID: 14372 RVA: 0x001938C8 File Offset: 0x00191AC8
	private void updateAvatars()
	{
		int tCurrentVersion = SelectedUnit.getSelectionVersion();
		if (tCurrentVersion == this._last_selection_version)
		{
			return;
		}
		this._last_selection_version = tCurrentVersion;
		if (this._offset == 0)
		{
			this.clear();
			this.showAvatars(this.getOffset(), this.getNextAmount());
		}
	}

	// Token: 0x06003825 RID: 14373 RVA: 0x0019390C File Offset: 0x00191B0C
	private void showAvatars(int pOffset, int pAmount)
	{
		using (ListPool<Actor> tActors = new ListPool<Actor>(SelectedUnit.getAllSelected()))
		{
			tActors.Remove(SelectedUnit.unit);
			for (int i = pOffset; i < pOffset + pAmount; i++)
			{
				Actor tActor = tActors[i];
				UiUnitAvatarElement tAvatar = this._pool_avatars.getNext();
				Button tElementButton;
				if (!tAvatar.TryGetComponent<Button>(out tElementButton))
				{
					tAvatar.AddComponent<Button>();
				}
				UnitAvatarLoader tLoader = tAvatar.avatarLoader;
				tAvatar.load(tActor);
				Button tButton;
				if (!tLoader.TryGetComponent<Button>(out tButton))
				{
					tButton = tLoader.AddComponent<Button>();
					tButton.onClick.RemoveAllListeners();
					tButton.onClick.AddListener(delegate()
					{
						Actor tAvatarActor = tAvatar.getActor();
						tAvatar.show(SelectedUnit.unit);
						SelectedUnit.makeMainSelected(tAvatarActor);
						int tIndex = this._showing_avatars.IndexOf(tAvatar);
						this._stats_version[tIndex] = SelectedUnit.unit.getStatsDirtyVersion();
						this.showWorldTip(tAvatarActor);
						PowerTabController.instance.resetToStartScrollPosition();
					});
				}
				CanvasGroup component = tLoader.GetComponent<CanvasGroup>();
				component.interactable = true;
				component.blocksRaycasts = true;
				this._showing_avatars.Add(tAvatar);
				this._stats_version.Add(tActor.getStatsDirtyVersion());
			}
			this._offset += pAmount;
			this.updateUnfolderButton();
			base.recalcTabSize();
		}
	}

	// Token: 0x06003826 RID: 14374 RVA: 0x00193A50 File Offset: 0x00191C50
	private void updateUnfolderButton()
	{
		int tLeftTotal = SelectedUnit.countSelected() - 1 - this._offset;
		if (tLeftTotal <= 0)
		{
			this._unfolder.gameObject.SetActive(false);
			return;
		}
		this._unfolder.transform.SetSiblingIndex(this._avatars_container.childCount - 1);
		this._unfolder.gameObject.SetActive(true);
		this._unfolder.setText(string.Format("+{0}", tLeftTotal));
		bool tIsLimitReached = this._offset >= 100;
		this._unfolder.getButton().interactable = !tIsLimitReached;
		if (tIsLimitReached)
		{
			this._unfolder_background.sprite = this._unfolder_inactive;
			return;
		}
		this._unfolder_background.sprite = this._unfolder_active;
	}

	// Token: 0x06003827 RID: 14375 RVA: 0x00193B18 File Offset: 0x00191D18
	protected override void showStatsGeneral(Actor pMeta)
	{
		base.showStatsGeneral(pMeta);
		if (this.hasUnit())
		{
			Actor tActor = this.getUnit();
			this._unit_element.showStats(tActor);
		}
	}

	// Token: 0x06003828 RID: 14376 RVA: 0x00193B47 File Offset: 0x00191D47
	public void avatarTouchScream()
	{
		this.as_meta_with_unit.avatarTouch();
	}

	// Token: 0x06003829 RID: 14377 RVA: 0x00193B54 File Offset: 0x00191D54
	protected override void clearLastObject()
	{
		base.clearLastObject();
		this.as_meta_with_unit.clearLastUnit();
		this._offset = 0;
	}

	// Token: 0x0600382A RID: 14378 RVA: 0x00193B70 File Offset: 0x00191D70
	private int getNextAmount()
	{
		int tRest = SelectedUnit.countSelected() - 1 - this._offset;
		return Mathf.Min(21, tRest);
	}

	// Token: 0x0600382B RID: 14379 RVA: 0x00193B94 File Offset: 0x00191D94
	private int getOffset()
	{
		return this._offset;
	}

	// Token: 0x0600382C RID: 14380 RVA: 0x00193B9C File Offset: 0x00191D9C
	private void showWorldTip(Actor pActor)
	{
		string tRow = LocalizedTextManager.getText("now_looking_at", null, false);
		string tColor = pActor.getColor().color_text;
		string tName = pActor.name.ColorHex(tColor, false);
		tRow = tRow.Replace("$name$", tName);
		WorldTip.instance.showToolbarText(tRow);
	}

	// Token: 0x0600382D RID: 14381 RVA: 0x00193BE8 File Offset: 0x00191DE8
	private void clear()
	{
		this._offset = 0;
		this._pool_avatars.clear(true);
		this._showing_avatars.Clear();
		this._stats_version.Clear();
	}

	// Token: 0x040029A1 RID: 10657
	private const int MAX_UNITS_PER_FOLD = 21;

	// Token: 0x040029A2 RID: 10658
	private const int MAX_UNITS_TOTAL = 100;

	// Token: 0x040029A3 RID: 10659
	[SerializeField]
	private SelectedMetaUnitElement _unit_element;

	// Token: 0x040029A4 RID: 10660
	[SerializeField]
	private GameObject _unit_element_separator;

	// Token: 0x040029A5 RID: 10661
	[SerializeField]
	private ActorSelectedContainerStatus _container_status;

	// Token: 0x040029A6 RID: 10662
	[SerializeField]
	private ActorSelectedContainerEquipment _container_equipment;

	// Token: 0x040029A7 RID: 10663
	[SerializeField]
	private RectTransform _avatars_container;

	// Token: 0x040029A8 RID: 10664
	[SerializeField]
	private UiUnitAvatarElement _avatar_prefab;

	// Token: 0x040029A9 RID: 10665
	[SerializeField]
	private UnfoldButton _unfolder;

	// Token: 0x040029AA RID: 10666
	[SerializeField]
	private Image _unfolder_background;

	// Token: 0x040029AB RID: 10667
	[SerializeField]
	private Sprite _unfolder_active;

	// Token: 0x040029AC RID: 10668
	[SerializeField]
	private Sprite _unfolder_inactive;

	// Token: 0x040029AD RID: 10669
	private ObjectPoolGenericMono<UiUnitAvatarElement> _pool_avatars;

	// Token: 0x040029B0 RID: 10672
	private int _last_selection_version;

	// Token: 0x040029B1 RID: 10673
	private List<UiUnitAvatarElement> _showing_avatars = new List<UiUnitAvatarElement>();

	// Token: 0x040029B2 RID: 10674
	private List<int> _stats_version = new List<int>();

	// Token: 0x040029B3 RID: 10675
	private int _offset;
}
