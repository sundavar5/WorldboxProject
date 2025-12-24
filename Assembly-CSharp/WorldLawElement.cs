using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000847 RID: 2119
public class WorldLawElement : MonoBehaviour
{
	// Token: 0x170003BC RID: 956
	// (get) Token: 0x06004258 RID: 16984 RVA: 0x001C11AC File Offset: 0x001BF3AC
	private string _law_id
	{
		get
		{
			WorldLawAsset asset = this._asset;
			if (asset == null)
			{
				return null;
			}
			return asset.id;
		}
	}

	// Token: 0x06004259 RID: 16985 RVA: 0x001C11C0 File Offset: 0x001BF3C0
	public void init(WorldLawAsset pAsset)
	{
		this._initialized = true;
		this._asset = pAsset;
		if (!string.IsNullOrEmpty(this._asset.icon_path))
		{
			this._icon.sprite = SpriteTextureLoader.getSprite(this._asset.icon_path);
		}
		this._button.GetComponent<TipButton>().setHoverAction(delegate
		{
			if (!InputHelpers.mouseSupported)
			{
				return;
			}
			this.showTooltip();
		}, true);
		this._button.name = this._law_id;
		this._category = base.GetComponentInParent<WorldLawCategory>();
	}

	// Token: 0x0600425A RID: 16986 RVA: 0x001C1242 File Offset: 0x001BF442
	private void OnEnable()
	{
		if (!Config.game_loaded || SmoothLoader.isLoading() || !this._initialized)
		{
			return;
		}
		this.updateStatus();
	}

	// Token: 0x0600425B RID: 16987 RVA: 0x001C1264 File Offset: 0x001BF464
	public void click()
	{
		if (!InputHelpers.mouseSupported)
		{
			if (!Tooltip.isShowingFor(this))
			{
				this.showTooltip();
				return;
			}
			Tooltip.hideTooltipNow();
		}
		if (this._asset.requires_premium && !Config.hasPremium)
		{
			ScrollWindow.showWindow("premium_menu");
			return;
		}
		PlayerOptionData tOption = World.world.world_laws.dict[this._law_id];
		bool tLastEnabled = tOption.boolVal;
		if (this._asset.can_turn_off)
		{
			tOption.boolVal = !tOption.boolVal;
		}
		else if (!tOption.boolVal)
		{
			tOption.boolVal = true;
		}
		if (tOption.boolVal && !tLastEnabled)
		{
			PlayerOptionAction on_state_enabled = this._asset.on_state_enabled;
			if (on_state_enabled != null)
			{
				on_state_enabled(tOption);
			}
		}
		World.world.world_laws.updateCaches();
		PlayerOptionAction on_switch = tOption.on_switch;
		if (on_switch != null)
		{
			on_switch(tOption);
		}
		this.updateStatus();
	}

	// Token: 0x0600425C RID: 16988 RVA: 0x001C1343 File Offset: 0x001BF543
	private void showTooltip()
	{
		Tooltip.show(this, "world_law", new TooltipData
		{
			world_law = this._asset
		});
	}

	// Token: 0x0600425D RID: 16989 RVA: 0x001C1364 File Offset: 0x001BF564
	public void updateStatus()
	{
		bool tEnabled = this.isLawEnabled();
		this._selection.enabled = tEnabled;
		if (tEnabled)
		{
			this._icon.color = Color.white;
		}
		else
		{
			this._icon.color = Color.grey;
		}
		WorldLawCategory category = this._category;
		if (category == null)
		{
			return;
		}
		category.updateCounter();
	}

	// Token: 0x0600425E RID: 16990 RVA: 0x001C13B9 File Offset: 0x001BF5B9
	public void addListener(UnityAction pAction)
	{
		this._button.onClick.AddListener(pAction);
	}

	// Token: 0x0600425F RID: 16991 RVA: 0x001C13CC File Offset: 0x001BF5CC
	public void setSelectionColor(Color pColor)
	{
		this._selection.color = pColor;
	}

	// Token: 0x06004260 RID: 16992 RVA: 0x001C13DA File Offset: 0x001BF5DA
	public bool isLawEnabled()
	{
		return World.world.world_laws.dict[this._law_id].boolVal;
	}

	// Token: 0x0400306A RID: 12394
	[SerializeField]
	private Button _button;

	// Token: 0x0400306B RID: 12395
	[SerializeField]
	private Image _icon;

	// Token: 0x0400306C RID: 12396
	[SerializeField]
	private Image _selection;

	// Token: 0x0400306D RID: 12397
	private WorldLawCategory _category;

	// Token: 0x0400306E RID: 12398
	private WorldLawAsset _asset;

	// Token: 0x0400306F RID: 12399
	private bool _initialized;
}
