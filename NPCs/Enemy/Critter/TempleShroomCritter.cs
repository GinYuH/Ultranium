using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Eldritch;

namespace Ultranium.NPCs.Enemy.Critter;

public class TempleShroomCritter : ModNPC
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Temple Shroom");
		Main.npcFrameCount[NPC.type] = 5;
	}

	public override void SetDefaults()
	{
		NPC.width = 16;
		NPC.height = 12;
		NPC.damage = 0;
		NPC.defense = 0;
		NPC.lifeMax = 5;
		NPC.dontCountMe = true;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.knockBackResist = 0.45f;
		NPC.aiStyle = 7;
		NPC.npcSlots = 0f;
		NPC.noGravity = false;
		base.AIType = 46;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		if (!spawnInfo.Player.GetModPlayer<UltraniumPlayer>().ZoneTemple || !NPC.downedGolemBoss)
		{
			return 0f;
		}
		return 0.06f;
	}

	public override void AI()
	{
		NPC.spriteDirection = NPC.direction;
	}

	public override void FindFrame(int frameHeight)
	{
		if (NPC.velocity != Vector2.Zero)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter >= 8.0)
			{
				NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
				NPC.frameCounter = 1.0;
			}
		}
		else
		{
			_ = NPC.frameCounter;
			NPC.frame.Y = 0;
		}
    }
    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<TempleShroom>()));
    }

    public override void OnKill()
	{
		if (!UltraniumWorld.SolarShroom)
		{
			UltraniumWorld.SolarShroom = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
	}
}
