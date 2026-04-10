using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.ShadowEvent;

public class ShadowEventSpawns : GlobalNPC
{
	public static bool DisabledSpawns;

	public override void EditSpawnPool(IDictionary<int, float> pool, NPCSpawnInfo spawnInfo)
	{
		if (!ShadowEventWorld.ShadowEventActive)
		{
			return;
		}
		pool.Clear();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		int num7 = 0;
		int num8 = 0;
		int num9 = 0;
		for (int i = 0; i < 200; i++)
		{
			NPC nPC = Main.npc[i];
			if (((Entity)nPC).active)
			{
				if (nPC.type == ((GlobalNPC)this).mod.NPCType("AbyssalWraith"))
				{
					num++;
				}
				else if (nPC.type == ((GlobalNPC)this).mod.NPCType("ShadeSpirit"))
				{
					num2++;
				}
				else if (nPC.type == ((GlobalNPC)this).mod.NPCType("Scp2521"))
				{
					num3++;
				}
				else if (nPC.type == ((GlobalNPC)this).mod.NPCType("Phantom"))
				{
					num4++;
				}
				else if (nPC.type == ((GlobalNPC)this).mod.NPCType("FlayerWraith"))
				{
					num5++;
				}
				else if (nPC.type == ((GlobalNPC)this).mod.NPCType("ShadeMass"))
				{
					num6++;
				}
				else if (nPC.type == ((GlobalNPC)this).mod.NPCType("AbyssalCultist"))
				{
					num7++;
				}
				else if (nPC.type == ((GlobalNPC)this).mod.NPCType("Warden"))
				{
					num8++;
				}
				else if (nPC.type == ((GlobalNPC)this).mod.NPCType("MotherPhantom"))
				{
					num9++;
				}
			}
		}
		if (!ShadowEventWorld.Phase2)
		{
			if (num > 4)
			{
				pool.Add(((GlobalNPC)this).mod.NPCType("AbyssalWraith"), 0f);
			}
			else
			{
				pool.Add(((GlobalNPC)this).mod.NPCType("AbyssalWraith"), 0.4f);
			}
			if (num2 > 3)
			{
				pool.Add(((GlobalNPC)this).mod.NPCType("ShadeSpirit"), 0f);
			}
			else
			{
				pool.Add(((GlobalNPC)this).mod.NPCType("ShadeSpirit"), 0.5f);
			}
			if (num3 > 6)
			{
				pool.Add(((GlobalNPC)this).mod.NPCType("Scp2521"), 0f);
			}
			else
			{
				pool.Add(((GlobalNPC)this).mod.NPCType("Scp2521"), 0.5f);
			}
			if (num4 > 3)
			{
				pool.Add(((GlobalNPC)this).mod.NPCType("Phantom"), 0f);
			}
			else
			{
				pool.Add(((GlobalNPC)this).mod.NPCType("Phantom"), 0.5f);
			}
		}
		if (!ShadowEventWorld.Phase2)
		{
			return;
		}
		if (num5 > 4)
		{
			pool.Add(((GlobalNPC)this).mod.NPCType("FlayerWraith"), 0f);
		}
		else
		{
			pool.Add(((GlobalNPC)this).mod.NPCType("FlayerWraith"), 0.4f);
		}
		if (num6 > 3)
		{
			pool.Add(((GlobalNPC)this).mod.NPCType("ShadeMass"), 0f);
		}
		else
		{
			pool.Add(((GlobalNPC)this).mod.NPCType("ShadeMass"), 0.25f);
		}
		if (num7 > 2)
		{
			pool.Add(((GlobalNPC)this).mod.NPCType("AbyssalCultist"), 0f);
		}
		else
		{
			pool.Add(((GlobalNPC)this).mod.NPCType("AbyssalCultist"), 0.5f);
		}
		if (num8 > 0)
		{
			pool.Add(((GlobalNPC)this).mod.NPCType("Warden"), 0f);
		}
		else
		{
			pool.Add(((GlobalNPC)this).mod.NPCType("Warden"), 0.07f);
		}
		if (ShadowEventWorld.EventTimer > 16200 && ShadowEventWorld.EventTimer < 20000)
		{
			if (num9 > 0)
			{
				pool.Add(((GlobalNPC)this).mod.NPCType("MotherPhantom"), 0f);
			}
			else
			{
				pool.Add(((GlobalNPC)this).mod.NPCType("MotherPhantom"), 0.03f);
			}
		}
	}

	public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
	{
		if (ShadowEventWorld.ShadowEventActive)
		{
			spawnRate = 60;
			maxSpawns = 100;
			if (NPC.AnyNPCs(((GlobalNPC)this).mod.NPCType("ErebusHead")) || NPC.AnyNPCs(((GlobalNPC)this).mod.NPCType("MindFlayer")))
			{
				spawnRate = 0;
				maxSpawns = 0;
			}
			if (DisabledSpawns)
			{
				spawnRate = 0;
				maxSpawns = 0;
			}
		}
	}

	public override void PostAI(NPC npc)
	{
		if (!ShadowEventWorld.ShadowEventActive)
		{
			return;
		}
		bool flag = false;
		if (npc.type != ((GlobalNPC)this).mod.NPCType("ErebusHead") || npc.type != ((GlobalNPC)this).mod.NPCType("ErebusBody"))
		{
			flag = Vector2.Distance(Main.player[npc.target].Center, npc.Center) >= 20000000f;
		}
		if (Main.invasionX == (double)Main.spawnTileX)
		{
			if (!flag)
			{
				npc.timeLeft = 500;
			}
			else if (flag && !npc.townNPC)
			{
				((Entity)npc).active = false;
			}
		}
	}
}
