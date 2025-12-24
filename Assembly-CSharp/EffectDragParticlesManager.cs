using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000683 RID: 1667
public class EffectDragParticlesManager : MonoBehaviour
{
	// Token: 0x0600358B RID: 13707 RVA: 0x0018901F File Offset: 0x0018721F
	private void Awake()
	{
		this._pool = new ObjectPoolGenericMono<EffectParticlesCursor>(this._prefab, base.transform);
		EffectDragParticlesManager.instance = this;
	}

	// Token: 0x0600358C RID: 13708 RVA: 0x0018903E File Offset: 0x0018723E
	private void Update()
	{
		this.updateSpawn();
		this.updateAnimation();
	}

	// Token: 0x0600358D RID: 13709 RVA: 0x0018904C File Offset: 0x0018724C
	private void updateAnimation()
	{
		if (this._pool.countActive() == 0)
		{
			return;
		}
		foreach (EffectParticlesCursor tEffect in this._pool.getListTotal())
		{
			if (tEffect.isActiveAndEnabled)
			{
				tEffect.update();
			}
		}
	}

	// Token: 0x0600358E RID: 13710 RVA: 0x001890B4 File Offset: 0x001872B4
	private void updateSpawn()
	{
		if (!Config.isDraggingItem())
		{
			return;
		}
		if (Config.dragging_item_object == null)
		{
			return;
		}
		if (!Config.dragging_item_object.spawn_particles_on_drag)
		{
			return;
		}
		if ((float)Time.frameCount % this._spawn_interval == 0f)
		{
			this.spawnNew(Config.dragging_item_object.transform.position);
		}
	}

	// Token: 0x0600358F RID: 13711 RVA: 0x00189108 File Offset: 0x00187308
	public void spawnNew(Vector3 pPos)
	{
		EffectParticlesCursor next = this._pool.getNext();
		next.setFrames(this._sprite_sets.GetRandom<SpriteSet>().sprites);
		next.launch();
		next.getAnimation().setActionFinish(new EffectParticlesCursorDelegate(this.finishingEffectAction));
		Vector2 tPos = pPos;
		tPos.x += Randy.randomFloat(-1f, 1f);
		tPos.y += Randy.randomFloat(-1f, 1f);
		next.transform.position = tPos;
	}

	// Token: 0x06003590 RID: 13712 RVA: 0x0018919E File Offset: 0x0018739E
	private void finishingEffectAction(MonoBehaviour pEffectObject)
	{
		this._pool.release(pEffectObject.GetComponent<EffectParticlesCursor>(), true);
	}

	// Token: 0x040027EC RID: 10220
	public static EffectDragParticlesManager instance;

	// Token: 0x040027ED RID: 10221
	private ObjectPoolGenericMono<EffectParticlesCursor> _pool;

	// Token: 0x040027EE RID: 10222
	[SerializeField]
	private EffectParticlesCursor _prefab;

	// Token: 0x040027EF RID: 10223
	[SerializeField]
	private List<SpriteSet> _sprite_sets;

	// Token: 0x040027F0 RID: 10224
	[SerializeField]
	public float _spawn_interval = 10f;
}
