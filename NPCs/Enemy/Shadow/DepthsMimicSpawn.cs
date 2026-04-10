using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Shadow.Depths;

public class DepthsMimicSpawn : ModPlayer
{
	public int LastChest;

	public override void PreUpdateBuffs()
	{
		if (Main.netMode != 1)
		{
			if (((ModPlayer)this).Player.chest == -1 && LastChest >= 0 && Main.chest[LastChest] != null)
			{
				int x = Main.chest[LastChest].x;
				int y = Main.chest[LastChest].y;
				ChestItemSummonCheck(x, y, ((ModPlayer)this).Mod);
			}
			LastChest = ((ModPlayer)this).Player.chest;
		}
	}

	public static bool ChestItemSummonCheck(int x, int y, Mod mod)
	{
		if (Main.netMode == 1)
		{
			return false;
		}
		int num = Chest.FindChest(x, y);
		if (num < 0)
		{
			return false;
		}
		int num2 = 0;
		int num3 = 0;
		ushort type = Main.tile[Main.chest[num].x, Main.chest[num].y].TileType;
		int num4 = Main.tile[Main.chest[num].x, Main.chest[num].y].TileFrameX / 36;
		if (type == 21 && (num4 < 5 || num4 > 6))
		{
			for (int i = 0; i < 40; i++)
			{
				if (Main.chest[num].item[i] != null && Main.chest[num].item[i].type > 0)
				{
					if (Main.chest[num].item[i].type == mod.Find<ModItem>("DepthsKey").Type)
					{
						num2 += Main.chest[num].item[i].stack;
					}
					else
					{
						num3++;
					}
				}
			}
		}
		if (num3 == 0 && num2 == 1)
		{
			if (Main.tile[x, y].TileType == 21)
			{
				if (Main.tile[x, y].TileFrameX % 36 != 0)
				{
					x--;
				}
				if (Main.tile[x, y].TileFrameY % 36 != 0)
				{
					y--;
				}
				int number = Chest.FindChest(x, y);
				for (int j = x; j <= x + 1; j++)
				{
					for (int k = y; k <= y + 1; k++)
					{
						if (Main.tile[j, k].TileType == 21)
						{
							Main.tile[j, k].HasTile = false;
						}
					}
				}
				for (int l = 0; l < 40; l++)
				{
					Main.chest[num].item[l] = new Item();
				}
				Chest.DestroyChest(x, y);
				NetMessage.SendData(34, -1, -1, null, 1, x, y, 0f, number);
				NetMessage.SendTileSquare(-1, x, y, 3);
			}
			int num5 = mod.Find<ModNPC>("DepthsMimic").Type;
			int num6 = NPC.NewNPC(x * 16 + 16, y * 16 + 32, num5, 0, 0f, 0f, 0f, 0f, 255);
			Main.npc[num6].whoAmI = num6;
			NetMessage.SendData(23, -1, -1, null, num6);
			Main.npc[num6].BigMimicSpawnSmoke();
		}
		return false;
	}
}
