using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Backgrounds.ShadowBiome;

public class UltraniumUGBGStyle : ModUgBgStyle
{
	public override bool ChooseBgStyle()
	{
		return Main.LocalPlayer.GetModPlayer<UltraniumPlayer>().ZoneShadow;
	}

	public override void FillTextureArray(int[] textureSlots)
	{
		textureSlots[0] = ((ModUgBgStyle)this).mod.GetBackgroundSlot("Backgrounds/ShadowBiome/ShadowUG0");
		textureSlots[1] = ((ModUgBgStyle)this).mod.GetBackgroundSlot("Backgrounds/ShadowBiome/ShadowUG1");
		textureSlots[2] = ((ModUgBgStyle)this).mod.GetBackgroundSlot("Backgrounds/ShadowBiome/ShadowUG2");
		textureSlots[3] = ((ModUgBgStyle)this).mod.GetBackgroundSlot("Backgrounds/ShadowBiome/ShadowUG3");
		textureSlots[4] = ((ModUgBgStyle)this).mod.GetBackgroundSlot("Backgrounds/ShadowBiome/ShadowUG4");
	}
}
