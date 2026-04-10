using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Depths;

public class AbyssRock : ModTile
{
	public override void SetDefaults()
	{
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<DarkStone>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<DepthGlowstone>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<ShadowGrass>()] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		Main.tileSolid[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(51, 49, 95), (LocalizedText)null);
		base.dustType = ((ModTile)this).mod.DustType("ShadowDustPurple");
		base.drop = ((ModTile)this).mod.ItemType("AbyssRockItem");
		base.soundType = 21;
		base.soundStyle = 1;
		base.mineResist = 5f;
		base.minPick = 200;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}
}
