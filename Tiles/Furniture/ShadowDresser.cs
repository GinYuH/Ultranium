using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Furniture;

public class ShadowDresser : ModTile
{
	public override void SetDefaults()
	{
		Main.tileSolidTop[((ModTile)this).Type] = true;
		Main.tileFrameImportant[((ModTile)this).Type] = true;
		Main.tileNoAttach[((ModTile)this).Type] = true;
		Main.tileTable[((ModTile)this).Type] = true;
		Main.tileContainer[((ModTile)this).Type] = true;
		Main.tileLavaDeath[((ModTile)this).Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.Origin = new Point16(1, 1);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
		TileObjectData.newTile.HookCheck = new PlacementHook((Func<int, int, int, int, int, int>)Chest.FindEmptyChest, -1, 0, true);
		TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook((Func<int, int, int, int, int, int>)Chest.AfterPlacement_Hook, -1, 0, false);
		TileObjectData.newTile.AnchorInvalidTiles = new int[1] { 127 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.addTile((int)((ModTile)this).Type);
		((ModTile)this).AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
		((ModTile)this).AddMapEntry(new Color(121, 14, 203), (LocalizedText)null);
		base.disableSmartCursor = true;
		base.adjTiles = new int[1] { 88 };
		base.dresser = "Dresser";
		base.dresserDrop = ((ModTile)this).mod.ItemType("ShadowDresserItem");
	}

	public override bool NewRightClick(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY == 0)
		{
			Main.CancelClothesWindow(quiet: true);
			Main.mouseRightRelease = false;
			int num = Main.tile[Player.tileTargetX, Player.tileTargetY].frameX / 18;
			num %= 3;
			num = Player.tileTargetX - num;
			int num2 = Player.tileTargetY - Main.tile[Player.tileTargetX, Player.tileTargetY].frameY / 18;
			if (localPlayer.sign > -1)
			{
				Main.PlaySound(11, -1, -1, 1, 1f, 0f);
				localPlayer.sign = -1;
				Main.editSign = false;
				Main.npcChatText = string.Empty;
			}
			if (Main.editChest)
			{
				Main.PlaySound(12, -1, -1, 1, 1f, 0f);
				Main.editChest = false;
				Main.npcChatText = string.Empty;
			}
			if (localPlayer.editedChestName)
			{
				localPlayer.editedChestName = false;
			}
			if (Main.netMode == 1)
			{
				if (num == localPlayer.chestX && num2 == localPlayer.chestY && localPlayer.chest != -1)
				{
					localPlayer.chest = -1;
					Recipe.FindRecipes();
					Main.PlaySound(11, -1, -1, 1, 1f, 0f);
				}
				else
				{
					NetMessage.SendData(31, -1, -1, null, num, num2);
					Main.stackSplit = 600;
				}
			}
			else
			{
				localPlayer.flyingPigChest = -1;
				int num3 = Chest.FindChest(num, num2);
				if (num3 != -1)
				{
					Main.stackSplit = 600;
					if (num3 == localPlayer.chest)
					{
						localPlayer.chest = -1;
						Recipe.FindRecipes();
						Main.PlaySound(11, -1, -1, 1, 1f, 0f);
					}
					else if (num3 != localPlayer.chest && localPlayer.chest == -1)
					{
						localPlayer.chest = num3;
						Main.playerInventory = true;
						Main.recBigList = false;
						Main.PlaySound(10, -1, -1, 1, 1f, 0f);
						localPlayer.chestX = num;
						localPlayer.chestY = num2;
					}
					else
					{
						localPlayer.chest = num3;
						Main.playerInventory = true;
						Main.recBigList = false;
						Main.PlaySound(12, -1, -1, 1, 1f, 0f);
						localPlayer.chestX = num;
						localPlayer.chestY = num2;
					}
					Recipe.FindRecipes();
				}
			}
		}
		else
		{
			Main.playerInventory = false;
			localPlayer.chest = -1;
			Recipe.FindRecipes();
			Main.dresserX = Player.tileTargetX;
			Main.dresserY = Player.tileTargetY;
			Main.OpenClothesWindow();
		}
		return true;
	}

	public override void MouseOverFar(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
		int tileTargetX = Player.tileTargetX;
		int num = Player.tileTargetY;
		int x = tileTargetX - tile.frameX % 54 / 18;
		if (tile.frameY % 36 != 0)
		{
			num--;
		}
		int num2 = Chest.FindChest(x, num);
		localPlayer.showItemIcon2 = -1;
		if (num2 < 0)
		{
			localPlayer.showItemIconText = Language.GetTextValue("LegacyDresserType.0");
		}
		else
		{
			if (Main.chest[num2].name != "")
			{
				localPlayer.showItemIconText = Main.chest[num2].name;
			}
			else
			{
				localPlayer.showItemIconText = base.chest;
			}
			if (localPlayer.showItemIconText == base.chest)
			{
				localPlayer.showItemIcon2 = ((ModTile)this).mod.ItemType("ShadowDresserItem");
				localPlayer.showItemIconText = "";
			}
		}
		localPlayer.noThrow = 2;
		localPlayer.showItemIcon = true;
		if (localPlayer.showItemIconText == "")
		{
			localPlayer.showItemIcon = false;
			localPlayer.showItemIcon2 = 0;
		}
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
		int tileTargetX = Player.tileTargetX;
		int num = Player.tileTargetY;
		int x = tileTargetX - tile.frameX % 54 / 18;
		if (tile.frameY % 36 != 0)
		{
			num--;
		}
		int num2 = Chest.FindChest(x, num);
		localPlayer.showItemIcon2 = -1;
		if (num2 < 0)
		{
			localPlayer.showItemIconText = Language.GetTextValue("LegacyDresserType.0");
		}
		else
		{
			if (Main.chest[num2].name != "")
			{
				localPlayer.showItemIconText = Main.chest[num2].name;
			}
			else
			{
				localPlayer.showItemIconText = base.chest;
			}
			if (localPlayer.showItemIconText == base.chest)
			{
				localPlayer.showItemIcon2 = ((ModTile)this).mod.ItemType("ShadowDresserItem");
				localPlayer.showItemIconText = "";
			}
		}
		localPlayer.noThrow = 2;
		localPlayer.showItemIcon = true;
		if (Main.tile[Player.tileTargetX, Player.tileTargetY].frameY > 0)
		{
			localPlayer.showItemIcon2 = 269;
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Item.NewItem(i * 16, j * 16, 48, 32, base.dresserDrop, 1, false, 0, false, false);
		Chest.DestroyChest(i, j);
	}
}
