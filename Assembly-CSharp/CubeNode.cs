using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Token: 0x02000706 RID: 1798
public class CubeNode : MonoBehaviour, IBeginDragHandler, IEventSystemHandler, IDragHandler, IEndDragHandler, IInitializePotentialDragHandler
{
	// Token: 0x17000332 RID: 818
	// (get) Token: 0x06003977 RID: 14711 RVA: 0x00199318 File Offset: 0x00197518
	public BaseUnlockableAsset current_asset
	{
		get
		{
			return this._data.asset;
		}
	}

	// Token: 0x06003978 RID: 14712 RVA: 0x00199325 File Offset: 0x00197525
	private void Start()
	{
		this._cube_overview = base.gameObject.GetComponentInParent<CubeOverview>();
		this.initClick();
		this.initTooltip();
	}

	// Token: 0x06003979 RID: 14713 RVA: 0x00199344 File Offset: 0x00197544
	public void update()
	{
		this._timer_change -= Time.deltaTime;
	}

	// Token: 0x0600397A RID: 14714 RVA: 0x00199358 File Offset: 0x00197558
	public void setDebugText(string pText)
	{
		this._text.text = pText;
	}

	// Token: 0x0600397B RID: 14715 RVA: 0x00199366 File Offset: 0x00197566
	public void clear()
	{
		this.connected_nodes.Clear();
		this.connections.Clear();
		this._timer_change = 0f;
	}

	// Token: 0x0600397C RID: 14716 RVA: 0x0019938C File Offset: 0x0019758C
	protected void initClick()
	{
		Button tButton;
		if (!base.TryGetComponent<Button>(out tButton))
		{
			return;
		}
		tButton.onClick.AddListener(new UnityAction(this.setPressed));
	}

	// Token: 0x0600397D RID: 14717 RVA: 0x001993BC File Offset: 0x001975BC
	protected void initTooltip()
	{
		TipButton tTipButton;
		if (!base.TryGetComponent<TipButton>(out tTipButton))
		{
			return;
		}
		Object.Destroy(tTipButton);
	}

	// Token: 0x0600397E RID: 14718 RVA: 0x001993DC File Offset: 0x001975DC
	private void showTooltip()
	{
		this._cube_overview.setLatestTouched(this);
		KnowledgeAsset tKnowledgeAsset = AssetManager.knowledge_library.get(this._data.knowledge_type);
		this._tooltip_data = tKnowledgeAsset.show_tooltip(base.transform, this._data.asset);
	}

	// Token: 0x0600397F RID: 14719 RVA: 0x0019942D File Offset: 0x0019762D
	public void setupAsset(CubeNodeAssetData pData)
	{
		if (this._timer_change > 0f)
		{
			return;
		}
		this._timer_change = 2f;
		this._data = pData;
		this._image.sprite = this._data.asset.getSprite();
	}

	// Token: 0x06003980 RID: 14720 RVA: 0x0019946A File Offset: 0x0019766A
	public void updateTooltip()
	{
		if (this.highlighted && Tooltip.isShowingFor(base.transform))
		{
			this._tooltip_data.tooltip_scale = Mathf.Lerp(0.4f, 1f, this.render_depth);
		}
	}

	// Token: 0x06003981 RID: 14721 RVA: 0x001994A1 File Offset: 0x001976A1
	public void setHighlighted()
	{
		if (this.highlighted)
		{
			return;
		}
		this.highlighted = true;
		this.scale_mod_spawn = 1.6f;
		this.showTooltip();
	}

	// Token: 0x06003982 RID: 14722 RVA: 0x001994C4 File Offset: 0x001976C4
	public void setPressed()
	{
		this._cube_overview.isDragging();
	}

	// Token: 0x06003983 RID: 14723 RVA: 0x001994D2 File Offset: 0x001976D2
	public void setColor(Color pColor)
	{
		this._image.color = pColor;
	}

	// Token: 0x06003984 RID: 14724 RVA: 0x001994E0 File Offset: 0x001976E0
	public void addConnection(CubeNode pNode, CubeNodeConnection pConnection)
	{
		this.connected_nodes.Add(pNode);
		this.connections.Add(pConnection);
	}

	// Token: 0x06003985 RID: 14725 RVA: 0x001994FA File Offset: 0x001976FA
	public void OnInitializePotentialDrag(PointerEventData pEventData)
	{
		CubeOverview cube_overview = this._cube_overview;
		if (cube_overview == null)
		{
			return;
		}
		cube_overview.SendMessage("OnInitializePotentialDrag", pEventData);
	}

	// Token: 0x06003986 RID: 14726 RVA: 0x00199512 File Offset: 0x00197712
	public void OnBeginDrag(PointerEventData pEventData)
	{
		CubeOverview cube_overview = this._cube_overview;
		if (cube_overview == null)
		{
			return;
		}
		cube_overview.SendMessage("OnBeginDrag", pEventData);
	}

	// Token: 0x06003987 RID: 14727 RVA: 0x0019952A File Offset: 0x0019772A
	public void OnDrag(PointerEventData pEventData)
	{
		CubeOverview cube_overview = this._cube_overview;
		if (cube_overview == null)
		{
			return;
		}
		cube_overview.SendMessage("OnDrag", pEventData);
	}

	// Token: 0x06003988 RID: 14728 RVA: 0x00199542 File Offset: 0x00197742
	public void OnEndDrag(PointerEventData pEventData)
	{
		CubeOverview cube_overview = this._cube_overview;
		if (cube_overview == null)
		{
			return;
		}
		cube_overview.SendMessage("OnEndDrag", pEventData);
	}

	// Token: 0x04002A60 RID: 10848
	private const float SCALE_HIGHLIGHTED = 1.6f;

	// Token: 0x04002A61 RID: 10849
	private const float SCALE_NORMAL = 1f;

	// Token: 0x04002A62 RID: 10850
	private const float TOOLTIP_SCALE_MIN = 0.4f;

	// Token: 0x04002A63 RID: 10851
	private const float TOOLTIP_SCALE_MAX = 1f;

	// Token: 0x04002A64 RID: 10852
	public Vector4 logical_pos;

	// Token: 0x04002A65 RID: 10853
	internal List<CubeNode> connected_nodes = new List<CubeNode>();

	// Token: 0x04002A66 RID: 10854
	private List<CubeNodeConnection> connections = new List<CubeNodeConnection>();

	// Token: 0x04002A67 RID: 10855
	[SerializeField]
	private Image _image;

	// Token: 0x04002A68 RID: 10856
	[SerializeField]
	private Text _text;

	// Token: 0x04002A69 RID: 10857
	private CubeOverview _cube_overview;

	// Token: 0x04002A6A RID: 10858
	internal float render_depth;

	// Token: 0x04002A6B RID: 10859
	internal float scale_mod_spawn = 1f;

	// Token: 0x04002A6C RID: 10860
	internal float bonus_scale = 1f;

	// Token: 0x04002A6D RID: 10861
	internal bool highlighted;

	// Token: 0x04002A6E RID: 10862
	private float _timer_change;

	// Token: 0x04002A6F RID: 10863
	private TooltipData _tooltip_data;

	// Token: 0x04002A70 RID: 10864
	private CubeNodeAssetData _data;
}
