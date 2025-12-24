using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020006CC RID: 1740
public class HappinessBarIcon : MonoBehaviour
{
	// Token: 0x060037C9 RID: 14281 RVA: 0x00191C11 File Offset: 0x0018FE11
	private void Awake()
	{
		base.GetComponentInParent<StatBar>().addCallback(new StatBarUpdated(this.barUpdated));
	}

	// Token: 0x060037CA RID: 14282 RVA: 0x00191C2A File Offset: 0x0018FE2A
	public void load(Actor pActor)
	{
		this._actor = pActor;
	}

	// Token: 0x060037CB RID: 14283 RVA: 0x00191C34 File Offset: 0x0018FE34
	private void barUpdated(float pValue, float pMax)
	{
		if (this._actor.isRekt())
		{
			return;
		}
		Sprite tSprite = HappinessHelper.getSpriteBasedOnHappinessValue(this._actor.getHappiness());
		this._icon.sprite = tSprite;
	}

	// Token: 0x060037CC RID: 14284 RVA: 0x00191C6C File Offset: 0x0018FE6C
	private void OnDisable()
	{
		this._actor = null;
	}

	// Token: 0x0400295B RID: 10587
	[SerializeField]
	private Image _icon;

	// Token: 0x0400295C RID: 10588
	private Actor _actor;
}
