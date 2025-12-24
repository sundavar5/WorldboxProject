using System;

namespace ai.behaviours
{
	// Token: 0x02000978 RID: 2424
	public class BehaviourTaskKingdomLibrary : AssetLibrary<BehaviourTaskKingdom>
	{
		// Token: 0x060046E7 RID: 18151 RVA: 0x001E1CC8 File Offset: 0x001DFEC8
		public override void init()
		{
			base.init();
			BehaviourTaskKingdom behaviourTaskKingdom = new BehaviourTaskKingdom();
			behaviourTaskKingdom.id = "nothing";
			BehaviourTaskKingdom pAsset = behaviourTaskKingdom;
			this.t = behaviourTaskKingdom;
			this.add(pAsset);
			BehaviourTaskKingdom behaviourTaskKingdom2 = new BehaviourTaskKingdom();
			behaviourTaskKingdom2.id = "wait1";
			pAsset = behaviourTaskKingdom2;
			this.t = behaviourTaskKingdom2;
			this.add(pAsset);
			this.t.addBeh(new KingdomBehRandomWait(0f, 1f));
			BehaviourTaskKingdom behaviourTaskKingdom3 = new BehaviourTaskKingdom();
			behaviourTaskKingdom3.id = "wait_random";
			pAsset = behaviourTaskKingdom3;
			this.t = behaviourTaskKingdom3;
			this.add(pAsset);
			this.t.addBeh(new KingdomBehRandomWait(0f, 5f));
			BehaviourTaskKingdom behaviourTaskKingdom4 = new BehaviourTaskKingdom();
			behaviourTaskKingdom4.id = "do_checks";
			pAsset = behaviourTaskKingdom4;
			this.t = behaviourTaskKingdom4;
			this.add(pAsset);
			this.t.addBeh(new KingdomBehCheckCapital());
			this.t.addBeh(new KingdomBehCheckKing());
			this.t.addBeh(new KingdomBehRandomWait(0f, 1f));
		}

		// Token: 0x060046E8 RID: 18152 RVA: 0x001E1DC9 File Offset: 0x001DFFC9
		public override void editorDiagnosticLocales()
		{
		}
	}
}
