using System;
using UnityEngine;

// Token: 0x020003F8 RID: 1016
public class Crabzilla : BaseActorComponent
{
	// Token: 0x06002315 RID: 8981 RVA: 0x00124998 File Offset: 0x00122B98
	internal override void create(Actor pActor)
	{
		base.create(pActor);
		base.transform.position = this.actor.current_position;
		this.bodyPos = new Vector3(0f, 27.8f, 0f);
		this.bodyPosTarget = new Vector3(0f, 27.8f, 0f);
		this.mouthSpriteAnim = this.mouthSprite.GetComponent<SpriteAnimation>();
		this.createLimbs();
		ControllableUnit.setControllableCreatureCrabzilla(this.actor);
		if (Config.isMobile)
		{
			WorldTip.showNow("crabzilla_controls_mobile", true, "top", 8f, "#F3961F");
		}
		else
		{
			WorldTip.showNow("crabzilla_controls_pc", true, "top", 8f, "#F3961F");
		}
		if (Config.joyControls)
		{
			UltimateJoystick.ResetJoysticks();
		}
		Vector3 tCur = base.transform.position;
		tCur.z = this.z_pos;
		base.transform.position = tCur;
		this.actor.current_position = base.transform.position;
	}

	// Token: 0x06002316 RID: 8982 RVA: 0x00124AA6 File Offset: 0x00122CA6
	public bool isBeamEnabled()
	{
		return this._beam_enabled;
	}

	// Token: 0x06002317 RID: 8983 RVA: 0x00124AAE File Offset: 0x00122CAE
	internal void legMoved()
	{
		if (this.bodyPosTimeout > 0f)
		{
			return;
		}
		this.bodyPosTarget.y = 27.8f + Randy.randomFloat(-3f, 3f);
	}

	// Token: 0x06002318 RID: 8984 RVA: 0x00124AE0 File Offset: 0x00122CE0
	public override void update(float pElapsed)
	{
		if (this.bodyPosTimeout > 0f)
		{
			this.bodyPosTimeout -= pElapsed;
		}
		this.arm1.update(pElapsed);
		this.arm2.update(pElapsed);
		CrabLeg[] array = this.list_legs;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].update(pElapsed);
		}
		if (this.isAnyLimbFlickering())
		{
			this.list_limbs[this.active_limb].update(pElapsed);
		}
		bool tAttackPressed = ControllableUnit.isAttackPressedLeft();
		this._beam_enabled = tAttackPressed;
		this.mouthSprite.SetActive(this.isBeamEnabled());
		if (this.mouthSprite.gameObject.activeSelf)
		{
			this.mouthSpriteAnim.update(pElapsed);
			MusicBox.inst.playDrawingSound("event:/SFX/UNIQUE/Crabzilla/CrabzillaVoice", this.actor.current_position.x, this.actor.current_position.y);
		}
		Vector2 tMovementVector = ControllableUnit.getMovementVector();
		if (!ControllableUnit.isMovementActionActive())
		{
			tMovementVector = Vector2.zero;
		}
		if (tMovementVector.x > 0f)
		{
			this.bodyRotationTarget.z = this.moveRotationLimit;
		}
		else if (tMovementVector.x < 0f)
		{
			this.bodyRotationTarget.z = -this.moveRotationLimit;
		}
		else
		{
			this.bodyRotationTarget.z = 0f;
		}
		float tElapsed = World.world.elapsed * 60f;
		this.bodyRotation = Vector3.MoveTowards(this.bodyRotation, this.bodyRotationTarget, 0.7f * tElapsed);
		if (tMovementVector.y > 0f && this.bodyRotation.z > this.moveRotationLimit)
		{
			this.bodyRotation.z = this.moveRotationLimit;
		}
		else if (tMovementVector.y < 0f && this.bodyRotation.z < -this.moveRotationLimit)
		{
			this.bodyRotation.z = -this.moveRotationLimit;
		}
		this.bodyPos.z = 0f;
		this.bodyPosTarget.z = 0f;
		this.mainBody.transform.localRotation = Quaternion.Euler(this.bodyRotation);
		this.bodyPos = Vector2.MoveTowards(this.bodyPos, this.bodyPosTarget, 0.7f * tElapsed);
		this.mainBody.transform.localPosition = this.bodyPos;
		Vector3 tNewPos = base.transform.position;
		if (!object.Equals(tMovementVector, Vector2.zero))
		{
			Vector2 tCur = base.transform.position;
			tCur = Vector2.MoveTowards(tCur, tCur + tMovementVector * 0.2f * tElapsed, 1f * tElapsed);
			tNewPos = new Vector3(tCur.x, tCur.y);
			if (tNewPos.x < 0f)
			{
				tNewPos.x = 0f;
			}
			if (tNewPos.y < 0f)
			{
				tNewPos.y = 0f;
			}
			if (tNewPos.x > (float)MapBox.width)
			{
				tNewPos.x = (float)MapBox.width;
			}
			if (tNewPos.y > (float)MapBox.height)
			{
				tNewPos.y = (float)MapBox.height;
			}
			tNewPos.z = this.z_pos;
		}
		tNewPos.x += this.actor.shake_offset.x;
		tNewPos.y += this.actor.shake_offset.y;
		base.transform.position = tNewPos;
		this.actor.current_position = base.transform.position;
		this.actor.dirty_current_tile = true;
		this.updateArms();
	}

	// Token: 0x06002319 RID: 8985 RVA: 0x00124E9C File Offset: 0x0012309C
	private void updateArms()
	{
		if (Config.joyControls)
		{
			Vector2 tCur = this.armTarget.transform.position;
			float tVerticalAxis = ControllableUnit.getJoyAxisVerticalRight();
			float tHorizontalAxis = ControllableUnit.getJoyAxisHorizontalRight();
			Vector2 tPosVec = new Vector2(tHorizontalAxis, tVerticalAxis);
			if (!object.Equals(tPosVec, Vector2.zero))
			{
				tCur = Vector2.MoveTowards(tCur, tCur + tPosVec * 2f, 1f);
				if (Toolbox.DistVec3(tCur, base.transform.position) > 35f)
				{
					tCur = Vector2.MoveTowards(base.transform.position, tCur, 35f);
				}
			}
			this.armTarget.transform.position = tCur;
			return;
		}
		Vector3 tMousePos = World.world.getMousePos();
		this.armTarget.transform.position = tMousePos;
	}

	// Token: 0x0600231A RID: 8986 RVA: 0x00124F88 File Offset: 0x00123188
	private void createLimbs()
	{
		this.list_joints = base.GetComponentsInChildren<CrabLegJoint>(false);
		CrabLegJoint[] array = this.list_joints;
		for (int j = 0; j < array.Length; j++)
		{
			array[j].crabzilla = this;
		}
		this.list_legs = base.GetComponentsInChildren<CrabLeg>(false);
		CrabLeg[] array2 = this.list_legs;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].crabzilla = this;
		}
		this.arm1.crabzilla = this;
		this.arm2.crabzilla = this;
		this.list_limbs = new CrabLimbGroup[Enum.GetNames(typeof(CrabLimb)).Length];
		for (int i = 0; i < this.list_limbs.Length; i++)
		{
			this.list_limbs[i] = new CrabLimbGroup((CrabLimb)i, this.actor);
		}
		this.list_limbs.Shuffle<CrabLimbGroup>();
		foreach (CrabLeg crabLeg in this.list_legs)
		{
			crabLeg.create();
			crabLeg.update(World.world.delta_time);
		}
		foreach (CrabLegJoint crabLegJoint in this.list_joints)
		{
			crabLegJoint.create();
			crabLegJoint.LateUpdate();
		}
		this.update(World.world.delta_time);
		array2 = this.list_legs;
		for (int j = 0; j < array2.Length; j++)
		{
			array2[j].moveLeg();
		}
	}

	// Token: 0x0600231B RID: 8987 RVA: 0x001250D0 File Offset: 0x001232D0
	internal static bool getHit(BaseSimObject pSelf, BaseSimObject pAttackedBy = null, WorldTile pTile = null)
	{
		Actor a = pSelf.a;
		Crabzilla tZilla = a.getActorComponent<Crabzilla>();
		if (a.getHealthRatio() > 0.45f)
		{
			return true;
		}
		tZilla.ShowLimbDamage();
		return true;
	}

	// Token: 0x0600231C RID: 8988 RVA: 0x00125100 File Offset: 0x00123300
	public void ShowLimbDamage()
	{
		if (this.isAnyLimbFlickering())
		{
			return;
		}
		this.active_limb++;
		if (this.active_limb >= this.list_limbs.Length)
		{
			this.active_limb = 0;
			this.list_limbs.Shuffle<CrabLimbGroup>();
		}
		this.actor.startShake(0.05f, 0.1f, true, true);
		this.list_limbs[this.active_limb].showDamage();
	}

	// Token: 0x0600231D RID: 8989 RVA: 0x0012516F File Offset: 0x0012336F
	private bool isAnyLimbFlickering()
	{
		return this.active_limb != -1 && this.list_limbs[this.active_limb].IsFlickering();
	}

	// Token: 0x0400194D RID: 6477
	internal const float HIGH_HP_THRESHOLD = 0.7f;

	// Token: 0x0400194E RID: 6478
	internal const float MED_HP_THRESHOLD = 0.35f;

	// Token: 0x0400194F RID: 6479
	private CrabLeg[] list_legs;

	// Token: 0x04001950 RID: 6480
	private CrabLegJoint[] list_joints;

	// Token: 0x04001951 RID: 6481
	private CrabLimbGroup[] list_limbs;

	// Token: 0x04001952 RID: 6482
	private int active_limb = -1;

	// Token: 0x04001953 RID: 6483
	public CrabBody mainBody;

	// Token: 0x04001954 RID: 6484
	internal const float angle0_min = -20f;

	// Token: 0x04001955 RID: 6485
	internal const float angle0_max = 30f;

	// Token: 0x04001956 RID: 6486
	public GameObject armTarget;

	// Token: 0x04001957 RID: 6487
	public GameObject mouthSprite;

	// Token: 0x04001958 RID: 6488
	private SpriteAnimation mouthSpriteAnim;

	// Token: 0x04001959 RID: 6489
	private bool _beam_enabled;

	// Token: 0x0400195A RID: 6490
	private Vector3 bodyRotationTarget;

	// Token: 0x0400195B RID: 6491
	private Vector3 bodyRotation;

	// Token: 0x0400195C RID: 6492
	private float moveRotationLimit = 5f;

	// Token: 0x0400195D RID: 6493
	private Vector3 bodyPosTarget;

	// Token: 0x0400195E RID: 6494
	private Vector3 bodyPos;

	// Token: 0x0400195F RID: 6495
	private float bodyPosTimeout;

	// Token: 0x04001960 RID: 6496
	public CrabArm arm1;

	// Token: 0x04001961 RID: 6497
	public CrabArm arm2;

	// Token: 0x04001962 RID: 6498
	public float z_pos = 10f;
}
