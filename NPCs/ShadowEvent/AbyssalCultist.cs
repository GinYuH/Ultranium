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
		// ((ModNPC)this).DisplayName.SetDefault("Abyssal Cultist");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 5;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.scale = 1.3f;
		((ModNPC)this).NPC.width = 30;
		((ModNPC)this).NPC.height = 60;
		((ModNPC)this).NPC.damage = 45;
		((ModNPC)this).NPC.defense = 50;
		((ModNPC)this).NPC.lifeMax = 2000;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit1;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath6;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("AbyssalCultistBanner").Type;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 3200;
		((ModNPC)this).NPC.damage = 55;
		((ModNPC)this).NPC.defense = 60;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/AbyssalCultistGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/AbyssalCultistGore2"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/AbyssalCultistGore3"));
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		player.AddBuff(((ModNPC)this).Mod.Find<ModBuff>("DarkDebuff").Type, 120);
	}

	public override void AI()
	{
		((ModNPC)this).NPC.TargetClosest();
		Player player = Main.player[((ModNPC)this).NPC.target];
		bool expertMode = Main.expertMode;
		Vector2 vector = player.Center - ((ModNPC)this).NPC.Center;
		((ModNPC)this).NPC.spriteDirection = Math.Sign(vector.X);
		((ModNPC)this).NPC.velocity *= 0f;
		ShootTimer += 1f;
		if (ShootTimer == 60f)
		{
			vector.Normalize();
			vector.X *= 7f;
			vector.Y *= 7f;
			int num = (expertMode ? 40 : 50);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector.X, vector.Y, ((ModNPC)this).Mod.Find<ModProjectile>("EldritchBlast").Type, num, 1f, ((ModNPC)this).NPC.target, 0f, 0f);
		}
		if (ShootTimer == 180f)
		{
			for (int i = 0; i < 50; i++)
			{
				int num2 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 89);
				Main.dust[num2].scale = 1.5f;
			}
			int num3 = Main.rand.Next(4);
			if (num3 == 0)
			{
				((ModNPC)this).NPC.position.X = player.position.X + 500f;
				((ModNPC)this).NPC.position.Y = player.position.Y + 300f;
			}
			if (num3 == 1)
			{
				((ModNPC)this).NPC.position.X = player.position.X + 500f;
				((ModNPC)this).NPC.position.Y = player.position.Y - 400f;
			}
			if (num3 == 2)
			{
				((ModNPC)this).NPC.position.X = player.position.X - 600f;
				((ModNPC)this).NPC.position.Y = player.position.Y - 400f;
			}
			if (num3 == 3)
			{
				((ModNPC)this).NPC.position.X = player.position.X - 600f;
				((ModNPC)this).NPC.position.Y = player.position.Y + 300f;
			}
			for (int j = 0; j < 50; j++)
			{
				int num4 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 89);
				Main.dust[num4].scale = 1.5f;
			}
		}
		if (ShootTimer > 240f)
		{
			ShootTimer = 0f;
		}
	}

	public override void OnKill()
	{
		if (Main.rand.Next(3) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DarkMatter").Type, Main.rand.Next(2, 4), false, 0, false, false);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter > 5.0)
		{
			((ModNPC)this).NPC.frame.Y = ((ModNPC)this).NPC.frame.Y + frameHeight;
			((ModNPC)this).NPC.frameCounter = 0.0;
		}
		if (((ModNPC)this).NPC.frame.Y >= frameHeight * 5)
		{
			((ModNPC)this).NPC.frame.Y = 0;
		}
	}
}
