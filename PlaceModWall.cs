using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.WorldBuilding;

namespace Ultranium;

public class PlaceModWall : GenAction
{
	public ushort _type;

	public bool _neighbors;

	public Func<int, int, Tile, bool> _canReplace;

	public PlaceModWall(int type, bool neighbors = true)
	{
		_type = (ushort)type;
		_neighbors = neighbors;
	}

	public PlaceModWall ExtraParams(Func<int, int, Tile, bool> canReplace)
	{
		_canReplace = canReplace;
		return this;
	}

	public override bool Apply(Point origin, int x, int y, params object[] args)
	{
		if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
		{
			return false;
		}
		if (_canReplace == null || (_canReplace != null && _canReplace(x, y, GenBase._tiles[x, y])))
		{
			GenBase._tiles[x, y].WallType = _type;
			WorldGen.SquareWallFrame(x, y);
			if (_neighbors)
			{
				WorldGen.SquareWallFrame(x + 1, y);
				WorldGen.SquareWallFrame(x - 1, y);
				WorldGen.SquareWallFrame(x, y - 1);
				WorldGen.SquareWallFrame(x, y + 1);
			}
		}
		return UnitApply(origin, x, y, args);
	}
}
