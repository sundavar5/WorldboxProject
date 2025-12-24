using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000364 RID: 868
public class Boulder : BaseEffect
{
	// Token: 0x060020D8 RID: 8408 RVA: 0x00118F00 File Offset: 0x00117100
	public override void Awake()
	{
		base.Awake();
		this.sprite_renderer = this.mainSprite.GetComponent<SpriteRenderer>();
		this.shadowRenderer = this.shadowSprite.GetComponent<SpriteRenderer>();
		this.mainTransform = this.mainSprite.transform;
		this.shadowTransform = this.shadowSprite.transform;
	}

	// Token: 0x060020D9 RID: 8409 RVA: 0x00118F58 File Offset: 0x00117158
	public override void update(float pElapsed)
	{
		base.update(pElapsed);
		this.updateForce(pElapsed);
		if (this.impactEffect > 0f)
		{
			this.impactEffect -= pElapsed;
		}
		if (this.position_height != 0f)
		{
			this.angle += this.angleRotation * pElapsed;
			this.mainTransform.localEulerAngles = new Vector3(0f, 0f, this.angle);
		}
	}

	// Token: 0x060020DA RID: 8410 RVA: 0x00118FD0 File Offset: 0x001171D0
	private void updateForce(float pElapsed)
	{
		this._force_timer -= pElapsed * 2.5f;
		if (this._force_timer <= 0f)
		{
			this._force_timer = 2f;
			this.actionLanded();
			return;
		}
		float tHeight = this.getHeightPosition();
		Vector2 tUpdatedBouncePos = this.calcCurrentPos();
		this.setCurrentPosition(tUpdatedBouncePos.x, tUpdatedBouncePos.y, tHeight);
		this.updateCurrentPosition();
	}

	// Token: 0x060020DB RID: 8411 RVA: 0x00119038 File Offset: 0x00117238
	private void updateShadow()
	{
		float value = (this.position_height / -5f + 10f) / 10f;
		float tShadowAlpha = Mathf.Clamp(value, 0.15f, 1f) * 0.3f;
		this.setShadowAlpha(tShadowAlpha);
		float tShadowScale = Mathf.Clamp(value, 0.25f, 0.9f) * 0.3f;
		Vector3 tScale = this.shadowTransform.localScale;
		tScale.Set(tShadowScale, tShadowScale, 1f);
		this.shadowTransform.localScale = tScale;
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x001190B8 File Offset: 0x001172B8
	private void setShadowAlpha(float pVal)
	{
		float alpha = pVal;
		if (alpha < 0f)
		{
			alpha = 0f;
		}
		Color tColor = this.shadowRenderer.color;
		tColor.a = alpha;
		this.shadowRenderer.color = tColor;
	}

	// Token: 0x060020DD RID: 8413 RVA: 0x001190F8 File Offset: 0x001172F8
	private void spawnEffect(string pEffectID)
	{
		if (this.impactEffect > 0f)
		{
			return;
		}
		this.impactEffect = 0.8f;
		Vector3 tVec = this.current_position;
		tVec.y -= 2f;
		EffectsLibrary.spawnAt(pEffectID, tVec, this.mainTransform.localScale.x);
	}

	// Token: 0x060020DE RID: 8414 RVA: 0x00119154 File Offset: 0x00117354
	internal void actionLanded()
	{
		this._previous_bounce_position = this.current_position;
		this._bounces_left--;
		this.current_tile = World.world.GetTile((int)base.transform.localPosition.x, (int)base.transform.localPosition.y);
		bool tBounceAgain = true;
		if (this.current_tile != null && this.current_tile.Type.lava)
		{
			tBounceAgain = false;
		}
		if (this._bounces_left < 1)
		{
			tBounceAgain = false;
		}
		if (tBounceAgain)
		{
			this.sequencedBounce();
			return;
		}
		this.explosion();
	}

	// Token: 0x060020DF RID: 8415 RVA: 0x001191E8 File Offset: 0x001173E8
	private void sequencedBounce()
	{
		Vector3 tVec = this.current_position;
		tVec.y -= 2f;
		EffectsLibrary.spawnExplosionWave(tVec, (float)this._bounces_left * 0.14f, 6f);
		World.world.startShake(0.3f, 0.01f, 1f, false, true);
		if (!Toolbox.inMapBorder(ref this.current_position))
		{
			this.spawnEffect("fx_boulder_impact_water");
			return;
		}
		if (this.current_tile == null)
		{
			return;
		}
		if (this.current_tile.Type.ocean)
		{
			this.spawnEffect("fx_boulder_impact_water");
		}
		else
		{
			this.spawnEffect("fx_boulder_impact");
		}
		World.world.loopWithBrush(this.current_tile, Brush.get(5, "circ_"), new PowerActionWithID(Boulder.tileDrawBoulder), null);
		World.world.applyForceOnTile(this.current_tile, 5, 0.5f, false, 0, null, null, null, false);
		World.world.conway_layer.checkKillRange(this.current_tile.pos, 5);
	}

	// Token: 0x060020E0 RID: 8416 RVA: 0x001192F0 File Offset: 0x001174F0
	private void explosion()
	{
		if (this.current_tile == null || this.current_tile.Type.ocean)
		{
			this.spawnEffect("fx_boulder_impact_water");
		}
		else
		{
			this.spawnEffect("fx_boulder_impact");
		}
		this.impactEffect = 0f;
		if (Toolbox.inMapBorder(ref this.current_position))
		{
			MapAction.damageWorld(this.current_tile, 10, AssetManager.terraform.get("bomb"), null);
		}
		this.spawnEffect("fx_explosion_small");
		this.controller.killObject(this);
	}

	// Token: 0x060020E1 RID: 8417 RVA: 0x0011937C File Offset: 0x0011757C
	public static bool tileDrawBoulder(WorldTile pTile, string pPowerID)
	{
		pTile.doUnits(delegate(Actor pActor)
		{
			AchievementLibrary.ball_to_ball.checkBySignal(pActor);
			pActor.getHitFullHealth(AttackType.Gravity);
		});
		if (pTile.Type.ocean && Randy.randomChance(0.3f))
		{
			World.world.drop_manager.spawnParabolicDrop(pTile, "rain", 0f, 1f, 30f, 0.7f, 22f, -1f);
		}
		if (pTile.Type.lava && Randy.randomChance(0.3f))
		{
			World.world.drop_manager.spawnParabolicDrop(pTile, "lava", 0f, 1f, 30f, 0.7f, 22f, -1f);
		}
		MapAction.decreaseTile(pTile, true, "destroy");
		return true;
	}

	// Token: 0x060020E2 RID: 8418 RVA: 0x00119454 File Offset: 0x00117654
	public void spawnOn(Vector2 pPosition)
	{
		this._bounce_positions.Clear();
		if (Boulder.isRandomLaunch(pPosition))
		{
			this._force_timer = 1f;
		}
		else
		{
			this._force_timer = 2f;
		}
		this._bounces_left = 3;
		this.angle = 0f;
		this.angleRotation = Randy.randomFloat(-200f, 200f);
		this.impactEffect = 0f;
		Vector2 tForce;
		if (Boulder.isRandomLaunch(pPosition))
		{
			tForce.x = Randy.randomFloat(-40f, 40f);
			tForce.y = Randy.randomFloat(-40f, 40f);
			tForce = Vector2.ClampMagnitude(tForce, 40f);
		}
		else
		{
			tForce = Boulder.chargeVector(pPosition) * 0.777f;
		}
		this._previous_bounce_position = pPosition;
		this._previous_bounce_position.y = this._previous_bounce_position.y - this.getHeightPosition();
		this._previous_bounce_position -= tForce * this.getBounceProgress();
		for (int i = 0; i < 3; i++)
		{
			int tMultiplier = i + 1;
			Vector2 tVector = default(Vector2);
			tVector.x = this._previous_bounce_position.x + tForce.x * (float)tMultiplier;
			tVector.y = this._previous_bounce_position.y + tForce.y * (float)tMultiplier;
			this._bounce_positions.Add(tVector);
		}
		this.updateCurrentPosition();
		Boulder.endCharging();
	}

	// Token: 0x060020E3 RID: 8419 RVA: 0x001195B1 File Offset: 0x001177B1
	private void setCurrentPosition(float pX, float pY, float pHeight)
	{
		this.current_position.x = pX;
		this.current_position.y = pY;
		this.position_height = pHeight;
	}

	// Token: 0x060020E4 RID: 8420 RVA: 0x001195D4 File Offset: 0x001177D4
	private void updateCurrentPosition()
	{
		Vector3 tPosition = base.transform.localPosition;
		tPosition.x = this.current_position.x;
		tPosition.y = this.current_position.y;
		tPosition.z = this.position_height + 5f;
		base.transform.localPosition = tPosition;
		Vector3 tHeight = this.mainTransform.localPosition;
		tHeight.y = this.position_height;
		this.mainTransform.localPosition = tHeight;
		this.updateShadow();
	}

	// Token: 0x060020E5 RID: 8421 RVA: 0x0011965B File Offset: 0x0011785B
	private float getBounceProgress()
	{
		return 1f - this._force_timer / 2f;
	}

	// Token: 0x060020E6 RID: 8422 RVA: 0x0011966F File Offset: 0x0011786F
	private float getBounceProgressMirrored()
	{
		return 1f - Mathf.Abs(this.getBounceProgress() * 2f - 1f);
	}

	// Token: 0x060020E7 RID: 8423 RVA: 0x0011968E File Offset: 0x0011788E
	private float getHeightProgress()
	{
		return iTween.easeOutQuad(0f, 1f, this.getBounceProgressMirrored());
	}

	// Token: 0x060020E8 RID: 8424 RVA: 0x001196A5 File Offset: 0x001178A5
	private float getHeightPosition()
	{
		return (float)this._bounces_left * this.getHeightProgress() * 10f;
	}

	// Token: 0x060020E9 RID: 8425 RVA: 0x001196BB File Offset: 0x001178BB
	private int getCurrentBounceIndex()
	{
		return 3 - this._bounces_left;
	}

	// Token: 0x060020EA RID: 8426 RVA: 0x001196C5 File Offset: 0x001178C5
	private Vector2 getNextBouncePos()
	{
		return this._bounce_positions[this.getCurrentBounceIndex()];
	}

	// Token: 0x060020EB RID: 8427 RVA: 0x001196D8 File Offset: 0x001178D8
	private Vector2 calcCurrentPos()
	{
		return Vector2.Lerp(this._previous_bounce_position, this.getNextBouncePos(), this.getBounceProgress());
	}

	// Token: 0x060020EC RID: 8428 RVA: 0x001196F4 File Offset: 0x001178F4
	public static void chargeBoulder(Vector2 pPosition, Touch pTouch = default(Touch))
	{
		Boulder._latest_touch = pTouch;
		if (ScrollWindow.isWindowActive())
		{
			Boulder.endCharging();
			return;
		}
		if (HotkeyLibrary.many_mod.isHolding() || (!InputHelpers.mouseSupported && DebugConfig.isOn(DebugOption.FastSpawn)))
		{
			if (Boulder._charge_started)
			{
				Boulder.endCharging();
			}
			Boulder.releaseManyBoulders(pPosition);
			return;
		}
		if (Boulder.isInteractionJustStarted())
		{
			Boulder.startCharging(pPosition);
			return;
		}
		if (Boulder.isInteractionJustEnded())
		{
			Boulder.releaseBoulder();
		}
	}

	// Token: 0x060020ED RID: 8429 RVA: 0x0011975D File Offset: 0x0011795D
	private static void startCharging(Vector2 pPosition)
	{
		Boulder._charge_started = true;
		Boulder._initial_charge_position = pPosition;
		Boulder._latest_touch_id = Boulder._latest_touch.fingerId;
	}

	// Token: 0x060020EE RID: 8430 RVA: 0x0011977A File Offset: 0x0011797A
	private static void endCharging()
	{
		Boulder._charge_started = false;
		Boulder._latest_touch_id = -2;
	}

	// Token: 0x060020EF RID: 8431 RVA: 0x00119789 File Offset: 0x00117989
	public static void checkRelease()
	{
		if (!Boulder._charge_started)
		{
			return;
		}
		if (!Boulder.isBoulderPowerSelected())
		{
			Boulder.endCharging();
			return;
		}
		Boulder.spawnParticles();
		if (Boulder.isInteractionJustEnded())
		{
			Boulder.releaseBoulder();
		}
	}

	// Token: 0x060020F0 RID: 8432 RVA: 0x001197B1 File Offset: 0x001179B1
	private static void releaseManyBoulders(Vector2 pPosition)
	{
		Boulder._initial_charge_position = pPosition;
		Boulder.releaseBoulder();
	}

	// Token: 0x060020F1 RID: 8433 RVA: 0x001197C0 File Offset: 0x001179C0
	private static void releaseBoulder()
	{
		Vector2 tPosition = Boulder.getPointerPosition();
		EffectsLibrary.spawnAt("fx_boulder", tPosition, 1f);
	}

	// Token: 0x060020F2 RID: 8434 RVA: 0x001197E4 File Offset: 0x001179E4
	private static void spawnParticles()
	{
		Vector2 tPosition = Vector2.zero;
		if (!Boulder.getPointerPositionPure(ref tPosition))
		{
			return;
		}
		if (Boulder.isRandomLaunch(tPosition))
		{
			return;
		}
		EffectsLibrary.spawnAt("fx_boulder_charge", tPosition, 1f);
	}

	// Token: 0x060020F3 RID: 8435 RVA: 0x0011981C File Offset: 0x00117A1C
	private static bool isRandomLaunch(Vector2 pPosition)
	{
		return Boulder.chargeVector(pPosition).magnitude < 1.5f;
	}

	// Token: 0x060020F4 RID: 8436 RVA: 0x0011983E File Offset: 0x00117A3E
	private static bool isBoulderPowerSelected()
	{
		PowerButton selectedButton = PowerButtonSelector.instance.selectedButton;
		string a;
		if (selectedButton == null)
		{
			a = null;
		}
		else
		{
			GodPower godPower = selectedButton.godPower;
			a = ((godPower != null) ? godPower.id : null);
		}
		return a == "bowling_ball";
	}

	// Token: 0x060020F5 RID: 8437 RVA: 0x0011986C File Offset: 0x00117A6C
	private static Vector2 chargeVector(Vector2 pPosition)
	{
		return Boulder._initial_charge_position - pPosition;
	}

	// Token: 0x060020F6 RID: 8438 RVA: 0x00119879 File Offset: 0x00117A79
	public static Vector2 chargeVector()
	{
		return Boulder.chargeVector(Boulder.getPointerPosition());
	}

	// Token: 0x060020F7 RID: 8439 RVA: 0x00119885 File Offset: 0x00117A85
	private static bool isInteractionJustStarted()
	{
		if (Boulder._charge_started)
		{
			return false;
		}
		if (InputHelpers.mouseSupported)
		{
			if (Input.GetMouseButtonDown(0))
			{
				return true;
			}
		}
		else if (Boulder._latest_touch.fingerId != Boulder._latest_touch_id)
		{
			return true;
		}
		return false;
	}

	// Token: 0x060020F8 RID: 8440 RVA: 0x001198B5 File Offset: 0x00117AB5
	private static bool isInteractionJustEnded()
	{
		if (InputHelpers.mouseSupported)
		{
			if (Input.GetMouseButtonUp(0))
			{
				return true;
			}
		}
		else if (Input.touchCount == 0 || Boulder._latest_touch.phase == TouchPhase.Ended)
		{
			return true;
		}
		return false;
	}

	// Token: 0x060020F9 RID: 8441 RVA: 0x001198DF File Offset: 0x00117ADF
	private static Vector2 getPointerPosition()
	{
		if (InputHelpers.mouseSupported)
		{
			return World.world.getMousePos();
		}
		return World.world.camera.ScreenToWorldPoint(Boulder._latest_touch.position);
	}

	// Token: 0x060020FA RID: 8442 RVA: 0x00119918 File Offset: 0x00117B18
	private static bool getPointerPositionPure(ref Vector2 pPosition)
	{
		if (InputHelpers.mouseSupported)
		{
			pPosition = World.world.getMousePos();
			return true;
		}
		Touch tTouch;
		if (World.world.player_control.getTouchPos(out tTouch, false))
		{
			pPosition = World.world.camera.ScreenToWorldPoint(tTouch.position);
			return true;
		}
		return false;
	}

	// Token: 0x04001843 RID: 6211
	private const float SPEED = 2.5f;

	// Token: 0x04001844 RID: 6212
	private const int BOUNCES_AMOUNT = 3;

	// Token: 0x04001845 RID: 6213
	private const float SINGLE_BOUNCE_TIMER = 2f;

	// Token: 0x04001846 RID: 6214
	private const float BASE_HEIGHT_MULTIPLIER = 10f;

	// Token: 0x04001847 RID: 6215
	private const float BASE_LENGTH_MULTIPLIER = 40f;

	// Token: 0x04001848 RID: 6216
	private const float INITIAL_ANGLE_RANGE = 200f;

	// Token: 0x04001849 RID: 6217
	private const float CHARGE_VECTOR_MULTIPLIER = 0.777f;

	// Token: 0x0400184A RID: 6218
	private const float Z_SORTING_FIX = 5f;

	// Token: 0x0400184B RID: 6219
	private const int NO_TOUCH_ID = -2;

	// Token: 0x0400184C RID: 6220
	private float angle;

	// Token: 0x0400184D RID: 6221
	private float angleRotation;

	// Token: 0x0400184E RID: 6222
	private float impactEffect;

	// Token: 0x0400184F RID: 6223
	public GameObject mainSprite;

	// Token: 0x04001850 RID: 6224
	public GameObject shadowSprite;

	// Token: 0x04001851 RID: 6225
	private SpriteRenderer shadowRenderer;

	// Token: 0x04001852 RID: 6226
	private Transform mainTransform;

	// Token: 0x04001853 RID: 6227
	private Transform shadowTransform;

	// Token: 0x04001854 RID: 6228
	private Vector2 _previous_bounce_position;

	// Token: 0x04001855 RID: 6229
	private List<Vector2> _bounce_positions = new List<Vector2>();

	// Token: 0x04001856 RID: 6230
	private int _bounces_left;

	// Token: 0x04001857 RID: 6231
	private float _force_timer;

	// Token: 0x04001858 RID: 6232
	private static bool _charge_started;

	// Token: 0x04001859 RID: 6233
	private static Vector2 _initial_charge_position;

	// Token: 0x0400185A RID: 6234
	private static Touch _latest_touch;

	// Token: 0x0400185B RID: 6235
	private static int _latest_touch_id = -2;
}
