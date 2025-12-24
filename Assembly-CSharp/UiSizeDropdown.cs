using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007A6 RID: 1958
public class UiSizeDropdown : MonoBehaviour
{
	// Token: 0x06003DF8 RID: 15864 RVA: 0x001B06FD File Offset: 0x001AE8FD
	private void Start()
	{
		this.createDropdownOptions();
		this.renderDropdownValue(this.dropdown);
		this.dropdown.onValueChanged.AddListener(delegate(int <p0>)
		{
			this.DropdownValueChanged(this.dropdown);
		});
	}

	// Token: 0x06003DF9 RID: 15865 RVA: 0x001B072D File Offset: 0x001AE92D
	private void createDropdownOptions()
	{
		this.dropdown = base.GetComponent<Dropdown>();
		this.dropdown.ClearOptions();
		this.options.Clear();
		this.dropdown.AddOptions(this.options);
	}

	// Token: 0x06003DFA RID: 15866 RVA: 0x001B0762 File Offset: 0x001AE962
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this.dropdown = base.GetComponent<Dropdown>();
		this.renderDropdownValue(this.dropdown);
	}

	// Token: 0x06003DFB RID: 15867 RVA: 0x001B0784 File Offset: 0x001AE984
	private void DropdownValueChanged(Dropdown change)
	{
	}

	// Token: 0x06003DFC RID: 15868 RVA: 0x001B0788 File Offset: 0x001AE988
	private void renderDropdownValue(Dropdown dropdown)
	{
		string currentValue = PlayerConfig.dict["ui_size"].stringVal;
		dropdown.value = this.options.IndexOf(currentValue);
		dropdown.RefreshShownValue();
	}

	// Token: 0x04002CFF RID: 11519
	private Button button;

	// Token: 0x04002D00 RID: 11520
	private Dropdown dropdown;

	// Token: 0x04002D01 RID: 11521
	private List<string> options = new List<string>();
}
