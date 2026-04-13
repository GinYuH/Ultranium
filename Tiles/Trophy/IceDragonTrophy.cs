using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Trophy;

public class IceDragonTrophy : ModTile
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
}
