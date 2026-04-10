using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Ambient.Purple;

public class PurpleGlowShroom : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileCut[((ModTile)this).Type] = true;
		Main.tileNoFail[((ModTile)this).Type] = true;
		base.DustType = ((ModTile)this).Mod.Find<ModDust>("ShadowDustPurple").Type;
		((ModTile)this).AddMapEntry(new Color(52, 6, 40), (LocalizedText)null);
		TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
		TileObjectData.newTile.AnchorValidTiles = new int[1] { ((ModTile)this).Mod.Find<ModTile>("PurpleShadowGrass").Type };
		TileObjectData.newTile.AnchorAlternateTiles = new int[2] { 78, 380 };
		TileObjectData.addTile((int)((ModTile)this).Type);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.6f;
		g = 0.1f;
		b = 0.6f;
	}

	public override bool Drop(int i, int j)/* tModPorter Note: Removed. Use CanDrop to decide if an item should drop. Use GetItemDrops to decide which item drops. Item drops based on placeStyle are handled automatically now, so this method might be able to be removed altogether. */
	{
		if (Main.rand.Next(3) == 1)
		{
			Item.NewItem(i * 16, j * 16, 64, 32, ((ModTile)this).Mod.Find<ModItem>("GlowShroomItem").Type, 1, false, 0, false, false);
		}
		return false;
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
