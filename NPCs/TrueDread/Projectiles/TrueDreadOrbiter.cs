using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.TrueDread.Projectiles;

public class TrueDreadOrbiter : ModNPC
{
	private int ShootTimer;

	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Dread Orbiter");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.lifeMax = 2500;
		((ModNPC)this).NPC.width = 26;
		((ModNPC)this).NPC.height = 20;
		((ModNPC)this).NPC.damage = 60;
		((ModNPC)this).NPC.defense = 70;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.npcSlots = 0f;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit1;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath6;
		((ModNPC)this).NPC.scale = 1f;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.immortal = true;
		((ModNPC)this).NPC.dontTakeDamage = true;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter >= 11.0)
		{
			((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
			((ModNPC)this).NPC.frameCounter = 1.0;
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[((ModNPC)this).NPC.target];
		NPC nPC = Main.npc[(int)((ModNPC)this).NPC.ai[1]];
		if (nPC != Main.npc[0])
		{
			((ModNPC)this).NPC.ai[0] += 2f;
			int num = 240;
			double num2 = (double)((ModNPC)this).NPC.ai[0] * (Math.PI / 180.0);
			((ModNPC)this).NPC.position.X = nPC.Center.X - (float)(int)(Math.Cos(num2) * (double)num) - (float)(((ModNPC)this).NPC.width / 2);
			((ModNPC)this).NPC.position.Y = nPC.Center.Y - (float)(int)(Math.Sin(num2) * (double)num) - (float)(((ModNPC)this).NPC.height / 2);
		}
		if (!((Entity)nPC).active)
		{
			((Entity)((ModNPC)this).NPC).active = false;
		}
		ShootTimer++;
		if (ShootTimer >= 50)
		{
			((Entity)((ModNPC)this).NPC).active = false;
			float num3 = 12f;
			int num4 = ((ModNPC)this).Mod.Find<ModProjectile>("DreadOrbiterBolt").Type;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
			float num5 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num5) * (double)num3 * -1.0), (float)(Math.Sin(num5) * (double)num3 * -1.0), num4, 30, 0f, Main.myPlayer, 0f, 0f);
		}
	}
}
