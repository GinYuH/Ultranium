using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ethereal;

[AutoloadBossHead]
public class Xenanis : ModNPC
{
	private int timer;

	private int timer2;

	private int moveSpeed;

	private int moveSpeedY;

	private float HomeY = 100f;

	public bool attacking;

	public static bool Clones;

	public static bool ClonesSpawned;

	public bool Phase2;

	public bool Transition;

	public int players;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Xenanis");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 12;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).npc.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).npc.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 130;
		((ModNPC)this).npc.height = 156;
		((ModNPC)this).npc.damage = 40;
		((ModNPC)this).npc.lifeMax = 52000;
		((ModNPC)this).npc.defense = 50;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.boss = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit7;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath10;
		((ModNPC)this).npc.value = Item.buyPrice(0, 15);
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.alpha = 0;
		((ModNPC)this).npc.buffImmune[24] = true;
		base.bossBag = ((ModNPC)this).mod.ItemType("EtherealBag");
		base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/Xenanis");
		((ModNPC)this).npc.netAlways = true;
		((ModNPC)this).npc.aiStyle = -1;
		players = 1;
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		players = numPlayers;
		((ModNPC)this).npc.lifeMax = 63000 + numPlayers * 6300;
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
			if (((ModNPC)this).npc.Center.X >= player.Center.X && moveSpeed >= -100)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).npc.Center.X <= player.Center.X && moveSpeed <= 100)
			{
				moveSpeed++;
			}
			((ModNPC)this).npc.velocity.X = (float)moveSpeed * 0.1f;
			if (((ModNPC)this).npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			((ModNPC)this).npc.velocity.Y = (float)moveSpeedY * 0.12f;
		}
		if (((ModNPC)this).npc.ai[0] == 2f)
		{
			((ModNPC)this).npc.velocity *= 0f;
		}
		if (((ModNPC)this).npc.life > ((ModNPC)this).npc.lifeMax / 2 && !Transition)
		{
			timer++;
			if (timer == 120 || timer == 150 || timer == 180 || timer == 210 || timer == 240 || timer == 270 || timer == 300)
			{
				((ModNPC)this).npc.ai[0] = 0f;
				float num2 = 6.5f;
				int num3 = 299;
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				float num4 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num4) * (double)num2 * -1.0), (float)(Math.Sin(num4) * (double)num2 * -1.0), num3, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 360 || timer == 450 || timer == 540 || timer == 630 || timer == 720)
			{
				((ModNPC)this).npc.ai[0] = 2f;
				for (int i = 0; i < 50; i++)
				{
					int num5 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
					Main.dust[num5].scale = 1.5f;
				}
				int num6 = Main.rand.Next(4);
				if (num6 == 0)
				{
					((ModNPC)this).npc.position.X = player.position.X + 500f;
					((ModNPC)this).npc.position.Y = player.position.Y + 300f;
				}
				if (num6 == 1)
				{
					((ModNPC)this).npc.position.X = player.position.X + 500f;
					((ModNPC)this).npc.position.Y = player.position.Y - 400f;
				}
				if (num6 == 2)
				{
					((ModNPC)this).npc.position.X = player.position.X - 600f;
					((ModNPC)this).npc.position.Y = player.position.Y - 400f;
				}
				if (num6 == 3)
				{
					((ModNPC)this).npc.position.X = player.position.X - 600f;
					((ModNPC)this).npc.position.Y = player.position.Y + 300f;
				}
				for (int j = 0; j < 50; j++)
				{
					int num7 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
					Main.dust[num7].scale = 1.5f;
				}
			}
			if (timer == 380 || timer == 470 || timer == 560 || timer == 650 || timer == 740)
			{
				for (int k = -2; k <= 2; k++)
				{
					Projectile.NewProjectile(((ModNPC)this).npc.Center, 7.5f * ((ModNPC)this).npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(6f) * (float)k), ((ModNPC)this).mod.ProjectileType("EtherealBlast"), num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (timer == 800)
			{
				for (int l = 0; l < 50; l++)
				{
					int num8 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
					Main.dust[num8].scale = 1.5f;
				}
				((ModNPC)this).npc.position.X = player.position.X - 50f;
				((ModNPC)this).npc.position.Y = player.position.Y - 400f;
				for (int m = 0; m < 50; m++)
				{
					int num9 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
					Main.dust[num9].scale = 1.5f;
				}
			}
			if (timer == 860)
			{
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X + 100f, ((ModNPC)this).npc.Center.Y + 100f, 0f, 0f, ((ModNPC)this).mod.ProjectileType("EtherealPortalSmall"), num, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X + 100f, ((ModNPC)this).npc.Center.Y - 100f, 0f, 0f, ((ModNPC)this).mod.ProjectileType("EtherealPortalSmall"), num, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X - 100f, ((ModNPC)this).npc.Center.Y + 100f, 0f, 0f, ((ModNPC)this).mod.ProjectileType("EtherealPortalSmall"), num, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X - 100f, ((ModNPC)this).npc.Center.Y - 100f, 0f, 0f, ((ModNPC)this).mod.ProjectileType("EtherealPortalSmall"), num, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 950)
			{
				((ModNPC)this).npc.ai[0] = 1f;
			}
			if (timer == 1010 || timer == 1070 || timer == 1130 || timer == 1190 || timer == 1250)
			{
				float num10 = 10f;
				int num11 = ((ModNPC)this).mod.ProjectileType("EtherealFireBall");
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				float num12 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num12) * (double)num10 * -1.0), (float)(Math.Sin(num12) * (double)num10 * -1.0), num11, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 1340)
			{
				((ModNPC)this).npc.ai[0] = 2f;
				for (int n = 0; n < 50; n++)
				{
					int num13 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
					Main.dust[num13].scale = 1.5f;
				}
				((ModNPC)this).npc.position.X = player.position.X - 100f;
				((ModNPC)this).npc.position.Y = player.position.Y - 500f;
				for (int num14 = 0; num14 < 50; num14++)
				{
					int num15 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
					Main.dust[num15].scale = 1.5f;
				}
			}
			if (timer == 1370)
			{
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("EtherealLaserRift"), num, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer >= 1900)
			{
				timer = 0;
				((ModNPC)this).npc.ai[0] = 0f;
			}
		}
		if (((ModNPC)this).npc.localAI[1] == 0f && ((ModNPC)this).npc.life <= ((ModNPC)this).npc.lifeMax / 2)
		{
			Projectile.NewProjectile(player.Center.X + 500f, player.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("PurpleCloneSpawner"), 0, 1f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(player.Center.X - 500f, player.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("BlackCloneSpawner"), 0, 1f, Main.myPlayer, 0f, 0f);
			((ModNPC)this).npc.localAI[1] += 1f;
		}
		if (NPC.AnyNPCs(((ModNPC)this).mod.NPCType("XenanisClone1")) || NPC.AnyNPCs(((ModNPC)this).mod.NPCType("XenanisClone2")))
		{
			Transition = true;
			((ModNPC)this).npc.immortal = true;
			((ModNPC)this).npc.dontTakeDamage = true;
			((ModNPC)this).npc.position.X = player.position.X - 100f;
			((ModNPC)this).npc.position.Y = player.position.Y - 500f;
			timer = 0;
			timer2 = 0;
		}
		else
		{
			Transition = false;
			((ModNPC)this).npc.immortal = false;
			((ModNPC)this).npc.dontTakeDamage = false;
		}
		if (((ModNPC)this).npc.life <= ((ModNPC)this).npc.lifeMax / 2 && !Transition)
		{
			timer = 0;
			timer2++;
			if (timer2 < 200 && timer2 > 60)
			{
				((ModNPC)this).npc.ai[0] = 1f;
			}
			if (timer2 == 80 || timer2 == 100 || timer2 == 120 || timer2 == 140 || timer2 == 160 || timer2 == 180)
			{
				float num16 = 12f;
				int num17 = ((ModNPC)this).mod.ProjectileType("EtherealFireBall");
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				float num18 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num18) * (double)num16 * -1.0), (float)(Math.Sin(num18) * (double)num16 * -1.0), num17, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 240 || timer2 == 330 || timer2 == 420 || timer2 == 510)
			{
				((ModNPC)this).npc.ai[0] = 2f;
				for (int num19 = 0; num19 < 50; num19++)
				{
					int num20 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
					Main.dust[num20].scale = 1.5f;
				}
				((ModNPC)this).npc.position.X = player.position.X - 50f;
				((ModNPC)this).npc.position.Y = player.position.Y - 400f;
				for (int num21 = 0; num21 < 50; num21++)
				{
					int num22 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
					Main.dust[num22].scale = 1.5f;
				}
			}
			if (timer2 == 260 || timer2 == 350 || timer2 == 450)
			{
				Main.PlaySound(SoundID.Item103, ((ModNPC)this).npc.position);
				Vector2 spinningpoint = new Vector2(5f, 0f).RotatedByRandom(Math.PI * 2.0);
				for (int num23 = 0; num23 < 12; num23++)
				{
					Vector2 vector = spinningpoint.RotatedBy(Math.PI / 3.0 * ((double)num23 + Main.rand.NextDouble() - 0.5));
					Projectile.NewProjectile(((ModNPC)this).npc.Center, vector, ((ModNPC)this).mod.ProjectileType("EtherealBlast"), num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (timer2 == 540)
			{
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("EtherealPortalBig"), num + 30, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 720)
			{
				((ModNPC)this).npc.ai[0] = 1f;
			}
			if (timer2 == 740 || timer2 == 760 || timer2 == 780 || timer2 == 800 || timer2 == 820)
			{
				float num24 = 10f;
				int num25 = ((ModNPC)this).mod.ProjectileType("EtherealPortalSmall");
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				float num26 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num26) * (double)num24 * -1.0), (float)(Math.Sin(num26) * (double)num24 * -1.0), num25, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 940)
			{
				((ModNPC)this).npc.ai[0] = 2f;
				for (int num27 = 0; num27 < 50; num27++)
				{
					int num28 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
					Main.dust[num28].scale = 1.5f;
				}
				((ModNPC)this).npc.position.X = player.position.X - 100f;
				((ModNPC)this).npc.position.Y = player.position.Y - 500f;
				for (int num29 = 0; num29 < 50; num29++)
				{
					int num30 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 62);
					Main.dust[num30].scale = 1.5f;
				}
			}
			if (timer2 == 980)
			{
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("EtherealLaserRift2"), num + 10, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 1480)
			{
				timer2 = 0;
				((ModNPC)this).npc.ai[0] = 0f;
			}
		}
		if (timer < 800 || (timer > 950 && timer < 1340))
		{
			attacking = false;
		}
		if ((timer > 800 && timer < 950) || timer > 1340 || (timer2 >= 520 && timer2 < 720) || timer2 >= 940)
		{
			attacking = true;
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/XenanisGore1"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/XenanisGore2"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/XenanisGore3"));
		Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/XenanisGore4"));
		return true;
	}

	public override void NPCLoot()
	{
		if (Main.expertMode)
		{
			((ModNPC)this).npc.DropBossBags();
		}
		else
		{
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("EtherealSword"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("EtherealBow"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("EtherealTome"), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("EtherealSummon"), 1, false, 0, false, false);
			}
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("XenanisFlesh"), Main.rand.Next(10, 18), false, 0, false, false);
			if (Main.rand.Next(20) == 0)
			{
				Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("XenanisWings"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("EtherealDidgeridoo"), 1, false, 0, false, false);
			}
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("XenanisMask"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("XenanisTrophyItem"), 1, false, 0, false, false);
		}
		if (!UltraniumWorld.downedXenanis)
		{
			UltraniumWorld.downedXenanis = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
	}

	public override void BossLoot(ref string name, ref int potionType)
	{
		potionType = 499;
	}
}
