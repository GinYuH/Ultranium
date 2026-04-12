using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs;

public class NPCGlobal : GlobalNPC
{
	public int players;

	public override bool InstancePerEntity => true;

	public override void ApplyDifficultyAndPlayerScaling(NPC npc, int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
	}

	public override void SetDefaults(NPC npc)
	{
		players = 0;
	}

    public override void ModifyShop(NPCShop shop)
    {
        if (shop.NpcType == 453)
		{
			shop.Add(Mod.Find<ModItem>("StrangeUndergrowth").Type, new Condition("In Strange Undergrwoth", () => UltraniumWorld.StrangeUndergrowth));
		}
    }
}
