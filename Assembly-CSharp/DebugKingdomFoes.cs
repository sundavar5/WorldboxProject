using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005B4 RID: 1460
public class DebugKingdomFoes : MonoBehaviour
{
	// Token: 0x0600303A RID: 12346 RVA: 0x00175674 File Offset: 0x00173874
	private void Awake()
	{
		this.create();
	}

	// Token: 0x0600303B RID: 12347 RVA: 0x0017567C File Offset: 0x0017387C
	private void create()
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		AssetManager.kingdoms.checkForMissingTags();
		foreach (KingdomAsset tKingdomAsset in AssetManager.kingdoms.list)
		{
			if (!tKingdomAsset.isTemplateAsset())
			{
				Transform tTransformTarget;
				if (tKingdomAsset.group_main)
				{
					tTransformTarget = this._grid_main.transform;
				}
				else if (tKingdomAsset.group_creeps)
				{
					tTransformTarget = this._grid_creeps.transform;
				}
				else if (tKingdomAsset.concept)
				{
					tTransformTarget = this._grid_concepts.transform;
				}
				else if (tKingdomAsset.is_forced_by_trait)
				{
					tTransformTarget = this._grid_others.transform;
				}
				else if (tKingdomAsset.group_minicivs_cool)
				{
					tTransformTarget = this._grid_minicivs_special.transform;
				}
				else if (tKingdomAsset.group_miniciv)
				{
					tTransformTarget = this._grid_minicivs.transform;
				}
				else if (tKingdomAsset.civ)
				{
					tTransformTarget = this._grid_civs.transform;
				}
				else if (tKingdomAsset.mobs)
				{
					tTransformTarget = this._grid_mobs.transform;
				}
				else
				{
					tTransformTarget = this._grid_others.transform;
				}
				DebugKingdomButton tNewButton = Object.Instantiate<DebugKingdomButton>(this._prefab_button, tTransformTarget);
				tNewButton.setAsset(tKingdomAsset);
				this._buttons.Add(tNewButton);
				tNewButton.GetComponent<Button>().onClick.AddListener(delegate()
				{
					this.select(tNewButton);
				});
			}
		}
		this.select(this._buttons.GetRandom<DebugKingdomButton>());
	}

	// Token: 0x0600303C RID: 12348 RVA: 0x00175834 File Offset: 0x00173A34
	private void select(DebugKingdomButton pButton)
	{
		this._current_selected = pButton.kingdom_asset;
		this._selector.transform.position = pButton.transform.position;
		this.updateButtons();
	}

	// Token: 0x0600303D RID: 12349 RVA: 0x00175864 File Offset: 0x00173A64
	private void updateButtons()
	{
		foreach (DebugKingdomButton debugKingdomButton in this._buttons)
		{
			debugKingdomButton.checkSelected(this._current_selected);
		}
	}

	// Token: 0x0400245B RID: 9307
	[SerializeField]
	private DebugKingdomButton _prefab_button;

	// Token: 0x0400245C RID: 9308
	[SerializeField]
	private Image _selector;

	// Token: 0x0400245D RID: 9309
	[SerializeField]
	private GridLayoutGroup _grid_main;

	// Token: 0x0400245E RID: 9310
	[SerializeField]
	private GridLayoutGroup _grid_civs;

	// Token: 0x0400245F RID: 9311
	[SerializeField]
	private GridLayoutGroup _grid_minicivs;

	// Token: 0x04002460 RID: 9312
	[SerializeField]
	private GridLayoutGroup _grid_minicivs_special;

	// Token: 0x04002461 RID: 9313
	[SerializeField]
	private GridLayoutGroup _grid_concepts;

	// Token: 0x04002462 RID: 9314
	[SerializeField]
	private GridLayoutGroup _grid_mobs;

	// Token: 0x04002463 RID: 9315
	[SerializeField]
	private GridLayoutGroup _grid_creeps;

	// Token: 0x04002464 RID: 9316
	[SerializeField]
	private GridLayoutGroup _grid_others;

	// Token: 0x04002465 RID: 9317
	private List<DebugKingdomButton> _buttons = new List<DebugKingdomButton>();

	// Token: 0x04002466 RID: 9318
	private KingdomAsset _current_selected;

	// Token: 0x04002467 RID: 9319
	private bool _initialized;
}
