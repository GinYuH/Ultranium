using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Ambient;

public class GlowShroom : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileCut[((ModTile)this).Type] = true;
		Main.tileNoFail[((ModTile)this).Type] = true;
		base.DustType = ((ModTile)this).Mod.Find<ModDust>("ShadowDustPurple").Type;
		((ModTile)this).AddMapEntry(new Color(58, 11, 67), (LocalizedText)null);
		TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
		TileObjectData.newTile.AnchorValidTiles = new int[3]
		{
			((ModTile)this).Mod.Find<ModTile>("ShadowGrass").Type,
			((ModTile)this).Mod.Find<ModTile>("ShadowSoil").Type,
			((ModTile)this).Mod.Find<ModTile>("DepthRock").Type
		};
		TileObjectData.newTile.AnchorAlternateTiles = new int[2] { 78, 380 };
		TileObjectData.addTile((int)((ModTile)this).Type);
		RegisterItemDrop(((ModTile)this).Mod.Find<ModItem>("GlowShroomItem").Type);
    }

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.6f;
		g = 0.1f;
		b = 0.6f;
	}

    public override bool CanDrop(int i, int j)
    {
		return Main.rand.Next(3) == 1;
    }

	public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
	{
		if (i % 2 == 1)
		{
			spriteEffects = SpriteEffects.FlipHorizontally;
		}
	}

	public override void RandomUpdate(int i, int j)
	{
		if (Main.tile[i, j].TileFrameX == 0)
		{
			Main.tile[i, j].TileFrameX += 18;
		}
		else if (Main.tile[i, j].TileFrameX == 18)
		{
			Main.tile[i, j].TileFrameX += 18;
		}
	}
}
