using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Depths;

public class AbyssRock : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileMerge[Type][ModContent.TileType<DarkStone>()] = true;
		Main.tileMerge[Type][ModContent.TileType<DepthGlowstone>()] = true;
		Main.tileMerge[Type][ModContent.TileType<ShadowGrass>()] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = true;
		Main.tileMergeDirt[Type] = true;
		Main.tileSolid[Type] = true;
		AddMapEntry(new Color(51, 49, 95), (LocalizedText)null);
		base.DustType = Mod.Find<ModDust>("ShadowDustPurple").Type;
		base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("AbyssRockItem").Type;
		base.HitSound = 21;
		//base.soundStyle/* tModPorter Note: Removed. Integrate into HitSound */ = 1;
		base.MineResist = 5f;
		base.MinPick = 200;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}
}
