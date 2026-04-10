using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs;

public class NPCGlobal : GlobalNPC
{
	public int players;

	public override bool InstancePerEntity => true;

	public override void ScaleExpertStats(NPC npc, int numPlayers, float bossLifeScale)
	{
		players = numPlayers;
	}

	public override void SetDefaults(NPC npc)
	{
		players = 0;
	}

	public override void SetupShop(int type, Chest shop, ref int nextSlot)
	{
		if (type == 453 && UltraniumWorld.StrangeUndergrowth)
		{
			shop.item[nextSlot].SetDefaults(((GlobalNPC)this).mod.ItemType("StrangeUndergrowth"), false);
			nextSlot++;
		}
	}
}
