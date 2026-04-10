using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Waters;

public class DepthWater : ModWaterStyle
{

	public override int ChooseWaterfallStyle()
    {
        return ModContent.GetInstance<DepthWaterfall>().Slot;
    }

	public override int GetSplashDust()
	{
		return Mod.Find<ModDust>("ShadowDustPurple").Type;
	}

	public override int GetDropletGore()
	{
		return Mod.Find<ModGore>("DepthWaterDroplet").Type;
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
