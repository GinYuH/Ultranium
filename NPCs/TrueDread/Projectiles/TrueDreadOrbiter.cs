using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.TrueDread.Projectiles;

public class TrueDreadOrbiter : ModNPC
{
	private int ShootTimer;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Dread Orbiter");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.lifeMax = 2500;
		((ModNPC)this).npc.width = 26;
		((ModNPC)this).npc.height = 20;
		((ModNPC)this).npc.damage = 60;
		((ModNPC)this).npc.defense = 70;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.npcSlots = 0f;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit1;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath6;
		((ModNPC)this).npc.scale = 1f;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.immortal = true;
		((ModNPC)this).npc.dontTakeDamage = true;
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

	public override bool CheckActive()
	{
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[((ModNPC)this).npc.target];
		NPC nPC = Main.npc[(int)((ModNPC)this).npc.ai[1]];
		if (nPC != Main.npc[0])
		{
			((ModNPC)this).npc.ai[0] += 2f;
			int num = 240;
			double num2 = (double)((ModNPC)this).npc.ai[0] * (Math.PI / 180.0);
			((ModNPC)this).npc.position.X = nPC.Center.X - (float)(int)(Math.Cos(num2) * (double)num) - (float)(((ModNPC)this).npc.width / 2);
			((ModNPC)this).npc.position.Y = nPC.Center.Y - (float)(int)(Math.Sin(num2) * (double)num) - (float)(((ModNPC)this).npc.height / 2);
		}
		if (!((Entity)nPC).active)
		{
			((Entity)((ModNPC)this).npc).active = false;
		}
		ShootTimer++;
		if (ShootTimer >= 50)
		{
			((Entity)((ModNPC)this).npc).active = false;
			float num3 = 12f;
			int num4 = ((ModNPC)this).mod.ProjectileType("DreadOrbiterBolt");
			Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
			float num5 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num5) * (double)num3 * -1.0), (float)(Math.Sin(num5) * (double)num3 * -1.0), num4, 30, 0f, Main.myPlayer, 0f, 0f);
		}
	}
}
