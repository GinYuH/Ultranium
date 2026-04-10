using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ObjectData;

namespace Ultranium;

public class BaseTile
{
	public static Vector2 FindTopLeft(int x, int y)
	{
		Tile tile = Main.tile[x, y];
		if (tile == null)
		{
			return new Vector2(x, y);
		}
		TileObjectData tileData = TileObjectData.GetTileData(tile.TileType, 0);
		x -= tile.TileFrameX / 18 % tileData.Width;
		y -= tile.TileFrameY / 18 % tileData.Height;
		return new Vector2(x, y);
	}
}
