using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Depths;

public class DepthCrawler : ModNPC
{
	public int jumpCooldown;

	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Depth Crawler");
		Main.npcFrameCount[NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		NPC.npcSlots = 1f;
		NPC.width = 62;
		NPC.height = 34;
		NPC.damage = 50;
		NPC.defense = 15;
		NPC.lifeMax = 165;
		NPC.knockBackResist = 0.1f;
		NPC.aiStyle = 3;
		base.AIType = 257;
		NPC.HitSound = SoundID.NPCHit29;
		NPC.DeathSound = SoundID.NPCDeath31;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("DepthCrawlerBanner").Type;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0)
		{
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Depths/CrawlerGore1"));
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Depths/CrawlerGore2"));
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Depths/CrawlerGore3"));
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Depths/CrawlerGore4"));
		}
	}

	public override void AI()
	{
		Player player = Main.player[NPC.target];
		NPC.TargetClosest();
		jumpCooldown++;
		float num = 10.5f;
		if (Math.Abs(NPC.Center.X - player.Center.X) <= 100f && NPC.Bottom.Y > player.Bottom.Y && NPC.velocity.Y == 0f && jumpCooldown <= 0)
		{
			NPC.velocity.Y -= num;
			jumpCooldown = 15;
		}
	}

	public override bool PreAI()
	{
		NPC.spriteDirection = NPC.direction;
		return true;
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter >= 6.0)
		{
			NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
			NPC.frameCounter = 1.0;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.Player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.SpawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.SpawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.Player.GetModPlayer<UltraniumPlayer>().ZoneDepth)
			{
				return 0f;
			}
			return 20f;
		}
		return 0f;
	}

	public override void OnKill()
	{
		if (Main.rand.Next(5) == 0)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("ShadowEssence").Type, 1, false, 0, false, false);
		}
	}
}
