using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ModLoader;
using Ultranium.Items;
using Ultranium.Items.Blood;
using Ultranium.Items.Magic;
using Ultranium.Items.Melee;
using Ultranium.Items.Minion;
using Ultranium.Items.Minion.Materials;
using Ultranium.Items.Ranged;

namespace Ultranium.NPCs;

public class GlobalDrops : GlobalNPC
{
    public override void ModifyNPCLoot(NPC npc, NPCLoot npcLoot)
    {
        if (npc.type == 10 || npc.type == 95)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FakeWhoopieCushion>(), 35));
        }
        if (npc.type == 31 || npc.type == 294 || npc.type == 295 || npc.type == 296)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterJavelin>(), 30));
        }
		if (npc.type == 489 || npc.type == 490)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<BloodClot>(), 3, 2, 3));
		}
		if (npc.type == 3 || npc.type == 2)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<BloodClot>(), 3));
        }
		if (npc.type == 160)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Drip>(), 30));
        }
        if (npc.type == 370)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DukeYoyo>(), 5));
        }
        if (npc.type == 4)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StaffOfCthulhu>()));
        }
        if (npc.type == 245)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(5, ModContent.ItemType<SolThrow>(), ModContent.ItemType<SolSpear>()));
        }
        if (npc.type == 56 || npc.type == 43)
        {
            //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Leaf>()));
        }
        if (npc.type == 439)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<LunaticFireStaff>(), ModContent.ItemType<LunaticIceStaff>(), ModContent.ItemType<LunaticStarStaff>()));
        }
		if (npc.type == 35)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Necrosis>(), 3));
        }
        if (npc.type == 29)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadowRod>(), 20));
        }
        if (npc.type == 471)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadowFlame>(), 1, 6, 11));
        }
        if (npc.type == 50)
        {
            npcLoot.Add(ItemDropRule.Common(1309));
        }
    }
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
	}
}
