using System;

// Token: 0x0200068B RID: 1675
public interface IEquipmentWindow : IAugmentationsWindow<IEquipmentEditor>
{
	// Token: 0x060035B9 RID: 13753 RVA: 0x0018987C File Offset: 0x00187A7C
	void reloadEquipment()
	{
		this.GetComponentInChildren<UnitEquipmentContainer>(false).reloadEquipment(false);
	}

	// Token: 0x060035BA RID: 13754
	void checkEquipmentTabIcon();
}
