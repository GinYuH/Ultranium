using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Misc;

public class ShadowWoodTile : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		Main.tileBlendAll[((ModTile)this).Type] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(19, 17, 24), (LocalizedText)null);
		base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = ((ModTile)this).Mod.Find<ModItem>("ShadowWood").Type;
		base.DustType = ((ModTile)this).Mod.Find<ModDust>("ShadowWoodDust").Type;
	}

	public override bool CanExplode(int i, int j)
	{
		return true;
	}
}
