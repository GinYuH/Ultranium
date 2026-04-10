using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Tiles.Ambient;
using Ultranium.Tiles.ShadowBiome.Depths;
using Ultranium.Tiles.ShadowBiome.Trees;

namespace Ultranium.Tiles.ShadowBiome;

public class ShadowGrass : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileMergeDirt[Type] = true;
		Main.tileMerge[Type][ModContent.TileType<ShadowOreTile>()] = true;
		Main.tileMerge[Type][ModContent.TileType<DarkStone>()] = true;
		Main.tileMerge[Type][ModContent.TileType<DepthGlowstone>()] = true;
		Main.tileMerge[Type][ModContent.TileType<AbyssRock>()] = true;
		Main.tileMerge[Type][ModContent.TileType<ShadowStoneTile>()] = true;
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		AddMapEntry(new Color(19, 121, 95), (LocalizedText)null);
		base.DustType = Mod.Find<ModDust>("ShadowSoilDust").Type;
		base.MineResist = 1f;
		// = Mod.Find<ModTile>("ShadowTreeSapling").Type;
        base.MinPick = 1;
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j);
		Tile tileSafely2 = Framing.GetTileSafely(i, j + 1);
		Tile tileSafely3 = Framing.GetTileSafely(i, j - 1);
		if (Utils.NextBool(WorldGen.genRand, 12) && !tileSafely3.HasTile && !(tileSafely2.LiquidType == LiquidID.Lava) && !tileSafely.BottomSlope && !tileSafely.TopSlope && !tileSafely.IsHalfBlock && !tileSafely.TopSlope)
		{
			tileSafely3.TileType = (ushort)ModContent.TileType<ShadowFlora>();
			tileSafely3.HasTile = true;
			tileSafely3.TileFrameY = 0;
			tileSafely3.TileFrameX = (short)(WorldGen.genRand.Next(8) * 18);
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
		if (Utils.NextBool(WorldGen.genRand, 12) && !tileSafely2.HasTile && !(tileSafely2.LiquidType == LiquidID.Lava) && !tileSafely.BottomSlope)
		{
			tileSafely2.TileType = (ushort)ModContent.TileType<ShadowGrassVine>();
			tileSafely2.HasTile = true;
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
		if (Framing.GetTileSafely(i, j - 1).TileType == 0 && Framing.GetTileSafely(i, j - 2).TileType == 0 && Main.rand.Next(5) == 0)
		{
			if (Main.rand.Next(3) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<ShadowFlora>());
				NetMessage.SendObjectPlacement(-1, i - 1, j - 1, ModContent.TileType<ShadowFlora>(), 0, 0, -1, -1);
			}
			if (Main.rand.Next(3) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroom>());
				NetMessage.SendObjectPlacement(-1, i - 1, j - 1, ModContent.TileType<GlowShroom>(), 0, 0, -1, -1);
			}
		}
	}
}
