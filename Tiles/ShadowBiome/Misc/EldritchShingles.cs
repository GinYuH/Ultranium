using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Misc;

public class EldritchShingles : ModTile
{
	public override void SetDefaults()
	{
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = false;
		((ModTile)this).AddMapEntry(new Color(53, 59, 74), (LocalizedText)null);
		base.dustType = -1;
		base.drop = ((ModTile)this).mod.ItemType("EldritchShingleItem");
	}
}
