using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class Phantom : ModNPC
{
	public int ShootTimer;

	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Phantom");
		Main.npcFrameCount[NPC.type] = Main.npcFrameCount[560];
	}

	public override void SetDefaults()
	{
		NPC.scale = 1.2f;
		NPC.width = 34;
		NPC.height = 34;
		NPC.damage = 80;
		NPC.defense = 60;
		NPC.lifeMax = 2000;
		NPC.HitSound = new SoundStyle("Ultranium/Sounds/PhantomHit");
		NPC.DeathSound = new SoundStyle("Ultranium/Sounds/PhantomDeath") with { PitchVariance = 0.5f };
		NPC.knockBackResist = 0.9f;
		NPC.knockBackResist = 0f;
		NPC.noGravity = true;
		NPC.aiStyle = 108;
		base.AnimationType = 559;
		base.AIType = 560;
		NPC.buffImmune[24] = true;
		NPC.noTileCollide = true;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("PhantomBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 3200;
		NPC.damage = 115;
		NPC.defense = 75;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("PhantomGore1").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("PhantomGore2").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("PhantomGore3").Type);
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 120);
	}

	public override void AI()
	{
		NPC.TargetClosest();
		Player player = Main.player[NPC.target];
		ShootTimer++;
		if (ShootTimer == 600)
		{
			float num = 6f;
			float num2 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num2) * (double)num * -1.0), (float)(Math.Sin(num2) * (double)num * -1.0), Mod.Find<ModProjectile>("PhantomWave").Type, 40, 0f, 0, 0f, 0f);
			ShootTimer = 0;
		}
		if (Main.rand.Next(500) == 0)
		{
			SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/PhantomIdle"), NPC.position);
		}
	}

	public override void OnKill()
	{
		if (Main.rand.Next(4) == 0)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("DarkMatter").Type, 1, false, 0, false, false);
		}
	}
}
