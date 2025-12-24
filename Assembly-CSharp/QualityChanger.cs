using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020001E2 RID: 482
public class QualityChanger : MonoBehaviour
{
	// Token: 0x06000DF0 RID: 3568 RVA: 0x000BEC9C File Offset: 0x000BCE9C
	internal void reset()
	{
		this.setLowRes(true);
		this._transition_animation_factor = 1f;
		if (Config.isMobile)
		{
			this._main_zoom = 160f;
		}
		else
		{
			this._main_zoom = 240f;
		}
		World.world.world_layer.setRendererEnabled(true);
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x000BECEC File Offset: 0x000BCEEC
	internal void update()
	{
		LibraryMaterials.instance.updateMat();
		if (this.isLowRes() && this._tween_buildings < 0.3f)
		{
			this._render_buildings = false;
		}
		else
		{
			this._render_buildings = (!this.isFullLowRes() && this._tween_buildings != 0f);
		}
		if (this._color_alpha.a != this._transition_animation_factor || (this._transition_animation_factor != 0f && this._transition_animation_factor != 1f))
		{
			float tChangeModTween = iTween.easeInCirc(0f, 1f, this._transition_animation_factor);
			this._tween_buildings = 1f - tChangeModTween;
			LibraryMaterials.instance.updateZoomoutValue(this._tween_buildings);
		}
		if (this.isLowRes() && this._transition_animation_factor < 1f)
		{
			this._transition_animation_factor += Time.deltaTime * 3.5f;
			if (this._transition_animation_factor > 1f)
			{
				this._transition_animation_factor = 1f;
				this._tween_buildings = 0f;
			}
		}
		else if (!this.isLowRes() && this._transition_animation_factor > 0f)
		{
			this._transition_animation_factor -= Time.deltaTime * 3.5f;
			if (this._transition_animation_factor < 0f)
			{
				this._transition_animation_factor = 0f;
				this._tween_buildings = 1f;
			}
		}
		if (!PlayerConfig.optionBoolEnabled("minimap_transition_animation"))
		{
			if (this.isLowRes())
			{
				this._transition_animation_factor = 1f;
				this._tween_buildings = 0f;
			}
			else
			{
				this._transition_animation_factor = 0f;
				this._tween_buildings = 1f;
			}
			this.setZoomOrthographic(MoveCamera.instance.main_camera.orthographicSize);
		}
		this._color_alpha.a = this._transition_animation_factor;
		Color tColorMain = this._color_alpha;
		if (this.isLowRes())
		{
			tColorMain.a = Mathf.Max(0.9f, tColorMain.a);
		}
		World.world.world_layer.sprRnd.color = tColorMain;
		World.world.world_layer_edges.sprRnd.color = this._color_alpha;
		World.world.unit_layer.sprRnd.color = this._color_alpha;
		World.world.tilemap.checkEnableForWaterRunups(this.isLowRes());
		if (this.isLowRes())
		{
			World.world.world_layer.setRendererEnabled(true);
			if (World.world.tilemap.gameObject.activeSelf)
			{
				Color tColor = World.world._world_layer_switch_effect.color;
				tColor.a = 0.1f;
				World.world._world_layer_switch_effect.color = tColor;
			}
			World.world.tilemap.enableTiles(false);
			return;
		}
		World.world.tilemap.enableTiles(true);
		if (this._transition_animation_factor == 0f)
		{
			World.world.world_layer.setRendererEnabled(false);
		}
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x000BEFC8 File Offset: 0x000BD1C8
	public bool isFullLowRes()
	{
		return this.isLowRes() && this._transition_animation_factor == 1f;
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x000BEFE1 File Offset: 0x000BD1E1
	public bool shouldRenderUnitShadows()
	{
		return TrailerMonolith.enable_trailer_stuff || (Config.shadows_active && !this.isLowRes() && this.isZoomLevelWithinUnitShadows());
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x000BF00A File Offset: 0x000BD20A
	public bool isZoomLevelWithinUnitShadows()
	{
		return this._current_zoom_orthographic < this.getZoomBoundUnitShadows();
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x000BF01D File Offset: 0x000BD21D
	public float getZoomRateBoundLow()
	{
		return this.getCameraMultiplier(this._main_zoom);
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x000BF02B File Offset: 0x000BD22B
	public float getZoomRateShadows()
	{
		return this.getCameraMultiplier(this._main_zoom * 0.85f);
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x000BF03F File Offset: 0x000BD23F
	public bool shouldRenderBuildingShadows()
	{
		return TrailerMonolith.enable_trailer_stuff || (Config.shadows_active && World.world.camera.orthographicSize <= this.getZoomRateShadows() && this._render_buildings);
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x000BF072 File Offset: 0x000BD272
	public float getZoomBoundUnitShadows()
	{
		return this.getCameraMultiplier(this._main_zoom * 0.7f);
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x000BF088 File Offset: 0x000BD288
	private float getCameraMultiplier(float pVal)
	{
		if (!DebugConfig.isOn(DebugOption.UseCameraAspect))
		{
			return pVal;
		}
		int tPixelWidth = World.world.camera.pixelWidth;
		int tPixelHeight = World.world.camera.pixelHeight;
		float tCameraAspect = World.world.camera.aspect;
		float tDivision = (float)tPixelHeight;
		if (World.world.camera.pixelWidth > tPixelHeight)
		{
			tDivision = (float)tPixelWidth;
		}
		if (tCameraAspect > 2f)
		{
			tDivision /= tCameraAspect * 0.5f;
		}
		pVal *= (float)tPixelHeight / tDivision * 0.5f;
		return pVal;
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x000BF10C File Offset: 0x000BD30C
	internal void setZoomOrthographic(float pZoom)
	{
		this._current_zoom_orthographic = pZoom;
		bool tCurrentLowZoomState = this._current_zoom_orthographic > this.getZoomRateBoundLow();
		if (tCurrentLowZoomState == this.isLowRes())
		{
			return;
		}
		this.setLowRes(tCurrentLowZoomState);
		if (this.isLowRes())
		{
			MusicBox.playSound("event:/SFX/MAP/BitScaleWhooshIn", -1f, -1f, false, false);
		}
		else
		{
			MusicBox.playSound("event:/SFX/MAP/BitScaleWhooshOut", -1f, -1f, false, false);
		}
		World.world.zone_calculator.clearCurrentDrawnZones(true);
		World.world.resetRedrawTimer();
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x000BF190 File Offset: 0x000BD390
	public float getZoomRatioLow()
	{
		float num = this._current_zoom_orthographic - 10f;
		float tZoomMax = this.getZoomRateBoundLow() - 10f;
		return Mathf.Clamp(num / tZoomMax, 0f, 1f);
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x000BF1C8 File Offset: 0x000BD3C8
	public float getZoomRatioHigh()
	{
		if (this.getZoomRatioLow() < 1f)
		{
			return 0f;
		}
		float num = this._current_zoom_orthographic - this.getZoomRateBoundLow();
		float tZoomMax = 400f - this.getZoomRateBoundLow();
		return Mathf.Clamp(num / tZoomMax, 0f, 1f);
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x000BF214 File Offset: 0x000BD414
	public float getZoomRatioFull()
	{
		float num = this._current_zoom_orthographic - 10f;
		float tZoomMax = 400f;
		return Mathf.Clamp(num / tZoomMax, 0f, 1f);
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x000BF244 File Offset: 0x000BD444
	private void setLowRes(bool pValue)
	{
		this._low_resolution = pValue;
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x000BF24D File Offset: 0x000BD44D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool shouldRenderBuildings()
	{
		return this._render_buildings;
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x000BF255 File Offset: 0x000BD455
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public bool isLowRes()
	{
		return this._low_resolution;
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x000BF25D File Offset: 0x000BD45D
	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public float getTweenBuildingsValue()
	{
		return this._tween_buildings;
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x000BF268 File Offset: 0x000BD468
	public void debug(DebugTool pTool)
	{
		pTool.setText("Zoom_Low:", this.getZoomRatioLow(), 0f, false, 0L, false, false, "");
		pTool.setText("Zoom_High:", this.getZoomRatioHigh(), 0f, false, 0L, false, false, "");
		pTool.setText("Zoom_Full:", this.getZoomRatioFull(), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("lowRes", this.isLowRes(), 0f, false, 0L, false, false, "");
		pTool.setText("_timer_animation", this._transition_animation_factor, 0f, false, 0L, false, false, "");
		pTool.setText("getTweenBuildinsValue", this.getTweenBuildingsValue(), 0f, false, 0L, false, false, "");
		pTool.setText("isBuildingRendered", this.shouldRenderBuildings(), 0f, false, 0L, false, false, "");
		pTool.setText("_current_zoom", this._current_zoom_orthographic, 0f, false, 0L, false, false, "");
		pTool.setText("_main_zoom", this._main_zoom, 0f, false, 0L, false, false, "");
		pTool.setText("camera_zoom_max", World.world.move_camera.orthographic_size_max, 0f, false, 0L, false, false, "");
		pTool.setText("camera_ortho", World.world.move_camera.main_camera.orthographicSize, 0f, false, 0L, false, false, "");
		pTool.setText("camera_zoom_min", 10f, 0f, false, 0L, false, false, "");
		pTool.setText("BOUND_STRATOSPHERE_ORTHOGRAPHIC", 400f, 0f, false, 0L, false, false, "");
		pTool.setText("isFullLowRes()", this.isFullLowRes(), 0f, false, 0L, false, false, "");
		pTool.setText("renderShadowsUnits()", this.shouldRenderUnitShadows(), 0f, false, 0L, false, false, "");
	}

	// Token: 0x04000E56 RID: 3670
	private const float TIMER_SPEED_MULTIPLIER = 3.5f;

	// Token: 0x04000E57 RID: 3671
	private const float BUILDING_HIDE_VALUE = 0.3f;

	// Token: 0x04000E58 RID: 3672
	public const float BUILDING_SHADER_VALUE_BOUND = 4f;

	// Token: 0x04000E59 RID: 3673
	public const float BUILDING_SHADER_VALUE_BOUND_2 = 3f;

	// Token: 0x04000E5A RID: 3674
	private const float BOUND_RATE_SHADOWS_UNITS = 0.7f;

	// Token: 0x04000E5B RID: 3675
	private const float BOUND_RATE_SHADOWS_BUILDINGS = 0.85f;

	// Token: 0x04000E5C RID: 3676
	private const float BOUND_STRATOSPHERE_ORTHOGRAPHIC = 400f;

	// Token: 0x04000E5D RID: 3677
	private bool _low_resolution;

	// Token: 0x04000E5E RID: 3678
	private float _transition_animation_factor = 1f;

	// Token: 0x04000E5F RID: 3679
	private float _tween_buildings;

	// Token: 0x04000E60 RID: 3680
	private Color _color_alpha = new Color(1f, 1f, 1f);

	// Token: 0x04000E61 RID: 3681
	private bool _render_buildings;

	// Token: 0x04000E62 RID: 3682
	private float _current_zoom_orthographic;

	// Token: 0x04000E63 RID: 3683
	private float _main_zoom;
}
