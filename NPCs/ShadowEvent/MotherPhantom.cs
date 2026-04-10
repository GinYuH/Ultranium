using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.ShadowEvent;

public class MotherPhantom : ModNPC
{
	private int timer;

	private int moveSpeed;

	private int moveSpeedY;

	private float HomeY = 130f;

	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Mother Phantom");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 5;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).NPC.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.scale = 1f;
		((ModNPC)this).NPC.width = 244;
		((ModNPC)this).NPC.height = 190;
		((ModNPC)this).NPC.damage = 85;
		((ModNPC)this).NPC.lifeMax = 28500;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.HitSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/PhantomHit");
		((ModNPC)this).NPC.DeathSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/MotherPhantomDeath")?.WithVolume(2f)?.WithPitchVariance(0.5f);
		((ModNPC)this).NPC.defense = 35;
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.lavaImmune = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.aiStyle = 0;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("MotherPhantomBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 35000;
		((ModNPC)this).NPC.damage = 120;
		((ModNPC)this).NPC.defense = 50;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (((ModNPC)this).NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/MotherPhantomTrail").Width * 0.5f, (float)((ModNPC)this).NPC.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY);
				Color color = ((ModNPC)this).NPC.GetAlpha(drawColor) * ((float)(((ModNPC)this).NPC.oldPos.Length - i) / (float)((ModNPC)this).NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/MotherPhantomTrail"), position, ((ModNPC)this).NPC.frame, color, ((ModNPC)this).NPC.rotation, vector, ((ModNPC)this).NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/MotherPhantomGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/MotherPhantomGore2"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/MotherPhantomGore3"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/MotherPhantomGore4"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/MotherPhantomGore5"));
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		player.AddBuff(((ModNPC)this).Mod.Find<ModBuff>("DarkDebuff").Type, 300);
	}

	public override void AI()
	{
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).NPC.target];
		bool expertMode = Main.expertMode;
		((ModNPC)this).NPC.netUpdate = true;
		((ModNPC)this).NPC.TargetClosest();
		int num = (expertMode ? 38 : 42);
		timer++;
		if (timer == 120)
		{
			for (int i = 0; i < 50; i++)
			{
				int num2 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 89);
				Main.dust[num2].scale = 1.5f;
			}
			((ModNPC)this).NPC.position.X = player.position.X - 100f;
			((ModNPC)this).NPC.position.Y = player.position.Y - 400f;
			for (int j = 0; j < 50; j++)
			{
				int num3 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 89);
				Main.dust[num3].scale = 1.5f;
			}
			((ModNPC)this).NPC.ai[0] = 1f;
		}
		if (timer == 180 || timer == 210 || timer == 240 || timer == 270 || timer == 300)
		{
			float num4 = 8f;
			float num5 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("MotherPhantomBolt").Type, 40, 0f, 0, 0f, 0f);
		}
		if (timer >= 360 && timer <= 540)
		{
			((ModNPC)this).NPC.ai[0] = 2f;
		}
		if (timer == 540)
		{
			for (int k = 0; k < 50; k++)
			{
				int num6 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("ShadowDustPurple").Type);
				Main.dust[num6].scale = 1.5f;
			}
			((ModNPC)this).NPC.position.X = player.position.X - 100f;
			((ModNPC)this).NPC.position.Y = player.position.Y - 400f;
			for (int l = 0; l < 50; l++)
			{
				int num7 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("ShadowDustPurple").Type);
				Main.dust[num7].scale = 1.5f;
			}
			((ModNPC)this).NPC.ai[0] = 0f;
		}
		if (timer == 600 || timer == 630)
		{
			Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector.Normalize();
			vector.X *= 5f;
			vector.Y *= 5f;
			int num8 = 5;
			for (int m = 0; m < num8; m++)
			{
				float num9 = (float)Main.rand.Next(-300, 300) * 0.01f;
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector.X + num9, vector.Y + num9, ((ModNPC)this).Mod.Find<ModProjectile>("PhantomWave").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 660)
		{
			for (int n = 0; n < 50; n++)
			{
				int num10 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("ShadowDustPurple").Type);
				Main.dust[num10].scale = 1.5f;
			}
			((ModNPC)this).NPC.position.X = player.position.X - 100f;
			((ModNPC)this).NPC.position.Y = player.position.Y - 400f;
			for (int num11 = 0; num11 < 50; num11++)
			{
				int num12 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("ShadowDustPurple").Type);
				Main.dust[num12].scale = 1.5f;
			}
		}
		if ((timer == 720 || timer == 760) && NPC.CountNPCS(((ModNPC)this).Mod.Find<ModNPC>("Phantom").Type) < 2)
		{
			NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y, ((ModNPC)this).Mod.Find<ModNPC>("Phantom").Type, 0, 0f, 0f, 0f, 0f, 255);
		}
		if (timer == 770)
		{
			timer = 0;
		}
		if (((ModNPC)this).NPC.ai[0] == 0f)
		{
			((ModNPC)this).NPC.velocity.X = (((ModNPC)this).NPC.velocity.Y = 0f);
		}
		if (((ModNPC)this).NPC.ai[0] == 1f)
		{
			if (((ModNPC)this).NPC.Center.X >= player.Center.X && moveSpeed >= -50)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).NPC.Center.X <= player.Center.X && moveSpeed <= 50)
			{
				moveSpeed++;
			}
			((ModNPC)this).NPC.velocity.X = (float)moveSpeed * 0.1f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -50)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 50)
			{
				moveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)moveSpeedY * 0.12f;
		}
		if (((ModNPC)this).NPC.ai[0] == 2f)
		{
			if (((ModNPC)this).NPC.Center.X >= player.Center.X && moveSpeed >= -100)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).NPC.Center.X <= player.Center.X && moveSpeed <= 100)
			{
				moveSpeed++;
			}
			((ModNPC)this).NPC.velocity.X = (float)moveSpeed * 0.1f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -100)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 100)
			{
				moveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)moveSpeedY * 0.12f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter >= 8.0)
		{
			((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
			((ModNPC)this).NPC.frameCounter = 1.0;
		}
	}

	public override void OnKill()
	{
		Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DarkMatter").Type, Main.rand.Next(3, 8), false, 0, false, false);
	}
}
