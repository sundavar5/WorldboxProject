using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

// Token: 0x02000709 RID: 1801
public class CubeOverview : MonoBehaviour, IInitializePotentialDragHandler, IEventSystemHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
	// Token: 0x0600398F RID: 14735 RVA: 0x0019962D File Offset: 0x0019782D
	protected void Awake()
	{
		this._pool_nodes = new ObjectPoolGenericMono<CubeNode>(this._prefab_node, this._parent_nodes);
		this._pool_connections = new ObjectPoolGenericMono<CubeNodeConnection>(this._prefab_connection, this._parent_connections);
	}

	// Token: 0x06003990 RID: 14736 RVA: 0x00199660 File Offset: 0x00197860
	private void initStartPositions()
	{
		for (int i = 0; i < CubeOverview._hypercube_positions.Length; i++)
		{
			CubeNodeAssetData tAsset = this._all_available_assets.GetRandom<CubeNodeAssetData>();
			CubeNode tNode = this._pool_nodes.getNext();
			tNode.setupAsset(tAsset);
			tNode.logical_pos = CubeOverview._hypercube_positions[i];
			tNode.setDebugText(i.ToString() ?? "");
			tNode.gameObject.name = i.ToString();
			this._nodes.Add(tNode);
			this._nodes_by_index.Add(tNode);
		}
		this.updateNodesVisual();
	}

	// Token: 0x06003991 RID: 14737 RVA: 0x001996F8 File Offset: 0x001978F8
	private void prepareConnections()
	{
		for (int i = 0; i < CubeOverview._hypercube_connections.GetLength(0); i++)
		{
			int tIndexA = CubeOverview._hypercube_connections[i, 0];
			int tIndexB = CubeOverview._hypercube_connections[i, 1];
			CubeNode tNode = this._nodes_by_index[tIndexA];
			CubeNode tNode2 = this._nodes_by_index[tIndexB];
			this.makeConnection(tNode, tNode2);
		}
	}

	// Token: 0x06003992 RID: 14738 RVA: 0x0019975C File Offset: 0x0019795C
	private Vector3 project4Dto3D(Vector4 p)
	{
		float tDist = this._perspective_strength_main * this._perspective_strength_main_mod;
		float tPull = Mathf.Exp(-Mathf.Abs(p.w) * this._mod_lense);
		float tPw = p.w;
		float tP_WWarp = Mathf.Sin(tPw * this._mod_warp);
		if (this._mod_warp > 0f)
		{
			tPw = tP_WWarp;
		}
		float tDivisor = tDist - tPw;
		if (Mathf.Abs(tDivisor) < 0.01f)
		{
			tDivisor = 0.01f * Mathf.Sign(tDivisor);
		}
		float tWFactor;
		if (tDivisor != 0f)
		{
			tWFactor = tDist / tDivisor;
		}
		else
		{
			tWFactor = 0f;
		}
		tWFactor *= tPull;
		return new Vector3(p.x * tWFactor * this._perspective_strength_x, p.y * tWFactor * this._perspective_strength_y, p.z * tWFactor * this._perspective_strength_z);
	}

	// Token: 0x06003993 RID: 14739 RVA: 0x0019982C File Offset: 0x00197A2C
	private void updateRotationAndSpeeds()
	{
		if (!this._is_dragging)
		{
			this._angle_4d += Time.deltaTime * this._speed_mod_4d;
		}
		if (Input.GetMouseButton(0))
		{
			this._perspective_strength_main = Mathf.Lerp(this._perspective_strength_main, 4f, 0.1f);
		}
		else
		{
			this._perspective_strength_main = Mathf.Lerp(this._perspective_strength_main, 3f, 0.1f);
		}
		float tTargetRotationInner_x = -this._offset_x;
		float tTargetRotationInner_y = -this._offset_y;
		float tTargetRotationOuter_x = this._offset_y;
		float tTargetRotationOuter_y = this._offset_y;
		if (!this._is_dragging)
		{
			tTargetRotationInner_x += this._speed_mod_inner;
			tTargetRotationInner_y += this._speed_mod_inner;
			tTargetRotationOuter_x += this._speed_mod_outer;
			tTargetRotationOuter_y += this._speed_mod_outer;
		}
		Quaternion tCombinedRotationSpeedInner = Quaternion.Euler(tTargetRotationInner_x, tTargetRotationInner_y, 0f);
		this._rotation_q = tCombinedRotationSpeedInner * this._rotation_q;
		Quaternion tCombinedRotationSpeedOuter = Quaternion.Euler(tTargetRotationOuter_x, tTargetRotationOuter_y, 0f);
		this._rotation_q_2 = tCombinedRotationSpeedOuter * this._rotation_q_2;
	}

	// Token: 0x06003994 RID: 14740 RVA: 0x00199928 File Offset: 0x00197B28
	private void updateNodesVisual()
	{
		float tSpeed = this._angle_4d;
		foreach (CubeNode tNode in this._nodes)
		{
			bool flag = tNode.logical_pos.w < 0f;
			float tSpacing = this.spacing * this._spacing_mod;
			Vector4 tRotatedLogicalPos = this.rotate4D(tNode.logical_pos, tSpeed);
			Vector3 tProjectedPos = this.project4Dto3D(tRotatedLogicalPos) * tSpacing;
			Vector3 tRotatedPosition = (flag ? this._rotation_q : this._rotation_q_2) * tProjectedPos;
			tNode.transform.localPosition = tRotatedPosition;
			this.calculateNodeDepth(tNode, this.RADIUS_NODE_PLACEMENT);
			this.updateNodeColorAndScale(tNode);
		}
		this.sortNodesByDepth();
	}

	// Token: 0x06003995 RID: 14741 RVA: 0x00199A00 File Offset: 0x00197C00
	private Vector4 rotate4D(Vector4 pPoint, float pAngle)
	{
		float tCos = Mathf.Cos(pAngle);
		float tSin = Mathf.Sin(pAngle);
		float x = pPoint.x * tCos - pPoint.w * tSin;
		float tNewW = pPoint.x * tSin + pPoint.w * tCos;
		float tNewY = pPoint.y * tCos - pPoint.z * tSin;
		float tNewZ = pPoint.y * tSin + pPoint.z * tCos;
		return new Vector4(x, tNewY, tNewZ, tNewW);
	}

	// Token: 0x06003996 RID: 14742 RVA: 0x00199A6C File Offset: 0x00197C6C
	protected void OnEnable()
	{
		this._object_main.transform.DOKill(false);
		this._object_main.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		this._object_main.transform.DOScale(1f, 0.6f).SetEase(Ease.OutBack);
		this.fillAssets();
		this.clearContent();
		this.initStartPositions();
		this.prepareConnections();
		this._is_dragging = false;
	}

	// Token: 0x06003997 RID: 14743 RVA: 0x00199AF0 File Offset: 0x00197CF0
	private CubeNodeConnection makeConnection(CubeNode pNode1, CubeNode pNode2)
	{
		CubeNodeConnection tConnection = this._pool_connections.getNext();
		tConnection.node_1 = pNode1;
		tConnection.node_2 = pNode2;
		pNode1.addConnection(pNode2, tConnection);
		pNode2.addConnection(pNode1, tConnection);
		if (pNode1.logical_pos.w < 0f && pNode2.logical_pos.w < 0f)
		{
			tConnection.setConnection(true);
		}
		else
		{
			tConnection.setConnection(false);
		}
		tConnection.gameObject.name = "connection " + pNode1.gameObject.name + "-" + pNode2.gameObject.name;
		return tConnection;
	}

	// Token: 0x06003998 RID: 14744 RVA: 0x00199B8C File Offset: 0x00197D8C
	private void fillAssets()
	{
		this._all_available_assets.Clear();
		if (this._filter_asset != null)
		{
			this.loadUnlockables(this._filter_asset.get_library(), this._filter_asset.id);
			this._filter_asset = null;
			return;
		}
		this.loadUnlockables(AssetManager.actor_library, "units");
		this.loadUnlockables(AssetManager.items, "items");
		this.loadUnlockables(AssetManager.gene_library, "genes");
		this.loadUnlockables(AssetManager.traits, "traits");
		this.loadUnlockables(AssetManager.subspecies_traits, "subspecies_traits");
		this.loadUnlockables(AssetManager.culture_traits, "culture_traits");
		this.loadUnlockables(AssetManager.language_traits, "language_traits");
		this.loadUnlockables(AssetManager.clan_traits, "clan_traits");
		this.loadUnlockables(AssetManager.religion_traits, "religion_traits");
		this.loadUnlockables(AssetManager.kingdoms_traits, "kingdom_traits");
		this.loadUnlockables(AssetManager.plots_library, "plots");
	}

	// Token: 0x06003999 RID: 14745 RVA: 0x00199C88 File Offset: 0x00197E88
	private void loadUnlockables(ILibraryWithUnlockables pLibrary, string pType)
	{
		foreach (BaseUnlockableAsset tAsset in pLibrary.elements_list)
		{
			if (tAsset.show_in_knowledge_window && !tAsset.isTemplateAsset())
			{
				this._all_available_assets.Add(new CubeNodeAssetData(tAsset, pType));
			}
		}
	}

	// Token: 0x0600399A RID: 14746 RVA: 0x00199CF0 File Offset: 0x00197EF0
	private void Update()
	{
		if (InputHelpers.mouseSupported || this._latest_touched_node == null || !Tooltip.isShowingFor(this._latest_touched_node.transform))
		{
			this.updateRotationAndSpeeds();
		}
		foreach (CubeNode cubeNode in this._nodes)
		{
			cubeNode.update();
		}
		if (!this._is_dragging)
		{
			this.smoothOffsets();
			this._active_node = this.getHighlightedNode();
			this.highlightNode(this._active_node);
		}
		this.updateNodesVisual();
		this.updateConnectionPositions();
		this.updateKnobs();
	}

	// Token: 0x0600399B RID: 14747 RVA: 0x00199DAC File Offset: 0x00197FAC
	private void updateKnobs()
	{
		float tModSPeed = 0.05f;
		if (this._knob_perspective_strength_main != null)
		{
			float tMod = this._knob_perspective_strength_main.getDragMod() * 0.03f;
			this._perspective_strength_main_mod += tMod * tModSPeed;
			this._perspective_strength_main_mod = Mathf.Clamp(this._perspective_strength_main_mod, 0.1f, 1f);
		}
		if (this._knob_perspective_strength_x != null)
		{
			float tMod2 = this._knob_perspective_strength_x.getDragMod();
			this._perspective_strength_x += tMod2 * tModSPeed;
			this._perspective_strength_x = Mathf.Clamp(this._perspective_strength_x, 0f, 2f);
		}
		if (this._knob_perspective_strength_y != null)
		{
			float tMod3 = this._knob_perspective_strength_y.getDragMod();
			this._perspective_strength_y += tMod3 * tModSPeed;
			this._perspective_strength_y = Mathf.Clamp(this._perspective_strength_y, 0f, 2f);
		}
		if (this._knob_perspective_strength_z != null)
		{
			float tMod4 = this._knob_perspective_strength_z.getDragMod();
			this._perspective_strength_z += tMod4 * tModSPeed;
			this._perspective_strength_z = Mathf.Clamp(this._perspective_strength_z, 0f, 2f);
		}
		if (this._knob_spacing != null)
		{
			float tMod5 = this._knob_spacing.getDragMod();
			this._spacing_mod += tMod5 * tModSPeed;
			this._spacing_mod = Mathf.Clamp(this._spacing_mod, 0f, 3f);
		}
		if (this._knob_warp != null)
		{
			float tMod6 = this._knob_warp.getDragMod();
			this._mod_warp += tMod6 * tModSPeed;
			this._mod_warp = Mathf.Clamp(this._mod_warp, 0f, 10f);
		}
		if (this._knob_lense != null)
		{
			float tMod7 = this._knob_lense.getDragMod();
			this._mod_lense += tMod7 * tModSPeed;
			this._mod_lense = Mathf.Clamp(this._mod_lense, 0f, 2f);
		}
		if (this._knob_speed_outer != null)
		{
			float tMod8 = this._knob_speed_outer.getDragMod();
			this._speed_mod_outer += tMod8 * tModSPeed;
			this._speed_mod_outer = Mathf.Clamp(this._speed_mod_outer, 0f, 20f);
		}
		if (this._knob_speed_inner != null)
		{
			float tMod9 = this._knob_speed_inner.getDragMod();
			this._speed_mod_inner += tMod9 * tModSPeed;
			this._speed_mod_inner = Mathf.Clamp(this._speed_mod_inner, 0f, 20f);
		}
		if (this._knob_connection_size != null)
		{
			float tMod10 = this._knob_connection_size.getDragMod();
			this._mod_connection_size += tMod10 * tModSPeed;
			this._mod_connection_size = Mathf.Clamp(this._mod_connection_size, 0f, 10f);
		}
		if (this._knob_icon_size != null)
		{
			float tMod11 = this._knob_icon_size.getDragMod();
			this._mod_node_size += tMod11 * tModSPeed;
			this._mod_node_size = Mathf.Clamp(this._mod_node_size, 0f, 20f);
		}
		if (this._knob_speed_4d != null)
		{
			float tMod12 = this._knob_speed_4d.getDragMod();
			this._speed_mod_4d += tMod12 * tModSPeed;
			this._speed_mod_4d = Mathf.Clamp(this._speed_mod_4d, 0f, 20f);
		}
		if (this._knob_reset != null)
		{
			float tMod13 = this._knob_reset.getDragMod();
			tMod13 = Math.Abs(tMod13);
			this._perspective_strength_main = Mathf.Lerp(this._perspective_strength_main, 3f, tMod13 * tModSPeed);
			this._perspective_strength_x = Mathf.Lerp(this._perspective_strength_x, 1f, tMod13 * tModSPeed);
			this._perspective_strength_y = Mathf.Lerp(this._perspective_strength_y, 1f, tMod13 * tModSPeed);
			this._perspective_strength_z = Mathf.Lerp(this._perspective_strength_z, 1f, tMod13 * tModSPeed);
			this._spacing_mod = Mathf.Lerp(this._spacing_mod, 1f, tMod13 * tModSPeed);
			this._speed_mod_outer = Mathf.Lerp(this._speed_mod_outer, 0.2f, tMod13 * tModSPeed);
			this._speed_mod_inner = Mathf.Lerp(this._speed_mod_inner, 0.2f, tMod13 * tModSPeed);
			this._speed_mod_4d = Mathf.Lerp(this._speed_mod_4d, 0.3f, tMod13 * tModSPeed);
			this._mod_connection_size = Mathf.Lerp(this._mod_connection_size, 1f, tMod13 * tModSPeed);
			this._mod_node_size = Mathf.Lerp(this._mod_node_size, 1f, tMod13 * tModSPeed);
			this._perspective_strength_main_mod = Mathf.Lerp(this._perspective_strength_main_mod, 1f, tMod13 * tModSPeed);
			this._mod_warp = Mathf.Lerp(this._mod_warp, 0f, tMod13 * tModSPeed);
			this._mod_lense = Mathf.Lerp(this._mod_lense, 0f, tMod13 * tModSPeed);
		}
	}

	// Token: 0x0600399C RID: 14748 RVA: 0x0019A28C File Offset: 0x0019848C
	private void updateConnectionPositions()
	{
		foreach (CubeNodeConnection cubeNodeConnection in this._pool_connections.getListTotal())
		{
			cubeNodeConnection.update();
			float tScaleMod = 1f;
			CubeNode tNode = cubeNodeConnection.node_1;
			CubeNode tNode2 = cubeNodeConnection.node_2;
			if (cubeNodeConnection.inner_cube)
			{
				tScaleMod = 3f;
			}
			if (tNode.highlighted || tNode2.highlighted)
			{
				tScaleMod = 6f;
			}
			tScaleMod *= this._mod_connection_size;
			float tDepthNodes;
			if (tNode.render_depth > tNode2.render_depth)
			{
				tDepthNodes = tNode.render_depth;
			}
			else
			{
				tDepthNodes = tNode2.render_depth;
			}
			Color tColor = Color.Lerp(this._color_connection_back, this._color_connection_default, tDepthNodes);
			cubeNodeConnection.image.color = tColor;
			Vector2 tPos = tNode.transform.localPosition;
			Vector2 tPos2 = tNode2.transform.localPosition;
			Vector2 tPos3 = (tPos + tPos2) / 2f;
			cubeNodeConnection.transform.localPosition = tPos3;
			float tDistance = Vector3.Distance(tPos, tPos2);
			cubeNodeConnection.transform.localScale = new Vector3(tDistance, tScaleMod, 1f);
			Vector3 tDirection = tPos2 - tPos;
			float tAngle = Mathf.Atan2(tDirection.y, tDirection.x) * 57.29578f;
			cubeNodeConnection.transform.rotation = Quaternion.Euler(0f, 0f, tAngle);
		}
	}

	// Token: 0x0600399D RID: 14749 RVA: 0x0019A430 File Offset: 0x00198630
	public CubeNodeAssetData getRandom()
	{
		return this._all_available_assets.GetRandom<CubeNodeAssetData>();
	}

	// Token: 0x0600399E RID: 14750 RVA: 0x0019A43D File Offset: 0x0019863D
	public void setLatestTouched(CubeNode pNode)
	{
		this._latest_touched_node = pNode;
	}

	// Token: 0x0600399F RID: 14751 RVA: 0x0019A446 File Offset: 0x00198646
	public void setFilterAsset(KnowledgeAsset pAsset)
	{
		this._filter_asset = pAsset;
	}

	// Token: 0x060039A0 RID: 14752 RVA: 0x0019A450 File Offset: 0x00198650
	private void highlightAllConnectonsFromDrag(float pLight)
	{
		foreach (CubeNodeConnection tConnection in this._pool_connections.getListTotal())
		{
			if (tConnection.mod_light <= pLight)
			{
				tConnection.mod_light = pLight;
			}
		}
	}

	// Token: 0x060039A1 RID: 14753 RVA: 0x0019A4AC File Offset: 0x001986AC
	private void highlightNode(CubeNode pHighlighted = null)
	{
		foreach (CubeNode tNode in this._nodes)
		{
			if (!(tNode == pHighlighted) && tNode.highlighted)
			{
				tNode.highlighted = false;
				Tooltip.hideTooltipNow();
			}
		}
		if (pHighlighted != null)
		{
			pHighlighted.setHighlighted();
		}
	}

	// Token: 0x060039A2 RID: 14754 RVA: 0x0019A520 File Offset: 0x00198720
	private CubeNode getClosestNodeToCursor()
	{
		CubeNode tResult = null;
		float tBestDist = float.MaxValue;
		Vector2 tCursorPosition = Input.mousePosition;
		if (!InputHelpers.mouseSupported && InputHelpers.touchCount == 0)
		{
			return this._active_node;
		}
		foreach (CubeNode tNode in this._nodes)
		{
			Vector2 tPos = tNode.transform.position;
			float tDist = Vector2.Distance(tCursorPosition, tPos);
			if (tDist <= 40f)
			{
				if (tNode == this._active_node)
				{
					return tNode;
				}
				if (tDist < tBestDist)
				{
					tBestDist = tDist;
					tResult = tNode;
				}
			}
		}
		return tResult;
	}

	// Token: 0x060039A3 RID: 14755 RVA: 0x0019A5E0 File Offset: 0x001987E0
	private void smoothOffsets()
	{
		this._offset_x = Mathf.Lerp(this._offset_x, this._offset_target_x, 0.1f);
		this._offset_y = Mathf.Lerp(this._offset_y, this._offset_target_y, 0.1f);
	}

	// Token: 0x060039A4 RID: 14756 RVA: 0x0019A61A File Offset: 0x0019881A
	internal bool isDragging()
	{
		return this._is_dragging;
	}

	// Token: 0x060039A5 RID: 14757 RVA: 0x0019A624 File Offset: 0x00198824
	private void calculateNodeDepth(CubeNode pElement, float pRadius)
	{
		float zPosition = pElement.transform.localPosition.z;
		float tDepthFactor = Mathf.InverseLerp(-pRadius, pRadius, zPosition);
		pElement.render_depth = tDepthFactor;
	}

	// Token: 0x060039A6 RID: 14758 RVA: 0x0019A654 File Offset: 0x00198854
	private void sortNodesByDepth()
	{
		foreach (CubeNode cubeNode in this._nodes)
		{
			cubeNode.transform.SetAsLastSibling();
		}
		this._nodes.Sort((CubeNode a, CubeNode b) => a.render_depth.CompareTo(b.render_depth));
	}

	// Token: 0x060039A7 RID: 14759 RVA: 0x0019A6D4 File Offset: 0x001988D4
	private void clearContent()
	{
		foreach (CubeNode cubeNode in this._nodes)
		{
			cubeNode.clear();
		}
		foreach (CubeNodeConnection cubeNodeConnection in this._pool_connections.getListTotal())
		{
			cubeNodeConnection.clear();
		}
		this._rotation_q = Quaternion.identity;
		this._rotation_q_2 = Quaternion.identity;
		this._pool_connections.clear(true);
		this._pool_nodes.clear(true);
		this._nodes.Clear();
		this._nodes_by_index.Clear();
	}

	// Token: 0x060039A8 RID: 14760 RVA: 0x0019A7A8 File Offset: 0x001989A8
	public void OnDrag(PointerEventData eventData)
	{
		this._is_dragging = true;
		Vector2 tMouseDelta = eventData.delta;
		if (tMouseDelta.magnitude > this._last_mouse_delta.magnitude)
		{
			this.highlightAllConnectonsFromDrag(0.35f);
		}
		this._last_mouse_delta = tMouseDelta;
		this._offset_x = -tMouseDelta.y * 0.46f;
		this._offset_y = tMouseDelta.x * 0.46f;
	}

	// Token: 0x060039A9 RID: 14761 RVA: 0x0019A810 File Offset: 0x00198A10
	public void OnEndDrag(PointerEventData eventData)
	{
		this._is_dragging = false;
		Vector2 tMouseDelta = eventData.delta;
		this._offset_target_x += -tMouseDelta.y * 0.005f;
		this._offset_target_y += tMouseDelta.x * 0.005f;
		if (Mathf.Abs(this._offset_target_x) > 0.7f || Mathf.Abs(this._offset_target_y) > 0.7f)
		{
			if (Mathf.Abs(this._offset_target_x) > Mathf.Abs(this._offset_target_y))
			{
				this._offset_target_y = this._offset_target_y / Mathf.Abs(this._offset_target_x) * 0.7f;
			}
			else
			{
				this._offset_target_x = this._offset_target_x / Mathf.Abs(this._offset_target_y) * 0.7f;
			}
		}
		this._offset_target_x = Mathf.Clamp(this._offset_target_x, -0.7f, 0.7f);
		this._offset_target_y = Mathf.Clamp(this._offset_target_y, -0.7f, 0.7f);
		this.highlightAllConnectonsFromDrag(1f);
	}

	// Token: 0x060039AA RID: 14762 RVA: 0x0019A919 File Offset: 0x00198B19
	public void OnInitializePotentialDrag(PointerEventData eventData)
	{
		eventData.useDragThreshold = false;
		this._last_mouse_delta = Vector2.zero;
	}

	// Token: 0x060039AB RID: 14763 RVA: 0x0019A930 File Offset: 0x00198B30
	public void OnBeginDrag(PointerEventData eventData)
	{
		this._offset_x = (this._offset_target_x = 0f);
		this._offset_y = (this._offset_target_y = 0f);
		Tooltip.hideTooltipNow();
	}

	// Token: 0x060039AC RID: 14764 RVA: 0x0019A96C File Offset: 0x00198B6C
	private void updateNodeColorAndScale(CubeNode pNode)
	{
		Color tColor;
		if (pNode.current_asset.isUnlockedByPlayer())
		{
			if (pNode.highlighted)
			{
				tColor = Color.Lerp(this._color_node_back, this._node_highlighted, pNode.render_depth);
			}
			else
			{
				tColor = Color.Lerp(this._color_node_back, this._color_node_front, pNode.render_depth);
			}
		}
		else
		{
			tColor = Toolbox.color_black;
		}
		pNode.setColor(tColor);
		float tScale = Mathf.Lerp(0.4f, 1.2f, pNode.render_depth);
		if (Mathf.Approximately(tScale, 0.4f))
		{
			pNode.setupAsset(this.getRandom());
		}
		tScale *= pNode.scale_mod_spawn * pNode.bonus_scale;
		tScale *= this._mod_node_size;
		pNode.transform.localScale = new Vector3(tScale, tScale, tScale);
		pNode.updateTooltip();
	}

	// Token: 0x060039AD RID: 14765 RVA: 0x0019AA30 File Offset: 0x00198C30
	private CubeNode getHighlightedNode()
	{
		if (this._is_dragging)
		{
			return null;
		}
		if (this._offset_x > 1.05f || this._offset_x < -1.05f)
		{
			return null;
		}
		if (this._offset_y > 1.05f || this._offset_y < -1.05f)
		{
			return null;
		}
		return this.getClosestNodeToCursor();
	}

	// Token: 0x04002A7A RID: 10874
	[SerializeField]
	private DragSnapElement _knob_perspective_strength_x;

	// Token: 0x04002A7B RID: 10875
	[SerializeField]
	private DragSnapElement _knob_perspective_strength_y;

	// Token: 0x04002A7C RID: 10876
	[SerializeField]
	private DragSnapElement _knob_perspective_strength_z;

	// Token: 0x04002A7D RID: 10877
	[SerializeField]
	private DragSnapElement _knob_perspective_strength_main;

	// Token: 0x04002A7E RID: 10878
	[SerializeField]
	private DragSnapElement _knob_warp;

	// Token: 0x04002A7F RID: 10879
	[SerializeField]
	private DragSnapElement _knob_lense;

	// Token: 0x04002A80 RID: 10880
	[SerializeField]
	private DragSnapElement _knob_spacing;

	// Token: 0x04002A81 RID: 10881
	[SerializeField]
	private DragSnapElement _knob_speed_outer;

	// Token: 0x04002A82 RID: 10882
	[SerializeField]
	private DragSnapElement _knob_speed_inner;

	// Token: 0x04002A83 RID: 10883
	[SerializeField]
	private DragSnapElement _knob_speed_4d;

	// Token: 0x04002A84 RID: 10884
	[SerializeField]
	private DragSnapElement _knob_icon_size;

	// Token: 0x04002A85 RID: 10885
	[SerializeField]
	private DragSnapElement _knob_connection_size;

	// Token: 0x04002A86 RID: 10886
	[SerializeField]
	private DragSnapElement _knob_reset;

	// Token: 0x04002A87 RID: 10887
	private CubeNode _active_node;

	// Token: 0x04002A88 RID: 10888
	[SerializeField]
	private CubeNode _prefab_node;

	// Token: 0x04002A89 RID: 10889
	[SerializeField]
	private CubeNodeConnection _prefab_connection;

	// Token: 0x04002A8A RID: 10890
	[SerializeField]
	private RectTransform _parent_connections;

	// Token: 0x04002A8B RID: 10891
	[SerializeField]
	private RectTransform _parent_nodes;

	// Token: 0x04002A8C RID: 10892
	[SerializeField]
	private GameObject _object_main;

	// Token: 0x04002A8D RID: 10893
	private float _offset_target_x = -0.015f;

	// Token: 0x04002A8E RID: 10894
	private float _offset_target_y = 0.07f;

	// Token: 0x04002A8F RID: 10895
	private bool _is_dragging;

	// Token: 0x04002A90 RID: 10896
	private Vector2 _last_mouse_delta;

	// Token: 0x04002A91 RID: 10897
	private float _offset_x;

	// Token: 0x04002A92 RID: 10898
	private float _offset_y;

	// Token: 0x04002A93 RID: 10899
	internal bool highlighted;

	// Token: 0x04002A94 RID: 10900
	private List<CubeNode> _nodes_by_index = new List<CubeNode>();

	// Token: 0x04002A95 RID: 10901
	private List<CubeNode> _nodes = new List<CubeNode>();

	// Token: 0x04002A96 RID: 10902
	private ObjectPoolGenericMono<CubeNode> _pool_nodes;

	// Token: 0x04002A97 RID: 10903
	private ObjectPoolGenericMono<CubeNodeConnection> _pool_connections;

	// Token: 0x04002A98 RID: 10904
	private Quaternion _rotation_q = Quaternion.identity;

	// Token: 0x04002A99 RID: 10905
	private Quaternion _rotation_q_2 = Quaternion.identity;

	// Token: 0x04002A9A RID: 10906
	private List<CubeNodeAssetData> _all_available_assets = new List<CubeNodeAssetData>();

	// Token: 0x04002A9B RID: 10907
	private CubeNode _latest_touched_node;

	// Token: 0x04002A9C RID: 10908
	private KnowledgeAsset _filter_asset;

	// Token: 0x04002A9D RID: 10909
	private float _angle_4d;

	// Token: 0x04002A9E RID: 10910
	private const float DRAGGING_SMOOTHING_TIME = 0.1f;

	// Token: 0x04002A9F RID: 10911
	private const float ROTATION_BOUNDS = 0.7f;

	// Token: 0x04002AA0 RID: 10912
	private const float ROTATION_BOUNDS_MARGIN = 1.05f;

	// Token: 0x04002AA1 RID: 10913
	private const float DRAG_SPEED = 0.46f;

	// Token: 0x04002AA2 RID: 10914
	private const float DRAG_ROTATE_SPEED = 0.005f;

	// Token: 0x04002AA3 RID: 10915
	private const float MIN_NODE_CURSOR_DISTANCE = 40f;

	// Token: 0x04002AA4 RID: 10916
	public float RADIUS_NODE_PLACEMENT = 30f;

	// Token: 0x04002AA5 RID: 10917
	private const float NODE_SCALE_MIN = 0.4f;

	// Token: 0x04002AA6 RID: 10918
	private const float NODE_SCALE_MAX = 1.2f;

	// Token: 0x04002AA7 RID: 10919
	private Color _color_node_back = Toolbox.makeColor("#1D7A74");

	// Token: 0x04002AA8 RID: 10920
	private Color _color_node_front = Toolbox.makeColor("#DDDDDD");

	// Token: 0x04002AA9 RID: 10921
	private Color _node_highlighted = Toolbox.makeColor("#FFFFFF");

	// Token: 0x04002AAA RID: 10922
	private Color _color_connection_back = Toolbox.makeColor("#1D7A74", 0.5f);

	// Token: 0x04002AAB RID: 10923
	private Color _color_connection_default = Toolbox.makeColor("#3AFFF5", 1f);

	// Token: 0x04002AAC RID: 10924
	private const float PERSPECTIVE_STRENGTH_MAIN = 3f;

	// Token: 0x04002AAD RID: 10925
	private const float PERSPECTIVE_STRENGTH_MAIN_MOD = 1f;

	// Token: 0x04002AAE RID: 10926
	private const float PERSPECTIVE_STRENGTH_AXIS = 1f;

	// Token: 0x04002AAF RID: 10927
	private const float SPACING_MOD = 1f;

	// Token: 0x04002AB0 RID: 10928
	private const float SPEED_MOD_OUTER = 0.2f;

	// Token: 0x04002AB1 RID: 10929
	private const float SPEED_MOD_INNER = 0.2f;

	// Token: 0x04002AB2 RID: 10930
	private const float SPEED_MOD_4D = 0.3f;

	// Token: 0x04002AB3 RID: 10931
	private const float MOD_NODE_SIZE = 1f;

	// Token: 0x04002AB4 RID: 10932
	private const float MOD_CONNECTION_SIZE = 1f;

	// Token: 0x04002AB5 RID: 10933
	private const float WARP_MOD = 0f;

	// Token: 0x04002AB6 RID: 10934
	private const float LENSE_MOD = 0f;

	// Token: 0x04002AB7 RID: 10935
	private const float FOLD_MOD = 0f;

	// Token: 0x04002AB8 RID: 10936
	private float _perspective_strength_main_mod = 1f;

	// Token: 0x04002AB9 RID: 10937
	private float _perspective_strength_main = 3f;

	// Token: 0x04002ABA RID: 10938
	private float _perspective_strength_x = 1f;

	// Token: 0x04002ABB RID: 10939
	private float _perspective_strength_y = 1f;

	// Token: 0x04002ABC RID: 10940
	private float _perspective_strength_z = 1f;

	// Token: 0x04002ABD RID: 10941
	private float _mod_lense;

	// Token: 0x04002ABE RID: 10942
	private float _mod_warp;

	// Token: 0x04002ABF RID: 10943
	private float _spacing_mod = 1f;

	// Token: 0x04002AC0 RID: 10944
	private float _speed_mod_inner = 0.2f;

	// Token: 0x04002AC1 RID: 10945
	private float _speed_mod_outer = 0.2f;

	// Token: 0x04002AC2 RID: 10946
	private float _speed_mod_4d = 0.3f;

	// Token: 0x04002AC3 RID: 10947
	private float _mod_node_size = 1f;

	// Token: 0x04002AC4 RID: 10948
	private float _mod_connection_size = 1f;

	// Token: 0x04002AC5 RID: 10949
	public float spacing = 25f;

	// Token: 0x04002AC6 RID: 10950
	private static readonly Vector4[] _hypercube_positions = new Vector4[]
	{
		new Vector4(-1f, -1f, -1f, -1f),
		new Vector4(1f, -1f, -1f, -1f),
		new Vector4(-1f, 1f, -1f, -1f),
		new Vector4(1f, 1f, -1f, -1f),
		new Vector4(-1f, -1f, 1f, -1f),
		new Vector4(1f, -1f, 1f, -1f),
		new Vector4(-1f, 1f, 1f, -1f),
		new Vector4(1f, 1f, 1f, -1f),
		new Vector4(-1f, -1f, -1f, 1f),
		new Vector4(1f, -1f, -1f, 1f),
		new Vector4(-1f, 1f, -1f, 1f),
		new Vector4(1f, 1f, -1f, 1f),
		new Vector4(-1f, -1f, 1f, 1f),
		new Vector4(1f, -1f, 1f, 1f),
		new Vector4(-1f, 1f, 1f, 1f),
		new Vector4(1f, 1f, 1f, 1f)
	};

	// Token: 0x04002AC7 RID: 10951
	private static readonly int[,] _hypercube_connections = new int[,]
	{
		{
			0,
			1
		},
		{
			0,
			2
		},
		{
			0,
			4
		},
		{
			0,
			8
		},
		{
			1,
			3
		},
		{
			1,
			5
		},
		{
			1,
			9
		},
		{
			2,
			3
		},
		{
			2,
			6
		},
		{
			2,
			10
		},
		{
			3,
			7
		},
		{
			3,
			11
		},
		{
			4,
			5
		},
		{
			4,
			6
		},
		{
			4,
			12
		},
		{
			5,
			7
		},
		{
			5,
			13
		},
		{
			6,
			7
		},
		{
			6,
			14
		},
		{
			7,
			15
		},
		{
			8,
			9
		},
		{
			8,
			10
		},
		{
			8,
			12
		},
		{
			9,
			11
		},
		{
			9,
			13
		},
		{
			10,
			11
		},
		{
			10,
			14
		},
		{
			11,
			15
		},
		{
			12,
			13
		},
		{
			12,
			14
		},
		{
			13,
			15
		},
		{
			14,
			15
		}
	};
}
