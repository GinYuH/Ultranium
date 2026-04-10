using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class Warden : ModNPC
{
	public int JumpTimer;

	public int Timer;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Abyssal Brute");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 8;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.width = 138;
		((ModNPC)this).npc.height = 144;
		((ModNPC)this).npc.damage = 80;
		((ModNPC)this).npc.defense = 70;
		((ModNPC)this).npc.lifeMax = 12000;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit49;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath55;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("AbyssBruteBanner");
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 24000;
		((ModNPC)this).npc.damage = 130;
		((ModNPC)this).npc.defense = 70;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/WardenGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/WardenGore2"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/WardenGore3"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/WardenGore4"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/WardenGore5"));
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModNPC)this).mod.BuffType("DarkDebuff"), 180);
	}

	public override void AI()
	{
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
		_ = Main.player[((ModNPC)this).npc.target];
		((ModNPC)this).npc.TargetClosest();
		Timer++;
		if (Timer == 500)
		{
			Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector.Normalize();
			vector.X *= 25f;
			vector.Y *= 25f;
			((ModNPC)this).npc.velocity.X = vector.X;
			((ModNPC)this).npc.velocity.Y = vector.Y;
			Vector2 vector2 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector2.Normalize();
			vector2.X *= 25f;
			vector2.Y *= 25f;
		}
		if (Timer > 500 && Timer < 570)
		{
			((ModNPC)this).npc.rotation += 0.5f * (float)((ModNPC)this).npc.direction;
			Vector2 position = ((ModNPC)this).npc.Center + Vector2.Normalize(((ModNPC)this).npc.velocity) * 10f;
			Dust obj = Main.dust[Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 89)];
			obj.position = position;
			obj.velocity = ((ModNPC)this).npc.velocity.RotatedBy(Math.PI / 2.0) * 0.05f + ((ModNPC)this).npc.velocity / 2f;
			obj.position += ((ModNPC)this).npc.velocity.RotatedBy(Math.PI / 2.0);
			obj.fadeIn = 0.5f;
			obj.noGravity = true;
			Dust obj2 = Main.dust[Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 89)];
			obj2.position = position;
			obj2.velocity = ((ModNPC)this).npc.velocity.RotatedBy(-Math.PI / 2.0) * 0.05f + ((ModNPC)this).npc.velocity / 2f;
			obj2.position += ((ModNPC)this).npc.velocity.RotatedBy(-Math.PI / 2.0);
			obj2.fadeIn = 0.5f;
			obj2.noGravity = true;
		}
		else
		{
			((ModNPC)this).npc.rotation = 0f;
		}
		if (Timer < 840)
		{
			((ModNPC)this).npc.aiStyle = 3;
			base.aiType = 508;
		}
		if (Timer > 840)
		{
			((ModNPC)this).npc.velocity.X *= 0f;
			if (Timer == 880 || Timer == 920 || Timer == 960)
			{
				Vector2 vector3 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector3.Normalize();
				vector3.X *= 6f;
				vector3.Y *= 6f;
				int num = Main.rand.Next(3, 5);
				for (int i = 0; i < num; i++)
				{
					float num2 = (float)Main.rand.Next(-300, 300) * 0.01f;
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector3.X + num2, vector3.Y + num2, ((ModNPC)this).mod.ProjectileType("WardenBolt"), 60, 1f, Main.myPlayer, 0f, 0f);
				}
			}
		}
		if (Timer == 1020)
		{
			Timer = 0;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter > 6.0)
		{
			((ModNPC)this).npc.frame.Y = ((ModNPC)this).npc.frame.Y + frameHeight;
			((ModNPC)this).npc.frameCounter = 0.0;
		}
		if (((ModNPC)this).npc.frame.Y >= frameHeight * 6)
		{
			((ModNPC)this).npc.frame.Y = 0;
		}
		if (Timer > 500 && Timer < 570)
		{
			((ModNPC)this).npc.frame.Y = 7 * frameHeight;
		}
		if (Timer > 840)
		{
			((ModNPC)this).npc.frame.Y = 6 * frameHeight;
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
