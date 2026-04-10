using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles;

public class AuroraOre : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileMerge[((ModTile)this).Type][161] = true;
		Main.tileMerge[((ModTile)this).Type][147] = true;
		TileID.Sets.Ore[((ModTile)this).Type] = true;
		Main.tileSpelunker[((ModTile)this).Type] = true;
		Main.tileOreFinderPriority[((ModTile)this).Type] = 410;
		Main.tileSolid[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(240, 15, 207), (LocalizedText)null);
		base.DustType = 86;
		base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ((ModTile)this).Mod.Find<ModItem>("AuroraOreItem").Type;
		base.MineResist = 2.5f;
		base.MinPick = 45;
	}
}
