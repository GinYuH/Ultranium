using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Trophy;

public class SquidTrophy : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3Wall);
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleWrapLimit = 36;
		TileObjectData.addTile((int)Type);
		base.DustType = 7;
        Terraria.ID.TileID.Sets.DisableSmartCursor[Type] = true;
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Trophy");
		AddMapEntry(new Color(120, 85, 60), val);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		int num = 0;
		if (frameX / 54 == 0)
		{
			num = Mod.Find<ModItem>("SquidTrophyItem").Type;
		}
		if (num > 0)
		{
			Item.NewItem(null, i * 16, j * 16, 48, 48, num, 1, false, 0, false, false);
		}
	}
}
