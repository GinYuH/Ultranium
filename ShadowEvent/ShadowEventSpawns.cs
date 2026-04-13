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
			if (nPC.active)
			{
				if (nPC.type == Mod.Find<ModNPC>("AbyssalWraith").Type)
				{
					num++;
				}
				else if (nPC.type == Mod.Find<ModNPC>("ShadeSpirit").Type)
				{
					num2++;
				}
				else if (nPC.type == Mod.Find<ModNPC>("Scp2521").Type)
				{
					num3++;
				}
				else if (nPC.type == Mod.Find<ModNPC>("Phantom").Type)
				{
					num4++;
				}
				else if (nPC.type == Mod.Find<ModNPC>("FlayerWraith").Type)
				{
					num5++;
				}
				else if (nPC.type == Mod.Find<ModNPC>("ShadeMass").Type)
				{
					num6++;
				}
				else if (nPC.type == Mod.Find<ModNPC>("AbyssalCultist").Type)
				{
					num7++;
				}
				else if (nPC.type == Mod.Find<ModNPC>("Warden").Type)
				{
					num8++;
				}
				else if (nPC.type == Mod.Find<ModNPC>("MotherPhantom").Type)
				{
					num9++;
				}
			}
		}
		if (!ShadowEventWorld.Phase2)
		{
			if (num > 4)
			{
				pool.Add(Mod.Find<ModNPC>("AbyssalWraith").Type, 0f);
			}
			else
			{
				pool.Add(Mod.Find<ModNPC>("AbyssalWraith").Type, 0.4f);
			}
			if (num2 > 3)
			{
				pool.Add(Mod.Find<ModNPC>("ShadeSpirit").Type, 0f);
			}
			else
			{
				pool.Add(Mod.Find<ModNPC>("ShadeSpirit").Type, 0.5f);
			}
			if (num3 > 6)
			{
				pool.Add(Mod.Find<ModNPC>("Scp2521").Type, 0f);
			}
			else
			{
				pool.Add(Mod.Find<ModNPC>("Scp2521").Type, 0.5f);
			}
			if (num4 > 3)
			{
				pool.Add(Mod.Find<ModNPC>("Phantom").Type, 0f);
			}
			else
			{
				pool.Add(Mod.Find<ModNPC>("Phantom").Type, 0.5f);
			}
		}
		if (!ShadowEventWorld.Phase2)
		{
			return;
		}
		if (num5 > 4)
		{
			pool.Add(Mod.Find<ModNPC>("FlayerWraith").Type, 0f);
		}
		else
		{
			pool.Add(Mod.Find<ModNPC>("FlayerWraith").Type, 0.4f);
		}
		if (num6 > 3)
		{
			pool.Add(Mod.Find<ModNPC>("ShadeMass").Type, 0f);
		}
		else
		{
			pool.Add(Mod.Find<ModNPC>("ShadeMass").Type, 0.25f);
		}
		if (num7 > 2)
		{
			pool.Add(Mod.Find<ModNPC>("AbyssalCultist").Type, 0f);
		}
		else
		{
			pool.Add(Mod.Find<ModNPC>("AbyssalCultist").Type, 0.5f);
		}
		if (num8 > 0)
		{
			pool.Add(Mod.Find<ModNPC>("Warden").Type, 0f);
		}
		else
		{
			pool.Add(Mod.Find<ModNPC>("Warden").Type, 0.07f);
		}
		if (ShadowEventWorld.EventTimer > 16200 && ShadowEventWorld.EventTimer < 20000)
		{
			if (num9 > 0)
			{
				pool.Add(Mod.Find<ModNPC>("MotherPhantom").Type, 0f);
			}
			else
			{
				pool.Add(Mod.Find<ModNPC>("MotherPhantom").Type, 0.03f);
			}
		}
	}

	public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
	{
		if (ShadowEventWorld.ShadowEventActive)
		{
			spawnRate = 60;
			maxSpawns = 100;
			if (NPC.AnyNPCs(Mod.Find<ModNPC>("ErebusHead").Type) || NPC.AnyNPCs(Mod.Find<ModNPC>("MindFlayer").Type))
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
		if (npc.type != Mod.Find<ModNPC>("ErebusHead").Type || npc.type != Mod.Find<ModNPC>("ErebusBody").Type)
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
                npc.active = false;
			}
		}
	}
}
