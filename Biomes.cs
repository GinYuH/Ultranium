using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;
using Ultranium.Backgrounds.ShadowBiome;
using Ultranium.NPCs.ShadowWorm;
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
}
