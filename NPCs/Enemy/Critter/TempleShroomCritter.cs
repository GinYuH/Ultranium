using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class TempleShroomCritter : ModNPC
{
	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Temple Shroom");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 5;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 16;
		((ModNPC)this).npc.height = 12;
		((ModNPC)this).npc.damage = 0;
		((ModNPC)this).npc.defense = 0;
		((ModNPC)this).npc.lifeMax = 5;
		((ModNPC)this).npc.dontCountMe = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit1;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).npc.knockBackResist = 0.45f;
		((ModNPC)this).npc.aiStyle = 7;
		((ModNPC)this).npc.npcSlots = 0f;
		((ModNPC)this).npc.noGravity = false;
		base.aiType = 46;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		if (!spawnInfo.player.GetModPlayer<UltraniumPlayer>().ZoneTemple || !NPC.downedGolemBoss)
		{
			return 0f;
		}
		return 0.06f;
	}

	public override void AI()
	{
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
	}

	public override void FindFrame(int frameHeight)
	{
		if (((ModNPC)this).npc.velocity != Vector2.Zero)
		{
			((ModNPC)this).npc.frameCounter += 1.0;
			if (((ModNPC)this).npc.frameCounter >= 8.0)
			{
				((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
				((ModNPC)this).npc.frameCounter = 1.0;
			}
		}
		else
		{
			_ = ((ModNPC)this).npc.frameCounter;
			((ModNPC)this).npc.frame.Y = 0;
		}
	}

	public override void NPCLoot()
	{
		Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("TempleShroom"), 1, false, 0, false, false);
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
