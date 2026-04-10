using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.Shrine;

public class UltrumRock : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileMergeDirt[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		AddMapEntry(new Color(49, 37, 34), (LocalizedText)null);
		base.DustType = Mod.Find<ModDust>("UltraniumDust").Type;
		base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("UltrumRockItem").Type;
		base.HitSound = 21;
		//base.soundStyle/* tModPorter Note: Removed. Integrate into HitSound */ = 1;
		base.MinPick = 1;
	}
}
