using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.WorldBuilding;

namespace Ultranium;

public class SetMapBrightness : GenAction
{
	public byte _brightness;

	public SetMapBrightness(byte brightness)
	{
		_brightness = brightness;
	}

	public override bool Apply(Point origin, int x, int y, params object[] args)
	{
		if (x < 0 || x > Main.maxTilesX || y < 0 || y > Main.maxTilesY)
		{
			return false;
		}
		if (GenBase._tiles[x, y] == null)
		{
			GenBase._tiles[x, y] = new Tile();
		}
		Main.Map.UpdateLighting(x, y, Math.Max(Main.Map[x, y].Light, _brightness));
		return ((GenAction)this).UnitApply(origin, x, y, args);
	}
}
