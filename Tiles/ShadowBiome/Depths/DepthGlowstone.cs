using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Ultranium.Tiles.ShadowBiome.Depths;

public class DepthGlowstone : ModTile
{
	public override void SetDefaults()
	{
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<ShadowGrass>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<DarkStone>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<DepthGlowstone>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<AbyssRock>()] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		Main.tileSolid[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(19, 121, 95), (LocalizedText)null);
		base.dustType = 89;
		base.drop = ((ModTile)this).mod.ItemType("DepthGlowstoneItem");
		base.soundType = 21;
		base.soundStyle = 1;
		base.mineResist = 5f;
		base.minPick = 200;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.1f;
		g = 0.5f;
		b = 0.45f;
	}
}
