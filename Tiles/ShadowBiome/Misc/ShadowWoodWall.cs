using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Misc;

public class ShadowWoodWall : ModWall
{
	public override void SetStaticDefaults()
	{
		Main.wallHouse[((ModWall)this).Type] = true;
		//base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ((ModWall)this).Mod.Find<ModItem>("ShadowWoodWallItem").Type;
		((ModWall)this).AddMapEntry(new Color(8, 6, 15), (LocalizedText)null);
	}
}
