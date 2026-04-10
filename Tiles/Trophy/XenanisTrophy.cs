using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Trophy;

public class XenanisTrophy : ModTile
{
	public override void SetDefaults()
	{
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleWrapLimit = 36;
		TileObjectData.addTile((int)((ModTile)this).Type);
		base.dustType = 7;
		base.disableSmartCursor = true;
		ModTranslation val = ((ModTile)this).CreateMapEntryName((string)null);
		val.SetDefault("Trophy");
		((ModTile)this).AddMapEntry(new Color(120, 85, 60), val);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		int num = 0;
		if (frameX / 54 == 0)
		{
			num = ((ModTile)this).mod.ItemType("XenanisTrophyItem");
		}
		if (num > 0)
		{
			Item.NewItem(i * 16, j * 16, 48, 48, num, 1, false, 0, false, false);
		}
	}
}
