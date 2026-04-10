using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
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
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/Ethereal/XenanisClone2").Width * 0.5f, (float)((ModNPC)this).npc.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).npc.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).npc.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).npc.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).npc.gfxOffY);
				Color color = ((ModNPC)this).npc.GetAlpha(drawColor) * ((float)(((ModNPC)this).npc.oldPos.Length - i) / (float)((ModNPC)this).npc.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/Ethereal/XenanisClone2"), position, ((ModNPC)this).npc.frame, color, ((ModNPC)this).npc.rotation, vector, ((ModNPC)this).npc.scale, effects, 0f);
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
		((ModNPC)this).npc.velocity *= 0.985f;
		timer++;
		if (timer < 60)
		{
			((ModNPC)this).npc.alpha -= 3;
			((ModNPC)this).npc.ai[0] = 1f;
		}
		if (timer == 140 || timer == 200 || timer == 260 || timer == 320)
		{
			Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
			vector.Normalize();
			vector.X *= 17f;
			vector.Y *= 17f;
			((ModNPC)this).npc.velocity.X = vector.X;
			((ModNPC)this).npc.velocity.Y = vector.Y;
		}
		if (timer > 380 && timer < 680)
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
			if (timer == 400 || timer == 440 || timer == 480 || timer == 520 || timer == 560 || timer == 600 || timer == 640)
			{
				float num2 = 6.5f;
				int num3 = ((ModNPC)this).mod.ProjectileType("EtherealWave");
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				float num4 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num4) * (double)num2 * -1.0), (float)(Math.Sin(num4) * (double)num2 * -1.0), num3, num, 0f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer >= 680 && timer < 800)
		{
			((ModNPC)this).npc.velocity *= 0f;
		}
		if (timer == 680 || timer == 760)
		{
			Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
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
				Main.PlaySound(SoundID.Item78, ((ModNPC)this).npc.Center);
				Main.projectile[Projectile.NewProjectile(((ModNPC)this).npc.Center, vector2, ((ModNPC)this).mod.ProjectileType("XenanisTentacle"), num, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 200f;
			}
		}
		if (timer >= 820)
		{
			timer = 60;
		}
		return true;
	}
}
