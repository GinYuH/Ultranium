using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Waters;

public class ShadowWater : ModWaterStyle
{

	public override int ChooseWaterfallStyle()
	{
		return ModContent.GetInstance<ShadowWaterfall>().Slot;
    }

	public override int GetSplashDust()
	{
		return 89;
	}

	public override int GetDropletGore()
	{
		return Mod.Find<ModGore>("ShadowWaterDroplet").Type;
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
