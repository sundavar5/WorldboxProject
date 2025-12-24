using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020002CB RID: 715
public class NameplateManager : MonoBehaviour
{
	// Token: 0x06001A44 RID: 6724 RVA: 0x000F758F File Offset: 0x000F578F
	private void Awake()
	{
		this._canvas = base.GetComponent<Canvas>();
		this.canvas_rect = this._canvas.GetComponent<RectTransform>();
		this.canvas_scaler = base.GetComponent<CanvasScaler>();
	}

	// Token: 0x06001A45 RID: 6725 RVA: 0x000F75BC File Offset: 0x000F57BC
	private void prepare()
	{
		this._next_index = 0;
		this._nameplate_mode = ((PlayerConfig.getOptionInt("map_names") == 0) ? NameplateRenderingType.Full : NameplateRenderingType.BannerOnly);
		this._nano_object_set = SelectedObjects.isNanoObjectSet();
		this._selected_nano_object = SelectedObjects.getSelectedNanoObject();
		this.cached_favorites_only = PlayerConfig.optionBoolEnabled("only_favorited_meta");
		this.canvas_size_delta = this.canvas_rect.sizeDelta;
		this.cached_canvas_scale = this.canvas_scaler.scaleFactor;
		this.canvas_size_delta_mod_x = this.canvas_size_delta.x * 0.5f;
		this.canvas_size_delta_mod_y = this.canvas_size_delta.y * 0.5f;
	}

	// Token: 0x06001A46 RID: 6726 RVA: 0x000F765C File Offset: 0x000F585C
	internal void update()
	{
		Bench.bench("nameplates", "nameplates_total", false);
		Bench.bench("prepare", "nameplates", false);
		this.prepare();
		Bench.benchEnd("prepare", "nameplates", false, 0L, false);
		Bench.bench("check_mode", "nameplates", false);
		MetaType tMetaType = this.getCurrentMode();
		this.setMode(tMetaType);
		NameplateAsset tNameplateAsset = null;
		MetaTypeAsset tMetaTypeAsset = null;
		if (!tMetaType.isNone())
		{
			tNameplateAsset = AssetManager.nameplates_library.map_modes_nameplates[tMetaType];
			tMetaTypeAsset = tMetaType.getAsset();
		}
		Bench.benchEnd("check_mode", "nameplates", false, 0L, false);
		Bench.bench("set_nameplates", "nameplates", false);
		if (CanvasMain.isNameplatesAllowed())
		{
			if (tMetaType == MetaType.None)
			{
				if (base.gameObject.activeSelf)
				{
					base.gameObject.SetActive(false);
				}
			}
			else
			{
				if (!base.gameObject.activeSelf)
				{
					base.gameObject.SetActive(true);
				}
				tNameplateAsset.action_main(this, tNameplateAsset);
			}
		}
		Bench.benchEnd("set_nameplates", "nameplates", false, (long)this._active.Count, false);
		Bench.bench("updateOverlappingPositions", "nameplates", false);
		bool tUpdateOverlappingPositions = false;
		if (!tMetaType.isNone() && tMetaTypeAsset != null)
		{
			if (tMetaTypeAsset.isMetaZoneOptionSelectedFluid())
			{
				if (tNameplateAsset.overlap_for_fluid_mode)
				{
					tUpdateOverlappingPositions = true;
				}
			}
			else
			{
				tUpdateOverlappingPositions = true;
			}
		}
		if (tUpdateOverlappingPositions)
		{
			this.updateOverlappingPosition();
		}
		Bench.benchEnd("updateOverlappingPositions", "nameplates", false, 0L, false);
		Bench.bench("updateTweenScale", "nameplates", false);
		this.updateTweenScale();
		Bench.benchEnd("updateTweenScale", "nameplates", false, 0L, false);
		Bench.bench("checkActive", "nameplates", false);
		this.checkActive();
		Bench.benchEnd("checkActive", "nameplates", false, 0L, false);
		Bench.bench("findObjectForTooltip", "nameplates", false);
		NanoObject tNanoObjectForTooltip = this.findObjectForTooltip();
		Bench.benchEnd("findObjectForTooltip", "nameplates", false, 0L, false);
		Bench.bench("showTooltip", "nameplates", false);
		if (tNanoObjectForTooltip != null)
		{
			tNanoObjectForTooltip.getMetaTypeAsset().cursor_tooltip_action(tNanoObjectForTooltip);
		}
		Bench.benchEnd("showTooltip", "nameplates", false, 0L, false);
		Bench.bench("check_siblings", "nameplates", false);
		this.checkSiblingsToFront();
		Bench.benchEnd("check_siblings", "nameplates", false, 0L, false);
		Bench.bench("finale", "nameplates", false);
		this.finale();
		Bench.benchEnd("finale", "nameplates", false, 0L, false);
		Bench.benchEnd("nameplates", "nameplates_total", false, 0L, false);
	}

	// Token: 0x06001A47 RID: 6727 RVA: 0x000F78F0 File Offset: 0x000F5AF0
	private void checkSiblingsToFront()
	{
		if (this.cursor_over_text != null)
		{
			this.cursor_over_text.transform.SetAsLastSibling();
		}
		if (SelectedObjects.isNanoObjectSet())
		{
			foreach (NameplateText tNameplate in this._active)
			{
				if (tNameplate.nano_object == SelectedObjects.getSelectedNanoObject())
				{
					tNameplate.transform.SetAsLastSibling();
					break;
				}
			}
		}
	}

	// Token: 0x06001A48 RID: 6728 RVA: 0x000F797C File Offset: 0x000F5B7C
	private void checkActive()
	{
		for (int i = this._next_index - 1; i >= 0; i--)
		{
			this._active[i].checkActive();
		}
	}

	// Token: 0x06001A49 RID: 6729 RVA: 0x000F79B0 File Offset: 0x000F5BB0
	private void updateTweenScale()
	{
		this._tween_timer += Time.deltaTime * 2f;
		this._tween_timer = Mathf.Clamp(this._tween_timer, 0f, 1f);
		float tTargetY = iTween.easeOutBack(0f, 1f, this._tween_timer);
		tTargetY *= 0.5f;
		this._tween_scale = tTargetY;
	}

	// Token: 0x06001A4A RID: 6730 RVA: 0x000F7A18 File Offset: 0x000F5C18
	private NanoObject findObjectForTooltip()
	{
		this.cursor_over_text = null;
		if (World.world.isBusyWithUI())
		{
			return null;
		}
		if (ControllableUnit.isControllingUnit())
		{
			return null;
		}
		Vector2 tCursorPosition;
		if (!InputHelpers.mouseSupported && !this.checkTouch(out tCursorPosition))
		{
			return null;
		}
		tCursorPosition = World.world.getMousePos();
		Vector2 tCursorScreenPos = World.world.camera.WorldToScreenPoint(tCursorPosition);
		bool tMouseSupported = InputHelpers.mouseSupported;
		NanoObject tResult = null;
		float tDistBest = float.MaxValue;
		NameplateText tBestNameplate = null;
		for (int i = 0; i < this._active.Count; i++)
		{
			NameplateText tText = this._active[i];
			if (tText.isShowing())
			{
				Vector2 tTextScreenPosition = tText.getLastScreenPosition();
				float tDist = Toolbox.SquaredDist(tTextScreenPosition.x, tTextScreenPosition.y, tCursorScreenPos.x, tCursorScreenPos.y);
				if (tText.map_text_rect_click.Contains(tCursorScreenPos) && (!(tBestNameplate != null) || (tDist <= tDistBest && tDist <= 625f)))
				{
					tBestNameplate = tText;
					tDistBest = tDist;
				}
			}
		}
		if (tBestNameplate != null)
		{
			NanoObject tNanoObject = tBestNameplate.nano_object;
			if (Input.mousePresent)
			{
				tResult = tNanoObject;
			}
			this.cursor_over_text = tBestNameplate;
			Vector3 tCursorOverScale = tBestNameplate.transform.localScale;
			tCursorOverScale *= 1.1f;
			this.cursor_over_text.forceScale(tCursorOverScale);
			if (tNanoObject is IMetaObject && tMouseSupported)
			{
				((IMetaObject)tNanoObject).setCursorOver();
			}
		}
		return tResult;
	}

	// Token: 0x06001A4B RID: 6731 RVA: 0x000F7B86 File Offset: 0x000F5D86
	public bool isOverNameplate()
	{
		return this.cursor_over_text != null;
	}

	// Token: 0x06001A4C RID: 6732 RVA: 0x000F7B94 File Offset: 0x000F5D94
	private bool checkTouch(out Vector2 pPosition)
	{
		pPosition = Globals.POINT_IN_VOID;
		if (Input.touchCount == 0)
		{
			return false;
		}
		Touch tTouch = Input.touches[0];
		if (tTouch.phase == TouchPhase.Began && this._touch_released)
		{
			this._latest_touch_id = tTouch.fingerId;
			this._touch_released = false;
			return false;
		}
		if (tTouch.fingerId != this._latest_touch_id || tTouch.phase != TouchPhase.Ended || this._touch_released)
		{
			return false;
		}
		this._touch_released = true;
		pPosition = World.world.camera.ScreenToWorldPoint(tTouch.position);
		return true;
	}

	// Token: 0x06001A4D RID: 6733 RVA: 0x000F7C40 File Offset: 0x000F5E40
	private MetaType getCurrentMode()
	{
		MetaType tMode = MetaType.None;
		if (Zones.showMapNames())
		{
			if (!Zones.hasPowerForceMapMode())
			{
				tMode = Zones.getCurrentMapBorderMode(false);
				if (tMode.isNone())
				{
					tMode = MetaType.City;
				}
			}
			else
			{
				tMode = Zones.getForcedMapMode();
			}
		}
		return tMode;
	}

	// Token: 0x06001A4E RID: 6734 RVA: 0x000F7C77 File Offset: 0x000F5E77
	private void setMode(MetaType pMode)
	{
		if (this._last_mode == pMode)
		{
			return;
		}
		this._last_mode = pMode;
		this.clearAll();
	}

	// Token: 0x06001A4F RID: 6735 RVA: 0x000F7C90 File Offset: 0x000F5E90
	private void updateOverlappingPosition()
	{
		if (this._next_index <= 0)
		{
			return;
		}
		using (ListPool<NameplateText> tActiveNameplates = new ListPool<NameplateText>(this._next_index))
		{
			for (int i = 0; i < this._next_index; i++)
			{
				NameplateText tText = this._active[i];
				tActiveNameplates.Add(tText);
			}
			if (tActiveNameplates.Count > 1)
			{
				tActiveNameplates.Sort(new Comparison<NameplateText>(this.compareNameplates));
				using (ListPool<NameplateText> tVisiblePlates = new ListPool<NameplateText>(this._next_index))
				{
					foreach (NameplateText ptr in tActiveNameplates)
					{
						NameplateText tCandidate = ptr;
						bool tOverlap = false;
						foreach (NameplateText ptr2 in tVisiblePlates)
						{
							NameplateText tVisiblePlate = ptr2;
							if (tCandidate.overlapsWithOtherPlate(tVisiblePlate))
							{
								tOverlap = true;
								break;
							}
						}
						if (tOverlap)
						{
							tCandidate.setShowing(false);
						}
						else
						{
							tVisiblePlates.Add(tCandidate);
						}
					}
				}
			}
		}
	}

	// Token: 0x06001A50 RID: 6736 RVA: 0x000F7DD0 File Offset: 0x000F5FD0
	private void OnDrawGizmos()
	{
		Camera tCam = Camera.main;
		if (tCam == null)
		{
			return;
		}
		for (int i = 0; i < this._next_index; i++)
		{
			NameplateText tPlate = this._active[i];
			Rect tRekt = tPlate.map_text_rect_overlap;
			Vector3 tScreenBottomLeft = new Vector3(tRekt.xMin, tRekt.yMin, tCam.nearClipPlane);
			Vector3 tScreenTopRight = new Vector3(tRekt.xMax, tRekt.yMax, tCam.nearClipPlane);
			Vector3 tWorldBottomLeft = tCam.ScreenToWorldPoint(tScreenBottomLeft);
			Vector3 tWorldTopRight = tCam.ScreenToWorldPoint(tScreenTopRight);
			Vector3 center = (tWorldBottomLeft + tWorldTopRight) * 0.5f;
			Vector3 tSize = new Vector3(tWorldTopRight.x - tWorldBottomLeft.x, tWorldTopRight.y - tWorldBottomLeft.y, 0.1f);
			Gizmos.color = (tPlate.isShowing() ? Color.green : Color.red);
			Gizmos.DrawWireCube(center, tSize);
		}
	}

	// Token: 0x06001A51 RID: 6737 RVA: 0x000F7EC0 File Offset: 0x000F60C0
	private int compareNameplates(NameplateText pText1, NameplateText pText2)
	{
		NanoObject tSelectedObject = SelectedObjects.getSelectedNanoObject();
		bool tP1Selected = pText1.nano_object == tSelectedObject;
		bool tP2Selected = pText2.nano_object == tSelectedObject;
		if (tP1Selected != tP2Selected)
		{
			return (tP2Selected ? 1 : 0) - (tP1Selected ? 1 : 0);
		}
		if (pText1.favorited != pText2.favorited)
		{
			return (pText2.favorited ? 1 : 0) - (pText1.favorited ? 1 : 0);
		}
		if (pText1.priority_capital != pText2.priority_capital)
		{
			return (pText2.priority_capital ? 1 : 0) - (pText1.priority_capital ? 1 : 0);
		}
		int tPopCompare = pText2.priority_population.CompareTo(pText1.priority_population);
		if (tPopCompare != 0)
		{
			return tPopCompare;
		}
		return pText1.nano_object.id.CompareTo(pText2.nano_object.id);
	}

	// Token: 0x06001A52 RID: 6738 RVA: 0x000F7F81 File Offset: 0x000F6181
	public NameplateText prepareNext(NameplateAsset pAsset, NanoObject pMeta)
	{
		NameplateText nameplateToRender = this.getNameplateToRender();
		nameplateToRender.prepare(pAsset, pMeta, this._tween_scale, this._nameplate_mode, this._nano_object_set, this._selected_nano_object);
		return nameplateToRender;
	}

	// Token: 0x06001A53 RID: 6739 RVA: 0x000F7FAC File Offset: 0x000F61AC
	private NameplateText getNameplateToRender()
	{
		NameplateText tObject;
		if (this._active.Count > this._next_index)
		{
			tObject = this._active[this._next_index];
		}
		else
		{
			if (this._pool.Count == 0)
			{
				tObject = this.createNew();
			}
			else
			{
				tObject = this._pool.Pop();
			}
			this._active.Add(tObject);
		}
		this._next_index++;
		return tObject;
	}

	// Token: 0x06001A54 RID: 6740 RVA: 0x000F801D File Offset: 0x000F621D
	protected virtual NameplateText createNew()
	{
		NameplateText nameplateText = Object.Instantiate<NameplateText>(this.prefab, base.transform);
		nameplateText.newNameplate(this, string.Format("map text {0}", this._pool.Count + this._active.Count));
		return nameplateText;
	}

	// Token: 0x06001A55 RID: 6741 RVA: 0x000F8060 File Offset: 0x000F6260
	internal void clearAll()
	{
		this._tween_timer = 0.5f;
		this._tween_scale = 0f;
		if (this._active.Count == 0)
		{
			return;
		}
		for (int i = 0; i < this._active.Count; i++)
		{
			NameplateText tObject = this._active[i];
			tObject.clearFull();
			tObject.gameObject.SetActive(false);
			this._pool.Push(tObject);
		}
		this._active.Clear();
	}

	// Token: 0x06001A56 RID: 6742 RVA: 0x000F80DD File Offset: 0x000F62DD
	private void finale()
	{
		this.clearLast();
	}

	// Token: 0x06001A57 RID: 6743 RVA: 0x000F80E8 File Offset: 0x000F62E8
	public void clearCaches()
	{
		foreach (NameplateText nameplateText in this._active)
		{
			nameplateText.clearCaches();
		}
	}

	// Token: 0x06001A58 RID: 6744 RVA: 0x000F8138 File Offset: 0x000F6338
	public void clearLast()
	{
		int tDiff = this._active.Count - this._next_index;
		if (tDiff <= 0)
		{
			return;
		}
		while (tDiff > 0)
		{
			int tIndex = this._active.Count - 1;
			NameplateText tObject = this._active[tIndex];
			tObject.clearFull();
			tObject.gameObject.SetActive(false);
			this._active.RemoveAt(tIndex);
			this._pool.Push(tObject);
			tDiff--;
		}
	}

	// Token: 0x04001454 RID: 5204
	private readonly Stack<NameplateText> _pool = new Stack<NameplateText>();

	// Token: 0x04001455 RID: 5205
	private readonly List<NameplateText> _active = new List<NameplateText>();

	// Token: 0x04001456 RID: 5206
	private int _next_index;

	// Token: 0x04001457 RID: 5207
	public NameplateText prefab;

	// Token: 0x04001458 RID: 5208
	private Canvas _canvas;

	// Token: 0x04001459 RID: 5209
	internal CanvasScaler canvas_scaler;

	// Token: 0x0400145A RID: 5210
	internal RectTransform canvas_rect;

	// Token: 0x0400145B RID: 5211
	internal Vector2 canvas_size_delta;

	// Token: 0x0400145C RID: 5212
	internal float canvas_size_delta_mod_x;

	// Token: 0x0400145D RID: 5213
	internal float canvas_size_delta_mod_y;

	// Token: 0x0400145E RID: 5214
	private MetaType _last_mode;

	// Token: 0x0400145F RID: 5215
	public NameplateText cursor_over_text;

	// Token: 0x04001460 RID: 5216
	private int _latest_touch_id;

	// Token: 0x04001461 RID: 5217
	private bool _touch_released;

	// Token: 0x04001462 RID: 5218
	private float _tween_timer;

	// Token: 0x04001463 RID: 5219
	private float _tween_scale;

	// Token: 0x04001464 RID: 5220
	internal bool cached_favorites_only;

	// Token: 0x04001465 RID: 5221
	internal float cached_canvas_scale;

	// Token: 0x04001466 RID: 5222
	private NameplateRenderingType _nameplate_mode;

	// Token: 0x04001467 RID: 5223
	private bool _nano_object_set;

	// Token: 0x04001468 RID: 5224
	private NanoObject _selected_nano_object;
}
