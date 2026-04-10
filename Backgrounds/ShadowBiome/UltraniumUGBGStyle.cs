using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Backgrounds.ShadowBiome;

public class UltraniumUGBGStyle : ModUndergroundBackgroundStyle
{
	public override bool ChooseBgStyle()/* tModPorter Note: Removed. Create a ModBiome (or ModSceneEffect) class and override UndergroundBackgroundStyle property to return this object through Mod/ModContent.Find, then move this code into IsBiomeActive (or IsSceneEffectActive) */
	{
		return Main.LocalPlayer.GetModPlayer<UltraniumPlayer>().ZoneShadow;
	}

	public override void FillTextureArray(int[] textureSlots)
	{
		textureSlots[0] = BackgroundTextureLoader.GetBackgroundSlot(((ModUndergroundBackgroundStyle)this).Mod, "Backgrounds/ShadowBiome/ShadowUG0");
		textureSlots[1] = BackgroundTextureLoader.GetBackgroundSlot(((ModUndergroundBackgroundStyle)this).Mod, "Backgrounds/ShadowBiome/ShadowUG1");
		textureSlots[2] = BackgroundTextureLoader.GetBackgroundSlot(((ModUndergroundBackgroundStyle)this).Mod, "Backgrounds/ShadowBiome/ShadowUG2");
		textureSlots[3] = BackgroundTextureLoader.GetBackgroundSlot(((ModUndergroundBackgroundStyle)this).Mod, "Backgrounds/ShadowBiome/ShadowUG3");
		textureSlots[4] = BackgroundTextureLoader.GetBackgroundSlot(((ModUndergroundBackgroundStyle)this).Mod, "Backgrounds/ShadowBiome/ShadowUG4");
	}
}
