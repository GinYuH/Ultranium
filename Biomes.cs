using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Ultranium.Backgrounds.ShadowBiome;
using Ultranium.NPCs.Aldin;
using Ultranium.NPCs.Dread;
using Ultranium.NPCs.Ethereal;
using Ultranium.NPCs.IceDragon;
using Ultranium.NPCs.Ignodium;
using Ultranium.NPCs.ShadowWorm;
using Ultranium.NPCs.TrueDread;
using Ultranium.NPCs.Ultrum;
using Ultranium.ShadowEvent;
using Ultranium.Tiles.Waters;

namespace Ultranium
{
    public class ShadowBiome : ModBiome
    {
        public override ModWaterStyle WaterStyle => ModContent.GetInstance<ShadowWater>();
        public override ModSurfaceBackgroundStyle SurfaceBackgroundStyle => ModContent.GetInstance<ShadowSurface>();

        public override ModUndergroundBackgroundStyle UndergroundBackgroundStyle => ModContent.GetInstance<UltraniumUGBGStyle>();

        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ShadowBiome");
        public override string BestiaryIcon => "Ultranium/Icon";
        public override string BackgroundPath => "Ultranium/Icon";
        public override string MapBackground => "Ultranium/Icon";

        public override bool IsBiomeActive(Player player)
        {
            bool isA = UltraniumWorld.ShadowTiles > 100 && !player.GetModPlayer<UltraniumPlayer>().ZoneDepth;
            player.GetModPlayer<UltraniumPlayer>().ZoneShadow = isA;
            return isA;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            player.ManageSpecialBiomeVisuals("Ultranium:ShadowBiome", isActive);
        }
    }
    public class DepthsBiome : ModBiome
    {
        public override ModWaterStyle WaterStyle => ModContent.GetInstance<DepthWater>();
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/DarkDepths");
        public override string BestiaryIcon => "Ultranium/Icon";
        public override string BackgroundPath => "Ultranium/Icon";
        public override string MapBackground => "Ultranium/Icon";

        public override bool IsBiomeActive(Player player)
        {
            return player.GetModPlayer<UltraniumPlayer>().ZoneDepth;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            Lighting.GlobalBrightness = 0.65f;
        }
    }

    public class ShadowEventOne : ModSceneEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ShadowEventWave1");
        public override bool IsSceneEffectActive(Player player)
        {
            return ShadowEventWorld.ShadowEventActive && !ShadowEventWorld.Phase2;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<ErebusHead>()))
                player.ManageSpecialBiomeVisuals("Ultranium:ShadowEvent1", isActive);
        }
    }

    public class ShadowEventTwo : ModSceneEffect
    {
        public override SceneEffectPriority Priority => SceneEffectPriority.Environment;
        public override int Music => MusicLoader.GetMusicSlot(Mod, "Sounds/Music/ShadowEventWave2");
        public override bool IsSceneEffectActive(Player player)
        {
            return ShadowEventWorld.ShadowEventActive && ShadowEventWorld.Phase2;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            if (!NPC.AnyNPCs(ModContent.NPCType<ErebusHead>()))
                player.ManageSpecialBiomeVisuals("Ultranium:ShadowEvent2", isActive);
        }
    }

    public class UltraniumBossScene : ModSceneEffect
    {
        public override bool IsSceneEffectActive(Player player)
        {
            return true;
        }

        public override void SpecialVisuals(Player player, bool isActive)
        {
            bool flag = NPC.AnyNPCs(ModContent.NPCType<IceDragon>()) && IceDragon.BlizzardEffect;
            player.ManageSpecialBiomeVisuals("Blizzard", flag, default(Vector2));
            bool flag2 = NPC.AnyNPCs(ModContent.NPCType<DreadBoss>());
            bool flag3 = NPC.AnyNPCs(ModContent.NPCType<DreadBossP2>());
            bool flag4 = NPC.AnyNPCs(ModContent.NPCType<FakeDread>());
            player.ManageSpecialBiomeVisuals("Ultranium:DreadBoss", flag2 || flag3 || flag4, default(Vector2));
            bool flag5 = NPC.AnyNPCs(ModContent.NPCType<Xenanis>());
            player.ManageSpecialBiomeVisuals("Ultranium:EtherealBoss", flag5, default(Vector2));
            bool flag6 = NPC.AnyNPCs(ModContent.NPCType<Ultrum>());
            player.ManageSpecialBiomeVisuals("Ultranium:Ultrum", flag6, default(Vector2));
            bool flag7 = NPC.AnyNPCs(ModContent.NPCType<TrueDread>());
            player.ManageSpecialBiomeVisuals("Ultranium:TrueDread", flag7, default(Vector2));
            bool flag8 = NPC.AnyNPCs(ModContent.NPCType<Ignodium>());
            player.ManageSpecialBiomeVisuals("Ultranium:Ignodium", flag8, default(Vector2));
            bool flag11 = NPC.AnyNPCs(ModContent.NPCType<ErebusHead>());
            player.ManageSpecialBiomeVisuals("Ultranium:Erebus", flag11, default(Vector2));
            bool flag12 = NPC.AnyNPCs(ModContent.NPCType<Aldin>());
            player.ManageSpecialBiomeVisuals("Ultranium:Aldin", flag12, default(Vector2));
        }
    }
}
