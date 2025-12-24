using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x0200040E RID: 1038
[RequireComponent(typeof(NameInput))]
public class PlayerNameInput : MonoBehaviour
{
	// Token: 0x060023E7 RID: 9191 RVA: 0x0012BCF9 File Offset: 0x00129EF9
	private void Awake()
	{
		this._name_input = base.GetComponent<NameInput>();
		this._name_input.addListener(new UnityAction<string>(this.inputAction));
	}

	// Token: 0x060023E8 RID: 9192 RVA: 0x0012BD1E File Offset: 0x00129F1E
	private void Update()
	{
		this._name_input.textField.color = World.world.getArchitectMood().getColorText();
	}

	// Token: 0x060023E9 RID: 9193 RVA: 0x0012BD3F File Offset: 0x00129F3F
	private void OnEnable()
	{
		this._name_input.setText(World.world.map_stats.player_name);
	}

	// Token: 0x060023EA RID: 9194 RVA: 0x0012BD5B File Offset: 0x00129F5B
	private void inputAction(string pInput)
	{
		World.world.map_stats.player_name = pInput;
	}

	// Token: 0x040019E0 RID: 6624
	private NameInput _name_input;
}
