using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006D4 RID: 1748
public class PossessionUnitInfo : MonoBehaviour
{
	// Token: 0x0600380F RID: 14351 RVA: 0x001934D0 File Offset: 0x001916D0
	private void OnEnable()
	{
		Actor tUnit = ControllableUnit.getControllableUnit();
		if (tUnit == null)
		{
			return;
		}
		this.showForUnit(tUnit);
	}

	// Token: 0x06003810 RID: 14352 RVA: 0x001934F0 File Offset: 0x001916F0
	private void Update()
	{
		Actor tUnit = ControllableUnit.getControllableUnit();
		if (tUnit == null)
		{
			return;
		}
		this.showForUnit(tUnit);
	}

	// Token: 0x06003811 RID: 14353 RVA: 0x00193510 File Offset: 0x00191710
	private void showForUnit(Actor pActor)
	{
		if (pActor.isSexMale())
		{
			this._icon_sex.sprite = SpriteTextureLoader.getSprite("ui/icons/IconMale");
		}
		else
		{
			this._icon_sex.sprite = SpriteTextureLoader.getSprite("ui/icons/IconFemale");
		}
		this._icon_species.sprite = pActor.asset.getSpriteIcon();
		if (pActor.kingdom.isCiv())
		{
			this._banner_kingdom.gameObject.SetActive(true);
			this._banner_kingdom.load(pActor.kingdom);
		}
		else
		{
			this._banner_kingdom.gameObject.SetActive(false);
		}
		float tHealthBarCur = (float)pActor.getHealth();
		float tHealthMax = (float)pActor.getMaxHealth();
		this._bar_health.setBar(tHealthBarCur, tHealthMax, "/" + ((int)tHealthMax).ToText(4), false, false, true, 0.25f);
		this._name_field.text = pActor.getName();
		this._name_field.color = pActor.kingdom.getColor().getColorText();
		this._text_age.text = pActor.getAge().ToString();
		this._text_kills.text = pActor.data.kills.ToString();
		this._text_level.text = pActor.level.ToString();
	}

	// Token: 0x04002999 RID: 10649
	[SerializeField]
	private Text _name_field;

	// Token: 0x0400299A RID: 10650
	[SerializeField]
	private Image _icon_species;

	// Token: 0x0400299B RID: 10651
	[SerializeField]
	private Image _icon_sex;

	// Token: 0x0400299C RID: 10652
	[SerializeField]
	private KingdomBanner _banner_kingdom;

	// Token: 0x0400299D RID: 10653
	[SerializeField]
	private Text _text_age;

	// Token: 0x0400299E RID: 10654
	[SerializeField]
	private Text _text_kills;

	// Token: 0x0400299F RID: 10655
	[SerializeField]
	private Text _text_level;

	// Token: 0x040029A0 RID: 10656
	[SerializeField]
	private StatBar _bar_health;
}
