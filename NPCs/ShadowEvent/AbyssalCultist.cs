using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class AbyssalCultist : ModNPC
{
	private float ShootTimer;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Abyssal Cultist");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 5;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.scale = 1.3f;
		((ModNPC)this).npc.width = 30;
		((ModNPC)this).npc.height = 60;
		((ModNPC)this).npc.damage = 45;
		((ModNPC)this).npc.defense = 50;
		((ModNPC)this).npc.lifeMax = 2000;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit1;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath6;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("AbyssalCultistBanner");
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 3200;
		((ModNPC)this).npc.damage = 55;
		((ModNPC)this).npc.defense = 60;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/AbyssalCultistGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/AbyssalCultistGore2"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/AbyssalCultistGore3"));
		return true;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModNPC)this).mod.BuffType("DarkDebuff"), 120);
	}

	public override void AI()
	{
		((ModNPC)this).npc.TargetClosest();
		Player player = Main.player[((ModNPC)this).npc.target];
		bool expertMode = Main.expertMode;
		Vector2 vector = player.Center - ((ModNPC)this).npc.Center;
		((ModNPC)this).npc.spriteDirection = Math.Sign(vector.X);
		((ModNPC)this).npc.velocity *= 0f;
		ShootTimer += 1f;
		if (ShootTimer == 60f)
		{
			vector.Normalize();
			vector.X *= 7f;
			vector.Y *= 7f;
			int num = (expertMode ? 40 : 50);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector.X, vector.Y, ((ModNPC)this).mod.ProjectileType("EldritchBlast"), num, 1f, ((ModNPC)this).npc.target, 0f, 0f);
		}
		if (ShootTimer == 180f)
		{
			for (int i = 0; i < 50; i++)
			{
				int num2 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 89);
				Main.dust[num2].scale = 1.5f;
			}
			int num3 = Main.rand.Next(4);
			if (num3 == 0)
			{
				((ModNPC)this).npc.position.X = player.position.X + 500f;
				((ModNPC)this).npc.position.Y = player.position.Y + 300f;
			}
			if (num3 == 1)
			{
				((ModNPC)this).npc.position.X = player.position.X + 500f;
				((ModNPC)this).npc.position.Y = player.position.Y - 400f;
			}
			if (num3 == 2)
			{
				((ModNPC)this).npc.position.X = player.position.X - 600f;
				((ModNPC)this).npc.position.Y = player.position.Y - 400f;
			}
			if (num3 == 3)
			{
				((ModNPC)this).npc.position.X = player.position.X - 600f;
				((ModNPC)this).npc.position.Y = player.position.Y + 300f;
			}
			for (int j = 0; j < 50; j++)
			{
				int num4 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 89);
				Main.dust[num4].scale = 1.5f;
			}
		}
		if (ShootTimer > 240f)
		{
			ShootTimer = 0f;
		}
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(3) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DarkMatter"), Main.rand.Next(2, 4), false, 0, false, false);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter > 5.0)
		{
			((ModNPC)this).npc.frame.Y = ((ModNPC)this).npc.frame.Y + frameHeight;
			((ModNPC)this).npc.frameCounter = 0.0;
		}
		if (((ModNPC)this).npc.frame.Y >= frameHeight * 5)
		{
			((ModNPC)this).npc.frame.Y = 0;
		}
	}
}
