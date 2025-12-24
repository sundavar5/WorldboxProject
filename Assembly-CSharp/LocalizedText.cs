using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UPersian.Utils;

// Token: 0x02000470 RID: 1136
public class LocalizedText : UIBehaviour
{
	// Token: 0x060026CC RID: 9932 RVA: 0x0013B04D File Offset: 0x0013924D
	protected override void Awake()
	{
		base.Awake();
		this.text = base.GetComponent<Text>();
	}

	// Token: 0x060026CD RID: 9933 RVA: 0x0013B061 File Offset: 0x00139261
	protected override void Start()
	{
		base.Start();
		if (this.autoField)
		{
			LocalizedTextManager.addTextField(this);
			this.updateText(true);
		}
	}

	// Token: 0x060026CE RID: 9934 RVA: 0x0013B07E File Offset: 0x0013927E
	public void setKeyAndUpdate(string pKey)
	{
		this.key = pKey;
		this.updateText(true);
	}

	// Token: 0x060026CF RID: 9935 RVA: 0x0013B090 File Offset: 0x00139290
	protected override void OnRectTransformDimensionsChange()
	{
		GameLanguageAsset current_language = LocalizedTextManager.current_language;
		if (current_language != null && !current_language.isRTL())
		{
			return;
		}
		if (string.IsNullOrEmpty(this.key) || this.key == "??????")
		{
			return;
		}
		if (this.text == null)
		{
			return;
		}
		this.updateText(true);
		base.OnRectTransformDimensionsChange();
	}

	// Token: 0x060026D0 RID: 9936 RVA: 0x0013B0F0 File Offset: 0x001392F0
	internal virtual void updateText(bool pCheckText = true)
	{
		if (this.text == null)
		{
			return;
		}
		if (LocalizedTextManager.instance == null || !LocalizedTextManager.instance.initiated)
		{
			return;
		}
		if (LocalizedTextManager.current_font != null)
		{
			this.text.font = LocalizedTextManager.current_font;
		}
		string tText = LocalizedTextManager.getText(this.key, this.text, false);
		if (this.convertToUppercase)
		{
			tText = tText.ToUpper();
		}
		if (this.specialTags && tText.Contains("$"))
		{
			if (tText.Contains("$total_prem_powers$"))
			{
				tText = tText.Replace("$total_prem_powers$", GodPower.premium_powers.Count.ToString() ?? "");
			}
			if (tText.Contains("$minutes$"))
			{
				tText = tText.Replace("$minutes$", 30.ToString() ?? "");
			}
			if (tText.Contains("$minutes_clock$"))
			{
				tText = tText.Replace("$minutes_clock$", 720.ToString() ?? "");
			}
			if (tText.Contains("$hours_clock$"))
			{
				tText = tText.Replace("$hours_clock$", 12.ToString() ?? "");
			}
			if (tText.Contains("$power$") && Config.power_to_unlock != null)
			{
				tText = tText.Replace("$power$", Config.power_to_unlock.getLocaleID().Localize() ?? "");
			}
			if (tText.Contains("$hours$"))
			{
				tText = tText.Replace("$hours$", 3.ToString() ?? "");
			}
			if (tText.Contains("$number$"))
			{
				tText = tText.Replace("$number$", 3.ToString() ?? "");
			}
			if (tText.Contains("$discord_count$"))
			{
				tText = tText.Replace("$discord_count$", 560000.ToText() ?? "");
			}
			if (tText.Contains("$wbcode$"))
			{
				tText = tText.Replace("$wbcode$", "<color=cyan>WB-5555-1166-5555</color>");
			}
			if (tText.Contains("$lifeissimhours$"))
			{
				tText = tText.Replace("$lifeissimhours$", 24f.ToText());
			}
			if (tText.Contains("$current_era_year"))
			{
				tText = tText.Replace("$current_era_year$", Date.getCurrentYear().ToText());
			}
			if (tText.Contains("$era_moons_left"))
			{
				int tMoonsLeft = World.world.era_manager.calculateMoonsLeft();
				tText = tText.Replace("$era_moons_left$", tMoonsLeft.ToText());
			}
		}
		this.text.text = tText;
		this.checkTextFont(null);
		if (pCheckText)
		{
			this.checkSpecialLanguages(null);
		}
	}

	// Token: 0x060026D1 RID: 9937 RVA: 0x0013B3A0 File Offset: 0x001395A0
	internal void checkTextFont(GameLanguageAsset pLanguage = null)
	{
		if (this.text == null)
		{
			return;
		}
		if (pLanguage == null)
		{
			pLanguage = LocalizedTextManager.current_language;
		}
		Font tFont = pLanguage.font();
		if (tFont == null)
		{
			return;
		}
		this.text.font = tFont;
	}

	// Token: 0x060026D2 RID: 9938 RVA: 0x0013B3E8 File Offset: 0x001395E8
	internal void checkSpecialLanguages(GameLanguageAsset pLanguage = null)
	{
		if (this.text == null)
		{
			return;
		}
		if (pLanguage == null)
		{
			pLanguage = LocalizedTextManager.current_language;
		}
		this.checkTextFont(pLanguage);
		if (this._text_alignment_before == null)
		{
			this._text_alignment_before = new TextAnchor?(this.text.alignment);
		}
		if (this._font_style_before == null)
		{
			this._font_style_before = new FontStyle?(this.text.fontStyle);
		}
		if (this._shadow_before == null)
		{
			this._shadow_before = new bool?(this._has_shadow = this.text.HasComponent<Shadow>());
		}
		if (pLanguage.hasForcedStyle())
		{
			this.text.fontStyle = pLanguage.force_style.style;
			if (this.text.fontSize < 9 && pLanguage.force_style.shadow && !this._has_shadow)
			{
				this.text.gameObject.AddComponent<Shadow>().effectColor = new Color(0f, 0f, 0f, 160f);
				this._has_shadow = true;
			}
		}
		else
		{
			this.text.fontStyle = this._font_style_before.Value;
			if (this._has_shadow)
			{
				bool? shadow_before = this._shadow_before;
				bool flag = false;
				if (shadow_before.GetValueOrDefault() == flag & shadow_before != null)
				{
					Shadow tTextShadow;
					if (this.text.TryGetComponent<Shadow>(out tTextShadow))
					{
						Object.Destroy(tTextShadow);
					}
					this._has_shadow = false;
				}
			}
		}
		if (pLanguage.isRTL())
		{
			this.text.text = LocalizedText.getRTLText(this.text, this.text.text);
			this.text.alignment = this.getRTLAlignment(this._text_alignment_before.Value);
		}
		else
		{
			this.text.alignment = this._text_alignment_before.Value;
		}
		if (pLanguage.isHindi() && !Regex.IsMatch(this.text.text, "[a-zA-Z]"))
		{
			this.text.SetHindiText(this.text.text);
		}
	}

	// Token: 0x060026D3 RID: 9939 RVA: 0x0013B5F4 File Offset: 0x001397F4
	internal static string getRTLText(Text pText, string pString)
	{
		pText.cachedTextGenerator.Populate(pString, pText.GetGenerationSettings(pText.rectTransform.rect.size));
		List<UILineInfo> tLines = pText.cachedTextGenerator.lines as List<UILineInfo>;
		if (tLines == null)
		{
			return null;
		}
		string tLinedText = "";
		if (tLines.Count == 0)
		{
			tLinedText = pString;
		}
		for (int i = 0; i < tLines.Count; i++)
		{
			if (i < tLines.Count - 1)
			{
				int startIndex = tLines[i].startCharIdx;
				int length = tLines[i + 1].startCharIdx - tLines[i].startCharIdx;
				tLinedText += pString.Substring(startIndex, length);
				if (tLinedText.Length > 0 && tLinedText[tLinedText.Length - 1] != '\n' && tLinedText[tLinedText.Length - 1] != '\r')
				{
					tLinedText += "\n";
				}
			}
			else
			{
				tLinedText += pString.Substring(tLines[i].startCharIdx);
			}
		}
		UPersianUtils.RtlFix(ref tLinedText);
		return tLinedText;
	}

	// Token: 0x060026D4 RID: 9940 RVA: 0x0013B712 File Offset: 0x00139912
	internal TextAnchor getRTLAlignment(TextAnchor pTextAlignment)
	{
		if (pTextAlignment == TextAnchor.UpperLeft)
		{
			return TextAnchor.UpperRight;
		}
		if (pTextAlignment == TextAnchor.UpperRight)
		{
			return TextAnchor.UpperLeft;
		}
		if (pTextAlignment == TextAnchor.MiddleLeft)
		{
			return TextAnchor.MiddleRight;
		}
		if (pTextAlignment == TextAnchor.MiddleRight)
		{
			return TextAnchor.MiddleLeft;
		}
		if (pTextAlignment == TextAnchor.LowerLeft)
		{
			return TextAnchor.LowerRight;
		}
		if (pTextAlignment == TextAnchor.LowerRight)
		{
			return TextAnchor.LowerLeft;
		}
		return pTextAlignment;
	}

	// Token: 0x04001D1E RID: 7454
	public const string DEFAULT_KEY = "??????";

	// Token: 0x04001D1F RID: 7455
	protected const char LINE_ENDING = '\n';

	// Token: 0x04001D20 RID: 7456
	public bool convertToUppercase;

	// Token: 0x04001D21 RID: 7457
	public bool autoField = true;

	// Token: 0x04001D22 RID: 7458
	public bool specialTags;

	// Token: 0x04001D23 RID: 7459
	public string key = "??????";

	// Token: 0x04001D24 RID: 7460
	private FontStyle? _font_style_before;

	// Token: 0x04001D25 RID: 7461
	private bool? _shadow_before = new bool?(false);

	// Token: 0x04001D26 RID: 7462
	private bool _has_shadow;

	// Token: 0x04001D27 RID: 7463
	internal Text text;

	// Token: 0x04001D28 RID: 7464
	private TextAnchor? _text_alignment_before;
}
