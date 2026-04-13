using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
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
        if (npc.type == NPCID.GiantWormHead || npc.type == NPCID.DiggerHead)
		{
			npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FakeWhoopieCushion>(), 35));
        }
        if (npc.type == NPCID.AngryBones || npc.type == NPCID.AngryBonesBig || npc.type == NPCID.AngryBonesBigMuscle || npc.type == NPCID.AngryBonesBigHelmet)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<WaterJavelin>(), 30));
        }
		if (npc.type == NPCID.BloodZombie || npc.type == NPCID.Drippler)
		{
			npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<BloodClot>(), 3, 2, 3));
		}
		if (npc.type == NPCID.Zombie || npc.type == NPCID.DemonEye)
        {
            npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsBloodMoonAndNotFromStatue(), ModContent.ItemType<BloodClot>(), 3));
        }
		if (npc.type == NPCID.Truffle)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Drip>(), 30));
        }
        if (npc.type == NPCID.DukeFishron)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DukeYoyo>(), 5));
        }
        if (npc.type == NPCID.EyeofCthulhu)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<StaffOfCthulhu>()));
        }
        if (npc.type == NPCID.Golem)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(5, ModContent.ItemType<SolThrow>(), ModContent.ItemType<SolSpear>()));
        }
        // Porting note: Who????
        if (npc.type == NPCID.Snatcher || npc.type == NPCID.ManEater)
        {
            //npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Leaf>()));
        }
        if (npc.type == NPCID.CultistBoss)
        {
            npcLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<LunaticFireStaff>(), ModContent.ItemType<LunaticIceStaff>(), ModContent.ItemType<LunaticStarStaff>()));
        }
		if (npc.type == NPCID.SkeletronHead)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Necrosis>(), 3));
        }
        if (npc.type == NPCID.GoblinSorcerer)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadowRod>(), 20));
        }
        if (npc.type == NPCID.GoblinSummoner)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadowFlame>(), 1, 6, 11));
        }
        if (npc.type == NPCID.KingSlime)
        {
            npcLoot.Add(ItemDropRule.Common(1309));
        }
    }
	public override void OnKill(NPC npc)
	{
		if (npc.type == NPCID.SkeletronHead && Main.dayTime)
		{
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("SoulCrushingDisappointment").Type, 1, false, 0, false, false);
			if (!UltraniumWorld.SoulCrushingDisappointment)
			{
				UltraniumWorld.SoulCrushingDisappointment = true;
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.WorldData);
				}
			}
		}
		if (npc.type == NPCID.Truffle)
		{
			Item.NewItem(null, (int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, Mod.Find<ModItem>("TruffleShroom").Type, 1, false, 0, false, false);
			if (!UltraniumWorld.TruffleShroom)
			{
				UltraniumWorld.TruffleShroom = true;
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.WorldData);
				}
			}
		}
	}
}
