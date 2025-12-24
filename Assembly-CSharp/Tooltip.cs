using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200077A RID: 1914
public class Tooltip : MonoBehaviour
{
	// Token: 0x17000391 RID: 913
	// (get) Token: 0x06003C93 RID: 15507 RVA: 0x001A41DE File Offset: 0x001A23DE
	private static Canvas _parent_canvas
	{
		get
		{
			return CanvasMain.instance.canvas_tooltip;
		}
	}

	// Token: 0x06003C94 RID: 15508 RVA: 0x001A41EC File Offset: 0x001A23EC
	private void Awake()
	{
		this._rect = base.GetComponent<RectTransform>();
		this._description_container = this.description.transform.parent.gameObject;
		this._layout_group = base.GetComponent<VerticalLayoutGroup>();
		this._headline = this.topGraphics.transform.parent.GetComponent<LayoutElement>();
		if (this.description_2 != null)
		{
			this._description_2_container = this.description_2.transform.parent.gameObject;
		}
	}

	// Token: 0x06003C95 RID: 15509 RVA: 0x001A4270 File Offset: 0x001A2470
	public static Tooltip getTooltip(string pID)
	{
		Tooltip tResultTooltip = null;
		if (!Tooltip._dict_tooltips.TryGetValue(pID, out tResultTooltip))
		{
			TooltipAsset tAsset = AssetManager.tooltips.get(pID);
			if (tAsset == null)
			{
				string message = "Tooltip Asset " + pID + " doesn't exist.";
				Debug.LogError(message);
				throw new Exception(message);
			}
			Tooltip tTooltipPrefab = Resources.Load<Tooltip>(tAsset.prefab_id);
			if (tTooltipPrefab == null)
			{
				Debug.LogWarning("Tooltip prefab for " + tAsset.prefab_id + " could not be found");
				tTooltipPrefab = Resources.Load<Tooltip>("tooltips/tooltip_normal");
			}
			tResultTooltip = Object.Instantiate<Tooltip>(tTooltipPrefab, Tooltip._parent_canvas.transform);
			tResultTooltip.transform.name = tAsset.id;
			tResultTooltip.asset = tAsset;
			Tooltip._dict_tooltips.Add(pID, tResultTooltip);
		}
		return tResultTooltip;
	}

	// Token: 0x06003C96 RID: 15510 RVA: 0x001A432C File Offset: 0x001A252C
	public static void checkClearAll()
	{
		foreach (Tooltip tooltip in Tooltip._dict_tooltips.Values)
		{
			tooltip.checkClear();
		}
	}

	// Token: 0x06003C97 RID: 15511 RVA: 0x001A4380 File Offset: 0x001A2580
	public void checkClear()
	{
		if (!base.gameObject.activeSelf && this._last_object != null)
		{
			if (this._clear_timeout < 0.2f)
			{
				this._clear_timeout += Time.deltaTime;
				return;
			}
			this._last_object = null;
		}
	}

	// Token: 0x06003C98 RID: 15512 RVA: 0x001A43C0 File Offset: 0x001A25C0
	public static bool isShowingFor(object pObject)
	{
		using (Dictionary<string, Tooltip>.ValueCollection.Enumerator enumerator = Tooltip._dict_tooltips.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current._last_object == pObject)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06003C99 RID: 15513 RVA: 0x001A4420 File Offset: 0x001A2620
	public static Tooltip findActive(Predicate<Tooltip> pMatch)
	{
		foreach (Tooltip tTooltip in Tooltip._dict_tooltips.Values)
		{
			if (tTooltip.gameObject.activeSelf && pMatch(tTooltip))
			{
				return tTooltip;
			}
		}
		return null;
	}

	// Token: 0x06003C9A RID: 15514 RVA: 0x001A4490 File Offset: 0x001A2690
	public static void show(object pObject, string pType, TooltipData pData)
	{
		if (CanvasMain.tooltip_show_timeout > 0f)
		{
			return;
		}
		if (ScrollWindow.isAnimationActive())
		{
			return;
		}
		if (Config.isDraggingItem())
		{
			return;
		}
		if (InputHelpers.mouseSupported)
		{
			if ((Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(1)) && !Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
			{
				return;
			}
			if (Input.mouseScrollDelta.y != 0f)
			{
				return;
			}
		}
		Tooltip.hideTooltip(null, false, pType);
		Tooltip tTooltip = Tooltip.getTooltip(pType);
		if (tTooltip == null)
		{
			return;
		}
		tTooltip.clear();
		tTooltip.data = pData;
		if (pObject == null)
		{
			return;
		}
		tTooltip.showTooltip(pObject, pType);
	}

	// Token: 0x06003C9B RID: 15515 RVA: 0x001A4536 File Offset: 0x001A2736
	public void clearTextRows()
	{
		this.stats_description.text = "";
		this.stats_values.text = "";
	}

	// Token: 0x06003C9C RID: 15516 RVA: 0x001A4558 File Offset: 0x001A2758
	private void clearStats()
	{
		this.clearTextRows();
		this.stats_container.SetActive(false);
		this.resetDescription();
		this.resetBottomDescription();
	}

	// Token: 0x06003C9D RID: 15517 RVA: 0x001A4578 File Offset: 0x001A2778
	public void showTooltip(object pObject, string pType)
	{
		if (ScrollWindow.isAnimationActive())
		{
			return;
		}
		this._touch = (Input.touchCount > 0);
		bool tSameObject = false;
		if (this._last_object == pObject)
		{
			tSameObject = true;
		}
		else if (this._last_object != null)
		{
			Type type = this._last_object.GetType();
			Type tType2 = pObject.GetType();
			if (type == tType2)
			{
				tSameObject = true;
			}
		}
		this._last_object = pObject;
		this._sim_tooltip = this.data.is_sim_tooltip;
		if (!base.gameObject.activeSelf)
		{
			base.gameObject.SetActive(true);
		}
		this._type = pType;
		this.asset = AssetManager.tooltips.get(this._type);
		float tTargetY = this.data.tooltip_scale;
		float tTargetX = this.data.tooltip_scale;
		base.transform.localScale = new Vector3(tTargetX, tTargetY, 1f);
		this._timeout = 0.1f;
		this.clearStats();
		this.description.text = "";
		this.opinion_list.Clear();
		TooltipShowAction callback = this.asset.callback;
		if (callback != null)
		{
			callback(this, this._type, this.data);
		}
		this.checkBottomLineSeparator();
		this.showStatValues();
		LayoutRebuilder.ForceRebuildLayoutImmediate(this._rect);
		this.reposition();
		this.name.GetComponent<LocalizedText>().checkSpecialLanguages(this.data.game_language_asset);
		this.description.GetComponent<LocalizedText>().checkSpecialLanguages(this.data.game_language_asset);
		Text text = this.description_2;
		if (text != null)
		{
			text.GetComponent<LocalizedText>().checkSpecialLanguages(this.data.game_language_asset);
		}
		if (!tSameObject)
		{
			bool sound_allowed = this.data.sound_allowed;
		}
	}

	// Token: 0x06003C9E RID: 15518 RVA: 0x001A4720 File Offset: 0x001A2920
	private void checkBottomLineSeparator()
	{
		Transform transform = base.transform.FindRecursive("Line Bottom Separator");
		GameObject tLineSeparator = (transform != null) ? transform.gameObject : null;
		if (tLineSeparator == null)
		{
			return;
		}
		bool tHaveDescription1Text = this.description.gameObject.activeSelf && this.description.text.Length > 0;
		if (this.description_2.gameObject.activeSelf && this.description_2.text.Length > 0 && tHaveDescription1Text && this.stats_description.text.Length == 0)
		{
			tLineSeparator.SetActive(true);
			return;
		}
		tLineSeparator.SetActive(false);
	}

	// Token: 0x06003C9F RID: 15519 RVA: 0x001A47CA File Offset: 0x001A29CA
	public bool isTouchTooltip()
	{
		return this._touch;
	}

	// Token: 0x06003CA0 RID: 15520 RVA: 0x001A47D4 File Offset: 0x001A29D4
	internal void reposition()
	{
		TooltipDirection tDirection = this.getDirection(Input.mousePosition);
		Vector2 tPos;
		this.getPosition(tDirection, out tPos);
		this._rect.position = tPos;
		if (base.transform.localScale.x != this.data.tooltip_scale)
		{
			base.transform.localScale = new Vector3(this.data.tooltip_scale, this.data.tooltip_scale, 1f);
		}
		float tBackgroundHeight = this.background.rectTransform.sizeDelta.y;
		if (tBackgroundHeight.Equals(this._last_height))
		{
			return;
		}
		if (tBackgroundHeight < 27f)
		{
			this.topGraphics.sprite = this.tooltipTopGraphicsNormal;
			this._headline.preferredHeight = 17f;
		}
		else
		{
			this.topGraphics.sprite = this.tooltipTopGraphicsFlat;
			this._headline.preferredHeight = 21.6f;
		}
		this._last_height = tBackgroundHeight;
	}

	// Token: 0x06003CA1 RID: 15521 RVA: 0x001A48D0 File Offset: 0x001A2AD0
	internal bool nullCheck(object pObject)
	{
		if (this.data == null)
		{
			return true;
		}
		if (pObject == null)
		{
			return true;
		}
		NanoObject tNanoObject = pObject as NanoObject;
		if (tNanoObject == null)
		{
			GameObject tGameObject = pObject as GameObject;
			if (tGameObject == null)
			{
				MonoBehaviour tMonoObject = pObject as MonoBehaviour;
				if (tMonoObject != null)
				{
					if (tMonoObject == null || tMonoObject.gameObject == null)
					{
						return true;
					}
				}
			}
			else if (tGameObject == null)
			{
				return true;
			}
		}
		else if (!tNanoObject.isAlive())
		{
			return true;
		}
		return false;
	}

	// Token: 0x06003CA2 RID: 15522 RVA: 0x001A493C File Offset: 0x001A2B3C
	internal void getPosition(TooltipDirection pDirection, out Vector2 pPos)
	{
		pPos = Input.mousePosition;
		float tCanvasScale = Tooltip._parent_canvas.scaleFactor * this.data.tooltip_scale;
		Vector2 sizeDelta = this._rect.sizeDelta;
		Vector2 sizeDelta2 = this._rect.sizeDelta;
		Vector2 tPivot = new Vector2(0.5f, 0.5f);
		if (pDirection.HasFlag(TooltipDirection.Up))
		{
			tPivot.y = 0f;
		}
		if (pDirection.HasFlag(TooltipDirection.MagnetDown))
		{
			tPivot.y = 0f;
		}
		if (pDirection.HasFlag(TooltipDirection.Down))
		{
			tPivot.y = 1f;
		}
		if (pDirection.HasFlag(TooltipDirection.MagnetUp))
		{
			tPivot.y = 1f;
		}
		if (pDirection.HasFlag(TooltipDirection.Left))
		{
			tPivot.x = 1f;
		}
		if (pDirection.HasFlag(TooltipDirection.MagnetRight))
		{
			tPivot.x = 1f;
		}
		if (pDirection.HasFlag(TooltipDirection.Right))
		{
			tPivot.x = 0f;
		}
		if (pDirection.HasFlag(TooltipDirection.MagnetLeft))
		{
			tPivot.x = 0f;
		}
		if (pDirection.HasFlag(TooltipDirection.Up))
		{
			pPos.y += 25f;
		}
		if (pDirection.HasFlag(TooltipDirection.Down))
		{
			pPos.y -= 25f;
		}
		if (pDirection.HasFlag(TooltipDirection.Left))
		{
			pPos.x -= 25f;
		}
		if (pDirection.HasFlag(TooltipDirection.Right))
		{
			pPos.x += 25f;
		}
		if (pDirection.HasFlag(TooltipDirection.MagnetUp))
		{
			pPos.y = (float)Screen.height;
		}
		if (pDirection.HasFlag(TooltipDirection.MagnetDown))
		{
			pPos.y = 0f;
		}
		if (pDirection.HasFlag(TooltipDirection.MagnetLeft))
		{
			pPos.x = 0f;
		}
		if (pDirection.HasFlag(TooltipDirection.MagnetRight))
		{
			pPos.x = (float)Screen.width;
		}
		this._rect.pivot = tPivot;
		this._rect.anchorMin = tPivot;
		this._rect.anchorMax = tPivot;
	}

	// Token: 0x06003CA3 RID: 15523 RVA: 0x001A4BC8 File Offset: 0x001A2DC8
	internal TooltipDirection getDirection(Vector2 pPos)
	{
		float x = pPos.x;
		float y = pPos.y;
		float tCanvasScale = Tooltip._parent_canvas.scaleFactor * this.data.tooltip_scale;
		float tHeight = this._rect.sizeDelta.y * tCanvasScale + 25f;
		float tWidth = this._rect.sizeDelta.x * tCanvasScale + 25f;
		TooltipDirection tResult = TooltipDirection.None;
		bool tOutOfBoundsBottom = y - tHeight <= 0f;
		float num = tHeight / 2f;
		bool tOutOfBoundsTop = y + tHeight > (float)Screen.height;
		bool tOutOfBoundsTopHalf = y + tHeight / 2f > (float)Screen.height;
		bool tOutOfBoundsRightHalf = x + tWidth / 2f > (float)Screen.width;
		bool tOutOfBoundsRight = x + tWidth > (float)Screen.width;
		bool tOutOfBoundsLeftHalf = x - tWidth / 2f <= 0f;
		bool tOutOfBoundsLeft = x - tWidth <= 0f;
		bool tOutOfBoundsSidesHalf = tOutOfBoundsLeftHalf && tOutOfBoundsRightHalf;
		bool tOutOfBoundsSides = tOutOfBoundsLeft && tOutOfBoundsRight;
		if (!this.isTouchTooltip())
		{
			if (tOutOfBoundsBottom)
			{
				tResult |= TooltipDirection.MagnetDown;
			}
			else if (tOutOfBoundsTop)
			{
				tResult |= TooltipDirection.Down;
			}
			else
			{
				tResult |= TooltipDirection.Down;
			}
			if (tOutOfBoundsRight)
			{
				if (tOutOfBoundsBottom)
				{
					tResult |= TooltipDirection.Left;
				}
				else
				{
					tResult |= TooltipDirection.MagnetRight;
				}
			}
			else
			{
				tResult |= TooltipDirection.Right;
			}
		}
		else
		{
			if (tOutOfBoundsTop)
			{
				if (!tOutOfBoundsSides)
				{
					if (tOutOfBoundsTopHalf)
					{
						tResult |= TooltipDirection.MagnetUp;
					}
				}
				else if (tOutOfBoundsBottom)
				{
					tResult |= TooltipDirection.MagnetUp;
				}
				else
				{
					tResult |= TooltipDirection.Down;
				}
			}
			else if (tOutOfBoundsBottom)
			{
				tResult |= TooltipDirection.Up;
			}
			else if (!tOutOfBoundsSidesHalf)
			{
				tResult |= TooltipDirection.Up;
			}
			if (tOutOfBoundsLeftHalf)
			{
				if (tOutOfBoundsLeft)
				{
					tResult |= TooltipDirection.Right;
				}
				else
				{
					tResult |= TooltipDirection.MagnetLeft;
				}
			}
			else if (tOutOfBoundsRightHalf)
			{
				if (tOutOfBoundsRight)
				{
					tResult |= TooltipDirection.Left;
				}
				else
				{
					tResult |= TooltipDirection.MagnetRight;
				}
			}
			else if (tResult == TooltipDirection.None || tResult == TooltipDirection.MagnetUp)
			{
				if (tOutOfBoundsLeft)
				{
					tResult |= TooltipDirection.Right;
				}
				else
				{
					tResult |= TooltipDirection.Left;
				}
			}
		}
		return tResult;
	}

	// Token: 0x06003CA4 RID: 15524 RVA: 0x001A4D6C File Offset: 0x001A2F6C
	internal void setDescription(string pDescription, string pColor = null)
	{
		this.resetDescription();
		this.addDescription(pDescription, pColor);
	}

	// Token: 0x06003CA5 RID: 15525 RVA: 0x001A4D7C File Offset: 0x001A2F7C
	internal void addDescription(string pDescription, string pColor = null)
	{
		if (pDescription != "")
		{
			if (!string.IsNullOrEmpty(pColor))
			{
				pDescription = Toolbox.coloredText(pDescription, pColor, false);
			}
			Text text = this.description;
			text.text += pDescription;
			this._description_container.SetActive(true);
		}
	}

	// Token: 0x06003CA6 RID: 15526 RVA: 0x001A4DCB File Offset: 0x001A2FCB
	internal void resetDescription()
	{
		this.description.text = "";
		this.description.font = LocalizedTextManager.current_font;
		this._description_container.SetActive(false);
	}

	// Token: 0x06003CA7 RID: 15527 RVA: 0x001A4DF9 File Offset: 0x001A2FF9
	internal void setBottomDescription(string pDescription, string pColor = null)
	{
		this.resetBottomDescription();
		this.addBottomDescription(pDescription, pColor);
	}

	// Token: 0x06003CA8 RID: 15528 RVA: 0x001A4E0C File Offset: 0x001A300C
	internal void addBottomDescription(string pDescription, string pColor = null)
	{
		if (pDescription != "")
		{
			if (!string.IsNullOrEmpty(pColor))
			{
				pDescription = Toolbox.coloredText(pDescription, pColor, false);
			}
			Text text = this.description_2;
			text.text += pDescription;
			this._description_2_container.SetActive(true);
		}
	}

	// Token: 0x06003CA9 RID: 15529 RVA: 0x001A4E5B File Offset: 0x001A305B
	internal void resetBottomDescription()
	{
		if (this.description_2 == null)
		{
			return;
		}
		this.description_2.text = "";
		this.description_2.font = LocalizedTextManager.current_font;
		this._description_2_container.SetActive(false);
	}

	// Token: 0x06003CAA RID: 15530 RVA: 0x001A4E98 File Offset: 0x001A3098
	internal void addStatValues(string pStats, string pValues)
	{
		Text text = this.stats_description;
		text.text += pStats;
		Text text2 = this.stats_values;
		text2.text += pValues;
		this.stats_container.SetActive(true);
	}

	// Token: 0x06003CAB RID: 15531 RVA: 0x001A4ED4 File Offset: 0x001A30D4
	internal void showOpinion(string pDescriptionString, string pValuesString, Text pTextDescription = null, Text pTextValues = null)
	{
		if (pTextDescription == null)
		{
			pTextDescription = this.stats_description;
			pTextValues = this.stats_values;
		}
		Text text = pTextDescription;
		text.text += pDescriptionString;
		Text text2 = pTextValues;
		text2.text += pValuesString;
	}

	// Token: 0x06003CAC RID: 15532 RVA: 0x001A4F14 File Offset: 0x001A3114
	internal void showStatValues()
	{
		if (this.stats_description.text.Length > 0)
		{
			this.stats_container.SetActive(true);
			LocalizedText tValues;
			if (this.stats_values.TryGetComponent<LocalizedText>(out tValues) && tValues.enabled)
			{
				tValues.checkSpecialLanguages(null);
			}
			LocalizedText tDescription;
			if (this.stats_description.TryGetComponent<LocalizedText>(out tDescription) && tDescription.enabled)
			{
				tDescription.checkSpecialLanguages(null);
			}
		}
	}

	// Token: 0x06003CAD RID: 15533 RVA: 0x001A4F7C File Offset: 0x001A317C
	internal void addItemText(string pID, float pValue, bool pPercent = false, bool pAddColor = true, bool pAddPlus = true, string pMainColor = "#43FF43", bool pForceZero = false)
	{
		if (pValue == 0f && !pForceZero)
		{
			return;
		}
		string tValString = pValue.ToText();
		if (pPercent)
		{
			tValString += "%";
		}
		if (!pAddColor)
		{
			this.addLineText(pID, tValString, "#FFFFFF", pPercent, true, 21);
			return;
		}
		if (pValue > 0f)
		{
			if (pAddPlus)
			{
				tValString = "+" + tValString;
			}
			this.addLineText(pID, tValString, pMainColor, pPercent, true, 21);
			return;
		}
		this.addLineText(pID, tValString, "#FB2C21", pPercent, true, 21);
	}

	// Token: 0x06003CAE RID: 15534 RVA: 0x001A4FFA File Offset: 0x001A31FA
	internal void addLineIntText(string pID, int pValue, string pColor = null, bool pLocalize = true)
	{
		this.addLineText(pID, pValue.ToText(), pColor, false, pLocalize, 21);
	}

	// Token: 0x06003CAF RID: 15535 RVA: 0x001A500F File Offset: 0x001A320F
	internal void addLineIntText(string pID, long pValue, string pColor = null, bool pLocalize = true, int pLimitValue = 21)
	{
		this.addLineLongText(pID, pValue, pColor, pLocalize, pLimitValue);
	}

	// Token: 0x06003CB0 RID: 15536 RVA: 0x001A501E File Offset: 0x001A321E
	internal void addLineLongText(string pID, long pValue, string pColor = null, bool pLocalize = true, int pLimitValue = 21)
	{
		this.addLineText(pID, pValue.ToText(), pColor, false, pLocalize, pLimitValue);
	}

	// Token: 0x06003CB1 RID: 15537 RVA: 0x001A5034 File Offset: 0x001A3234
	internal void addLineBreak()
	{
		if (this.stats_description.text.Length == 0)
		{
			return;
		}
		Text text = this.stats_description;
		text.text += "\n";
		Text text2 = this.stats_values;
		text2.text += "\n";
	}

	// Token: 0x06003CB2 RID: 15538 RVA: 0x001A508C File Offset: 0x001A328C
	public void tryShowBoolDebug(string pIO, bool pValue)
	{
		string tColor;
		if (pValue)
		{
			tColor = "#43FF43";
		}
		else
		{
			tColor = "#FB2C21";
		}
		this.addLineText(pIO, pValue.ToString(), tColor, false, false, 21);
	}

	// Token: 0x06003CB3 RID: 15539 RVA: 0x001A50C0 File Offset: 0x001A32C0
	internal void addLineText(string pID, string pValue, string pColor = null, bool pPercent = false, bool pLocalize = true, int pLimitValue = 21)
	{
		if (this.stats_description.text.Length > 0)
		{
			this.addLineBreak();
		}
		if (pValue != null && pValue.Length > pLimitValue)
		{
			pValue = pValue.Substring(0, pLimitValue - 1) + "...";
		}
		string tLocalizedText = pLocalize ? pID.Localize() : pID;
		if (pPercent)
		{
			tLocalizedText += " %";
		}
		if (!string.IsNullOrEmpty(pColor))
		{
			Text text = this.stats_description;
			text.text += tLocalizedText;
			Text text2 = this.stats_values;
			text2.text += Toolbox.coloredText(pValue, pColor, false);
			return;
		}
		Text text3 = this.stats_description;
		text3.text += tLocalizedText;
		Text text4 = this.stats_values;
		text4.text += pValue;
	}

	// Token: 0x06003CB4 RID: 15540 RVA: 0x001A5194 File Offset: 0x001A3394
	internal void addOpinion(TooltipOpinionInfo pOpinion)
	{
		this.opinion_list.Add(pOpinion);
	}

	// Token: 0x06003CB5 RID: 15541 RVA: 0x001A51A4 File Offset: 0x001A33A4
	private void Update()
	{
		this.updateTextContentAnimation();
		if (this._timeout > 0f)
		{
			this._timeout -= Time.deltaTime;
			return;
		}
		if (InputHelpers.GetAnyMouseButtonDown() || Input.mouseScrollDelta.y != 0f || ScrollRectExtended.isAnyDragged())
		{
			this.hide();
		}
	}

	// Token: 0x06003CB6 RID: 15542 RVA: 0x001A51FC File Offset: 0x001A33FC
	private void LateUpdate()
	{
		if (this._hide_tooltip_timer != null)
		{
			if (Vector2.Distance(Input.mousePosition, this._hide_pos) > 10f)
			{
				this.hide();
			}
			return;
		}
		this.reposition();
	}

	// Token: 0x06003CB7 RID: 15543 RVA: 0x001A5230 File Offset: 0x001A3430
	private void updateTextContentAnimation()
	{
		if (this._timeout_animation > 0f)
		{
			this._timeout_animation -= Time.deltaTime;
			return;
		}
		this._timeout_animation = 0.08f;
		if (this.asset.callback_text_animated != null)
		{
			this.asset.callback_text_animated(this, this._type, this.data);
		}
	}

	// Token: 0x06003CB8 RID: 15544 RVA: 0x001A5292 File Offset: 0x001A3492
	public void hide()
	{
		this.clearHideTimer();
		base.gameObject.SetActive(false);
		this._clear_timeout = 0f;
		this._sim_tooltip = false;
		TooltipData tooltipData = this.data;
		if (tooltipData != null)
		{
			tooltipData.Dispose();
		}
		this.data = null;
	}

	// Token: 0x06003CB9 RID: 15545 RVA: 0x001A52D0 File Offset: 0x001A34D0
	private void OnDisable()
	{
		this.clear();
	}

	// Token: 0x06003CBA RID: 15546 RVA: 0x001A52D8 File Offset: 0x001A34D8
	private void clear()
	{
		ObjectPoolGenericMono<Image> objectPoolGenericMono = this.pool_traits_actor;
		if (objectPoolGenericMono != null)
		{
			objectPoolGenericMono.clear(true);
		}
		ObjectPoolGenericMono<TooltipOutlineItem> objectPoolGenericMono2 = this.pool_equipments_actor;
		if (objectPoolGenericMono2 != null)
		{
			objectPoolGenericMono2.clear(true);
		}
		ObjectPoolGenericMono<Image> objectPoolGenericMono3 = this.pool_traits_culture;
		if (objectPoolGenericMono3 != null)
		{
			objectPoolGenericMono3.clear(true);
		}
		ObjectPoolGenericMono<Image> objectPoolGenericMono4 = this.pool_traits_language;
		if (objectPoolGenericMono4 != null)
		{
			objectPoolGenericMono4.clear(true);
		}
		ObjectPoolGenericMono<StatsIcon> objectPoolGenericMono5 = this.pool_icons;
		if (objectPoolGenericMono5 != null)
		{
			objectPoolGenericMono5.clear(true);
		}
		ObjectPoolGenericMono<StatsIcon> objectPoolGenericMono6 = this.pool_icons_2;
		if (objectPoolGenericMono6 != null)
		{
			objectPoolGenericMono6.clear(true);
		}
		this.clearHideTimer();
	}

	// Token: 0x06003CBB RID: 15547 RVA: 0x001A5358 File Offset: 0x001A3558
	public static void hideTooltip(object pObjectToSkip, bool pOnlySimObjects, string pSkipType)
	{
		foreach (Tooltip tTooltip in Tooltip._dict_tooltips.Values)
		{
			if ((pObjectToSkip == null || pObjectToSkip != tTooltip._last_object) && (!pOnlySimObjects || tTooltip._sim_tooltip) && (!(pSkipType != string.Empty) || !(tTooltip.asset.id == pSkipType)) && tTooltip.gameObject.activeSelf)
			{
				tTooltip.hide();
				tTooltip._last_object = null;
			}
		}
	}

	// Token: 0x06003CBC RID: 15548 RVA: 0x001A53FC File Offset: 0x001A35FC
	public static void blockTooltips(float pDuration = 0f)
	{
		Tooltip.hideTooltip(null, false, string.Empty);
		if (pDuration > 0f)
		{
			CanvasMain.addTooltipShowTimeout(pDuration);
		}
	}

	// Token: 0x06003CBD RID: 15549 RVA: 0x001A5418 File Offset: 0x001A3618
	public static void hideTooltipNow()
	{
		Tooltip.hideTooltip(null, false, string.Empty);
	}

	// Token: 0x06003CBE RID: 15550 RVA: 0x001A5426 File Offset: 0x001A3626
	public static void hideTooltip()
	{
		Tooltip.scheduledHide(0.08f, false);
	}

	// Token: 0x06003CBF RID: 15551 RVA: 0x001A5434 File Offset: 0x001A3634
	public static bool anyActive()
	{
		using (Dictionary<string, Tooltip>.ValueCollection.Enumerator enumerator = Tooltip._dict_tooltips.Values.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.gameObject.activeSelf)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06003CC0 RID: 15552 RVA: 0x001A5498 File Offset: 0x001A3698
	public static void scheduledHide(float pTimeout = 0.15f, bool pSkipTouch = false)
	{
		foreach (Tooltip tTooltip in Tooltip._dict_tooltips.Values)
		{
			if (tTooltip.gameObject.activeSelf && (!pSkipTouch || !tTooltip.isTouchTooltip()))
			{
				tTooltip.scheduleHide(pTimeout);
			}
		}
	}

	// Token: 0x06003CC1 RID: 15553 RVA: 0x001A5508 File Offset: 0x001A3708
	public static void cancelHiding()
	{
		foreach (Tooltip tTooltip in Tooltip._dict_tooltips.Values)
		{
			if (tTooltip.gameObject.activeSelf)
			{
				tTooltip.clearHideTimer();
			}
		}
	}

	// Token: 0x06003CC2 RID: 15554 RVA: 0x001A556C File Offset: 0x001A376C
	private void scheduleHide(float pTimeout)
	{
		this.clearHideTimer();
		this._hide_tooltip_timer = base.StartCoroutine(this.hideDelayed(pTimeout));
		this._hide_pos = Input.mousePosition;
	}

	// Token: 0x06003CC3 RID: 15555 RVA: 0x001A5597 File Offset: 0x001A3797
	private void clearHideTimer()
	{
		if (this._hide_tooltip_timer != null)
		{
			base.StopCoroutine(this._hide_tooltip_timer);
		}
		this._hide_tooltip_timer = null;
	}

	// Token: 0x06003CC4 RID: 15556 RVA: 0x001A55B4 File Offset: 0x001A37B4
	private IEnumerator hideDelayed(float pTimeout)
	{
		yield return new WaitForSecondsRealtime(pTimeout);
		this.hide();
		yield break;
	}

	// Token: 0x06003CC5 RID: 15557 RVA: 0x001A55CA File Offset: 0x001A37CA
	public Image getRawIcon(string pName)
	{
		Transform transform = base.transform.FindRecursive(pName);
		if (transform == null)
		{
			Debug.LogError("Icon not found " + pName);
		}
		return transform.GetComponent<Image>();
	}

	// Token: 0x06003CC6 RID: 15558 RVA: 0x001A55F6 File Offset: 0x001A37F6
	public void setRawIcon(string pName, Sprite pSprite)
	{
		Image rawIcon = this.getRawIcon(pName);
		if (rawIcon == null)
		{
			Debug.LogError("Icon not found " + pName);
		}
		rawIcon.sprite = pSprite;
	}

	// Token: 0x06003CC7 RID: 15559 RVA: 0x001A5620 File Offset: 0x001A3820
	public void setTitle(string pMainText, string pSubText = "", string pColorHex = "#F3961F")
	{
		string tFinalText = Toolbox.coloredText(pMainText, pColorHex, false);
		if (pSubText != "")
		{
			string tColor = Toolbox.makeDarkerColor(pColorHex, 0.8f);
			string tSubText = Toolbox.coloredText(LocalizedTextManager.getText(pSubText, null, false), tColor, false);
			tSubText = "<size=7>" + tSubText + "</size>";
			tFinalText = tFinalText + "\n" + tSubText;
		}
		this.name.text = tFinalText;
	}

	// Token: 0x06003CC8 RID: 15560 RVA: 0x001A5689 File Offset: 0x001A3889
	public Image getSpeciesIcon()
	{
		return this.getRawIcon("IconSpecies");
	}

	// Token: 0x06003CC9 RID: 15561 RVA: 0x001A5696 File Offset: 0x001A3896
	public void setSpeciesIcon(Sprite pSprite)
	{
		this.setRawIcon("IconSpecies", pSprite);
	}

	// Token: 0x04002C00 RID: 11264
	private static Dictionary<string, Tooltip> _dict_tooltips = new Dictionary<string, Tooltip>();

	// Token: 0x04002C01 RID: 11265
	private bool _sim_tooltip;

	// Token: 0x04002C02 RID: 11266
	private object _last_object;

	// Token: 0x04002C03 RID: 11267
	internal static float tweenTime = 0.08f;

	// Token: 0x04002C04 RID: 11268
	public Sprite tooltipTopGraphicsFlat;

	// Token: 0x04002C05 RID: 11269
	public Sprite tooltipTopGraphicsNormal;

	// Token: 0x04002C06 RID: 11270
	public Image topGraphics;

	// Token: 0x04002C07 RID: 11271
	private LayoutElement _headline;

	// Token: 0x04002C08 RID: 11272
	private VerticalLayoutGroup _layout_group;

	// Token: 0x04002C09 RID: 11273
	public Image background;

	// Token: 0x04002C0A RID: 11274
	public new Text name;

	// Token: 0x04002C0B RID: 11275
	public Text description;

	// Token: 0x04002C0C RID: 11276
	public Text description_2;

	// Token: 0x04002C0D RID: 11277
	public Text stats_description;

	// Token: 0x04002C0E RID: 11278
	public Text stats_values;

	// Token: 0x04002C0F RID: 11279
	public GameObject stats_container;

	// Token: 0x04002C10 RID: 11280
	private GameObject _description_container;

	// Token: 0x04002C11 RID: 11281
	private GameObject _description_2_container;

	// Token: 0x04002C12 RID: 11282
	internal List<TooltipOpinionInfo> opinion_list = new List<TooltipOpinionInfo>();

	// Token: 0x04002C13 RID: 11283
	internal ObjectPoolGenericMono<Image> pool_traits_actor;

	// Token: 0x04002C14 RID: 11284
	internal ObjectPoolGenericMono<TooltipOutlineItem> pool_equipments_actor;

	// Token: 0x04002C15 RID: 11285
	internal ObjectPoolGenericMono<Image> pool_traits_culture;

	// Token: 0x04002C16 RID: 11286
	internal ObjectPoolGenericMono<Image> pool_traits_language;

	// Token: 0x04002C17 RID: 11287
	internal ObjectPoolGenericMono<StatsIcon> pool_icons;

	// Token: 0x04002C18 RID: 11288
	internal ObjectPoolGenericMono<StatsIcon> pool_icons_2;

	// Token: 0x04002C19 RID: 11289
	[NonSerialized]
	public TooltipAsset asset;

	// Token: 0x04002C1A RID: 11290
	[NonSerialized]
	private string _type;

	// Token: 0x04002C1B RID: 11291
	public TooltipData data;

	// Token: 0x04002C1C RID: 11292
	private RectTransform _rect;

	// Token: 0x04002C1D RID: 11293
	private Coroutine _hide_tooltip_timer;

	// Token: 0x04002C1E RID: 11294
	private Vector2 _hide_pos;

	// Token: 0x04002C1F RID: 11295
	private float _last_height;

	// Token: 0x04002C20 RID: 11296
	private float _timeout;

	// Token: 0x04002C21 RID: 11297
	private float _timeout_animation;

	// Token: 0x04002C22 RID: 11298
	private float _clear_timeout;

	// Token: 0x04002C23 RID: 11299
	private const int TOOLTIP_WIDTH = 113;

	// Token: 0x04002C24 RID: 11300
	private const int CURSOR_MARGIN = 25;

	// Token: 0x04002C25 RID: 11301
	private bool _touch;
}
