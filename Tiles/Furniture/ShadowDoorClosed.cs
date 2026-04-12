using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Furniture;

public class ShadowDoorClosed : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[Type] = true;
		Main.tileBlockLight[Type] = true;
		Main.tileSolid[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileID.Sets.NotReallySolid[Type] = true;
		TileID.Sets.DrawsWalls[Type] = true;
		TileID.Sets.HasOutlines[Type] = true;
		TileObjectData.newTile.Width = 1;
		TileObjectData.newTile.Height = 3;
		TileObjectData.newTile.Origin = new Point16(0, 0);
		TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.UsesCustomCanPlace = true;
		TileObjectData.newTile.LavaDeath = true;
		TileObjectData.newTile.CoordinateHeights = new int[3] { 16, 16, 16 };
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Origin = new Point16(0, 1);
		TileObjectData.addAlternate(0);
		TileObjectData.newAlternate.CopyFrom(TileObjectData.newTile);
		TileObjectData.newAlternate.Origin = new Point16(0, 2);
		TileObjectData.addAlternate(0);
		TileObjectData.addTile((int)Type);
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsDoor);
		LocalizedText val = CreateMapEntryName();
		// val.SetDefault("Door");
		AddMapEntry(new Color(31, 34, 40), val);
		TileID.Sets.DisableSmartCursor[Type] = true;
		base.AdjTiles = new int[1] { 10 };
		TileID.Sets.OpenDoorID[Type] = Mod.Find<ModTile>("ShadowDoorOpen").Type;
	}

	public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
	{
		return true;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = 1;
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(null, i * 16, j * 16, 16, 48, Mod.Find<ModItem>("ShadowDoorItem").Type, 1, false, 0, false, false);
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = Mod.Find<ModItem>("ShadowDoorItem").Type;
	}
}
