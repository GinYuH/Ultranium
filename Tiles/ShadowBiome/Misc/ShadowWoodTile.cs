using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Dusts;

namespace Ultranium.Tiles.ShadowBiome.Misc;

public class ShadowWoodTile : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolid[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileBlendAll[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = true;
		AddMapEntry(new Color(19, 17, 24), (LocalizedText)null);
        DustType = ModContent.DustType<ShadowDustPurple>();
    }

	public override bool CanExplode(int i, int j)
	{
		return true;
	}
}
