using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
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
		// ((ModNPC)this).DisplayName.SetDefault("Xenanis Apparition");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 12;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).NPC.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 130;
		((ModNPC)this).NPC.height = 156;
		((ModNPC)this).NPC.damage = 40;
		((ModNPC)this).NPC.lifeMax = 26000;
		((ModNPC)this).NPC.defense = 50;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit7;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath10;
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.lavaImmune = true;
		((ModNPC)this).NPC.alpha = 0;
		((ModNPC)this).NPC.buffImmune[24] = true;
		((ModNPC)this).NPC.netAlways = true;
		((ModNPC)this).NPC.alpha = 255;
		((ModNPC)this).NPC.aiStyle = -1;
		players = 1;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		((ModNPC)this).NPC.lifeMax = 32000 + numPlayers * 3200;
		((ModNPC)this).NPC.damage = 65;
		((ModNPC)this).NPC.defense = 65;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (((ModNPC)this).NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ethereal/XenanisClone1").Width * 0.5f, (float)((ModNPC)this).NPC.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY);
				Color color = ((ModNPC)this).NPC.GetAlpha(drawColor) * ((float)(((ModNPC)this).NPC.oldPos.Length - i) / (float)((ModNPC)this).NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ethereal/XenanisClone1"), position, ((ModNPC)this).NPC.frame, color, ((ModNPC)this).NPC.rotation, vector, ((ModNPC)this).NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override void FindFrame(int frameHeight)
	{
		if (!attacking)
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
		}
		if (attacking)
		{
			((ModNPC)this).NPC.frameCounter += 1.0;
			if (((ModNPC)this).NPC.frameCounter > 6.0)
			{
				((ModNPC)this).NPC.frame.Y = ((ModNPC)this).NPC.frame.Y + frameHeight;
				((ModNPC)this).NPC.frameCounter = 0.0;
			}
			if (((ModNPC)this).NPC.frame.Y >= frameHeight * 12)
			{
				((ModNPC)this).NPC.frame.Y = frameHeight * 6;
			}
		}
	}

	public override bool PreAI()
	{
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).NPC.target];
		((ModNPC)this).NPC.netUpdate = true;
		((ModNPC)this).NPC.TargetClosest();
		int num = (Main.expertMode ? 30 : 45);
		if (Main.player[((ModNPC)this).NPC.target].dead || Main.dayTime)
		{
			((ModNPC)this).NPC.ai[0] += 1f;
			((ModNPC)this).NPC.velocity.Y = 40f;
			((ModNPC)this).NPC.ai[3] += 1f;
			if (((ModNPC)this).NPC.ai[3] >= 100f)
			{
				((Entity)((ModNPC)this).NPC).active = false;
			}
		}
		if (((ModNPC)this).NPC.ai[0] == 0f)
		{
			if (((ModNPC)this).NPC.Center.X >= player.Center.X && moveSpeed >= -53)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).NPC.Center.X <= player.Center.X && moveSpeed <= 53)
			{
				moveSpeed++;
			}
			((ModNPC)this).NPC.velocity.X = (float)moveSpeed * 0.09f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)moveSpeedY * 0.1f;
		}
		if (((ModNPC)this).NPC.ai[0] == 1f)
		{
			((ModNPC)this).NPC.velocity *= 0f;
		}
		timer++;
		if (timer < 60)
		{
			((ModNPC)this).NPC.alpha -= 3;
			((ModNPC)this).NPC.ai[0] = 1f;
		}
		if (timer == 60)
		{
			((ModNPC)this).NPC.ai[0] = 0f;
		}
		if (timer == 120 || timer == 180 || timer == 240)
		{
			for (int i = -1; i <= 1; i++)
			{
				Projectile.NewProjectile(((ModNPC)this).NPC.Center, 6f * ((ModNPC)this).NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)i), ((ModNPC)this).Mod.Find<ModProjectile>("EtherealCloneFlame").Type, num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 300)
		{
			for (int j = 0; j < 50; j++)
			{
				int num2 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
				Main.dust[num2].scale = 1.5f;
			}
			((ModNPC)this).NPC.position.X = player.position.X - 50f;
			((ModNPC)this).NPC.position.Y = player.position.Y - 500f;
			for (int k = 0; k < 50; k++)
			{
				int num3 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
				Main.dust[num3].scale = 1.5f;
			}
			((ModNPC)this).NPC.ai[0] = 1f;
		}
		if (timer == 330)
		{
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("EtherealPortalBig").Type, num + 30, 1f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 440 || timer == 460 || timer == 480 || timer == 500 || timer == 520 || timer == 540 || timer == 560)
		{
			float num4 = 12f;
			int num5 = ((ModNPC)this).Mod.Find<ModProjectile>("EtherealFireBall").Type;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
			float num6 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num6) * (double)num4 * -1.0), (float)(Math.Sin(num6) * (double)num4 * -1.0), num5, num, 0f, Main.myPlayer, 0f, 0f);
			((ModNPC)this).NPC.ai[0] = 0f;
		}
		if (timer == 620)
		{
			((ModNPC)this).NPC.position.X = player.position.X - 50f;
			((ModNPC)this).NPC.position.Y = player.position.Y - 400f;
			((ModNPC)this).NPC.ai[0] = 1f;
		}
		if (timer == 670 || timer == 690 || timer == 720)
		{
			float num7 = 10f;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
			float num8 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			_ = Main.projectile[Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num8) * (double)num7 * -1.0), (float)(Math.Sin(num8) * (double)num7 * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("EtherealDeathray").Type, num + 10, 0f, 0, 0f, 0f)];
		}
		if (timer == 760)
		{
			timer = 60;
			((ModNPC)this).NPC.ai[0] = 0f;
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
