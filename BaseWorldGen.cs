using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium;

public class BaseWorldGen
{
	public class GenHelper
	{
		public class TileData
		{
			public int X;

			public int Y;

			public Tile tile;

			public TileData(int i, int j, Tile t)
			{
				X = i;
				Y = j;
				tile = t;
			}
		}

		public List<TileData> tiles = new List<TileData>();

		public Action<int, int> generate;

		public float rotation;

		public int rotX;

		public int rotY;

		public GenHelper(Action<int, int> gen)
		{
			generate = gen;
		}

		public void Gen(int x, int y)
		{
			Gen(x, y, rotX, rotY, rotation);
		}

		public void Gen(int x, int y, int rotationX, int rotationY, float genRotation)
		{
			tiles.Clear();
			Tile[,] tile = Main.tile;
			Main.tile = new Tile[Main.maxTilesX, Main.maxTilesY];
			generate(x, y);
			for (int i = 0; i < Main.maxTilesX; i++)
			{
				for (int j = 0; j < Main.maxTilesY; j++)
				{
					Tile tile2 = Main.tile[i, j];
					if (tile2 != null)
					{
						tiles.Add(new TileData(i, j, tile2));
					}
				}
			}
			Main.tile = tile;
			Vector2 origin = new Vector2((x + rotationX) * 16, (y + rotationY) * 16);
			List<Point> list = new List<Point>();
			foreach (TileData tile4 in tiles)
			{
				Vector2 vecToRot = new Vector2(tile4.X * 16, tile4.Y * 16);
				vecToRot = BaseUtility.RotateVector(origin, vecToRot, genRotation);
				int num = (int)vecToRot.X / 16;
				int num2 = (int)vecToRot.Y / 16;
				if (vecToRot.X % 16f > 0f)
				{
					num--;
				}
				if (vecToRot.Y % 16f > 0f)
				{
					num2--;
				}
				Point item = new Point(num, num2);
				list.Add(item);
				Main.tile[num, num2] = tile4.tile;
			}
			foreach (Point item2 in list)
			{
				WorldGen.TileFrame(item2.X, item2.Y);
				Tile tile3 = Main.tile[item2.X, item2.Y];
				if (tile3 != null && tile3.wall > 0)
				{
					Framing.WallFrame(item2.X, item2.Y);
				}
			}
			list.Clear();
		}

		public bool CheckTile(ref int x, ref int y, ref Point point, int offsetX, int offsetY)
		{
			int num = x + offsetX;
			int num2 = y + offsetY;
			if (ValidTile(num, num2))
			{
				x = num;
				y = num2;
				point = new Point(num, num2);
				return true;
			}
			return false;
		}

		public bool ValidTile(int x, int y)
		{
			if (Main.tile[x, y] != null)
			{
				if (!Main.tile[x, y].active())
				{
					return Main.tile[x, y].wall == 0;
				}
				return false;
			}
			return true;
		}
	}

	public static int GetFirstTileFloor(int x, int startY, bool solid = true)
	{
		for (int i = startY; i < Main.maxTilesY; i++)
		{
			Tile tile = Main.tile[x, i];
			if (tile != null && tile.nactive() && (!solid || Main.tileSolid[tile.type]))
			{
				return i;
			}
		}
		return Main.maxTilesY;
	}

	public static int GetFirstTileCeiling(int x, int startY, bool solid = true)
	{
		for (int num = startY; num > 0; num--)
		{
			Tile tile = Main.tile[x, num];
			if (tile != null && tile.nactive() && (!solid || Main.tileSolid[tile.type]))
			{
				return num;
			}
		}
		return 0;
	}

	public static int GetFirstTileSide(int startX, int y, bool left, bool solid = true)
	{
		if (left)
		{
			for (int num = startX; num > 0; num--)
			{
				Tile tile = Main.tile[num, y];
				if (tile != null && tile.nactive() && (!solid || Main.tileSolid[tile.type]))
				{
					return num;
				}
			}
			return 0;
		}
		for (int i = startX; i < Main.maxTilesX; i++)
		{
			Tile tile2 = Main.tile[i, y];
			if (tile2 != null && tile2.nactive() && (!solid || Main.tileSolid[tile2.type]))
			{
				return i;
			}
		}
		return Main.maxTilesX;
	}

	public static void ReplaceTiles(Vector2 position, int radius, int[] tiles, int[] replacements, bool silent = false, bool sync = true)
	{
		int num = (int)(position.X / 16f - (float)radius);
		int num2 = (int)(position.X / 16f + (float)radius);
		int num3 = (int)(position.Y / 16f - (float)radius);
		int num4 = (int)(position.Y / 16f + (float)radius);
		if (num < 0)
		{
			num = 0;
		}
		if (num2 > Main.maxTilesX)
		{
			num2 = Main.maxTilesX;
		}
		if (num3 < 0)
		{
			num3 = 0;
		}
		if (num4 > Main.maxTilesY)
		{
			num4 = Main.maxTilesY;
		}
		float num5 = (float)radius * 16f;
		for (int i = num; i <= num2; i++)
		{
			for (int j = num3; j <= num4; j++)
			{
				if ((double)Vector2.Distance(new Vector2((float)i * 16f + 8f, (float)j * 16f + 8f), position) < (double)num5 && Main.tile[i, j] != null && Main.tile[i, j].active())
				{
					int type = Main.tile[i, j].type;
					int index = 0;
					if (BaseUtility.InArray(tiles, type, ref index))
					{
						GenerateTile(i, j, replacements[index], -1, 0, active: true, removeLiquid: false, -2, silent, sync: false);
					}
				}
			}
		}
		if (sync && Main.netMode != 0)
		{
			NetMessage.SendTileSquare(-1, (int)(position.X / 16f), (int)(position.Y / 16f), radius * 2 + 2);
		}
	}

	public static bool KillChestAndItems(int X, int Y)
	{
		for (int i = 0; i < 1000; i++)
		{
			if (Main.chest[i] != null && Main.chest[i].x == X && Main.chest[i].y == Y)
			{
				Main.chest[i] = null;
				return true;
			}
		}
		return false;
	}

	public static void GenerateLiquid(int x, int y, int liquidType, bool updateFlow = true, int liquidHeight = 255, bool sync = true)
	{
		liquidHeight = (int)MathHelper.Clamp(liquidHeight, 0f, 255f);
		Main.tile[x, y].liquid = (byte)liquidHeight;
		switch (liquidType)
		{
		case 0:
			Main.tile[x, y].lava(lava: false);
			Main.tile[x, y].honey(honey: false);
			break;
		case 1:
			Main.tile[x, y].lava(lava: true);
			Main.tile[x, y].honey(honey: false);
			break;
		case 2:
			Main.tile[x, y].lava(lava: false);
			Main.tile[x, y].honey(honey: true);
			break;
		}
		if (updateFlow)
		{
			Liquid.AddWater(x, y);
		}
		if (sync && Main.netMode != 0)
		{
			NetMessage.SendTileSquare(-1, x, y, 1);
		}
	}

	public static void GenerateLiquid(int x, int y, int width, int height, int liquidType, bool updateFlow = true, int liquidHeight = 255, bool sync = true)
	{
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				GenerateLiquid(i + x, j + y, liquidType, updateFlow, liquidHeight, sync: false);
			}
		}
		int num = ((width > height) ? width : height);
		if (sync && Main.netMode != 0)
		{
			NetMessage.SendTileSquare(-1, x + (int)((float)width * 0.5f) - 1, y + (int)((float)height * 0.5f) - 1, num + 4);
		}
	}

	public static void GenerateTile(int x, int y, int tile, int wall, int tileStyle = 0, bool active = true, bool removeLiquid = true, int slope = -2, bool silent = false, bool sync = true)
	{
		try
		{
			if (Main.tile[x, y] == null)
			{
				Main.tile[x, y] = new Tile();
			}
			TileObjectData tileObjectData = ((tile <= -1) ? null : TileObjectData.GetTileData(tile, tileStyle));
			int num = tileObjectData?.Width ?? 1;
			int num2 = tileObjectData?.Height ?? 1;
			int num3 = ((tile == -1 || tileObjectData == null) ? 1 : tileObjectData.Width);
			int num4 = ((tile == -1 || tileObjectData == null) ? 1 : tileObjectData.Height);
			byte b = Main.tile[x, y].slope();
			bool flag = Main.tile[x, y].halfBrick();
			if (tile != -1)
			{
				WorldGen.destroyObject = true;
				if (num > 1 || num2 > 1)
				{
					Vector2 vector = BaseTile.FindTopLeft(x, y);
					for (int i = 0; i < num; i++)
					{
						for (int j = 0; j < num2; j++)
						{
							int num5 = (int)vector.X + i;
							int num6 = (int)vector.Y + j;
							if (i == 0 && j == 0 && Main.tile[num5, num6].type == 21)
							{
								KillChestAndItems(num5, num6);
							}
							Main.tile[x, y].type = 0;
							Main.tile[x, y].active(active: false);
							if (!silent)
							{
								WorldGen.KillTile(x, y, fail: false, effectOnly: false, noItem: true);
							}
							if (removeLiquid)
							{
								GenerateLiquid(num5, num6, 0, updateFlow: true, 0, sync: false);
							}
						}
					}
					for (int k = 0; k < num; k++)
					{
						for (int l = 0; l < num2; l++)
						{
							int i2 = (int)vector.X + k;
							int j2 = (int)vector.Y + l;
							WorldGen.SquareTileFrame(i2, j2);
							WorldGen.SquareWallFrame(i2, j2);
						}
					}
				}
				else if (!silent)
				{
					WorldGen.KillTile(x, y, fail: false, effectOnly: false, noItem: true);
				}
				WorldGen.destroyObject = false;
				if (active)
				{
					if (num3 <= 1 && num4 <= 1 && !Main.tileFrameImportant[tile])
					{
						Main.tile[x, y].type = (ushort)tile;
						Main.tile[x, y].active(active: true);
						if (slope == -2 && flag)
						{
							Main.tile[x, y].halfBrick(halfBrick: true);
						}
						else if (slope == -1)
						{
							Main.tile[x, y].halfBrick(halfBrick: true);
						}
						else
						{
							Main.tile[x, y].slope((slope == -2) ? b : ((byte)slope));
						}
						WorldGen.SquareTileFrame(x, y);
					}
					else
					{
						WorldGen.destroyObject = true;
						if (!silent)
						{
							for (int m = 0; m < num3; m++)
							{
								for (int n = 0; n < num4; n++)
								{
									WorldGen.KillTile(x + m, y + n, fail: false, effectOnly: false, noItem: true);
								}
							}
						}
						WorldGen.destroyObject = false;
						int j3 = ((tile == 10) ? y : (y + num2));
						WorldGen.PlaceTile(x, j3, tile, mute: true, forced: true, -1, tileStyle);
						for (int num7 = 0; num7 < num3; num7++)
						{
							for (int num8 = 0; num8 < num4; num8++)
							{
								WorldGen.SquareTileFrame(x + num7, y + num8);
							}
						}
					}
				}
				else
				{
					Main.tile[x, y].active(active: false);
				}
			}
			if (wall != -1)
			{
				if (wall == -2)
				{
					wall = 0;
				}
				Main.tile[x, y].wall = 0;
				WorldGen.PlaceWall(x, y, wall, mute: true);
			}
			if (sync && Main.netMode != 0)
			{
				int num9 = num3 + Math.Max(0, num - 1);
				int num10 = num4 + Math.Max(0, num2 - 1);
				int num11 = ((num9 > num10) ? num9 : num10);
				NetMessage.SendTileSquare(-1, x + (int)((float)num11 * 0.5f), y + (int)((float)num11 * 0.5f), num11 + 1);
			}
		}
		catch (Exception ex)
		{
			ErrorLogger.Log("TILEGEN ERROR: " + ex.Message);
			ErrorLogger.Log(ex.StackTrace);
			ErrorLogger.Log("--------");
		}
	}

	public static void GenerateLine(GenConditions gen, int x, int y, int endX, int endY, int thickness, bool sync = true)
	{
		if (gen == null)
		{
			throw new Exception("GenConditions cannot be null!");
		}
		if (endX < x)
		{
			int num = x;
			x = endX;
			endX = num;
		}
		bool flag = endY < y;
		if (flag)
		{
			x += Math.Abs(endX - x);
		}
		if (x == endX && y == endY)
		{
			int tile = gen.GetTile(0);
			int wall = gen.GetWall(0);
			if ((tile <= -1 || gen.CanPlace == null || gen.CanPlace(x, y, tile, wall)) && (wall <= -1 || gen.CanPlaceWall == null || gen.CanPlaceWall(x, y, tile, wall)))
			{
				GenerateTile(x, y, tile, wall, 0, tile != -1, removeLiquid: true, 0, silent: false, sync);
				if (gen.slope)
				{
					SmoothTiles(x, y, x, y);
				}
			}
			return;
		}
		if (x == endX || y == endY)
		{
			if (endY < y)
			{
				int num2 = y;
				y = endY;
				endY = num2;
			}
			bool flag2 = x == endX;
			int num3 = -1;
			int num4 = -1;
			for (int i = 0; i < (flag2 ? (endY - y) : (endX - x)); i++)
			{
				for (int j = 0; j < thickness; j++)
				{
					num3 = ((gen.tiles == null) ? (-1) : (gen.orderTiles ? (num3 + 1) : WorldGen.genRand.Next(gen.tiles.Length)));
					num4 = ((gen.walls == null) ? (-1) : (gen.orderWalls ? (num4 + 1) : WorldGen.genRand.Next(gen.walls.Length)));
					if (num3 != -1 && num3 >= gen.tiles.Length)
					{
						num3 = 0;
					}
					if (num4 != -1 && num4 >= gen.walls.Length)
					{
						num4 = 0;
					}
					int num5 = (flag2 ? j : i);
					int num6 = (flag2 ? i : j);
					int num7 = x + num5;
					int num8 = y + num6;
					bool num9 = num3 == -1 || gen.CanPlace == null || gen.CanPlace(num7, num8, gen.GetTile(num3), gen.GetWall(num4));
					bool flag3 = num4 == -1 || gen.CanPlaceWall == null || gen.CanPlaceWall(num7, num8, gen.GetTile(num3), gen.GetWall(num4));
					if (num9 && flag3)
					{
						GenerateTile(num7, num8, gen.GetTile(num3), gen.GetWall(num4), 0, gen.GetTile(num3) != -1, removeLiquid: true, 0, silent: false, sync: false);
					}
				}
			}
			_ = gen.slope;
			if (sync && Main.netMode != 0)
			{
				int num10 = ((endY - y > endX - x) ? (endY - y) : (endX - x));
				if (thickness > num10)
				{
					num10 = thickness;
				}
				NetMessage.SendData(20, -1, -1, NetworkText.FromLiteral(""), num10, x, y);
			}
			return;
		}
		Vector2 vector = new Vector2(x, y);
		Vector2 vector2 = new Vector2(endX, endY);
		Vector2 vector3 = new Vector2(endX, endY) - new Vector2(x, y);
		vector3.Normalize();
		float num11 = Vector2.Distance(vector, vector2);
		float num12 = 0f;
		float num13 = BaseUtility.RotationTo(vector, vector2);
		if (num13 < 0f)
		{
			num13 = (float)Math.PI * 2f - Math.Abs(num13);
		}
		float num14 = MathHelper.Lerp(0f, 1f, num13 / ((float)Math.PI * 2f));
		bool flag4 = num14 < 0.125f || (num14 > 0.375f && num14 < 0.625f) || num14 > 0.825f;
		int num15 = -1;
		int num16 = -1;
		int number = x;
		int num17 = y;
		for (; num12 < num11; num12 += 1f)
		{
			Vector2 vector4 = vector + vector3 * num12;
			Point point = new Point((int)vector4.X, (int)vector4.Y);
			for (int k = 0; k < thickness; k++)
			{
				num15 = ((gen.tiles == null) ? (-1) : (gen.orderTiles ? (num15 + 1) : WorldGen.genRand.Next(gen.tiles.Length)));
				num16 = ((gen.walls == null) ? (-1) : (gen.orderWalls ? (num16 + 1) : WorldGen.genRand.Next(gen.walls.Length)));
				if (num15 != -1 && num15 >= gen.tiles.Length)
				{
					num15 = 0;
				}
				if (num16 != -1 && num16 >= gen.walls.Length)
				{
					num16 = 0;
				}
				int num18 = ((!flag4) ? k : 0);
				int num19 = (flag4 ? k : 0);
				int num20 = point.X + num18;
				int num21 = (flag ? (point.Y - num19) : (point.Y + num19));
				bool num22 = num15 == -1 || gen.CanPlace == null || gen.CanPlace(num20, num21, gen.GetTile(num15), gen.GetWall(num16));
				bool flag5 = num16 == -1 || gen.CanPlaceWall == null || gen.CanPlaceWall(num20, num21, gen.GetTile(num15), gen.GetWall(num16));
				if (num22 && flag5)
				{
					GenerateTile(num20, num21, gen.GetTile(num15), gen.GetWall(num16), 0, gen.GetTile(num15) != -1, removeLiquid: true, 0, silent: false, sync: false);
				}
			}
			if (sync && Main.netMode != 0 && ((!flag4 && Math.Abs(num17 - point.Y) >= 5) || (flag4 && Math.Abs(num17 - point.Y) >= 5) || num12 + 1f > num11))
			{
				int num23 = Math.Max(5, thickness);
				NetMessage.SendData(10, -1, -1, NetworkText.FromLiteral(""), number, num17, num23, num23);
				number = point.X;
				num17 = point.Y;
			}
		}
	}

	public static void GenerateHall(GenConditions gen, int x, int y, int endX, int endY, int thickness, int height, bool sync = true)
	{
		if (gen == null)
		{
			throw new Exception("GenConditions cannot be null!");
		}
		if (endX < x)
		{
			int num = x;
			x = endX;
			endX = num;
		}
		bool num2 = endX < x;
		bool flag = endY < y;
		int num3 = ((!num2) ? 1 : (-1));
		int num4 = ((!flag) ? 1 : (-1));
		Vector2 startPos = new Vector2(x, y);
		Vector2 endPos = new Vector2(endX, endY);
		float num5 = MathHelper.Lerp(0f, 1f, BaseUtility.RotationTo(startPos, endPos) / ((float)Math.PI * 2f));
		bool flag2 = num5 < 0.125f || (num5 > 0.375f && num5 < 0.625f) || num5 > 0.825f;
		Vector2 vector = new Vector2(endX, endY);
		(new int[1])[0] = -2;
		Vector2 vector2 = new Vector2(flag2 ? x : (x + 2 * num3), flag2 ? (y + 2 * num4) : y);
		Vector2 vector3 = new Vector2(flag2 ? endX : (endX + 2 * num3), flag2 ? (endY + 2 * num4) : endY);
		Vector2 vector4 = new Vector2(flag2 ? x : (x + (thickness * 2 + height) * num3), flag2 ? (y + (thickness * 2 + height) * num4) : y);
		Vector2 vector5 = new Vector2(flag2 ? endX : (endX + (thickness * 2 + height) * num3), flag2 ? (endY + (thickness * 2 + height) * num4) : endY);
		int[] tiles = gen.tiles;
		int[] walls = gen.walls;
		gen.tiles = null;
		GenerateLine(gen, (int)vector2.X, (int)vector2.Y, (int)vector3.X, (int)vector3.Y, thickness * 3 + height - 2, sync: false);
		gen.tiles = tiles;
		gen.walls = null;
		GenerateLine(gen, x, y, (int)vector.X, (int)vector.Y, thickness, sync: false);
		GenerateLine(gen, (int)vector4.X, (int)vector4.Y, (int)vector5.X, (int)vector5.Y, thickness, sync: false);
		gen.walls = walls;
	}

	public static void GenerateTrapezoid(GenConditions gen, int x, int y, int endX, int endY, int thickness, int height, bool sync = true)
	{
		if (gen == null)
		{
			throw new Exception("GenConditions cannot be null!");
		}
		if (endX < x)
		{
			int num = x;
			x = endX;
			endX = num;
		}
		Vector2 startPos = new Vector2(x, y);
		Vector2 endPos = new Vector2(endX, endY);
		float num2 = MathHelper.Lerp(0f, 1f, BaseUtility.RotationTo(startPos, endPos) / ((float)Math.PI * 2f));
		bool flag = num2 < 0.125f || (num2 > 0.375f && num2 < 0.625f) || num2 > 0.825f;
		Vector2 vector = new Vector2(endX, endY);
		Vector2 vector2 = new Vector2(x + thickness, y + thickness);
		Vector2 vector3 = new Vector2(flag ? endX : (endX + thickness), flag ? (endY + thickness) : endY);
		Vector2 vector4 = new Vector2(flag ? x : (x + thickness * 2 + height), flag ? (y + thickness * 2 + height) : y);
		Vector2 vector5 = new Vector2(flag ? endX : (endX + thickness * 2 + height), flag ? (endY + thickness * 2 + height) : endY);
		int[] tiles = gen.tiles;
		int[] walls = gen.walls;
		gen.tiles = null;
		GenerateLine(gen, (int)vector2.X, (int)vector2.Y, (int)vector3.X, (int)vector3.Y, thickness + height, sync: false);
		gen.tiles = tiles;
		gen.walls = null;
		GenerateLine(gen, x, y, (int)vector.X, (int)vector.Y, thickness, sync: false);
		GenerateLine(gen, (int)vector4.X, (int)vector4.Y, (int)vector5.X, (int)vector5.Y, thickness, sync: false);
		GenerateLine(gen, x, y, (int)vector4.X, (int)vector4.Y, thickness, sync: false);
		GenerateLine(gen, (int)vector.X, (int)vector.Y, flag ? ((int)vector5.X) : ((int)vector5.X + thickness), flag ? ((int)vector5.Y + thickness) : ((int)vector5.Y), thickness, sync: false);
		gen.walls = walls;
	}

	public static void GenerateRoomOld(int x, int y, int width, int height, int tile, int wall)
	{
		GenerateRoomOld(x, y, width, height, tile, tile, tile, wall);
	}

	public static void GenerateRoomOld(int x, int y, int width, int height, int tileSides, int tileFloor, int tileCeiling, int wall, bool wallEnds = false, int sideThickness = 1, int floorThickness = 1, int ceilingThickness = 1, bool sync = true)
	{
		if (tileSides != -1 && sideThickness > 1)
		{
			width += sideThickness;
			x -= sideThickness / 2;
		}
		if (tileFloor != -1 && floorThickness > 1)
		{
			height += floorThickness;
		}
		if (tileCeiling != -1 && ceilingThickness > 1)
		{
			height += ceilingThickness;
			y -= ceilingThickness / 2;
		}
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				int x2 = i + x;
				int y2 = j + y;
				if ((wallEnds || tileCeiling != -1) && j < ceilingThickness)
				{
					GenerateTile(x2, y2, tileCeiling, (wallEnds && j == 0) ? wall : (-1), 0, tileCeiling != -1 || !wallEnds, removeLiquid: true, 0, silent: false, sync: false);
				}
				else if ((wallEnds || tileFloor != -1) && j >= height - floorThickness)
				{
					GenerateTile(x2, y2, tileFloor, (wallEnds && j >= height - 1) ? wall : (-1), 0, tileFloor != -1 || !wallEnds, removeLiquid: true, 0, silent: false, sync: false);
				}
				else if ((wallEnds || tileSides != -1) && (i < sideThickness || i >= width - sideThickness))
				{
					GenerateTile(x2, y2, tileSides, (wallEnds && i > 0 && i < width - 1) ? wall : (-1), 0, tileSides != -1 || !wallEnds, removeLiquid: true, 0, silent: false, sync: false);
				}
				else if (i >= sideThickness && i < width - sideThickness && j >= ceilingThickness && j < height - floorThickness)
				{
					GenerateTile(x2, y2, -1, wall, 0, active: false, removeLiquid: true, 0, silent: false, sync: false);
				}
			}
		}
		int num = ((width > height) ? width : height);
		if (sync && Main.netMode != 0)
		{
			NetMessage.SendTileSquare(-1, x + (int)((float)width * 0.5f) - 1, y + (int)((float)height * 0.5f) - 1, num + 4);
		}
	}

	public static void GenerateChest(int x, int y, int type, int chestStyle, int[] stackIDs, bool randomAmounts = false, bool randomPrefix = false, bool sync = true)
	{
		int[] array = new int[20];
		for (int i = 0; i < array.Length; i++)
		{
			if (randomAmounts)
			{
				array[i] = WorldGen.genRand.Next(1, 6);
			}
			else
			{
				array[i] = 1;
			}
		}
		GenerateChest(x, y, type, chestStyle, stackIDs, array, randomPrefix, sync);
	}

	public static void GenerateChest(int x, int y, int type, int chestStyle, int[] stackIDs, int[] stackAmounts, bool randomPrefix = false, bool sync = true)
	{
		int[] array = new int[20];
		for (int i = 0; i < array.Length; i++)
		{
			if (randomPrefix)
			{
				array[i] = -1;
			}
			else
			{
				array[i] = -10;
			}
		}
		GenerateChest(x, y, type, chestStyle, stackIDs, stackAmounts, array, sync);
	}

	public static void GenerateChest(int x, int y, int type, int chestStyle, int[] stackIDs, int[] stackAmounts, int[] stackPrefixes, bool sync = true)
	{
		int num = WorldGen.PlaceChest(x - 1, y, (ushort)type, notNearOtherChests: false, chestStyle);
		if (num >= 0)
		{
			for (int i = 0; i < Main.chest[num].item.Length; i++)
			{
				if (stackIDs == null)
				{
					break;
				}
				if (stackIDs.Length <= i)
				{
					break;
				}
				Main.chest[num].item[i].SetDefaults(stackIDs[i], false);
				Main.chest[num].item[i].stack = stackAmounts[i];
				if (stackPrefixes[i] != -10)
				{
					Main.chest[num].item[i].Prefix(stackPrefixes[i]);
				}
			}
		}
		WorldGen.SquareTileFrame(x + 1, y);
		if (sync && Main.netMode != 0)
		{
			NetMessage.SendTileSquare(-1, x, y, 2);
		}
	}

	public static void SmoothTiles(int topX, int topY, int bottomX, int bottomY)
	{
		Main.tileSolid[137] = false;
		for (int i = topX; i < bottomX; i++)
		{
			for (int j = topY; j < bottomY; j++)
			{
				if (Main.tile[i, j].type == 48 || Main.tile[i, j].type == 137 || Main.tile[i, j].type == 232 || Main.tile[i, j].type == 191 || Main.tile[i, j].type == 151 || Main.tile[i, j].type == 274)
				{
					continue;
				}
				if (!Main.tile[i, j - 1].active())
				{
					if (WorldGen.SolidTile(i, j))
					{
						if (Main.tile[i - 1, j].halfBrick() || Main.tile[i + 1, j].halfBrick() || Main.tile[i - 1, j].slope() != 0 || Main.tile[i + 1, j].slope() != 0)
						{
							continue;
						}
						if (WorldGen.SolidTile(i, j + 1))
						{
							if (!WorldGen.SolidTile(i - 1, j) && !Main.tile[i - 1, j + 1].halfBrick() && WorldGen.SolidTile(i - 1, j + 1) && WorldGen.SolidTile(i + 1, j) && !Main.tile[i + 1, j - 1].active())
							{
								if (WorldGen.genRand.Next(2) == 0)
								{
									WorldGen.SlopeTile(i, j, 2);
								}
								else
								{
									WorldGen.PoundTile(i, j);
								}
							}
							else if (!WorldGen.SolidTile(i + 1, j) && !Main.tile[i + 1, j + 1].halfBrick() && WorldGen.SolidTile(i + 1, j + 1) && WorldGen.SolidTile(i - 1, j) && !Main.tile[i - 1, j - 1].active())
							{
								if (WorldGen.genRand.Next(2) == 0)
								{
									WorldGen.SlopeTile(i, j, 1);
								}
								else
								{
									WorldGen.PoundTile(i, j);
								}
							}
							else if (WorldGen.SolidTile(i + 1, j + 1) && WorldGen.SolidTile(i - 1, j + 1) && !Main.tile[i + 1, j].active() && !Main.tile[i - 1, j].active())
							{
								WorldGen.PoundTile(i, j);
							}
							if (WorldGen.SolidTile(i, j))
							{
								if (WorldGen.SolidTile(i - 1, j) && WorldGen.SolidTile(i + 1, j + 2) && !Main.tile[i + 1, j].active() && !Main.tile[i + 1, j + 1].active() && !Main.tile[i - 1, j - 1].active())
								{
									WorldGen.KillTile(i, j);
								}
								else if (WorldGen.SolidTile(i + 1, j) && WorldGen.SolidTile(i - 1, j + 2) && !Main.tile[i - 1, j].active() && !Main.tile[i - 1, j + 1].active() && !Main.tile[i + 1, j - 1].active())
								{
									WorldGen.KillTile(i, j);
								}
								else if (!Main.tile[i - 1, j + 1].active() && !Main.tile[i - 1, j].active() && WorldGen.SolidTile(i + 1, j) && WorldGen.SolidTile(i, j + 2))
								{
									if (WorldGen.genRand.Next(5) == 0)
									{
										WorldGen.KillTile(i, j);
									}
									else if (WorldGen.genRand.Next(5) == 0)
									{
										WorldGen.PoundTile(i, j);
									}
									else
									{
										WorldGen.SlopeTile(i, j, 2);
									}
								}
								else if (!Main.tile[i + 1, j + 1].active() && !Main.tile[i + 1, j].active() && WorldGen.SolidTile(i - 1, j) && WorldGen.SolidTile(i, j + 2))
								{
									if (WorldGen.genRand.Next(5) == 0)
									{
										WorldGen.KillTile(i, j);
									}
									else if (WorldGen.genRand.Next(5) == 0)
									{
										WorldGen.PoundTile(i, j);
									}
									else
									{
										WorldGen.SlopeTile(i, j, 1);
									}
								}
							}
						}
						if (WorldGen.SolidTile(i, j) && !Main.tile[i - 1, j].active() && !Main.tile[i + 1, j].active())
						{
							WorldGen.KillTile(i, j);
						}
					}
					else
					{
						if (Main.tile[i, j].active() || Main.tile[i, j + 1].type == 151 || Main.tile[i, j + 1].type == 274)
						{
							continue;
						}
						if (Main.tile[i + 1, j].type != 190 && Main.tile[i + 1, j].type != 48 && Main.tile[i + 1, j].type != 232 && WorldGen.SolidTile(i - 1, j + 1) && WorldGen.SolidTile(i + 1, j) && !Main.tile[i - 1, j].active() && !Main.tile[i + 1, j - 1].active())
						{
							WorldGen.PlaceTile(i, j, Main.tile[i, j + 1].type);
							if (WorldGen.genRand.Next(2) == 0)
							{
								WorldGen.SlopeTile(i, j, 2);
							}
							else
							{
								WorldGen.PoundTile(i, j);
							}
						}
						if (Main.tile[i - 1, j].type != 190 && Main.tile[i - 1, j].type != 48 && Main.tile[i - 1, j].type != 232 && WorldGen.SolidTile(i + 1, j + 1) && WorldGen.SolidTile(i - 1, j) && !Main.tile[i + 1, j].active() && !Main.tile[i - 1, j - 1].active())
						{
							WorldGen.PlaceTile(i, j, Main.tile[i, j + 1].type);
							if (WorldGen.genRand.Next(2) == 0)
							{
								WorldGen.SlopeTile(i, j, 1);
							}
							else
							{
								WorldGen.PoundTile(i, j);
							}
						}
					}
				}
				else if (!Main.tile[i, j + 1].active() && WorldGen.genRand.Next(2) == 0 && WorldGen.SolidTile(i, j) && !Main.tile[i - 1, j].halfBrick() && !Main.tile[i + 1, j].halfBrick() && Main.tile[i - 1, j].slope() == 0 && Main.tile[i + 1, j].slope() == 0 && WorldGen.SolidTile(i, j - 1))
				{
					if (WorldGen.SolidTile(i - 1, j) && !WorldGen.SolidTile(i + 1, j) && WorldGen.SolidTile(i - 1, j - 1))
					{
						WorldGen.SlopeTile(i, j, 3);
					}
					else if (WorldGen.SolidTile(i + 1, j) && !WorldGen.SolidTile(i - 1, j) && WorldGen.SolidTile(i + 1, j - 1))
					{
						WorldGen.SlopeTile(i, j, 4);
					}
				}
			}
		}
		for (int k = topX; k < bottomX; k++)
		{
			for (int l = topY; l < bottomY; l++)
			{
				if (WorldGen.genRand.Next(2) == 0 && !Main.tile[k, l - 1].active() && Main.tile[k, l].type != 137 && Main.tile[k, l].type != 48 && Main.tile[k, l].type != 232 && Main.tile[k, l].type != 191 && Main.tile[k, l].type != 151 && Main.tile[k, l].type != 274 && Main.tile[k, l].type != 75 && Main.tile[k, l].type != 76 && WorldGen.SolidTile(k, l) && Main.tile[k - 1, l].type != 137 && Main.tile[k + 1, l].type != 137)
				{
					if (WorldGen.SolidTile(k, l + 1) && WorldGen.SolidTile(k + 1, l) && !Main.tile[k - 1, l].active())
					{
						WorldGen.SlopeTile(k, l, 2);
					}
					if (WorldGen.SolidTile(k, l + 1) && WorldGen.SolidTile(k - 1, l) && !Main.tile[k + 1, l].active())
					{
						WorldGen.SlopeTile(k, l, 1);
					}
				}
				if (Main.tile[k, l].slope() == 1 && !WorldGen.SolidTile(k - 1, l))
				{
					WorldGen.SlopeTile(k, l, 0);
					WorldGen.PoundTile(k, l);
				}
				if (Main.tile[k, l].slope() == 2 && !WorldGen.SolidTile(k + 1, l))
				{
					WorldGen.SlopeTile(k, l, 0);
					WorldGen.PoundTile(k, l);
				}
			}
		}
		Main.tileSolid[137] = true;
	}
}
