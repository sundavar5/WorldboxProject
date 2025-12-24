using System;
using System.Collections.Generic;
using LayoutGroupExt;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000641 RID: 1601
public class BaseAugmentationsEditor : MonoBehaviour
{
	// Token: 0x06003432 RID: 13362 RVA: 0x0018551C File Offset: 0x0018371C
	private void Awake()
	{
		this.create();
		this._stats_window = base.GetComponentInParent<StatsWindow>();
		RainSwitcherButton rainSwitcherButton = this.rain_state_switcher;
		if (rainSwitcherButton == null)
		{
			return;
		}
		rainSwitcherButton.getButton().onClick.AddListener(delegate()
		{
			this.rain_state_toggle_action();
		});
	}

	// Token: 0x06003433 RID: 13363 RVA: 0x00185556 File Offset: 0x00183756
	protected virtual void OnEnable()
	{
		this.reloadButtons();
		this.checkEnabledGroups();
		if (!this.rain_editor)
		{
			this._stats_window.updateStats();
		}
	}

	// Token: 0x06003434 RID: 13364 RVA: 0x00185577 File Offset: 0x00183777
	protected virtual void create()
	{
		if (this._created)
		{
			return;
		}
		this._created = true;
	}

	// Token: 0x06003435 RID: 13365 RVA: 0x00185589 File Offset: 0x00183789
	protected virtual void onEnableRain()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003436 RID: 13366 RVA: 0x00185590 File Offset: 0x00183790
	public virtual void reloadButtons()
	{
		if (!base.gameObject.activeInHierarchy)
		{
			return;
		}
		this.loadAugmentationGroups();
	}

	// Token: 0x06003437 RID: 13367 RVA: 0x001855A6 File Offset: 0x001837A6
	protected virtual void showActiveButtons()
	{
		throw new NotImplementedException();
	}

	// Token: 0x06003438 RID: 13368 RVA: 0x001855AD File Offset: 0x001837AD
	private void loadAugmentationGroups()
	{
		if (this._groups_initialized)
		{
			return;
		}
		this._groups_initialized = true;
		this.groupsBuilder();
	}

	// Token: 0x06003439 RID: 13369 RVA: 0x001855C5 File Offset: 0x001837C5
	protected virtual void checkEnabledGroups()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600343A RID: 13370 RVA: 0x001855CC File Offset: 0x001837CC
	protected virtual void groupsBuilder()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600343B RID: 13371 RVA: 0x001855D3 File Offset: 0x001837D3
	protected virtual void startSignal()
	{
	}

	// Token: 0x0600343C RID: 13372 RVA: 0x001855D5 File Offset: 0x001837D5
	protected virtual void onNanoWasModified()
	{
		throw new NotImplementedException();
	}

	// Token: 0x0600343D RID: 13373 RVA: 0x001855DC File Offset: 0x001837DC
	protected virtual void toggleRainState(ref RainState pState)
	{
		if (pState == RainState.Add)
		{
			pState = RainState.Remove;
			this.rain_state_switcher.toggleState(true);
		}
		else
		{
			pState = RainState.Add;
			this.rain_state_switcher.toggleState(false);
		}
		this.rain_editor_state = pState;
		this.reloadButtons();
	}

	// Token: 0x0400275D RID: 10077
	public Transform augmentation_groups_parent;

	// Token: 0x0400275E RID: 10078
	public Text text_counter_augmentations;

	// Token: 0x0400275F RID: 10079
	public LocalizedText window_title_text;

	// Token: 0x04002760 RID: 10080
	public Image power_icon;

	// Token: 0x04002761 RID: 10081
	public Transform powers_icons;

	// Token: 0x04002762 RID: 10082
	public GridLayoutGroupExtended selected_editor_augmentations_grid;

	// Token: 0x04002763 RID: 10083
	public RainSwitcherButton rain_state_switcher;

	// Token: 0x04002764 RID: 10084
	protected List<string> augmentations_list_link;

	// Token: 0x04002765 RID: 10085
	protected readonly HashSet<string> augmentations_hashset = new HashSet<string>();

	// Token: 0x04002766 RID: 10086
	public bool rain_editor;

	// Token: 0x04002767 RID: 10087
	public RainState rain_editor_state;

	// Token: 0x04002768 RID: 10088
	private bool _groups_initialized;

	// Token: 0x04002769 RID: 10089
	private bool _created;

	// Token: 0x0400276A RID: 10090
	private StatsWindow _stats_window;

	// Token: 0x0400276B RID: 10091
	protected ToggleRainStateAction rain_state_toggle_action;
}
