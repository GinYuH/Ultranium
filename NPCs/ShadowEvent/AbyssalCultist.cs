using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Eldritch;
using Ultranium.Items.Shade;

namespace Ultranium.NPCs.ShadowEvent;

public class AbyssalCultist : ModNPC
{
	private float ShootTimer;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Abyssal Cultist");
		Main.npcFrameCount[NPC.type] = 5;
	}

	public override void SetDefaults()
	{
		NPC.scale = 1.3f;
		NPC.width = 30;
		NPC.height = 60;
		NPC.damage = 45;
		NPC.defense = 50;
		NPC.lifeMax = 2000;
		NPC.noGravity = true;
		NPC.knockBackResist = 0f;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath6;
		Banner = NPC.type;
		BannerItem = Mod.Find<ModItem>("AbyssalCultistBanner").Type;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 3200;
		NPC.damage = 55;
		NPC.defense = 60;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("AbyssalCultistGore1").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("AbyssalCultistGore2").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("AbyssalCultistGore3").Type);
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
		bool expertMode = Main.expertMode;
		Vector2 vector = player.Center - NPC.Center;
		NPC.spriteDirection = Math.Sign(vector.X);
		NPC.velocity *= 0f;
		ShootTimer += 1f;
		if (ShootTimer == 60f)
		{
			vector.Normalize();
			vector.X *= 7f;
			vector.Y *= 7f;
			int num = (expertMode ? 40 : 50);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("EldritchBlast").Type, num, 1f, NPC.target, 0f, 0f);
		}
		if (ShootTimer == 180f)
		{
			for (int i = 0; i < 50; i++)
			{
				int num2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemEmerald);
				Main.dust[num2].scale = 1.5f;
			}
			int num3 = Main.rand.Next(4);
			if (num3 == 0)
			{
				NPC.position.X = player.position.X + 500f;
				NPC.position.Y = player.position.Y + 300f;
			}
			if (num3 == 1)
			{
				NPC.position.X = player.position.X + 500f;
				NPC.position.Y = player.position.Y - 400f;
			}
			if (num3 == 2)
			{
				NPC.position.X = player.position.X - 600f;
				NPC.position.Y = player.position.Y - 400f;
			}
			if (num3 == 3)
			{
				NPC.position.X = player.position.X - 600f;
				NPC.position.Y = player.position.Y + 300f;
			}
			for (int j = 0; j < 50; j++)
			{
				int num4 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemEmerald);
				Main.dust[num4].scale = 1.5f;
			}
		}
		if (ShootTimer > 240f)
		{
			ShootTimer = 0f;
		}
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkMatter>(), 3, 2, 3));
    }

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter > 5.0)
		{
			NPC.frame.Y = NPC.frame.Y + frameHeight;
			NPC.frameCounter = 0.0;
		}
		if (NPC.frame.Y >= frameHeight * 5)
		{
			NPC.frame.Y = 0;
		}
	}
}
