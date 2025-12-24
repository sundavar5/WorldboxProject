using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x020006B0 RID: 1712
public class ButtonGraphCategory : MonoBehaviour
{
	// Token: 0x060036C8 RID: 14024 RVA: 0x0018CEA4 File Offset: 0x0018B0A4
	private void Awake()
	{
		this.init();
	}

	// Token: 0x060036C9 RID: 14025 RVA: 0x0018CEAC File Offset: 0x0018B0AC
	public void init()
	{
		if (this._initialized)
		{
			return;
		}
		this._initialized = true;
		base.GetComponent<Button>().onClick.AddListener(new UnityAction(this.switchCategory));
		this._tip_button = base.GetComponent<TipButton>();
		this._button_graphics = base.GetComponent<Image>();
		this._icon = base.transform.FindRecursive("Icon").GetComponent<Image>();
		this._main_container = base.GetComponentInParent<GraphCategoriesContainer>();
		this._text = base.transform.FindRecursive("Title").GetComponent<Text>();
		this._colored_circle = base.transform.FindRecursive("Colored Circle").GetComponent<Image>();
		this._background_circle = base.transform.FindRecursive("Background Circle").GetComponent<Image>();
		this._tip_button.hoverAction = delegate()
		{
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			this.showTooltip();
		};
		this.checkSpriteStatus();
	}

	// Token: 0x060036CA RID: 14026 RVA: 0x0018CF94 File Offset: 0x0018B194
	public void setAsset(HistoryDataAsset pAsset)
	{
		if (pAsset == null)
		{
			return;
		}
		this._asset = pAsset;
		this._colored_circle.color = pAsset.getColorMain();
		this._icon.sprite = SpriteTextureLoader.getSprite(pAsset.path_icon);
	}

	// Token: 0x060036CB RID: 14027 RVA: 0x0018CFD5 File Offset: 0x0018B1D5
	private void Update()
	{
		this.checkSpriteStatus();
	}

	// Token: 0x060036CC RID: 14028 RVA: 0x0018CFE0 File Offset: 0x0018B1E0
	private void checkSpriteStatus()
	{
		if (this.is_on)
		{
			this._button_graphics.sprite = this.sprite_on;
			this._background_circle.gameObject.SetActive(true);
			return;
		}
		this._button_graphics.sprite = this.sprite_off;
		this._background_circle.gameObject.SetActive(false);
	}

	// Token: 0x060036CD RID: 14029 RVA: 0x0018D03A File Offset: 0x0018B23A
	private void switchCategory()
	{
		if (!InputHelpers.mouseSupported && !Tooltip.isShowingFor(this))
		{
			this.showTooltip();
		}
		this.is_on = !this.is_on;
		this._main_container.setCategoryEnabled(base.name, this.is_on);
	}

	// Token: 0x060036CE RID: 14030 RVA: 0x0018D078 File Offset: 0x0018B278
	private void showTooltip()
	{
		TooltipData tData = new TooltipData
		{
			tip_name = this._asset.getLocaleID(),
			tip_description = this._asset.getDescriptionID(),
			tip_description_2 = "graph_tip"
		};
		Tooltip.show(this, "tip", tData);
	}

	// Token: 0x060036CF RID: 14031 RVA: 0x0018D0C4 File Offset: 0x0018B2C4
	public void turnOff()
	{
		this.is_on = false;
	}

	// Token: 0x060036D0 RID: 14032 RVA: 0x0018D0CD File Offset: 0x0018B2CD
	public void turnOn()
	{
		this.is_on = true;
	}

	// Token: 0x0400288F RID: 10383
	public Sprite sprite_on;

	// Token: 0x04002890 RID: 10384
	public Sprite sprite_off;

	// Token: 0x04002891 RID: 10385
	public Sprite sprite_on_light;

	// Token: 0x04002892 RID: 10386
	private Image _button_graphics;

	// Token: 0x04002893 RID: 10387
	private Image _icon;

	// Token: 0x04002894 RID: 10388
	public bool is_on;

	// Token: 0x04002895 RID: 10389
	private GraphCategoriesContainer _main_container;

	// Token: 0x04002896 RID: 10390
	private Text _text;

	// Token: 0x04002897 RID: 10391
	private Image _colored_circle;

	// Token: 0x04002898 RID: 10392
	private Image _background_circle;

	// Token: 0x04002899 RID: 10393
	private TipButton _tip_button;

	// Token: 0x0400289A RID: 10394
	private HistoryDataAsset _asset;

	// Token: 0x0400289B RID: 10395
	private bool _initialized;
}
