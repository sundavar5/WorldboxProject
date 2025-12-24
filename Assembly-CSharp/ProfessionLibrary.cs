using System;
using System.Collections.Generic;

// Token: 0x02000072 RID: 114
[Serializable]
public class ProfessionLibrary : AssetLibrary<ProfessionAsset>
{
	// Token: 0x06000438 RID: 1080 RVA: 0x0002CA18 File Offset: 0x0002AC18
	public override void init()
	{
		base.init();
		this.add(new ProfessionAsset
		{
			id = "nothing",
			profession_id = UnitProfession.Nothing
		});
		this.add(new ProfessionAsset
		{
			id = "unit",
			profession_id = UnitProfession.Unit,
			is_civilian = true
		});
		this.add(new ProfessionAsset
		{
			id = "warrior",
			profession_id = UnitProfession.Warrior,
			can_capture = true,
			cancel_when_no_city = true
		});
		this.t.addDecision("warrior_try_join_army_group");
		this.t.addDecision("check_warrior_limit");
		this.t.addDecision("city_walking_to_danger_zone");
		this.t.addDecision("warrior_army_captain_idle_walking_city");
		this.t.addDecision("warrior_army_captain_waiting");
		this.t.addDecision("warrior_army_leader_move_random");
		this.t.addDecision("warrior_army_leader_move_to_attack_target");
		this.t.addDecision("warrior_army_follow_leader");
		this.t.addDecision("warrior_random_move");
		this.t.addDecision("check_warrior_transport");
		this.t.addDecision("warrior_train_with_dummy");
		this.add(new ProfessionAsset
		{
			id = "king",
			profession_id = UnitProfession.King,
			can_capture = true
		});
		this.t.addDecision("king_check_new_city_foundation");
		this.t.addDecision("king_change_kingdom_language");
		this.t.addDecision("king_change_kingdom_culture");
		this.t.addDecision("king_change_kingdom_religion");
		this.t.addDecision("claim_land");
		this.add(new ProfessionAsset
		{
			id = "leader",
			profession_id = UnitProfession.Leader,
			can_capture = true
		});
		this.t.addDecision("leader_change_city_language");
		this.t.addDecision("leader_change_city_culture");
		this.t.addDecision("leader_change_city_religion");
		this.t.addDecision("claim_land");
	}

	// Token: 0x06000439 RID: 1081 RVA: 0x0002CC24 File Offset: 0x0002AE24
	public override void linkAssets()
	{
		base.linkAssets();
		this.linkDecisions();
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x0002CC34 File Offset: 0x0002AE34
	private void linkDecisions()
	{
		foreach (ProfessionAsset professionAsset in this.list)
		{
			professionAsset.linkDecisions();
		}
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x0002CC84 File Offset: 0x0002AE84
	public override ProfessionAsset add(ProfessionAsset pAsset)
	{
		this._dict_profession_id.Add(pAsset.profession_id, pAsset);
		return base.add(pAsset);
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x0002CC9F File Offset: 0x0002AE9F
	public virtual ProfessionAsset get(UnitProfession pID)
	{
		return this._dict_profession_id[pID];
	}

	// Token: 0x04000359 RID: 857
	public static readonly UnitProfession[] list_enum_profession_ids = (UnitProfession[])Enum.GetValues(typeof(UnitProfession));

	// Token: 0x0400035A RID: 858
	private Dictionary<UnitProfession, ProfessionAsset> _dict_profession_id = new Dictionary<UnitProfession, ProfessionAsset>();
}
