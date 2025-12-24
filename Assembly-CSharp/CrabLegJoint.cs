using System;
using UnityEngine;

// Token: 0x020003F2 RID: 1010
public class CrabLegJoint : MonoBehaviour
{
	// Token: 0x06002307 RID: 8967 RVA: 0x001241CC File Offset: 0x001223CC
	internal void create()
	{
		this.actual_z_pos = base.transform.localPosition.z;
		this.length0 = Vector2.Distance(this.Joint0.position, this.Joint1.position);
		this.length1 = Vector2.Distance(this.Joint1.position, this.Hand.position);
		bool flag = this.mirrored;
		this.targetDistance = Vector2.Distance(this.Joint0.position, this.Target.position);
		Vector2 diff = this.Target.position - this.Joint0.position;
		this.atan = -this.crabzilla.transform.rotation.eulerAngles.z + Mathf.Atan2(diff.y, diff.x) * 57.29578f;
		float cosAngle0 = (this.targetDistance * this.targetDistance + this.length0 * this.length0 - this.length1 * this.length1) / (2f * this.targetDistance * this.length0);
		this.defaultAngle = Mathf.Acos(cosAngle0) * 57.29578f;
		this.angleMin = this.defaultAngle + 20f;
		this.angleMax = this.defaultAngle + 20f;
		this.bodyPoint = new GameObject("leg_point_" + base.transform.name)
		{
			transform = 
			{
				position = new Vector3(base.transform.position.x, base.transform.position.y, 0f),
				parent = this.crabzilla.mainBody.transform
			}
		}.transform;
	}

	// Token: 0x06002308 RID: 8968 RVA: 0x001243C0 File Offset: 0x001225C0
	public bool isAngleOk(float pMinAngle, float pMaxAngle)
	{
		this.angleMin = this.defaultAngle + pMinAngle;
		this.angleMax = this.defaultAngle + pMaxAngle;
		bool flag = Toolbox.inBounds(this.angle0, this.angleMin, this.angleMax);
		Vector2 dir = this.Joint1.transform.position - this.Hand.transform.position;
		bool angle1_ok = Toolbox.inBounds(Mathf.Atan2(dir.y, dir.x) * 57.29578f, this.groundAngleMin, this.groundAngleMax);
		return flag && angle1_ok;
	}

	// Token: 0x06002309 RID: 8969 RVA: 0x00124458 File Offset: 0x00122658
	internal void LateUpdate()
	{
		Vector3 tBPos = this.bodyPoint.position;
		tBPos.z = 0f;
		base.transform.position = tBPos;
		base.transform.localPosition = new Vector3(base.transform.localPosition.x, base.transform.localPosition.y, this.actual_z_pos);
		this.targetDistance = Vector2.Distance(this.Joint0.position, this.Target.position);
		Vector2 diff = this.Target.position - this.Joint0.position;
		this.atan = -this.crabzilla.transform.rotation.eulerAngles.z + Mathf.Atan2(diff.y, diff.x) * 57.29578f;
		if (this.length0 + this.length1 < this.targetDistance)
		{
			this.jointAngle0 = this.atan;
			this.jointAngle1 = 0f;
		}
		else
		{
			float cosAngle0 = (this.targetDistance * this.targetDistance + this.length0 * this.length0 - this.length1 * this.length1) / (2f * this.targetDistance * this.length0);
			this.angle0 = Mathf.Acos(cosAngle0) * 57.29578f;
			float cosAngle = (this.length1 * this.length1 + this.length0 * this.length0 - this.targetDistance * this.targetDistance) / (2f * this.length1 * this.length0);
			this.angle1 = Mathf.Acos(cosAngle) * 57.29578f;
			if (this.mirrored)
			{
				this.jointAngle0 = this.atan + this.angle0;
				this.jointAngle1 = 180f + this.angle1;
			}
			else
			{
				this.jointAngle0 = this.atan - this.angle0;
				this.jointAngle1 = 180f - this.angle1;
			}
		}
		if (float.IsNaN(this.jointAngle0))
		{
			return;
		}
		Vector3 Euler0 = this.Joint0.transform.localEulerAngles;
		Euler0.z = this.jointAngle0;
		this.Joint0.transform.localEulerAngles = Euler0;
		Vector3 Euler = this.Joint1.transform.localEulerAngles;
		Euler.z = this.jointAngle1;
		this.Joint1.transform.localEulerAngles = Euler;
	}

	// Token: 0x0400191D RID: 6429
	[Header("Joints")]
	public Transform Joint0;

	// Token: 0x0400191E RID: 6430
	public Transform Joint1;

	// Token: 0x0400191F RID: 6431
	public Transform Hand;

	// Token: 0x04001920 RID: 6432
	[Header("Target")]
	public Transform Target;

	// Token: 0x04001921 RID: 6433
	private float length0;

	// Token: 0x04001922 RID: 6434
	private float length1;

	// Token: 0x04001923 RID: 6435
	public float targetDistance;

	// Token: 0x04001924 RID: 6436
	public bool mirrored;

	// Token: 0x04001925 RID: 6437
	internal Crabzilla crabzilla;

	// Token: 0x04001926 RID: 6438
	public float angleMax;

	// Token: 0x04001927 RID: 6439
	public float angleMin;

	// Token: 0x04001928 RID: 6440
	public float defaultAngle;

	// Token: 0x04001929 RID: 6441
	private float atan;

	// Token: 0x0400192A RID: 6442
	private float jointAngle0;

	// Token: 0x0400192B RID: 6443
	private float jointAngle1;

	// Token: 0x0400192C RID: 6444
	public float angle0;

	// Token: 0x0400192D RID: 6445
	public float angle1;

	// Token: 0x0400192E RID: 6446
	public float groundAngleMin = 50f;

	// Token: 0x0400192F RID: 6447
	public float groundAngleMax = 140f;

	// Token: 0x04001930 RID: 6448
	internal Transform bodyPoint;

	// Token: 0x04001931 RID: 6449
	public float actual_z_pos;
}
