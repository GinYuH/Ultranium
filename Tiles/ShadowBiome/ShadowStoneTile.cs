using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Tiles.Ambient;
using Ultranium.Tiles.ShadowBiome.Depths;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowStoneTile : ModTile
{
	public override void SetDefaults()
	{
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<ShadowOreTile>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<DarkStone>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<DepthGlowstone>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<AbyssRock>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<ShadowGrass>()] = true;
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(37, 41, 58), (LocalizedText)null);
		base.dustType = ((ModTile)this).mod.DustType("ShadowStoneDust");
		base.drop = ((ModTile)this).mod.ItemType("ShadowStone");
		base.soundType = 21;
		base.soundStyle = 1;
		base.mineResist = 1f;
	}

	public override void RandomUpdate(int i, int j)
	{
		if (Framing.GetTileSafely(i, j - 1).type == 0 && Framing.GetTileSafely(i, j - 2).type == 0 && Main.rand.Next(10) == 0 && Main.rand.Next(10) == 0)
		{
			WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroom>());
			NetMessage.SendObjectPlacment(-1, i - 1, j - 1, ModContent.TileType<GlowShroom>(), 0, 0, -1, -1);
		}
	}
}
