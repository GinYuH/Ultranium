using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Dread.Projectiles;

public class DreadOrbiter : ModNPC
{
	private int ShootTimer;

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Dread Orbiter");
		Main.npcFrameCount[NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		NPC.lifeMax = 2500;
		NPC.width = 36;
		NPC.height = 36;
		NPC.damage = 60;
		NPC.defense = 70;
		NPC.knockBackResist = 0f;
		NPC.npcSlots = 0f;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath6;
		NPC.scale = 1f;
		NPC.noTileCollide = true;
		NPC.noGravity = true;
		NPC.immortal = true;
		NPC.dontTakeDamage = true;
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter >= 11.0)
		{
			NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
			NPC.frameCounter = 1.0;
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void AI()
	{
		Player player = Main.player[NPC.target];
		NPC nPC = Main.npc[(int)NPC.ai[1]];
		if (nPC != Main.npc[0])
		{
			NPC.ai[0] += 2f;
			int num = 165;
			double num2 = (double)NPC.ai[0] * (Math.PI / 180.0);
			NPC.position.X = nPC.Center.X - (float)(int)(Math.Cos(num2) * (double)num) - (float)(NPC.width / 2);
			NPC.position.Y = nPC.Center.Y - (float)(int)(Math.Sin(num2) * (double)num) - (float)(NPC.height / 2);
		}
		if (!((Entity)nPC).active)
		{
			((Entity)NPC).active = false;
		}
		ShootTimer++;
		if (ShootTimer >= 60)
		{
			((Entity)NPC).active = false;
			int num3 = (Main.expertMode ? 25 : 45);
			float num4 = 10f;
			int num5 = Mod.Find<ModProjectile>("DreadOrbiterBolt").Type;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
			float num6 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num6) * (double)num4 * -1.0), (float)(Math.Sin(num6) * (double)num4 * -1.0), num5, num3, 0f, Main.myPlayer, 0f, 0f);
		}
	}
}
