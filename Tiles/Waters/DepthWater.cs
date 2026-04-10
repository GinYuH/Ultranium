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
		return ((ModWaterStyle)this).Mod.GetWaterfallStyleSlot("DepthWaterfall");
	}

	public override int GetSplashDust()
	{
		return ((ModWaterStyle)this).Mod.Find<ModDust>("ShadowDustPurple").Type;
	}

	public override int GetDropletGore()
	{
		return ((ModWaterStyle)this).Mod.GetGoreSlot("Tiles/Waters/DepthWaterDroplet");
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
