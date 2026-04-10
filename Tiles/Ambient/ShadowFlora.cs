using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Ambient;

internal class ShadowFlora : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolid[((ModTile)this).Type] = false;
		Main.tileSolidTop[((ModTile)this).Type] = false;
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileCut[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.addTile((int)((ModTile)this).Type);
		base.DustType = 89;
		((ModTile)this).AddMapEntry(new Color(19, 121, 95), (LocalizedText)null);
		base.HitSound = 6;
		base.soundStyle/* tModPorter Note: Removed. Integrate into HitSound */ = 1;
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
