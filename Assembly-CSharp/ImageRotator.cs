using System;
using UnityEngine;

// Token: 0x020005E3 RID: 1507
public class ImageRotator : MonoBehaviour
{
	// Token: 0x0600317F RID: 12671 RVA: 0x0017A7F7 File Offset: 0x001789F7
	private void Update()
	{
		base.transform.Rotate(Vector3.forward * this.rotation_speed * Time.deltaTime, Space.Self);
	}

	// Token: 0x04002553 RID: 9555
	public float rotation_speed = 70f;
}
