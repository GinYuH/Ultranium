using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Furniture;

public class ShadowClock : ModTile
{
	public override void SetDefaults()
	{
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileNoAttach[((ModTile)this).Type] = true;
		TileID.Sets.HasOutlines[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
		TileObjectData.newTile.Height = 5;
		TileObjectData.newTile.CoordinateHeights = new int[5] { 16, 16, 16, 16, 16 };
		TileObjectData.addTile((int)((ModTile)this).Type);
		ModTranslation val = ((ModTile)this).CreateMapEntryName((string)null);
		val.SetDefault("Clock");
		base.disableSmartCursor = true;
		((ModTile)this).AddMapEntry(new Color(31, 34, 40), val);
		base.adjTiles = new int[1] { 104 };
		base.animationFrameHeight = 88;
	}

	public override void AnimateTile(ref int frame, ref int frameCounter)
	{
		frameCounter++;
		if (frameCounter > 25)
		{
			frameCounter = 0;
			frame++;
			if (frame > 3)
			{
				frame = 0;
			}
		}
	}

	public override bool NewRightClick(int x, int y)
	{
		string text = "AM";
		double num = Main.time;
		if (!Main.dayTime)
		{
			num += 54000.0;
		}
		num = num / 86400.0 * 24.0;
		num = num - 7.5 - 12.0;
		if (num < 0.0)
		{
			num += 24.0;
		}
		if (num >= 12.0)
		{
			text = "PM";
		}
		int num2 = (int)num;
		double num3 = (int)((num - (double)num2) * 60.0);
		string text2 = string.Concat(num3);
		if (num3 < 10.0)
		{
			text2 = "0" + text2;
		}
		if (num2 > 12)
		{
			num2 -= 12;
		}
		if (num2 == 0)
		{
			num2 = 12;
		}
		Main.NewText("Time: " + num2 + ":" + text2 + " " + text, byte.MaxValue, (byte)240, (byte)20, false);
		return true;
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (closer)
		{
			Main.clock = true;
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(i * 16, j * 16, 48, 32, ((ModTile)this).mod.ItemType("ShadowClockItem"), 1, false, 0, false, false);
	}
}
