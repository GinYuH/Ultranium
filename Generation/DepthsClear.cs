using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Ultranium.Tiles.Ambient;
using Ultranium.Tiles.ShadowBiome;
using Ultranium.Tiles.ShadowBiome.Depths;

namespace Ultranium.Generation;

public class DepthsClear
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
			[new Color(255, 255, 255)] = -1,
			[Color.Black] = -2
		};
		Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
		{
			[new Color(37, 41, 58)] = ModContent.WallType<DarkStoneWall>(),
			[Color.Black] = -2
		};
		TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(ModContent.GetTexture("Ultranium/Generation/DepthsClear"), colorToTile, ModContent.GetTexture("Ultranium/Generation/DepthsClear"), colorToWall, ModContent.GetTexture("Ultranium/Generation/DepthsClear"));
		texGenerator.Generate(x - texGenerator.width / 2, y - texGenerator.height / 2, silent: true, sync: true);
	}
}
