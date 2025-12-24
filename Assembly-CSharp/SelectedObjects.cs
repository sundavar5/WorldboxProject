using System;

// Token: 0x0200043F RID: 1087
public static class SelectedObjects
{
	// Token: 0x060025B8 RID: 9656 RVA: 0x00136FED File Offset: 0x001351ED
	public static void unselectNanoObject()
	{
		SelectedObjects._selected_nano_object = null;
		PowerTabController.prev_selected_meta_id = null;
	}

	// Token: 0x060025B9 RID: 9657 RVA: 0x00136FFB File Offset: 0x001351FB
	public static bool isNanoObjectSelected(NanoObject pNanoObject)
	{
		return SelectedObjects.isNanoObjectSet() && pNanoObject == SelectedObjects._selected_nano_object;
	}

	// Token: 0x060025BA RID: 9658 RVA: 0x0013700E File Offset: 0x0013520E
	public static bool isNanoObjectSet()
	{
		return !SelectedObjects._selected_nano_object.isRekt();
	}

	// Token: 0x060025BB RID: 9659 RVA: 0x0013701D File Offset: 0x0013521D
	public static void setNanoObject(NanoObject pNanoObject)
	{
		SelectedObjects._selected_nano_object = pNanoObject;
		SoundBox.click();
	}

	// Token: 0x060025BC RID: 9660 RVA: 0x0013702A File Offset: 0x0013522A
	public static NanoObject getSelectedNanoObject()
	{
		return SelectedObjects._selected_nano_object;
	}

	// Token: 0x04001CB2 RID: 7346
	private static NanoObject _selected_nano_object;
}
