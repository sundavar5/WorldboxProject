using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x0200053B RID: 1339
public class KeyValueField : MonoBehaviour, IPointerEnterHandler, IEventSystemHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
	// Token: 0x06002BC3 RID: 11203 RVA: 0x0015A0F0 File Offset: 0x001582F0
	private void Awake()
	{
		Button tButton = this.value.GetComponent<Button>();
		if (Input.mousePresent)
		{
			tButton.OnHover(delegate()
			{
				if (InputHelpers.mouseSupported)
				{
					UnityAction unityAction = this.on_hover_value;
					if (unityAction == null)
					{
						return;
					}
					unityAction();
				}
			});
			tButton.OnHoverOut(delegate()
			{
				if (InputHelpers.mouseSupported)
				{
					UnityAction unityAction = this.on_hover_value_out;
					if (unityAction == null)
					{
						return;
					}
					unityAction();
				}
			});
		}
		tButton.onClick.AddListener(delegate()
		{
			if (!InputHelpers.mouseSupported && !Tooltip.isShowingFor(this))
			{
				UnityAction unityAction = this.on_hover_value;
				if (unityAction != null)
				{
					unityAction();
				}
				EventSystem.current.SetSelectedGameObject(base.gameObject);
				return;
			}
			this.setBackgroundColor(this._not_highlight_color);
			UnityAction unityAction2 = this.on_click_value;
			if (unityAction2 == null)
			{
				return;
			}
			unityAction2();
		});
		this._name_text = this.name_text.GetComponent<LocalizedText>();
		this._value = this.value.GetComponent<LocalizedText>();
		this._check_language = (this._name_text != null || this._value != null);
	}

	// Token: 0x06002BC4 RID: 11204 RVA: 0x0015A190 File Offset: 0x00158390
	private void OnEnable()
	{
		this.checkLanguage();
		if (this.auto_odd_even_coloring)
		{
			this.checkOddEvenColor(base.transform.GetActiveSiblingIndex());
		}
	}

	// Token: 0x06002BC5 RID: 11205 RVA: 0x0015A1B1 File Offset: 0x001583B1
	private void OnDisable()
	{
		this.on_hover_value = null;
		this.on_hover_value_out = null;
		this.on_click_value = null;
	}

	// Token: 0x06002BC6 RID: 11206 RVA: 0x0015A1C8 File Offset: 0x001583C8
	private void checkLanguage()
	{
		if (!this._check_language)
		{
			return;
		}
		LocalizedText localizedText = this._name_text;
		if (localizedText != null)
		{
			localizedText.checkSpecialLanguages(null);
		}
		LocalizedText localizedText2 = this._value;
		if (localizedText2 == null)
		{
			return;
		}
		localizedText2.checkSpecialLanguages(null);
	}

	// Token: 0x06002BC7 RID: 11207 RVA: 0x0015A1F6 File Offset: 0x001583F6
	public void checkOddEvenColor(int pIndex)
	{
		if (pIndex % 2 != 0)
		{
			this.setEvenColor();
			return;
		}
		this.setOddColor();
	}

	// Token: 0x06002BC8 RID: 11208 RVA: 0x0015A20A File Offset: 0x0015840A
	public void OnPointerEnter(PointerEventData pData)
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		this._not_highlight_color = this.background.color;
		this.setHighlightColor();
	}

	// Token: 0x06002BC9 RID: 11209 RVA: 0x0015A22B File Offset: 0x0015842B
	public void OnPointerExit(PointerEventData pData)
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		this.setBackgroundColor(this._not_highlight_color);
	}

	// Token: 0x06002BCA RID: 11210 RVA: 0x0015A241 File Offset: 0x00158441
	public void OnSelect(BaseEventData pEventData)
	{
		if (InputHelpers.mouseSupported)
		{
			return;
		}
		this._not_highlight_color = this.background.color;
		this.setHighlightColor();
	}

	// Token: 0x06002BCB RID: 11211 RVA: 0x0015A262 File Offset: 0x00158462
	public void OnDeselect(BaseEventData pEventData)
	{
		if (InputHelpers.mouseSupported)
		{
			return;
		}
		this.setNotHighlightColor();
	}

	// Token: 0x06002BCC RID: 11212 RVA: 0x0015A272 File Offset: 0x00158472
	public void setEvenColor()
	{
		this.setBackgroundColor(this.even_color);
	}

	// Token: 0x06002BCD RID: 11213 RVA: 0x0015A280 File Offset: 0x00158480
	public void setOddColor()
	{
		this.setBackgroundColor(this.odd_color);
	}

	// Token: 0x06002BCE RID: 11214 RVA: 0x0015A28E File Offset: 0x0015848E
	public void setHighlightColor()
	{
		this.setBackgroundColor(this.highlight_color);
	}

	// Token: 0x06002BCF RID: 11215 RVA: 0x0015A29C File Offset: 0x0015849C
	public void setNotHighlightColor()
	{
		this.setBackgroundColor(this._not_highlight_color);
	}

	// Token: 0x06002BD0 RID: 11216 RVA: 0x0015A2AA File Offset: 0x001584AA
	private void setBackgroundColor(Color pColor)
	{
		this.background.color = pColor;
	}

	// Token: 0x06002BD1 RID: 11217 RVA: 0x0015A2B8 File Offset: 0x001584B8
	public void setMetaForTooltip(MetaType pMetaType, long pMetaId, string pTooltipId = null, TooltipDataGetter pData = null)
	{
		this.on_hover_value = null;
		this.on_hover_value_out = new UnityAction(Tooltip.hideTooltip);
		this.on_click_value = null;
		if (!pMetaType.isNone())
		{
			MetaTypeAsset tAsset = AssetManager.meta_type_library.getAsset(pMetaType);
			this.on_hover_value = delegate()
			{
				tAsset.stat_hover(pMetaId, this);
			};
			this.on_click_value = delegate()
			{
				tAsset.stat_click(pMetaId, this);
			};
			return;
		}
		if (!string.IsNullOrEmpty(pTooltipId))
		{
			this.on_hover_value = delegate()
			{
				Tooltip.show(this, pTooltipId, pData());
			};
		}
	}

	// Token: 0x0400219D RID: 8605
	public Color odd_color = Toolbox.makeColor("#000000", 0f);

	// Token: 0x0400219E RID: 8606
	public Color even_color = Toolbox.makeColor("#30322B");

	// Token: 0x0400219F RID: 8607
	public Color highlight_color = Toolbox.makeColor("#111111");

	// Token: 0x040021A0 RID: 8608
	public Image background;

	// Token: 0x040021A1 RID: 8609
	public Image icon;

	// Token: 0x040021A2 RID: 8610
	public Image icon_secondary;

	// Token: 0x040021A3 RID: 8611
	public Text name_text;

	// Token: 0x040021A4 RID: 8612
	public Text value;

	// Token: 0x040021A5 RID: 8613
	public bool auto_odd_even_coloring;

	// Token: 0x040021A6 RID: 8614
	public UnityAction on_hover_value;

	// Token: 0x040021A7 RID: 8615
	public UnityAction on_hover_value_out;

	// Token: 0x040021A8 RID: 8616
	public UnityAction on_click_value;

	// Token: 0x040021A9 RID: 8617
	private Color _not_highlight_color;

	// Token: 0x040021AA RID: 8618
	private LocalizedText _name_text;

	// Token: 0x040021AB RID: 8619
	private LocalizedText _value;

	// Token: 0x040021AC RID: 8620
	private bool _check_language = true;
}
