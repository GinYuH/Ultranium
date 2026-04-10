using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal;

public class XenanisClone1 : ModNPC
{
	private int timer;

	private int moveSpeed;

	private int moveSpeedY;

	private float HomeY = 100f;

	public bool attacking;

	public int players;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Xenanis Apparition");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 12;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).npc.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).npc.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 130;
		((ModNPC)this).npc.height = 156;
		((ModNPC)this).npc.damage = 40;
		((ModNPC)this).npc.lifeMax = 26000;
		((ModNPC)this).npc.defense = 50;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit7;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath10;
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.alpha = 0;
		((ModNPC)this).npc.buffImmune[24] = true;
		((ModNPC)this).npc.netAlways = true;
		((ModNPC)this).npc.alpha = 255;
		((ModNPC)this).npc.aiStyle = -1;
		players = 1;
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		players = numPlayers;
		((ModNPC)this).npc.lifeMax = 32000 + numPlayers * 3200;
		((ModNPC)this).npc.damage = 65;
		((ModNPC)this).npc.defense = 65;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		if (((ModNPC)this).npc.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ethereal/XenanisClone1").Width * 0.5f, (float)((ModNPC)this).npc.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).npc.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).npc.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).npc.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).npc.gfxOffY);
				Color color = ((ModNPC)this).npc.GetAlpha(drawColor) * ((float)(((ModNPC)this).npc.oldPos.Length - i) / (float)((ModNPC)this).npc.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ethereal/XenanisClone1"), position, ((ModNPC)this).npc.frame, color, ((ModNPC)this).npc.rotation, vector, ((ModNPC)this).npc.scale, effects, 0f);
			}
		}
		return true;
	}

	public override void FindFrame(int frameHeight)
	{
		if (!attacking)
		{
			((ModNPC)this).npc.frameCounter += 1.0;
			if (((ModNPC)this).npc.frameCounter > 6.0)
			{
				((ModNPC)this).npc.frame.Y = ((ModNPC)this).npc.frame.Y + frameHeight;
				((ModNPC)this).npc.frameCounter = 0.0;
			}
			if (((ModNPC)this).npc.frame.Y >= frameHeight * 6)
			{
				((ModNPC)this).npc.frame.Y = 0;
			}
		}
		if (attacking)
		{
			((ModNPC)this).npc.frameCounter += 1.0;
			if (((ModNPC)this).npc.frameCounter > 6.0)
			{
				((ModNPC)this).npc.frame.Y = ((ModNPC)this).npc.frame.Y + frameHeight;
				((ModNPC)this).npc.frameCounter = 0.0;
			}
			if (((ModNPC)this).npc.frame.Y >= frameHeight * 12)
			{
				((ModNPC)this).npc.frame.Y = frameHeight * 6;
			}
		}
	}

	public override bool PreAI()
	{
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).npc.target];
		((ModNPC)this).npc.netUpdate = true;
		((ModNPC)this).npc.TargetClosest();
		int num = (Main.expertMode ? 30 : 45);
		if (Main.player[((ModNPC)this).npc.target].dead || Main.dayTime)
		{
			((ModNPC)this).npc.ai[0] += 1f;
			((ModNPC)this).npc.velocity.Y = 40f;
			((ModNPC)this).npc.ai[3] += 1f;
			if (((ModNPC)this).npc.ai[3] >= 100f)
			{
				((Entity)((ModNPC)this).npc).active = false;
			}
		}
		if (((ModNPC)this).npc.ai[0] == 0f)
		{
			if (((ModNPC)this).npc.Center.X >= player.Center.X && moveSpeed >= -53)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).npc.Center.X <= player.Center.X && moveSpeed <= 53)
			{
				moveSpeed++;
			}
			((ModNPC)this).npc.velocity.X = (float)moveSpeed * 0.09f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)moveSpeedY * 0.1f;
		}
		if (((ModNPC)this).npc.ai[0] == 1f)
		{
			((ModNPC)this).npc.velocity *= 0f;
		}
		timer++;
		if (timer < 60)
		{
			((ModNPC)this).npc.alpha -= 3;
			((ModNPC)this).npc.ai[0] = 1f;
		}
		if (timer == 60)
		{
			((ModNPC)this).npc.ai[0] = 0f;
		}
		if (timer == 120 || timer == 180 || timer == 240)
		{
			for (int i = -1; i <= 1; i++)
			{
				Projectile.NewProjectile(((ModNPC)this).npc.Center, 6f * ((ModNPC)this).npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)i), ((ModNPC)this).mod.ProjectileType("EtherealCloneFlame"), num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 300)
		{
			for (int j = 0; j < 50; j++)
			{
				int num2 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
				Main.dust[num2].scale = 1.5f;
			}
			((ModNPC)this).npc.position.X = player.position.X - 50f;
			((ModNPC)this).npc.position.Y = player.position.Y - 500f;
			for (int k = 0; k < 50; k++)
			{
				int num3 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
				Main.dust[num3].scale = 1.5f;
			}
			((ModNPC)this).npc.ai[0] = 1f;
		}
		if (timer == 330)
		{
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("EtherealPortalBig"), num + 30, 1f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 440 || timer == 460 || timer == 480 || timer == 500 || timer == 520 || timer == 540 || timer == 560)
		{
			float num4 = 12f;
			int num5 = ((ModNPC)this).mod.ProjectileType("EtherealFireBall");
			Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
			float num6 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num6) * (double)num4 * -1.0), (float)(Math.Sin(num6) * (double)num4 * -1.0), num5, num, 0f, Main.myPlayer, 0f, 0f);
			((ModNPC)this).npc.ai[0] = 0f;
		}
		if (timer == 620)
		{
			((ModNPC)this).npc.position.X = player.position.X - 50f;
			((ModNPC)this).npc.position.Y = player.position.Y - 400f;
			((ModNPC)this).npc.ai[0] = 1f;
		}
		if (timer == 670 || timer == 690 || timer == 720)
		{
			float num7 = 10f;
			Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
			float num8 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
			_ = Main.projectile[Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num8) * (double)num7 * -1.0), (float)(Math.Sin(num8) * (double)num7 * -1.0), ((ModNPC)this).mod.ProjectileType("EtherealDeathray"), num + 10, 0f, 0, 0f, 0f)];
		}
		if (timer == 760)
		{
			timer = 60;
			((ModNPC)this).npc.ai[0] = 0f;
		}
		if ((timer > 300 && timer < 400) || (timer > 620 && timer < 750))
		{
			attacking = true;
		}
		else
		{
			attacking = false;
		}
		return true;
	}
}
