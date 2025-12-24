using System;
using System.Globalization;
using System.IO;
using Humanizer;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000737 RID: 1847
public class AutoSaveElement : MonoBehaviour, IPointerMoveHandler, IEventSystemHandler
{
	// Token: 0x06003AC4 RID: 15044 RVA: 0x0019F16F File Offset: 0x0019D36F
	private void Awake()
	{
		this._button.OnHoverOut(delegate()
		{
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			Tooltip.hideTooltip();
		});
	}

	// Token: 0x06003AC5 RID: 15045 RVA: 0x0019F19B File Offset: 0x0019D39B
	public void OnPointerMove(PointerEventData pData)
	{
		if (!InputHelpers.mouseSupported)
		{
			return;
		}
		if (Tooltip.anyActive())
		{
			return;
		}
		this.tooltipAction();
	}

	// Token: 0x06003AC6 RID: 15046 RVA: 0x0019F1B4 File Offset: 0x0019D3B4
	private void tooltipAction()
	{
		if (this._meta_data == null)
		{
			return;
		}
		if (!Config.tooltips_active)
		{
			return;
		}
		this._meta_data.temp_date_string = SaveManager.getMapCreationTime(this._world_path);
		Tooltip.show(this._button, "map_meta", new TooltipData
		{
			map_meta = this._meta_data
		});
	}

	// Token: 0x06003AC7 RID: 15047 RVA: 0x0019F20C File Offset: 0x0019D40C
	public void load(AutoSaveData pData)
	{
		this._world_path = pData.path;
		string tPathPreviewImage = SaveManager.generatePngSmallPreviewPath(pData.path);
		if (!string.IsNullOrEmpty(tPathPreviewImage) && File.Exists(tPathPreviewImage))
		{
			byte[] pngBytes = File.ReadAllBytes(tPathPreviewImage);
			Texture2D tTexture = new Texture2D(32, 32);
			tTexture.anisoLevel = 0;
			tTexture.filterMode = FilterMode.Point;
			if (tTexture.LoadImage(pngBytes))
			{
				Sprite sprite_small_preview = Sprite.Create(tTexture, new Rect(0f, 0f, 32f, 32f), new Vector2(0.5f, 0.5f));
				this._preview.sprite = sprite_small_preview;
			}
		}
		this._meta_data = SaveManager.getMetaFor(pData.path);
		this._save_name.text = this._meta_data.mapStats.name;
		this._save_name.color = this._meta_data.mapStats.getArchitectMood().getColorText();
		this._kingdoms.setValue(this._meta_data.kingdoms, "");
		this._cities.setValue(this._meta_data.cities, "");
		this._population.setValue(this._meta_data.population, "");
		this._mobs.setValue(this._meta_data.mobs, "");
		this._age.setValue(Date.getYear(this._meta_data.mapStats.world_time), "");
		string result = "";
		string tLang = "";
		try
		{
			DateTime d = Epoch.toDateTime(pData.timestamp);
			CultureInfo tCulture = LocalizedTextManager.getCulture(null);
			DateTime tFutureTime = DateTime.UtcNow.AddDays(7.0);
			if (d.Year < 2017)
			{
				result = "GREG";
			}
			else if (d > tFutureTime)
			{
				result = "DREDD";
			}
			else if (LocalizedTextManager.cultureSupported())
			{
				DateTime input = d;
				bool utcDate = true;
				CultureInfo culture = tCulture;
				result = input.Humanize(utcDate, null, culture);
			}
			else
			{
				string tShortDatePattern = tCulture.DateTimeFormat.ShortDatePattern;
				result = d.ToString(tShortDatePattern, tCulture);
			}
		}
		catch (Exception message)
		{
			Debug.Log("failed with " + tLang);
			Debug.LogError(message);
		}
		this._save_time_ago.text = result;
		base.gameObject.name = "AutoSaveElement_" + pData.timestamp.ToString();
	}

	// Token: 0x06003AC8 RID: 15048 RVA: 0x0019F47C File Offset: 0x0019D67C
	public void clickLoadAutoSave()
	{
		SaveManager.setCurrentPath(this._world_path);
		ScrollWindow.showWindow("load_world");
	}

	// Token: 0x06003AC9 RID: 15049 RVA: 0x0019F493 File Offset: 0x0019D693
	private void OnDisable()
	{
		this._meta_data = null;
		if (this._preview != null)
		{
			this._preview.sprite = null;
		}
	}

	// Token: 0x04002B72 RID: 11122
	[SerializeField]
	private Image _preview;

	// Token: 0x04002B73 RID: 11123
	[SerializeField]
	private Text _save_name;

	// Token: 0x04002B74 RID: 11124
	[SerializeField]
	private Text _save_time_ago;

	// Token: 0x04002B75 RID: 11125
	[SerializeField]
	private CountUpOnClick _kingdoms;

	// Token: 0x04002B76 RID: 11126
	[SerializeField]
	private CountUpOnClick _cities;

	// Token: 0x04002B77 RID: 11127
	[SerializeField]
	private CountUpOnClick _population;

	// Token: 0x04002B78 RID: 11128
	[SerializeField]
	private CountUpOnClick _mobs;

	// Token: 0x04002B79 RID: 11129
	[SerializeField]
	private CountUpOnClick _age;

	// Token: 0x04002B7A RID: 11130
	[SerializeField]
	private Button _button;

	// Token: 0x04002B7B RID: 11131
	[SerializeField]
	private GameObject _premium_icon;

	// Token: 0x04002B7C RID: 11132
	private string _world_path;

	// Token: 0x04002B7D RID: 11133
	private MapMetaData _meta_data;
}
