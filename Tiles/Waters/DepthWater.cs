using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Waters;

public class DepthWater : ModWaterStyle
{
	public override bool ChooseWaterStyle()
	{
		return Main.LocalPlayer.GetModPlayer<UltraniumPlayer>().ZoneDepth;
	}

	public override int ChooseWaterfallStyle()
	{
		return ((ModWaterStyle)this).mod.GetWaterfallStyleSlot("DepthWaterfall");
	}

	public override int GetSplashDust()
	{
		return ((ModWaterStyle)this).mod.DustType("ShadowDustPurple");
	}

	public override int GetDropletGore()
	{
		return ((ModWaterStyle)this).mod.GetGoreSlot("Tiles/Waters/DepthWaterDroplet");
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
