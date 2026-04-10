using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs;

public class GlobalDrops : GlobalNPC
{
	public override void NPCLoot(NPC npc)
	{
		if (npc.type == 35 && Main.dayTime)
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("SoulCrushingDisappointment"), 1, false, 0, false, false);
			if (!UltraniumWorld.SoulCrushingDisappointment)
			{
				UltraniumWorld.SoulCrushingDisappointment = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7);
				}
			}
		}
		if (npc.type == 160)
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("TruffleShroom"), 1, false, 0, false, false);
			if (!UltraniumWorld.TruffleShroom)
			{
				UltraniumWorld.TruffleShroom = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7);
				}
			}
		}
		if (Main.rand.Next(35) == 0 && (npc.type == 10 || npc.type == 95))
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("FakeWhoopieCushion"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(30) == 0 && (npc.type == 31 || npc.type == 294 || npc.type == 295 || npc.type == 296))
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("WaterJavelin"), 1, false, 0, false, false);
		}
		if (Main.bloodMoon && Main.rand.Next(3) < 1)
		{
			if (npc.type == 489)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("BloodClot"), Main.rand.Next(2, 4), false, 0, false, false);
			}
			if (npc.type == 490)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("BloodClot"), Main.rand.Next(2, 4), false, 0, false, false);
			}
			if (npc.type == 3)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("BloodClot"), Main.rand.Next(1, 2), false, 0, false, false);
			}
			if (npc.type == 2)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("BloodClot"), Main.rand.Next(1, 1), false, 0, false, false);
			}
		}
		if (Main.rand.Next(30) < 1 && npc.type == 32)
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("Drip"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(5) < 1 && npc.type == 370)
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("DukeYoyo"), 1, false, 0, false, false);
		}
		if (npc.type == 4)
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("StaffOfCthulhu"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(5) < 1 && npc.type == 245)
		{
			int num = Main.rand.Next(2);
			if (num == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("SolSpear"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("SolThrow"), 1, false, 0, false, false);
			}
		}
		if (Main.rand.Next(1) < 1)
		{
			if (npc.type == 56)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("Leaf"), Main.rand.Next(1, 3), false, 0, false, false);
			}
			if (npc.type == 43)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("Leaf"), Main.rand.Next(1, 3), false, 0, false, false);
			}
		}
		if (npc.type == 439)
		{
			int num2 = Main.rand.Next(3);
			if (num2 == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("LunaticFireStaff"), 1, false, 0, false, false);
			}
			if (num2 == 1)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("LunaticIceStaff"), 1, false, 0, false, false);
			}
			if (num2 == 2)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("LunaticStarStaff"), 1, false, 0, false, false);
			}
		}
		if (Main.rand.Next(3) < 1 && npc.type == 35)
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("Necrosis"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(20) < 1 && npc.type == 29)
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("ShadowRod"), 1, false, 0, false, false);
		}
		if (npc.type == 471)
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).mod.ItemType("ShadowFlame"), Main.rand.Next(6, 12), false, 0, false, false);
		}
		if (npc.type == 50)
		{
			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1309, 1, false, 0, false, false);
		}
	}
}
