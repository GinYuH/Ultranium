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
		// ((ModNPC)this).DisplayName.SetDefault("Abyssal Brute");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 8;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.width = 138;
		((ModNPC)this).NPC.height = 144;
		((ModNPC)this).NPC.damage = 80;
		((ModNPC)this).NPC.defense = 70;
		((ModNPC)this).NPC.lifeMax = 12000;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit49;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath55;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("AbyssBruteBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 24000;
		((ModNPC)this).NPC.damage = 130;
		((ModNPC)this).NPC.defense = 70;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/WardenGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/WardenGore2"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/WardenGore3"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/WardenGore4"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/WardenGore5"));
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		player.AddBuff(((ModNPC)this).Mod.Find<ModBuff>("DarkDebuff").Type, 180);
	}

	public override void AI()
	{
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
		_ = Main.player[((ModNPC)this).NPC.target];
		((ModNPC)this).NPC.TargetClosest();
		Timer++;
		if (Timer == 500)
		{
			Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector.Normalize();
			vector.X *= 25f;
			vector.Y *= 25f;
			((ModNPC)this).NPC.velocity.X = vector.X;
			((ModNPC)this).NPC.velocity.Y = vector.Y;
			Vector2 vector2 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector2.Normalize();
			vector2.X *= 25f;
			vector2.Y *= 25f;
		}
		if (Timer > 500 && Timer < 570)
		{
			((ModNPC)this).NPC.rotation += 0.5f * (float)((ModNPC)this).NPC.direction;
			Vector2 position = ((ModNPC)this).NPC.Center + Vector2.Normalize(((ModNPC)this).NPC.velocity) * 10f;
			Dust obj = Main.dust[Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 89)];
			obj.position = position;
			obj.velocity = ((ModNPC)this).NPC.velocity.RotatedBy(Math.PI / 2.0) * 0.05f + ((ModNPC)this).NPC.velocity / 2f;
			obj.position += ((ModNPC)this).NPC.velocity.RotatedBy(Math.PI / 2.0);
			obj.fadeIn = 0.5f;
			obj.noGravity = true;
			Dust obj2 = Main.dust[Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 89)];
			obj2.position = position;
			obj2.velocity = ((ModNPC)this).NPC.velocity.RotatedBy(-Math.PI / 2.0) * 0.05f + ((ModNPC)this).NPC.velocity / 2f;
			obj2.position += ((ModNPC)this).NPC.velocity.RotatedBy(-Math.PI / 2.0);
			obj2.fadeIn = 0.5f;
			obj2.noGravity = true;
		}
		else
		{
			((ModNPC)this).NPC.rotation = 0f;
		}
		if (Timer < 840)
		{
			((ModNPC)this).NPC.aiStyle = 3;
			base.AIType = 508;
		}
		if (Timer > 840)
		{
			((ModNPC)this).NPC.velocity.X *= 0f;
			if (Timer == 880 || Timer == 920 || Timer == 960)
			{
				Vector2 vector3 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
				vector3.Normalize();
				vector3.X *= 6f;
				vector3.Y *= 6f;
				int num = Main.rand.Next(3, 5);
				for (int i = 0; i < num; i++)
				{
					float num2 = (float)Main.rand.Next(-300, 300) * 0.01f;
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector3.X + num2, vector3.Y + num2, ((ModNPC)this).Mod.Find<ModProjectile>("WardenBolt").Type, 60, 1f, Main.myPlayer, 0f, 0f);
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
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter > 6.0)
		{
			((ModNPC)this).NPC.frame.Y = ((ModNPC)this).NPC.frame.Y + frameHeight;
			((ModNPC)this).NPC.frameCounter = 0.0;
		}
		if (((ModNPC)this).NPC.frame.Y >= frameHeight * 6)
		{
			((ModNPC)this).NPC.frame.Y = 0;
		}
		if (Timer > 500 && Timer < 570)
		{
			((ModNPC)this).NPC.frame.Y = 7 * frameHeight;
		}
		if (Timer > 840)
		{
			((ModNPC)this).NPC.frame.Y = 6 * frameHeight;
		}
	}

	public override void OnKill()
	{
		if (Main.rand.Next(5) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DarkMatter").Type, 1, false, 0, false, false);
		}
	}
}
