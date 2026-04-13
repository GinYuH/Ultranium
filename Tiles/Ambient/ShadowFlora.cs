using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Ambient;

internal class ShadowFlora : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolid[Type] = false;
		Main.tileSolidTop[Type] = false;
		Main.tileFrameImportant[Type] = true;
		Main.tileCut[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.addTile((int)Type);
		base.DustType = DustID.GemEmerald;
		AddMapEntry(new Color(19, 121, 95), (LocalizedText)null);
		base.HitSound = SoundID.Grass;
		//base.soundStyle/* tModPorter Note: Removed. Integrate into HitSound */ = 1;
	}

	public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
	{
		if (i % 2 == 1)
		{
			spriteEffects = SpriteEffects.FlipHorizontally;
		}
	}

	public override bool HasWalkDust()
	{
		return true;
	}
}
