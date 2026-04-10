using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.ShadowBiome.Trees;

public class ShadowTreeSapling : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileNoAttach[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileObjectData.newTile.Width = 1;
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.AnchorValidTiles = new int[1] { ModContent.TileType<ShadowGrass>() };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.DrawFlipHorizontal = true;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.RandomStyleRange = 3;
		TileObjectData.addTile((int)((ModTile)this).Type);
		base.sapling/* tModPorter Note: Removed. Use TileID.Sets.TreeSapling and TileID.Sets.CommonSapling instead */ = true;
		LocalizedText val = ((ModTile)this).CreateMapEntryName((string)null);
		// val.SetDefault("Sapling");
		((ModTile)this).AddMapEntry(new Color(200, 200, 200), val);
		base.DustType = 1;
		base.AdjTiles = new int[1] { 20 };
	}

	public override int SaplingGrowthType(ref int style)/* tModPorter Note: Removed. Use ModTree.SaplingGrowthType */
	{
		style = 0;
		return ModContent.TileType<ShadowTreeSapling>();
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void RandomUpdate(int i, int j)
	{
		if (WorldGen.genRand.Next(20) == 0)
		{
			bool flag = WorldGen.PlayerLOS(i, j);
			if (WorldGen.GrowTree(i, j) && flag)
			{
				WorldGen.TreeGrowFXCheck(i, j);
			}
		}
	}

	public override void SetSpriteEffects(int i, int j, ref SpriteEffects effects)
	{
		if (i % 2 == 1)
		{
			effects = SpriteEffects.FlipHorizontally;
		}
	}
}
