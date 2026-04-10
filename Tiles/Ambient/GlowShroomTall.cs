using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Ambient;

public class GlowShroomTall : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileNoFail[((ModTile)this).Type] = true;
		Main.tileMergeDirt[((ModTile)this).Type] = true;
		base.DustType = ((ModTile)this).Mod.Find<ModDust>("ShadowDustPurple").Type;
		base.HitSound = 6;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.addTile((int)((ModTile)this).Type);
		((ModTile)this).AddMapEntry(new Color(58, 11, 67), (LocalizedText)null);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.8f;
		g = 0.1f;
		b = 0.8f;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = 2;
	}

	public override void SetDrawPositions(int i, int j, ref int width, ref int offsetY, ref int height, ref short tileFrameX, ref short tileFrameY)
	{
		offsetY = 2;
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		Tile tileSafely = Framing.GetTileSafely(i, j + 2);
		if (!tileSafely.HasTile || tileSafely.IsHalfBlock || tileSafely.TopSlope)
		{
			WorldGen.KillTile(i, j);
		}
		return true;
	}
}
