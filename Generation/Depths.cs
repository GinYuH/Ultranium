using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Ultranium.Tiles.Ambient;
using Ultranium.Tiles.Furniture;
using Ultranium.Tiles.ShadowBiome;
using Ultranium.Tiles.ShadowBiome.Depths;
using Ultranium.Tiles.ShadowBiome.Misc;

namespace Ultranium.Generation;

public class Depths
{
	public static void Generate(int x, int y)
	{
		GenTiles(x, y);
	}

	private static void GenTiles(int x, int y)
	{
		Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
		{
			[new Color(26, 0, 10)] = ModContent.TileType<ShadowStoneTile>(),
			[new Color(37, 41, 58)] = ModContent.TileType<DarkStone>(),
			[new Color(34, 166, 162)] = ModContent.TileType<ShadowGrass>(),
			[new Color(54, 19, 95)] = ModContent.TileType<AbyssRock>(),
			[new Color(19, 121, 95)] = ModContent.TileType<DepthGlowstone>(),
			[new Color(65, 74, 112)] = ModContent.TileType<DepthVines>(),
			[new Color(220, 222, 176)] = ModContent.TileType<EldritchShingles>(),
			[new Color(21, 90, 48)] = ModContent.TileType<DepthGlowstone>(),
			[new Color(58, 11, 67)] = ModContent.TileType<PurpleShadowGrass>(),
			[new Color(255, 255, 255)] = -1,
			[Color.Black] = -2
		};
		Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
		{
			[new Color(37, 41, 58)] = ModContent.WallType<DarkStoneWall>(),
			[new Color(58, 11, 67)] = ModContent.WallType<PurpleDarkstoneWall>(),
			[new Color(34, 166, 162)] = ModContent.WallType<ShadowStoneWall>(),
			[Color.Black] = -2
		};
		TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(ModContent.Request<Texture2D>("Ultranium/Generation/Depths").Value, colorToTile, ModContent.Request<Texture2D>("Ultranium/Generation/DepthsWalls").Value, colorToWall, ModContent.Request<Texture2D>("Ultranium/Generation/DepthsWater").Value);
		texGenerator.Generate(x - texGenerator.width / 2, y - texGenerator.height / 2, silent: true, sync: true);
		WorldGen.PlaceChest(x - 6, y + 229, (ushort)ModContent.TileType<ShadowChest>());
	}
}
