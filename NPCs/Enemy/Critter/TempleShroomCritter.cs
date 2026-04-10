using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class TempleShroomCritter : ModNPC
{
	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Temple Shroom");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 5;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 16;
		((ModNPC)this).NPC.height = 12;
		((ModNPC)this).NPC.damage = 0;
		((ModNPC)this).NPC.defense = 0;
		((ModNPC)this).NPC.lifeMax = 5;
		((ModNPC)this).NPC.dontCountMe = true;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit1;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).NPC.knockBackResist = 0.45f;
		((ModNPC)this).NPC.aiStyle = 7;
		((ModNPC)this).NPC.npcSlots = 0f;
		((ModNPC)this).NPC.noGravity = false;
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
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
	}

	public override void FindFrame(int frameHeight)
	{
		if (((ModNPC)this).NPC.velocity != Vector2.Zero)
		{
			((ModNPC)this).NPC.frameCounter += 1.0;
			if (((ModNPC)this).NPC.frameCounter >= 8.0)
			{
				((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
				((ModNPC)this).NPC.frameCounter = 1.0;
			}
		}
		else
		{
			_ = ((ModNPC)this).NPC.frameCounter;
			((ModNPC)this).NPC.frame.Y = 0;
		}
	}

	public override void OnKill()
	{
		Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("TempleShroom").Type, 1, false, 0, false, false);
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
