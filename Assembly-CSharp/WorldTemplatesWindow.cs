using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000619 RID: 1561
public class WorldTemplatesWindow : MonoBehaviour
{
	// Token: 0x06003336 RID: 13110 RVA: 0x001822EC File Offset: 0x001804EC
	private void Awake()
	{
		this.switch_button.click_increase = new Action(this.increaseSize);
		this.switch_button.click_decrease = new Action(this.decreaseSize);
	}

	// Token: 0x06003337 RID: 13111 RVA: 0x0018231C File Offset: 0x0018051C
	public void increaseSize()
	{
		int curIndex = MapSizeLibrary.getSizes().IndexOf(Config.customMapSize);
		curIndex++;
		if (curIndex > MapSizeLibrary.getSizes().Length - 1)
		{
			curIndex = 0;
		}
		Config.customMapSize = MapSizeLibrary.getSizes()[curIndex];
	}

	// Token: 0x06003338 RID: 13112 RVA: 0x00182358 File Offset: 0x00180558
	public void decreaseSize()
	{
		int curIndex = MapSizeLibrary.getSizes().IndexOf(Config.customMapSize);
		curIndex--;
		if (curIndex < 0)
		{
			curIndex = MapSizeLibrary.getSizes().Length - 1;
		}
		Config.customMapSize = MapSizeLibrary.getSizes()[curIndex];
	}

	// Token: 0x06003339 RID: 13113 RVA: 0x00182394 File Offset: 0x00180594
	private void Update()
	{
		MapSizeAsset tAsset = AssetManager.map_sizes.get(Config.customMapSize);
		if (tAsset.show_warning)
		{
			this.text_hi.gameObject.SetActive(false);
			this.text_size_warning.gameObject.SetActive(true);
		}
		else
		{
			this.text_hi.gameObject.SetActive(true);
			this.text_size_warning.gameObject.SetActive(false);
		}
		this.icon_1.sprite = tAsset.getIconSprite();
		this.icon_2.sprite = tAsset.getIconSprite();
	}

	// Token: 0x0600333A RID: 13114 RVA: 0x00182424 File Offset: 0x00180624
	private void OnEnable()
	{
		MapGenTemplate tTemplate = AssetManager.map_gen_templates.get(Config.current_map_template);
		this.preview_template.sprite = SpriteTextureLoader.getSprite(tTemplate.path_icon);
		this.checkButtons();
		if (tTemplate.show_reset_button)
		{
			this.reset_button.SetActive(true);
			return;
		}
		this.reset_button.SetActive(false);
	}

	// Token: 0x0600333B RID: 13115 RVA: 0x00182480 File Offset: 0x00180680
	public void resetTemplate()
	{
		MapGenTemplate tTemplate = AssetManager.map_gen_templates.get(Config.current_map_template);
		AssetManager.map_gen_templates.resetTemplateValues(tTemplate);
		this.checkButtons();
	}

	// Token: 0x0600333C RID: 13116 RVA: 0x001824B0 File Offset: 0x001806B0
	private void checkButtons()
	{
		MapGenTemplate tTemplate = AssetManager.map_gen_templates.get(Config.current_map_template);
		for (int i = 0; i < this.container_buttons.childCount; i++)
		{
			WorldTemplateButton tButton = this.container_buttons.GetChild(i).gameObject.GetComponent<WorldTemplateButton>();
			if (!(tButton == null))
			{
				string tId = tButton.name;
				if (AssetManager.map_gen_settings.get(tId).allowed_check(tTemplate))
				{
					tButton.gameObject.SetActive(true);
					tButton.updateCounter();
				}
				else
				{
					tButton.gameObject.SetActive(false);
				}
			}
		}
	}

	// Token: 0x040026CF RID: 9935
	public Text text_hi;

	// Token: 0x040026D0 RID: 9936
	public Text text_size_warning;

	// Token: 0x040026D1 RID: 9937
	public Image icon_1;

	// Token: 0x040026D2 RID: 9938
	public Image icon_2;

	// Token: 0x040026D3 RID: 9939
	public Image preview_template;

	// Token: 0x040026D4 RID: 9940
	public Transform container_buttons;

	// Token: 0x040026D5 RID: 9941
	public GameObject reset_button;

	// Token: 0x040026D6 RID: 9942
	public CustomButtonSwitch switch_button;
}
