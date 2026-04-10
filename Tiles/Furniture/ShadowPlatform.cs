using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Furniture;

public class ShadowPlatform : ModTile
{
	public override void SetDefaults()
	{
		Main.tileLighted[((ModTile)this).Type] = true;
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileSolidTop[((ModTile)this).Type] = true;
		Main.tileSolid[((ModTile)this).Type] = true;
		Main.tileNoAttach[((ModTile)this).Type] = true;
		Main.tileTable[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileID.Sets.Platforms[((ModTile)this).Type] = true;
		TileObjectData.newTile.CoordinateHeights = new int[1] { 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.StyleMultiplier = 27;
		TileObjectData.newTile.StyleWrapLimit = 27;
		TileObjectData.newTile.UsesCustomCanPlace = false;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.addTile((int)((ModTile)this).Type);
		((ModTile)this).AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
		((ModTile)this).AddMapEntry(new Color(31, 34, 40), (LocalizedText)null);
		base.drop = ((ModTile)this).mod.ItemType("ShadowPlatformItem");
		base.disableSmartCursor = true;
		base.adjTiles = new int[1] { 19 };
	}

	public override void PostSetDefaults()
	{
		Main.tileNoSunLight[((ModTile)this).Type] = false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
