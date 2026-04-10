using System;

namespace Ultranium;

public class GenConditions
{
	public int[] tiles;

	public int[] walls;

	public bool orderTiles;

	public bool orderWalls;

	public bool slope;

	public Func<int, int, int, int, bool> CanPlace;

	public Func<int, int, int, int, bool> CanPlaceWall;

	public int GetTile(int index)
	{
		if (tiles != null && tiles.Length > index)
		{
			return tiles[index];
		}
		return -1;
	}

	public int GetWall(int index)
	{
		if (walls != null && walls.Length > index)
		{
			return walls[index];
		}
		return -1;
	}
}
