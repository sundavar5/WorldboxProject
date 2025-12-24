using System;
using UnityEngine;

// Token: 0x02000346 RID: 838
public class Meteorite : BaseEffect
{
	// Token: 0x06002049 RID: 8265 RVA: 0x0011526E File Offset: 0x0011346E
	public override void Awake()
	{
		base.Awake();
		this._shadow_renderer = this.shadowSprite.GetComponent<SpriteRenderer>();
	}

	// Token: 0x0600204A RID: 8266 RVA: 0x00115287 File Offset: 0x00113487
	internal override void create()
	{
		base.create();
	}

	// Token: 0x0600204B RID: 8267 RVA: 0x00115290 File Offset: 0x00113490
	public void spawnOn(WorldTile pTile, string pTerraformId, Actor pActor)
	{
		this.terraform_asset = pTerraformId;
		this.tile = pTile;
		this._radius = 20;
		base.transform.position = new Vector3(pTile.posV3.x, pTile.posV3.y);
		this.current_position.x = Randy.randomFloat(-200f, 200f);
		this.current_position.y = Randy.randomFloat(200f, 250f);
		this.updateMainSpritePos();
		this.setShadowAlpha(0f);
	}

	// Token: 0x0600204C RID: 8268 RVA: 0x00115320 File Offset: 0x00113520
	private void updateMainSpritePos()
	{
		Vector3 tVec = default(Vector3);
		tVec.x = this.current_position.x;
		tVec.y = this.current_position.y;
		float tDistToGround = this.current_position.y;
		tVec.z = tDistToGround;
		this.mainSprite.transform.localPosition = tVec;
	}

	// Token: 0x0600204D RID: 8269 RVA: 0x00115380 File Offset: 0x00113580
	protected void smoothMovement(Vector2 end, float pElapsed)
	{
		if ((this.current_position - end).sqrMagnitude > 1E-45f)
		{
			this.current_position = Vector2.MoveTowards(this.current_position, end, this._falling_speed * pElapsed);
			this.updateMainSpritePos();
			this.shadowSprite.transform.localPosition = new Vector2(this.current_position.x, this.shadowSprite.transform.localPosition.y);
			return;
		}
		this.explode();
	}

	// Token: 0x0600204E RID: 8270 RVA: 0x0011540C File Offset: 0x0011360C
	public override void update(float pElapsed)
	{
		this.smoothMovement(Vector2.zero, pElapsed);
		this._shadow_alpha += World.world.elapsed * 0.2f;
		this.setShadowAlpha(this._shadow_alpha);
		this.mainSprite.transform.Rotate(this.rotationSpeed * World.world.elapsed);
		this.shadowSprite.transform.Rotate(this.rotationSpeed * World.world.elapsed);
		if (this._timer_smoke > 0f)
		{
			this._timer_smoke -= World.world.elapsed;
			return;
		}
		EffectsLibrary.spawnAt("fx_fire_smoke", this.mainSprite.transform.position, 1f);
		this._timer_smoke = 0.05f;
	}

	// Token: 0x0600204F RID: 8271 RVA: 0x001154E8 File Offset: 0x001136E8
	protected void setShadowAlpha(float pVal)
	{
		this._shadow_alpha = pVal;
		if (this._shadow_alpha < 0f)
		{
			this._shadow_alpha = 0f;
		}
		Color tColor = this._shadow_renderer.color;
		tColor.a = this._shadow_alpha;
		this._shadow_renderer.color = tColor;
	}

	// Token: 0x06002050 RID: 8272 RVA: 0x0011553C File Offset: 0x0011373C
	private void explode()
	{
		World.world.game_stats.data.meteoritesLaunched += 1L;
		MapAction.damageWorld(this.tile, this._radius, AssetManager.terraform.get(this.terraform_asset), this._owner);
		EffectsLibrary.spawnExplosionWave(this.tile.posV3, (float)this._radius, 1f);
		Vector3 tVec = new Vector3((float)this.tile.pos.x, (float)(this.tile.pos.y - 2));
		float tScale = Randy.randomFloat(0.8f, 0.9f);
		EffectsLibrary.spawnAt("fx_explosion_meteorite", tVec, tScale);
		this.addRandomMineral(this.tile);
		this.addRandomMineral(this.tile.zone.getRandomTile());
		this.addRandomMineral(this.tile.zone.getRandomTile());
		this.addRandomMineral(this.tile.zone.getRandomTile());
		this.addRandomMineral(this.tile.zone.getRandomTile());
		this.controller.killObject(this);
	}

	// Token: 0x06002051 RID: 8273 RVA: 0x00115667 File Offset: 0x00113867
	private void addRandomMineral(WorldTile pTile)
	{
		if (pTile == null)
		{
			return;
		}
		World.world.buildings.addBuilding("mineral_adamantine", pTile, true, false, BuildPlacingType.New);
	}

	// Token: 0x06002052 RID: 8274 RVA: 0x00115686 File Offset: 0x00113886
	public static void spawnMeteoriteDisaster(WorldTile pTile, Actor pActor = null)
	{
		EffectsLibrary.spawn("fx_meteorite", pTile, "meteorite_disaster", null, 0f, -1f, -1f, pActor);
	}

	// Token: 0x06002053 RID: 8275 RVA: 0x001156AA File Offset: 0x001138AA
	public static void spawnMeteorite(WorldTile pTile, Actor pActor = null)
	{
		EffectsLibrary.spawn("fx_meteorite", pTile, "meteorite", null, 0f, -1f, -1f, pActor);
	}

	// Token: 0x04001777 RID: 6007
	private SpriteRenderer _shadow_renderer;

	// Token: 0x04001778 RID: 6008
	public Vector3 rotationSpeed = new Vector3(0f, 0f, 50f);

	// Token: 0x04001779 RID: 6009
	private float _falling_speed = 200f;

	// Token: 0x0400177A RID: 6010
	public GameObject mainSprite;

	// Token: 0x0400177B RID: 6011
	public GameObject shadowSprite;

	// Token: 0x0400177C RID: 6012
	private int _radius;

	// Token: 0x0400177D RID: 6013
	private float _shadow_alpha;

	// Token: 0x0400177E RID: 6014
	private float _timer_smoke = 0.01f;

	// Token: 0x0400177F RID: 6015
	public string terraform_asset = "meteorite";

	// Token: 0x04001780 RID: 6016
	private Actor _owner;
}
