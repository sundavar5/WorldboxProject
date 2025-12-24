using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006E5 RID: 1765
public class UnitStatsElement : UnitElement, IStatsElement, IRefreshElement
{
	// Token: 0x060038A6 RID: 14502 RVA: 0x00195C94 File Offset: 0x00193E94
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		this._stats_icons.setIconValue(pName, pMainVal, pMax, pColor, pFloat, pEnding, pSeparator);
	}

	// Token: 0x060038A7 RID: 14503 RVA: 0x00195CAC File Offset: 0x00193EAC
	protected override void Awake()
	{
		this._stats_icons = base.gameObject.AddOrGetComponent<StatsIconContainer>();
		base.Awake();
	}

	// Token: 0x060038A8 RID: 14504 RVA: 0x00195CC5 File Offset: 0x00193EC5
	protected override IEnumerator showContent()
	{
		if (this.actor == null)
		{
			yield break;
		}
		if (!this.actor.isAlive())
		{
			yield break;
		}
		this.actor.updateStats();
		if (this.actor.asset.inspect_stats)
		{
			this.showAttribute("i_diplomacy", (int)this.actor.stats["diplomacy"]);
			this.showAttribute("i_stewardship", (int)this.actor.stats["stewardship"]);
			this.showAttribute("i_intelligence", (int)this.actor.stats["intelligence"]);
			this.showAttribute("i_warfare", (int)this.actor.stats["warfare"]);
			int tMaxDamage = (int)this.actor.stats["damage"];
			int tMinDamage = (int)((float)tMaxDamage * this.actor.stats["damage_range"]);
			this.setIconValue("i_damage", (float)tMinDamage, new float?((float)tMaxDamage), "", false, "", '-');
			this.setIconValue("i_armor", this.actor.stats["armor"], null, "", false, "%", '/');
			this.setIconValue("i_speed", this.actor.stats["speed"], null, "", false, "", '/');
			this.setIconValue("i_critical_chance", this.actor.stats["critical_chance"] * 100f, null, "", false, "%", '/');
			this.setIconValue("i_attack_speed", this.actor.stats["attack_speed"], null, "", true, "", '/');
		}
		if (this.actor.asset.inspect_kills)
		{
			this.setIconValue("i_kills", (float)this.actor.data.kills, null, "", false, "", '/');
		}
		if (this.actor.asset.inspect_children)
		{
			this.setIconValue("i_births", (float)this.actor.data.births, null, "", false, "", '/');
			this.setIconValue("i_children", (float)this.actor.current_children_count, new float?((float)this.actor.getMaxOffspring()), "", false, "", '/');
		}
		this.setIconValue("i_renown", (float)this.actor.data.renown, null, "", false, "", '/');
		if (this.actor.asset.inspect_experience)
		{
			bool flag = this.actor.hasTrait("immortal");
			StatsIcon tLifespan = this._stats_icons.getIconViaId("i_lifespan");
			if (flag)
			{
				this._stats_icons.setText("i_lifespan", "???", "#F3961F");
			}
			else
			{
				tLifespan.enable_animation = true;
				this.setIconValue("i_lifespan", this.actor.stats["lifespan"], null, "", false, "", '/');
			}
			this.setIconValue("i_level", (float)this.actor.data.level, null, "", false, "", '/');
			this.setIconValue("i_experience", (float)this.actor.data.experience, new float?((float)this.actor.getExpToLevelup()), "", false, "", '/');
		}
		Sprite tSpeciesSprite = this.actor.asset.getSpriteIcon();
		if (tSpeciesSprite == null || this.actor.asset.is_boat)
		{
			tSpeciesSprite = this._default_creature_icon;
		}
		this._icon_species.sprite = tSpeciesSprite;
		if (this.actor.asset.inspect_sex)
		{
			if (this.actor.isSexMale())
			{
				this._icon_sex.sprite = SpriteTextureLoader.getSprite("ui/icons/IconMale");
			}
			else
			{
				this._icon_sex.sprite = SpriteTextureLoader.getSprite("ui/icons/IconFemale");
			}
		}
		else
		{
			this._icon_sex.sprite = tSpeciesSprite;
		}
		yield break;
	}

	// Token: 0x060038A9 RID: 14505 RVA: 0x00195CD4 File Offset: 0x00193ED4
	private void showAttribute(string pName, int pValue)
	{
		StatsIcon tIcon;
		this._stats_icons.TryGetValue(pName, out tIcon);
		if (tIcon == null)
		{
			return;
		}
		tIcon.gameObject.SetActive(true);
		if (pValue < 4)
		{
			tIcon.setValue((float)pValue, null, "#FB2C21", false, "", '/', false);
			return;
		}
		if (pValue >= 20)
		{
			tIcon.setValue((float)pValue, null, "#43FF43", false, "", '/', false);
			return;
		}
		tIcon.setValue((float)pValue);
	}

	// Token: 0x060038AB RID: 14507 RVA: 0x00195D5F File Offset: 0x00193F5F
	GameObject IStatsElement.get_gameObject()
	{
		return base.gameObject;
	}

	// Token: 0x04002A06 RID: 10758
	[SerializeField]
	private Image _icon_sex;

	// Token: 0x04002A07 RID: 10759
	[SerializeField]
	private Image _icon_species;

	// Token: 0x04002A08 RID: 10760
	[SerializeField]
	private Sprite _default_creature_icon;

	// Token: 0x04002A09 RID: 10761
	private StatsIconContainer _stats_icons;
}
