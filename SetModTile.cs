using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.WorldBuilding;

namespace Ultranium;

public class SetModTile : GenAction
{
	public ushort _type;

	public bool _doFraming;

	public bool _doNeighborFraming;

	public Func<int, int, Tile, bool> _canReplace;

	public SetModTile(ushort type, bool setSelfFrames = false, bool setNeighborFrames = true)
	{
		_type = type;
		_doFraming = setSelfFrames;
		_doNeighborFraming = setNeighborFrames;
	}

	public SetModTile ExtraParams(Func<int, int, Tile, bool> canReplace)
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
			GenBase._tiles[x, y].ResetToType(_type);
			if (_doFraming)
			{
				WorldUtils.TileFrame(x, y, _doNeighborFraming);
			}
		}
		return UnitApply(origin, x, y, args);
	}
}
