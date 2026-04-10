using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Furniture;

public class DepthCrateTile : ModTile
{
	public override void SetDefaults()
	{
		Main.tileTable[((ModTile)this).Type] = true;
		Main.tileSolidTop[((ModTile)this).Type] = true;
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileNoAttach[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.addTile((int)((ModTile)this).Type);
		ModTranslation val = ((ModTile)this).CreateMapEntryName((string)null);
		val.SetDefault("Crate");
		((ModTile)this).AddMapEntry(new Color(150, 150, 150), val);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(i * 16, j * 16, 64, 32, ((ModTile)this).mod.ItemType("DepthCrate"), 1, false, 0, false, false);
	}
}
