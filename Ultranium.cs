using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Shaders;
using Terraria.GameContent.UI;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;
using Ultranium.Backgrounds.Boss;
using Ultranium.Backgrounds.Cosmic;
using Ultranium.Backgrounds.EtherealSky;
using Ultranium.Backgrounds.ShadowBiome.Sky;
using Ultranium.Backgrounds.ShadowEventSky;
using Ultranium.Backgrounds.TrueDread;
using Ultranium.Items.BossBags.Acc;
using Ultranium.Items.BossSummon;
using Ultranium.Items.Dread;
using Ultranium.Items.Dread.Materials;
using Ultranium.Items.Dread.TrueDread;
using Ultranium.Items.Eldritch;
using Ultranium.Items.Eldritch.ShadowEvent;
using Ultranium.Items.Ethereal;
using Ultranium.Items.Guardians.Hell;
using Ultranium.Items.Guardians.Nature;
using Ultranium.Items.Ice;
using Ultranium.Items.Ocean;
using Ultranium.Items.Pets;
using Ultranium.NPCs.Dread;
using Ultranium.NPCs.Ethereal;
using Ultranium.NPCs.IceDragon;
using Ultranium.NPCs.Ignodium;
using Ultranium.NPCs.Ocean;
using Ultranium.NPCs.ShadowEvent;
using Ultranium.NPCs.ShadowWorm;
using Ultranium.NPCs.Town;
using Ultranium.NPCs.TrueDread;
using Ultranium.NPCs.Ultrum;
using Ultranium.ShadowEvent;

namespace Ultranium;

internal class Ultranium : Mod
{
	public static int GlowShroomCurrencyID;

	public static float seizureAmount;

	private static int seizureTimer;

	public static ModHotKey SpecialKey;

	private Mod mod => ModLoader.GetMod("Ultranium");

	public Ultranium()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		((Mod)this).Properties = new ModProperties
		{
			Autoload = true,
			AutoloadGores = true,
			AutoloadSounds = true,
			AutoloadBackgrounds = true
		};
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(mod);
		val.AddRecipeGroup("Ultranium:Iron/Lead", 5);
		val.AddTile(16);
		val.SetResult(285, 1);
		val.AddRecipe();
		ModRecipe val2 = new ModRecipe((Mod)(object)this);
		val2.AddRecipeGroup("Ultranium:Silver/Tungsten", 10);
		val2.AddTile(16);
		val2.SetResult(54, 1);
		val2.AddRecipe();
		ModRecipe val3 = new ModRecipe((Mod)(object)this);
		val3.AddIngredient(210, 6);
		val3.AddTile(16);
		val3.SetResult(212, 1);
		val3.AddRecipe();
		ModRecipe val4 = new ModRecipe((Mod)(object)this);
		val4.AddIngredient(31, 1);
		val4.AddIngredient(751, 35);
		val4.AddTile(16);
		val4.SetResult(53, 1);
		val4.AddRecipe();
		ModRecipe val5 = new ModRecipe((Mod)(object)this);
		val5.AddIngredient(53, 1);
		val5.AddIngredient(169, 35);
		val5.AddTile(16);
		val5.SetResult(857, 1);
		val5.AddRecipe();
		ModRecipe val6 = new ModRecipe((Mod)(object)this);
		val6.AddIngredient(53, 1);
		val6.AddIngredient(593, 35);
		val6.AddTile(16);
		val6.SetResult(987, 1);
		val6.AddRecipe();
		ModRecipe val7 = new ModRecipe((Mod)(object)this);
		val7.AddIngredient(331, 5);
		val7.AddIngredient(947, 20);
		val7.AddTile(16);
		val7.SetResult(1291, 1);
		val7.AddRecipe();
		ModRecipe val8 = new ModRecipe((Mod)(object)this);
		val8.AddIngredient(178, 10);
		val8.AddIngredient((Mod)null, "BloodClot", 10);
		val8.AddTile(16);
		val8.SetResult(29, 1);
		val8.AddRecipe();
		ModRecipe val9 = new ModRecipe((Mod)(object)this);
		val9.AddIngredient((Mod)null, "ShadowFlame", 8);
		val9.AddTile(134);
		val9.SetResult(3052, 1);
		val9.AddRecipe();
		ModRecipe val10 = new ModRecipe((Mod)(object)this);
		val10.AddIngredient((Mod)null, "ShadowFlame", 8);
		val10.AddTile(134);
		val10.SetResult(3053, 1);
		val10.AddRecipe();
		ModRecipe val11 = new ModRecipe((Mod)(object)this);
		val11.AddIngredient((Mod)null, "ShadowFlame", 8);
		val11.AddTile(134);
		val11.SetResult(3054, 1);
		val11.AddRecipe();
		ModRecipe val12 = new ModRecipe((Mod)(object)this);
		val12.AddIngredient(3467, 12);
		val12.AddTile(412);
		val12.SetResult(3063, 1);
		val12.AddRecipe();
		ModRecipe val13 = new ModRecipe((Mod)(object)this);
		val13.AddIngredient(3467, 12);
		val13.AddTile(412);
		val13.SetResult(3065, 1);
		val13.AddRecipe();
		ModRecipe val14 = new ModRecipe((Mod)(object)this);
		val14.AddIngredient(3467, 12);
		val14.AddTile(412);
		val14.SetResult(3389, 1);
		val14.AddRecipe();
		ModRecipe val15 = new ModRecipe((Mod)(object)this);
		val15.AddIngredient(3467, 12);
		val15.AddTile(412);
		val15.SetResult(1553, 1);
		val15.AddRecipe();
		ModRecipe val16 = new ModRecipe((Mod)(object)this);
		val16.AddIngredient(3467, 12);
		val16.AddTile(412);
		val16.SetResult(3546, 1);
		val16.AddRecipe();
		ModRecipe val17 = new ModRecipe((Mod)(object)this);
		val17.AddIngredient(3467, 12);
		val17.AddTile(412);
		val17.SetResult(3570, 1);
		val17.AddRecipe();
		ModRecipe val18 = new ModRecipe((Mod)(object)this);
		val18.AddIngredient(3467, 12);
		val18.AddTile(412);
		val18.SetResult(3541, 1);
		val18.AddRecipe();
		ModRecipe val19 = new ModRecipe((Mod)(object)this);
		val19.AddIngredient(3467, 12);
		val19.AddTile(412);
		val19.SetResult(3569, 1);
		val19.AddRecipe();
		ModRecipe val20 = new ModRecipe((Mod)(object)this);
		val20.AddIngredient(3467, 12);
		val20.AddTile(412);
		val20.SetResult(3571, 1);
		val20.AddRecipe();
	}

	public override void AddRecipeGroups()
	{
		RecipeGroup recipeGroup = new RecipeGroup(() => " Iron/Lead Bar", 22, 704);
		RecipeGroup.RegisterGroup("Ultranium:Iron/Lead", recipeGroup);
		recipeGroup = new RecipeGroup(() => " Silver/Tungsten Bar", 21, 705);
		RecipeGroup.RegisterGroup("Ultranium:Silver/Tungsten", recipeGroup);
		recipeGroup = new RecipeGroup(() => " Adamantite/Titanium Bar", 391, 1198);
		RecipeGroup.RegisterGroup("Ultranium:Adamantite/Titanium", recipeGroup);
		recipeGroup = new RecipeGroup(() => " Shadow Scale/Tissue Samples", 86, 1329);
		RecipeGroup.RegisterGroup("Ultranium:ShadowScale/TissueSample", recipeGroup);
		recipeGroup = new RecipeGroup(() => " Rotten Chunk/Vetebrae", 68, 1330);
		RecipeGroup.RegisterGroup("Ultranium:RottenChunk/Vetebrae", recipeGroup);
		recipeGroup = new RecipeGroup(() => " Demonite Javelin/Crimtane Pike", mod.ItemType("DemoniteJavelin"), mod.ItemType("CrimsonJavelin"));
		RecipeGroup.RegisterGroup("Ultranium:DemoniteJavelin/CrimtanePike", recipeGroup);
	}

	public override void PostSetupContent()
	{
		Mod val = ModLoader.GetMod("FKBossHealthBar");
		if (val != null)
		{
			val.Call(new object[1] { "hbStart" });
			val.Call(new object[5]
			{
				"hbSetTexture",
				((Mod)this).GetTexture("UI/ErebusBarLeft"),
				((Mod)this).GetTexture("UI/ErebusBarMiddle"),
				((Mod)this).GetTexture("UI/ErebusBarRight"),
				((Mod)this).GetTexture("UI/HBFill")
			});
			val.Call(new object[3] { "hbSetMidBarOffset", -32, 12 });
			val.Call(new object[3] { "hbSetBossHeadCentre", 80, 32 });
			val.Call(new object[2] { "hbSetFillDecoOffsetSmall", 20 });
			val.Call(new object[2]
			{
				"hbFinishSingle",
				((Mod)this).NPCType("ErebusHead")
			});
		}
		Mod val2 = ModLoader.GetMod("BossChecklist");
		if (val2 != null)
		{
			val2.Call(new object[13]
			{
				"AddBoss",
				3.5f,
				ModContent.NPCType<ZephyrSquid>(),
				this,
				"Zephyr Squid",
				(Func<bool>)(() => UltraniumWorld.downedSquid),
				ModContent.ItemType<CoralBait>(),
				new List<int>(),
				new List<int>
				{
					ModContent.ItemType<OceanScale>(),
					ModContent.ItemType<ZephyrBlade>(),
					ModContent.ItemType<ZephyrKnife>(),
					ModContent.ItemType<ZephyrTrident>(),
					ModContent.ItemType<ZephyrScepter>(),
					ModContent.ItemType<WormPet>(),
					ModContent.ItemType<MysticTentacle>()
				},
				"Fish in the ocean while using [i:" + ((Mod)this).ItemType("CoralBait") + "] as bait.",
				"Despawn Info.",
				"Ultranium/BossTextures/Squid",
				"Ultranium/BossTextures/SquidHead"
			});
			val2.Call(new object[13]
			{
				"AddBoss",
				5.5f,
				ModContent.NPCType<IceDragon>(),
				this,
				"Glacieron",
				(Func<bool>)(() => UltraniumWorld.downedDragon),
				ModContent.ItemType<IceFood>(),
				new List<int>(),
				new List<int>
				{
					ModContent.ItemType<IcePelt>(),
					ModContent.ItemType<GlacialFlail>(),
					ModContent.ItemType<GlacialGun>(),
					ModContent.ItemType<GlacialWand>(),
					ModContent.ItemType<IceTalon>()
				},
				"Use [i:" + ((Mod)this).ItemType("IceFood") + "] in the tundra.",
				"Despawn Info.",
				"Ultranium/BossTextures/IceDragon",
				"Ultranium/BossTextures/IceDragonHead"
			});
			val2.Call(new object[13]
			{
				"AddBoss",
				6.5f,
				ModContent.NPCType<DreadBoss>(),
				this,
				"Dread",
				(Func<bool>)(() => UltraniumWorld.downedDread),
				ModContent.ItemType<DreadBeacon>(),
				new List<int>(),
				new List<int>
				{
					ModContent.ItemType<DreadFlame>(),
					ModContent.ItemType<DreadScale>(),
					ModContent.ItemType<DreadSword>(),
					ModContent.ItemType<DreadBow>(),
					ModContent.ItemType<DreadStaff>(),
					ModContent.ItemType<DreadHeart>()
				},
				"Use a [i:" + ((Mod)this).ItemType("DreadBeacon") + "] anywhere during the night",
				"Despawn Info.",
				"Ultranium/BossTextures/Dread",
				"Ultranium/BossTextures/DreadHead"
			});
			val2.Call(new object[13]
			{
				"AddBoss",
				10.5f,
				ModContent.NPCType<Xenanis>(),
				this,
				"Xenanis",
				(Func<bool>)(() => UltraniumWorld.downedXenanis),
				ModContent.ItemType<EtherealLantern>(),
				new List<int>(),
				new List<int>
				{
					ModContent.ItemType<XenanisFlesh>(),
					ModContent.ItemType<EtherealSword>(),
					ModContent.ItemType<EtherealBow>(),
					ModContent.ItemType<EtherealTome>(),
					ModContent.ItemType<EtherealSummon>(),
					ModContent.ItemType<EtherealDidgeridoo>(),
					ModContent.ItemType<EtherealCore>()
				},
				"Use an [i:" + ((Mod)this).ItemType("EtherealLantern") + "] anywhere at night",
				"Despawn Info.",
				"Ultranium/BossTextures/Xenanis",
				"Ultranium/BossTextures/XenanisHead"
			});
			val2.Call(new object[13]
			{
				"AddBoss",
				15.1f,
				ModContent.NPCType<Ultrum>(),
				this,
				"Ultrum",
				(Func<bool>)(() => UltraniumWorld.downedUltrum),
				ModContent.ItemType<UltrumSummon>(),
				new List<int>(),
				new List<int>
				{
					ModContent.ItemType<UltrumShard>(),
					ModContent.ItemType<UltraniumSword>(),
					ModContent.ItemType<UltraFlail>(),
					ModContent.ItemType<UltraniumBow>(),
					ModContent.ItemType<UltraniumKunai>(),
					ModContent.ItemType<UltraniumStaff>(),
					ModContent.ItemType<UltraTome>(),
					ModContent.ItemType<UltraniumScepter>(),
					ModContent.ItemType<UltrumRelic>()
				},
				"Use an [i:" + ((Mod)this).ItemType("UltrumSummon") + "] on the surface",
				"Despawn Info.",
				"Ultranium/BossTextures/Ultrum",
				"Ultranium/BossTextures/UltrumHead"
			});
			val2.Call(new object[13]
			{
				"AddBoss",
				15.2f,
				ModContent.NPCType<Ignodium>(),
				this,
				"Ignodium",
				(Func<bool>)(() => UltraniumWorld.downedIgnodium),
				ModContent.ItemType<NetherBeacon>(),
				new List<int>(),
				new List<int>
				{
					ModContent.ItemType<HellShard>(),
					ModContent.ItemType<HellFlail>(),
					ModContent.ItemType<HellThrow>(),
					ModContent.ItemType<HellGun>(),
					ModContent.ItemType<HellJavelin>(),
					ModContent.ItemType<HellStaff>(),
					ModContent.ItemType<HellTome>(),
					ModContent.ItemType<HellScepter>(),
					ModContent.ItemType<IgnodiumRelic>()
				},
				"Use a [i:" + ((Mod)this).ItemType("NetherBeacon") + "] in the underworld",
				"Despawn Info.",
				"Ultranium/BossTextures/Ignodium",
				"Ultranium/BossTextures/IgnodiumHead"
			});
			val2.Call(new object[13]
			{
				"AddBoss",
				15.3f,
				ModContent.NPCType<TrueDread>(),
				this,
				"Absolute Dread",
				(Func<bool>)(() => UltraniumWorld.downedTrueDread),
				ModContent.ItemType<DreadBeacon>(),
				new List<int>(),
				new List<int>
				{
					ModContent.ItemType<NightmareFuel>(),
					ModContent.ItemType<DreadSpear>(),
					ModContent.ItemType<DreadYoyo>(),
					ModContent.ItemType<DreadDisc>(),
					ModContent.ItemType<DreadFlameBlaster>(),
					ModContent.ItemType<FearStaff>(),
					ModContent.ItemType<DreadTome>(),
					ModContent.ItemType<DreadScepter>()
				},
				"Use a [i:" + ((Mod)this).ItemType("DreadBeacon") + "] after the guardians of nature and hell have been defeated",
				"Despawn Info.",
				"Ultranium/BossTextures/TrueDread",
				"Ultranium/BossTextures/DreadHead"
			});
			val2.Call(new object[13]
			{
				"AddEvent",
				15.4f,
				new List<int>
				{
					ModContent.NPCType<AbyssalWraith>(),
					ModContent.NPCType<Scp2521>(),
					ModContent.NPCType<ShadeSpirit>(),
					ModContent.NPCType<Phantom>(),
					ModContent.NPCType<ShadeMass>(),
					ModContent.NPCType<AbyssalCultist>(),
					ModContent.NPCType<FlayerWraith>(),
					ModContent.NPCType<Warden>(),
					ModContent.NPCType<MindFlayer>(),
					ModContent.NPCType<MotherPhantom>()
				},
				this,
				"Abyssal Armageddon",
				(Func<bool>)(() => UltraniumWorld.downedShadowEvent),
				ModContent.ItemType<DarkResonator>(),
				new List<int>(),
				new List<int>
				{
					ModContent.ItemType<DarkMatter>(),
					ModContent.ItemType<FlayerBlade>(),
					ModContent.ItemType<FlayerBow>(),
					ModContent.ItemType<FlayerStaff>(),
					ModContent.ItemType<EldritchScythe>(),
					ModContent.ItemType<EldritchGun>(),
					ModContent.ItemType<EldritchTome>()
				},
				"Use the [i:" + ((Mod)this).ItemType("DarkResonator") + "] during the night",
				"Despawn Info.",
				"Ultranium/BossTextures/ShadowEventEnemies",
				"Ultranium/BossTextures/ShadowEventIcon"
			});
			val2.Call(new object[13]
			{
				"AddBoss",
				15.41f,
				ModContent.NPCType<ErebusHead>(),
				this,
				"Erebus",
				(Func<bool>)(() => UltraniumWorld.downedErebus),
				ModContent.ItemType<ErebusFood>(),
				new List<int>(),
				new List<int>
				{
					ModContent.ItemType<NightmareScale>(),
					ModContent.ItemType<Noctis>(),
					ModContent.ItemType<SolibusOrba>(),
					ModContent.ItemType<Exitium>(),
					ModContent.ItemType<Crepus>(),
					ModContent.ItemType<Inanis>(),
					ModContent.ItemType<CavumNigrum>(),
					ModContent.ItemType<Nihil>(),
					ModContent.ItemType<Umbra>(),
					ModContent.ItemType<Caliginus>(),
					ModContent.ItemType<ErebusGuitar>(),
					ModContent.ItemType<ShadowHeart>()
				},
				"Spawns at the end of the Abyssal Armageddon event, or, use [i:" + ((Mod)this).ItemType("ErebusFood") + "] during the Abyssal Armageddon Event",
				"Despawn Info.",
				"Ultranium/BossTextures/DarkWorm",
				"Ultranium/BossTextures/DarkWormHead"
			});
		}
	}

	private static bool BossDowned(object[] args)
	{
		if (args.Length < 2)
		{
			throw new ArgumentException("No boss name specified");
		}
		string text = args[1] as string;
		return text switch
		{
			"Squid" => UltraniumWorld.downedSquid, 
			"Dread" => UltraniumWorld.downedDread, 
			"Xenanis" => UltraniumWorld.downedXenanis, 
			"Ultrum" => UltraniumWorld.downedUltrum, 
			"Ignodium" => UltraniumWorld.downedIgnodium, 
			"TrueDread" => UltraniumWorld.downedTrueDread, 
			"ShadowEvent" => UltraniumWorld.downedShadowEvent, 
			"Erebus" => UltraniumWorld.downedErebus, 
			"Aldin" => UltraniumWorld.downedAldin, 
			_ => throw new ArgumentException("Invalid boss name:" + text), 
		};
	}

	public override void Load()
	{
		GlowShroomCurrencyID = CustomCurrencyManager.RegisterCurrency(new GlowShroomData(mod.ItemType("GlowShroomItem"), 999L));
		GameShaders.Armor.BindShader(mod.ItemType("EtherealDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades")).UseColor(0.5f, 0.9f, 0.9f).UseSecondaryColor(0.6f, 0.35f, 0.9f);
		GameShaders.Armor.BindShader(mod.ItemType("EldritchDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorHades")).UseColor(0.1f, 0.5f, 0.42f).UseSecondaryColor(0.2f, 0.22f, 0.6f);
		GameShaders.Armor.BindShader(mod.ItemType("IceDragonDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorLivingFlame")).UseColor(2f, 2f, 2f).UseSecondaryColor(1.2f, 1.9f, 2f);
		GameShaders.Armor.BindShader(mod.ItemType("AuroraDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorBrightnessGradient")).UseColor(0.95f, 0.15f, 0.85f).UseSecondaryColor(0.1f, 1f, 0.62f);
		GameShaders.Armor.BindShader(mod.ItemType("DepthsDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorTwilight")).UseImage("Images/Misc/Perlin").UseColor(0f, 1f, 0.45f);
		GameShaders.Armor.BindShader(mod.ItemType("DreadDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorPhase")).UseImage("Images/Misc/Perlin").UseColor(0.55f, 0f, 0f);
		GameShaders.Armor.BindShader(mod.ItemType("UltrumDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorFlow")).UseColor(0.84f, 2.21f, 0f).UseSecondaryColor(0f, 1.72f, 0.98f);
		GameShaders.Armor.BindShader(mod.ItemType("IgnodiumDye"), new ArmorShaderData(Main.PixelShaderRef, "ArmorFlow")).UseColor(2.55f, 1.61f, 0f).UseSecondaryColor(1f, 1.3f, 1.5f);
		if (!Main.dedServ)
		{
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/ShadowBiome"), ((Mod)this).ItemType("ShadowMusicBox"), ((Mod)this).TileType("ShadowMusicBoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/DarkDepths"), ((Mod)this).ItemType("DepthMusicBox"), ((Mod)this).TileType("DepthMusicBoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/ZephyrSquid"), ((Mod)this).ItemType("ZephyrMusicBox"), ((Mod)this).TileType("ZephyrMusicBoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/IceDragon"), ((Mod)this).ItemType("IceMusicBox"), ((Mod)this).TileType("IceMusicBoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/Dread"), ((Mod)this).ItemType("DreadMusicBox"), ((Mod)this).TileType("DreadMusicBoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/Xenanis"), ((Mod)this).ItemType("EtherealMusicBox"), ((Mod)this).TileType("EtherealMusicBoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/GuardiansPhase1"), ((Mod)this).ItemType("GuardianPhase1Box"), ((Mod)this).TileType("GuardianPhase1BoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/GuardiansPhase2"), ((Mod)this).ItemType("GuardianPhase2Box"), ((Mod)this).TileType("GuardianPhase2BoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/ShadowEventWave1"), ((Mod)this).ItemType("ShadowEventBox"), ((Mod)this).TileType("ShadowEventBoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/ShadowEventWave2"), ((Mod)this).ItemType("ShadowEventBox2"), ((Mod)this).TileType("ShadowEventBoxTile2"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/RealDread"), ((Mod)this).ItemType("TrueDreadBox"), ((Mod)this).TileType("TrueDreadBoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/MindFlayer"), ((Mod)this).ItemType("FlayerMusicBox"), ((Mod)this).TileType("FlayerMusicBoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/ErebusTheme"), ((Mod)this).ItemType("ErebusMusicBox"), ((Mod)this).TileType("ErebusMusicBoxTile"), 0);
			((Mod)this).AddMusicBox(((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/Aldin"), ((Mod)this).ItemType("AldinMusicBox"), ((Mod)this).TileType("AldinMusicBoxTile"), 0);
			Filters.Scene["Ultranium:ShadowBiome"] = new Filter(new ShadowBiomeScreenShaderData("FilterMiniTower").UseColor(0f, 0.2f, 0.05f).UseOpacity(0.5f), EffectPriority.VeryHigh);
			SkyManager.Instance["Ultranium:ShadowBiome"] = new ShadowBiomeSky();
			Filters.Scene["Blizzard"] = new Filter(new BlizzardShaderData("FilterBlizzardForeground").UseColor(1f, 1f, 1.5f).UseSecondaryColor(0.7f, 0.7f, 1f).UseImage("Images/Misc/noise")
				.UseIntensity(0.4f)
				.UseImageScale(new Vector2(3f, 0.75f)), EffectPriority.High);
			Overlays.Scene["Blizzard"] = new SimpleOverlay("Images/Misc/noise", new BlizzardShaderData("FilterBlizzardBackground").UseColor(1f, 1f, 1.5f).UseSecondaryColor(0.7f, 0.7f, 1f).UseImage("Images/Misc/noise")
				.UseIntensity(0.4f)
				.UseImageScale(new Vector2(3f, 0.75f)), EffectPriority.High, RenderLayers.Landscape);
			Filters.Scene["Ultranium:DreadBoss"] = new Filter(new DreadScreenShaderData("FilterMiniTower").UseColor(0.5f, 0f, 0f).UseOpacity(0.6f), EffectPriority.VeryHigh);
			SkyManager.Instance["Ultranium:DreadBoss"] = new DreadSky();
			Filters.Scene["Ultranium:EtherealBoss"] = new Filter(new XenanisScreenShaderData("FilterMiniTower").UseColor(0f, 0f, 0f).UseOpacity(0.5f), EffectPriority.VeryHigh);
			SkyManager.Instance["Ultranium:EtherealBoss"] = new XenanisSky();
			Filters.Scene["Ultranium:Ultrum"] = new Filter(new UltrumScreenShaderData("FilterMiniTower").UseColor(0.25f, 0.8f, 0.1f).UseOpacity(0.3f), EffectPriority.VeryHigh);
			SkyManager.Instance["Ultranium:Ultrum"] = new UltrumSky();
			Filters.Scene["Ultranium:Ignodium"] = new Filter(new FlameScreenShaderData("FilterMiniTower").UseColor(1f, 0.6f, 0.3f).UseOpacity(0.5f), EffectPriority.VeryHigh);
			SkyManager.Instance["Ultranium:Ignodium"] = new FlameSky();
			Filters.Scene["Ultranium:TrueDread"] = new Filter(new TrueDreadScreenShaderData("FilterMiniTower").UseColor(0.5f, 0f, 0f).UseOpacity(0.6f), EffectPriority.VeryHigh);
			SkyManager.Instance["Ultranium:TrueDread"] = new TrueDreadSky();
			Filters.Scene["Ultranium:ShadowEvent"] = new Filter(new ShadowEventScreenShaderData("FilterMiniTower").UseColor(0.1f, 0.6f, 0.25f).UseOpacity(0.55f), EffectPriority.VeryHigh);
			SkyManager.Instance["Ultranium:ShadowEvent"] = new ShadowEventSky();
			Filters.Scene["Ultranium:ShadowEvent2"] = new Filter(new ShadowEventDarkScreenShaderData("FilterMiniTower").UseColor(0.15f, 0.15f, 0.2f).UseOpacity(0.7f), EffectPriority.VeryHigh);
			SkyManager.Instance["Ultranium:ShadowEvent2"] = new ShadowEventSkyDark();
			Filters.Scene["Ultranium:Erebus"] = new Filter(new ErebusScreenShaderData("FilterMiniTower").UseColor(0.2f, 0.1f, 0.3f).UseOpacity(0.5f), EffectPriority.VeryHigh);
			SkyManager.Instance["Ultranium:Erebus"] = new ErebusSky();
			Filters.Scene["Ultranium:Aldin"] = new Filter(new CosmicScreenShaderData("FilterMiniTower").UseColor(0.3f, 0.2f, 0.7f).UseOpacity(0.5f), EffectPriority.VeryHigh);
			SkyManager.Instance["Ultranium:Aldin"] = new AldinSky();
			if (Main.netMode != 2)
			{
				Ref<Effect> @ref = new Ref<Effect>(((Mod)this).GetEffect("Effects/ShockwaveEffect"));
				Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(@ref, "Shockwave"), EffectPriority.VeryHigh);
				Filters.Scene["Shockwave"].Load();
			}
			SpecialKey = ((Mod)this).RegisterHotKey("Special Ability", "E");
		}
	}

	public override void ModifySunLightColor(ref Color tileColor, ref Color backgroundColor)
	{
		if (UltraniumWorld.ShadowTiles > 0)
		{
			float val = (float)UltraniumWorld.ShadowTiles / 200f;
			val = Math.Min(val, 1f);
			int r = backgroundColor.R;
			int g = backgroundColor.G;
			int b = backgroundColor.B;
			r -= (int)(155f * val * ((float)(int)backgroundColor.R / 255f));
			g -= (int)(90f * val * ((float)(int)backgroundColor.G / 255f));
			b -= (int)(120f * val * ((float)(int)backgroundColor.B / 255f));
			r = Utils.Clamp(r, 15, 255);
			g = Utils.Clamp(g, 15, 255);
			b = Utils.Clamp(b, 15, 255);
			backgroundColor.R = (byte)r;
			backgroundColor.G = (byte)g;
			backgroundColor.B = (byte)b;
		}
	}

	public override void ModifyTransformMatrix(ref SpriteViewMatrix Transform)
	{
		if (!Main.gameMenu)
		{
			seizureTimer++;
			if (seizureAmount >= 0f && seizureTimer >= 5)
			{
				seizureAmount -= 0.1f;
			}
			if (seizureAmount < 0f)
			{
				seizureAmount = 0f;
			}
			Main.screenPosition += new Vector2(seizureAmount * Main.rand.NextFloat(), seizureAmount * Main.rand.NextFloat());
		}
		else
		{
			seizureAmount = 0f;
			seizureTimer = 0;
		}
	}

	public override void UpdateMusic(ref int music, ref MusicPriority priority)
	{
		if (Main.myPlayer != -1 && !Main.gameMenu && ((Entity)Main.LocalPlayer).active)
		{
			if (Main.LocalPlayer.GetModPlayer<UltraniumPlayer>().ZoneShadow)
			{
				music = ((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/ShadowBiome");
				priority = (MusicPriority)4;
			}
			if (Main.LocalPlayer.GetModPlayer<UltraniumPlayer>().ZoneDepth)
			{
				music = ((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/DarkDepths");
				priority = (MusicPriority)4;
			}
			if (ShadowEventWorld.ShadowEventActive && !ShadowEventWorld.Phase2)
			{
				music = ((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/ShadowEventWave1");
				priority = (MusicPriority)4;
			}
			if (ShadowEventWorld.ShadowEventActive && ShadowEventWorld.Phase2)
			{
				music = ((Mod)this).GetSoundSlot((SoundType)51, "Sounds/Music/ShadowEventWave2");
				priority = (MusicPriority)4;
			}
		}
	}
}
