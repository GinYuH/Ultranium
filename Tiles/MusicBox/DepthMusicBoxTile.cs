using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.MusicBox;

public class DepthMusicBoxTile : ModTile
{
	public override void SetDefaults()
	{
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileObsidianKill[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile((int)((ModTile)this).Type);
		base.disableSmartCursor = true;
		ModTranslation val = ((ModTile)this).CreateMapEntryName((string)null);
		val.SetDefault("Music Box");
		((ModTile)this).AddMapEntry(new Color(200, 200, 200), val);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(i * 16, j * 16, 16, 48, ((ModTile)this).mod.ItemType("DepthMusicBox"), 1, false, 0, false, false);
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.showItemIcon = true;
		localPlayer.showItemIcon2 = ((ModTile)this).mod.ItemType("DepthMusicBox");
	}
}
