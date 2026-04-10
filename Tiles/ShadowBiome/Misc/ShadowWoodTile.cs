using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Misc;

public class ShadowWoodTile : ModTile
{
	public override void SetDefaults()
	{
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		Main.tileBlendAll[((ModTile)this).Type] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(19, 17, 24), (LocalizedText)null);
		base.drop = ((ModTile)this).mod.ItemType("ShadowWood");
		base.dustType = ((ModTile)this).mod.DustType("ShadowWoodDust");
	}

	public override bool CanExplode(int i, int j)
	{
		return true;
	}
}
