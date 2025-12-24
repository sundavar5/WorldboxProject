using System;

// Token: 0x020000D0 RID: 208
[Serializable]
public class CombatActionAsset : Asset
{
	// Token: 0x04000709 RID: 1801
	public bool play_unit_attack_sounds;

	// Token: 0x0400070A RID: 1802
	public CombatActionPool[] pools;

	// Token: 0x0400070B RID: 1803
	public string tag_required;

	// Token: 0x0400070C RID: 1804
	public int rate = 1;

	// Token: 0x0400070D RID: 1805
	public float chance = 0.2f;

	// Token: 0x0400070E RID: 1806
	public float cooldown = 1f;

	// Token: 0x0400070F RID: 1807
	public bool is_spell_use;

	// Token: 0x04000710 RID: 1808
	public int cost_stamina;

	// Token: 0x04000711 RID: 1809
	public int cost_mana;

	// Token: 0x04000712 RID: 1810
	public bool basic;

	// Token: 0x04000713 RID: 1811
	public CombatAction action;

	// Token: 0x04000714 RID: 1812
	public CombatActionActor action_actor;

	// Token: 0x04000715 RID: 1813
	public CombatActionActorTargetPosition action_actor_target_position;

	// Token: 0x04000716 RID: 1814
	public CombatActionCheckStart can_do_action;
}
