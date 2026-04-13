using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.MusicBox;

public class ZephyrMusicBoxTile : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileObsidianKill[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style2x2);
		TileObjectData.newTile.Origin = new Point16(0, 1);
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.DrawYOffset = 2;
		TileObjectData.addTile((int)Type);
        Terraria.ID.TileID.Sets.DisableSmartCursor[Type] = true;
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Music Box");
		AddMapEntry(new Color(200, 200, 200), val);
	}



	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = Mod.Find<ModItem>("ZephyrMusicBox").Type;
	}
}
