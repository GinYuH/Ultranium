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
		//DisplayName.SetDefault("Xenanis Apparition");
		Main.npcFrameCount[NPC.type] = 12;
		NPCID.Sets.TrailCacheLength[NPC.type] = 10;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		NPC.width = 130;
		NPC.height = 156;
		NPC.damage = 40;
		NPC.lifeMax = 26000;
		NPC.defense = 50;
		NPC.knockBackResist = 0f;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.HitSound = SoundID.NPCHit7;
		NPC.DeathSound = SoundID.NPCDeath10;
		NPC.npcSlots = 1f;
		NPC.lavaImmune = true;
		NPC.alpha = 0;
		NPC.buffImmune[24] = true;
		NPC.netAlways = true;
		NPC.alpha = 255;
		NPC.aiStyle = -1;
		players = 1;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance * bossAdjustment);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/Ethereal/XenanisClone1").Width() * 0.5f, (float)NPC.height * 0.5f);
			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/Ethereal/XenanisClone1").Value, position, NPC.frame, color, NPC.rotation, vector, NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override void FindFrame(int frameHeight)
	{
		if (!attacking)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y >= frameHeight * 6)
			{
				NPC.frame.Y = 0;
			}
		}
		if (attacking)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y >= frameHeight * 12)
			{
				NPC.frame.Y = frameHeight * 6;
			}
		}
	}

	public override bool PreAI()
	{
		NPC.spriteDirection = NPC.direction;
		NPC.rotation = NPC.velocity.X * 0.02f;
		Player player = Main.player[NPC.target];
		NPC.netUpdate = true;
		NPC.TargetClosest();
		int num = (Main.expertMode ? 30 : 45);
		if (Main.player[NPC.target].dead || Main.dayTime)
		{
			NPC.ai[0] += 1f;
			NPC.velocity.Y = 40f;
			NPC.ai[3] += 1f;
			if (NPC.ai[3] >= 100f)
			{
				NPC.active = false;
			}
		}
		if (NPC.ai[0] == 0f)
		{
			if (NPC.Center.X >= player.Center.X && moveSpeed >= -53)
			{
				moveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && moveSpeed <= 53)
			{
				moveSpeed++;
			}
			NPC.velocity.X = (float)moveSpeed * 0.09f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			NPC.velocity.Y = (float)moveSpeedY * 0.1f;
		}
		if (NPC.ai[0] == 1f)
		{
			NPC.velocity *= 0f;
		}
		timer++;
		if (timer < 60)
		{
			NPC.alpha -= 3;
			NPC.ai[0] = 1f;
		}
		if (timer == 60)
		{
			NPC.ai[0] = 0f;
		}
		if (timer == 120 || timer == 180 || timer == 240)
		{
			for (int i = -1; i <= 1; i++)
			{
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 6f * NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)i), Mod.Find<ModProjectile>("EtherealCloneFlame").Type, num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 300)
		{
			for (int j = 0; j < 50; j++)
			{
				int num2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
				Main.dust[num2].scale = 1.5f;
			}
			NPC.position.X = player.position.X - 50f;
			NPC.position.Y = player.position.Y - 500f;
			for (int k = 0; k < 50; k++)
			{
				int num3 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.PurpleTorch);
				Main.dust[num3].scale = 1.5f;
			}
			NPC.ai[0] = 1f;
		}
		if (timer == 330)
		{
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("EtherealPortalBig").Type, num + 30, 1f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 440 || timer == 460 || timer == 480 || timer == 500 || timer == 520 || timer == 540 || timer == 560)
		{
			float num4 = 12f;
			int num5 = Mod.Find<ModProjectile>("EtherealFireBall").Type;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
			float num6 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num6) * (double)num4 * -1.0), (float)(Math.Sin(num6) * (double)num4 * -1.0), num5, num, 0f, Main.myPlayer, 0f, 0f);
			NPC.ai[0] = 0f;
		}
		if (timer == 620)
		{
			NPC.position.X = player.position.X - 50f;
			NPC.position.Y = player.position.Y - 400f;
			NPC.ai[0] = 1f;
		}
		if (timer == 670 || timer == 690 || timer == 720)
		{
			float num7 = 10f;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
			float num8 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			_ = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num8) * (double)num7 * -1.0), (float)(Math.Sin(num8) * (double)num7 * -1.0), Mod.Find<ModProjectile>("EtherealDeathray").Type, num + 10, 0f, 0, 0f, 0f)];
		}
		if (timer == 760)
		{
			timer = 60;
			NPC.ai[0] = 0f;
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
