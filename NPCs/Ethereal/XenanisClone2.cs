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
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ethereal/XenanisClone2").Width * 0.5f, (float)((ModNPC)this).NPC.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY);
				Color color = ((ModNPC)this).NPC.GetAlpha(drawColor) * ((float)(((ModNPC)this).NPC.oldPos.Length - i) / (float)((ModNPC)this).NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ethereal/XenanisClone2"), position, ((ModNPC)this).NPC.frame, color, ((ModNPC)this).NPC.rotation, vector, ((ModNPC)this).NPC.scale, effects, 0f);
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
		((ModNPC)this).NPC.velocity *= 0.985f;
		timer++;
		if (timer < 60)
		{
			((ModNPC)this).NPC.alpha -= 3;
			((ModNPC)this).NPC.ai[0] = 1f;
		}
		if (timer == 140 || timer == 200 || timer == 260 || timer == 320)
		{
			Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector.Normalize();
			vector.X *= 17f;
			vector.Y *= 17f;
			((ModNPC)this).NPC.velocity.X = vector.X;
			((ModNPC)this).NPC.velocity.Y = vector.Y;
		}
		if (timer > 380 && timer < 680)
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
			if (timer == 400 || timer == 440 || timer == 480 || timer == 520 || timer == 560 || timer == 600 || timer == 640)
			{
				float num2 = 6.5f;
				int num3 = ((ModNPC)this).Mod.Find<ModProjectile>("EtherealWave").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
				float num4 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num4) * (double)num2 * -1.0), (float)(Math.Sin(num4) * (double)num2 * -1.0), num3, num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer >= 680 && timer < 800)
		{
			((ModNPC)this).NPC.velocity *= 0f;
		}
		if (timer == 680 || timer == 760)
		{
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
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
				SoundEngine.PlaySound(SoundID.Item78, ((ModNPC)this).NPC.Center);
				Main.projectile[Projectile.NewProjectile(((ModNPC)this).NPC.Center, vector2, ((ModNPC)this).Mod.Find<ModProjectile>("XenanisTentacle").Type, num, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 200f;
			}
		}
		if (timer >= 820)
		{
			timer = 60;
		}
		return true;
	}
}
