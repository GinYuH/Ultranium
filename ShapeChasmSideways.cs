using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.World.Generation;

namespace Ultranium;

public class ShapeChasmSideways : GenShape
{
	public int _startheight = 20;

	public int _endheight = 5;

	public int _length = 60;

	public int _variance;

	public int _randomHeading;

	public float[] _heightVariance;

	public bool _dir = true;

	public ShapeChasmSideways(int startheight, int endheight, int length, int variance, int randomHeading, float[] heightVariance = null, bool dir = true)
	{
		_startheight = startheight;
		_endheight = endheight;
		_length = length;
		_variance = variance;
		_randomHeading = randomHeading;
		_heightVariance = heightVariance;
		_dir = dir;
	}

	public void ResetChasmParams(int startheight, int endheight, int length, int variance, int randomHeading, float[] heightVariance = null, bool dir = true)
	{
		_startheight = startheight;
		_endheight = endheight;
		_length = length;
		_variance = variance;
		_randomHeading = randomHeading;
		_heightVariance = heightVariance;
		_dir = dir;
	}

	private bool DoChasm(Point origin, GenAction action, int startheight, int endheight, int length, int variance, int randomHeading, float[] heightVariance, bool dir)
	{
		Point point = origin;
		for (int i = 0; i < length; i++)
		{
			int num = (int)MathHelper.Lerp(startheight, endheight, (float)i / (float)length);
			if (heightVariance != null)
			{
				num = Math.Max(endheight, (int)((float)startheight * BaseUtility.MultiLerp((float)i / (float)length, heightVariance)));
			}
			int num2 = point.X + (dir ? i : (-i));
			int num3 = point.Y + (startheight - num);
			if (variance != 0)
			{
				num3 += ((Main.rand.Next(2) == 0) ? (-Main.rand.Next(variance)) : Main.rand.Next(variance));
			}
			if (randomHeading != 0)
			{
				num3 += randomHeading * (i / 2);
			}
			int num4 = num3 + num - (startheight - num);
			for (int j = num3; j < num4; j++)
			{
				int num5 = j;
				if (!((GenShape)this).UnitApply(action, point, num2, num5, new object[0]) && base._quitOnFail)
				{
					return false;
				}
			}
		}
		return true;
	}

	public override bool Perform(Point origin, GenAction action)
	{
		return DoChasm(origin, action, _startheight, _endheight, _length, _variance, _randomHeading, _heightVariance, _dir);
	}
}
