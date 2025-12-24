using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200071C RID: 1820
public class OnomasticsDropdown : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
{
	// Token: 0x06003A0A RID: 14858 RVA: 0x0019BBB3 File Offset: 0x00199DB3
	private void Start()
	{
		this._dropdown = base.GetComponent<Dropdown>();
		this.createDropdownOptions();
		this._dropdown.onValueChanged.AddListener(new UnityAction<int>(this.dropdownValueChanged));
	}

	// Token: 0x06003A0B RID: 14859 RVA: 0x0019BBE4 File Offset: 0x00199DE4
	private void createDropdownOptions()
	{
		this._dropdown.ClearOptions();
		this._options = new List<string>();
		this._options.Add("");
		foreach (NameGeneratorAsset tVal in AssetManager.name_generator.list)
		{
			if (tVal.onomastics_templates.Count < 1)
			{
				this._options.Add("<color=red>" + tVal.id + "</color>");
			}
			else if (tVal.onomastics_templates.Count < 2)
			{
				this._options.Add(tVal.id);
			}
			else
			{
				for (int i = 0; i < tVal.onomastics_templates.Count; i++)
				{
					this._options.Add(tVal.id + "#" + i.ToString());
				}
			}
		}
		this._options.Sort((string a, string b) => Toolbox.removeRichTextTags(a).CompareTo(Toolbox.removeRichTextTags(b)));
		this._dropdown.AddOptions(this._options);
	}

	// Token: 0x06003A0C RID: 14860 RVA: 0x0019BD24 File Offset: 0x00199F24
	private void dropdownValueChanged(int pOption)
	{
		if (pOption < 0 || pOption >= this._dropdown.options.Count)
		{
			return;
		}
		string tAssetID = this._dropdown.options[pOption].text;
		if (string.IsNullOrEmpty(tAssetID))
		{
			return;
		}
		tAssetID = Toolbox.removeRichTextTags(tAssetID);
		int tIndex = 0;
		if (tAssetID.Contains('#'))
		{
			string[] array = tAssetID.Split('#', StringSplitOptions.None);
			tAssetID = array[0];
			if (!int.TryParse(array[1], out tIndex))
			{
				return;
			}
		}
		NameGeneratorAsset tTemplate = AssetManager.name_generator.get(tAssetID);
		if (tTemplate == null)
		{
			return;
		}
		OnomasticsDropdown.current_template = tAssetID;
		OnomasticsDropdown.current_template_index = tIndex;
		string tOnomasticsTemplate = null;
		if (tIndex >= 0 && tIndex < tTemplate.onomastics_templates.Count)
		{
			tOnomasticsTemplate = tTemplate.onomastics_templates[tIndex];
		}
		this._onomastics_tab.loadTemplate(tOnomasticsTemplate);
	}

	// Token: 0x06003A0D RID: 14861 RVA: 0x0019BDE0 File Offset: 0x00199FE0
	public void OnPointerClick(PointerEventData pEventData)
	{
		if (pEventData.selectedObject == null || pEventData.selectedObject.GetComponentInChildren<Scrollbar>() != null || !this._dropdown.IsActive() || !this._dropdown.IsInteractable())
		{
			return;
		}
		ScrollRect componentInChildren = base.gameObject.GetComponentInChildren<ScrollRect>();
		Scrollbar scrollbar = (componentInChildren != null) ? componentInChildren.verticalScrollbar : null;
		if (this._options.Count > 1 && scrollbar != null)
		{
			if (scrollbar.direction == Scrollbar.Direction.TopToBottom)
			{
				scrollbar.value = Mathf.Max(0.001f, (float)this._dropdown.value / (float)(this._options.Count - 1));
				return;
			}
			scrollbar.value = Mathf.Max(0.001f, 1f - (float)this._dropdown.value / (float)(this._options.Count - 1));
		}
	}

	// Token: 0x04002AED RID: 10989
	[SerializeField]
	private OnomasticsTab _onomastics_tab;

	// Token: 0x04002AEE RID: 10990
	private Button _button;

	// Token: 0x04002AEF RID: 10991
	private Dropdown _dropdown;

	// Token: 0x04002AF0 RID: 10992
	private List<string> _options;

	// Token: 0x04002AF1 RID: 10993
	internal static string current_template;

	// Token: 0x04002AF2 RID: 10994
	internal static int current_template_index;
}
