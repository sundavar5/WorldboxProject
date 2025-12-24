using System;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020006A0 RID: 1696
public class LivingIcon : MonoBehaviour
{
	// Token: 0x06003656 RID: 13910 RVA: 0x0018B4EE File Offset: 0x001896EE
	private void Awake()
	{
		this.init_position = base.transform.position;
	}

	// Token: 0x06003657 RID: 13911 RVA: 0x0018B501 File Offset: 0x00189701
	public void kill()
	{
		LivingIcon.killed_mod++;
		base.enabled = false;
	}

	// Token: 0x06003658 RID: 13912 RVA: 0x0018B518 File Offset: 0x00189718
	public void Update()
	{
		Vector3 tMousePos = Input.mousePosition;
		float num = Vector2.Distance(base.transform.position, tMousePos);
		float tDistToFly = (float)(80 + LivingIcon.killed_mod * 10);
		if (num < tDistToFly)
		{
			if (this.speed_away == 0f && LivingIcon.killed_mod > 6)
			{
				this.speed_away = (float)(LivingIcon.killed_mod * 10);
			}
			this.speed_away += 200f * Time.deltaTime * (float)LivingIcon.killed_mod;
		}
		else if (this.speed_away > 0f)
		{
			this.speed_away -= 500f * Time.deltaTime;
			if (this.speed_away < 0f)
			{
				this.speed_away = 0f;
			}
		}
		if (this.speed_away > 0f)
		{
			base.transform.position = Vector2.MoveTowards(base.transform.position, tMousePos, -1f * this.speed_away * Time.deltaTime);
			this.return_timer = 1f;
			this.speed_back = 0f;
			this.<Update>g__rotate|7_0();
			return;
		}
		if (this.return_timer > 0f)
		{
			this.return_timer -= Time.deltaTime;
			return;
		}
		if (Vector2.Distance(base.transform.position, this.init_position) > 1f)
		{
			this.speed_back += Time.deltaTime * 400f;
			base.transform.position = Vector2.MoveTowards(base.transform.position, this.init_position, Time.deltaTime * this.speed_back);
			return;
		}
		this.speed_back = 0f;
	}

	// Token: 0x0600365B RID: 13915 RVA: 0x0018B6F8 File Offset: 0x001898F8
	[CompilerGenerated]
	private void <Update>g__rotate|7_0()
	{
		Vector3 tVec = base.transform.eulerAngles;
		tVec.z += 10f;
		base.transform.eulerAngles = tVec;
	}

	// Token: 0x04002842 RID: 10306
	private Vector3 init_position;

	// Token: 0x04002843 RID: 10307
	private float speed_back;

	// Token: 0x04002844 RID: 10308
	private float speed_away;

	// Token: 0x04002845 RID: 10309
	private float return_timer;

	// Token: 0x04002846 RID: 10310
	public static int killed_mod = 1;
}
