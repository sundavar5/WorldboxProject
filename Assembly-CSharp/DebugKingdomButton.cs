using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005B3 RID: 1459
public class DebugKingdomButton : MonoBehaviour
{
	// Token: 0x17000285 RID: 645
	// (get) Token: 0x06003033 RID: 12339 RVA: 0x001751D8 File Offset: 0x001733D8
	public Image image
	{
		get
		{
			return this._image;
		}
	}

	// Token: 0x06003034 RID: 12340 RVA: 0x001751E0 File Offset: 0x001733E0
	public void setAsset(KingdomAsset pAsset)
	{
		this.kingdom_asset = pAsset;
		this._image.sprite = this.kingdom_asset.getSprite();
		this.setupTooltip();
		if (this.kingdom_asset.assets_discrepancies_bad != null)
		{
			this._discrepancy_have.gameObject.SetActive(true);
			return;
		}
		this._discrepancy_have.gameObject.SetActive(false);
	}

	// Token: 0x06003035 RID: 12341 RVA: 0x00175240 File Offset: 0x00173440
	public void checkSelected(KingdomAsset pAssetMain)
	{
		this._discrepancy_bad.gameObject.SetActive(false);
		this._discrepancy_normal.gameObject.SetActive(false);
		if (this.kingdom_asset == pAssetMain)
		{
			this.image.color = Color.white;
			return;
		}
		if (this.kingdom_asset.assets_discrepancies != null && this.kingdom_asset.assets_discrepancies.Contains(pAssetMain.id))
		{
			this._discrepancy_normal.gameObject.SetActive(true);
		}
		if (pAssetMain.assets_discrepancies_bad != null && pAssetMain.assets_discrepancies_bad.Contains(this.kingdom_asset.id))
		{
			this._discrepancy_bad.gameObject.SetActive(true);
		}
		if (pAssetMain.isFoe(this.kingdom_asset))
		{
			this.image.color = new Color(0.2f, 0.2f, 0.2f);
			return;
		}
		this.image.color = Color.white;
	}

	// Token: 0x06003036 RID: 12342 RVA: 0x00175330 File Offset: 0x00173530
	public void setupTooltip()
	{
		TipButton tTipButton;
		if (!base.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		tTipButton.hoverAction = new TooltipAction(this.showTooltip);
	}

	// Token: 0x06003037 RID: 12343 RVA: 0x0017535A File Offset: 0x0017355A
	private void showTooltip()
	{
		Tooltip.show(base.gameObject, "debug_kingdom_assets", new TooltipData
		{
			kingdom_asset = this.kingdom_asset
		});
	}

	// Token: 0x06003038 RID: 12344 RVA: 0x00175380 File Offset: 0x00173580
	public static void getTooltipDescription(KingdomAsset pAsset, out string pDescription, out string pDescription2)
	{
		pDescription = string.Empty;
		pDescription2 = string.Empty;
		if (pAsset.list_tags.Count > 0)
		{
			pDescription += "--- OWN TAGS ---\n".ColorHex(ColorStyleLibrary.m.color_text_grey_dark, false);
			foreach (string tTag in pAsset.list_tags)
			{
				pDescription += (tTag + "\n").ColorHex(ColorStyleLibrary.m.color_text_grey, false);
			}
		}
		if (pAsset.friendly_tags.Count > 0)
		{
			pDescription += "--- FRIENDLY TAGS ---\n".ColorHex(ColorStyleLibrary.m.color_text_grey_dark, false);
			foreach (string tTag2 in pAsset.friendly_tags)
			{
				pDescription += (tTag2 + "\n").ColorHex("#43FF43", false);
			}
		}
		if (pAsset.enemy_tags.Count > 0)
		{
			pDescription += "#--- ENEMY TAGS ---\n".ColorHex(ColorStyleLibrary.m.color_text_grey_dark, false);
			foreach (string tTag3 in pAsset.enemy_tags)
			{
				pDescription += (tTag3 + "\n").ColorHex("#FB2C21", false);
			}
		}
		if (pAsset.assets_discrepancies != null && pAsset.assets_discrepancies.Count > 0)
		{
			pDescription2 = string.Format("!! Discrepancies {0}!!\n", pAsset.assets_discrepancies.Count).ColorHex("#D85BC5", false);
			int tCount = 0;
			foreach (string tID in pAsset.assets_discrepancies)
			{
				if (tID.Contains(pAsset.id) || pAsset.id.Contains(tID))
				{
					pDescription2 += tID.ColorHex("#FB2C21", false);
				}
				else
				{
					pDescription2 += tID;
				}
				if (pDescription2.Length > 150)
				{
					int tLeft = pAsset.assets_discrepancies.Count - tCount;
					pDescription2 += string.Format(" and {0} more...!!!", tLeft).ColorHex("#8CFF99", false);
					break;
				}
				if (tCount < pAsset.assets_discrepancies.Count - 1)
				{
					pDescription2 += ", ";
				}
				tCount++;
			}
		}
	}

	// Token: 0x04002455 RID: 9301
	[SerializeField]
	private Button _button;

	// Token: 0x04002456 RID: 9302
	[SerializeField]
	private Image _image;

	// Token: 0x04002457 RID: 9303
	internal KingdomAsset kingdom_asset;

	// Token: 0x04002458 RID: 9304
	[SerializeField]
	private Image _discrepancy_bad;

	// Token: 0x04002459 RID: 9305
	[SerializeField]
	private Image _discrepancy_have;

	// Token: 0x0400245A RID: 9306
	[SerializeField]
	private Image _discrepancy_normal;
}
