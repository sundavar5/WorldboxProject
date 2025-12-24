using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006DA RID: 1754
public class UnitBarsElement : UnitElement
{
	// Token: 0x0600384A RID: 14410 RVA: 0x00194DE4 File Offset: 0x00192FE4
	protected override IEnumerator showContent()
	{
		this.showHappiness();
		this.showHunger();
		this.showStamina();
		this.showMana();
		yield break;
	}

	// Token: 0x0600384B RID: 14411 RVA: 0x00194DF4 File Offset: 0x00192FF4
	private void showMana()
	{
		if (this.actor.asset.force_hide_mana)
		{
			return;
		}
		this._mana.gameObject.SetActive(true);
		int tMax = this.actor.getMaxMana();
		int tValue = Mathf.Clamp(this.actor.getMana(), 0, tMax);
		this._mana.setBar((float)tValue, (float)tMax, "/" + tMax.ToText(4), true, false, true, 0.3f);
	}

	// Token: 0x0600384C RID: 14412 RVA: 0x00194E6C File Offset: 0x0019306C
	private void showStamina()
	{
		if (this.actor.asset.force_hide_stamina)
		{
			return;
		}
		this._stamina.gameObject.SetActive(true);
		int tMax = this.actor.getMaxStamina();
		int tValue = Mathf.Clamp(this.actor.getStamina(), 0, tMax);
		this._stamina.setBar((float)tValue, (float)tMax, "/" + tMax.ToText(4), true, false, true, 0.3f);
	}

	// Token: 0x0600384D RID: 14413 RVA: 0x00194EE4 File Offset: 0x001930E4
	private void showHappiness()
	{
		if (!this.actor.hasEmotions())
		{
			return;
		}
		this._happiness.GetComponentInChildren<HappinessBarIcon>().load(this.actor);
		this._happiness.gameObject.SetActive(true);
		int tPercentValue = this.actor.getHappinessPercent();
		this._happiness.setBar((float)tPercentValue, 100f, "%", true, false, true, 0.3f);
	}

	// Token: 0x0600384E RID: 14414 RVA: 0x00194F54 File Offset: 0x00193154
	private void showHunger()
	{
		if (!this.actor.needsFood())
		{
			return;
		}
		this._hunger.gameObject.SetActive(true);
		int tValue = (int)((float)this.actor.getNutrition() / (float)this.actor.getMaxNutrition() * 100f);
		this._hunger.setBar((float)tValue, 100f, "%", true, false, true, 0.3f);
		if (this.actor.hasFavoriteFood())
		{
			this._favorite_food_bg.gameObject.SetActive(true);
			this._favorite_food_sprite.gameObject.SetActive(true);
			this._favorite_food_sprite.sprite = this.actor.favorite_food_asset.getSpriteIcon();
		}
	}

	// Token: 0x0600384F RID: 14415 RVA: 0x0019500C File Offset: 0x0019320C
	protected override void clear()
	{
		this._mana.gameObject.SetActive(false);
		this._stamina.gameObject.SetActive(false);
		this._hunger.gameObject.SetActive(false);
		this._happiness.gameObject.SetActive(false);
		this._favorite_food_bg.gameObject.SetActive(false);
		this._favorite_food_sprite.gameObject.SetActive(false);
		base.clear();
	}

	// Token: 0x040029CE RID: 10702
	[SerializeField]
	private StatBar _hunger;

	// Token: 0x040029CF RID: 10703
	[SerializeField]
	private StatBar _happiness;

	// Token: 0x040029D0 RID: 10704
	[SerializeField]
	private StatBar _stamina;

	// Token: 0x040029D1 RID: 10705
	[SerializeField]
	private StatBar _mana;

	// Token: 0x040029D2 RID: 10706
	[SerializeField]
	private Image _favorite_food_sprite;

	// Token: 0x040029D3 RID: 10707
	[SerializeField]
	private Image _favorite_food_bg;
}
