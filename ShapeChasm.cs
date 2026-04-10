using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.WorldBuilding;

namespace Ultranium;

public class ShapeChasm : GenShape
{
	public int _startwidth = 20;

	public int _endwidth = 5;

	public int _depth = 60;

	public int _variance;

	public int _randomHeading;

	public float[] _widthVariance;

	public bool _dir = true;

	public ShapeChasm(int startwidth, int endwidth, int depth, int variance, int randomHeading, float[] widthVariance = null, bool dir = true)
	{
		_startwidth = startwidth;
		_endwidth = endwidth;
		_depth = depth;
		_variance = variance;
		_randomHeading = randomHeading;
		_widthVariance = widthVariance;
		_dir = dir;
	}

	public void ResetChasmParams(int startwidth, int endwidth, int depth, int variance, int randomHeading, float[] widthVariance = null, bool dir = true)
	{
		_startwidth = startwidth;
		_endwidth = endwidth;
		_depth = depth;
		_variance = variance;
		_randomHeading = randomHeading;
		_widthVariance = widthVariance;
		_dir = dir;
	}

	private bool DoChasm(Point origin, GenAction action, int startwidth, int endwidth, int depth, int variance, int randomHeading, float[] widthVariance, bool dir)
	{
		Point point = origin;
		for (int i = 0; i < depth; i++)
		{
			int num = (int)MathHelper.Lerp(startwidth, endwidth, (float)i / (float)depth);
			if (widthVariance != null)
			{
				num = Math.Max(endwidth, (int)((float)startwidth * BaseUtility.MultiLerp((float)i / (float)depth, widthVariance)));
			}
			int num2 = point.X + (startwidth - num);
			int num3 = point.Y + (dir ? i : (-i));
			if (variance != 0)
			{
				num2 += ((Main.rand.Next(2) == 0) ? (-Main.rand.Next(variance)) : Main.rand.Next(variance));
			}
			if (randomHeading != 0)
			{
				num2 += randomHeading * (i / 2);
			}
			int num4 = num2 + num - (startwidth - num);
			for (int j = num2; j < num4; j++)
			{
				int num5 = j;
				if (!UnitApply(action, point, num5, num3, new object[0]) && base._quitOnFail)
				{
					return false;
				}
			}
		}
		return true;
	}

	public override bool Perform(Point origin, GenAction action)
	{
		return DoChasm(origin, action, _startwidth, _endwidth, _depth, _variance, _randomHeading, _widthVariance, _dir);
	}
}
