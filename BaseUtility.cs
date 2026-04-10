using System;
using Microsoft.Xna.Framework;

namespace Ultranium;

public class BaseUtility
{
	public static int[] CombineArrays(int[] array1, int[] array2)
	{
		int[] array3 = new int[array1.Length + array2.Length];
		for (int i = 0; i < array1.Length; i++)
		{
			array3[i] = array1[i];
		}
		for (int j = 0; j < array2.Length; j++)
		{
			array3[array1.Length + j] = array2[j];
		}
		return array3;
	}

	public static bool InArray(int[] array, int value)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (value == array[i])
			{
				return true;
			}
		}
		return false;
	}

	public static bool InArray(int[] array, int value, ref int index)
	{
		for (int i = 0; i < array.Length; i++)
		{
			if (value == array[i])
			{
				index = i;
				return true;
			}
		}
		return false;
	}

	public static float MultiLerp(float percent, params float[] floats)
	{
		float num = 1f / ((float)floats.Length - 1f);
		float num2 = num;
		int num3 = 0;
		while (percent / num2 > 1f && num3 < floats.Length - 2)
		{
			num2 += num;
			num3++;
		}
		return MathHelper.Lerp(floats[num3], floats[num3 + 1], (percent - num * (float)num3) / num);
	}

	public static float RotationTo(Vector2 startPos, Vector2 endPos)
	{
		return (float)Math.Atan2(endPos.Y - startPos.Y, endPos.X - startPos.X);
	}

	public static Vector2 RotateVector(Vector2 origin, Vector2 vecToRot, float rot)
	{
		float x = (float)(Math.Cos(rot) * (double)(vecToRot.X - origin.X) - Math.Sin(rot) * (double)(vecToRot.Y - origin.Y) + (double)origin.X);
		float y = (float)(Math.Sin(rot) * (double)(vecToRot.X - origin.X) + Math.Cos(rot) * (double)(vecToRot.Y - origin.Y) + (double)origin.Y);
		return new Vector2(x, y);
	}
}
