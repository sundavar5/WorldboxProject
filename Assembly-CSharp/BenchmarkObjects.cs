using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

// Token: 0x0200051C RID: 1308
public class BenchmarkObjects : MonoBehaviour
{
	// Token: 0x06002AE9 RID: 10985 RVA: 0x001553A6 File Offset: 0x001535A6
	public BenchmarkObjects()
	{
		BenchmarkObjects.instance = this;
	}

	// Token: 0x06002AEA RID: 10986 RVA: 0x001553E0 File Offset: 0x001535E0
	private void Update()
	{
		this.update(Time.deltaTime);
		this.total_unity_objects = this.actors_unity.Count;
		this.total_simple_objects = this.actors_simple.Count;
		this.total_simple_objects_visible = this.actors_simple_visible.Count;
	}

	// Token: 0x06002AEB RID: 10987 RVA: 0x00155420 File Offset: 0x00153620
	public void addObjectsSimple(int pAmount = 2000)
	{
		for (int i = 0; i < pAmount; i++)
		{
			TestActorSimpleObject tActor = new TestActorSimpleObject();
			tActor.create(this.some_sprites);
			this.actors_simple.Add(tActor);
		}
	}

	// Token: 0x06002AEC RID: 10988 RVA: 0x00155458 File Offset: 0x00153658
	public void addObjectsUnity(int pAmount = 2000)
	{
		for (int i = 0; i < pAmount; i++)
		{
			TestActorGameObject tActor = Object.Instantiate<TestActorGameObject>(this.prefab_unity_object);
			tActor.create(this.some_sprites);
			tActor.transform.parent = base.transform;
			this.actors_unity.Add(tActor);
		}
	}

	// Token: 0x06002AED RID: 10989 RVA: 0x001554A8 File Offset: 0x001536A8
	public void killAll()
	{
		foreach (TestActorGameObject testActorGameObject in this.actors_unity)
		{
			Object.Destroy(testActorGameObject.gameObject, 0.01f);
		}
		this.actors_unity.Clear();
		this.actors_simple.Clear();
	}

	// Token: 0x06002AEE RID: 10990 RVA: 0x00155518 File Offset: 0x00153718
	public void randomRespawn()
	{
		foreach (TestActorGameObject testActorGameObject in this.actors_unity)
		{
			testActorGameObject.randomRespawn();
		}
		foreach (TestActorSimpleObject testActorSimpleObject in this.actors_simple)
		{
			testActorSimpleObject.randomRespawn();
		}
	}

	// Token: 0x06002AEF RID: 10991 RVA: 0x001555A8 File Offset: 0x001537A8
	public void update(float pElapsed)
	{
		this.updateKeys();
		this.updateUnityActors(pElapsed);
		this.updateSimpleActors(pElapsed);
		this.updateVisibility(pElapsed);
	}

	// Token: 0x06002AF0 RID: 10992 RVA: 0x001555C8 File Offset: 0x001537C8
	private void updateKeys()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			this.addObjectsSimple(2000);
		}
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			this.addObjectsUnity(2000);
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			this.randomRespawn();
		}
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			this.killAll();
		}
	}

	// Token: 0x06002AF1 RID: 10993 RVA: 0x0015561C File Offset: 0x0015381C
	private void updateUnityActors(float pElapsed)
	{
		for (int i = 0; i < this.actors_unity.Count; i++)
		{
			this.actors_unity[i].update(pElapsed);
		}
	}

	// Token: 0x06002AF2 RID: 10994 RVA: 0x00155654 File Offset: 0x00153854
	private void updateSimpleActors(float pElapsed)
	{
		Parallel.ForEach<TestActorSimpleObject>(this.actors_simple, World.world.parallel_options, delegate(TestActorSimpleObject pActor)
		{
			pActor.update(pElapsed);
		});
	}

	// Token: 0x06002AF3 RID: 10995 RVA: 0x00155690 File Offset: 0x00153890
	private void updateVisibility(float pElapsed)
	{
		this.actors_simple_visible.Clear();
		float tZoneSize = 8f;
		for (int i = 0; i < this.actors_simple.Count; i++)
		{
			TestActorSimpleObject tTestActorSimpleObject = this.actors_simple[i];
			float pos_x = tTestActorSimpleObject.pos_x;
			float tPosY = tTestActorSimpleObject.pos_y;
			int tZoneX = Mathf.FloorToInt(pos_x / tZoneSize);
			int tZoneY = Mathf.FloorToInt(tPosY / tZoneSize);
			TileZone tZone = World.world.zone_calculator.getZone(tZoneX, tZoneY);
			if (tZone != null && tZone.visible)
			{
				this.actors_simple_visible.Add(tTestActorSimpleObject);
			}
		}
	}

	// Token: 0x0400202F RID: 8239
	public static BenchmarkObjects instance;

	// Token: 0x04002030 RID: 8240
	public List<Sprite> some_sprites = new List<Sprite>();

	// Token: 0x04002031 RID: 8241
	public TestActorGameObject prefab_unity_object;

	// Token: 0x04002032 RID: 8242
	private List<TestActorGameObject> actors_unity = new List<TestActorGameObject>();

	// Token: 0x04002033 RID: 8243
	internal List<TestActorSimpleObject> actors_simple = new List<TestActorSimpleObject>();

	// Token: 0x04002034 RID: 8244
	internal List<TestActorSimpleObject> actors_simple_visible = new List<TestActorSimpleObject>();

	// Token: 0x04002035 RID: 8245
	public int total_unity_objects;

	// Token: 0x04002036 RID: 8246
	public int total_simple_objects;

	// Token: 0x04002037 RID: 8247
	public int total_simple_objects_visible;
}
