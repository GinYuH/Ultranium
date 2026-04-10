using System;
using Microsoft.Xna.Framework;
using Terraria.World.Generation;

namespace Ultranium;

public class RadialDitherTopMiddle : GenAction
{
	private int _width;

	private int _height;

	private float _innerRadius;

	private float _outerRadius;

	public RadialDitherTopMiddle(int width, int height, float innerRadius, float outerRadius)
	{
		_width = width;
		_height = height;
		_innerRadius = innerRadius;
		_outerRadius = outerRadius;
	}

	public override bool Apply(Point origin, int x, int y, params object[] args)
	{
		float num = Vector2.Distance(value2: new Vector2((float)origin.X + (float)(_width / 2), origin.Y), value1: new Vector2(x, y));
		float num2 = Math.Max(0f, Math.Min(1f, (num - _innerRadius) / (_outerRadius - _innerRadius)));
		if (GenBase._random.NextDouble() > (double)num2)
		{
			return ((GenAction)this).UnitApply(origin, x, y, args);
		}
		return ((GenAction)this).Fail();
	}
}
