using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020005CA RID: 1482
public class ButtonClickMaptemplate : MonoBehaviour
{
	// Token: 0x060030B2 RID: 12466 RVA: 0x001775FC File Offset: 0x001757FC
	private void Awake()
	{
		string tTemplateID = base.transform.name;
		this._button = base.GetComponent<Button>();
		this._button.onClick.AddListener(new UnityAction(this.click));
		if (Input.mousePresent)
		{
			this._button.OnHover(delegate()
			{
				if (!InputHelpers.mouseSupported)
				{
					return;
				}
				this.showTooltip();
			});
			this._button.OnHoverOut(delegate()
			{
				if (!InputHelpers.mouseSupported)
				{
					return;
				}
				Tooltip.hideTooltip();
			});
		}
		this._template = AssetManager.map_gen_templates.get(tTemplateID);
		base.transform.Find("preview_icon").GetComponent<Image>().sprite = SpriteTextureLoader.getSprite(this._template.path_icon);
	}

	// Token: 0x060030B3 RID: 12467 RVA: 0x001776C0 File Offset: 0x001758C0
	private void showTooltip()
	{
		Tooltip.show(this._button.gameObject, "normal", new TooltipData
		{
			tip_name = this._template.getLocaleID(),
			tip_description = this._template.getDescriptionID()
		});
	}

	// Token: 0x060030B4 RID: 12468 RVA: 0x00177700 File Offset: 0x00175900
	public void click()
	{
		if (!InputHelpers.mouseSupported)
		{
			if (!Tooltip.isShowingFor(this._button.gameObject))
			{
				this.showTooltip();
				return;
			}
			Tooltip.hideTooltipNow();
		}
		Config.current_map_template = this._template.id;
		ScrollWindow.showWindow("new_world_templates_2");
	}

	// Token: 0x040024E1 RID: 9441
	private Button _button;

	// Token: 0x040024E2 RID: 9442
	private MapGenTemplate _template;
}
