using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Shadow;

public class DarkDemon : ModNPC
{
	private int Timer;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Dark Demon");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 30;
		((ModNPC)this).npc.height = 30;
		((ModNPC)this).npc.damage = 18;
		((ModNPC)this).npc.defense = 10;
		((ModNPC)this).npc.lifeMax = 55;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit21;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath24;
		((ModNPC)this).npc.value = Item.buyPrice(0, 0, 1);
		((ModNPC)this).npc.knockBackResist = 0.5f;
		((ModNPC)this).npc.aiStyle = 14;
		((ModNPC)this).npc.noGravity = true;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("DarkDemonBanner");
	}

	public override void AI()
	{
		((ModNPC)this).npc.TargetClosest();
		Player player = Main.player[((ModNPC)this).npc.target];
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.03f;
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
		Timer++;
		if (Timer > 600)
		{
			((ModNPC)this).npc.velocity *= 0f;
		}
		if (Timer == 650)
		{
			float num = 6f;
			float num2 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num2) * (double)num * -1.0), (float)(Math.Sin(num2) * (double)num * -1.0), ((ModNPC)this).mod.ProjectileType("DarkDemonScythe"), 20, 0f, 0, 0f, 0f);
			Timer = 0;
		}
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (((ModNPC)this).npc.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowBiome/DemonGore1"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowBiome/DemonGore2"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowBiome/DemonGore3"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowBiome/DemonGore4"));
		}
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter >= 8.0)
		{
			((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
			((ModNPC)this).npc.frameCounter = 1.0;
		}
	}

	public override void NPCLoot()
	{
		if (Utils.NextBool(Main.rand, 2))
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("ShadowEssence"), Main.rand.Next(1, 4), false, 0, false, false);
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
			if (!spawnInfo.player.GetModPlayer<UltraniumPlayer>().ZoneShadow)
			{
				return 0f;
			}
			return 20f;
		}
		return 0f;
	}
}
