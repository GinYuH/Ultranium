using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
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
		SetModTree((ModTree)(object)new ShadowTree())/* tModPorter Note: Removed. Assign GrowsOnTileId to this tile type in ModTree.SetStaticDefaults instead */;
		base.DustType = Mod.Find<ModDust>("ShadowSoilDust").Type;
		base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("PurpleShadowGrassItem").Type;
		base.MineResist = 1f;
		base.MinPick = 1;
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
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
		if (Framing.GetTileSafely(i, j - 1).TileType == 0 && Framing.GetTileSafely(i, j - 2).TileType == 0 && Main.rand.Next(8) == 0)
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
