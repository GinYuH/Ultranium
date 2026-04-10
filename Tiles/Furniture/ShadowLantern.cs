using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Furniture;

public class ShadowLantern : ModTile
{
	public override void SetDefaults()
	{
		Main.tileSolidTop[((ModTile)this).Type] = false;
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileNoAttach[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = false;
		Main.tileLighted[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
		((ModTile)this).AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTorch);
		ModTranslation val = ((ModTile)this).CreateMapEntryName((string)null);
		val.SetDefault("Lantern");
		((ModTile)this).AddMapEntry(new Color(31, 34, 40), val);
		base.disableSmartCursor = true;
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.addTile((int)((ModTile)this).Type);
		base.soundStyle = 21;
	}

	public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
	{
		if (i % 2 == 1)
		{
			spriteEffects = SpriteEffects.FlipHorizontally;
		}
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		SpriteEffects effects = SpriteEffects.None;
		if (i % 2 == 1)
		{
			effects = SpriteEffects.FlipHorizontally;
		}
		Vector2 vector = new Vector2(Main.offScreenRange, Main.offScreenRange);
		if (Main.drawToScreen)
		{
			vector = Vector2.Zero;
		}
		Tile tile = Main.tile[i, j];
		int num = 8;
		int num2 = 0;
		int height = 8;
		TileLoader.SetDrawPositions(i, j, ref num, ref num2, ref height);
		Texture2D texture = ((ModTile)this).mod.GetTexture("Tiles/Furniture/ShadowLantern_Flame");
		ulong seed = Main.TileFrameSeed ^ (ulong)(((long)j << 32) | (uint)i);
		for (int k = 0; k < 7; k++)
		{
			float num3 = (float)Utils.RandomInt(ref seed, -10, 11) * 0.15f;
			float num4 = (float)Utils.RandomInt(ref seed, -10, 1) * 0.35f;
			Main.spriteBatch.Draw(texture, new Vector2((float)(i * 16 - (int)Main.screenPosition.X) - ((float)num - 16f) / 2f + num3, (float)(j * 16 - (int)Main.screenPosition.Y + num2) + num4) + vector, new Rectangle(tile.frameX, tile.frameY, num, height), new Color(100, 100, 100, 0), 0f, default(Vector2), 1f, effects, 0f);
		}
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(i * 16, j * 16, 16, 32, ((ModTile)this).mod.ItemType("ShadowLanternItem"), 1, false, 0, false, false);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.25f;
		g = 0f;
		b = 0.5f;
	}
}
