using System;
using UnityEngine.Events;

// Token: 0x0200062B RID: 1579
public class AllianceWindow : WindowMetaGeneric<Alliance, AllianceData>
{
	// Token: 0x170002C6 RID: 710
	// (get) Token: 0x0600338C RID: 13196 RVA: 0x00183728 File Offset: 0x00181928
	public override MetaType meta_type
	{
		get
		{
			return MetaType.Alliance;
		}
	}

	// Token: 0x170002C7 RID: 711
	// (get) Token: 0x0600338D RID: 13197 RVA: 0x0018372C File Offset: 0x0018192C
	protected override Alliance meta_object
	{
		get
		{
			return SelectedMetas.selected_alliance;
		}
	}

	// Token: 0x0600338E RID: 13198 RVA: 0x00183733 File Offset: 0x00181933
	protected override void initNameInput()
	{
		base.initNameInput();
		this.mottoInput.addListener(new UnityAction<string>(this.applyInputMotto));
	}

	// Token: 0x0600338F RID: 13199 RVA: 0x00183752 File Offset: 0x00181952
	private void applyInputMotto(string pInput)
	{
		if (pInput == null)
		{
			return;
		}
		if (this.meta_object == null)
		{
			return;
		}
		this.meta_object.data.motto = pInput;
	}

	// Token: 0x06003390 RID: 13200 RVA: 0x00183774 File Offset: 0x00181974
	protected override void showTopPartInformation()
	{
		base.showTopPartInformation();
		Alliance tAlliance = this.meta_object;
		if (tAlliance == null)
		{
			return;
		}
		this.mottoInput.setText(tAlliance.getMotto());
		this.mottoInput.textField.color = tAlliance.getColor().getColorText();
	}

	// Token: 0x06003391 RID: 13201 RVA: 0x001837C0 File Offset: 0x001819C0
	internal override void showStatsRows()
	{
		Alliance tAlliance = this.meta_object;
		base.tryShowPastNames();
		base.showStatRow("founded", tAlliance.getFoundedDate(), MetaType.None, -1L, "iconAge", null, null);
		base.tryToShowActor("alliance_founder", tAlliance.data.founder_actor_id, tAlliance.data.founder_actor_name, null, "actor_traits/iconStupid");
		base.tryToShowMetaKingdom("alliance_founder_kingdom", tAlliance.data.founder_kingdom_id, tAlliance.data.founder_kingdom_name, null);
	}

	// Token: 0x06003392 RID: 13202 RVA: 0x0018383F File Offset: 0x00181A3F
	protected override void OnDisable()
	{
		base.OnDisable();
		this.mottoInput.inputField.DeactivateInputField();
	}

	// Token: 0x04002711 RID: 10001
	public NameInput mottoInput;

	// Token: 0x04002712 RID: 10002
	public StatBar bar_experience;
}
