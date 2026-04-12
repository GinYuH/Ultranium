using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Misc;

public class EldritchShingles : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = false;
		AddMapEntry(new Color(53, 59, 74), (LocalizedText)null);
		base.DustType = -1;
		//base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("EldritchShingleItem").Type;
	}
}
