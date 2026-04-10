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
		((ModNPC)this).DisplayName.SetDefault("Mother Phantom");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 5;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).npc.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).npc.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.scale = 1f;
		((ModNPC)this).npc.width = 244;
		((ModNPC)this).npc.height = 190;
		((ModNPC)this).npc.damage = 85;
		((ModNPC)this).npc.lifeMax = 28500;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.HitSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/PhantomHit");
		((ModNPC)this).npc.DeathSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/MotherPhantomDeath")?.WithVolume(2f)?.WithPitchVariance(0.5f);
		((ModNPC)this).npc.defense = 35;
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.aiStyle = 0;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("MotherPhantomBanner");
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		((ModNPC)this).npc.lifeMax = 35000;
		((ModNPC)this).npc.damage = 120;
		((ModNPC)this).npc.defense = 50;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		if (((ModNPC)this).npc.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/MotherPhantomTrail").Width * 0.5f, (float)((ModNPC)this).npc.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).npc.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).npc.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).npc.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).npc.gfxOffY);
				Color color = ((ModNPC)this).npc.GetAlpha(drawColor) * ((float)(((ModNPC)this).npc.oldPos.Length - i) / (float)((ModNPC)this).npc.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/MotherPhantomTrail"), position, ((ModNPC)this).npc.frame, color, ((ModNPC)this).npc.rotation, vector, ((ModNPC)this).npc.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/MotherPhantomGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/MotherPhantomGore2"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/MotherPhantomGore3"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/MotherPhantomGore4"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowEvent/MotherPhantomGore5"));
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitPlayer(Player player, int damage, bool crit)
	{
		player.AddBuff(((ModNPC)this).mod.BuffType("DarkDebuff"), 300);
	}

	public override void AI()
	{
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).npc.target];
		bool expertMode = Main.expertMode;
		((ModNPC)this).npc.netUpdate = true;
		((ModNPC)this).npc.TargetClosest();
		int num = (expertMode ? 38 : 42);
		timer++;
		if (timer == 120)
		{
			for (int i = 0; i < 50; i++)
			{
				int num2 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 89);
				Main.dust[num2].scale = 1.5f;
			}
			((ModNPC)this).npc.position.X = player.position.X - 100f;
			((ModNPC)this).npc.position.Y = player.position.Y - 400f;
			for (int j = 0; j < 50; j++)
			{
				int num3 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 89);
				Main.dust[num3].scale = 1.5f;
			}
			((ModNPC)this).npc.ai[0] = 1f;
		}
		if (timer == 180 || timer == 210 || timer == 240 || timer == 270 || timer == 300)
		{
			float num4 = 8f;
			float num5 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0), ((ModNPC)this).mod.ProjectileType("MotherPhantomBolt"), 40, 0f, 0, 0f, 0f);
		}
		if (timer >= 360 && timer <= 540)
		{
			((ModNPC)this).npc.ai[0] = 2f;
		}
		if (timer == 540)
		{
			for (int k = 0; k < 50; k++)
			{
				int num6 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("ShadowDustPurple"));
				Main.dust[num6].scale = 1.5f;
			}
			((ModNPC)this).npc.position.X = player.position.X - 100f;
			((ModNPC)this).npc.position.Y = player.position.Y - 400f;
			for (int l = 0; l < 50; l++)
			{
				int num7 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("ShadowDustPurple"));
				Main.dust[num7].scale = 1.5f;
			}
			((ModNPC)this).npc.ai[0] = 0f;
		}
		if (timer == 600 || timer == 630)
		{
			Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector.Normalize();
			vector.X *= 5f;
			vector.Y *= 5f;
			int num8 = 5;
			for (int m = 0; m < num8; m++)
			{
				float num9 = (float)Main.rand.Next(-300, 300) * 0.01f;
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector.X + num9, vector.Y + num9, ((ModNPC)this).mod.ProjectileType("PhantomWave"), num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 660)
		{
			for (int n = 0; n < 50; n++)
			{
				int num10 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("ShadowDustPurple"));
				Main.dust[num10].scale = 1.5f;
			}
			((ModNPC)this).npc.position.X = player.position.X - 100f;
			((ModNPC)this).npc.position.Y = player.position.Y - 400f;
			for (int num11 = 0; num11 < 50; num11++)
			{
				int num12 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("ShadowDustPurple"));
				Main.dust[num12].scale = 1.5f;
			}
		}
		if ((timer == 720 || timer == 760) && NPC.CountNPCS(((ModNPC)this).mod.NPCType("Phantom")) < 2)
		{
			NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y, ((ModNPC)this).mod.NPCType("Phantom"), 0, 0f, 0f, 0f, 0f, 255);
		}
		if (timer == 770)
		{
			timer = 0;
		}
		if (((ModNPC)this).npc.ai[0] == 0f)
		{
			((ModNPC)this).npc.velocity.X = (((ModNPC)this).npc.velocity.Y = 0f);
		}
		if (((ModNPC)this).npc.ai[0] == 1f)
		{
			if (((ModNPC)this).npc.Center.X >= player.Center.X && moveSpeed >= -50)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).npc.Center.X <= player.Center.X && moveSpeed <= 50)
			{
				moveSpeed++;
			}
			((ModNPC)this).npc.velocity.X = (float)moveSpeed * 0.1f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -50)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 50)
			{
				moveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)moveSpeedY * 0.12f;
		}
		if (((ModNPC)this).npc.ai[0] == 2f)
		{
			if (((ModNPC)this).npc.Center.X >= player.Center.X && moveSpeed >= -100)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).npc.Center.X <= player.Center.X && moveSpeed <= 100)
			{
				moveSpeed++;
			}
			((ModNPC)this).npc.velocity.X = (float)moveSpeed * 0.1f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -100)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 100)
			{
				moveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)moveSpeedY * 0.12f;
		}
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter >= 8.0)
		{
			((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
			((ModNPC)this).npc.frameCounter = 1.0;
		}
	}

	public override void NPCLoot()
	{
		Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DarkMatter"), Main.rand.Next(3, 8), false, 0, false, false);
	}
}
