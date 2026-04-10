using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Tiles.ShadowBiome;

namespace Ultranium.Tiles.Ambient;

public class ShadowGrassVine : ModTile
{
	public override void SetDefaults()
	{
		Main.tileCut[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		Main.tileNoFail[((ModTile)this).Type] = true;
		Main.tileNoAttach[((ModTile)this).Type] = true;
		base.soundType = 6;
		base.dustType = 89;
		((ModTile)this).AddMapEntry(new Color(58, 11, 67), (LocalizedText)null);
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j + 1);
		if (tileSafely.active() && tileSafely.type == ((ModTile)this).Type)
		{
			WorldGen.KillTile(i, j + 1);
		}
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j - 1);
		int num = -1;
		if (tileSafely.active() && !tileSafely.bottomSlope())
		{
			num = tileSafely.type;
		}
		if (num == ModContent.TileType<ShadowGrass>() || num == ((ModTile)this).Type)
		{
			return true;
		}
		WorldGen.KillTile(i, j);
		return true;
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j + 1);
		if (tileSafely.active() || tileSafely.lava())
		{
			return;
		}
		bool flag = false;
		int num = j;
		while (num > j - 10)
		{
			Tile tileSafely2 = Framing.GetTileSafely(i, num);
			if (tileSafely2.bottomSlope())
			{
				break;
			}
			if (!tileSafely2.active() || tileSafely2.type != ModContent.TileType<ShadowGrass>())
			{
				num--;
				continue;
			}
			flag = true;
			break;
		}
		if (flag)
		{
			tileSafely.type = ((ModTile)this).Type;
			tileSafely.active(active: true);
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
	}
}
