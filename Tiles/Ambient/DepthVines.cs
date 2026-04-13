using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Tiles.ShadowBiome.Depths;

namespace Ultranium.Tiles.Ambient;

public class DepthVines : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileCut[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileLavaDeath[Type] = true;
		Main.tileNoFail[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileLighted[Type] = true;
		HitSound = SoundID.Grass;
		DustType = DustID.GemEmerald;
		AddMapEntry(new Color(21, 90, 48), (LocalizedText)null);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.1f;
		g = 0.5f;
		b = 0.45f;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j + 1);
		if (tileSafely.HasTile && tileSafely.TileType == Type)
		{
			WorldGen.KillTile(i, j + 1);
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j - 1);
		int num = -1;
		if (tileSafely.HasTile && !tileSafely.BottomSlope)
		{
			num = tileSafely.TileType;
		}
		if (num == ModContent.TileType<DarkStone>() || num == Type)
		{
			return true;
		}
		WorldGen.KillTile(i, j);
		return true;
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j + 1);
		if (tileSafely.HasTile || (tileSafely.LiquidType == LiquidID.Lava))
		{
			return;
		}
		bool flag = false;
		int num = j;
		while (num > j - 10)
		{
			Tile tileSafely2 = Framing.GetTileSafely(i, num);
			if (tileSafely2.BottomSlope)
			{
				break;
			}
			if (!tileSafely2.HasTile || tileSafely2.TileType != ModContent.TileType<DarkStone>())
			{
				num--;
				continue;
			}
			flag = true;
			break;
		}
		if (flag)
		{
			tileSafely.TileType = Type;
			tileSafely.HasTile = true;
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
	}
}
