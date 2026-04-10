using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Tiles.Ambient;
using Ultranium.Tiles.ShadowBiome.Depths;
using Ultranium.Tiles.ShadowBiome.Trees;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowGrass : ModTile
{
	public override void SetDefaults()
	{
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<ShadowOreTile>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<DarkStone>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<DepthGlowstone>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<AbyssRock>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<ShadowStoneTile>()] = true;
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(19, 121, 95), (LocalizedText)null);
		((ModTile)this).SetModTree((ModTree)(object)new ShadowTree());
		base.dustType = ((ModTile)this).mod.DustType("ShadowSoilDust");
		base.drop = ((ModTile)this).mod.ItemType("ShadowGrassItem");
		base.mineResist = 1f;
		base.minPick = 1;
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j);
		Tile tileSafely2 = Framing.GetTileSafely(i, j + 1);
		Tile tileSafely3 = Framing.GetTileSafely(i, j - 1);
		if (Utils.NextBool(WorldGen.genRand, 12) && !tileSafely3.active() && !tileSafely2.lava() && !tileSafely.bottomSlope() && !tileSafely.topSlope() && !tileSafely.halfBrick() && !tileSafely.topSlope())
		{
			tileSafely3.type = (ushort)ModContent.TileType<ShadowFlora>();
			tileSafely3.active(active: true);
			tileSafely3.frameY = 0;
			tileSafely3.frameX = (short)(WorldGen.genRand.Next(8) * 18);
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
		if (Utils.NextBool(WorldGen.genRand, 12) && !tileSafely2.active() && !tileSafely2.lava() && !tileSafely.bottomSlope())
		{
			tileSafely2.type = (ushort)ModContent.TileType<ShadowGrassVine>();
			tileSafely2.active(active: true);
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
		if (Framing.GetTileSafely(i, j - 1).type == 0 && Framing.GetTileSafely(i, j - 2).type == 0 && Main.rand.Next(5) == 0)
		{
			if (Main.rand.Next(3) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<ShadowFlora>());
				NetMessage.SendObjectPlacment(-1, i - 1, j - 1, ModContent.TileType<ShadowFlora>(), 0, 0, -1, -1);
			}
			if (Main.rand.Next(3) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroom>());
				NetMessage.SendObjectPlacment(-1, i - 1, j - 1, ModContent.TileType<GlowShroom>(), 0, 0, -1, -1);
			}
		}
	}

	public override int SaplingGrowthType(ref int style)
	{
		style = 0;
		return ((ModTile)this).mod.TileType("ShadowTreeSapling");
	}
}
