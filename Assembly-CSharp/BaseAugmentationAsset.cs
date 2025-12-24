using System;
using System.Collections.Generic;
using System.ComponentModel;

// Token: 0x020000A2 RID: 162
[Serializable]
public class BaseAugmentationAsset : BaseUnlockableAsset
{
	// Token: 0x17000018 RID: 24
	// (get) Token: 0x0600055C RID: 1372 RVA: 0x00052E3C File Offset: 0x0005103C
	// (set) Token: 0x0600055D RID: 1373 RVA: 0x00052E44 File Offset: 0x00051044
	public List<string> spells_ids { get; set; }

	// Token: 0x0600055E RID: 1374 RVA: 0x00052E4D File Offset: 0x0005104D
	public bool hasDecisions()
	{
		return this.decisions_assets != null;
	}

	// Token: 0x0600055F RID: 1375 RVA: 0x00052E58 File Offset: 0x00051058
	public bool hasCombatActions()
	{
		return this.combat_actions != null;
	}

	// Token: 0x06000560 RID: 1376 RVA: 0x00052E63 File Offset: 0x00051063
	public bool hasSpells()
	{
		return this.spells != null;
	}

	// Token: 0x06000561 RID: 1377 RVA: 0x00052E6E File Offset: 0x0005106E
	public void addDecision(string pID)
	{
		if (this.decision_ids == null)
		{
			this.decision_ids = new List<string>();
		}
		this.decision_ids.Add(pID);
	}

	// Token: 0x06000562 RID: 1378 RVA: 0x00052E8F File Offset: 0x0005108F
	public void addSpell(string pSpell)
	{
		if (this.spells_ids == null)
		{
			this.spells_ids = new List<string>();
		}
		this.spells_ids.Add(pSpell);
	}

	// Token: 0x06000563 RID: 1379 RVA: 0x00052EB0 File Offset: 0x000510B0
	public void addCombatAction(string pCombatActionID)
	{
		if (this.combat_actions_ids == null)
		{
			this.combat_actions_ids = new List<string>();
		}
		this.combat_actions_ids.Add(pCombatActionID);
	}

	// Token: 0x06000564 RID: 1380 RVA: 0x00052ED1 File Offset: 0x000510D1
	public void linkCombatActions()
	{
		if (this.combat_actions_ids == null || this.combat_actions_ids.Count == 0)
		{
			return;
		}
		this.combat_actions = new CombatActionHolder();
		this.combat_actions.fillFromIDS(this.combat_actions_ids);
	}

	// Token: 0x06000565 RID: 1381 RVA: 0x00052F08 File Offset: 0x00051108
	public void linkSpells()
	{
		if (this.spells_ids == null || this.spells_ids.Count == 0)
		{
			return;
		}
		this.spells = new List<SpellAsset>();
		foreach (string tId in this.spells_ids)
		{
			SpellAsset tSpellAsset = AssetManager.spells.get(tId);
			if (tSpellAsset != null)
			{
				this.spells.Add(tSpellAsset);
			}
		}
	}

	// Token: 0x06000566 RID: 1382 RVA: 0x00052F90 File Offset: 0x00051190
	public virtual BaseCategoryAsset getGroup()
	{
		throw new NotImplementedException();
	}

	// Token: 0x040005AC RID: 1452
	[DefaultValue(true)]
	public bool can_be_given = true;

	// Token: 0x040005AD RID: 1453
	[DefaultValue(true)]
	public bool can_be_removed = true;

	// Token: 0x040005AE RID: 1454
	[DefaultValue(true)]
	public bool show_in_meta_editor = true;

	// Token: 0x040005AF RID: 1455
	public WorldActionTrait action_on_object_remove;

	// Token: 0x040005B0 RID: 1456
	public WorldAction action_special_effect;

	// Token: 0x040005B1 RID: 1457
	public WorldActionTrait action_on_augmentation_add;

	// Token: 0x040005B2 RID: 1458
	public WorldActionTrait action_on_augmentation_remove;

	// Token: 0x040005B3 RID: 1459
	public WorldActionTrait action_on_augmentation_load;

	// Token: 0x040005B4 RID: 1460
	[DefaultValue(1f)]
	public float special_effect_interval = 1f;

	// Token: 0x040005B5 RID: 1461
	public AttackAction action_attack_target;

	// Token: 0x040005B6 RID: 1462
	public string group_id;

	// Token: 0x040005B7 RID: 1463
	public int priority;

	// Token: 0x040005B8 RID: 1464
	public string special_locale_id;

	// Token: 0x040005B9 RID: 1465
	[NonSerialized]
	public CombatActionHolder combat_actions;

	// Token: 0x040005BA RID: 1466
	[NonSerialized]
	public List<SpellAsset> spells;

	// Token: 0x040005BC RID: 1468
	public List<string> combat_actions_ids;

	// Token: 0x040005BD RID: 1469
	[NonSerialized]
	public DecisionAsset[] decisions_assets;

	// Token: 0x040005BE RID: 1470
	public List<string> decision_ids;
}
