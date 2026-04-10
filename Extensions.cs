using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Terraria;

namespace Ultranium;

public static class Extensions
{
	public static bool IsInWorld(this Point point)
	{
		if (point.X >= 0 && point.Y >= 0 && point.X < Main.maxTilesX)
		{
			return point.Y < Main.maxTilesY;
		}
		return false;
	}

	public static object GetFieldValue(this Type type, string fieldName, object obj = null, BindingFlags? flags = null)
	{
		if (!flags.HasValue)
		{
			flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
		}
		return type.GetField(fieldName, flags.Value).GetValue(obj);
	}

	public static T GetFieldValue<T>(this Type type, string fieldName, object obj = null, BindingFlags? flags = null)
	{
		if (!flags.HasValue)
		{
			flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
		}
		return (T)type.GetField(fieldName, flags.Value).GetValue(obj);
	}
}
