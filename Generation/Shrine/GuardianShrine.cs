using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Ultranium.Tiles.Furniture.Shrine;
using Ultranium.Tiles.Shrine;

namespace Ultranium.Generation.Shrine;

public class GuardianShrine
{
	public static void Generate(int x, int y)
	{
		GenTiles(x, y);
	}

	private static void GenTiles(int x, int y)
	{
		Dictionary<Color, int> colorToTile = new Dictionary<Color, int>
		{
			[new Color(49, 37, 34)] = ModContent.TileType<UltrumRock>(),
			[new Color(28, 36, 64)] = ModContent.TileType<IgnodiumRock>(),
			[new Color(255, 255, 255)] = -1,
			[Color.Black] = -2
		};
		Dictionary<Color, int> colorToWall = new Dictionary<Color, int>
		{
			[new Color(49, 37, 34)] = -2,
			[new Color(28, 36, 64)] = -2,
			[Color.Black] = -2
		};
		TexGen texGenerator = BaseWorldGenTex.GetTexGenerator(ModContent.GetTexture("Ultranium/Generation/Shrine/GuardianShrine"), colorToTile, ModContent.GetTexture("Ultranium/Generation/Shrine/GuardianShrine"), colorToWall);
		texGenerator.Generate(x - texGenerator.width / 2, y - texGenerator.height / 2, silent: true, sync: true);
		WorldGen.PlaceChest(x - 1, y + 28, (ushort)ModContent.TileType<ShrineChest>());
		WorldGen.PlaceObject(x - 6, y + 28, (ushort)ModContent.TileType<UltrumShrine>());
		WorldGen.PlaceObject(x + 5, y + 28, (ushort)ModContent.TileType<IgnodiumShrine>());
	}
}
