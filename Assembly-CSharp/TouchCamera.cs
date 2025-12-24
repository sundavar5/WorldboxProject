using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
public class TouchCamera : MonoBehaviour
{
	// Token: 0x06000026 RID: 38 RVA: 0x00003864 File Offset: 0x00001A64
	private void Awake()
	{
		this._camera = Camera.main;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00003874 File Offset: 0x00001A74
	private void Update()
	{
		if (Input.touchCount == 0)
		{
			this.oldTouchPositions[0] = null;
			this.oldTouchPositions[1] = null;
			return;
		}
		if (Input.touchCount == 1)
		{
			if (this.oldTouchPositions[0] == null || this.oldTouchPositions[1] != null)
			{
				this.oldTouchPositions[0] = new Vector2?(Input.GetTouch(0).position);
				this.oldTouchPositions[1] = null;
				return;
			}
			Vector2 newTouchPosition = Input.GetTouch(0).position;
			Transform transform = base.transform;
			Vector3 position = transform.position;
			Transform transform2 = base.transform;
			Vector2? vector = (this.oldTouchPositions[0] - newTouchPosition) * this._camera.orthographicSize;
			float d = (float)this._camera.pixelHeight;
			transform.position = position + transform2.TransformDirection(((vector != null) ? new Vector2?(vector.GetValueOrDefault() / d * 2f) : null).Value);
			this.oldTouchPositions[0] = new Vector2?(newTouchPosition);
			return;
		}
		else
		{
			if (this.oldTouchPositions[1] == null)
			{
				this.oldTouchPositions[0] = new Vector2?(Input.GetTouch(0).position);
				this.oldTouchPositions[1] = new Vector2?(Input.GetTouch(1).position);
				this.oldTouchVector = (this.oldTouchPositions[0] - this.oldTouchPositions[1]).Value;
				this.oldTouchDistance = this.oldTouchVector.magnitude;
				return;
			}
			Vector2 screen = new Vector2((float)this._camera.pixelWidth, (float)this._camera.pixelHeight);
			Vector2[] newTouchPositions = new Vector2[]
			{
				Input.GetTouch(0).position,
				Input.GetTouch(1).position
			};
			Vector2 newTouchVector = newTouchPositions[0] - newTouchPositions[1];
			float newTouchDistance = newTouchVector.magnitude;
			base.transform.position += base.transform.TransformDirection(((this.oldTouchPositions[0] + this.oldTouchPositions[1] - screen) * this._camera.orthographicSize / screen.y).Value);
			this._camera.orthographicSize = Mathf.Clamp(this._camera.orthographicSize * (this.oldTouchDistance / newTouchDistance), 10f, this.orthographicSizeMax);
			base.transform.position -= base.transform.TransformDirection((newTouchPositions[0] + newTouchPositions[1] - screen) * this._camera.orthographicSize / screen.y);
			this.oldTouchPositions[0] = new Vector2?(newTouchPositions[0]);
			this.oldTouchPositions[1] = new Vector2?(newTouchPositions[1]);
			this.oldTouchVector = newTouchVector;
			this.oldTouchDistance = newTouchDistance;
			return;
		}
	}

	// Token: 0x04000026 RID: 38
	private Vector2?[] oldTouchPositions = new Vector2?[2];

	// Token: 0x04000027 RID: 39
	private Vector2 oldTouchVector;

	// Token: 0x04000028 RID: 40
	private float oldTouchDistance;

	// Token: 0x04000029 RID: 41
	private Camera _camera;

	// Token: 0x0400002A RID: 42
	private const int orthographicSizeMin = 10;

	// Token: 0x0400002B RID: 43
	internal float orthographicSizeMax = 130f;
}
