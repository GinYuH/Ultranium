using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Tiles.Ambient;

namespace Ultranium.Tiles.ShadowBiome.Depths;

public class DarkStone : ModTile
{
	public override void SetDefaults()
	{
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<ShadowGrass>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<AbyssRock>()] = true;
		Main.tileMerge[((ModTile)this).Type][ModContent.TileType<DepthGlowstone>()] = true;
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileBlockLight[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(31, 29, 49), (LocalizedText)null);
		base.dustType = ((ModTile)this).mod.DustType("ShadowDustBlack");
		base.drop = ((ModTile)this).mod.ItemType("DarkStoneItem");
		base.soundType = 21;
		base.soundStyle = 1;
		base.mineResist = 7f;
		base.minPick = 200;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j);
		Tile tileSafely2 = Framing.GetTileSafely(i, j + 1);
		Tile tileSafely3 = Framing.GetTileSafely(i, j - 1);
		if (Utils.NextBool(WorldGen.genRand, 7) && !tileSafely3.active() && !tileSafely2.lava() && !tileSafely.bottomSlope() && !tileSafely.topSlope() && !tileSafely.halfBrick() && !tileSafely.topSlope())
		{
			tileSafely3.type = (ushort)ModContent.TileType<ShadowFlora>();
			tileSafely3.active(active: true);
			tileSafely3.frameY = 0;
			tileSafely3.frameX = (short)(WorldGen.genRand.Next(5) * 18);
			WorldGen.SquareTileFrame(i, j + 2);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
		if (Utils.NextBool(WorldGen.genRand, 7) && !tileSafely2.active() && !tileSafely2.lava() && !tileSafely.bottomSlope())
		{
			tileSafely2.type = (ushort)ModContent.TileType<DepthVines>();
			tileSafely2.active(active: true);
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
		if (Utils.NextBool(WorldGen.genRand, 20) && !tileSafely2.active() && !tileSafely2.lava() && !tileSafely.bottomSlope())
		{
			tileSafely2.type = (ushort)ModContent.TileType<GlowShroomVine>();
			tileSafely2.active(active: true);
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.netMode == 2)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
		if (Framing.GetTileSafely(i, j - 1).type != 0 || Framing.GetTileSafely(i, j - 2).type != 0)
		{
			return;
		}
		if (Main.rand.Next(8) == 0)
		{
			if (Main.rand.Next(8) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroomHuge>());
				NetMessage.SendObjectPlacment(-1, i - 1, j - 1, ModContent.TileType<GlowShroomHuge>(), 0, 0, -1, -1);
			}
			if (Main.rand.Next(8) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroomHuge2>());
				NetMessage.SendObjectPlacment(-1, i - 1, j - 1, ModContent.TileType<GlowShroomHuge2>(), 0, 0, -1, -1);
			}
			if (Main.rand.Next(2) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroomTall>());
				NetMessage.SendObjectPlacment(-1, i - 1, j - 1, ModContent.TileType<GlowShroomTall>(), 0, 0, -1, -1);
			}
		}
		if (Main.rand.Next(5) == 0)
		{
			WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroomTall>());
			NetMessage.SendObjectPlacment(-1, i - 1, j - 1, ModContent.TileType<GlowShroomTall>(), 0, 0, -1, -1);
		}
	}
}
