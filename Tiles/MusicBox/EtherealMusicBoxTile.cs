using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.MusicBox;

public class EtherealMusicBoxTile : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileObsidianKill[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile((int)((ModTile)this).Type);
		base.disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
		LocalizedText val = ((ModTile)this).CreateMapEntryName((string)null);
		// val.SetDefault("Music Box");
		((ModTile)this).AddMapEntry(new Color(200, 200, 200), val);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(i * 16, j * 16, 16, 48, ((ModTile)this).Mod.Find<ModItem>("EtherealMusicBox").Type, 1, false, 0, false, false);
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ((ModTile)this).Mod.Find<ModItem>("EtherealMusicBox").Type;
	}
}
