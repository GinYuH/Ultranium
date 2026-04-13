using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowWorm;

public class RestlessSoul : ModNPC
{
	private int ShootTimer;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Restless Soul");
	}

	public override void SetDefaults()
	{
		NPC.lifeMax = 6500;
		NPC.scale = 2f;
		NPC.width = 26;
		NPC.height = 20;
		NPC.damage = 60;
		NPC.defense = 50;
		NPC.knockBackResist = 0f;
		NPC.npcSlots = 0f;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath6;
		NPC.scale = 1f;
		NPC.noTileCollide = true;
		NPC.noGravity = true;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 7500;
		NPC.damage = 75;
		NPC.defense = 65;
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
		Player player = Main.player[NPC.target];
		if (NPC.CountNPCS(Mod.Find<ModNPC>("ErebusHead").Type) == 0)
		{
			NPC.active = false;
		}
		ShootTimer++;
		if (ShootTimer == 240)
		{
			float num = 7.5f;
			int num2 = Mod.Find<ModProjectile>("DarkMatterBolt").Type;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
			float num3 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num3) * (double)num * -1.0), (float)(Math.Sin(num3) * (double)num * -1.0), num2, 30, 0f, Main.myPlayer, 0f, 0f);
			ShootTimer = 0;
		}
		NPC nPC = Main.npc[(int)NPC.ai[0]];
		NPC.ai[2] += 0f;
		NPC.ai[1] += 2f;
		int num4 = 165 + (int)(Math.Sin(NPC.ai[2] / 60f) * 30.0);
		double num5 = (double)NPC.ai[1] * (Math.PI / 180.0);
		NPC.position.X = nPC.Center.X - (float)(int)(Math.Cos(num5) * (double)num4) - (float)(NPC.width / 2);
		NPC.position.Y = nPC.Center.Y - (float)(int)(Math.Sin(num5) * (double)num4) - (float)(NPC.height / 2);
		if (!NPC.active)
		{
			NPC.active = false;
		}
	}
}
