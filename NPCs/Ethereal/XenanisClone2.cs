using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal;

public class XenanisClone2 : ModNPC
{
	private Player player;

	private float speed;

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

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		NPC.lifeMax = 32000 + numPlayers * 3200;
		NPC.damage = 65;
		NPC.defense = 65;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/Ethereal/XenanisClone2").Width() * 0.5f, (float)NPC.height * 0.5f);
			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/Ethereal/XenanisClone2").Value, position, NPC.frame, color, NPC.rotation, vector, NPC.scale, effects, 0f);
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
				((Entity)NPC).active = false;
			}
		}
		NPC.velocity *= 0.985f;
		timer++;
		if (timer < 60)
		{
			NPC.alpha -= 3;
			NPC.ai[0] = 1f;
		}
		if (timer == 140 || timer == 200 || timer == 260 || timer == 320)
		{
			Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
			vector.Normalize();
			vector.X *= 17f;
			vector.Y *= 17f;
			NPC.velocity.X = vector.X;
			NPC.velocity.Y = vector.Y;
		}
		if (timer > 380 && timer < 680)
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
			if (timer == 400 || timer == 440 || timer == 480 || timer == 520 || timer == 560 || timer == 600 || timer == 640)
			{
				float num2 = 6.5f;
				int num3 = Mod.Find<ModProjectile>("EtherealWave").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				float num4 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num4) * (double)num2 * -1.0), (float)(Math.Sin(num4) * (double)num2 * -1.0), num3, num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer >= 680 && timer < 800)
		{
			NPC.velocity *= 0f;
		}
		if (timer == 680 || timer == 760)
		{
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
			float num5 = 20f;
			float num6 = 0f;
			if (timer == 680)
			{
				num6 = 13f;
			}
			if (timer == 760)
			{
				num6 = 17f;
			}
			float num7 = MathHelper.ToRadians(360f);
			for (int i = 0; (float)i < num6; i++)
			{
				Vector2 vector2 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num7, num7, (float)i / num6)) * num5;
				SoundEngine.PlaySound(SoundID.Item78, NPC.Center);
				Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vector2, Mod.Find<ModProjectile>("XenanisTentacle").Type, num, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 200f;
			}
		}
		if (timer >= 820)
		{
			timer = 60;
		}
		return true;
	}
}
