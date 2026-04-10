using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs;

public class GlobalDrops : GlobalNPC
{
	public override void OnKill(NPC npc)
	{
		if (npc.type == 35 && Main.dayTime)
		{
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("SoulCrushingDisappointment").Type, 1, false, 0, false, false);
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
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("TruffleShroom").Type, 1, false, 0, false, false);
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
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("FakeWhoopieCushion").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(30) == 0 && (npc.type == 31 || npc.type == 294 || npc.type == 295 || npc.type == 296))
		{
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("WaterJavelin").Type, 1, false, 0, false, false);
		}
		if (Main.bloodMoon && Main.rand.Next(3) < 1)
		{
			if (npc.type == 489)
			{
				Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("BloodClot").Type, Main.rand.Next(2, 4), false, 0, false, false);
			}
			if (npc.type == 490)
			{
				Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("BloodClot").Type, Main.rand.Next(2, 4), false, 0, false, false);
			}
			if (npc.type == 3)
			{
				Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("BloodClot").Type, Main.rand.Next(1, 2), false, 0, false, false);
			}
			if (npc.type == 2)
			{
				Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("BloodClot").Type, Main.rand.Next(1, 1), false, 0, false, false);
			}
		}
		if (Main.rand.Next(30) < 1 && npc.type == 32)
		{
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("Drip").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(5) < 1 && npc.type == 370)
		{
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("DukeYoyo").Type, 1, false, 0, false, false);
		}
		if (npc.type == 4)
		{
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("StaffOfCthulhu").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(5) < 1 && npc.type == 245)
		{
			int num = Main.rand.Next(2);
			if (num == 0)
			{
				Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("SolSpear").Type, 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("SolThrow").Type, 1, false, 0, false, false);
			}
		}
		if (Main.rand.Next(1) < 1)
		{
			if (npc.type == 56)
			{
				Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("Leaf").Type, Main.rand.Next(1, 3), false, 0, false, false);
			}
			if (npc.type == 43)
			{
				Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("Leaf").Type, Main.rand.Next(1, 3), false, 0, false, false);
			}
		}
		if (npc.type == 439)
		{
			int num2 = Main.rand.Next(3);
			if (num2 == 0)
			{
				Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("LunaticFireStaff").Type, 1, false, 0, false, false);
			}
			if (num2 == 1)
			{
				Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("LunaticIceStaff").Type, 1, false, 0, false, false);
			}
			if (num2 == 2)
			{
				Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("LunaticStarStaff").Type, 1, false, 0, false, false);
			}
		}
		if (Main.rand.Next(3) < 1 && npc.type == 35)
		{
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("Necrosis").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(20) < 1 && npc.type == 29)
		{
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("ShadowRod").Type, 1, false, 0, false, false);
		}
		if (npc.type == 471)
		{
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ((GlobalNPC)this).Mod.Find<ModItem>("ShadowFlame").Type, Main.rand.Next(6, 12), false, 0, false, false);
		}
		if (npc.type == 50)
		{
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, 1309, 1, false, 0, false, false);
		}
	}
}
