using System;
using UnityEngine;

// Token: 0x02000355 RID: 853
public class UnitSelectionEffect : BaseAnimatedObject
{
	// Token: 0x060020A6 RID: 8358 RVA: 0x001174BB File Offset: 0x001156BB
	internal override void create()
	{
		base.create();
		base.transform.parent = World.world.transform;
		base.transform.name = "unit_selector_effect";
	}

	// Token: 0x060020A7 RID: 8359 RVA: 0x001174E8 File Offset: 0x001156E8
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		bool tVisible = this.visibleCheck();
		this.setVisible(tVisible);
	}

	// Token: 0x060020A8 RID: 8360 RVA: 0x0011750C File Offset: 0x0011570C
	public bool visibleCheck()
	{
		if (!MapBox.isRenderGameplay())
		{
			return false;
		}
		if (World.world.isAnyPowerSelected())
		{
			GodPower tPower = World.world.selected_buttons.selectedButton.godPower;
			if (!tPower.allow_unit_selection && !tPower.show_close_actor)
			{
				return false;
			}
		}
		if (World.world.isBusyWithUI())
		{
			return false;
		}
		Actor tUnit = World.world.getActorNearCursor();
		if (ControllableUnit.isControllingUnit())
		{
			tUnit = null;
		}
		UnitSelectionEffect.setLastActor(tUnit);
		return tUnit != null;
	}

	// Token: 0x060020A9 RID: 8361 RVA: 0x00117584 File Offset: 0x00115784
	public void setVisible(bool pVisible)
	{
		if (pVisible != this._is_visible)
		{
			base.gameObject.SetActive(pVisible);
			if (!pVisible)
			{
				base.transform.position = Globals.POINT_IN_VOID;
				base.transform.localScale = Vector3.one;
				UnitSelectionEffect.setLastActor(null);
			}
		}
		if (pVisible)
		{
			base.transform.position = UnitSelectionEffect.last_actor.current_position;
			base.transform.localScale = UnitSelectionEffect.last_actor.current_scale;
		}
		this._is_visible = pVisible;
	}

	// Token: 0x060020AA RID: 8362 RVA: 0x00117608 File Offset: 0x00115808
	public static void setLastActor(Actor pActor)
	{
		UnitSelectionEffect.last_actor = pActor;
	}

	// Token: 0x040017C7 RID: 6087
	public static Actor last_actor;

	// Token: 0x040017C8 RID: 6088
	private bool _is_visible = true;
}
