using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Furniture;

public class ShadowBed : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileID.Sets.HasOutlines[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style4x2);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 18 };
		TileObjectData.addTile((int)((ModTile)this).Type);
		LocalizedText val = ((ModTile)this).CreateMapEntryName((string)null);
		((ModTile)this).AddMapEntry(new Color(31, 34, 40), val);
		// val.SetDefault("Bed");
		base.disableSmartCursor/* tModPorter Note: Removed. Use TileID.Sets.DisableSmartCursor instead */ = true;
		base.AdjTiles = new int[1] { 79 };
		base.bed/* tModPorter Note: Removed. Use TileID.Sets.CanBeSleptIn instead */ = true;
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
		Item.NewItem(null, i * 16, j * 16, 64, 32, ModContent.ItemType<ShadowBedItem>(), 1, false, 0, false, false);
	}

	public override bool RightClick(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		Tile tile = Main.tile[i, j];
		int num = i - tile.TileFrameX / 18;
		int num2 = j + 2;
		num += ((tile.TileFrameX >= 72) ? 5 : 2);
		if (tile.TileFrameY % 38 != 0)
		{
			num2--;
		}
		localPlayer.FindSpawn();
		if (localPlayer.SpawnX == num && localPlayer.SpawnY == num2)
		{
			localPlayer.RemoveSpawn();
			Main.NewText("Spawn point removed!", byte.MaxValue, (byte)240, (byte)20, false);
		}
		else if (Player.CheckSpawn(num, num2))
		{
			localPlayer.ChangeSpawn(num, num2);
			Main.NewText("Spawn point set!", byte.MaxValue, (byte)240, (byte)20, false);
		}
		return true;
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		localPlayer.cursorItemIconID = ModContent.ItemType<ShadowBedItem>();
	}
}
