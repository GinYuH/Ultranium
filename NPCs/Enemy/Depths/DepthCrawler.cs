using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Depths;

public class DepthCrawler : ModNPC
{
	public int jumpCooldown;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Depth Crawler");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.width = 62;
		((ModNPC)this).npc.height = 34;
		((ModNPC)this).npc.damage = 50;
		((ModNPC)this).npc.defense = 15;
		((ModNPC)this).npc.lifeMax = 165;
		((ModNPC)this).npc.knockBackResist = 0.1f;
		((ModNPC)this).npc.aiStyle = 3;
		base.aiType = 257;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit29;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath31;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("DepthCrawlerBanner");
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (((ModNPC)this).npc.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/Depths/CrawlerGore1"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/Depths/CrawlerGore2"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/Depths/CrawlerGore3"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/Depths/CrawlerGore4"));
		}
	}

	public override void AI()
	{
		Player player = Main.player[((ModNPC)this).npc.target];
		((ModNPC)this).npc.TargetClosest();
		jumpCooldown++;
		float num = 10.5f;
		if (Math.Abs(((ModNPC)this).npc.Center.X - player.Center.X) <= 100f && ((ModNPC)this).npc.Bottom.Y > player.Bottom.Y && ((ModNPC)this).npc.velocity.Y == 0f && jumpCooldown <= 0)
		{
			((ModNPC)this).npc.velocity.Y -= num;
			jumpCooldown = 15;
		}
	}

	public override bool PreAI()
	{
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
		return true;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter >= 6.0)
		{
			((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
			((ModNPC)this).npc.frameCounter = 1.0;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.player.GetModPlayer<UltraniumPlayer>().ZoneDepth)
			{
				return 0f;
			}
			return 20f;
		}
		return 0f;
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(5) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("ShadowEssence"), 1, false, 0, false, false);
		}
	}
}
