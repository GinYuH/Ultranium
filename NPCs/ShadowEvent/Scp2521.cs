using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class Scp2521 : ModNPC
{
	public int JumpTimer;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Abyss Strider");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.width = 100;
		((ModNPC)this).npc.height = 50;
		((ModNPC)this).npc.damage = 80;
		((ModNPC)this).npc.defense = 70;
		((ModNPC)this).npc.lifeMax = 1800;
		((ModNPC)this).npc.knockBackResist = 0.1f;
		((ModNPC)this).npc.aiStyle = 3;
		base.aiType = 257;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit49;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath55;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("Scp2521Banner");
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 3500;
		((ModNPC)this).npc.damage = 130;
		((ModNPC)this).npc.defense = 70;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/Scp2521Gore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/Scp2521Gore2"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/Scp2521Gore3"));
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModNPC)this).mod.BuffType("DarkDebuff"), 120);
	}

	public override void AI()
	{
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
		Player player = Main.player[((ModNPC)this).npc.target];
		((ModNPC)this).npc.TargetClosest();
		new Vector2(((ModNPC)this).npc.position.X + (float)(((ModNPC)this).npc.width / 2), ((ModNPC)this).npc.position.Y + (float)(((ModNPC)this).npc.height / 2));
		if (Vector2.Distance(((ModNPC)this).npc.Center, player.Center) >= 650f && Main.rand.Next(350) == 0)
		{
			Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector.Normalize();
			vector.X *= 6f;
			vector.Y *= 6f;
			int num = Main.rand.Next(1, 1);
			for (int i = 0; i < num; i++)
			{
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector.X, vector.Y, ((ModNPC)this).mod.ProjectileType("DarkMatterBolt"), 40, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		JumpTimer--;
		float num2 = 10.5f;
		if (Math.Abs(((ModNPC)this).npc.Center.X - player.Center.X) <= 100f && ((ModNPC)this).npc.Bottom.Y > player.Bottom.Y && ((ModNPC)this).npc.velocity.Y == 0f && JumpTimer <= 0)
		{
			((ModNPC)this).npc.velocity.Y -= num2;
			JumpTimer = 15;
		}
		if (Main.rand.Next(500) == 0)
		{
			int num3 = 0;
			switch (Main.rand.Next(2))
			{
			case 0:
				num3 = 41;
				break;
			case 1:
				num3 = 42;
				break;
			case 2:
				num3 = 43;
				break;
			}
			Main.PlaySound(29, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, num3, 1f, 0f);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter >= 11.0)
		{
			((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
			((ModNPC)this).npc.frameCounter = 1.0;
		}
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(5) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DarkMatter"), 1, false, 0, false, false);
		}
	}
}
