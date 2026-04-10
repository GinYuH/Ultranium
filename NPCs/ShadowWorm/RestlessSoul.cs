using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm;

public class RestlessSoul : ModNPC
{
	private int ShootTimer;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Restless Soul");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.lifeMax = 6500;
		((ModNPC)this).npc.scale = 2f;
		((ModNPC)this).npc.width = 26;
		((ModNPC)this).npc.height = 20;
		((ModNPC)this).npc.damage = 60;
		((ModNPC)this).npc.defense = 50;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.npcSlots = 0f;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit1;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath6;
		((ModNPC)this).npc.scale = 1f;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.noGravity = true;
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 7500;
		((ModNPC)this).npc.damage = 75;
		((ModNPC)this).npc.defense = 65;
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void AI()
	{
		Player player = Main.player[((ModNPC)this).npc.target];
		if (NPC.CountNPCS(((ModNPC)this).mod.NPCType("ErebusHead")) == 0)
		{
			((Entity)((ModNPC)this).npc).active = false;
		}
		ShootTimer++;
		if (ShootTimer == 240)
		{
			float num = 7.5f;
			int num2 = ((ModNPC)this).mod.ProjectileType("DarkMatterBolt");
			Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
			float num3 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num3) * (double)num * -1.0), (float)(Math.Sin(num3) * (double)num * -1.0), num2, 30, 0f, Main.myPlayer, 0f, 0f);
			ShootTimer = 0;
		}
		NPC nPC = Main.npc[(int)((ModNPC)this).npc.ai[0]];
		((ModNPC)this).npc.ai[2] += 0f;
		((ModNPC)this).npc.ai[1] += 2f;
		int num4 = 165 + (int)(Math.Sin(((ModNPC)this).npc.ai[2] / 60f) * 30.0);
		double num5 = (double)((ModNPC)this).npc.ai[1] * (Math.PI / 180.0);
		((ModNPC)this).npc.position.X = nPC.Center.X - (float)(int)(Math.Cos(num5) * (double)num4) - (float)(((ModNPC)this).npc.width / 2);
		((ModNPC)this).npc.position.Y = nPC.Center.Y - (float)(int)(Math.Sin(num5) * (double)num4) - (float)(((ModNPC)this).npc.height / 2);
		if (!((Entity)nPC).active)
		{
			((Entity)((ModNPC)this).npc).active = false;
		}
	}
}
