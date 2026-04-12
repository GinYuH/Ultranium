using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

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
		//base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("ShadowWood").Type;
		base.DustType = Mod.Find<ModDust>("ShadowWoodDust").Type;
	}

	public override bool CanExplode(int i, int j)
	{
		return true;
	}
}
