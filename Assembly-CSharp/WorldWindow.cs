using System;
using System.Collections.Generic;
using UnityEngine.Events;

// Token: 0x0200061C RID: 1564
public class WorldWindow : TabbedWindow, IInterestingPeopleWindow
{
	// Token: 0x0600334F RID: 13135 RVA: 0x00182A7A File Offset: 0x00180C7A
	protected override void create()
	{
		base.create();
		this.nameInput.addListener(new UnityAction<string>(this.applyInputName));
	}

	// Token: 0x06003350 RID: 13136 RVA: 0x00182A99 File Offset: 0x00180C99
	private void applyInputName(string pInput)
	{
		if (string.IsNullOrEmpty(pInput))
		{
			return;
		}
		World.world.map_stats.name = pInput;
	}

	// Token: 0x06003351 RID: 13137 RVA: 0x00182AB4 File Offset: 0x00180CB4
	private void OnEnable()
	{
		if (World.world.map_stats == null)
		{
			return;
		}
		this.nameInput.setText(World.world.map_stats.name);
	}

	// Token: 0x06003352 RID: 13138 RVA: 0x00182ADD File Offset: 0x00180CDD
	public IEnumerable<Actor> getInterestingUnitsList()
	{
		return World.world.units;
	}

	// Token: 0x040026E6 RID: 9958
	public NameInput nameInput;

	// Token: 0x040026E7 RID: 9959
	public InterestingPeopleTab interesting_people;
}
