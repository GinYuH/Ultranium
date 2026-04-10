using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles;

public class AuroraOre : ModTile
{
	public override void SetDefaults()
	{
		Main.tileMerge[((ModTile)this).Type][161] = true;
		Main.tileMerge[((ModTile)this).Type][147] = true;
		TileID.Sets.Ore[((ModTile)this).Type] = true;
		Main.tileSpelunker[((ModTile)this).Type] = true;
		Main.tileValue[((ModTile)this).Type] = 410;
		Main.tileSolid[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(240, 15, 207), (LocalizedText)null);
		base.dustType = 86;
		base.drop = ((ModTile)this).mod.ItemType("AuroraOreItem");
		base.mineResist = 2.5f;
		base.minPick = 45;
	}
}
