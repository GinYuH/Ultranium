using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Waters;

public class ShadowWater : ModWaterStyle
{
	public override bool ChooseWaterStyle()
	{
		return Main.LocalPlayer.GetModPlayer<UltraniumPlayer>().ZoneShadow;
	}

	public override int ChooseWaterfallStyle()
	{
		return ((ModWaterStyle)this).Mod.GetWaterfallStyleSlot("ShadowWaterfall");
	}

	public override int GetSplashDust()
	{
		return 89;
	}

	public override int GetDropletGore()
	{
		return ((ModWaterStyle)this).Mod.GetGoreSlot("Tiles/Waters/ShadowWaterDroplet");
	}

	public override void LightColorMultiplier(ref float r, ref float g, ref float b)
	{
		r = 1f;
		g = 1f;
		b = 1f;
	}

	public override Color BiomeHairColor()
	{
		return Color.Purple;
	}
}
