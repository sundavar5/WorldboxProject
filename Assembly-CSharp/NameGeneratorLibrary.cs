using System;
using System.Collections.Generic;
using System.Globalization;

// Token: 0x02000138 RID: 312
[Serializable]
public class NameGeneratorLibrary : AssetLibrary<NameGeneratorAsset>
{
	// Token: 0x0600094E RID: 2382 RVA: 0x00085AC8 File Offset: 0x00083CC8
	public override void init()
	{
		base.init();
		this.addClanNames();
		this.addWarNames();
		this.addAllianceNames();
		this.addKingdomMottos();
		this.addClanMottos();
		this.addAllianceMottos();
		this.addCultureNames();
		this.addCivNames();
		this.addMobNames();
		this.addItemNames();
		this.addWorldNames();
		this.addPlayerNames();
		this.addAnimalCivsNames();
		this.addBookNames();
		this.addItemSpecial();
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x00085B38 File Offset: 0x00083D38
	private void addAnimalCivsNames()
	{
		this.add(new NameGeneratorAsset
		{
			id = "tribe_generic_name"
		});
		this.addPartGroup("haka,ste,hvo,pa,ba,go");
		this.addPartGroup("r,d,f,d,v,tt,");
		this.addPartGroup("p,hb,,");
		this.addPartGroup("aa,e,o,,");
		this.addPartGroup("ich,hko,ov");
		this.addTemplate("Part_group");
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x00085BA0 File Offset: 0x00083DA0
	private void addClanNames()
	{
		this.add(new NameGeneratorAsset
		{
			id = "human_clan"
		});
		this.addOnomastic("012f3f4|0:ka ste vo pa ba go;1:r d f d v t;2:p b;3:a e o;4:ich ko ov");
		this.add(new NameGeneratorAsset
		{
			id = "elf_clan"
		});
		this.addOnomastic("0f0ff1f1ff2/102|0:sy se fe fu au ae oe oa ea ey a e u;1:lva da ba fa va na ma fo ri ru;2:lis ari ri ori ere era eba eta ba ra las los les");
		this.add(new NameGeneratorAsset
		{
			id = "dwarf_clan"
		});
		this.addOnomastic("0_145f8/0_14548|0:dah fam fus ro;1:d dr h mr r rk t th tr;4:a e o;5:b bre bro d dre drede dri r roke;8:dock dok dr kc ken kon kor or rack reck rock");
		this.add(new NameGeneratorAsset
		{
			id = "orc_clan"
		});
		this.addOnomastic("01f_49f/4_01f9f/411f9f|0:ba da da de de ka ke ma me ra za ze zo;1:ba da da dada de de dede ka ke ma me ra za ze zo;4:baha baka daka daka deez deka deka deze doka doke kaka keka maka mako meka moko raka waha zaga zaza zega zoga zoko zomo;9:k z");
		this.addGroup(new string[]
		{
			"folk_clan",
			"0f2f3f42c|0:ash car rad wey ald brad win;2:o e i a;3:r l t n d g v;4:ton ford ley wood ham worth",
			"frankish_clan",
			"0f_12f3V4|0:de du la le;1:beu clau cour loix mont val vil;2:a e i o u;3:g l n r t v;4:bois clair court fleur font mont ville",
			"germanic_clan",
			"0f_1f2f3V45C6V|0:aus gud van von zu;1:arn aus eich hoh met neu;2:a e o u;3:b d l n r t;4:b b h k m st w;5:a a e e ei o oi u;6:f ff hl hn ld n nn rf rg",
			"iberian_clan",
			"0f_12f3Vf4C56|0:de de del dela la pe ra;1:al ar bal cal mar val zar;2:a e i o u;3:d g l n r v z;4:a e i i la o o;6:a o o o;5:d ll ll n r r z",
			"monolux_clan",
			"9f_V00f1c3Vf6|0:bel cl col con da de do du es fl gr he la le mar mon ne pel ra re ro ru st str vi we ze;1:a e i o u;3:ch ng sch;6:ange buerg cort don dorph fon gen in lux mer mont sur ton vil weila;9:d' de",
			"nordic_clan",
			"0125f789|0:Th Er Ol Har Sv Bj S G L;1:o i a a e o i u ei;2:r k f ld n rn g nn f;5:ar in al an en;7:s s ss d b str l;8:o e o otti e o u;9:n n n r rg m nd",
			"poly_clan",
			"06Vfff1f3f6Vfff3|0:ha hu hu ka koa ku la ma ngi nu nu ra ta;1:ho ka ka kai ku lu ma ma pua wa;3:ha hi ka la la loa na ni nui ra ra ta;6:'",
			"pomeranian_clan",
			"012f3f|0:bor gr kr pom stet sw szcz wol zem;1:an ar en er in ir ol on or un ur;2:ch cz k s sz w z;3:ak cki czyk ek icz owicz ski wicz",
			"posh_clan",
			"01f2f3|0:ard arr ash bar berk castor corlyn drake earl gray greth grey hall hill horn lyth mar mar rath star tar tarrow thorne varren wess weybrook white wynch;1:a e i o u;2:r l t s n;3:bourne cliffe croft dale down ford fort grove hal hall heath hill march mont moor reach ridge rock ryen stead ster stone tell thorne ton torn vale wood",
			"rome_clan",
			"0f_1f2Cf3V45|0:da del della di gi;1:bon bru cae car fal giu mal mar tor vin;2:a e i o u;3:g l n r s t;4:a a e e i o o;5:llo ne ni ni no nte r tti vi",
			"rus_clan",
			"01Cf2Vf3Cf7V5M6F|2:g k k l m n r r r t;3:a e i o u;4:g l m n r s t v z;5:enko ev ich in ivich ky ov ovich yev;6:aya eva evna ichna ina ivna ova;7:d g k r s sk v;0:ab ag al al an b br d gr ig iv k m n p s sh sm v vl yu;1:a e e e ev ex i i i o o",
			"slavic_clan",
			"01v2f3f4M5F|0:ko mly no li ku be kra pie maz mys;1:wie b ch w n cz sz li;2:e i y;3:cz sz rz;4:ski cki dzki ny ty jek tek rek;5:ska cka dzka na ta jka tka rka",
			"nomad_clan",
			"0125f|0:ab abd al asg asgh az bad bas bash bil bin daw far fas gad gh hab had hus ibn jab jaw jaz kad raf;1:a i u;2:b d f h k l m n r s t z;5:an in un",
			"sino_clan",
			"016V4Cf|0:ch g h l l m w w x y zh zh zh;1:ang ang ang ao ei en i ia in iu ou u u uo;4:a e i o u;6:b b ch d f g g h k k k m m m n n sh",
			"siam_clan",
			"012378|0:ad b b b b ch ch ch ch d j k k kr kr m n p pr r r r r r s sh sr sr t th th th ud v v w w y;1:a ai e i o oo u;2:ch d k l m n ng p r t;3:a e ee i o u;7:ch ch k k l m n p ph r r s s s sr t w;8:aew ai ak at i in oen on ong",
			"vishna_clan",
			"012Cf|0:abhi aho andhra anga anu asma ava ba bha bho bri bu ce cha che cho chu da de di do fu ga go gu ha he ho ikshva iksva ja ka ke kha khe khme ki ko ksha ku la li lo ma mi mu na ne oi pa po pra pu ra re ro sa se sha shi shu si so sri su ta tha to tri utpa va vi ya;1:b cchav d damb darbh dasam dav dir dnapor duk dura dyot ffn g gar ghel gvansh h hadaval hadrath haman han har harat hey hisapal hurip ilendr indhav ink jahnat janat japah japat jayanagar jjik jvapal k katiy ket kotta ky l labh labhr lachur lakanagar lav lingg ll llabh llal llav lon luky lw m mar marup mas matat mava mbuw mm mp n nagar nchal nd ndhar ndy nes ng ngar ngg nggal ngha ngka nind niw njav nl nmal nn nna nt nv p pal pt pur pur pur r rabha rasen rashiv rat rataraj rath rayang ripunjay rjaratr rkh rkot rn rtadi ruma ryank s sal sh shiv shm shnu shtrakut shunag sunur t ta tah tar tava tiy tr ts tsy tu tyur ud urav ury v vagang vd vijay y yak ysal;2:a a a a a a aj ak al al am an an an an an an an an an and ap ar ar arh as at e e e eddy en en er i i i i i i i indh ir it o och om or u u u u u uh uj um ur us",
			"nihon_clan",
			"02135/0245/02134/0254|0:h k m n s t y;1:h k m n s t;2:ai a e i o u;3:a e i o u;4:ha ka mo mu shi chi da ya;5:gu be ki ma mo ra to ta wa ya"
		});
		this.addGroup(new string[]
		{
			"vishna_family",
			"01C3Vf4V6|0:ahlu apte ba ba ba ch cho da ga iye jo ka ka kha ku ma na pa ra sa sha si va;1:a e i o u;2:b ch d g h j k l m n p r s t v y;3:l ng;4:dar kar mar ng pal sh;6:a ar as e el i ingh ir o u u"
		});
	}

	// Token: 0x06000951 RID: 2385 RVA: 0x00085D88 File Offset: 0x00083F88
	private void addWarNames()
	{
		this.add(new NameGeneratorAsset
		{
			id = "war_spite",
			use_dictionary = true,
			replacer_kingdom = new NameGeneratorReplacerKingdom(NameGeneratorReplacers.replaceKingdom)
		});
		this.addDictPart("kingdom_name", "$kingdom$");
		this.addDictPart(" ", " ");
		this.addDictPart("of", "of");
		this.addDictPart("anger", "Anger,Bloodshed,Insanity,Screaming,Yelling,Brawl,Rage,Fury,Wrath,Grudge,Hate");
		this.addDictPart("bloody", "Bloody,Red,Blue,Green,Screaming,Angry,Total,Worldwide,Insane");
		this.addDictPart("hatred", "Hatred,Dislike,Grudge,Detest,Despise,Death,Destruction,Oblivion,Silence,Obliteration");
		this.addTemplate("bloody, ,hatred");
		this.addTemplate("anger, ,of, ,kingdom_name");
		this.add(new NameGeneratorAsset
		{
			id = "war_rebellion",
			use_dictionary = true,
			replacer_kingdom = new NameGeneratorReplacerKingdom(NameGeneratorReplacers.replaceKingdom)
		});
		this.addDictPart("kingdom_name", "$kingdom$");
		this.addDictPart(" ", " ");
		this.addDictPart("of", "of");
		this.addDictPart("rebellion", "Rebellion,Uprising,Unrest,Rising,Insurgency,Trouble,Freedom");
		this.addDictPart("color", "Red,Yellow,Bloody,Dark,Nice,Pink,Brutal,Cruel,Ruby,Angry,Annoyed,Furious,Mad,Wild,Enraged,Violent");
		this.addTemplate("rebellion, ,of, ,kingdom_name");
		this.addTemplate("color, ,rebellion");
		this.add(new NameGeneratorAsset
		{
			id = "war_inspire",
			use_dictionary = true,
			replacer_kingdom = new NameGeneratorReplacerKingdom(NameGeneratorReplacers.replaceKingdom)
		});
		this.addDictPart("kingdom_name", "$kingdom$");
		this.addDictPart(" ", " ");
		this.addDictPart("of", "of");
		this.addDictPart("great", "Great,Forced,Amazing,Inspired,Shocking,Horrific,Serious");
		this.addDictPart("idea", "Idea,Thought,Plan,Goal");
		this.addTemplate("great, ,idea, ,of, ,kingdom_name");
		this.addTemplate("idea, ,of, ,kingdom_name");
		this.add(new NameGeneratorAsset
		{
			id = "war_whisper",
			use_dictionary = true,
			replacer_kingdom = new NameGeneratorReplacerKingdom(NameGeneratorReplacers.replaceKingdom)
		});
		this.addDictPart(" ", " ");
		this.addDictPart("kingdom_name", "$kingdom$");
		this.addDictPart("'s", "'s");
		this.addDictPart("of", "of");
		this.addDictPart("evil", "Evil,Horrible,Wicked,Bad,Corrupt,Forced,Wrong,Mad,Crazy");
		this.addDictPart("unprovoked", "Unprovoked,Unjustified,Groundless,Unfair,Rude");
		this.addDictPart("war", "War,Fight,Conquest,Battle");
		this.addTemplate("evil, ,war, ,of, ,kingdom_name");
		this.addTemplate("war, ,of, ,kingdom_name");
		this.addTemplate("unprovoked, ,war");
		this.addTemplate("evil, ,war");
		this.add(new NameGeneratorAsset
		{
			id = "war_conquest",
			use_dictionary = true,
			replacer_kingdom = new NameGeneratorReplacerKingdom(NameGeneratorReplacers.replaceKingdom)
		});
		this.addDictPart(" ", " ");
		this.addDictPart("kingdom_name", "$kingdom$");
		this.addDictPart("'s", "'s");
		this.addDictPart("of", "of");
		this.addDictPart("great", "Great,Big,Huge,Red,Bloody,Bad,Good,Horrible,Sudden");
		this.addDictPart("war", "Conquest,War,Clash");
		this.addDictPart("justice", "Justice,Iron,Dreams,Storm,Kings,Terror,Broken Bones,Broken Dreams,Broken Future,Broken Promises,Broken Words,Fools,Angels,Freedom,Steel");
		this.addTemplate("great, ,war, ,of, ,kingdom_name");
		this.addTemplate("war, ,of, ,justice");
		this.addTemplate("great, ,war");
	}

	// Token: 0x06000952 RID: 2386 RVA: 0x000860F4 File Offset: 0x000842F4
	private void addAllianceNames()
	{
		this.add(new NameGeneratorAsset
		{
			id = "alliance_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Great,Steel,Holy,Eternal,Fancy,Strong,Big,Deadly,Mighty,Powerful,Serious,Nice,Supreme,Divine,Awesome,Epic,Brave,True,Wise,Ancient,Wild,Old,New,Golden");
		this.addDictPart(" ", " ");
		this.addDictPart("of", " of ");
		this.addDictPart("and", " and ");
		this.addDictPart("our", "Our");
		this.addDictPart("people", "People,Beings,Stars,Titans,Keepers,Eyes,Grains,Stares,Friends,Minds,Bellies,Hearts,Heads,Bodies,Lives,Folks,Eggs,Souls,Voices,Spirits,Guts,Noses,Skulls,Hopes,Roots,Flames,Bones,Dreams,Wills");
		this.addDictPart("second", "Alliance,Unity,Friendship,Union,League,Treaty,Coalition,Power,Accord,Pact,Club,Shield,Sword,Axe,Bond,Conclave,Order,Legion,Assembly");
		this.addDictPart("element", "Fire,Water,Earth,Air,Light,Darkness,Void,Thunder,Ice");
		this.addDictPart("creature", "Dragon,Turtle,Crab,Bear");
		this.addTemplate("first, ,second");
		this.addTemplate("first, ,people");
		this.addTemplate("second,of,first");
		this.addTemplate("second,of,people");
		this.addTemplate("people,and,people");
		this.addTemplate("our, ,second");
		this.addTemplate("people,of,element");
		this.addTemplate("first, ,people,of,element");
		this.addTemplate("creature, ,second");
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x00086214 File Offset: 0x00084414
	private void addItemSpecial()
	{
		this.add(new NameGeneratorAsset
		{
			id = "reforged_item"
		});
		this.addPartGroup("sh,ste");
		this.addPartGroup("a,e,o,y,oo");
		this.addPartGroup("sh,ste,la");
		this.addPartGroup("b,m,l");
		this.addPartGroup("a,e,o,y,oo");
		this.addPartGroup("a,e,o,y,oo");
		this.addTemplate("Part_group");
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x00086288 File Offset: 0x00084488
	private void addMobNames()
	{
		this.add(new NameGeneratorAsset
		{
			id = "sheep_name"
		});
		this.addOnomastic("012|0:b;1:a aa aaa o oo ooo y yy yyy;2:h");
		this.add(new NameGeneratorAsset
		{
			id = "penguin_name"
		});
		this.addOnomastic("0123|0:h w d t;1:u uu uuu a aa aaa;2:g gg d r z;3:o oo ooo a aa aaa");
		this.add(new NameGeneratorAsset
		{
			id = "turtle_name"
		});
		this.addOnomastic("012|0:l t g z;1:u uu a aa o oo;2:k kk;3:o oo ooo a aa aaa");
		this.add(new NameGeneratorAsset
		{
			id = "wolf_name"
		});
		this.addOnomastic("012|0:w;1:o oo oo a aa aaa;2:f;3:o oo ooo a aa aaa");
		this.add(new NameGeneratorAsset
		{
			id = "greg_name"
		});
		this.addOnomastic("0129|0:g;1:r r r r r r r r r r rr;2:e e e e e e e o;9:g gaz gef ges gim gis gonk gos");
		this.add(new NameGeneratorAsset
		{
			id = "chicken_name"
		});
		this.addOnomastic("o/oo/o_o_o/o_o|0:k g d b ka ko go keke koko kh kh;1:o a a");
		this.add(new NameGeneratorAsset
		{
			id = "alien_name"
		});
		this.addOnomastic("o/0_0|0:ze zo zo ze zog zag xag xeg xo xe;1:o oo oo a aa a y u;2:r d z ze tze tza n");
		this.add(new NameGeneratorAsset
		{
			id = "ufo_name"
		});
		this.addOnomastic("0-####f|0:tk xx yt z rt pp kl qr lol q rebr");
		this.add(new NameGeneratorAsset
		{
			id = "cold_one_name"
		});
		this.addOnomastic("o|0:ice icy co colo;1:m n mem non;2:e a o y;3:n c k brr b");
		this.add(new NameGeneratorAsset
		{
			id = "bug_name"
		});
		this.addOnomastic("o|0:e ee eee eeee;1:w ww www");
		this.add(new NameGeneratorAsset
		{
			id = "ant_name"
		});
		this.addOnomastic("o1|0:an ant ano;1:n o on");
		this.add(new NameGeneratorAsset
		{
			id = "demon_name"
		});
		this.addOnomastic("o/021|0:a'n e'r o'r o'h an am akan al bal fur d dev gar kra val;1:a i e;2:r vel v s on mon al");
		this.add(new NameGeneratorAsset
		{
			id = "angle_name"
		});
		this.addOnomastic("o/021|0:a'n e'r o'r o'h an am akan al bal fur d dev gar kra val;1:a i e;2:r vel v s on mon al");
		this.add(new NameGeneratorAsset
		{
			id = "fairy_name"
		});
		this.addOnomastic("o/01/010|0:ar er or ir;1:i a e;2:a e");
		this.add(new NameGeneratorAsset
		{
			id = "crab_name"
		});
		this.addOnomastic("o/0102/210|0:c cr;1:a aa aaa o o o;2:b be ba bo");
		this.add(new NameGeneratorAsset
		{
			id = "cow_name",
			parts = new string[]
			{
				"moo oom mi"
			}
		});
		this.addOnomastic("o/9_o/o_9/o_o|0:m l d b mad daa moo;1:i o a y;2:d m;9:moo oom mi");
		this.add(new NameGeneratorAsset
		{
			id = "default_name",
			vowels = new string[]
			{
				"a",
				"e",
				"i",
				"o",
				"u",
				"y",
				"oo"
			}
		});
		this.addOnomastic("031f2|0:pl p s l d b;1:mp rp rt b;2:kin tin le ee;3:a e i o u y oo");
		this.addPartGroup("Pl,P,S,L,D,B");
		this.addPartGroup2("mp,rp,rt,b,");
		this.addPartGroup3("kin,tin,le,ee");
		this.addTemplate("part_group,vowel,part_group2,part_group3");
		this.add(new NameGeneratorAsset
		{
			id = "evil_mage_name"
		});
		this.addOnomastic("9fo8cf/9f012/3f45/9f6_7|0:ru ur sp bl re bu;1:s t os ut ux oz az ez;2:t p k r ok at ap up op opo;3:o a e i;4:ra re ri ro;5:um us ux or;6:rust spore blight mold;7:ior ius mancer;9:a' o' ar' er';8:a o ata oto");
		this.add(new NameGeneratorAsset
		{
			id = "white_mage_name"
		});
		this.addOnomastic("9fo8cf/9f012/3f45|0:lu um sp gl my ag;1:s t os ut ux oz az ez;2:c p k r ok at ap up op opo;3:a e i o;4:ra re ri ro;5:um us ux or;7:ior ius mancer;9:a' e' ar' er';8:a o ata oto");
		this.add(new NameGeneratorAsset
		{
			id = "rhino_name"
		});
		this.addOnomastic("o/01_24/012|0:r g gr d t rh thr gro;1:o a oo aa u ar;2:k g rk rg;3:o a e;4:kh gr rk rh");
		this.add(new NameGeneratorAsset
		{
			id = "monkey_name"
		});
		this.addOnomastic("o/0123/010_01/012-012|0:m n k ch j p t;1:a e o i u;2:mp nk t ch;3:o ee ai oo");
		this.add(new NameGeneratorAsset
		{
			id = "armadillo_name"
		});
		this.addOnomastic("o/75/551c51c/9_o/9_75/77f7f9f|0:d t r m b;1:a i o u;2:l r d m;3:d t r m b;4:o a i u;5:sh l rm d;6:y ie oo a;7:lo do rmo;9:ar mor so lo mo dodo didi midi");
		this.add(new NameGeneratorAsset
		{
			id = "alpaca_name"
		});
		this.addOnomastic("o/012/3456/09f_02f|0:paca lama cito lomo poco pica lima lala lodo dima domo didi do pi ca li ma de do pa po ce;1:a u o i;2:c k f l m;3:c p l m h;4:o a u i;5:f m l p fof pop;6:y ie oo a;8:a o u i;9:sof flid fluf sofy hum cud wool hop");
		this.add(new NameGeneratorAsset
		{
			id = "capybara_name"
		});
		this.addOnomastic("o/012/345/678/43_3210|0:l m n s v z cap;1:a e i o u;2:r l m n v;3:ae ai oo ea ie;4:l m n r s b;5:a e i o u ara;6:th sh l m n;7:oo ia ea;8:n m l");
		this.add(new NameGeneratorAsset
		{
			id = "goat_name"
		});
		this.addOnomastic("o/012/3452f/6783f/67_o|0:kr gr thr dr br;1:a o u;2:nk rk x zz;3:ax ox ux;4:t k r d;5:o a u;6:kh gh th rr;7:or ar ur;8:x k r");
		this.add(new NameGeneratorAsset
		{
			id = "scorpion_name"
		});
		this.addOnomastic("9f9f_f99f_f9f|9:zek tek zkek zak qzo zok zip zez tak tik tek");
		this.add(new NameGeneratorAsset
		{
			id = "garlic_name"
		});
		this.addOnomastic("o/012/133f45/432/872|0:g l r b cl;1:a i o u;2:rl lc ck rv;3:ov al ic;4:k n m r;5:o a i;7:io eo;8:n k s");
		this.add(new NameGeneratorAsset
		{
			id = "necromancer_name"
		});
		this.addOnomastic("0f_0128f/3f_f245/6f786f/9_01f|0:thur kral nox;1:ae uo ei a;2:th x rn zt;3:nex lich zor;4:a i o u;5:n r s th;6:um or us;7:vex mur thrax;8:or ius;9:zar grim vox");
		this.add(new NameGeneratorAsset
		{
			id = "flower_name"
		});
		this.addOnomastic("o/012/7f_345/6f78|0:bl fl r p l;1:o a i e u;2:ss ll m n;3:pet ros blo;4:a o e i;5:l m n s;6:y ia um;7:bell lia ium;8:a e");
		this.add(new NameGeneratorAsset
		{
			id = "bandit_name"
		});
		this.addOnomastic("0M1F_23M4F/23M4F|0:don mr herr baron han;1:dona ms frau baroness hatun;2:nik nok nuk kin kon kun ni kan;3:on in un en;4:ona ina una na");
		this.add(new NameGeneratorAsset
		{
			id = "buffalo_name"
		});
		this.addOnomastic("o/0123/010_212/030|0:b d r g m;1:o a u e i;2:ff ll bb rr;3:oo aa uu");
		this.add(new NameGeneratorAsset
		{
			id = "fox_name"
		});
		this.addOnomastic("o/013|0:f ph v;1:o i u a;2:x xx xz;3:y in en");
		this.add(new NameGeneratorAsset
		{
			id = "hyena_name"
		});
		this.addOnomastic("0123/014_054/2354f/4_4|0:h y r l g;1:a e i o u;2:n r k t z;3:ah ek ik ak;4:gig gug dog geg kek kak kuk;5:aga");
		this.addOnomastic("01cf3vf6|0:bla chak dak gra hak kra lok nar rok ska tok zan vak zhak trak snok;1:a e o u i;3:k r l n t g z;6:aak raak fang klaa draa skaag naal raa kaz graak snaak rok daak");
		this.add(new NameGeneratorAsset
		{
			id = "hyena_city"
		});
		this.addOnomastic("01cf3vf6|0:bak kra gra hak jhak kaf klok vok gro skarn tok trak zan snok dra;1:a e o u i;3:k r n t l g;6:pit veld kraal draal fang prow cresh den rift klaaw rok fang skaag kop bay ridge");
		this.add(new NameGeneratorAsset
		{
			id = "crocodile_name"
		});
		this.addOnomastic("o/01_01/01_23/0f12v/34|0:g k r z gr kr;1:a o u;2:k zz rk kh rr kk;3:a o e;4:r z");
		this.add(new NameGeneratorAsset
		{
			id = "snake_name"
		});
		this.addOnomastic("o/010/o_01/01-01|0:s z ss zz sh sl sn sz;1:a e o u i;2:k x t h s z z;3:th zh ss ts ks");
		this.addOnomastic("01cf103vf|0:ss sz sl ss zh sh zz th;1:a e o u i;2:k x t h z r s;3:th zh ss ts ks zth kth ssh xth shh");
		this.add(new NameGeneratorAsset
		{
			id = "snake_city"
		});
		this.addOnomastic("0,01cf2vf6|0:ss zz zh sl sn sz;1:a e o u i;2:k x t h r l s z;6:ssa thra xiss kara ssith thar liss zath zhira kraza ssyl");
		this.addOnomastic("01cf3vf6|0:ash hiss kas liss nas ras shiss sith thass sliss zash ziss vass;1:a e o u i;3:s sh th l ss z;6:ssath thiss rassith zhass hissath lissra shaza zhaal sraal vashra");
		this.add(new NameGeneratorAsset
		{
			id = "frog_name"
		});
		this.addOnomastic("0123/456/7893|0:ri ry;1:b bb;2:it yt ot;3:er ing;4:j w y;5:imp mp ump;6:y er ing;7:kr cr;8:oa ee oe ea;9:q k c");
		this.add(new NameGeneratorAsset
		{
			id = "bioblob_name"
		});
		this.addOnomastic("01/230/0f1v2/45|0:b bl ooz gl;1:o oo u;2:z zz k q lk;3:o e y oo;4:b z r;5:eye ez ooz");
		this.add(new NameGeneratorAsset
		{
			id = "assimilator_name"
		});
		this.addOnomastic("0-###f#f#fu|0:tk xx yt z rt pp kl qr er");
		this.add(new NameGeneratorAsset
		{
			id = "lemon_name"
		});
		this.addOnomastic("o/41032/2100/010|0:l z c t li lo lo la;1:e i o a u;2:m n r t;3:o a i ar;4:x n d");
		this.add(new NameGeneratorAsset
		{
			id = "candy_name"
		});
		this.addOnomastic("o/01234/012/21_012|0:c s t g;1:a o u e i;2:n d f l;3:y o i;4:x r s");
		this.add(new NameGeneratorAsset
		{
			id = "crystal_sword_name"
		});
		this.addOnomastic("o/012/3f45/6_7|0:cr gl sp sh;1:y i a e;2:st l r z;3:t l m;4:al el il;5:ite ine yte;6:reef core shard;7:ium ite");
		this.add(new NameGeneratorAsset
		{
			id = "crystal_name"
		});
		this.addOnomastic("o/012/3f45/6_7|0:cr gl sp sh;1:y i a e;2:st l r z;3:t l m;4:al el il;5:ite ine yte;6:reef core shard cora shor;7:ium ite");
		this.add(new NameGeneratorAsset
		{
			id = "fire_skull_name"
		});
		this.addOnomastic("0124/21_5|0:f fl bl s th sk sks;1:i o a u e;2:r rk l t ch;3:oo oo ia ir ur ar aa;4:k ks c t;5:gor zor bor mor");
		this.add(new NameGeneratorAsset
		{
			id = "acid_blob_name"
		});
		this.addOnomastic("o/012/03|0:bl gl dr sl;1:a e er i o oo u y;2:b p m;3:er ir y i ster");
		this.add(new NameGeneratorAsset
		{
			id = "jumpy_skull_name"
		});
		this.addOnomastic("012/0f12|0:clack bone cr skull bony;1:e i o u le y er s;2:t d ck p n");
		this.add(new NameGeneratorAsset
		{
			id = "fire_elemental_name"
		});
		this.addOnomastic("o/012/3f45/6_7|0:fl bl cr fr;1:a e i o;2:r z m;3:e a o;4:mb rn bl zz;5:or ar er;6:ash ember blaze inferno;7:kin tor nix");
		this.add(new NameGeneratorAsset
		{
			id = "lil_pumpkin_name"
		});
		this.addOnomastic("o|0:m n x;1:a aa o oo y;2:x d z;3:i o a u;4:m n x z");
		this.add(new NameGeneratorAsset
		{
			id = "rat_name"
		});
		this.addOnomastic("o2c2c2c2c|0:ma ar a e i b c;1:v m n k rk x rt;2:o s a y e i;3:m n a d");
		this.add(new NameGeneratorAsset
		{
			id = "raccoon_name"
		});
		this.addOnomastic("o2c2c2c2c|0:ma ar a e i b c;1:v m n k rk x rt;2:o s a y e i;3:m n a d");
		this.add(new NameGeneratorAsset
		{
			id = "cat_name"
		});
		this.addOnomastic("o/o_o|0:m;1:e ee ee;2:o oo oo;3:w ww ww");
		this.add(new NameGeneratorAsset
		{
			id = "rabbit_name"
		});
		this.addOnomastic("o/o_o|0:j dj;1:a e o ae oe;2:t p m;3:o a y eke");
		this.add(new NameGeneratorAsset
		{
			id = "piranha_name"
		});
		this.addOnomastic("o/o_o|0:c;1:h;2:o a oe ae;3:p d b");
		this.add(new NameGeneratorAsset
		{
			id = "snowman_name"
		});
		this.addOnomastic("o/o_o|0:s z ss zz;1:o a o;2:s zz ss zzz;3:y e o");
		this.add(new NameGeneratorAsset
		{
			id = "bear_name"
		});
		this.addOnomastic("o/0124|0:h;1:o a e;2:n;3:y e o;4:a");
		this.add(new NameGeneratorAsset
		{
			id = "skeleton_name"
		});
		this.addOnomastic("5f012334|0:b;1:o a e;2:n;3:y e o;4:s;5:a o");
		this.add(new NameGeneratorAsset
		{
			id = "living_house_name"
		});
		this.addOnomastic("01234/012/012mf|0:h;1:o a e;2:m;3:i y e o;4:y e o");
		this.add(new NameGeneratorAsset
		{
			id = "living_plant_name"
		});
		this.addOnomastic("012345/0125/012mf|0:pl;1:o a e;2:n;3:t;4:i y e o;5:y e o");
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x00086B14 File Offset: 0x00084D14
	private void addWorldNames()
	{
		this.add(new NameGeneratorAsset
		{
			id = "world_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Forgotten,New,Red,Blue,Never,Green,Mirage,Ever,Hollow,Infinite,Empty,Ghost,Perfected,God's,Hidden,Stolen,Broken,Nightmare,Secret,Sacred,Cruel,Phantom,Free,Dragon,Demon,Thunder,Silent,Old,Ancient,Eternity,Flat,Dream,Greg's,Nefarious,Plain,Bygone,Medieval,Archaic,Inner,Crying,Bad");
		this.addDictPart(" ", " ");
		this.addDictPart("of", " of ");
		this.addDictPart("and", " and ");
		this.addDictPart("second", "Isles,Lands,Land,Realm,Box,Skulls,Memory,Earth,Sanctum,World,Planet,Archipelago,Territories");
		this.addDictPart("ofWords", "Souls,Hope,Sadness,Life,Sun,Moon,God,Blood,Dragons,Man,Greatness,Misery,Happiness,Death,Clouds,Rain,War,Despair,Mystery,Wizardry,Magic,Magnificence,Chaos,Madness,Sheep,Elders,Soup,Lemons,Coffee,Place");
		this.addTemplate("first, ,second");
		this.addTemplate("second,of,ofWords");
		this.addTemplate("second,and,ofWords");
		this.addTemplate("ofWords,and,ofWords");
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00086BCB File Offset: 0x00084DCB
	private void addPlayerNames()
	{
		this.add(new NameGeneratorAsset
		{
			id = "player_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Umom,Greg,Bre,The Architect,The One,The Creator,The Destroyer,Art Vandelay,The Great,The Wise,The Mighty,The Nope,Architect of the Evil,The Old,The Young,The Silent,The Loud,The Quiet,The Noisy,The Shy,The Fearful,The Scary,The Happy,The Angry,The Calm,The Furious,The Mad,The Daydreamer,The Lost,The Meme Lord,The Clumsy,The Forgetful,The Hungry,The Eternal,The Omnipotent,The Forgotten,The Shadow,The Light,The Dark,The Dreamer,The Keeper,The Watchful,The Insane,The Sane,The Crazy,The Normal,The Abnormal,The Weird,The Strange,The Odd,The Bad,The Beautiful,The Handsome,The Pretty,The Smart,The Arm,The Pointy,The Potato,The Awkward,The Overthinker,The Underthinker,The Procrastinator");
		this.addTemplate("first");
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x00086C08 File Offset: 0x00084E08
	private void addCivNames()
	{
		this.add(new NameGeneratorAsset
		{
			id = "human_unit",
			vowels = new string[]
			{
				"a",
				"e",
				"i",
				"o",
				"u",
				"y"
			},
			consonants = new string[]
			{
				"b",
				"c",
				"d",
				"g",
				"h",
				"ph",
				"ch",
				"k",
				"l",
				"m",
				"n",
				"p",
				"r",
				"s",
				"t",
				"v",
				"w",
				"sh"
			}
		});
		this.addOnomastic("010f1f0f1f1c0v|0:b b d g k n r s;1:a o");
		this.addOnomastic("2f3232f3Vf2Cf3Vf|2:a e i o u y;3:b c ch d g h k l m n p ph r s sh t v w");
		this.addTemplate("Letters#3-8");
		this.addGroup(new string[]
		{
			"folk_unit",
			"0Mf,15CfDff9Mf8F|0:b d f g h h j k l n t w w;1:al al an ar ed ed ed el en en eo ern il il in on os ydn yn;9:by den gar ley lin ric son ton win;5:a e i o u;8:brya da dena gara leya lina na ra rica sana ta wina",
			"frankish_unit",
			"0127M8F|0:bl br ch cl fr gr j kr l m n p s t v;1:a e i o u;2:ch ld lf lm nd rd rn rt;7:ald an an ard el ent er ert ich ied in on;8:a e elle ette ia ild ine ine und",
			"germanic_unit",
			"01C2f5Cf7V9F8M|0:a ae ba be bru d ea eo fr ge go gu hi i si t th th th w w;1:a ae e ea eo o u y;2:d d d d dal g g g l ld ld ld lf lf lfr n n ng r r rn th;9:aed eith id ifu ild ind inda inna ith ith ud un und urg urh ynn;8:ald ar ar ard ed elm ert ik im in ith olf ulf ulf und;7:b b b fl fr fr fr g g g gr h h h h hr kn l l m m r r sk sw sw tr w w w w w w;5:ald ald and ar ar ard elm eod er ert ic ich id id im in is ulf und",
			"iberian_unit",
			"0bf1Cf3f5M7vM6F|0:al ant bea ben cle din dom el fel fer gar isa joa jua lor mar mig pal ped ric rodr sal sol val xav ysa;1:a ae e i ia io o u ua;3:bel ben cel dri fan gon lmo mar per ric sol ter val vid;5:a al ar el im ino io io is o o o o o o o or uel;6:a ela ela ena ia ina ina ira isa ita la ola ona;7:b m x",
			"monolux_unit",
			"01CM2CF5Cf8F9M|0:al bea ber cle cor dal dolf el fe fer gar ger har hel jon kar lau lis lor mal mar mel nel nor ol osk pe re san syl te val wim zin;1:a e i o u;2:a e i o u y;5:a ael an ar el er et ia ien ik ine is on or us wen yn;8:a el ela et ia ine ine ira is ora wen yn;9:ald an ar en er ik mond olf on or rik us win",
			"nordic_unit",
			"01f2Cf4f5CM6CF|0:bj br dr ei fe gr gu hr ing jar r sk st th ul v var ym;1:ad ald alf arn en ik im in ir orn ulf;2:a ae e ei i io o u y yr;4:bj br fr gr hr jar sk st st th th ul v;5:andr ar ard ein fr idr ik ildr im or orn ulf ylf yr yrn;6:a hild ida inna is la ra rin thrun yla yn yrn",
			"poly_unit",
			"042f6f8F9M|0:hau kai kea lua maka moa niu pua wai;2:';4:a e i o u;6:ha ka la ma na pa ra ta wa;8:a lani lei wahine;9:kane koa nui o",
			"pomeranian_unit",
			"01Cf2V1Cf3FC4MC|0:bor gri kasz kro mar mat pom sop stas stett swa tom wol zem;1:a e i o u;2:k l m n ph r t;3:a aczka anka eczka enka entz ia ilda ina inka oczka;4:an aus av aw ek iek im ir iu o or us y",
			"posh_unit",
			"01C23F4M|0:aeth al bea cha dar ead ed el fran geof har is jas;1:a e e e i o u;2:b c d f g h l m n p r s t;3:alda anna da elle ette ilda ina;4:ar bert der mond red ry son ward win",
			"rome_unit",
			"01Vf3V3v8M9F|0:a ae au ba ce clau co co do equ fla gai gla ju le lu ma ma octa pe qui sa tri tu ve vi;1:c ca co d di do gi gri ki la li li ll lo ma me mi mi mit na ne no nt nt pa pp ra rc ri rne ro sa se ta te ti to v v va xi;3:c d l n r s t;8:ar er es i ianus ius ius or or us us;9:a a ella ia ia ina is ita",
			"rus_unit",
			"022Cf5Vf8F9M3v|0:al d iv n ol p s vl;2:a a a e e e e i;3:ch d g k k n n rg tr;9:an ich in ov y;8:a eva ia ina ova;5:k l m n r t v",
			"slavic_unit",
			"0M1F4Cf5M6M8F9FV|0:bog bol bor borys bron dam dom drag fil i jac jar kaz mar mil miros oleg rad slav stan toma val ves vlad vol woj wojt zbign zbygn;1:an ce dan ela ew ire jul le lu mag mi mi na ni oks ola rad sla svet va ve vi ya zo;9:ka na na na na na na nia nka nka ra ra ra sa ta va va wa wka;8:a e e i i i i le li mi o o sła y y;4:a e i o u y;6:an an av aw ek ek ek ek iew ik ir o or or;5:b cz cz d g i isl k l l l m m m n r r r st sz sz sł",
			"nomad_unit",
			"01234f5f|0:a ab ah am an as ba fa ha ka ma na ra sa ta ya za;1:a i u;2:b d f h k l m n r s t z;3:a i u;4:b d f h k l m n r s t z;5:al il ul",
			"sino_unit",
			"014Cfl/014Cf6f8C|0:ch g h l l m w w x y zh zh zh;1:ang ang ang ao ei en i ia in iu ou u u uo;4:a e i o u;6:b b ch d f g h k k m m n sh;8:a a ai an ei ei i o ua",
			"siam_unit",
			"04/42/032/032|0:b ch d f g k m p p s t t th v w;2:g h k m m n n n ng p r rn t t w;3:a a ae ai ai ao au e e ea ee ey i i ia o oo oy u ua;4:ae ai ai ao au ay ea ee ey ia oo oy ua",
			"vishna_unit",
			"0dVff1V5C2|0:an ar as ba de el esh ha in je ka ku ma na pa ra sa vi;1:dra j mar nt p sh t;2:b ch d g h j k l m n p r s t v y;5:a e i o u",
			"nihon_unit",
			"0f10f1C2M3F|0:b f h k m n r s t y;1:a e i o u;2:keo deo ki ro ji to shi su ta ya ke chi yu yo;3:ka ko mi na ri ra sa yo"
		});
		this.clone("human_city", "human_unit");
		this.addOnomastic("010f1f0f1f1c0v|0:b b d g k n r s;1:a o");
		this.addOnomastic("2f3232f3Vf2Cf3Vf|2:a e i o u y;3:b c ch d g h k l m n p ph r s sh t v w");
		this.clone("human_religion", "human_unit");
		this.addOnomastic("010f1f0f1f1c0v|0:b b d g k n r s;1:a o");
		this.addOnomastic("2f3232f3Vf2Cf3Vf|2:a e i o u y;3:b c ch d g h k l m n p ph r s sh t v w");
		this.clone("human_language", "human_unit");
		this.addOnomastic("010f1f0f1f1c0v|0:b b d g k n r s;1:a o");
		this.addOnomastic("2f3232f3Vf2Cf3Vf|2:a e i o u y;3:b c ch d g h k l m n p ph r s sh t v w");
		this.t.parts = new string[]
		{
			"ork",
			"ah",
			"ois",
			"nsin",
			"ona",
			"ina",
			"insk",
			"ovo",
			"va",
			"od",
			"irsk",
			"wa",
			"owo",
			"irk",
			"ow",
			"es",
			"go",
			"on",
			"owa",
			"dran",
			"drun",
			"be",
			"ka",
			"ama",
			"kyo",
			"ro",
			"poro",
			"to"
		};
		this.addTemplate("Letters#2-5,part");
		this.addGroup(new string[]
		{
			"folk_city",
			"0f1Vf5f89V5ef|0:a bra broo e gree hi tho we wi woo wre ye;1:d d k ll lm n n nd rn sh st w;9:d d dge ft k ld le ll m mbe n n rd rst;5:a e i o u;8:broo co cro da de fie fo ha hi hu ley ri stea to woo",
			"frankish_city",
			"0f1f2f3|0:char mon vill saint beau gren ver mont font val grand;1:a e i o u;2:b c d f g l m n p r s t v;3:bourg champ court fort lac mer pont sur val ville",
			"germanic_city",
			"03f6|0:al ba bla bro bu da dre do du elk ei ge gro ha he ka ko la le lo ma ne obe pu ro scho scha sta ste tru tra wal wa za ze;1:a e o u i;3:ch sch tsch dsch ech;6:ber stei dof au bah tal hem wal huf hus furt zel feld fut wiz rih bech sten ing",
			"iberian_city",
			"01Cf3f46v|0:alb arro bar cad cor del es grana ib jae las mad nav port sal se ter val zam;1:a e i o u;3:bor ce dri fort mir val zar;4:a ar es ia id or;6:m n x",
			"monolux_city",
			"00f1c3vf6|0:bel cl col con da do de du es fl gr he la le mar mon ne pel ra ro re ru st str vi we ze;1:a e o u i;3:ch ng sch;6:ange buerg weila mont vil fon sur mer ton dorph in gen cort don lux",
			"nordic_city",
			"013cf5vf8,9df|0:asg b b bj d eir f g gr h h ing j kn od r s skj sv th vl;1:ad ag ag agn al ar eir el en en ig ik im in ing irk old or or orn ulf un ylfr;3:a e i o u y;5:dr fjr ld rg rn sk st th;9:ad al ald and ard eim ell en ik ik ike og olm ord org und;8:b d fj fj g h h l r sk sk st st sv t v",
			"poly_city",
			"0f_f0lf4ff9|0:ahu ai ha ka ka la mai na pu wai wai;1:ba be ca co da de fa fe ga ge la le ma me na ne pa pe ra re sa se ta te va ve;9:au ea ea er ha hu ka la le li lo na ni ni nu ra te ua wa wa;4:'",
			"pomeranian_city",
			"0,11f3v6|0:b ch d g gr j k kr l m n p pl r st str sz szcz tr w z;1:a e i o u y;3:ch dz gr n rz st sz ł;6:borg gard gród lin nek ni nik no staw swald sław w wice wice wno wy",
			"posh_city",
			"01f3C4|0:b b c h h k n qu r str t;1:am art at een el ich ing ourne ox;3:a e i o u;4:borou brig buro field ford hare haym ley meyr shyre steyd syde ting ton ton vylle well wood wyrth",
			"rome_city",
			"013C5Vf9Cf|0:aq c fl m n n p p r t v v v;3:al an en ent i in on u;5:dun num p tia tum;9:a ce e el ia ia ium na um;1:al ar as av eap ed edi er er er omp or or or or u",
			"rus_city",
			"024f8C|0:ek k kr m n n p s sam sv v vl;2:ad ara asno ater aza eli er etro in izh och os ovo;8:ad ad ara evo insk ir ol olo orod ov ovo ovo ovsk sk urg y;4:b g gr k m m p sk v",
			"slavic_city",
			"01cf3vf6|0:b cz d gd g k kr l m n p r sz szcz w z r t ch ł;1:a e o u y i;3:cz sz rz dz ł rz ch;6:ów awa ice ek owa yno in lin anin zyn owic sk an skowo owice",
			"nomad_city",
			"212124|0:ba da fa ha ma na ra sa ta za;1:a i u;2:b d f h k l m n r s t z;4:ad adi ah al as asr i i i i",
			"sino_city",
			"0181f8V9|0:b b ch d g g k k l m m n n n p s sh t;1:a a a e e ei i o u u ua;8:h n n ng ng ng sh zh;9:ai an an ou",
			"siam_city",
			"03Cf4Vf3Cf4Vf5Vf6C_8f/04V_8|0:ban chi kru lae nak non om pat phu sam su udo udo;3:a e i o u y;4:ch g k l m n ng p r s t;5:b ch n th th;6:ai akhon ani ep et uri;8:hin kaen kret lao mai mui rai rat sot tao wan yai",
			"vishna_city",
			"03Cf2356Cf/03C24|0:ba bom cha del hy ja kan ko lu mum pu;2:b ch d g h j k l m n p r s t v;3:a e i o u;4:bad bay gar garg garh ghat halli kot nagar nam oor palli patt por prayag pur pur uru vada vaka valasa wada waka;5:bad bay gar garg garh ghat kot nagar nam patt por prayag pur pur;6:a e i",
			"nihon_city",
			"011f23f/011f32f|0:ga go hi ka kyo na osa ra ro sa shi to yo;1:ko ku ga go hi ka na ra ro sa shi shin to ya;2:bu ka ki ma ju wa yo;3:chi ha ku sa shi ra ta to ya"
		});
		this.clone("human_kingdom", "human_unit");
		this.addOnomastic("010f1f0f1f1c0v_4f/3f_010f1f0f1f1c0v/010f1f0f1f1c0v_5_6|0:b b d g k n r s;1:a o;3:great holy the;4:dynasty empire hegemony imperium kingdom realm;5:of;6:dawn dusk dynasty moon sun");
		this.addOnomastic("0f1010f1Vf0Cf1Vf_4f/3f_0f1010f1Vf0Cf1Vf/0f1010f1Vf0Cf1Vf_5_6|0:a e i o u y;1:b c ch d g h k l m n p ph r s sh t v w;3:great holy the;4:dynasty empire hegemony imperium kingdom realm;5:of;6:dawn dusk dynasty moon sun");
		this.addGroup(new string[]
		{
			"folk_kingdom",
			"0f1V5Cf8f9C|0:a bra broo e gree hi tho we wi woo wre ye;1:d d k ll lm n n nd rn sh st w;9:and arch aven ead idge ield ire oft old orth;5:a e i o u;8:cr f h hav l m r sh st w",
			"frankish_kingdom",
			"0f12f4C5f68|0:b fr l n n s g;1:a au eu o o u;2:nk rg rm rr st str le n th ll;4:a e i o u;5:b c d f g l m n p r s t v;6:an l m r t;8:al an and ar ark ealm el en er ia ia in on on or urg om eim ed eich ia ium en er on un en an in un",
			"germanic_kingdom",
			"01C2f5Cf7V9|0:a ae ba be bru d ea el eo fr ge go gu hi i si stei t th th th w w;1:a ae e ea eo o u y;2:d d d d dal g g g l ld ld ld lf lf lfr n n ng r r rn th;9:ade adt ald ald all and ane ard ark eim ein eld ield old old olf one ord orf orpe ort ost own und une urg;7:b b b fl fr fr fr g g g gr h h h h hr kn l l m m r r sk sw sw tr w w w w w w;5:ald ald and ar ar ard elm eod er ert ic ich id id im in is ulf und",
			"iberian_kingdom",
			"01C2V9f7f|0:alta cast el la mont porto roca santo sierra tor val villa;1:a ae e ia io o u ua;7:ar el eth is th yn;2:d dr l n nt r rd s t v x;9:ales anza ario ente esta illo ion ium olo oncia ora",
			"monolux_kingdom",
			"00f1c6|0:bel cl col con da de do du es flo gr he hel la le lux mar mon ne pel ra re ro ru st str vi we ze;1:a e i o u;3:ch ng sch;6:ange buerg burg cort don dorph fon gen heim in land lux mer mont reux stein sur ton vald veld vic vil weila",
			"nordic_kingdom",
			"0f174Ef,89df|0:bj br dr eir f fr g gr hj r sk st th v;1:agn akk al ark arn arr enr igg ik imh okk orr ulfr unn yrk yrkr;4:a ae au e ei i io o u yr;9:dr l ld ld ll mr nd nd r rn;8:a a a a a e ei o o u;7:f g h h l r sk sk th v",
			"poly_kingdom",
			"019v01|0:a ae e eu i io o u y;1:am an ap ar at au en et ha he ho is it ka ke kh ma me mi mo na ne ni no ra re ri ro sa se si so ta te ti to tu;9:'",
			"pomeranian_kingdom",
			"01C2f3Cf|0:bub first flat grif grim grod kasz kosl naug pomer pyr rumm stett stol szczec used;1:a e o u;2:b ch d g gr l l m r st t th we;3:ad and au edt en ia in itz ium od om or um urch",
			"posh_kingdom",
			"01Cf2C3Vf5Cf|0:alb angl avin br cam corn er ess jer kent nor ox sher som suff sus vic war wick winds;1:ia ion itan or tor;2:a e i o u;3:d d h l n p r r sh t;5:am and e elm ia ish om on",
			"rome_kingdom",
			"019|0:a ae e eu i io o u y;1:cast flav fort lt luc magn mper r reg sol temp val vent;9:alis anth ar ariel arion arix ary ax el er eth ia iel ion ior is is ium ius iusm on or or orius ra thys tor um yn",
			"rus_kingdom",
			"013C6f|0:k m n p r sl ts v;1:ar av etro iev olga ov ys ysk;3:a e i o u;9:dom ire ity rdom;6:dal gorod grad land rod skaya ver zan",
			"slavic_kingdom",
			"015f8f9C|0:bel bul cr cz don kash lyh lyt pol rys ser sl sl syl ykr;1:at ech es gar i o on u y;3:a e i o u;5:b n r s t v v;8:and ansk ard en om orb ov sk stan us v;9:a a ia ia ia",
			"nomad_kingdom",
			"0f12125|0:ab al ay fa ma sa um;1:a i u;2:b d f h k l m n r s t z;5:a e ia ya",
			"sino_kingdom",
			"010f456ff|0:ch h m q q s t w y zh;1:an ang in ing ing ong ou u u uan;4:b ch d g g k l m m n n p s t w;5:a ao uo;6:n ng",
			"siam_kingdom",
			"03C4345|0:ayu ha lanna pa ra sri su sukh;3:a e i o u;4:b ch k kh l m n p r s t th y;5:ai ani aya on uri",
			"vishna_kingdom",
			"012Cf|0:abhi aho andhra anga anu asma ava ba bha bho bri bu ce cha che cho chu da de di do fu ga go gu ha he ho ikshva iksva ja ka ke kha khe khme ki ko ksha ku la li lo ma mi mu na ne oi pa po pra pu ra re ro sa se sha shi shu si so sri su ta tha to tri utpa va vi ya;1:b cchav d damb darbh dasam dav dir dnapor duk dura dyot ffn g gar ghel gvansh h hadaval hadrath haman han har harat hey hisapal hurip ilendr indhav ink jahnat janat japah japat jayanagar jjik jvapal k katiy ket kotta ky l labh labhr lachur lakanagar lav lingg ll llabh llal llav lon luky lw m mar marup mas matat mava mbuw mm mp n nagar nchal nd ndhar ndy nes ng ngar ngg nggal ngha ngka nind niw njav nl nmal nn nna nt nv p pal pt pur pur pur r rabha rasen rashiv rat rataraj rath rayang ripunjay rjaratr rkh rkot rn rtadi ruma ryank s sal sh shiv shm shnu shtrakut shunag sunur t ta tah tar tava tiy tr ts tsy tu tyur ud urav ury v vagang vd vijay y yak ysal;2:a a a a a a aj ak al al am an an an an an an an an an and ap ar ar arh as at e e e eddy en en er i i i i i i i indh ir it o och om or u u u u u uh uj um ur us",
			"nihon_kingdom",
			"01f1ff2fff2ff2|0:an chi ga he ka ku ma mu na ra ro shi su to wa ya;1:ga he ka ku ma ma ma mu na ra ro su to wa ya;2:ba dai fu gu ji ko ku na sho te cho chi kyo"
		});
		this.add(new NameGeneratorAsset
		{
			id = "orc_unit",
			vowels = new string[]
			{
				"a",
				"e",
				"o"
			},
			consonants = new string[]
			{
				"d",
				"g",
				"k",
				"n",
				"p",
				"r",
				"t",
				"z"
			},
			parts = new string[]
			{
				"igh",
				"ord",
				"uz",
				"duz",
				"rid",
				"ogh",
				"agh",
				"okh",
				"uh",
				"dzz",
				"ez",
				"az",
				"oz",
				"top",
				"urg"
			}
		});
		this.addTemplate("Letters#3-7");
		this.addTemplate("Part");
		this.addTemplate("Part,part");
		this.addTemplate("Part, ,Part");
		this.addOnomastic("010f_010f/0f91f_0f91f/101|0:d g k n p r t z;1:a e o;9:agh az duz dzz ez igh ogh okh ord oz rid top uh urg uz");
		this.addOnomastic("0101fC0fV1fC/1010fV1fC0fV/9_f9f/91|0:d g k n p r t z;1:a e o;9:agh az duz dzz ez igh ogh okh ord oz rid top uh urg uz");
		this.clone("orc_city", "orc_unit");
		this.t.templates = new List<string[]>();
		this.addTemplate("Part, ,Part");
		this.addTemplate("consonant,letters#3-5");
		this.addTemplate("consonant,letters#3-5,part");
		this.addTemplate("consonant,letters#1-3, ,CONSONANT,letters#2-3");
		this.addOnomastic("01010f9f/010f_010/9_9|0:d g k n p r t z;1:a e o;9:agh az duz dzz ez igh ogh okh ord oz rid top uh urg uz");
		this.clone("orc_kingdom", "orc_unit");
		this.t.add_addition_chance = 0.5f;
		this.t.addition_start = new string[]
		{
			"Bloody",
			"Axe of",
			"The",
			"Blood of",
			"Bad",
			"Strong",
			"Tall",
			"Red",
			"Green"
		};
		this.t.addition_ending = new string[]
		{
			"Fighters",
			"Gang",
			"Band",
			"of Death",
			"Axes",
			"Brothers",
			"Warriors",
			"Boyz",
			"Horde"
		};
		this.t.templates = new List<string[]>();
		this.addTemplate("addition_start,CONSONANT,letters#2-3,part,addition_ending");
		this.addOnomastic("3_4_01010f9/01010f9/2f_01010f9/01010f9_5f/01010f9_4_6|0:d g k n p r t z;1:a e o;2:bloody the bad strong tall red green;3:axes axe blood club club;4:of;5:fighters gang band axes brothers warriors boyz horde;6:death glory blood;9:agh az duz dzz ez igh ogh okh ord oz rid top uh urg uz");
		this.addTemplate("addition_start,Part,addition_ending");
		this.addTemplate("addition_start,Part,part,addition_ending");
		this.addTemplate("addition_start,Part, ,Part,addition_ending");
		this.addOnomastic("3_4_9_f9f/9_f9f/2f_9_f9f/9_f9f_5f/9_f9f_4_6|0:d g k n p r t z;1:a e o;2:bloody the bad strong tall red green;3:axes axe blood club club;4:of;5:fighters gang band axes brothers warriors boyz horde;6:death glory blood;9:agh az duz dzz ez igh ogh okh ord oz rid top uh urg uz");
		this.addTemplate("addition_start,CONSONANT,letters#1-3, ,CONSONANT,letters#2-3,addition_ending");
		this.addOnomastic("3_4_010f_010/2f_010f_010/010f_010_5f/010f_010_4_6|0:d g k n p r t z;1:a e o;2:bloody the bad strong tall red green;3:axes axe blood club club;4:of;5:fighters gang band axes brothers warriors boyz horde;6:death glory blood;9:agh az duz dzz ez igh ogh okh ord oz rid top uh urg uz");
		this.addTemplate("CONSONANT,letters#3-5,part");
		this.addTemplate("CONSONANT,letters#1-3, ,CONSONANT,letters#2-3");
		this.addOnomastic("01010f9/010f_010|0:d g k n p r t z;1:a e o;9:agh az duz dzz ez igh ogh okh ord oz rid top uh urg uz");
		this.clone("orc_language", "orc_unit");
		this.addOnomastic("0f1C97/010f97C/010f_0107C|0:d g k n p r t z;1:a e o;9:agh az duz dzz ez igh ogh okh ord oz rid top uh urg uz;7:ic ish ian");
		this.clone("orc_religion", "orc_unit");
		this.addOnomastic("9fff_45df6_l|4:d d d k k k z z z b gr vr wr;5:e e e a a a o;6:k g rg;9:Big");
		this.add(new NameGeneratorAsset
		{
			id = "dwarf_unit",
			vowels = new string[]
			{
				"a",
				"e",
				"o"
			},
			special1 = new string[]
			{
				"Dhun",
				"Dum",
				"Gor",
				"Ber",
				"Ger",
				"Kog",
				"Gul",
				"Ther",
				"Thor",
				"Von",
				"Gil",
				"Ver",
				"Vagh",
				"Gigh",
				"Deg",
				"Dig",
				"Bam",
				"Von",
				"Han",
				"Dhir",
				"Mugh",
				"Mul",
				"Gorn",
				"Gan",
				"Bol",
				"Gal"
			},
			consonants = new string[]
			{
				"b",
				"d",
				"g",
				"h",
				"k",
				"m",
				"n",
				"p",
				"r",
				"s",
				"t"
			},
			parts = new string[]
			{
				"ahl",
				"uhm",
				"ril",
				"til",
				"rim",
				"dum",
				"al",
				"or",
				"uhr",
				"ihr",
				"or"
			},
			max_consonants_in_row = 1,
			max_vowels_in_row = 1
		});
		this.addOnomastic("9f_1f01f0fV8|0:b d g h k m n p r s t;1:a e o;8:ahl uhm ril til rim dum al or uhr ihr or;9:dhun dum gor ber ger kog gul ther thor von gil ver vagh gigh deg dig bam von han dhir mugh mul gorn gan bol gal");
		this.addTemplate("special1, ,Letters#2-4,part");
		this.addTemplate("special1, ,Letters#2-4,part");
		this.addTemplate("Letters#1-4,part");
		this.addTemplate("Letters#1-4,part");
		this.clone("dwarf_city", "dwarf_unit");
		this.t.templates = new List<string[]>();
		this.addOnomastic("9f_1f01f0fV8|0:b d g h k m n p r s t;1:a e o;8:ahl uhm ril til rim dum al or uhr ihr or;9:dhun dum gor ber ger kog gul ther thor von gil ver vagh gigh deg dig bam von han dhir mugh mul gorn gan bol gal");
		this.addTemplate("special1, ,Letters#2-4,part");
		this.addTemplate("letters#2-4,part");
		this.addTemplate("letters#2-4,part");
		this.clone("dwarf_kingdom", "dwarf_unit");
		this.t.add_addition_chance = 0.5f;
		this.t.addition_start = new string[]
		{
			"Miners of",
			"Great",
			"Rocky",
			"Spears of",
			"Ancient"
		};
		this.t.addition_ending = new string[]
		{
			"Stones",
			"Rocks",
			"Boulders",
			"Mountain",
			"Mountains",
			"Miners",
			"Kingdom",
			"Shields",
			"Picks",
			"Drunks"
		};
		this.t.templates = new List<string[]>();
		this.addOnomastic("3_4_9f_1f0f1Cf0Vf8/9f_1f0f1Cf0Vf8/2f_9f_1f0f1Cf0Vf8/9f_1f0f1Cf0Vf8_5f|2:great rocky ancient;3:miners spears;4:of;5:stones rocks boulders mountain mountains miners kingdom shields picks drunks;0:b d g h k m n p r s t;1:a e o;8:ahl uhm ril til rim dum al or uhr ihr or;9:dhun dum gor ber ger kog gul ther thor von gil ver vagh gigh deg dig bam von han dhir mugh mul gorn gan bol gal");
		this.addTemplate("addition_start,special1, ,Letters#1-4,part,addition_ending");
		this.addTemplate("addition_start,Letters#1-4,part,addition_ending");
		this.addTemplate("addition_start,Letters#1-4,part,addition_ending");
		this.addTemplate("addition_start,Letters#1-4,part,addition_ending");
		this.clone("dwarf_language", "dwarf_unit");
		this.addOnomastic("97/87/1f01ff0Vff87C|0:b d g h k m n p r s t;1:a e o;8:ahl uhm ril til rim dum al or uhr ihr or;9:dhun dum gor ber ger kog gul ther thor von gil ver vagh gigh deg dig bam von han dhir mugh mul gorn gan bol gal;7:ic ish ian");
		this.clone("dwarf_religion", "dwarf_unit");
		this.addOnomastic("97/87/1f01ff0Vff87C|0:b d g h k m n p r s t;1:a e o;8:ahl uhm ril til rim dum al or uhr ihr or;9:dhun dum gor ber ger kog gul ther thor von gil ver vagh gigh deg dig bam von han dhir mugh mul gorn gan bol gal;7:ia ism ity ogy am to anta ancy iya aka i");
		this.add(new NameGeneratorAsset
		{
			id = "elf_unit",
			vowels = new string[]
			{
				"a",
				"e",
				"o"
			},
			consonants = new string[]
			{
				"c",
				"d",
				"f",
				"h",
				"l",
				"m",
				"n",
				"r",
				"s",
				"t"
			},
			special1 = new string[]
			{
				"Ylh",
				"Emy",
				"Ny",
				"Ly",
				"If",
				"Se",
				"Am",
				"Yaa",
				"Omy",
				"Na",
				"Yg",
				"Yd",
				"Eo",
				"Yo",
				"O",
				"A"
			},
			special2 = new string[]
			{
				"A'",
				"E'",
				"O'"
			},
			parts = new string[]
			{
				"ua",
				"ra",
				"on",
				"ad",
				"nor",
				"ne",
				"hel",
				"on",
				"hil",
				"ore",
				"ora",
				"ona",
				"era",
				"eas",
				"ari",
				"adi",
				"ona"
			},
			max_consonants_in_row = 1,
			max_vowels_in_row = 1
		});
		this.addOnomastic("9f01f0fV4/9f10f1fC4/4m|0:c d f h l m n r s t;1:a e o;9:ylh emy ny ly if se am yaa omy na yg yd eo yo o a a' e' o';4:ua ra on ad nor ne hel on hil ore ora ona era eas ari adi ona");
		this.addTemplate("special1,letters#1-4,part");
		this.addTemplate("special2,Letters#1-4,part");
		this.addTemplate("letters#1-4,part");
		this.addTemplate("letters#1-4,part");
		this.clone("elf_city", "elf_unit");
		this.addOnomastic("9f01f0fV4/9f10f1fC4|0:c d f h l m n r s t;1:a e o;9:ylh emy ny ly if se am yaa omy na yg yd eo yo o a a' e' o';4:ua ra on ad nor ne hel on hil ore ora ona era eas ari adi ona");
		this.addTemplate("special1,letters#1-4,part");
		this.addTemplate("special2,Letters#1-4,part");
		this.addTemplate("letters#1-4,part");
		this.addTemplate("letters#1-4,part");
		this.addOnomastic("901f0fV_01f4/901f0fV_10f4/910f1fC_10f4/910f1fC_01f4|0:c d f h l m n r s t;1:a e o;9:ylh emy ny ly if se am yaa omy na yg yd eo yo o a;4:ua ra on ad nor ne hel on hil ore ora ona era eas ari adi ona");
		this.addTemplate("special1,letters#1-4, ,Letters#1-3,part");
		this.clone("elf_kingdom", "elf_unit");
		this.t.add_addition_chance = 0.5f;
		this.t.addition_start = new string[]
		{
			"Great",
			"Green",
			"Arrows of",
			"Spears of",
			"Ancient",
			"Royal"
		};
		this.t.addition_ending = new string[]
		{
			"Keepers",
			"Forest",
			"Children",
			"Brothers",
			"of Fire",
			"of Rain",
			"of Earth",
			"Kingdom",
			"Lands"
		};
		this.addOnomastic("9f01f0fV4/9f10f1fC4/4m|0:c d f h l m n r s t;1:a e o;9:ylh emy ny ly if se am yaa omy na yg yd eo yo o a a' e' o';4:ua ra on ad nor ne hel on hil ore ora ona era eas ari adi ona");
		this.addTemplate("addition_start,special1,letters#1-4,part,addition_ending");
		this.addTemplate("addition_start,special2,Letters#1-4,part,addition_ending");
		this.addTemplate("addition_start,special1,letters#1-4, ,Letters#1-3,part,addition_ending");
		this.addTemplate("addition_start,Letters#1-4,part,addition_ending");
		this.addTemplate("addition_start,Letters#1-4,part,addition_ending");
		this.clone("elf_language", "elf_unit");
		this.addOnomastic("95fff7/9f01f0fV45fff7/9f10f1fC45fff7/4m5fff7|0:c d f h l m n r s t;1:a e o;9:ylh emy ny ly if se am yaa omy na yg yd eo yo o a a' e' o';4:ua ra on ad nor ne hel on hil ore ora ona era eas ari adi ona;7:ic ish ian;5:'");
		this.clone("elf_religion", "elf_unit");
		this.addOnomastic("95fff7/9f01f0fV45fff7/9f10f1fC45fff7/4m5fff7|0:c d f h l m n r s t;1:a e o;9:ylh emy ny ly if se am yaa omy na yg yd eo yo o a a' e' o';4:ua ra on ad nor ne hel on hil ore ora ona era eas ari adi ona;7:ia ism ity ogy am to anta ancy iya aka i;5:'");
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00087D54 File Offset: 0x00085F54
	private void addCultureNames()
	{
		this.add(new NameGeneratorAsset
		{
			id = "human_culture"
		});
		this.addOnomastic("012|0:ne wa gi ca fo two ado ja ste sho klo e a ka ri wu to;1:ru ni too vo phu te ku ve su me du pe to ste o e a u oo;2:rab dab pab ian");
		this.add(new NameGeneratorAsset
		{
			id = "elf_culture"
		});
		this.addOnomastic("012/012/012/01|0:A' E' O' Yda Ifa Yaa Ia Oya Yra Na Ma Ya;1:dre dra de du te tre tra tro ti to tri na ma sa da pa;2:o e y a yo oi");
		this.add(new NameGeneratorAsset
		{
			id = "dwarf_culture"
		});
		this.addOnomastic("012/012/012/012/012/012/01|0:X Th Tr Gl H Dh Dum Dor Gor Ther Thor Gah Geh Ger Dur Der Dar Dig Deg Dag Ger Dhan Don Dan;1:i o a e odi adi edi ada idi uru udu igi egi oki oko;2:d ded dad dum gah geh h d b c gh th gh gagh digh dig dag dog");
		this.add(new NameGeneratorAsset
		{
			id = "orc_culture"
		});
		this.addOnomastic("0_1|0:Dek Kek Kak Dak Zeg Zog Zag Bak Dak Rak Dek Mek Mak;1:Dek Kek Kak Dak Zeg Zog Zag Bak Dak Rak Dek Mek Mak Bah Wah Waah");
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x00087DEC File Offset: 0x00085FEC
	private void addClanMottos()
	{
		this.add(new NameGeneratorAsset
		{
			id = "clan_mottos",
			use_dictionary = true
		});
		this.addDictPart("from", "from");
		this.addDictPart("we", "we");
		this.addDictPart("we_bring", "we bring");
		this.addDictPart("with", "with");
		this.addDictPart(" ", " ");
		this.addDictPart("bad_concept", "darkness,hell,war,unknown,abyss,nothing,void,blood");
		this.addDictPart("words_good", "light,updog,hope,power,pride,honor,strength,wisdom,skill,health,virtue,unity,valor,truth,peace,progress,prosperity,passion,compassion,duty,destiny,freedom,balance,pride,nature,justice,dignity,safety,vigilance,honesty,integrity,prosperity,purity");
		this.addDictPart("family_stuff", "Love,Live,Pray,Think,Dare,Kill,Sleep,Dream,Drink,Eat,Dance,Fight,Win,Build,Break,Conquere,Create,Rule,Worship,Adore,Laugh,Smile,Learn,Grow,Bite");
		this.addDictPart("family_stuff_lower_case", this.t.dict_parts["family_stuff"].ToLower());
		this.addTemplate("family_stuff,$comma$,family_stuff,$comma$,family_stuff");
		this.addTemplate("from, ,bad_concept,$comma$,we_bring, ,words_good");
		this.addTemplate("with, ,words_good,$comma$,we, ,family_stuff_lower_case");
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x00087EE0 File Offset: 0x000860E0
	private void addAllianceMottos()
	{
		this.add(new NameGeneratorAsset
		{
			id = "alliance_mottos",
			use_dictionary = true
		});
		this.addDictPart("we", "We");
		this.addDictPart("are", " are ");
		this.addDictPart("and", " and ");
		this.addDictPart("of", " of ");
		this.addDictPart("will", " will ");
		this.addDictPart("very", "very ");
		this.addDictPart(" ", " ");
		this.addDictPart("body_part", "heart,body,soul,brain,wings,mind");
		this.addDictPart("material_part", "steel,stone,ice,fire,silver,gold,iron");
		this.addDictPart("something_concept", "powerful,tough,stronk,strong,cool,eternal,kind,great,holy,fancy,nice,free,best,awesome");
		this.addDictPart("something_thing", "shield,brothers,power,alliance,together,sword,weapon,knife,axe,hammer,spear");
		this.addDictPart("protec", "protect,protec,atak,attack,defend,stand together,conquare,build,create,live forever,win,be,stay");
		this.addTemplate("body_part,of,material_part");
		this.addTemplate("we,are,something_thing");
		this.addTemplate("we,are,something_concept, ,body_part");
		this.addTemplate("we,are,very,something_concept, ,body_part");
		this.addTemplate("we,are,something_concept");
		this.addTemplate("we,are,very,something_concept");
		this.addTemplate("we,are,something_concept, ,something_thing");
		this.addTemplate("we,are,very,something_concept, ,something_thing");
		this.addTemplate("something_thing,of,material_part");
		this.addTemplate("we,will,protec");
		this.addTemplate("we,are,something_thing,and,something_concept");
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00088044 File Offset: 0x00086244
	private void addKingdomMottos()
	{
		this.add(new NameGeneratorAsset
		{
			id = "kingdom_mottos",
			use_dictionary = true
		});
		this.addDictPart("all_hail", "we follow ,all hail ,we believe in ,one and only ,our life for ,swords for ,all for ,blood for ,we fight for ");
		this.addDictPart("names", "Mastef,Hugo,Maxim,Jupe,Xiphos,Orange,Glonk,Nikon,Quicklast,Andrey,Sonja,Julia,Poncho,Faris,Cody,Jim,Steeam,Chicky,Chad,Luk,Midnight Blade,Luk,Half Chest,mountain,Airod,Astral,Little Glonk,Eye");
		this.addDictPart("concept", "life,death,sorrow,hope,awesome,tears,eternal,one,best");
		this.addDictPart("word_something", "nothing,love,spirits,death,life,truth,glory,wealth,roots");
		this.addDictPart("event", "victory,death,forgiveness,salvation,redemption,happiness,life");
		this.addDictPart("words_good", "updog,hope,power,pride,honor,strength,wisdom,skill,health,virtue,unity,valor,motherland,truth,peace,progress,prosperity,passion,compassion,duty,destiny,freedom,balance,pride,nature,justice,dignity,safety,vigilance,honesty,integrity,prosperity");
		this.addDictPart("words_bad", "fear,doubt,anger,frustration,failure,depression,contempt,pain,regrets,sadness,remorse");
		this.addDictPart("body_parts", "arm,leg,head,finger,body,heart,blood,tail,hell,neck,wings");
		this.addDictPart("weapon", "sword,axe,spear,bow");
		this.addDictPart("guided_by", "guided by ");
		this.addDictPart("forged_by", "forged by ");
		this.addDictPart("armor", "boots,shield,helmet,armor");
		this.addDictPart("items", "gold,coins,rock,stone");
		this.addDictPart("addition", "amazing,beatiful");
		this.addDictPart("foods", "food,fish,tea,beer,bread");
		this.addDictPart("elements", "fire,water,earth,air,magic,crystal,steel,poison,lightning,curse");
		this.addDictPart("we", "We");
		this.addDictPart("brings", " brings ");
		this.addDictPart("are", " are ");
		this.addDictPart("somewhere", " in Hell, in the Darkness, in the Shadows, in the Light, in the Sea, in the Land, in the Dreams, in the History, in the Heavens");
		this.addDictPart("ending_concept", "life,death,war,battle,sea,kingdom");
		this.addDictPart("bound", "bound,determined");
		this.addDictPart("this_is_ending", "fine,good");
		this.addDictPart("no", "no ,forget ,abandon ,destroy ,exterminate ,burn ");
		this.addDictPart("life", "love,life,death");
		this.addDictPart("we_ending", " we trust!, we believe!, we shine!");
		this.addDictPart("we_desire", " we desire, we believe, we want");
		this.addDictPart("is_the", " is the way, is the truth, is the destiny");
		this.addDictPart("we_like", "we like ,we love ");
		this.addDictPart("is", " is ");
		this.addDictPart("and", " and ");
		this.addDictPart("with", "With ");
		this.addDictPart("in", "In ");
		this.addDictPart("by", " by ");
		this.addDictPart("or", " or ");
		this.addDictPart("hold_our", "hold our ,hold my ");
		this.addDictPart("we_use_our", "we use our ");
		this.addDictPart("in_end", " in ");
		this.addDictPart("we_something", " we live, we go, we attack");
		this.addDictPart("through", " through ");
		this.addDictPart("only", "only ,always ,forever ");
		this.addDictPart("the", " the ");
		string LIST_ALL = "foods;weapon;armor;items;words_good;body_parts;elements";
		this.addTemplate("forged_by,names;foods;event;elements;words_good;words_bad");
		this.addTemplate("all_hail,names;foods;event");
		this.addTemplate("we_like,foods");
		this.addTemplate("event,through," + LIST_ALL);
		this.addTemplate("hold_our,foods;weapon;armor;items;words_good");
		this.addTemplate("words_good;foods;names,is,this_is_ending");
		this.addTemplate("in,words_good,we_ending");
		this.addTemplate("life,is,words_good");
		this.addTemplate("hold_our,weapon;armor;items");
		this.addTemplate("foods;weapon;armor;items;words_good,is_the");
		this.addTemplate("with," + LIST_ALL + ",somewhere");
		this.addTemplate("with," + LIST_ALL + ",we_something");
		this.addTemplate("with," + LIST_ALL + ",and," + LIST_ALL);
		this.addTemplate(string.Concat(new string[]
		{
			"with,",
			LIST_ALL,
			",and,",
			LIST_ALL,
			",we_something"
		}));
		this.addTemplate("we,are,concept");
		this.addTemplate("we,are,concept,somewhere");
		this.addTemplate("we,are,concept,somewhere");
		this.addTemplate("bound,by,word_something");
		this.addTemplate("no,words_bad,$comma$,no,words_bad,$comma$,only,words_good");
		this.addTemplate("words_good,$comma$,words_good,$comma$,words_good");
		this.addTemplate("words_good,and,words_good");
		this.addTemplate("in,word_something,we_ending");
		this.addTemplate("with,words_good,and,words_good");
		this.addTemplate("words_good;body_parts,somewhere");
		this.addTemplate("with,words_good;body_parts,and,words_good;body_parts");
		this.addTemplate("guided_by,words_good");
		this.addTemplate("words_good,brings,words_good");
		this.addTemplate("we_use_our,weapon;body_parts,in_end,ending_concept");
		this.addTemplate(LIST_ALL + ",or,words_bad");
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x000884A8 File Offset: 0x000866A8
	private void addDictPart(string pID, string pListString)
	{
		this.t.addDictPart(pID, pListString);
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x000884B7 File Offset: 0x000866B7
	private void addOnomastic(string pListString)
	{
		this.t.addOnomastic(pListString);
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x000884C5 File Offset: 0x000866C5
	private void addTemplate(string pTemplate)
	{
		this.t.addTemplate(pTemplate);
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x000884D3 File Offset: 0x000866D3
	private void addPartGroup(string pListString)
	{
		this.t.addPartGroup(pListString);
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x000884E1 File Offset: 0x000866E1
	private void addPartGroup2(string pListString)
	{
		this.t.addPartGroup2(pListString);
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x000884EF File Offset: 0x000866EF
	private void addPartGroup3(string pListString)
	{
		this.t.addPartGroup3(pListString);
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x000884FD File Offset: 0x000866FD
	public NameGeneratorAsset add(string pID, string pOnomasticTemplate)
	{
		NameGeneratorAsset result = this.add(new NameGeneratorAsset
		{
			id = pID
		});
		this.addOnomastic(pOnomasticTemplate);
		return result;
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x00088518 File Offset: 0x00086718
	public void addGroup(params string[] pSets)
	{
		if (pSets.Length % 2 != 0)
		{
			throw new ArgumentException("The number of parameters must be even.");
		}
		for (int i = 0; i < pSets.Length; i += 2)
		{
			string tId = pSets[i];
			string tOnomastic = pSets[i + 1];
			if (!string.IsNullOrEmpty(tOnomastic))
			{
				this.add(tId, tOnomastic);
			}
		}
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x00088560 File Offset: 0x00086760
	public override void post_init()
	{
		base.post_init();
		foreach (NameGeneratorAsset tAsset in this.list)
		{
			if (!tAsset.hasOnomastics() && !tAsset.use_dictionary)
			{
				if (tAsset.consonants == null)
				{
					tAsset.consonants = NameGeneratorAsset.consonants_sounds;
				}
				if (tAsset.vowels == null)
				{
					tAsset.vowels = NameGeneratorAsset.vowels_all;
				}
			}
		}
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x000885E8 File Offset: 0x000867E8
	public override void editorDiagnostic()
	{
		base.editorDiagnostic();
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x000885F0 File Offset: 0x000867F0
	private void checkOnWorldLoad()
	{
		Actor tActor = null;
		Kingdom tKingdom = null;
		foreach (Actor tWorldActor in MapBox.instance.units)
		{
			if (NameGeneratorChecks.hasLatinKing(tWorldActor))
			{
				if (tActor == null || tWorldActor.kingdom.king.hasLover())
				{
					tActor = tWorldActor;
				}
				if (NameGeneratorChecks.hasEnemyLatinKing(tWorldActor))
				{
					tActor = tWorldActor;
					break;
				}
			}
		}
		if (tActor == null)
		{
			return;
		}
		tKingdom = tActor.kingdom;
		foreach (NameGeneratorAsset tAsset in this.list)
		{
			List<string[]> templates = tAsset.templates;
			if (templates != null && templates.Count > 0)
			{
				if (tAsset.check != null && !tAsset.check(tActor))
				{
					BaseAssetLibrary.logAssetLog("<e>AssetLibrary<NameGeneratorAsset></e>: <b>" + tAsset.id + "</b> skipped test because check conditions failed", null);
				}
				else
				{
					foreach (string[] tClassicTemplate in tAsset.templates)
					{
						string tTest = NameGenerator.generateNameFromTemplate(tAsset, tActor, tKingdom, true, 0, null, tClassicTemplate, false, null, ActorSex.None, false);
						if (string.IsNullOrEmpty(tTest))
						{
							BaseAssetLibrary.logAssetLog("<e>AssetLibrary<NameGeneratorAsset></e>: <b>" + tAsset.id + "</b> had no result", string.Join(",", tClassicTemplate));
						}
						else if (tTest.Contains('$'))
						{
							BaseAssetLibrary.logAssetError(string.Concat(new string[]
							{
								"<e>AssetLibrary<NameGeneratorAsset></e>: <b>",
								tAsset.id,
								"</b> seems to be missing a replacer : <b>",
								tTest,
								"</b>"
							}), string.Join(",", tClassicTemplate));
						}
					}
				}
			}
		}
		MapBox.on_world_loaded = (Action)Delegate.Remove(MapBox.on_world_loaded, new Action(this.checkOnWorldLoad));
	}

	// Token: 0x06000967 RID: 2407 RVA: 0x00088830 File Offset: 0x00086A30
	public override NameGeneratorAsset clone(string pNew, string pFrom)
	{
		NameGeneratorAsset nameGeneratorAsset = base.clone(pNew, pFrom);
		nameGeneratorAsset.onomastics_templates = new List<string>();
		return nameGeneratorAsset;
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x00088848 File Offset: 0x00086A48
	private void addBookNames()
	{
		this.add(new NameGeneratorAsset
		{
			id = "$base_book_template$",
			use_dictionary = true
		});
		this.t.finalizer = new NameGeneratorFinalizer(NameGeneratorLibrary.finalizerBookName);
		this.addDictPart("$alliance$", "$alliance$");
		this.addDictPart("$architect_name$", "$architect_name$");
		this.addDictPart("$city$", "$city$");
		this.addDictPart("$city_random$", "$city_random$");
		this.addDictPart("$clan$", "$clan$");
		this.addDictPart("$clan_random$", "$clan_random$");
		this.addDictPart("$culture$", "$culture$");
		this.addDictPart("$culture_random$", "$culture_random$");
		this.addDictPart("$family$", "$family$");
		this.addDictPart("$family_founder_1$", "$family_founder_1$");
		this.addDictPart("$family_founder_2$", "$family_founder_2$");
		this.addDictPart("$family_random$", "$family_random$");
		this.addDictPart("$figure$", "$king$,$clan$,$leader$");
		this.addDictPart("$food$", "$food$");
		this.addDictPart("$king$", "$king$");
		this.addDictPart("$king_lover$", "$king_lover$");
		this.addDictPart("$kingdom$", "$kingdom$");
		this.addDictPart("$kingdom_random$", "$kingdom_random$");
		this.addDictPart("$king_random$", "$king_random$");
		this.addDictPart("$language$", "$language$");
		this.addDictPart("$leader$", "$leader$");
		this.addDictPart("$leader_random$", "$leader_random$");
		this.addDictPart("$random_subspecies$", "$random_subspecies$");
		this.addDictPart("$religion$", "$religion$");
		this.addDictPart("$subspecies$", "$subspecies$");
		this.addDictPart("$unit$", "$unit$");
		this.addDictPart("$warrior$", "$warrior$");
		this.addDictPart("$world_name$", "$world_name$");
		this.addDictPart("of", " of ");
		this.addDictPart("about", " about ");
		this.addDictPart("and", " and ");
		this.addDictPart("by", " by ");
		this.addDictPart("on", " on ");
		this.addDictPart("in", " in ");
		this.addDictPart("ofonbout", " of , about , on ");
		this.addDictPart("toon", " to , on ");
		this.addDictPart("their", "their");
		this.addDictPart("the", "the");
		this.addDictPart("to", "to");
		this.addDictPart("its", "its");
		this.addDictPart(" ", " ");
		this.clone("book_name_love_story", "$base_book_template$");
		this.t.replacer = new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnFamily);
		NameGeneratorAsset t = this.t;
		t.replacer = (NameGeneratorReplacer)Delegate.Combine(t.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnFamilyFounders));
		NameGeneratorAsset t2 = this.t;
		t2.replacer = (NameGeneratorReplacer)Delegate.Combine(t2.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnKingLover));
		NameGeneratorAsset t3 = this.t;
		t3.replacer = (NameGeneratorReplacer)Delegate.Combine(t3.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceAnyCity));
		this.addDictPart("story", "adventure,adventures,ballad,story,stories,tale,tales,myths,novel,legend,romance,love story,lore,history,chronicle,chronicles,ballad,ballads,poem,poems,poetry");
		this.addDictPart("love", "when,how,where,why");
		this.addDictPart("met", "fell for,met,saw,encountered,found,desired,admired,missed,needed,searched for,looked for,waited for,searched for,seeked,pursued,found,discovered,spotted,noticed,observed,beheld,caught sight of,caught a glimpse of");
		this.addDictPart("days", "days");
		this.addDictPart("day_numbers", "two,three,four,five,six,seven,eight,nine,ten,eleven,twelve,thirteen,fourteen,fifteen,sixteen,seventeen,eighteen,nineteen,twenty");
		this.addDictPart("month_name", "Crabuary,Greguary,Musch,Monolith,Meow,Joon,Jooly,Citrust,Septbark,Makotober,Novembear,Endember");
		this.addTemplate("$family$, ,story");
		this.addTemplate("story,of,$family$");
		this.addTemplate("story,of,$family_founder_1$");
		this.addTemplate("story,of,$family_founder_2$");
		this.addTemplate("story,of,$family_founder_1$,and,$family_founder_2$");
		this.addTemplate("story,of,$family_founder_2$,and,$family_founder_1$");
		this.addTemplate("story,of,$king$,and,$king_lover$");
		this.addTemplate("love, ,$family_founder_1$, ,met, ,$family_founder_2$");
		this.addTemplate("love, ,$family_founder_2$, ,met, ,$family_founder_1$");
		this.addTemplate("love, ,$king$, ,met, ,$king_lover$");
		this.addTemplate("day_numbers, ,days,in,month_name");
		this.addTemplate("day_numbers, ,days,of,month_name");
		this.addTemplate("$family_founder_1$,in,$city_random$");
		this.addTemplate("$family_founder_2$,in,$city_random$");
		this.clone("book_name_bad_story", "$base_book_template$");
		NameGeneratorAsset t4 = this.t;
		t4.replacer = (NameGeneratorReplacer)Delegate.Combine(t4.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceAnyLeader));
		NameGeneratorAsset t5 = this.t;
		t5.replacer = (NameGeneratorReplacer)Delegate.Combine(t5.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceAnyKing));
		NameGeneratorAsset t6 = this.t;
		t6.replacer = (NameGeneratorReplacer)Delegate.Combine(t6.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceAnyClan));
		NameGeneratorAsset t7 = this.t;
		t7.replacer = (NameGeneratorReplacer)Delegate.Combine(t7.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceAnyFamily));
		NameGeneratorAsset t8 = this.t;
		t8.replacer = (NameGeneratorReplacer)Delegate.Combine(t8.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceAnyFamilyFounders));
		this.addDictPart("story", "misadventure,failure,tragedy,disaster,folly,shame,farce,debacle");
		this.addDictPart("misdeeds", "sins,misdeeds,scandals,crimes,shames,faults,flaws,follies,regrets");
		this.addTemplate("misdeeds,of,$clan_random$");
		this.addTemplate("misdeeds,and,$clan_random$");
		this.addTemplate("misdeeds,of,$family_random$");
		this.addTemplate("misdeeds,and,$family_random$");
		this.addTemplate("misdeeds,of,$leader_random$");
		this.addTemplate("misdeeds,and,$leader_random$");
		this.addTemplate("misdeeds,of,$king_random$");
		this.addTemplate("misdeeds,and,$king_random$");
		this.addTemplate("story,of,$clan_random$");
		this.addTemplate("story,of,$family_random$");
		this.addTemplate("story,of,$leader_random$");
		this.addTemplate("story,of,$king_random$");
		this.addTemplate("story,of,$family_founder_1$,and,$family_founder_2$");
		this.addTemplate("story,of,$family_random$,and,their, ,misdeeds");
		this.addTemplate("story,of,misdeeds,of,$clan_random$");
		this.addTemplate("story,of,misdeeds,and,$clan_random$");
		this.addTemplate("story,of,misdeeds,of,$family_random$");
		this.addTemplate("story,of,misdeeds,and,$family_random$");
		this.addTemplate("story,of,misdeeds,of,$leader_random$");
		this.addTemplate("story,of,misdeeds,and,$leader_random$");
		this.addTemplate("story,of,misdeeds,of,$king_random$");
		this.addTemplate("story,of,misdeeds,and,$king_random$");
		this.addTemplate("story,of,$family_random$,and,their, ,misdeeds");
		this.addTemplate("story,of,$clan_random$,and,their, ,misdeeds");
		this.clone("book_name_fable", "$base_book_template$");
		NameGeneratorAsset t9 = this.t;
		t9.replacer = (NameGeneratorReplacer)Delegate.Combine(t9.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnName));
		NameGeneratorAsset t10 = this.t;
		t10.replacer = (NameGeneratorReplacer)Delegate.Combine(t10.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnCity));
		NameGeneratorAsset t11 = this.t;
		t11.replacer = (NameGeneratorReplacer)Delegate.Combine(t11.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnKingdom));
		NameGeneratorAsset t12 = this.t;
		t12.replacer = (NameGeneratorReplacer)Delegate.Combine(t12.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnAlliance));
		NameGeneratorAsset t13 = this.t;
		t13.replacer = (NameGeneratorReplacer)Delegate.Combine(t13.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceFigure));
		this.addDictPart("story", "fable,tale,myths,legend,lesson");
		this.addDictPart("folklore", "fables,tales,myths,legends,folklore,lessons");
		this.addDictPart("moral", "moral,virtue,lesson,teaching,warning,cautionary tale");
		this.addDictPart("virtue", "wisdom,kindness,courage,patience,humility,justice,friendship");
		this.addDictPart("vice", "greed,pride,foolishness,envy,anger,vanity,arrogance");
		this.addTemplate("story,of,$alliance$");
		this.addTemplate("story,of,$unit$");
		this.addTemplate("story,of,$city$,and,vice");
		this.addTemplate("story,of,$kingdom$");
		this.addTemplate("story,of,$figure$");
		this.addTemplate("folklore,of,$alliance$");
		this.addTemplate("folklore,of,$kingdom$");
		this.addTemplate("folklore,of,$clan$");
		this.addTemplate("folklore,of,$city$");
		this.addTemplate("story,of,$clan$,and,their, ,virtue");
		this.addTemplate("story,of,$clan$,and,their, ,vice");
		this.addTemplate("story,of,$unit$,and,the, ,moral");
		this.addTemplate("story,of,$unit$,and,the, ,$king$");
		this.addTemplate("story,of,$unit$,and,the, ,$leader$");
		this.addTemplate("story,of,$king$,and,the, ,moral");
		this.addTemplate("story,of,$king$,and,the, ,$kingdom$");
		this.addTemplate("story,of,$leader$,and,the, ,moral");
		this.addTemplate("story,of,$leader$,and,the, ,$city$");
		this.addTemplate("story,of,$unit$,and,their, ,virtue");
		this.addTemplate("virtue,and,vice,of,$unit$");
		this.addTemplate("virtue,of,$unit$");
		this.addTemplate("virtue,and,$unit$");
		this.addTemplate("vice,and,$unit$");
		this.addTemplate("virtue,and,vice,of,$figure$");
		this.addTemplate("virtue,of,$figure$");
		this.addTemplate("virtue,and,$figure$");
		this.addTemplate("vice,and,$figure$");
		this.clone("book_name_warfare_manual", "$base_book_template$");
		NameGeneratorAsset t14 = this.t;
		t14.replacer = (NameGeneratorReplacer)Delegate.Combine(t14.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceFigure));
		NameGeneratorAsset t15 = this.t;
		t15.replacer = (NameGeneratorReplacer)Delegate.Combine(t15.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnKingdom));
		this.addDictPart("manual", "manual,guide,handbook,codex,compendium,treatise,doctrine,principles,lessons,teachings");
		this.addDictPart("warfare", "warfare,battle,combat,strategy,tactics,conquest,defense");
		this.addDictPart("attribute", "glory,honor,discipline,strategy,cunning,victory,chaos");
		this.addTemplate("manual,of,warfare,by,$figure$");
		this.addTemplate("manual,of,warfare,of,$figure$");
		this.addTemplate("manual,of,warfare,of,$kingdom$");
		this.addTemplate("$figure$,and,$figure$");
		this.addTemplate("manual,of,the, ,$figure$,and,their, ,attribute");
		this.addTemplate("manual,of,warfare,and,attribute,of,$figure$");
		this.addTemplate("manual,of,attribute,and,warfare,of,$figure$");
		this.clone("book_name_economy_manual", "$base_book_template$");
		NameGeneratorAsset t16 = this.t;
		t16.replacer = (NameGeneratorReplacer)Delegate.Combine(t16.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceFavoriteFood));
		NameGeneratorAsset t17 = this.t;
		t17.replacer = (NameGeneratorReplacer)Delegate.Combine(t17.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceFigure));
		this.addDictPart("manual", "manual,guide,handbook,codex,compendium,treatise,doctrine,principles,lessons,teachings");
		this.addDictPart("concept", "economy,wealth,money,gold,trade,management,leadership,taxation,administration,tribute,toll,duty,levy,censure");
		this.addDictPart("attribute", "prosperity,riches,growth,efficiency,resources,change,progress,strategy");
		this.addDictPart("kingdom", "kingdom,realm,city,state,domain,empire");
		this.addTemplate("manual,ofonbout,$food$");
		this.addTemplate("concept,and,$food$");
		this.addTemplate("concept,and,attribute");
		this.addTemplate("$food$, ,concept");
		this.addTemplate("manual,ofonbout,concept,by,$figure$");
		this.addTemplate("manual,ofonbout,concept,of,$figure$");
		this.addTemplate("manual,ofonbout,concept,and,attribute,of,$figure$");
		this.addTemplate("manual,ofonbout,$figure$,and,their, ,attribute");
		this.addTemplate("manual,toon,concept,ofonbout,the, ,kingdom,by,$figure$");
		this.clone("book_name_stewardship_manual", "$base_book_template$");
		NameGeneratorAsset t18 = this.t;
		t18.replacer = (NameGeneratorReplacer)Delegate.Combine(t18.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceFigure));
		this.addDictPart("manual", "manual,guide,handbook,codex,compendium,treatise,doctrine,principles,lessons,teachings");
		this.addDictPart("concept", "economy,wealth,money,gold,trade,management,leadership,taxation,administration,tribute,toll,duty,levy,censure");
		this.addDictPart("attribute", "prosperity,riches,growth,efficiency,resources,change,progress,strategy");
		this.addDictPart("kingdom", "kingdom,realm,city,state,domain,empire");
		this.addTemplate("manual,ofonbout,concept");
		this.addTemplate("manual,ofonbout,concept,by,$figure$");
		this.addTemplate("manual,ofonbout,concept,of,$figure$");
		this.addTemplate("manual,ofonbout,concept,and,attribute");
		this.addTemplate("manual,ofonbout,concept,and,attribute,of,$figure$");
		this.addTemplate("manual,ofonbout,attribute,and,concept");
		this.addTemplate("manual,ofonbout,attribute,and,concept,of,$figure$");
		this.addTemplate("manual,ofonbout,$figure$,and,their, ,attribute");
		this.addTemplate("manual,toon,concept,ofonbout,the, ,kingdom");
		this.addTemplate("manual,toon,concept,ofonbout,the, ,kingdom,by,$figure$");
		this.addTemplate("concept,and,the, ,kingdom");
		this.addTemplate("concept,and,the, ,kingdom,by,$figure$");
		this.addTemplate("concept, ,manual");
		this.addTemplate("concept,and,attribute, ,manual");
		this.addTemplate("attribute, ,manual");
		this.addTemplate("attribute,and,concept, ,manual");
		this.clone("book_name_diplomacy_manual", "$base_book_template$");
		NameGeneratorAsset t19 = this.t;
		t19.replacer = (NameGeneratorReplacer)Delegate.Combine(t19.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceFigure));
		this.addDictPart("manual", "art,manual,guide,handbook,codex,compendium,treatise,doctrine,principles,lessons,teachings");
		this.addDictPart("concept", "diplomacy,negotiation,alliance,politics,relations,peacemaking,statesmanship");
		this.addDictPart("attribute", "cunning,grace,tact,patience,strategy,charisma,honor");
		this.addDictPart("kingdom", "kingdom,realm,state,empire,coalition,domain");
		this.addDictPart("kingdoms", "kingdoms,realms,states,empires,coalitions,domains");
		this.addTemplate("manual,ofonbout,concept");
		this.addTemplate("manual,ofonbout,concept,by,$figure$");
		this.addTemplate("manual,ofonbout,concept,of,$figure$");
		this.addTemplate("manual,ofonbout,concept,and,kingdom");
		this.addTemplate("manual,ofonbout,concept,and,attribute");
		this.addTemplate("manual,ofonbout,concept,and,attribute,of,$figure$");
		this.addTemplate("manual,ofonbout,concept,and,attribute,of,kingdoms");
		this.addTemplate("manual,ofonbout,attribute,and,concept");
		this.addTemplate("manual,ofonbout,attribute,and,concept,of,$figure$");
		this.addTemplate("manual,ofonbout,$figure$,and,their, ,attribute");
		this.addTemplate("manual,toon,concept,ofonbout,the, ,kingdom");
		this.addTemplate("manual,toon,concept,ofonbout,the, ,kingdom,by,$figure$");
		this.addTemplate("manual,ofonbout,attribute,and,concept");
		this.addTemplate("manual,ofonbout,attribute,and,concept,of,$figure$");
		this.addTemplate("manual,ofonbout,attribute,and,concept");
		this.addTemplate("manual,ofonbout,attribute,and,kingdom");
		this.addTemplate("manual,ofonbout,attribute,and,concept,of,kingdoms");
		this.addTemplate("concept,and,the, ,kingdom");
		this.addTemplate("concept,and,the, ,kingdom,by,$figure$");
		this.addTemplate("concept, ,manual");
		this.addTemplate("concept,and,attribute, ,manual");
		this.addTemplate("attribute, ,manual");
		this.addTemplate("attribute,and,concept, ,manual");
		this.addTemplate("concept,by,$figure$");
		this.addTemplate("concept,of,$figure$");
		this.addTemplate("concept,and,the, ,kingdom");
		this.addTemplate("kingdoms,and,concept");
		this.addTemplate("kingdoms,and,attribute");
		this.addTemplate("concept,and,attribute");
		this.addTemplate("concept,and,attribute,of,$figure$");
		this.addTemplate("concept,and,attribute,of,kingdoms");
		this.addTemplate("attribute,and,concept");
		this.addTemplate("attribute,and,concept,of,$figure$");
		this.addTemplate("$figure$,and,their, ,attribute");
		this.addTemplate("concept,ofonbout,the, ,kingdom");
		this.addTemplate("concept,ofonbout,the, ,kingdom,by,$figure$");
		this.addTemplate("attribute,and,concept");
		this.addTemplate("attribute,and,concept,of,$figure$");
		this.addTemplate("attribute,and,concept");
		this.addTemplate("attribute,of,kingdoms");
		this.addTemplate("attribute,and,kingdoms");
		this.addTemplate("attribute,and,the, ,kingdom");
		this.addTemplate("attribute,and,concept,of,kingdoms");
		this.clone("book_name_math", "$base_book_template$");
		NameGeneratorAsset t20 = this.t;
		t20.replacer = (NameGeneratorReplacer)Delegate.Combine(t20.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceFigure));
		NameGeneratorAsset t21 = this.t;
		t21.replacer = (NameGeneratorReplacer)Delegate.Combine(t21.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnCulture));
		NameGeneratorAsset t22 = this.t;
		t22.replacer = (NameGeneratorReplacer)Delegate.Combine(t22.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceAnyCulture));
		this.addDictPart("manual", "manual,guide,treatise,handbook,codex,compendium,treatise,doctrine,principles,lessons,teachings");
		this.addDictPart("math", "mathematics,geometry,algebra,arithmetic,logic,numbers,equations,fractals,calculus");
		this.addDictPart("attribute", "precision,logic,beauty,symmetry,harmony,proof,reasoning,theory");
		this.addDictPart("theory", "theory,principles,axioms,formulas,truths");
		this.addDictPart("kingdom", "kingdom,realm,state,empire,domain");
		this.addTemplate("manual,of,math,by,$figure$");
		this.addTemplate("theory,of,math,of,$figure$");
		this.addTemplate("theory,of,math,of,$culture$");
		this.addTemplate("theory,of,math,of,$culture_random$");
		this.addTemplate("manual,of,math,and,attribute,of,$figure$");
		this.addTemplate("manual,of,attribute,and,math,by,$figure$");
		this.addTemplate("manual,of,math,and,math,by,$king$");
		this.addTemplate("manual, ,to, ,math,of,the, ,kingdom,by,$figure$");
		this.addTemplate("manual,of,the, ,attribute,of,math,by,$figure$");
		this.addTemplate("manual,on,attribute,and,math,of,$figure$");
		this.addTemplate("math,by,$figure$");
		this.addTemplate("math,of,$figure$");
		this.addTemplate("math,of,$culture$");
		this.addTemplate("math,of,$culture_random$");
		this.addTemplate("math,and,attribute,of,$figure$");
		this.addTemplate("attribute,and,math,by,$figure$");
		this.addTemplate("math,and,math,by,$king$");
		this.addTemplate("math,of,the, ,kingdom,by,$figure$");
		this.addTemplate("the, ,attribute,of,math,by,$figure$");
		this.addTemplate("attribute,and,math,of,$figure$");
		this.clone("book_name_biology", "$base_book_template$");
		NameGeneratorAsset t23 = this.t;
		t23.replacer = (NameGeneratorReplacer)Delegate.Combine(t23.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnKingdom));
		NameGeneratorAsset t24 = this.t;
		t24.replacer = (NameGeneratorReplacer)Delegate.Combine(t24.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceAnyKingdom));
		NameGeneratorAsset t25 = this.t;
		t25.replacer = (NameGeneratorReplacer)Delegate.Combine(t25.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnSubspecies));
		NameGeneratorAsset t26 = this.t;
		t26.replacer = (NameGeneratorReplacer)Delegate.Combine(t26.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceAnySubspecies));
		NameGeneratorAsset t27 = this.t;
		t27.replacer = (NameGeneratorReplacer)Delegate.Combine(t27.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnCity));
		NameGeneratorAsset t28 = this.t;
		t28.replacer = (NameGeneratorReplacer)Delegate.Combine(t28.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceAnyCity));
		NameGeneratorAsset t29 = this.t;
		t29.replacer = (NameGeneratorReplacer)Delegate.Combine(t29.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceWorldName));
		NameGeneratorAsset t30 = this.t;
		t30.replacer = (NameGeneratorReplacer)Delegate.Combine(t30.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceArchitectName));
		this.addDictPart("manual", "Codex,Guide,Treatise,Handbook,Compendium");
		this.addDictPart("biology", "Life,Nature,Evolution,Anatomy,Biodiversity");
		this.addDictPart("attribute", "Origin,Growth,Evolution,Balance,Harmony,Complexity,Diversity,Physiology,Genetics,Symmetry,Adaptation,Resilience,Diet,Instincts,Mutations,Variations");
		this.addDictPart("event", "Extinction,Evolution,Adaptation,Migration,Domestication,Hybridization,Speciation,Invasion,Infection,Infestation,Parasitism,Commensalism,Mutualism,Cooperation,Competition,Predation,Decomposition,Respiration,Reproduction,Mutation,Selection,Variation,Inheritance,Expression,Regulation,Recombination,Divergence,Convergence");
		this.addDictPart("kingdom", "Realm,Domain,Empire,State");
		this.addDictPart("fauna", "Creatures,Beasts,Organisms,Flora,Fauna,Wildlife,Forms,Phenotypes,Oddities");
		this.addTemplate("event,of,biology");
		this.addTemplate("manual,of,biology");
		this.addTemplate("attribute,of,biology");
		this.addTemplate("kingdom,of,biology");
		this.addTemplate("biology,of,$world_name$");
		this.addTemplate("attribute,of,$world_name$");
		this.addTemplate("fauna,of,$world_name$");
		this.addTemplate("manual,of,$world_name$");
		this.addTemplate("fauna,of,$world_name$");
		this.addTemplate("fauna,of,$city_random$");
		this.addTemplate("biology,of,$random_subspecies$");
		this.addTemplate("fauna,of,$kingdom_random$");
		this.addTemplate("attribute,of,$random_subspecies$");
		this.addTemplate("$random_subspecies$,manual");
		this.addTemplate("$subspecies$,of,the, ,$kingdom$");
		this.addTemplate("$subspecies$,and,$city$");
		this.addTemplate("$subspecies$,and,biology");
		this.addTemplate("$random_subspecies$,and,biology");
		this.addTemplate("$random_subspecies$,of,$world_name$");
		this.addTemplate("$architect_name$,and,biology");
		this.addTemplate("$architect_name$,and,attribute");
		this.addTemplate("$architect_name$,and,$world_name$");
		this.clone("book_name_history", "$base_book_template$");
		NameGeneratorAsset t31 = this.t;
		t31.replacer = (NameGeneratorReplacer)Delegate.Combine(t31.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnName));
		NameGeneratorAsset t32 = this.t;
		t32.replacer = (NameGeneratorReplacer)Delegate.Combine(t32.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnKing));
		NameGeneratorAsset t33 = this.t;
		t33.replacer = (NameGeneratorReplacer)Delegate.Combine(t33.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnKingClan));
		NameGeneratorAsset t34 = this.t;
		t34.replacer = (NameGeneratorReplacer)Delegate.Combine(t34.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnLeader));
		NameGeneratorAsset t35 = this.t;
		t35.replacer = (NameGeneratorReplacer)Delegate.Combine(t35.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnKingdom));
		NameGeneratorAsset t36 = this.t;
		t36.replacer = (NameGeneratorReplacer)Delegate.Combine(t36.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnReligion));
		NameGeneratorAsset t37 = this.t;
		t37.replacer = (NameGeneratorReplacer)Delegate.Combine(t37.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnCulture));
		NameGeneratorAsset t38 = this.t;
		t38.replacer = (NameGeneratorReplacer)Delegate.Combine(t38.replacer, new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnLanguage));
		this.addDictPart("manual", "lessons,manual,guide,chronicle,chronicles,treatise,compendium,codex,account,record");
		this.addDictPart("history", "history,events,stories,legends,myths,lore");
		this.addDictPart("attribute", "truth,legacy,glory,memory,justice,honor");
		this.addDictPart("kingdom", "kingdom,realm,empire,state,domain,coalition");
		this.addDictPart("person", "$king$,$leader$,$unit$");
		this.addDictPart("subject", "$king$,$clan$,$leader$,$kingdom$,$religion$,$culture$,$language$");
		this.addTemplate("history,by,person");
		this.addTemplate("history,of,subject");
		this.addTemplate("history,and,attribute,by,person");
		this.addTemplate("history,and,attribute,of,subject");
		this.addTemplate("history,and,attribute");
		this.addTemplate("attribute,and,history");
		this.addTemplate("attribute,and,attribute");
		this.addTemplate("attribute,and,attribute,by,person");
		this.addTemplate("attribute,and,attribute,of,subject");
		this.addTemplate("kingdom,of,history");
		this.addTemplate("kingdom,and,history");
		this.addTemplate("kingdom,of,attribute");
		this.addTemplate("kingdom,and,attribute");
		this.addTemplate("kingdom,and,its, ,history");
		this.addTemplate("kingdom,and,its, ,history,by,person");
		this.addTemplate("manual,of,history,by,person");
		this.addTemplate("manual,of,history,of,subject");
		this.addTemplate("manual,of,the, ,history,of,subject");
		this.addTemplate("history,of,subject,and,its, ,kingdom");
		this.addTemplate("manual,of,history,and,attribute,by,person");
		this.addTemplate("manual,on,history,and,attribute,of,subject");
		this.addTemplate("manual,of,attribute,and,attribute,by,person");
		this.addTemplate("manual,of,kingdom,and,its, ,history");
		this.addTemplate("manual,of,kingdom,and,its, ,history,by,person");
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x00089D60 File Offset: 0x00087F60
	private static string finalizerBookName(string pName)
	{
		if (string.IsNullOrEmpty(pName))
		{
			return pName;
		}
		pName = OnomasticsData.convertToCultureTitleCase(pName);
		TextInfo tTextInfo = CultureInfo.InvariantCulture.TextInfo;
		bool tStartsWithPrep = false;
		foreach (string tWord in NameGeneratorLibrary._preps)
		{
			string tReplace = tTextInfo.ToTitleCase(tWord);
			pName = pName.Replace(" " + tReplace + " ", " " + tWord.ToLower() + " ");
			if (pName.StartsWith(tReplace))
			{
				tStartsWithPrep = true;
			}
		}
		if (pName.StartsWith("You"))
		{
			tStartsWithPrep = true;
		}
		if (pName.StartsWith("Our"))
		{
			tStartsWithPrep = true;
		}
		if (pName.StartsWith("Wh"))
		{
			tStartsWithPrep = true;
		}
		if (pName.StartsWith("How"))
		{
			tStartsWithPrep = true;
		}
		if (!tStartsWithPrep && Randy.randomBool())
		{
			if (Randy.randomBool())
			{
				pName = "The " + pName;
			}
			else
			{
				string[] tWords = pName.Split(' ', StringSplitOptions.None);
				bool tSingular = true;
				if (tWords.Length != 0 && tWords[0].EndsWith("s"))
				{
					tSingular = false;
				}
				if (tWords.Length > 1 && tWords[1].EndsWith("s"))
				{
					tSingular = false;
				}
				if (tSingular)
				{
					if (pName[0] == 'A' || pName[0] == 'E' || pName[0] == 'I' || pName[0] == 'O' || pName[0] == 'U')
					{
						pName = "An " + pName;
					}
					else
					{
						pName = "A " + pName;
					}
				}
			}
		}
		return pName;
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x00089EE8 File Offset: 0x000880E8
	private void addItemNames()
	{
		this.add(new NameGeneratorAsset
		{
			id = "boots_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Scruffy,Shabby,Sturdy,Bloody,Smooth,Worn,Savage,Broken,Suede,Fancy,Classic,Breezy,Elegant,Rugged,Ruched,Superior,Treaded");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Waders,Kicks,Galoshes,Mukluks,Kamik,Brogans,Wellingtons,Boots,Heels,Cleats,Studs,Pumps,Slippers,Sneakers,Clogs,Moccasins");
		this.addTemplate("first,space,second");
		this.addTemplate("first,space,second");
		this.addTemplate("first,space,second");
		this.addTemplate("second");
		this.add(new NameGeneratorAsset
		{
			id = "armor_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Golden,Defensive,Complete,Splendid,Knightly,Rusty,Ponderous,Stiff,Breasted,Pocketed");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Platiander,Platus,Plato,Plates,Platah,Plats,Platt,Plote,Plotete,Plute,Pluto,Plattmeister,Platander,Platitude,Platunder,Plattler,Plata");
		this.addTemplate("first,space,second");
		this.addTemplate("first,space,second");
		this.addTemplate("first,space,second");
		this.addTemplate("second");
		this.add(new NameGeneratorAsset
		{
			id = "helmet_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Glorified,Excruciating,Exceptional,Exquisite,Special,Don't Hit");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Headhurt,Headpain,Herald,Hithead,Headbox,Brainbox,Hardhead,Hardhat,Hothat,Facesmash,Faceplate,Facemate,Headcrate,Hothead,Headhit,Brainpain,Migraine,Brainhat,Cephalalgium");
		this.addTemplate("first,space,second");
		this.addTemplate("first,space,second");
		this.addTemplate("second");
		this.add(new NameGeneratorAsset
		{
			id = "ring_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Stone of,Healing,Raven's,Harbinger of,Crafted,The,Butcher's,Queen's,King's,Behemoth's,Troll's,Stinky");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Star,Apocalypse,Omen,Storm,Prophecy,Band,Doom,Wrath,Niceness,Apology,Courage,Shriek,Toy,Desire,Carrot,Contradiction");
		this.addTemplate("first,space,second");
		this.addTemplate("first,space,second");
		this.addTemplate("first,space,second");
		this.addTemplate("second");
		this.add(new NameGeneratorAsset
		{
			id = "amulet_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Eye of,Rising,Half Moon,Full Moon,Cat's,Harlequin's,Bloody,Smooth,Mysterious,Broken,Crooked,Declining,Tiny,Smol,Lil,Harmless,Nice");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Chance,Sun,Stone,Obelisk,Wrath,Bottom,Earth,Vortex,Vertigo,Garden,Backyard,Field,Planet,Meteor,Volcano,Sinkhole");
		this.addTemplate("first,space,second");
		this.addTemplate("first,space,second");
		this.addTemplate("first,space,second");
		this.addTemplate("second");
		this.add(new NameGeneratorAsset
		{
			id = "sword_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Scary,Great,Battle,Legendary,Hero,Twisted,Bloody,Empathic,Smooth,War,Savage,Broken,Sinister,Smort,Sharp");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Sworzenegger,Ignizherziander,Obliterator,Annihilator,Oblivion,Doomblade,Sculptor,Protector,Razorblade,Blade,Swordo,Swardo,Swyrd,Reaver,End,Defender,Slicer,Promise,Excaliander,Sworziander,Oblivionherziander");
		this.addTemplate("first,space,second");
		this.addTemplate("first,space,second");
		this.addTemplate("first,space,second");
		this.addTemplate("second");
		this.add(new NameGeneratorAsset
		{
			id = "sword_name_king",
			use_dictionary = true,
			check = new NameGeneratorCheck(NameGeneratorChecks.hasLatinKing),
			replacer = new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnKing)
		});
		this.addDictPart("first", "$king$'s");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Obliterator,Annihilator,Doomblade,Protector,Reaver,Defender,Wrath");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "weapon_name_enemy_king",
			use_dictionary = true,
			check = new NameGeneratorCheck(NameGeneratorChecks.hasEnemyLatinKing),
			replacer = new NameGeneratorReplacer(NameGeneratorReplacers.replaceEnemyKing)
		});
		this.addDictPart("firstKing", "$king$'s");
		this.addDictPart("first", "End of,Gift for,Tears of");
		this.addDictPart("space", " ");
		this.addDictPart("second", "End,Demise,Tears,Grave");
		this.addDictPart("secondKing", "$king$");
		this.addTemplate("firstKing,space,second");
		this.addTemplate("first,space,secondKing");
		this.add(new NameGeneratorAsset
		{
			id = "weapon_name_enemy_kingdom",
			use_dictionary = true,
			check = new NameGeneratorCheck(NameGeneratorChecks.hasEnemyLatinKingdom),
			replacer = new NameGeneratorReplacer(NameGeneratorReplacers.replaceEnemyKingdom)
		});
		this.addDictPart("firstKingdom", "$kingdom$'s");
		this.addDictPart("first", "End of,Gift for,Tears of");
		this.addDictPart("space", " ");
		this.addDictPart("second", "End,Demise,Tears,Annihilation");
		this.addDictPart("secondKingdom", "$kingdom$");
		this.addTemplate("firstKingdom,space,second");
		this.addTemplate("first,space,secondKingdom");
		this.add(new NameGeneratorAsset
		{
			id = "weapon_name_city",
			use_dictionary = true,
			check = new NameGeneratorCheck(NameGeneratorChecks.hasLatinCity),
			replacer = new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnCity)
		});
		this.addDictPart("first", "Hope of,Glory to,Defender of,Son of,Guard of,Soul of,Heart of");
		this.addDictPart("space", " ");
		this.addDictPart("second", "$city$");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "weapon_name_kingdom",
			use_dictionary = true,
			check = new NameGeneratorCheck(NameGeneratorChecks.hasLatinKingdom),
			replacer = new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnKingdom)
		});
		this.addDictPart("first", "Hope of,Glory to,Defender of,Son of,Guard of,Soul of,Heart of");
		this.addDictPart("space", " ");
		this.addDictPart("second", "$kingdom$");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "weapon_name_culture",
			use_dictionary = true,
			check = new NameGeneratorCheck(NameGeneratorChecks.hasLatinCulture),
			replacer = new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnCulture)
		});
		this.addDictPart("first", "Hope of,Glory to,Defender of,Son of,Guard of,Soul of,Heart of");
		this.addDictPart("space", " ");
		this.addDictPart("second", "$culture$");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "axe_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Wicked,Stormy,War,Misfortune's,Gladiator's,War Battle,Eternal,King's,Cosmic");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Aruba,Edge,Frostbite,Betrayer,Hope,Annihilation,Axe,Mercy,Chopper,Chopah,Blade,Reaver,Ravager");
		this.addTemplate("first,space,second");
		this.addTemplate("second");
		this.add(new NameGeneratorAsset
		{
			id = "axe_name_king",
			use_dictionary = true,
			check = new NameGeneratorCheck(NameGeneratorChecks.hasLatinKing),
			replacer = new NameGeneratorReplacer(NameGeneratorReplacers.replaceOwnKing)
		});
		this.addDictPart("first", "Cleaver of,Wrath of,Axe of,Blade of,Edge of");
		this.addDictPart("firstKing", "$king$'s");
		this.addDictPart("space", " ");
		this.addDictPart("secondKing", "$king$");
		this.addDictPart("second", "Cleaver,Battleaxe,Reaver,Axe,Bloodaxe");
		this.addTemplate("first,space,secondKing");
		this.addTemplate("firstKing,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "hammer_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Massive,Deluded,Hell's,Deluded,Guard's,Storm,Barbaric,Perfect");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Mostef,Xiphos,Hatred,Hammertime,Agony,Denial,Terror,Scream,Battlehammer,Stonefist,Gatecrasher,Glory,Smasher,Ambassador");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "spear_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Owl's,Ivan The,Glory's,Hatred's,Thirsty,Doomy,Long,Champion");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Eye,Poncho,Supernova,Nightbane,Corpsemaker,Pike,Poke,Spike,Stinger,Regret,Point,Impaler");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "stick_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Old,Curvy,Nice,Perfect,Memorable,Heavy,Big");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Friend,Palka,Derevyashka,Derevo,Drevo,Woodstick,Toothpick,Cudgel,Club,The Whackmaker");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "bow_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Wind's,Devil's,Wretched,Shadow");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Pain,Irk,Bow,Shooter,Sun,Strike,Hatred,Pew,Eye,Heartbeat,Piercer,Might,Meteor,Vengeance,Avalance,Eagle,Aie");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "shotgun_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Red,Black,Bloody,Dark,Big,Stunning");
		this.addDictPart("dash", "-,-");
		this.addDictPart("space", "-,-");
		this.addDictPart("name", "Anne,Betty,Emma,Ava,Mia,Amelia,Lisa,Julia,Sophia,Charvi,Layla,Jocelyn,Hannah,Dasha");
		this.addDictPart("numbers", "0,1,2,3,4,5,6,7,8,9,");
		this.addTemplate("first,dash,name,dash,numbers,numbers");
		this.addTemplate("first,dash,name,dash,numbers,numbers,numbers");
		this.addTemplate("first,dash,name,dash,numbers,numbers,numbers,numbers");
		this.addTemplate("name,dash,numbers,numbers");
		this.addTemplate("name,dash,numbers,numbers,numbers");
		this.addTemplate("name,dash,numbers,numbers,numbers,numbers");
		this.add(new NameGeneratorAsset
		{
			id = "blaster_name",
			use_dictionary = true
		});
		this.addDictPart("first", "X,Y,W,Alpha,Zeta,Omega");
		this.addDictPart("dash", "-,-");
		this.addDictPart("second", "404,101,69,111,420,25,96,LOL,LMAO,LULZ,RSRS,HOHO,55555");
		this.addTemplate("first,dash,second,dash,first");
		this.addTemplate("first,dash,second");
		this.addTemplate("second,dash,first");
		this.addTemplate("second,dash,second");
		this.addTemplate("first,dash,first,dash,first");
		this.addTemplate("first,dash,first,dash,second");
		this.add(new NameGeneratorAsset
		{
			id = "flame_sword_name",
			use_dictionary = true
		});
		this.addDictPart("first", "BURNING,FLAMING,SCREAMING,HELLISH,RED,ORANGE,HOT,FIERY");
		this.addDictPart("space", " ");
		this.addDictPart("second", "DESIRE,PASSION,KASAI,CRAVE,FLAME,CONSCIENCE,VOICE,WANTS,TONGUE,WORDS,LOVE,HEART,DEATH");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "flame_hammer_name",
			use_dictionary = true
		});
		this.addDictPart("first", "BURNING,FLAMING,SCREAMING,HELLISH,RED,ORANGE,HOT,FIERY");
		this.addDictPart("space", " ");
		this.addDictPart("second", "DESIRE,PASSION,CRAVE,FLAME,CONSCIENCE,VOICE,WANTS,TONGUE,WORDS,LOVE,HEART,DEATH");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "ice_hammer_name",
			use_dictionary = true
		});
		this.addDictPart("first", "FREEZING,FROSTING,SCREAMING,GLAZING,BLUE,WHITE,COLD,SHARP");
		this.addDictPart("space", " ");
		this.addDictPart("second", "DESIRE,PASSION,CRAVE,ICE,CONSCIENCE,VOICE,WANTS,TONGUE,WORDS,LOVE,HEART,DEATH");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "necromancer_staff_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Witty,Sorrowful,Depressed,Jealous,Choleric,Wrathful");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Monica,Rachel,Chandler,Bing,Ross,Joey,Gunther,Janice,Buffay,Phoebe");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "evil_staff_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Absurd,Bitter,Desolate,Possessive,Angry,Loyal,Damned");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Kramer,George,Costanza,Elaine,Susan,Newman,Jerry,Frank,Helen,Estelle");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "white_staff_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Funny,Sad,Gloomy,Envious,Grudging,Friendly,Kind,Punchy");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Marceline,Finn,Jake,Bomo,King,Bubblegum,Prismo,Space,Trunks,Bun,Ricardio");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "plague_doctor_staff_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Second,Next,Green,Red,Lovely");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Death,Hope,Anger,Chance,Heart");
		this.addTemplate("first,space,second");
		this.add(new NameGeneratorAsset
		{
			id = "druid_staff_name",
			use_dictionary = true
		});
		this.addDictPart("first", "Green,Forest's,Organic,Grassy,Lush,Leafy");
		this.addDictPart("space", " ");
		this.addDictPart("second", "Leaf,Focus,Edgar,Toad");
		this.addTemplate("first,space,second");
	}

	// Token: 0x0400097D RID: 2429
	public const string ALLIANCE = "$alliance$";

	// Token: 0x0400097E RID: 2430
	public const string CITY = "$city$";

	// Token: 0x0400097F RID: 2431
	public const string CITY_RANDOM = "$city_random$";

	// Token: 0x04000980 RID: 2432
	public const string CLAN = "$clan$";

	// Token: 0x04000981 RID: 2433
	public const string CLAN_RANDOM = "$clan_random$";

	// Token: 0x04000982 RID: 2434
	public const string CULTURE = "$culture$";

	// Token: 0x04000983 RID: 2435
	public const string CULTURE_RANDOM = "$culture_random$";

	// Token: 0x04000984 RID: 2436
	public const string KING = "$king$";

	// Token: 0x04000985 RID: 2437
	public const string KINGDOM = "$kingdom$";

	// Token: 0x04000986 RID: 2438
	public const string KINGDOM_RANDOM = "$kingdom_random$";

	// Token: 0x04000987 RID: 2439
	public const string KING_LOVER = "$king_lover$";

	// Token: 0x04000988 RID: 2440
	public const string KING_RANDOM = "$king_random$";

	// Token: 0x04000989 RID: 2441
	public const string LANGUAGE = "$language$";

	// Token: 0x0400098A RID: 2442
	public const string LANGUAGE_RANDOM = "$language_random$";

	// Token: 0x0400098B RID: 2443
	public const string LEADER = "$leader$";

	// Token: 0x0400098C RID: 2444
	public const string LEADER_LOVER = "$leader_lover$";

	// Token: 0x0400098D RID: 2445
	public const string LEADER_RANDOM = "$leader_random$";

	// Token: 0x0400098E RID: 2446
	public const string OWN_BEST_FRIEND = "$own_best_friend$";

	// Token: 0x0400098F RID: 2447
	public const string OWN_LOVER = "$own_lover$";

	// Token: 0x04000990 RID: 2448
	public const string RELIGION = "$religion$";

	// Token: 0x04000991 RID: 2449
	public const string RELIGION_RANDOM = "$religion_random$";

	// Token: 0x04000992 RID: 2450
	public const string FAMILY = "$family$";

	// Token: 0x04000993 RID: 2451
	public const string FAMILY_FOUNDER_1 = "$family_founder_1$";

	// Token: 0x04000994 RID: 2452
	public const string FAMILY_FOUNDER_2 = "$family_founder_2$";

	// Token: 0x04000995 RID: 2453
	public const string FAMILY_RANDOM = "$family_random$";

	// Token: 0x04000996 RID: 2454
	public const string FIGURE = "$figure$";

	// Token: 0x04000997 RID: 2455
	public const string FOOD = "$food$";

	// Token: 0x04000998 RID: 2456
	public const string ITEM = "$item$";

	// Token: 0x04000999 RID: 2457
	public const string SUBSPECIES_RANDOM = "$random_subspecies$";

	// Token: 0x0400099A RID: 2458
	public const string SUBSPECIES = "$subspecies$";

	// Token: 0x0400099B RID: 2459
	public const string UNIT = "$unit$";

	// Token: 0x0400099C RID: 2460
	public const string WARRIOR = "$warrior$";

	// Token: 0x0400099D RID: 2461
	public const string ARCHITECT_NAME = "$architect_name$";

	// Token: 0x0400099E RID: 2462
	public const string WORLD_NAME = "$world_name$";

	// Token: 0x0400099F RID: 2463
	public const string COMMA = "$comma$";

	// Token: 0x040009A0 RID: 2464
	private const string BASE_BOOK_TEMPLATE = "$base_book_template$";

	// Token: 0x040009A1 RID: 2465
	private static readonly string[] _preps = new string[]
	{
		"a",
		"and",
		"as",
		"at",
		"but",
		"by",
		"en",
		"for",
		"if",
		"in",
		"its",
		"nor",
		"of",
		"on",
		"or",
		"the",
		"their",
		"to",
		"v",
		"v.",
		"via",
		"vs",
		"vs.",
		"about"
	};
}
