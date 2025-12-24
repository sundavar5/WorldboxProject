using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

// Token: 0x02000430 RID: 1072
public class MusicBox : MonoBehaviour
{
	// Token: 0x17000201 RID: 513
	// (get) Token: 0x06002541 RID: 9537 RVA: 0x001339E6 File Offset: 0x00131BE6
	private static FMOD.Studio.System _studio_system
	{
		get
		{
			return RuntimeManager.StudioSystem;
		}
	}

	// Token: 0x06002542 RID: 9538 RVA: 0x001339ED File Offset: 0x00131BED
	private void Awake()
	{
		this.create();
	}

	// Token: 0x06002543 RID: 9539 RVA: 0x001339F8 File Offset: 0x00131BF8
	internal void create()
	{
		if (this._created)
		{
			return;
		}
		this._created = true;
		MusicBox.inst = this;
		this.debug_box = new MusicBoxDebug();
		this._lib = AssetManager.music_box;
		this.idle = new MusicBoxIdle();
		ScrollWindow.addCallbackHide(new ScrollWindowNameAction(this.hideWindowCallback));
		if (!MusicBox.fmod_disabled)
		{
			try
			{
				this._bus_master = RuntimeManager.GetBus("bus:/");
				if (this._bus_master.isValid())
				{
					this._bus_master.setVolume(0f);
				}
			}
			catch (Exception e)
			{
				string str = "MusicBox failed to init: ";
				Exception ex = e;
				UnityEngine.Debug.LogError(str + ((ex != null) ? ex.ToString() : null));
				MusicBox.music_on = false;
				MusicBox.sounds_on = false;
				return;
			}
			Platform tPlatform = Settings.Instance.FindCurrentPlatform();
			if (MusicBox.debug_sounds)
			{
				Platform.PropertyAccessors.LiveUpdate.Set(tPlatform, TriStateBool.Enabled);
				Platform.PropertyAccessors.Overlay.Set(tPlatform, TriStateBool.Development);
			}
			else
			{
				Platform.PropertyAccessors.LiveUpdate.Set(tPlatform, TriStateBool.Disabled);
				Platform.PropertyAccessors.Overlay.Set(tPlatform, TriStateBool.Disabled);
			}
			this.createMusicEvent();
			this.assignCallback();
			this.startMusic();
		}
		MusicBox.reserveFlag(MusicBoxLibrary.Neutral_001.id, true);
		this.clearParams();
		MusicBox._sound_object = new GameObject("musicbox_pan");
		this._camera_listener = new GameObject("fmod_listener");
		this._camera_listener.transform.parent = Camera.main.transform;
		this._camera_listener.AddComponent<StudioListener>();
	}

	// Token: 0x06002544 RID: 9540 RVA: 0x00133B84 File Offset: 0x00131D84
	private void setMusicState(MusicState pState)
	{
		this.music_state = pState;
		if (pState == MusicState.Menu)
		{
			MusicBox.reserveFlag("Menu", true);
		}
	}

	// Token: 0x06002545 RID: 9541 RVA: 0x00133B9C File Offset: 0x00131D9C
	private void checkDrawingSounds()
	{
		if (!MusicBox.sounds_on)
		{
			return;
		}
		bool tStop = false;
		if (InputHelpers.mouseSupported)
		{
			if (!Input.GetMouseButton(0))
			{
				tStop = true;
			}
			else if (!ControllableUnit.isControllingUnit() && World.world.isOverUI())
			{
				tStop = true;
			}
		}
		else if (Input.touchCount == 0)
		{
			tStop = true;
		}
		if (tStop)
		{
			MusicBox.inst.stopDrawingSounds();
		}
	}

	// Token: 0x06002546 RID: 9542 RVA: 0x00133BF4 File Offset: 0x00131DF4
	private void checkIdleVolume()
	{
		if (World.world.isPaused())
		{
			this._volume_idle -= Time.deltaTime;
			if (this._volume_idle < 0f)
			{
				this._volume_idle = 0f;
			}
		}
		else
		{
			this._volume_idle += Time.deltaTime;
			if (this._volume_idle > 1f)
			{
				this._volume_idle = 1f;
			}
		}
		if (!this._bus_idle.isValid())
		{
			this._bus_idle = RuntimeManager.GetBus("bus:/Idle");
		}
		this.checkBusVolume(this._volume_idle, this._bus_idle);
	}

	// Token: 0x06002547 RID: 9543 RVA: 0x00133C94 File Offset: 0x00131E94
	private void checkVolumes()
	{
		bool tValid = this._vca_sound_effects.isValid();
		if (!tValid)
		{
			this._vca_sound_effects = RuntimeManager.GetVCA("vca:/Sound Effects");
			this._vca_music = RuntimeManager.GetVCA("vca:/Music");
			this._vca_ui = RuntimeManager.GetVCA("vca:/UI");
			this._bus_master = RuntimeManager.GetBus("bus:/");
			if (!tValid)
			{
				return;
			}
		}
		this.checkBusVolume("volume_master_sound", this._bus_master);
		this.checkVcaVolume("volume_sound_effects", this._vca_sound_effects);
		this.checkVcaVolume("volume_music", this._vca_music);
		this.checkVcaVolume("volume_ui", this._vca_ui);
	}

	// Token: 0x06002548 RID: 9544 RVA: 0x00133D38 File Offset: 0x00131F38
	private void checkBusVolume(float pVolume, Bus pBus)
	{
		float tCurrentVolume;
		pBus.getVolume(out tCurrentVolume);
		if (tCurrentVolume != pVolume)
		{
			pBus.setVolume(pVolume);
		}
	}

	// Token: 0x06002549 RID: 9545 RVA: 0x00133D5C File Offset: 0x00131F5C
	private void checkBusVolume(string pOptionParam, Bus pBus)
	{
		float tVolumeFromOptions = (float)PlayerConfig.getIntValue(pOptionParam) / 100f;
		float tCurrentVolume;
		pBus.getVolume(out tCurrentVolume);
		if (tCurrentVolume != tVolumeFromOptions)
		{
			pBus.setVolume(tVolumeFromOptions);
		}
	}

	// Token: 0x0600254A RID: 9546 RVA: 0x00133D90 File Offset: 0x00131F90
	private void checkVcaVolume(string pOptionParam, VCA pVCA)
	{
		float tVolumeFromOptions = (float)PlayerConfig.getIntValue(pOptionParam) / 100f;
		float tCurrentVolume;
		pVCA.getVolume(out tCurrentVolume);
		if (tCurrentVolume != tVolumeFromOptions)
		{
			pVCA.setVolume(tVolumeFromOptions);
		}
	}

	// Token: 0x0600254B RID: 9547 RVA: 0x00133DC4 File Offset: 0x00131FC4
	public void update(float pElapsed)
	{
		if (MusicBox.fmod_disabled)
		{
			return;
		}
		Bench.bench("music_box", "music_box_total", false);
		Bench.bench("check_volume", "music_box", false);
		this.checkVolumes();
		this.checkIdleVolume();
		Bench.benchEnd("check_volume", "music_box", false, 0L, false);
		Bench.bench("update_idle", "music_box", false);
		this.idle.update(pElapsed);
		Bench.benchEnd("update_idle", "music_box", false, 0L, false);
		Bench.bench("update_debug", "music_box", false);
		this.debug_box.update();
		Bench.benchEnd("update_debug", "music_box", false, 0L, false);
		Bench.bench("update_drawing", "music_box", false);
		this.checkDrawingSounds();
		Bench.benchEnd("update_drawing", "music_box", false, 0L, false);
		Bench.bench("update_fmod_params", "music_box", false);
		Vector3 tListenerPos = new Vector3(0f, 0f, World.world.camera.orthographicSize * 1.5f);
		this._camera_listener.transform.localPosition = tListenerPos;
		this.updateMainFmodParams();
		Bench.benchEnd("update_fmod_params", "music_box", false, 0L, false);
		if (this._timer > 0f)
		{
			this._timer -= pElapsed;
			return;
		}
		this._timer = 1f;
		Bench.bench("clearParams", "music_box", false);
		this.clearParams();
		Bench.benchEnd("clearParams", "music_box", false, 0L, false);
		Bench.bench("drawFmodDebugZones", "music_box", false);
		this.drawFmodDebugZones();
		Bench.benchEnd("drawFmodDebugZones", "music_box", false, 0L, false);
		Bench.bench("countZonesUnits", "music_box", false);
		this.countUnitsInZones();
		Bench.benchEnd("countZonesUnits", "music_box", false, 0L, false);
		Bench.bench("countSpecialTiles", "music_box", false);
		this.countSpecialTilesInChunks();
		Bench.benchEnd("countSpecialTiles", "music_box", false, 0L, false);
		Bench.bench("checkUnitsParams", "music_box", false);
		this.checkUnitsParams();
		Bench.benchEnd("checkUnitsParams", "music_box", false, 0L, false);
		Bench.bench("checkCamera", "music_box", false);
		this.checkCamera();
		Bench.benchEnd("checkCamera", "music_box", false, 0L, false);
		Bench.bench("music_params_1", "music_box", false);
		foreach (MusicBoxContainerTiles tCont in this._lib.c_list_params)
		{
			if (tCont.enabled)
			{
				this.enableMusicParameter(tCont.asset.id);
			}
			else
			{
				this.disableMusicParameter(tCont.asset.id);
			}
		}
		Bench.benchEnd("music_params_1", "music_box", false, 0L, false);
		Bench.bench("music_params_2", "music_box", false);
		foreach (MusicBoxContainerUnits tCont2 in this._lib.c_dict_units.Values)
		{
			if (tCont2.enabled)
			{
				this.enableMusicParameter(tCont2.asset.id);
			}
			else
			{
				this.disableMusicParameter(tCont2.asset.id);
			}
		}
		Bench.benchEnd("music_params_2", "music_box", false, 0L, false);
		Bench.bench("flags", "music_box", false);
		if (this._flags_to_enable.Any<string>())
		{
			foreach (string tFlag in this._flags_to_enable)
			{
				this.enableMusicParameter(tFlag);
			}
			this._flags_to_enable.Clear();
		}
		Bench.benchEnd("flags", "music_box", false, 0L, false);
		Bench.bench("check_environment", "music_box", false);
		foreach (MusicBoxContainerTiles tContainer in this._lib.c_list_environments)
		{
			this.checkEnvironmentSound(tContainer);
		}
		Bench.benchEnd("check_environment", "music_box", false, 0L, false);
		Bench.benchEnd("music_box", "music_box_total", false, 0L, false);
	}

	// Token: 0x0600254C RID: 9548 RVA: 0x00134268 File Offset: 0x00132468
	private void updateMainFmodParams()
	{
		if (World.world.quality_changer.isLowRes())
		{
			MusicBox._studio_system.setParameterByName("MiniMap", 1f, false);
		}
		else
		{
			MusicBox._studio_system.setParameterByName("MiniMap", 0f, false);
		}
		float tZoomLow = World.world.quality_changer.getZoomRatioLow();
		float tZoomHigh = World.world.quality_changer.getZoomRatioHigh();
		float tZoomFull = World.world.quality_changer.getZoomRatioFull();
		MusicBox._studio_system.setParameterByName("Zoom_Low", tZoomLow, false);
		MusicBox._studio_system.setParameterByName("Zoom_High", tZoomHigh, false);
		MusicBox._studio_system.setParameterByName("Zoom_Full", tZoomFull, false);
	}

	// Token: 0x0600254D RID: 9549 RVA: 0x00134329 File Offset: 0x00132529
	public static void clearAllSounds()
	{
		if (MusicBox.fmod_disabled)
		{
			return;
		}
		MusicBox.inst.idle.clearAllSounds();
		MusicBox.inst.debug_box.clear();
	}

	// Token: 0x0600254E RID: 9550 RVA: 0x00134354 File Offset: 0x00132554
	public void clearParams()
	{
		foreach (Kingdom tKingdom in World.world.kingdoms)
		{
			MusicBoxContainerCivs tContainer;
			if (this._lib.c_dict_civs.TryGetValue(tKingdom.getSpecies(), out tContainer))
			{
				tContainer.kingdom_exists = true;
			}
		}
		this._tiles_sand = 0;
		this._tiles_shallow_water = 0;
		foreach (MusicBoxContainerCivs musicBoxContainerCivs in this._lib.c_dict_civs.Values)
		{
			musicBoxContainerCivs.clear();
		}
		foreach (MusicAsset musicAsset in this._lib.list)
		{
			MusicBoxContainerTiles container_tiles = musicAsset.container_tiles;
			if (container_tiles != null)
			{
				container_tiles.clear();
			}
		}
		foreach (MusicBoxContainerUnits musicBoxContainerUnits in this._lib.c_dict_units.Values)
		{
			musicBoxContainerUnits.clear();
		}
		DebugLayer.fmod_zones_to_draw.Clear();
	}

	// Token: 0x0600254F RID: 9551 RVA: 0x001344BC File Offset: 0x001326BC
	private void hideWindowCallback(string pWindowID)
	{
	}

	// Token: 0x06002550 RID: 9552 RVA: 0x001344C0 File Offset: 0x001326C0
	private void assignCallback()
	{
		this._music_callback = new EVENT_CALLBACK(MusicBox.beatEventCallback);
		this._timeline_info = new TimelineInfo();
		this._timeline_handle = GCHandle.Alloc(this._timeline_info, GCHandleType.Pinned);
		this._music_event.setUserData(GCHandle.ToIntPtr(this._timeline_handle));
		this._music_event.setCallback(this._music_callback, EVENT_CALLBACK_TYPE.TIMELINE_MARKER | EVENT_CALLBACK_TYPE.TIMELINE_BEAT);
	}

	// Token: 0x06002551 RID: 9553 RVA: 0x0013452A File Offset: 0x0013272A
	public static EventInstance getNewInstance(string pID)
	{
		return RuntimeManager.CreateInstance(pID);
	}

	// Token: 0x06002552 RID: 9554 RVA: 0x00134534 File Offset: 0x00132734
	public static EventInstance attachToObject(string pID, GameObject pObject, bool pPlay = true)
	{
		if (!MusicBox.sounds_on)
		{
			return default(EventInstance);
		}
		EventInstance tInstance = MusicBox.getNewInstance(pID);
		RuntimeManager.AttachInstanceToGameObject(tInstance, pObject.transform, false);
		if (pPlay)
		{
			tInstance.start();
		}
		return tInstance;
	}

	// Token: 0x06002553 RID: 9555 RVA: 0x00134572 File Offset: 0x00132772
	private void createMusicEvent()
	{
		this._music_event = MusicBox.getNewInstance("event:/MUSIC/ConsolidatedMusicEvent");
	}

	// Token: 0x06002554 RID: 9556 RVA: 0x00134584 File Offset: 0x00132784
	private void startMusic()
	{
		if (!MusicBox.music_on)
		{
			return;
		}
		this._music_event.start();
	}

	// Token: 0x06002555 RID: 9557 RVA: 0x0013459C File Offset: 0x0013279C
	[MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
	private static RESULT beatEventCallback(EVENT_CALLBACK_TYPE pType, IntPtr pInstancePtr, IntPtr pParameterPtr)
	{
		IntPtr tTimelineInfoPtr;
		RESULT tResult = MusicBox.inst._music_event.getUserData(out tTimelineInfoPtr);
		if (tResult != RESULT.OK)
		{
			UnityEngine.Debug.LogError("Timeline Callback error: " + tResult.ToString());
		}
		else if (tTimelineInfoPtr != IntPtr.Zero)
		{
			TimelineInfo timelineInfo = (TimelineInfo)GCHandle.FromIntPtr(tTimelineInfoPtr).Target;
			if (pType == EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
			{
				TIMELINE_MARKER_PROPERTIES parameter = (TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(pParameterPtr, typeof(TIMELINE_MARKER_PROPERTIES));
				timelineInfo.lastMarker = parameter.name;
				MusicBox.inst.markerReached(timelineInfo.lastMarker);
			}
		}
		return RESULT.OK;
	}

	// Token: 0x06002556 RID: 9558 RVA: 0x0013463F File Offset: 0x0013283F
	private void loadBanks()
	{
	}

	// Token: 0x06002557 RID: 9559 RVA: 0x00134644 File Offset: 0x00132844
	private void checkEnvironmentSound(MusicBoxContainerTiles pContainer)
	{
		MusicAsset tAsset = pContainer.asset;
		bool tPlay = true;
		if (tAsset.mini_map_only)
		{
			if (!World.world.quality_changer.isLowRes())
			{
				tPlay = false;
			}
		}
		else if (World.world.quality_changer.isLowRes())
		{
			tPlay = false;
		}
		else if (tAsset.min_zoom <= World.world.camera.orthographicSize)
		{
			tPlay = false;
		}
		if (tPlay && tAsset.min_tiles_to_play != 0 && pContainer.amount < tAsset.min_tiles_to_play)
		{
			tPlay = false;
		}
		pContainer.enabled = tPlay;
		if (tPlay)
		{
			this.playEnvironmentSound(pContainer);
			return;
		}
		this.stopEnvironmentSound(pContainer);
	}

	// Token: 0x06002558 RID: 9560 RVA: 0x001346D9 File Offset: 0x001328D9
	public static void playIdleSoundVisibleOnly(string pSoundPath, WorldTile pTile)
	{
		if (!MusicBox.sounds_on)
		{
			return;
		}
		MusicBox.playSoundVisibleOnly(pSoundPath, pTile);
	}

	// Token: 0x06002559 RID: 9561 RVA: 0x001346EA File Offset: 0x001328EA
	public static void playSoundVisibleOnly(string pSoundPath, WorldTile pTile)
	{
		if (!MusicBox.sounds_on)
		{
			return;
		}
		MusicBox.playSound(pSoundPath, pTile, true, true);
	}

	// Token: 0x0600255A RID: 9562 RVA: 0x00134700 File Offset: 0x00132900
	public static void playSound(string pSoundPath, WorldTile pTile, bool pGameViewOnly = false, bool pVisibleOnly = false)
	{
		if (string.IsNullOrEmpty(pSoundPath))
		{
			return;
		}
		if (pVisibleOnly && !pTile.zone.visible)
		{
			return;
		}
		MusicBox.playSound(pSoundPath, (float)pTile.pos.x, (float)pTile.pos.y, pGameViewOnly, false);
	}

	// Token: 0x0600255B RID: 9563 RVA: 0x0013474D File Offset: 0x0013294D
	public static void playSoundWorld(string pSoundPath)
	{
	}

	// Token: 0x0600255C RID: 9564 RVA: 0x0013474F File Offset: 0x0013294F
	public static void playSoundUI(string pSoundPath)
	{
		MusicBox.playSound(pSoundPath, -1f, -1f, false, false);
	}

	// Token: 0x0600255D RID: 9565 RVA: 0x00134764 File Offset: 0x00132964
	public static EventInstance PlayOneShot(GUID pGuid, Vector3 pPosition, bool pSet3D = true)
	{
		EventInstance tEventInstance = RuntimeManager.CreateInstance(pGuid);
		if (pSet3D)
		{
			tEventInstance.set3DAttributes(pPosition.To3DAttributes());
		}
		else
		{
			Vector3 tCamCenter = World.world.move_camera.transform.position;
			float tZoomDepth = World.world.move_camera.main_camera.orthographicSize;
			Vector3 tPosition = new Vector3(tCamCenter.x, tCamCenter.y, tZoomDepth);
			tEventInstance.set3DAttributes(tPosition.To3DAttributes());
		}
		tEventInstance.start();
		tEventInstance.release();
		return tEventInstance;
	}

	// Token: 0x0600255E RID: 9566 RVA: 0x001347E8 File Offset: 0x001329E8
	private static bool isEventExists(string pEventPath)
	{
		bool tExists;
		if (!MusicBox._events_cache.TryGetValue(pEventPath, out tExists))
		{
			EventDescription tDescription;
			tExists = (RuntimeManager.StudioSystem.getEvent(pEventPath, out tDescription) == RESULT.OK);
			MusicBox._events_cache.Add(pEventPath, tExists);
			if (!tExists)
			{
				UnityEngine.Debug.LogWarning("[FMOD] Missing event : " + pEventPath);
			}
			else
			{
				MusicBox._events_guids[pEventPath] = RuntimeManager.PathToGUID(pEventPath);
			}
		}
		return tExists;
	}

	// Token: 0x0600255F RID: 9567 RVA: 0x0013484C File Offset: 0x00132A4C
	public static void playSound(string pSoundPath, float pX = -1f, float pY = -1f, bool pGameViewOnly = false, bool pVisibleOnly = false)
	{
		if (!MusicBox.sounds_on)
		{
			return;
		}
		if (pGameViewOnly && World.world.quality_changer.isLowRes())
		{
			return;
		}
		if (!MusicBox.isEventExists(pSoundPath))
		{
			return;
		}
		GUID tSoundGUID = MusicBox._events_guids[pSoundPath];
		EventInstance? tState = null;
		try
		{
			if (pX != -1f && pY != -1f)
			{
				tState = new EventInstance?(MusicBox.PlayOneShot(tSoundGUID, new Vector3(pX, pY, 0f), true));
			}
			else
			{
				tState = new EventInstance?(MusicBox.PlayOneShot(tSoundGUID, Vector3.zero, false));
			}
		}
		catch (EventNotFoundException)
		{
		}
		if (DebugConfig.isOn(DebugOption.OverlaySounds) || DebugConfig.isOn(DebugOption.OverlaySoundsActive))
		{
			MusicBox.inst.debug_box.add(pSoundPath.Split('/', StringSplitOptions.None).Last<string>(), pX, pY, tState.Value);
		}
	}

	// Token: 0x06002560 RID: 9568 RVA: 0x00134924 File Offset: 0x00132B24
	public void playEnvironmentSound(MusicBoxContainerTiles pContainer)
	{
		if (!MusicBox.sounds_on)
		{
			return;
		}
		MusicAsset tAsset = pContainer.asset;
		EventInstance tInstance;
		if (this._environment_sounds.ContainsKey(tAsset.fmod_path))
		{
			tInstance = this._environment_sounds[tAsset.fmod_path];
		}
		else
		{
			tInstance = MusicBox.getNewInstance(tAsset.fmod_path);
			this._environment_sounds.Add(tAsset.fmod_path, tInstance);
		}
		MusicBox.setPan(tInstance, pContainer.cur_pan.x, pContainer.cur_pan.y);
		if (MusicBox.isPlaying(tInstance))
		{
			return;
		}
		tInstance.start();
	}

	// Token: 0x06002561 RID: 9569 RVA: 0x001349B4 File Offset: 0x00132BB4
	public void stopEnvironmentSound(MusicBoxContainerTiles pContainer)
	{
		MusicAsset tAsset = pContainer.asset;
		if (!this._environment_sounds.ContainsKey(tAsset.fmod_path))
		{
			return;
		}
		EventInstance tInstance = this._environment_sounds[tAsset.fmod_path];
		if (MusicBox.isPlaying(tInstance))
		{
			tInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
		}
	}

	// Token: 0x06002562 RID: 9570 RVA: 0x00134A00 File Offset: 0x00132C00
	public void playDrawingSound(string pSoundPath, float pX = -1f, float pY = -1f)
	{
		if (!MusicBox.sounds_on)
		{
			return;
		}
		EventInstance tInstance;
		if (this._drawing_sounds.ContainsKey(pSoundPath))
		{
			tInstance = this._drawing_sounds[pSoundPath];
		}
		else
		{
			tInstance = MusicBox.getNewInstance(pSoundPath);
			this._drawing_sounds.Add(pSoundPath, tInstance);
		}
		MusicBox.setPan(tInstance, pX, pY);
		tInstance.setParameterByName("cursor_speed", MapBox.cursor_speed.fmod_speed, false);
		if (MusicBox.isPlaying(tInstance))
		{
			return;
		}
		tInstance.start();
	}

	// Token: 0x06002563 RID: 9571 RVA: 0x00134A78 File Offset: 0x00132C78
	public static void setPan(EventInstance pInstance, float pX, float pY)
	{
		if (pX == -1f && pY == -1f)
		{
			return;
		}
		float tZ = 0f;
		MusicBox._sound_object.transform.position = new Vector3(pX, pY, tZ);
		ATTRIBUTES_3D attributes3D = MusicBox._sound_object.To3DAttributes();
		pInstance.set3DAttributes(attributes3D);
	}

	// Token: 0x06002564 RID: 9572 RVA: 0x00134AC8 File Offset: 0x00132CC8
	public void stopDrawingSound(string pID)
	{
		if (this._drawing_sounds.ContainsKey(pID))
		{
			EventInstance tInstance = this._drawing_sounds[pID];
			if (MusicBox.isPlaying(tInstance))
			{
				tInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			}
		}
	}

	// Token: 0x06002565 RID: 9573 RVA: 0x00134B04 File Offset: 0x00132D04
	public void stopDrawingSounds()
	{
		foreach (EventInstance tInstance in this._drawing_sounds.Values)
		{
			if (MusicBox.isPlaying(tInstance))
			{
				tInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
			}
		}
	}

	// Token: 0x06002566 RID: 9574 RVA: 0x00134B68 File Offset: 0x00132D68
	public static bool isPlaying(EventInstance pInstance)
	{
		PLAYBACK_STATE state;
		pInstance.getPlaybackState(out state);
		return state != PLAYBACK_STATE.STOPPED;
	}

	// Token: 0x06002567 RID: 9575 RVA: 0x00134B86 File Offset: 0x00132D86
	private void drawFmodDebugZones()
	{
	}

	// Token: 0x06002568 RID: 9576 RVA: 0x00134B88 File Offset: 0x00132D88
	private void countUnitsInZones()
	{
		foreach (MapChunk tChunk in World.world.zone_camera.getVisibleChunks())
		{
			if (!tChunk.objects.isEmpty())
			{
				this.countUnits(tChunk);
			}
		}
	}

	// Token: 0x06002569 RID: 9577 RVA: 0x00134BF4 File Offset: 0x00132DF4
	private void checkCamera()
	{
		if ((this._tiles_sand < 50 || this._tiles_shallow_water < 50) && this._tiles_sand >= 100 && this._tiles_shallow_water < 20)
		{
			MusicBoxLibrary.Locations_Desert.container_tiles.amount = this._tiles_sand + this._tiles_shallow_water;
		}
		this._lib.c_list_params.Sort(new Comparison<MusicBoxContainerTiles>(MusicBox.sorter));
		float tTotalTiles = 0f;
		for (int i = 0; i < this._lib.c_list_params.Count; i++)
		{
			MusicBoxContainerTiles tCont = this._lib.c_list_params[i];
			tCont.enabled = false;
			tTotalTiles += (float)tCont.amount;
		}
		float tPercentUsed = 0f;
		int tMax = 0;
		for (int j = 0; j < this._lib.c_list_params.Count; j++)
		{
			MusicBoxContainerTiles tCont2 = this._lib.c_list_params[j];
			tCont2.calculatePan();
			tCont2.percent = (float)tCont2.amount / tTotalTiles;
			tPercentUsed += tCont2.percent;
			if (tCont2.amount > 50)
			{
				if (tMax >= 2)
				{
					break;
				}
				tCont2.enabled = true;
				tMax++;
			}
		}
	}

	// Token: 0x0600256A RID: 9578 RVA: 0x00134D20 File Offset: 0x00132F20
	private void checkUnitsParams()
	{
		MusicBoxContainerUnits tContPriorityMedium = null;
		MusicBoxContainerUnits tContPriorityHigh = null;
		foreach (MusicBoxContainerUnits tCont in this._lib.c_dict_units.Values)
		{
			MusicAssetDelegate special_delegate_units = tCont.asset.special_delegate_units;
			if (special_delegate_units != null)
			{
				special_delegate_units(tCont);
			}
			if (tCont.units > 0)
			{
				if (tCont.asset.priority == MusicLayerPriority.High)
				{
					tContPriorityHigh = tCont;
				}
				else if (tCont.asset.priority == MusicLayerPriority.Medium)
				{
					tContPriorityMedium = tCont;
				}
			}
		}
		if (tContPriorityHigh != null)
		{
			tContPriorityMedium = null;
		}
		if (tContPriorityHigh != null || tContPriorityMedium != null)
		{
			foreach (MusicBoxContainerUnits tCont2 in this._lib.c_dict_units.Values)
			{
				if ((tContPriorityHigh == null || tCont2 != tContPriorityHigh) && (tContPriorityMedium == null || tCont2 != tContPriorityMedium))
				{
					tCont2.units = 0;
				}
			}
		}
		foreach (MusicBoxContainerUnits tCont3 in this._lib.c_dict_units.Values)
		{
			if (tCont3.units > 0)
			{
				tCont3.enabled = true;
			}
		}
	}

	// Token: 0x0600256B RID: 9579 RVA: 0x00134E80 File Offset: 0x00133080
	public static int sorter(MusicBoxContainerTiles pV1, MusicBoxContainerTiles pV2)
	{
		return pV2.amount.CompareTo(pV1.amount);
	}

	// Token: 0x0600256C RID: 9580 RVA: 0x00134E94 File Offset: 0x00133094
	private void countSpecialTilesInChunks()
	{
		List<MapChunk> tVisibleChunks = World.world.zone_camera.getVisibleChunks();
		int i = 0;
		int tLen = tVisibleChunks.Count;
		while (i < tLen)
		{
			MapChunk tChunk = tVisibleChunks[i];
			this.countSpecialTilesForZone(tChunk);
			i++;
		}
	}

	// Token: 0x0600256D RID: 9581 RVA: 0x00134ED4 File Offset: 0x001330D4
	private void countSpecialTilesForZone(MapChunk pChunk)
	{
		List<MusicBoxTileData> tTileTypesCount = pChunk.getSimpleData();
		TileTypeBase[] tTileTypes = TileLibrary.array_tiles;
		int i = 0;
		int tLen = tTileTypesCount.Count;
		while (i < tLen)
		{
			MusicBoxTileData tData = tTileTypesCount[i];
			TileTypeBase tType = tTileTypes[tData.tile_type_id];
			int tAmount = tData.amount;
			if (tAmount != 0)
			{
				List<MusicAsset> tListMusicAssets = tType.music_assets;
				if (tListMusicAssets != null)
				{
					int j = 0;
					int tLenJ = tListMusicAssets.Count;
					while (j < tLenJ)
					{
						tListMusicAssets[j].container_tiles.count(tAmount, pChunk.world_center_x, pChunk.world_center_y);
						j++;
					}
				}
			}
			i++;
		}
	}

	// Token: 0x0600256E RID: 9582 RVA: 0x00134F6C File Offset: 0x0013316C
	private void countUnits(MapChunk pChunk)
	{
		foreach (long tKingdomID in pChunk.objects.kingdoms)
		{
			Kingdom tKingdom = World.world.kingdoms.get(tKingdomID);
			if (tKingdom != null)
			{
				ActorAsset tActorAsset = tKingdom.getActorAsset();
				if (tActorAsset != null && tActorAsset.has_music_theme)
				{
					this._lib.c_dict_units[tActorAsset.music_theme].units++;
				}
			}
		}
	}

	// Token: 0x0600256F RID: 9583 RVA: 0x00135008 File Offset: 0x00133208
	private void enableMusicParameter(string pID)
	{
		this.setMusicParameter(pID, 1f);
	}

	// Token: 0x06002570 RID: 9584 RVA: 0x00135016 File Offset: 0x00133216
	private void disableMusicParameter(string pID)
	{
		this.setMusicParameter(pID, 0f);
	}

	// Token: 0x06002571 RID: 9585 RVA: 0x00135024 File Offset: 0x00133224
	private void setMusicParameter(string pID, float pValue)
	{
		this._music_event.setParameterByName(pID, pValue, false);
	}

	// Token: 0x06002572 RID: 9586 RVA: 0x00135038 File Offset: 0x00133238
	private void markerReached(string pMarker)
	{
		if (pMarker == "Intro")
		{
			return;
		}
		MusicAsset tAsset = this._lib.get(pMarker);
		if (tAsset == null)
		{
			return;
		}
		if (tAsset.disable_param_after_start)
		{
			this.disableMusicParameter(pMarker);
		}
		if (tAsset.action != null)
		{
			tAsset.action();
		}
	}

	// Token: 0x06002573 RID: 9587 RVA: 0x00135086 File Offset: 0x00133286
	public static void reserveFlag(string pID, bool pValue = true)
	{
		if (!MusicBox.music_on)
		{
			return;
		}
		MusicBox.inst._timer = -1f;
		MusicBox.inst._flags_to_enable.Add(pID);
	}

	// Token: 0x06002574 RID: 9588 RVA: 0x001350B0 File Offset: 0x001332B0
	public static void debug_fmod(DebugTool pTool)
	{
		if (MusicBox.fmod_disabled)
		{
			return;
		}
		Bank[] banks;
		MusicBox._studio_system.getBankList(out banks);
		EventDescription tEvent;
		RESULT tResult2 = MusicBox._studio_system.getEvent("event:/MUSIC/ConsolidatedMusicEvent", out tEvent);
		int tTimelinePos = -1;
		float tParam_new_world = -1f;
		PLAYBACK_STATE getPlaybackState = PLAYBACK_STATE.STARTING;
		MusicBox.inst._music_event.getParameterByName("new_world", out tParam_new_world);
		MusicBox.inst._music_event.getTimelinePosition(out tTimelinePos);
		MusicBox.inst._music_event.getPlaybackState(out getPlaybackState);
		pTool.setText("Zoom_Low:", World.world.quality_changer.getZoomRatioLow(), 0f, false, 0L, false, false, "");
		pTool.setText("Zoom_High:", World.world.quality_changer.getZoomRatioHigh(), 0f, false, 0L, false, false, "");
		pTool.setText("Zoom_Full:", World.world.quality_changer.getZoomRatioFull(), 0f, false, 0L, false, false, "");
		pTool.setSeparator();
		pTool.setText("idle_sim_objects:", MusicBox.inst.idle.CountCurrentSounds(), 0f, false, 0L, false, false, "");
		pTool.setText("music state:", MusicBox.inst.music_state, 0f, false, 0L, false, false, "");
		pTool.setText("IsInitialized:", RuntimeManager.IsInitialized, 0f, false, 0L, false, false, "");
		pTool.setText("Banks count:", banks.Length, 0f, false, 0L, false, false, "");
		pTool.setText("AnySampleDataLoading:", RuntimeManager.AnySampleDataLoading(), 0f, false, 0L, false, false, "");
		pTool.setText("Bank Master:", RuntimeManager.HasBankLoaded("Master"), 0f, false, 0L, false, false, "");
		pTool.setText("Bank Master.strings:", RuntimeManager.HasBankLoaded("Master.strings"), 0f, false, 0L, false, false, "");
		pTool.setText("MUSIC_EVENT by name:", tResult2.ToString(), 0f, false, 0L, false, false, "");
		pTool.setText("tParam_new_world:", tParam_new_world, 0f, false, 0L, false, false, "");
		pTool.setText("tTimelinePos:", tTimelinePos, 0f, false, 0L, false, false, "");
		pTool.setText("getPlaybackState:", getPlaybackState.ToString(), 0f, false, 0L, false, false, "");
	}

	// Token: 0x06002575 RID: 9589 RVA: 0x00135368 File Offset: 0x00133568
	public void debug_params(DebugTool pTool)
	{
		if (MusicBox.fmod_disabled)
		{
			return;
		}
		float tParamValue = 0f;
		for (int i = 0; i < this._lib.list.Count; i++)
		{
			string tParam = this._lib.list[i].id;
			MusicBox.inst._music_event.getParameterByName(tParam, out tParamValue);
			if (tParamValue == 1f)
			{
				pTool.setText(tParam + ":", tParamValue, 0f, false, 0L, false, false, "");
			}
		}
	}

	// Token: 0x06002576 RID: 9590 RVA: 0x001353F8 File Offset: 0x001335F8
	public void debug_world_params(DebugTool pTool)
	{
		if (MusicBox.fmod_disabled)
		{
			return;
		}
		foreach (MusicBoxContainerCivs tCont in this._lib.c_dict_civs.Values)
		{
			if (tCont.active)
			{
				pTool.setText(tCont.asset.id, string.Concat(new string[]
				{
					tCont.buildings.ToString(),
					" ",
					tCont.kingdom_exists.ToString(),
					" ",
					tCont.active.ToString()
				}), 0f, false, 0L, false, false, "");
			}
		}
		foreach (MusicAsset musicAsset in this._lib.list)
		{
			MusicBoxContainerTiles tCont2 = musicAsset.container_tiles;
			if (tCont2 != null && tCont2.enabled)
			{
				pTool.setText(tCont2.asset.id, string.Concat(new string[]
				{
					tCont2.amount.ToString(),
					" ",
					tCont2.enabled.ToString(),
					" ",
					tCont2.percent.ToText(),
					"%"
				}), 0f, false, 0L, false, false, "");
			}
		}
		pTool.setText("", "", 0f, false, 0L, false, false, "");
	}

	// Token: 0x06002577 RID: 9591 RVA: 0x001355A8 File Offset: 0x001337A8
	public void debug_unit_params(DebugTool pTool)
	{
		if (MusicBox.fmod_disabled)
		{
			return;
		}
		if (this._lib.c_dict_units.Count == 0)
		{
			return;
		}
		foreach (MusicBoxContainerUnits tCont in this._lib.c_dict_units.Values)
		{
			if (tCont.units != 0)
			{
				pTool.setText(tCont.asset.id, tCont.units.ToString() + " " + tCont.enabled.ToString(), 0f, false, 0L, false, false, "");
			}
		}
		pTool.setText("", "", 0f, false, 0L, false, false, "");
	}

	// Token: 0x17000202 RID: 514
	// (get) Token: 0x06002578 RID: 9592 RVA: 0x00135680 File Offset: 0x00133880
	private static bool fmod_disabled
	{
		get
		{
			return !MusicBox.music_on && !MusicBox.sounds_on;
		}
	}

	// Token: 0x04001C4E RID: 7246
	private const int MUSIC_ZONES_SIZE = 3;

	// Token: 0x04001C4F RID: 7247
	private const int IDLE_SOUND_TIMER_MIN = 5;

	// Token: 0x04001C50 RID: 7248
	private const int IDLE_SOUND_TIMER_MAX = 12;

	// Token: 0x04001C51 RID: 7249
	public static MusicBox inst;

	// Token: 0x04001C52 RID: 7250
	private readonly HashSet<string> _flags_to_enable = new HashSet<string>();

	// Token: 0x04001C53 RID: 7251
	private EventInstance _music_event;

	// Token: 0x04001C54 RID: 7252
	internal MusicBoxDebug debug_box;

	// Token: 0x04001C55 RID: 7253
	private float _timer;

	// Token: 0x04001C56 RID: 7254
	private const float INTERVAL_UPDATE = 1f;

	// Token: 0x04001C57 RID: 7255
	public static bool music_on = true;

	// Token: 0x04001C58 RID: 7256
	public static bool sounds_on = true;

	// Token: 0x04001C59 RID: 7257
	public static bool debug_sounds = true;

	// Token: 0x04001C5A RID: 7258
	private VCA _vca_sound_effects;

	// Token: 0x04001C5B RID: 7259
	private VCA _vca_music;

	// Token: 0x04001C5C RID: 7260
	private VCA _vca_ui;

	// Token: 0x04001C5D RID: 7261
	private Bus _bus_master;

	// Token: 0x04001C5E RID: 7262
	private Bus _bus_idle;

	// Token: 0x04001C5F RID: 7263
	private float _volume_idle = 1f;

	// Token: 0x04001C60 RID: 7264
	private EVENT_CALLBACK _music_callback;

	// Token: 0x04001C61 RID: 7265
	private TimelineInfo _timeline_info;

	// Token: 0x04001C62 RID: 7266
	private GCHandle _timeline_handle;

	// Token: 0x04001C63 RID: 7267
	public static bool new_world_on_start_played = false;

	// Token: 0x04001C64 RID: 7268
	private readonly Dictionary<string, EventInstance> _environment_sounds = new Dictionary<string, EventInstance>();

	// Token: 0x04001C65 RID: 7269
	private readonly Dictionary<string, EventInstance> _drawing_sounds = new Dictionary<string, EventInstance>();

	// Token: 0x04001C66 RID: 7270
	private static readonly Dictionary<string, bool> _events_cache = new Dictionary<string, bool>();

	// Token: 0x04001C67 RID: 7271
	private static readonly Dictionary<string, GUID> _events_guids = new Dictionary<string, GUID>();

	// Token: 0x04001C68 RID: 7272
	private static GameObject _sound_object;

	// Token: 0x04001C69 RID: 7273
	private int _tiles_sand;

	// Token: 0x04001C6A RID: 7274
	private int _tiles_shallow_water;

	// Token: 0x04001C6B RID: 7275
	public MusicState music_state;

	// Token: 0x04001C6C RID: 7276
	private MusicBoxLibrary _lib;

	// Token: 0x04001C6D RID: 7277
	public MusicBoxIdle idle;

	// Token: 0x04001C6E RID: 7278
	private GameObject _camera_listener;

	// Token: 0x04001C6F RID: 7279
	private bool _created;
}
