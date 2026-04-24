using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.GameContent.Shaders;
using Terraria.GameContent.UI;
using Terraria.Graphics;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
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
using Ultranium.Projectiles.Guardians.Nature;
using Ultranium.ShadowEvent;

namespace Ultranium;

internal class Ultranium : Mod
{
	public static int GlowShroomCurrencyID;

	public static float seizureAmount;

	public static int seizureTimer;

	public static ModKeybind SpecialKey;

	public static Mod mod => ModLoader.GetMod("Ultranium");

    public static LocalizedText GetText(string key)
    {
        return Language.GetText("Mods.Ultranium." + key);
    }
    public static string GetTextValue(string key)
    {
        return Language.GetText("Mods.Ultranium." + key).Value;
    }


    public override void PostSetupContent()
	{
		// PORTING NOTE: Boss Bar is no longer real
		/*Mod val = ModLoader.GetMod("FKBossHealthBar");
		if (val != null)
		{
			val.Call(new object[1] { "hbStart" });
			val.Call(new object[5]
			{
				"hbSetTexture",
				Mod.GetTexture("UI/ErebusBarLeft"),
				Mod.GetTexture("UI/ErebusBarMiddle"),
				Mod.GetTexture("UI/ErebusBarRight"),
				Mod.GetTexture("UI/HBFill")
			});
			val.Call(new object[3] { "hbSetMidBarOffset", -32, 12 });
			val.Call(new object[3] { "hbSetBossHeadCentre", 80, 32 });
			val.Call(new object[2] { "hbSetFillDecoOffsetSmall", 20 });
			val.Call(new object[2]
			{
				"hbFinishSingle",
				Mod.Find<ModNPC>("ErebusHead").Type
			});
		}*/
		if (ModLoader.TryGetMod("BossChecklist", out Mod val2))
        {
            val2.Call(
                "LogBoss",
                mod,
                "ZephyrSquid",
                3.5f,
                () => UltraniumWorld.downedSquid,
                ModContent.NPCType<ZephyrSquid>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<CoralBait>(),
                    ["spawnInfo"] = GetLocalization("NPCs.ZephyrSquid.SpawnInfo"),
					["customPortrait"] = DrawBoss("Ultranium/BossTextures/Squid"),
                }
            );
            val2.Call(
                "LogBoss",
                mod,
                "Glacieron",
                5.5f,
                () => UltraniumWorld.downedDragon,
                ModContent.NPCType<IceDragon>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<IceFood>(),
                    ["spawnInfo"] = GetLocalization("NPCs.IceDragon.SpawnInfo"),
                    ["customPortrait"] = DrawBoss("Ultranium/BossTextures/IceDragon"),
                }
            );
            val2.Call(
                "LogBoss",
                mod,
                "Dread",
                7.5f,
                () => UltraniumWorld.downedDread,
                ModContent.NPCType<DreadBoss>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<DreadBeacon>(),
                    ["spawnInfo"] = GetLocalization("NPCs.DreadBoss.SpawnInfo"),
                    ["customPortrait"] = DrawBoss("Ultranium/BossTextures/Dread"),
                }
            );
            val2.Call(
                "LogBoss",
                mod,
                "Xenanis",
                12.5f,
                () => UltraniumWorld.downedXenanis,
                ModContent.NPCType<Xenanis>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<EtherealLantern>(),
                    ["spawnInfo"] = GetLocalization("NPCs.Xenanis.SpawnInfo"),
                    ["customPortrait"] = DrawBoss("Ultranium/BossTextures/Xenanis"),
                }
            );
            val2.Call(
                "LogBoss",
                mod,
                "Ultrum",
                18.1f,
                () => UltraniumWorld.downedUltrum,
                ModContent.NPCType<NPCs.Ultrum.Ultrum>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<UltrumSummon>(),
                    ["spawnInfo"] = GetLocalization("NPCs.Ultrum.SpawnInfo"),
                    ["customPortrait"] = DrawBoss("Ultranium/BossTextures/Ultrum"),
                }
            );
            val2.Call(
                "LogBoss",
                mod,
                "Ignodium",
                18.2f,
                () => UltraniumWorld.downedIgnodium,
                ModContent.NPCType<Ignodium>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<NetherBeacon>(),
                    ["spawnInfo"] = GetLocalization("NPCs.Ignodium.SpawnInfo"),
                    ["customPortrait"] = DrawBoss("Ultranium/BossTextures/Ignodium"),
                }
            );
            val2.Call(
                "LogBoss",
                mod,
                "TrueDread",
                18.3f,
                () => UltraniumWorld.downedTrueDread,
                ModContent.NPCType<TrueDread>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<DreadBeacon>(),
                    ["spawnInfo"] = GetLocalization("NPCs.TrueDread.SpawnInfo"),
                    ["customPortrait"] = DrawBoss("Ultranium/BossTextures/TrueDread", 0.8f),
                }
            );
            val2.Call(
                "LogBoss",
                mod,
                "Erebus",
                18.41f,
                () => UltraniumWorld.downedErebus,
                ModContent.NPCType<ErebusHead>(),
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<ErebusFood>(),
                    ["spawnInfo"] = GetLocalization("NPCs.ErebusHead.SpawnInfo"),
                    ["customPortrait"] = DrawBoss("Ultranium/BossTextures/DarkWorm", 0.6f),
                }
            );
            val2.Call(
                "LogEvent",
                mod,
                "AbyssalArmageddon",
                18.4f,
                () => UltraniumWorld.downedShadowEvent,
                new List<int>() { 
					ModContent.NPCType<AbyssalWraith>(),
                    ModContent.NPCType<Scp2521>(),
                    ModContent.NPCType<ShadeSpirit>(),
                    ModContent.NPCType<Phantom>(),
                    ModContent.NPCType<ShadeMass>(),
                    ModContent.NPCType<AbyssalCultist>(),
                    ModContent.NPCType<FlayerWraith>(),
                    ModContent.NPCType<Warden>(),
                    ModContent.NPCType<MindFlayer>(),
                    ModContent.NPCType<MotherPhantom>() },
                new Dictionary<string, object>()
                {
                    ["spawnItems"] = ModContent.ItemType<DarkResonator>(),
                    ["spawnInfo"] = GetLocalization("Event.SpawnInfo"),
                    ["displayName"] = GetLocalization("Event.Name"),
                    ["customPortrait"] = DrawBoss("Ultranium/BossTextures/ShadowEventEnemies", 0.6f),
                    ["overrideHeadTextures"] = DrawBoss("Ultranium/BossTextures/ShadowEventIcon")
                }
            );
		}
    }
    public static Action<SpriteBatch, Rectangle, Color> DrawBoss(string path, float scale = 1)
    {
        return (spriteBatch, rect, color) =>
        {
            Texture2D texture = ModContent.Request<Texture2D>(path).Value;
            Vector2 centered = new Vector2(rect.X + (rect.Width / 2) - (texture.Width / 2) * scale, rect.Y + (rect.Height / 2) - (texture.Height / 2) * scale);
            spriteBatch.Draw(texture, centered, null, color, 0, Vector2.Zero, scale, 0, 0);
        };
    }

    // These only require the boss' name, so a simple .Call("erebus") works
    // No "BossDowned" or any such things required
    public override object Call(params object[] args)
	{
		if (args.Length < 1)
		{
			throw new ArgumentException("No boss name specified");
		}
		string text = args[0] as string;
        text = text.ToLower();
		return text switch
		{
			"squid" => UltraniumWorld.downedSquid,
            "zephyr" => UltraniumWorld.downedSquid,
            "zephyrsquid" => UltraniumWorld.downedSquid,
            "dread" => UltraniumWorld.downedDread, 
			"xenanis" => UltraniumWorld.downedXenanis, 
			"ultrum" => UltraniumWorld.downedUltrum, 
			"ignodium" => UltraniumWorld.downedIgnodium, 
			"truedread" => UltraniumWorld.downedTrueDread,
            "absolutedread" => UltraniumWorld.downedTrueDread,
            "shadowevent" => UltraniumWorld.downedShadowEvent, 
			"erebus" => UltraniumWorld.downedErebus, 
			"aldin" => UltraniumWorld.downedAldin,
            "glacieron" => UltraniumWorld.downedDragon,
            "dragon" => UltraniumWorld.downedDragon,
            _ => throw new ArgumentException("Invalid boss name:" + text), 
		};
	}

	public override void Load()
	{
		GlowShroomCurrencyID = CustomCurrencyManager.RegisterCurrency(new GlowShroomData(mod.Find<ModItem>("GlowShroomItem").Type, 999L));
		GameShaders.Armor.BindShader(mod.Find<ModItem>("EtherealDye").Type, new ArmorShaderData(Main.PixelShaderRef, "ArmorHades")).UseColor(0.5f, 0.9f, 0.9f).UseSecondaryColor(0.6f, 0.35f, 0.9f);
		GameShaders.Armor.BindShader(mod.Find<ModItem>("EldritchDye").Type, new ArmorShaderData(Main.PixelShaderRef, "ArmorHades")).UseColor(0.1f, 0.5f, 0.42f).UseSecondaryColor(0.2f, 0.22f, 0.6f);
		GameShaders.Armor.BindShader(mod.Find<ModItem>("IceDragonDye").Type, new ArmorShaderData(Main.PixelShaderRef, "ArmorLivingFlame")).UseColor(2f, 2f, 2f).UseSecondaryColor(1.2f, 1.9f, 2f);
		GameShaders.Armor.BindShader(mod.Find<ModItem>("AuroraDye").Type, new ArmorShaderData(Main.PixelShaderRef, "ArmorBrightnessGradient")).UseColor(0.95f, 0.15f, 0.85f).UseSecondaryColor(0.1f, 1f, 0.62f);
		GameShaders.Armor.BindShader(mod.Find<ModItem>("DepthsDye").Type, new ArmorShaderData(Main.PixelShaderRef, "ArmorTwilight")).UseImage("Images/Misc/Perlin").UseColor(0f, 1f, 0.45f);
		GameShaders.Armor.BindShader(mod.Find<ModItem>("DreadDye").Type, new ArmorShaderData(Main.PixelShaderRef, "ArmorPhase")).UseImage("Images/Misc/Perlin").UseColor(0.55f, 0f, 0f);
		GameShaders.Armor.BindShader(mod.Find<ModItem>("UltrumDye").Type, new ArmorShaderData(Main.PixelShaderRef, "ArmorFlow")).UseColor(0.84f, 2.21f, 0f).UseSecondaryColor(0f, 1.72f, 0.98f);
		GameShaders.Armor.BindShader(mod.Find<ModItem>("IgnodiumDye").Type, new ArmorShaderData(Main.PixelShaderRef, "ArmorFlow")).UseColor(2.55f, 1.61f, 0f).UseSecondaryColor(1f, 1.3f, 1.5f);
		if (!Main.dedServ)
		{
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/ShadowBiome"), mod.Find<ModItem>("ShadowMusicBox").Type, mod.Find<ModTile>("ShadowMusicBoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/DarkDepths"), mod.Find<ModItem>("DepthMusicBox").Type, mod.Find<ModTile>("DepthMusicBoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/ZephyrSquid"), mod.Find<ModItem>("ZephyrMusicBox").Type, mod.Find<ModTile>("ZephyrMusicBoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/IceDragon"), mod.Find<ModItem>("IceMusicBox").Type, mod.Find<ModTile>("IceMusicBoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/Dread"), mod.Find<ModItem>("DreadMusicBox").Type, mod.Find<ModTile>("DreadMusicBoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/Xenanis"), mod.Find<ModItem>("EtherealMusicBox").Type, mod.Find<ModTile>("EtherealMusicBoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/GuardiansPhase1"), mod.Find<ModItem>("GuardianPhase1Box").Type, mod.Find<ModTile>("GuardianPhase1BoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/GuardiansPhase2"), mod.Find<ModItem>("GuardianPhase2Box").Type, mod.Find<ModTile>("GuardianPhase2BoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/ShadowEventWave1"), mod.Find<ModItem>("ShadowEventBox").Type, mod.Find<ModTile>("ShadowEventBoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/ShadowEventWave2"), mod.Find<ModItem>("ShadowEventBox2").Type, mod.Find<ModTile>("ShadowEventBoxTile2").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/RealDread"), mod.Find<ModItem>("TrueDreadBox").Type, mod.Find<ModTile>("TrueDreadBoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/MindFlayer"), mod.Find<ModItem>("FlayerMusicBox").Type, mod.Find<ModTile>("FlayerMusicBoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/ErebusTheme"), mod.Find<ModItem>("ErebusMusicBox").Type, mod.Find<ModTile>("ErebusMusicBoxTile").Type);
			MusicLoader.AddMusicBox(mod, MusicLoader.GetMusicSlot(mod, "Sounds/Music/Aldin"), mod.Find<ModItem>("AldinMusicBox").Type, mod.Find<ModTile>("AldinMusicBoxTile").Type);
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
			if (Main.netMode != NetmodeID.Server)
			{
				Asset<Effect> @ref = mod.Assets.Request<Effect>("Effects/ShockwaveEffect");
                Filters.Scene["Shockwave"] = new Filter(new ScreenShaderData(@ref, "Shockwave"), EffectPriority.VeryHigh);
				Filters.Scene["Shockwave"].Load();
			}
			SpecialKey = KeybindLoader.RegisterKeybind(mod, "Special Ability", "E");
		}
	}
}
