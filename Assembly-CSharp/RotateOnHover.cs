using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020005F6 RID: 1526
public class RotateOnHover : MonoBehaviour
{
	// Token: 0x060031D7 RID: 12759 RVA: 0x0017C284 File Offset: 0x0017A484
	private void Awake()
	{
		this._rect_transform = base.GetComponent<RectTransform>();
		this._original_rotation = this._rect_transform.eulerAngles;
		this._original_pivot = this._rect_transform.pivot;
		this._original_shadows = new List<Vector2>();
		foreach (Shadow tShadow in this.shadows)
		{
			this._original_shadows.Add(tShadow.effectDistance);
		}
	}

	// Token: 0x060031D8 RID: 12760 RVA: 0x0017C31C File Offset: 0x0017A51C
	private void Start()
	{
		Button tButton = base.GetComponentInParent<Button>();
		if (tButton == null)
		{
			return;
		}
		tButton.OnHover(delegate()
		{
			this._is_hovering = true;
		});
		tButton.OnHoverOut(delegate()
		{
			this._is_hovering = false;
		});
	}

	// Token: 0x060031D9 RID: 12761 RVA: 0x0017C360 File Offset: 0x0017A560
	private void OnEnable()
	{
		this._is_hovering = false;
		this._rect_transform.eulerAngles = this._original_rotation;
		this._rect_transform.pivot = this._original_pivot;
		for (int i = 0; i < this.shadows.Count; i++)
		{
			this.shadows[i].effectDistance = this._original_shadows[i];
		}
	}

	// Token: 0x060031DA RID: 12762 RVA: 0x0017C3C9 File Offset: 0x0017A5C9
	private void OnDisable()
	{
		this._is_hovering = false;
		this._rect_transform.eulerAngles = Vector3.zero;
	}

	// Token: 0x060031DB RID: 12763 RVA: 0x0017C3E4 File Offset: 0x0017A5E4
	private void Update()
	{
		if (!this._is_hovering)
		{
			float tLerpSpeed = 15f * Time.deltaTime * 0.5f;
			this._rect_transform.pivot = Vector2.Lerp(this._rect_transform.pivot, this._original_pivot, tLerpSpeed);
			for (int i = 0; i < this.shadows.Count; i++)
			{
				this.shadows[i].effectDistance = Vector2.Lerp(this.shadows[i].effectDistance, this._original_shadows[i], tLerpSpeed);
			}
			this._rect_transform.rotation = Quaternion.Lerp(this._rect_transform.rotation, Quaternion.Euler(this._original_rotation), tLerpSpeed);
			return;
		}
		Vector2 vector = this._rect_transform.InverseTransformPoint(Input.mousePosition);
		float tDistanceX = Mathf.Clamp(vector.x / this._rect_transform.rect.width, -0.5f, 0.5f);
		float tDistanceY = Mathf.Clamp(vector.y / this._rect_transform.rect.height, -0.5f, 0.5f);
		Vector3 targetRotation = new Vector3(this._original_rotation.x - tDistanceY * 60f, this._original_rotation.y + tDistanceX * 60f, this._original_rotation.z);
		Vector2 tDistance = new Vector2(tDistanceX * 4f, tDistanceY * 4f);
		foreach (Shadow shadow in this.shadows)
		{
			shadow.effectDistance = tDistance;
		}
		this._rect_transform.pivot = new Vector2(this._original_pivot.x - tDistanceX * 0.1f, this._original_pivot.y - tDistanceY * 0.1f);
		this._rect_transform.rotation = Quaternion.Lerp(this._rect_transform.rotation, Quaternion.Euler(targetRotation), 15f * Time.deltaTime);
	}

	// Token: 0x040025C6 RID: 9670
	private const float TILT_ANGLE = 60f;

	// Token: 0x040025C7 RID: 9671
	private const float TILT_SPEED = 15f;

	// Token: 0x040025C8 RID: 9672
	public List<Shadow> shadows;

	// Token: 0x040025C9 RID: 9673
	private bool _is_hovering;

	// Token: 0x040025CA RID: 9674
	private Vector3 _original_rotation;

	// Token: 0x040025CB RID: 9675
	private Vector2 _original_pivot;

	// Token: 0x040025CC RID: 9676
	private List<Vector2> _original_shadows;

	// Token: 0x040025CD RID: 9677
	private RectTransform _rect_transform;
}
