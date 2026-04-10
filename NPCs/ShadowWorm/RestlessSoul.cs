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
		// ((ModNPC)this).DisplayName.SetDefault("Restless Soul");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.lifeMax = 6500;
		((ModNPC)this).NPC.scale = 2f;
		((ModNPC)this).NPC.width = 26;
		((ModNPC)this).NPC.height = 20;
		((ModNPC)this).NPC.damage = 60;
		((ModNPC)this).NPC.defense = 50;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.npcSlots = 0f;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit1;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath6;
		((ModNPC)this).NPC.scale = 1f;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.noGravity = true;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 7500;
		((ModNPC)this).NPC.damage = 75;
		((ModNPC)this).NPC.defense = 65;
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
		Player player = Main.player[((ModNPC)this).NPC.target];
		if (NPC.CountNPCS(((ModNPC)this).Mod.Find<ModNPC>("ErebusHead").Type) == 0)
		{
			((Entity)((ModNPC)this).NPC).active = false;
		}
		ShootTimer++;
		if (ShootTimer == 240)
		{
			float num = 7.5f;
			int num2 = ((ModNPC)this).Mod.Find<ModProjectile>("DarkMatterBolt").Type;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
			float num3 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num3) * (double)num * -1.0), (float)(Math.Sin(num3) * (double)num * -1.0), num2, 30, 0f, Main.myPlayer, 0f, 0f);
			ShootTimer = 0;
		}
		NPC nPC = Main.npc[(int)((ModNPC)this).NPC.ai[0]];
		((ModNPC)this).NPC.ai[2] += 0f;
		((ModNPC)this).NPC.ai[1] += 2f;
		int num4 = 165 + (int)(Math.Sin(((ModNPC)this).NPC.ai[2] / 60f) * 30.0);
		double num5 = (double)((ModNPC)this).NPC.ai[1] * (Math.PI / 180.0);
		((ModNPC)this).NPC.position.X = nPC.Center.X - (float)(int)(Math.Cos(num5) * (double)num4) - (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = nPC.Center.Y - (float)(int)(Math.Sin(num5) * (double)num4) - (float)(((ModNPC)this).NPC.height / 2);
		if (!((Entity)nPC).active)
		{
			((Entity)((ModNPC)this).NPC).active = false;
		}
	}
}
