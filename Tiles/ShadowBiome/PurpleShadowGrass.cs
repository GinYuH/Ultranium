using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Dusts;
using Ultranium.Tiles.Ambient.Purple;
using Ultranium.Tiles.ShadowBiome.Depths;
using Ultranium.Tiles.ShadowBiome.Trees;

namespace Ultranium.Tiles.ShadowBiome;

public class PurpleShadowGrass : ModTile
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
		AddMapEntry(new Color(58, 11, 67), (LocalizedText)null);
        DustType = ModContent.DustType<ShadowSoilDust>();
        MineResist = 1f;
		MinPick = 1;
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j);
		Tile tileSafely2 = Framing.GetTileSafely(i, j + 1);
		Framing.GetTileSafely(i, j - 1);
		if (Utils.NextBool(WorldGen.genRand, 12) && !tileSafely2.HasTile && !(tileSafely2.LiquidType == LiquidID.Lava) && !tileSafely.BottomSlope)
		{
			tileSafely2.TileType = (ushort)ModContent.TileType<PurpleGlowShroomVine>();
			tileSafely2.HasTile = true;
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
		if (Framing.GetTileSafely(i, j - 1).TileType == TileID.Dirt && Framing.GetTileSafely(i, j - 2).TileType == TileID.Dirt && Main.rand.Next(8) == 0)
		{
			if (Main.rand.Next(5) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<PurpleGlowShroomHuge>());
				NetMessage.SendObjectPlacement(-1, i - 1, j - 1, ModContent.TileType<PurpleGlowShroomHuge>(), 0, 0, -1, -1);
			}
			if (Main.rand.Next(5) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<PurpleGlowShroomHuge2>());
				NetMessage.SendObjectPlacement(-1, i - 1, j - 1, ModContent.TileType<PurpleGlowShroomHuge2>(), 0, 0, -1, -1);
			}
			if (Main.rand.Next(2) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<PurpleGlowShroomTall>());
				NetMessage.SendObjectPlacement(-1, i - 1, j - 1, ModContent.TileType<PurpleGlowShroomTall>(), 0, 0, -1, -1);
			}
			if (Main.rand.Next(2) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<PurpleGlowShroom>());
				NetMessage.SendObjectPlacement(-1, i - 1, j - 1, ModContent.TileType<PurpleGlowShroom>(), 0, 0, -1, -1);
			}
		}
	}
}
