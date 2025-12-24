using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020007A2 RID: 1954
[Obsolete]
public class ResolutionDropdown : MonoBehaviour
{
	// Token: 0x06003DE2 RID: 15842 RVA: 0x001B0410 File Offset: 0x001AE610
	private void Start()
	{
		this.dropdown = base.GetComponent<Dropdown>();
		this.PopulateDropdown(this.dropdown);
		this.dropdown.onValueChanged.AddListener(delegate(int <p0>)
		{
			this.DropdownValueChanged(this.dropdown);
		});
	}

	// Token: 0x06003DE3 RID: 15843 RVA: 0x001B0446 File Offset: 0x001AE646
	private void OnEnable()
	{
		if (!Config.game_loaded)
		{
			return;
		}
		this.dropdown = base.GetComponent<Dropdown>();
		this.PopulateDropdown(this.dropdown);
	}

	// Token: 0x06003DE4 RID: 15844 RVA: 0x001B0468 File Offset: 0x001AE668
	private void DropdownValueChanged(Dropdown change)
	{
		Resolution[] resolutions = Screen.resolutions;
		if (ResolutionDropdown.options[change.value] == LocalizedTextManager.getText("windowed_mode", null, false))
		{
			PlayerConfig.setFullScreen(false, true);
		}
		else
		{
			foreach (Resolution res in resolutions)
			{
				if (res.ToString() == ResolutionDropdown.options[change.value])
				{
					if (!Screen.fullScreen)
					{
						PlayerConfig.setFullScreen(true, false);
					}
					Screen.SetResolution(res.width, res.height, true, res.refreshRate);
					break;
				}
			}
		}
		this.fullscreenOption.checkGameOption(false);
	}

	// Token: 0x06003DE5 RID: 15845 RVA: 0x001B051C File Offset: 0x001AE71C
	private void PopulateDropdown(Dropdown dropdown)
	{
		ResolutionDropdown.options.Clear();
		foreach (Resolution res in Screen.resolutions)
		{
			ResolutionDropdown.options.Add(res.ToString());
		}
		ResolutionDropdown.options.Add(LocalizedTextManager.getText("windowed_mode", null, false));
		dropdown.ClearOptions();
		ResolutionDropdown.options.Reverse();
		int currentValue = ResolutionDropdown.options.IndexOf(Screen.currentResolution.ToString());
		if (!Screen.fullScreen)
		{
			currentValue = ResolutionDropdown.options.IndexOf(LocalizedTextManager.getText("windowed_mode", null, false));
		}
		dropdown.AddOptions(ResolutionDropdown.options);
		if (currentValue > -1)
		{
			dropdown.value = currentValue;
		}
		else
		{
			ResolutionDropdown.options.Insert(0, Screen.currentResolution.ToString());
			dropdown.AddOptions(ResolutionDropdown.options);
			dropdown.value = ResolutionDropdown.options.IndexOf(Screen.currentResolution.ToString());
		}
		dropdown.RefreshShownValue();
	}

	// Token: 0x04002CF9 RID: 11513
	private Button button;

	// Token: 0x04002CFA RID: 11514
	private Dropdown dropdown;

	// Token: 0x04002CFB RID: 11515
	public OptionBool fullscreenOption;

	// Token: 0x04002CFC RID: 11516
	private static List<string> options = new List<string>();
}
