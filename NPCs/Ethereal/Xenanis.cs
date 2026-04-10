using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
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
		// ((ModNPC)this).DisplayName.SetDefault("Xenanis");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 12;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).NPC.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 130;
		((ModNPC)this).NPC.height = 156;
		((ModNPC)this).NPC.damage = 40;
		((ModNPC)this).NPC.lifeMax = 52000;
		((ModNPC)this).NPC.defense = 50;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.boss = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit7;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath10;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 15);
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.lavaImmune = true;
		((ModNPC)this).NPC.alpha = 0;
		((ModNPC)this).NPC.buffImmune[24] = true;
		base.bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ((ModNPC)this).Mod.Find<ModItem>("EtherealBag").Type;
		base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/Xenanis");
		((ModNPC)this).NPC.netAlways = true;
		((ModNPC)this).NPC.aiStyle = -1;
		players = 1;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		((ModNPC)this).NPC.lifeMax = 63000 + numPlayers * 6300;
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
			if (((ModNPC)this).NPC.Center.X >= player.Center.X && moveSpeed >= -100)
			{
				moveSpeed--;
			}
			else if (((ModNPC)this).NPC.Center.X <= player.Center.X && moveSpeed <= 100)
			{
				moveSpeed++;
			}
			((ModNPC)this).NPC.velocity.X = (float)moveSpeed * 0.1f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -30)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 30)
			{
				moveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)moveSpeedY * 0.12f;
		}
		if (((ModNPC)this).NPC.ai[0] == 2f)
		{
			((ModNPC)this).NPC.velocity *= 0f;
		}
		if (((ModNPC)this).NPC.life > ((ModNPC)this).NPC.lifeMax / 2 && !Transition)
		{
			timer++;
			if (timer == 120 || timer == 150 || timer == 180 || timer == 210 || timer == 240 || timer == 270 || timer == 300)
			{
				((ModNPC)this).NPC.ai[0] = 0f;
				float num2 = 6.5f;
				int num3 = 299;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
				float num4 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num4) * (double)num2 * -1.0), (float)(Math.Sin(num4) * (double)num2 * -1.0), num3, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 360 || timer == 450 || timer == 540 || timer == 630 || timer == 720)
			{
				((ModNPC)this).NPC.ai[0] = 2f;
				for (int i = 0; i < 50; i++)
				{
					int num5 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
					Main.dust[num5].scale = 1.5f;
				}
				int num6 = Main.rand.Next(4);
				if (num6 == 0)
				{
					((ModNPC)this).NPC.position.X = player.position.X + 500f;
					((ModNPC)this).NPC.position.Y = player.position.Y + 300f;
				}
				if (num6 == 1)
				{
					((ModNPC)this).NPC.position.X = player.position.X + 500f;
					((ModNPC)this).NPC.position.Y = player.position.Y - 400f;
				}
				if (num6 == 2)
				{
					((ModNPC)this).NPC.position.X = player.position.X - 600f;
					((ModNPC)this).NPC.position.Y = player.position.Y - 400f;
				}
				if (num6 == 3)
				{
					((ModNPC)this).NPC.position.X = player.position.X - 600f;
					((ModNPC)this).NPC.position.Y = player.position.Y + 300f;
				}
				for (int j = 0; j < 50; j++)
				{
					int num7 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
					Main.dust[num7].scale = 1.5f;
				}
			}
			if (timer == 380 || timer == 470 || timer == 560 || timer == 650 || timer == 740)
			{
				for (int k = -2; k <= 2; k++)
				{
					Projectile.NewProjectile(((ModNPC)this).NPC.Center, 7.5f * ((ModNPC)this).NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(6f) * (float)k), ((ModNPC)this).Mod.Find<ModProjectile>("EtherealBlast").Type, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (timer == 800)
			{
				for (int l = 0; l < 50; l++)
				{
					int num8 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
					Main.dust[num8].scale = 1.5f;
				}
				((ModNPC)this).NPC.position.X = player.position.X - 50f;
				((ModNPC)this).NPC.position.Y = player.position.Y - 400f;
				for (int m = 0; m < 50; m++)
				{
					int num9 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
					Main.dust[num9].scale = 1.5f;
				}
			}
			if (timer == 860)
			{
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X + 100f, ((ModNPC)this).NPC.Center.Y + 100f, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("EtherealPortalSmall").Type, num, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X + 100f, ((ModNPC)this).NPC.Center.Y - 100f, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("EtherealPortalSmall").Type, num, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X - 100f, ((ModNPC)this).NPC.Center.Y + 100f, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("EtherealPortalSmall").Type, num, 1f, Main.myPlayer, 0f, 0f);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X - 100f, ((ModNPC)this).NPC.Center.Y - 100f, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("EtherealPortalSmall").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 950)
			{
				((ModNPC)this).NPC.ai[0] = 1f;
			}
			if (timer == 1010 || timer == 1070 || timer == 1130 || timer == 1190 || timer == 1250)
			{
				float num10 = 10f;
				int num11 = ((ModNPC)this).Mod.Find<ModProjectile>("EtherealFireBall").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
				float num12 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num12) * (double)num10 * -1.0), (float)(Math.Sin(num12) * (double)num10 * -1.0), num11, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 1340)
			{
				((ModNPC)this).NPC.ai[0] = 2f;
				for (int n = 0; n < 50; n++)
				{
					int num13 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
					Main.dust[num13].scale = 1.5f;
				}
				((ModNPC)this).NPC.position.X = player.position.X - 100f;
				((ModNPC)this).NPC.position.Y = player.position.Y - 500f;
				for (int num14 = 0; num14 < 50; num14++)
				{
					int num15 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
					Main.dust[num15].scale = 1.5f;
				}
			}
			if (timer == 1370)
			{
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("EtherealLaserRift").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer >= 1900)
			{
				timer = 0;
				((ModNPC)this).NPC.ai[0] = 0f;
			}
		}
		if (((ModNPC)this).NPC.localAI[1] == 0f && ((ModNPC)this).NPC.life <= ((ModNPC)this).NPC.lifeMax / 2)
		{
			Projectile.NewProjectile(player.Center.X + 500f, player.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("PurpleCloneSpawner").Type, 0, 1f, Main.myPlayer, 0f, 0f);
			Projectile.NewProjectile(player.Center.X - 500f, player.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("BlackCloneSpawner").Type, 0, 1f, Main.myPlayer, 0f, 0f);
			((ModNPC)this).NPC.localAI[1] += 1f;
		}
		if (NPC.AnyNPCs(((ModNPC)this).Mod.Find<ModNPC>("XenanisClone1").Type) || NPC.AnyNPCs(((ModNPC)this).Mod.Find<ModNPC>("XenanisClone2").Type))
		{
			Transition = true;
			((ModNPC)this).NPC.immortal = true;
			((ModNPC)this).NPC.dontTakeDamage = true;
			((ModNPC)this).NPC.position.X = player.position.X - 100f;
			((ModNPC)this).NPC.position.Y = player.position.Y - 500f;
			timer = 0;
			timer2 = 0;
		}
		else
		{
			Transition = false;
			((ModNPC)this).NPC.immortal = false;
			((ModNPC)this).NPC.dontTakeDamage = false;
		}
		if (((ModNPC)this).NPC.life <= ((ModNPC)this).NPC.lifeMax / 2 && !Transition)
		{
			timer = 0;
			timer2++;
			if (timer2 < 200 && timer2 > 60)
			{
				((ModNPC)this).NPC.ai[0] = 1f;
			}
			if (timer2 == 80 || timer2 == 100 || timer2 == 120 || timer2 == 140 || timer2 == 160 || timer2 == 180)
			{
				float num16 = 12f;
				int num17 = ((ModNPC)this).Mod.Find<ModProjectile>("EtherealFireBall").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
				float num18 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num18) * (double)num16 * -1.0), (float)(Math.Sin(num18) * (double)num16 * -1.0), num17, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 240 || timer2 == 330 || timer2 == 420 || timer2 == 510)
			{
				((ModNPC)this).NPC.ai[0] = 2f;
				for (int num19 = 0; num19 < 50; num19++)
				{
					int num20 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
					Main.dust[num20].scale = 1.5f;
				}
				((ModNPC)this).NPC.position.X = player.position.X - 50f;
				((ModNPC)this).NPC.position.Y = player.position.Y - 400f;
				for (int num21 = 0; num21 < 50; num21++)
				{
					int num22 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
					Main.dust[num22].scale = 1.5f;
				}
			}
			if (timer2 == 260 || timer2 == 350 || timer2 == 450)
			{
				SoundEngine.PlaySound(SoundID.Item103, ((ModNPC)this).NPC.position);
				Vector2 spinningpoint = new Vector2(5f, 0f).RotatedByRandom(Math.PI * 2.0);
				for (int num23 = 0; num23 < 12; num23++)
				{
					Vector2 vector = spinningpoint.RotatedBy(Math.PI / 3.0 * ((double)num23 + Main.rand.NextDouble() - 0.5));
					Projectile.NewProjectile(((ModNPC)this).NPC.Center, vector, ((ModNPC)this).Mod.Find<ModProjectile>("EtherealBlast").Type, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (timer2 == 540)
			{
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("EtherealPortalBig").Type, num + 30, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 720)
			{
				((ModNPC)this).NPC.ai[0] = 1f;
			}
			if (timer2 == 740 || timer2 == 760 || timer2 == 780 || timer2 == 800 || timer2 == 820)
			{
				float num24 = 10f;
				int num25 = ((ModNPC)this).Mod.Find<ModProjectile>("EtherealPortalSmall").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
				float num26 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num26) * (double)num24 * -1.0), (float)(Math.Sin(num26) * (double)num24 * -1.0), num25, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 940)
			{
				((ModNPC)this).NPC.ai[0] = 2f;
				for (int num27 = 0; num27 < 50; num27++)
				{
					int num28 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
					Main.dust[num28].scale = 1.5f;
				}
				((ModNPC)this).NPC.position.X = player.position.X - 100f;
				((ModNPC)this).NPC.position.Y = player.position.Y - 500f;
				for (int num29 = 0; num29 < 50; num29++)
				{
					int num30 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 62);
					Main.dust[num30].scale = 1.5f;
				}
			}
			if (timer2 == 980)
			{
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("EtherealLaserRift2").Type, num + 10, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer2 == 1480)
			{
				timer2 = 0;
				((ModNPC)this).NPC.ai[0] = 0f;
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
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/XenanisGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/XenanisGore2"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/XenanisGore3"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/XenanisGore4"));
		return true;
	}

	public override void OnKill()
	{
		if (Main.expertMode)
		{
			((ModNPC)this).NPC.DropBossBags();
		}
		else
		{
			int num = Main.rand.Next(4);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("EtherealSword").Type, 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("EtherealBow").Type, 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("EtherealTome").Type, 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("EtherealSummon").Type, 1, false, 0, false, false);
			}
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("XenanisFlesh").Type, Main.rand.Next(10, 18), false, 0, false, false);
			if (Main.rand.Next(20) == 0)
			{
				Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("XenanisWings").Type, 1, false, 0, false, false);
			}
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("EtherealDidgeridoo").Type, 1, false, 0, false, false);
			}
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("XenanisMask").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("XenanisTrophyItem").Type, 1, false, 0, false, false);
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
