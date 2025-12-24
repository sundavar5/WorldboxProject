using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000851 RID: 2129
public class WorldLawsEditor : MonoBehaviour
{
	// Token: 0x06004293 RID: 17043 RVA: 0x001C309F File Offset: 0x001C129F
	private void Awake()
	{
		this.create();
	}

	// Token: 0x06004294 RID: 17044 RVA: 0x001C30A7 File Offset: 0x001C12A7
	private void OnEnable()
	{
		this.updateButtons();
	}

	// Token: 0x06004295 RID: 17045 RVA: 0x001C30AF File Offset: 0x001C12AF
	private void create()
	{
		this.createCategories();
		this.createElements();
	}

	// Token: 0x06004296 RID: 17046 RVA: 0x001C30C0 File Offset: 0x001C12C0
	private void createCategories()
	{
		foreach (WorldLawGroupAsset tGroup in AssetManager.world_law_groups.list)
		{
			Transform tCategoryParent = this._categories_parent;
			WorldLawCategory tCategory = Object.Instantiate<WorldLawCategory>(this._category_prefab, tCategoryParent);
			this._categories_dict.Add(tGroup.id, tCategory);
			tCategory.init(tGroup);
		}
	}

	// Token: 0x06004297 RID: 17047 RVA: 0x001C3140 File Offset: 0x001C1340
	private void createElements()
	{
		foreach (WorldLawAsset tAsset in AssetManager.world_laws_library.list)
		{
			if (!string.IsNullOrEmpty(tAsset.group_id))
			{
				WorldLawCategory tCategory = this._categories_dict[tAsset.group_id];
				WorldLawElement tElement = Object.Instantiate<WorldLawElement>(this._element_prefab, tCategory.grid.transform);
				tElement.name = tAsset.id;
				tElement.init(tAsset);
				tCategory.addElement(tElement);
			}
		}
	}

	// Token: 0x06004298 RID: 17048 RVA: 0x001C31E0 File Offset: 0x001C13E0
	private void updateButtons()
	{
		foreach (WorldLawCategory worldLawCategory in this._categories_dict.Values)
		{
			worldLawCategory.updateButtons();
		}
	}

	// Token: 0x06004299 RID: 17049 RVA: 0x001C3238 File Offset: 0x001C1438
	public void resetToDefault()
	{
		foreach (WorldLawAsset tAsset in AssetManager.world_laws_library.list)
		{
			if (tAsset.can_turn_off)
			{
				PlayerOptionData tOption = tAsset.getOption();
				bool tWasEnabled = tOption.boolVal;
				tOption.boolVal = tAsset.default_state;
				if (tOption.boolVal && !tWasEnabled)
				{
					PlayerOptionAction on_state_enabled = tAsset.on_state_enabled;
					if (on_state_enabled != null)
					{
						on_state_enabled(tOption);
					}
				}
				PlayerOptionAction on_state_change = tAsset.on_state_change;
				if (on_state_change != null)
				{
					on_state_change(tOption);
				}
			}
		}
		World.world.world_laws.updateCaches();
		this.updateButtons();
	}

	// Token: 0x040030C4 RID: 12484
	[SerializeField]
	private WorldLawElement _element_prefab;

	// Token: 0x040030C5 RID: 12485
	[SerializeField]
	private WorldLawCategory _category_prefab;

	// Token: 0x040030C6 RID: 12486
	[SerializeField]
	private Transform _categories_parent;

	// Token: 0x040030C7 RID: 12487
	private Dictionary<string, WorldLawCategory> _categories_dict = new Dictionary<string, WorldLawCategory>();
}
