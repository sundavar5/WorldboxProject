using System;
using System.Collections.Generic;
using ai.behaviours;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000754 RID: 1876
public class SelectedMetaUnitElement : MonoBehaviour
{
	// Token: 0x06003B56 RID: 15190 RVA: 0x001A0668 File Offset: 0x0019E868
	private void Awake()
	{
		foreach (StatsIcon tStatsIcon in base.GetComponentsInChildren<StatsIcon>(true))
		{
			if (!this._stats_icons.TryAdd(tStatsIcon.name, tStatsIcon))
			{
				Debug.LogError("Duplicate icon name! " + tStatsIcon.name);
			}
		}
	}

	// Token: 0x06003B57 RID: 15191 RVA: 0x001A06B8 File Offset: 0x0019E8B8
	public void show(Actor pActor, string pLocaleKey)
	{
		this._actor = pActor;
		this._avatar.show(this._actor);
		if (string.IsNullOrEmpty(pLocaleKey))
		{
			this._title.text = this._actor.getName();
		}
		else
		{
			string tLocalized = LocalizedTextManager.getText(pLocaleKey, null, false);
			this._title.text = tLocalized.Replace("$unit$", this._actor.getName());
		}
		this._title.color = this._actor.kingdom.getColor().getColorText();
		if (this._actor.isSexMale())
		{
			this._icon_sex.sprite = SpriteTextureLoader.getSprite("ui/icons/IconMale");
		}
		else
		{
			this._icon_sex.sprite = SpriteTextureLoader.getSprite("ui/icons/IconFemale");
		}
		this._icon_species.sprite = this._actor.getActorAsset().getSprite();
	}

	// Token: 0x06003B58 RID: 15192 RVA: 0x001A079C File Offset: 0x0019E99C
	public void updateBarAndTask(Actor pActor)
	{
		float tHealthBarCur = (float)pActor.getHealth();
		float tHealthMax = (float)pActor.getMaxHealth();
		this._bar_health.setBar(tHealthBarCur, tHealthMax, "/" + ((int)tHealthMax).ToText(4), false, false, true, 0.25f);
		BehaviourTaskActor tTask = pActor.ai.task;
		string tTaskText = pActor.getTaskText();
		Sprite tIcon;
		if (tTask == null)
		{
			tIcon = this._no_task_icon;
		}
		else
		{
			tIcon = tTask.getSprite();
		}
		this._task_icon_left.sprite = tIcon;
		this._task_icon_right.sprite = tIcon;
		this._task_title.text = tTaskText;
	}

	// Token: 0x06003B59 RID: 15193 RVA: 0x001A082C File Offset: 0x0019EA2C
	public void showStats(Actor pActor)
	{
		int tMaxDamage = (int)pActor.stats["damage"];
		int tMinDamage = (int)((float)tMaxDamage * pActor.stats["damage_range"]);
		this.setIconValue("i_age", (float)pActor.data.getAge(), null, "", false, "", '/');
		this.setIconValue("i_damage", (float)tMinDamage, new float?((float)tMaxDamage), "", false, "", '-');
		this.setIconValue("i_armor", pActor.stats["armor"], null, "", false, "%", '/');
		this.setIconValue("i_kills", (float)pActor.data.kills, null, "", false, "", '/');
		this.setIconValue("i_renown", (float)pActor.data.renown, null, "", false, "", '/');
		this.setIconValue("i_level", (float)pActor.data.level, null, "", false, "", '/');
		this.setIconValue("i_experience", (float)pActor.data.experience, new float?((float)pActor.getExpToLevelup()), "", false, "", '/');
		this.setIconValue("i_money", (float)pActor.money, null, "", false, "", '/');
		this.setIconValue("i_loot", (float)pActor.loot, null, "", false, "", '/');
	}

	// Token: 0x06003B5A RID: 15194 RVA: 0x001A09E8 File Offset: 0x0019EBE8
	public void setIconValue(string pName, float pMainVal, float? pMax = null, string pColor = "", bool pFloat = false, string pEnding = "", char pSeparator = '/')
	{
		StatsIcon tIcon = this.getIconViaId(pName);
		if (tIcon == null)
		{
			return;
		}
		if (tIcon.areValuesTooClose(pMainVal))
		{
			return;
		}
		tIcon.setValue(pMainVal, pMax, pColor, pFloat, pEnding, pSeparator, false);
	}

	// Token: 0x06003B5B RID: 15195 RVA: 0x001A0A24 File Offset: 0x0019EC24
	public StatsIcon getIconViaId(string pName)
	{
		StatsIcon tIcon;
		this._stats_icons.TryGetValue(pName, out tIcon);
		if (tIcon == null)
		{
			return null;
		}
		tIcon.gameObject.SetActive(true);
		return tIcon;
	}

	// Token: 0x06003B5C RID: 15196 RVA: 0x001A0A58 File Offset: 0x0019EC58
	public void spawnAvatarText()
	{
		this._unit_texts.spawnAvatarText(this._actor);
	}

	// Token: 0x06003B5D RID: 15197 RVA: 0x001A0A6B File Offset: 0x0019EC6B
	public UiUnitAvatarElement getAvatar()
	{
		return this._avatar;
	}

	// Token: 0x04002B9B RID: 11163
	[SerializeField]
	private UiUnitAvatarElement _avatar;

	// Token: 0x04002B9C RID: 11164
	[SerializeField]
	private Text _title;

	// Token: 0x04002B9D RID: 11165
	[SerializeField]
	private UnitTextManager _unit_texts;

	// Token: 0x04002B9E RID: 11166
	[SerializeField]
	private Image _icon_sex;

	// Token: 0x04002B9F RID: 11167
	[SerializeField]
	private Image _icon_species;

	// Token: 0x04002BA0 RID: 11168
	[SerializeField]
	private Image _task_icon_left;

	// Token: 0x04002BA1 RID: 11169
	[SerializeField]
	private Image _task_icon_right;

	// Token: 0x04002BA2 RID: 11170
	[SerializeField]
	private Text _task_title;

	// Token: 0x04002BA3 RID: 11171
	[SerializeField]
	private Sprite _no_task_icon;

	// Token: 0x04002BA4 RID: 11172
	[SerializeField]
	private StatBar _bar_health;

	// Token: 0x04002BA5 RID: 11173
	private Dictionary<string, StatsIcon> _stats_icons = new Dictionary<string, StatsIcon>();

	// Token: 0x04002BA6 RID: 11174
	private Actor _actor;
}
