using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Tiles.Ambient;

namespace Ultranium.Tiles.ShadowBiome.Depths;

public class DarkStone : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileMerge[Type][ModContent.TileType<ShadowGrass>()] = true;
		Main.tileMerge[Type][ModContent.TileType<AbyssRock>()] = true;
		Main.tileMerge[Type][ModContent.TileType<DepthGlowstone>()] = true;
		Main.tileSolid[Type] = true;
		Main.tileBlockLight[Type] = true;
		AddMapEntry(new Color(31, 29, 49), (LocalizedText)null);
		base.DustType = Mod.Find<ModDust>("ShadowDustBlack").Type;
		//base.ItemDrop/* tModPorter Note: Removed. Tiles and walls will drop the item which places them automatically. Use RegisterItemDrop to alter the automatic drop if necessary. */ = Mod.Find<ModItem>("DarkStoneItem").Type;
		base.HitSound = SoundID.Tink;
		//base.soundStyle/* tModPorter Note: Removed. Integrate into HitSound */ = 1;
		base.MineResist = 7f;
		base.MinPick = 200;
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
		if (Utils.NextBool(WorldGen.genRand, 7) && !tileSafely3.HasTile && !(tileSafely2.LiquidType == LiquidID.Lava) && !tileSafely.BottomSlope && !tileSafely.TopSlope && !tileSafely.IsHalfBlock && !tileSafely.TopSlope)
		{
			tileSafely3.TileType = (ushort)ModContent.TileType<ShadowFlora>();
			tileSafely3.HasTile = true;
			tileSafely3.TileFrameY = 0;
			tileSafely3.TileFrameX = (short)(WorldGen.genRand.Next(5) * 18);
			WorldGen.SquareTileFrame(i, j + 2);
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
		if (Utils.NextBool(WorldGen.genRand, 7) && !tileSafely2.HasTile && !(tileSafely2.LiquidType == LiquidID.Lava) && !tileSafely.BottomSlope)
		{
			tileSafely2.TileType = (ushort)ModContent.TileType<DepthVines>();
			tileSafely2.HasTile = true;
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
		if (Utils.NextBool(WorldGen.genRand, 20) && !tileSafely2.HasTile && !(tileSafely2.LiquidType == LiquidID.Lava) && !tileSafely.BottomSlope)
		{
			tileSafely2.TileType = (ushort)ModContent.TileType<GlowShroomVine>();
			tileSafely2.HasTile = true;
			WorldGen.SquareTileFrame(i, j + 1);
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendTileSquare(-1, i, j + 1, 3);
			}
		}
		if (Framing.GetTileSafely(i, j - 1).TileType != TileID.Dirt || Framing.GetTileSafely(i, j - 2).TileType != TileID.Dirt)
		{
			return;
		}
		if (Main.rand.Next(8) == 0)
		{
			if (Main.rand.Next(8) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroomHuge>());
				NetMessage.SendObjectPlacement(-1, i - 1, j - 1, ModContent.TileType<GlowShroomHuge>(), 0, 0, -1, -1);
			}
			if (Main.rand.Next(8) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroomHuge2>());
				NetMessage.SendObjectPlacement(-1, i - 1, j - 1, ModContent.TileType<GlowShroomHuge2>(), 0, 0, -1, -1);
			}
			if (Main.rand.Next(2) == 0)
			{
				WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroomTall>());
				NetMessage.SendObjectPlacement(-1, i - 1, j - 1, ModContent.TileType<GlowShroomTall>(), 0, 0, -1, -1);
			}
		}
		if (Main.rand.Next(5) == 0)
		{
			WorldGen.PlaceObject(i - 1, j - 1, ModContent.TileType<GlowShroomTall>());
			NetMessage.SendObjectPlacement(-1, i - 1, j - 1, ModContent.TileType<GlowShroomTall>(), 0, 0, -1, -1);
		}
	}
}
