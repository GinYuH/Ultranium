using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Ambient.Purple;

public class PurpleGlowShroomTall : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileBlockLight[Type] = true;
		Main.tileLighted[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoFail[Type] = true;
		Main.tileMergeDirt[Type] = true;
		DustType = Mod.Find<ModDust>("ShadowDustPurple").Type;
		HitSound = SoundID.Grass;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
		TileObjectData.newTile.Height = 2;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.addTile((int)Type);
		AddMapEntry(new Color(52, 6, 40), (LocalizedText)null);
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
