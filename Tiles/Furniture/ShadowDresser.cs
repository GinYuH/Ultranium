using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Ultranium.Tiles.Furniture;

public class ShadowDresser : ModTile
{
	public override void SetStaticDefaults()
	{
		Main.tileSolidTop[Type] = true;
		Main.tileFrameImportant[Type] = true;
		Main.tileNoAttach[Type] = true;
		Main.tileTable[Type] = true;
		Main.tileContainer[Type] = true;
		Main.tileLavaDeath[Type] = true;
		TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
		TileObjectData.newTile.Origin = new Point16(1, 1);
		TileObjectData.newTile.CoordinateHeights = new int[2] { 16, 16 };
        TileObjectData.newTile.HookCheckIfCanPlace = new PlacementHook(new Func<int, int, int, int, int, int, int>(Chest.FindEmptyChest), -1, 0, true);
        TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(new Func<int, int, int, int, int, int, int>(Chest.AfterPlacement_Hook), -1, 0, false);
        TileObjectData.newTile.AnchorInvalidTiles = new int[1] { 127 };
		TileObjectData.newTile.StyleHorizontal = true;
		TileObjectData.newTile.LavaDeath = false;
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, TileObjectData.newTile.Width, 0);
		TileObjectData.addTile((int)Type);
		AddToArray(ref TileID.Sets.RoomNeeds.CountsAsTable);
		AddMapEntry(new Color(121, 14, 203), (LocalizedText)null);
		TileID.Sets.DisableSmartCursor[Type] = true;
		base.AdjTiles = new int[1] { 88 };
		TileID.Sets.BasicDresser[Type] = true;
	}

	public override bool RightClick(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		if (Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameY == 0)
		{
			Main.CancelClothesWindow(quiet: true);
			Main.mouseRightRelease = false;
			int num = Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameX / 18;
			num %= 3;
			num = Player.tileTargetX - num;
			int num2 = Player.tileTargetY - Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameY / 18;
			if (localPlayer.sign > -1)
			{
				SoundEngine.PlaySound(SoundID.MenuClose);
				localPlayer.sign = -1;
				Main.editSign = false;
				Main.npcChatText = string.Empty;
			}
			if (Main.editChest)
			{
				SoundEngine.PlaySound(SoundID.MenuTick);
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
					SoundEngine.PlaySound(SoundID.MenuClose);
				}
				else
				{
					NetMessage.SendData(31, -1, -1, null, num, num2);
					Main.stackSplit = 600;
				}
			}
			else
            {
                localPlayer.piggyBankProjTracker.Clear();
                localPlayer.voidLensChest.Clear();
                int num3 = Chest.FindChest(num, num2);
				if (num3 != -1)
				{
					Main.stackSplit = 600;
					if (num3 == localPlayer.chest)
					{
						localPlayer.chest = -1;
						Recipe.FindRecipes();
						SoundEngine.PlaySound(SoundID.MenuClose);
					}
					else if (num3 != localPlayer.chest && localPlayer.chest == -1)
					{
						localPlayer.chest = num3;
						Main.playerInventory = true;
						Main.recBigList = false;
						SoundEngine.PlaySound(SoundID.MenuOpen);
						localPlayer.chestX = num;
						localPlayer.chestY = num2;
					}
					else
					{
						localPlayer.chest = num3;
						Main.playerInventory = true;
						Main.recBigList = false;
						SoundEngine.PlaySound(SoundID.MenuTick);
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
			Main.interactedDresserTopLeftX = Player.tileTargetX;
			Main.interactedDresserTopLeftY = Player.tileTargetY;
			Main.OpenClothesWindow();
		}
		return true;
	}

	public override void MouseOverFar(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
        string chestName = TileLoader.DefaultContainerName(tile.TileType, tile.TileFrameX, tile.TileFrameY);
        int tileTargetX = Player.tileTargetX;
		int num = Player.tileTargetY;
		int x = tileTargetX - tile.TileFrameX % 54 / 18;
		if (tile.TileFrameY % 36 != 0)
		{
			num--;
		}
		int num2 = Chest.FindChest(x, num);
		localPlayer.cursorItemIconID = -1;
		if (num2 < 0)
		{
			localPlayer.cursorItemIconText = Language.GetTextValue("LegacyDresserType.0");
		}
		else
		{
			if (Main.chest[num2].name != "")
			{
				localPlayer.cursorItemIconText = Main.chest[num2].name;
			}
			else
			{
				localPlayer.cursorItemIconText = chestName;
			}
			if (localPlayer.cursorItemIconText == chestName)
			{
				localPlayer.cursorItemIconID = Mod.Find<ModItem>("ShadowDresserItem").Type;
				localPlayer.cursorItemIconText = "";
			}
		}
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		if (localPlayer.cursorItemIconText == "")
		{
			localPlayer.cursorItemIconEnabled = false;
			localPlayer.cursorItemIconID = 0;
		}
	}

	public override void MouseOver(int i, int j)
	{
		Player localPlayer = Main.LocalPlayer;
		Tile tile = Main.tile[Player.tileTargetX, Player.tileTargetY];
        string chestName = TileLoader.DefaultContainerName(tile.TileType, tile.TileFrameX, tile.TileFrameY);
        int tileTargetX = Player.tileTargetX;
		int num = Player.tileTargetY;
		int x = tileTargetX - tile.TileFrameX % 54 / 18;
		if (tile.TileFrameY % 36 != 0)
		{
			num--;
		}
		int num2 = Chest.FindChest(x, num);
		localPlayer.cursorItemIconID = -1;
		if (num2 < 0)
		{
			localPlayer.cursorItemIconText = Language.GetTextValue("LegacyDresserType.0");
		}
		else
		{
			if (Main.chest[num2].name != "")
			{
				localPlayer.cursorItemIconText = Main.chest[num2].name;
			}
			else
			{
				localPlayer.cursorItemIconText = chestName;
			}
			if (localPlayer.cursorItemIconText == chestName)
			{
				localPlayer.cursorItemIconID = Mod.Find<ModItem>("ShadowDresserItem").Type;
				localPlayer.cursorItemIconText = "";
			}
		}
		localPlayer.noThrow = 2;
		localPlayer.cursorItemIconEnabled = true;
		if (Main.tile[Player.tileTargetX, Player.tileTargetY].TileFrameY > 0)
		{
			localPlayer.cursorItemIconID = 269;
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void KillMultiTile(int i, int j, int frameX, int frameY)
	{
		Chest.DestroyChest(i, j);
	}
}
