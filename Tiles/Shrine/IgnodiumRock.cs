using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Shrine;

public class IgnodiumRock : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(47, 62, 72), (LocalizedText)null);
		base.DustType = 6;
		base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ((ModTile)this).Mod.Find<ModItem>("IgnodiumRockItem").Type;
		base.HitSound = 21;
		base.soundStyle/* tModPorter Note: Removed. Integrate into HitSound */ = 1;
		base.MinPick = 1;
	}
}
