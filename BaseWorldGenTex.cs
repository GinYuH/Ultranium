using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium;

public class BaseWorldGenTex
{
	public static Dictionary<Color, int> colorToLiquid;

	public static Dictionary<Color, int> colorToSlope;


    public static Color[] GetColorsFromRawImg(Mod mod, string imagePath, out int width, out int height)
    {
        if (!imagePath.EndsWith(".rawimg"))
        {
            imagePath = imagePath + ".rawimg";
        }

        var bytes = mod.GetFileBytes(imagePath).AsSpan();
        width = BitConverter.ToInt32(bytes[4..8]);
        height = BitConverter.ToInt32(bytes[8..12]);
        var colors = new Color[width * height];
        MemoryMarshal.Cast<byte, Color>(bytes[12..]).CopyTo(colors.AsSpan());
        return colors;
    }

    public static TexGen GetTexGenerator(string tileTex, Dictionary<Color, int> colorToTile, string wallTex = null, Dictionary<Color, int> colorToWall = null, string liquidTex = null, string slopeTex = null, string objectTex = null, Dictionary<Color, int> colorToObject = null)
	{
		if (colorToLiquid == null)
		{
			colorToLiquid = new Dictionary<Color, int>();
			colorToLiquid[Color.Black] = -2;
			colorToLiquid[new Color(0, 0, 255)] = 0;
			colorToLiquid[new Color(255, 0, 0)] = 1;
			colorToLiquid[new Color(255, 255, 0)] = 2;
			colorToSlope = new Dictionary<Color, int>();
			colorToSlope[new Color(255, 0, 0)] = 1;
			colorToSlope[new Color(0, 255, 0)] = 2;
			colorToSlope[new Color(0, 0, 255)] = 3;
			colorToSlope[new Color(255, 255, 0)] = 4;
			colorToSlope[new Color(255, 255, 255)] = -1;
			colorToSlope[Color.Black] = -2;
		}
		Mod mod = Ultranium.mod;
		Color[] array = GetColorsFromRawImg(mod, tileTex, out int widt, out int heig);
		Color[] array2 = [Color.White];
		if (wallTex != null)
		{
			array2 = GetColorsFromRawImg(mod, wallTex, out _, out _);
		}
		Color[] array3 = [Color.White];
		if (liquidTex != null)
        {
            array3 = GetColorsFromRawImg(mod, liquidTex, out _, out _);
        }
		Color[] array4 = [Color.White];
        if (slopeTex != null)
        {
            array4 = GetColorsFromRawImg(mod, slopeTex, out _, out _);
        }
		Color[] array5 = [Color.White];
        if (objectTex != null)
        {
            array5 = GetColorsFromRawImg(mod, objectTex, out _, out _);
        }
		int num = 0;
		int num2 = 0;
		TexGen texGen = new TexGen(widt, heig);
		for (int i = 0; i < array.Length; i++)
		{
			Color key = array[i];
			Color key2 = ((wallTex == null) ? Color.Black : array2[i]);
			Color key3 = ((liquidTex == null) ? Color.Black : array3[i]);
			Color key4 = ((slopeTex == null) ? Color.Black : array4[i]);
			Color key5 = ((objectTex == null) ? Color.Black : array5[i]);
			int id = (colorToTile.ContainsKey(key) ? colorToTile[key] : (-1));
			int wid = ((colorToWall != null && colorToWall.ContainsKey(key2)) ? colorToWall[key2] : (-1));
			int num3 = ((colorToLiquid != null && colorToLiquid.ContainsKey(key3)) ? colorToLiquid[key3] : (-1));
			int sl = ((colorToSlope != null && colorToSlope.ContainsKey(key4)) ? colorToSlope[key4] : (-1));
			int ob = ((colorToObject != null && colorToObject.ContainsKey(key5)) ? colorToObject[key5] : 0);
			texGen.tileGen[num, num2] = new TileInfo(id, 0, wid, num3, (num3 != -1) ? 255 : 0, sl, ob);
			num++;
			if (num >= widt)
			{
				num = 0;
				num2++;
			}
			if (num2 >= heig)
			{
				break;
			}
		}
		return texGen;
	}
}
