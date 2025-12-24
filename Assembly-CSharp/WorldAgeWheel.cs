using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000816 RID: 2070
public class WorldAgeWheel : MonoBehaviour
{
	// Token: 0x170003B3 RID: 947
	// (get) Token: 0x060040CE RID: 16590 RVA: 0x001BB2AE File Offset: 0x001B94AE
	private WorldAgeManager _era_manager
	{
		get
		{
			return World.world.era_manager;
		}
	}

	// Token: 0x170003B4 RID: 948
	// (get) Token: 0x060040CF RID: 16591 RVA: 0x001BB2BA File Offset: 0x001B94BA
	private MapStats _map_stats
	{
		get
		{
			return World.world.map_stats;
		}
	}

	// Token: 0x060040D0 RID: 16592 RVA: 0x001BB2C6 File Offset: 0x001B94C6
	private void Awake()
	{
		this._floating_tween = base.transform.DOLocalMoveY(base.transform.localPosition.y - 4f, 2.5f, false).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
	}

	// Token: 0x060040D1 RID: 16593 RVA: 0x001BB304 File Offset: 0x001B9504
	public void init(WorldAgeElementAction pOnClickAction)
	{
		this._initialized = true;
		this._pieces = base.GetComponentsInChildren<WorldAgeWheelPiece>();
		for (int i = 0; i < this._pieces.Length; i++)
		{
			WorldAgeWheelPiece worldAgeWheelPiece = this._pieces[i];
			worldAgeWheelPiece.mask.alphaHitTestMinimumThreshold = 0.5f;
			worldAgeWheelPiece.init(i);
			worldAgeWheelPiece.setAge(this._era_manager.getAgeFromSlot(i));
			worldAgeWheelPiece.addClickCallback(pOnClickAction);
		}
		this.updateElements();
	}

	// Token: 0x060040D2 RID: 16594 RVA: 0x001BB374 File Offset: 0x001B9574
	private void Update()
	{
		this.updateArrowAnimation();
	}

	// Token: 0x060040D3 RID: 16595 RVA: 0x001BB37C File Offset: 0x001B957C
	private void updateArrowAnimation()
	{
		this.setArrowPosition(this._arrow_container_main, ref this._current_arrow_angle_main, ref this._target_arrow_angle_main);
	}

	// Token: 0x060040D4 RID: 16596 RVA: 0x001BB396 File Offset: 0x001B9596
	private void setArrowPosition(Transform pContainer, ref float pCurrentAngle, ref float pTargetAngle)
	{
		pCurrentAngle = Mathf.LerpAngle(pCurrentAngle, pTargetAngle, Time.deltaTime * 5f);
		if (Mathf.Approximately(pCurrentAngle, pTargetAngle))
		{
			pCurrentAngle = pTargetAngle;
		}
		this.setRotation(pContainer, pCurrentAngle);
	}

	// Token: 0x060040D5 RID: 16597 RVA: 0x001BB3C6 File Offset: 0x001B95C6
	private void finishArrowAnimation()
	{
		this._current_arrow_angle_main = this._target_arrow_angle_main;
		this._current_arrow_angle_secondary = this._target_arrow_angle_secondary;
	}

	// Token: 0x060040D6 RID: 16598 RVA: 0x001BB3E0 File Offset: 0x001B95E0
	private void OnEnable()
	{
		this._floating_tween.Play<Tweener>();
		if (this._initialized)
		{
			this.updateElements();
		}
		this.finishArrowAnimation();
		this.updateArrowAnimation();
	}

	// Token: 0x060040D7 RID: 16599 RVA: 0x001BB408 File Offset: 0x001B9608
	public void updateElements()
	{
		this.updateArrows();
		this.updateDimming();
		if (this._era_manager.isPaused())
		{
			this._image_arrow_main.sprite = this._sprite_arrow_pause;
			return;
		}
		this._image_arrow_main.sprite = this._sprite_arrow;
	}

	// Token: 0x060040D8 RID: 16600 RVA: 0x001BB448 File Offset: 0x001B9648
	private void updateArrows()
	{
		float tInitialAngle = this.getInitialAngle();
		this._target_arrow_angle_main = tInitialAngle + 22f;
	}

	// Token: 0x060040D9 RID: 16601 RVA: 0x001BB469 File Offset: 0x001B9669
	private void updateDimming()
	{
	}

	// Token: 0x060040DA RID: 16602 RVA: 0x001BB46B File Offset: 0x001B966B
	private float getInitialAngle()
	{
		return (float)(45 * this._era_manager.getCurrentSlotIndex());
	}

	// Token: 0x060040DB RID: 16603 RVA: 0x001BB47C File Offset: 0x001B967C
	private void setRotation(Transform pElement, float pAngle)
	{
		Quaternion tRotation = Quaternion.AngleAxis(pAngle, Vector3.back);
		pElement.localRotation = tRotation;
	}

	// Token: 0x060040DC RID: 16604 RVA: 0x001BB49C File Offset: 0x001B969C
	private void OnDisable()
	{
		this._floating_tween.Pause<Tweener>();
	}

	// Token: 0x060040DD RID: 16605 RVA: 0x001BB4AA File Offset: 0x001B96AA
	public IReadOnlyCollection<WorldAgeWheelPiece> getPieces()
	{
		return this._pieces;
	}

	// Token: 0x060040DE RID: 16606 RVA: 0x001BB4B2 File Offset: 0x001B96B2
	public WorldAgeWheelPiece getPiece(int pIndex)
	{
		return this._pieces[pIndex];
	}

	// Token: 0x04002EFE RID: 12030
	private const int ANGLE_PER_PIECE = 45;

	// Token: 0x04002EFF RID: 12031
	[SerializeField]
	private Sprite _sprite_arrow;

	// Token: 0x04002F00 RID: 12032
	[SerializeField]
	private Sprite _sprite_arrow_pause;

	// Token: 0x04002F01 RID: 12033
	[SerializeField]
	private Image _image_arrow_main;

	// Token: 0x04002F02 RID: 12034
	[SerializeField]
	private Image _image_arrow_secondary;

	// Token: 0x04002F03 RID: 12035
	[SerializeField]
	private Transform _arrow_container_main;

	// Token: 0x04002F04 RID: 12036
	[SerializeField]
	private Transform _arrow_container_secondary;

	// Token: 0x04002F05 RID: 12037
	[SerializeField]
	private Transform _dimming_container;

	// Token: 0x04002F06 RID: 12038
	private WorldAgeWheelPiece[] _pieces;

	// Token: 0x04002F07 RID: 12039
	private Tweener _floating_tween;

	// Token: 0x04002F08 RID: 12040
	private bool _initialized;

	// Token: 0x04002F09 RID: 12041
	private float _target_arrow_angle_main;

	// Token: 0x04002F0A RID: 12042
	private float _target_arrow_angle_secondary;

	// Token: 0x04002F0B RID: 12043
	private float _current_arrow_angle_main;

	// Token: 0x04002F0C RID: 12044
	private float _current_arrow_angle_secondary;
}
