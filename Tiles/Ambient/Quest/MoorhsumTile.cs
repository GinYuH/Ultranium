using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Ambient.Quest;

public class MoorhsumTile : ModTile
{
	public override void SetDefaults()
	{
		Main.tileBlockLight[((ModTile)this).Type] = true;
		Main.tileLighted[((ModTile)this).Type] = true;
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileCut[((ModTile)this).Type] = true;
		Main.tileNoFail[((ModTile)this).Type] = true;
		((ModTile)this).AddMapEntry(new Color(51, 49, 95), (LocalizedText)null);
		TileObjectData.newTile.CopyFrom(TileObjectData.StyleAlch);
		TileObjectData.newTile.AnchorValidTiles = new int[1] { 70 };
		TileObjectData.newTile.AnchorAlternateTiles = new int[2] { 78, 380 };
		TileObjectData.addTile((int)((ModTile)this).Type);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.85f;
		g = 0.2f;
		b = 0.85f;
	}

	public override bool Drop(int i, int j)
	{
		Item.NewItem(i * 16, j * 16, 64, 32, ((ModTile)this).mod.ItemType("Moorhsum"), 1, false, 0, false, false);
		if (!UltraniumWorld.Moorhsum)
		{
			UltraniumWorld.Moorhsum = true;
			UltraniumWorld.StrangeUndergrowth = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
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
		if (Main.tile[i, j].frameX == 0)
		{
			Main.tile[i, j].frameX += 18;
		}
		else if (Main.tile[i, j].frameX == 18)
		{
			Main.tile[i, j].frameX += 18;
		}
	}
}
