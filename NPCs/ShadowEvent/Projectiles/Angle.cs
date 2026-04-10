using System;

namespace Ultranium.NPCs.ShadowEvent.Projectiles;

public struct Angle
{
	public float Value;

	public Angle(float angle)
	{
		Value = angle;
		float num = Value % ((float)Math.PI * 2f);
		float num2 = Value - num;
		Value -= num2;
		if (Value < 0f)
		{
			Value += (float)Math.PI * 2f;
		}
	}

	public static Angle operator +(Angle a1, Angle a2)
	{
		return new Angle(a1.Value + a2.Value);
	}

	public static Angle operator -(Angle a1, Angle a2)
	{
		return new Angle(a1.Value - a2.Value);
	}

	public Angle Opposite()
	{
		return new Angle(Value + (float)Math.PI);
	}

	public bool ClockwiseFrom(Angle other)
	{
		if (other.Value >= (float)Math.PI)
		{
			if (Value < other.Value)
			{
				return Value >= other.Opposite().Value;
			}
			return false;
		}
		if (!(Value < other.Value))
		{
			return Value >= other.Opposite().Value;
		}
		return true;
	}

	public bool Between(Angle cLimit, Angle ccLimit)
	{
		if (cLimit.Value < ccLimit.Value)
		{
			if (Value >= cLimit.Value)
			{
				return Value <= ccLimit.Value;
			}
			return false;
		}
		if (!(Value >= cLimit.Value))
		{
			return Value <= ccLimit.Value;
		}
		return true;
	}
}
