using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Trophy;

public class FlayerTrophy : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleWrapLimit = 36;
		TileObjectData.addTile((int)((ModTile)this).Type);
		base.DustType = 7;
		base.disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
		LocalizedText val = ((ModTile)this).CreateMapEntryName((string)null);
		// val.SetDefault("Trophy");
		((ModTile)this).AddMapEntry(new Color(120, 85, 60), val);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		int num = 0;
		if (frameX / 54 == 0)
		{
			num = ((ModTile)this).Mod.Find<ModItem>("FlayerTrophyItem").Type;
		}
		if (num > 0)
		{
			Item.NewItem(i * 16, j * 16, 48, 48, num, 1, false, 0, false, false);
		}
	}
}
