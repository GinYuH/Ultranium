using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Furniture;

internal class ShadowLamp : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileLighted[((ModTile)this).Type] = true;
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileNoAttach[((ModTile)this).Type] = true;
		Main.tileWaterDeath[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style1xX);
		TileObjectData.newTile.WaterDeath = true;
		TileObjectData.newTile.WaterPlacement = LiquidPlacement.NotAllowed;
		TileObjectData.newTile.LavaPlacement = LiquidPlacement.NotAllowed;
		TileObjectData.addTile((int)((ModTile)this).Type);
		((ModTile)this).AddMapEntry(new Color(31, 34, 40), Language.GetText("MapObject.FloorLamp"));
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(null, i * 16, j * 16, 16, 48, ModContent.ItemType<ShadowLampItem>(), 1, false, 0, false, false);
	}

	public override void HitWire(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		int num = j - tile.TileFrameY / 18 % 3;
		short num2 = (short)((tile.TileFrameX > 0) ? (-18) : 18);
		Main.tile[i, num].TileFrameX += num2;
		Main.tile[i, num + 1].TileFrameX += num2;
		Main.tile[i, num + 2].TileFrameX += num2;
		Wiring.SkipWire(i, num);
		Wiring.SkipWire(i, num + 1);
		Wiring.SkipWire(i, num + 2);
		NetMessage.SendTileSquare(-1, i, num + 1, 3);
	}

	public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
	{
		if (i % 2 == 1)
		{
			spriteEffects = SpriteEffects.FlipHorizontally;
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		if (Main.tile[i, j].TileFrameX == 0)
		{
			r = 0.25f;
			g = 0f;
			b = 0.5f;
		}
	}

	public override void DrawEffects(int i, int j, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
	{
		if (Main.gamePaused || !Main.instance.IsActive || (Lighting.UpdateEveryFrame && !Utils.NextBool(Main.rand, 4)))
		{
			return;
		}
		Tile tile = Main.tile[i, j];
		short frameX = tile.TileFrameX;
		short frameY = tile.TileFrameY;
		if (!Utils.NextBool(Main.rand, 40) || frameX != 0)
		{
			return;
		}
		int num = frameY / 54;
		if (frameY / 18 % 3 != 0)
		{
			return;
		}
		int num2 = -1;
		if (num == 0)
		{
			num2 = 21;
		}
		if (num2 != -1)
		{
			int num3 = Dust.NewDust(new Vector2(i * 16 + 4, j * 16 + 2), 4, 4, num2, 0f, 0f, 100);
			if (Main.rand.Next(3) != 0)
			{
				Main.dust[num3].noGravity = true;
			}
			Main.dust[num3].velocity *= 0.3f;
			Main.dust[num3].velocity.Y = Main.dust[num3].velocity.Y - 1.5f;
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
		int num = 16;
		int num2 = 0;
		int height = 16;
		TileLoader.SetDrawPositions(i, j, ref num, ref num2, ref height);
		Texture2D texture = ((ModTile)this).Mod.GetTexture("Tiles/Furniture/ShadowLamp_Flame");
		ulong seed = Main.TileFrameSeed ^ (ulong)(((long)j << 32) | (uint)i);
		for (int k = 0; k < 7; k++)
		{
			float num3 = (float)Utils.RandomInt(ref seed, -10, 11) * 0.15f;
			float num4 = (float)Utils.RandomInt(ref seed, -10, 1) * 0.35f;
			Main.spriteBatch.Draw(texture, new Vector2((float)(i * 16 - (int)Main.screenPosition.X) - ((float)num - 16f) / 2f + num3, (float)(j * 16 - (int)Main.screenPosition.Y + num2) + num4) + vector, new Rectangle(tile.TileFrameX, tile.TileFrameY, num, height), new Color(100, 100, 100, 0), 0f, default(Vector2), 1f, effects, 0f);
		}
	}
}
