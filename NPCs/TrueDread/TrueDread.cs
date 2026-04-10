using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.TrueDread;

[AutoloadBossHead]
public class TrueDread : ModNPC
{
	private Player player;

	private float speed;

	private int timer;

	private int AttackType;

	private int AttackType2;

	private bool Circling;

	private int CircleTimer;

	private int CircleShootTimer;

	private float rotate;

	private float SpinX;

	private float SpinY;

	private int Spin;

	public int players;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Absolute Dread");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 6;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).npc.type] = 6;
		NPCID.Sets.TrailingMode[((ModNPC)this).npc.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 262;
		((ModNPC)this).npc.height = 262;
		((ModNPC)this).npc.scale = 1.2f;
		((ModNPC)this).npc.lifeMax = 320000;
		((ModNPC)this).npc.damage = 80;
		((ModNPC)this).npc.defense = 100;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.boss = true;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.netAlways = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit7;
		((ModNPC)this).npc.DeathSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/DreadRoar")?.WithVolume(1f)?.WithPitchVariance(0.5f);
		((ModNPC)this).npc.value = Item.buyPrice(0, 50);
		((ModNPC)this).npc.npcSlots = 1f;
		base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/RealDread");
		base.bossBag = ((ModNPC)this).mod.ItemType("TrueDreadBag");
		((ModNPC)this).npc.aiStyle = -1;
		players = 1;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		players = numPlayers;
		((ModNPC)this).npc.lifeMax = 400000 + numPlayers * 400000;
		((ModNPC)this).npc.damage = 85;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
	{
		if (((ModNPC)this).npc.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/TrueDread/TrueDreadTrail").Width * 0.5f, (float)((ModNPC)this).npc.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).npc.oldPos.Length; i++)
			{
				Vector2 position = ((ModNPC)this).npc.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).npc.gfxOffY);
				Color color = ((ModNPC)this).npc.GetAlpha(drawColor) * ((float)(((ModNPC)this).npc.oldPos.Length - i) / (float)((ModNPC)this).npc.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/TrueDread/TrueDreadTrail"), position, ((ModNPC)this).npc.frame, color, ((ModNPC)this).npc.rotation, vector, ((ModNPC)this).npc.scale, SpriteEffects.None, 0f);
			}
		}
		return true;
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
	{
		if (projectile.type == 634 || projectile.type == 617 || projectile.type == 620 || projectile.type == 632 || projectile.type == 631 || projectile.type == 639 || projectile.type == 616 || projectile.type == 502 || projectile.type == 503 || projectile.type == 636)
		{
			damage /= 2;
		}
	}

	public override void AI()
	{
		player = Main.player[((ModNPC)this).npc.target];
		Vector2 vector = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
		vector.Normalize();
		if (!((Entity)player).active || player.dead || Main.dayTime)
		{
			((ModNPC)this).npc.TargetClosest(faceTarget: false);
			player = Main.player[((ModNPC)this).npc.target];
			if (!((Entity)player).active || player.dead || Main.dayTime)
			{
				((ModNPC)this).npc.velocity = new Vector2(0f, -10f);
				if (((ModNPC)this).npc.timeLeft > 120)
				{
					((ModNPC)this).npc.timeLeft = 120;
				}
				return;
			}
		}
		float num = (float)Math.Atan2(vector.Y, vector.X);
		Vector2 vector2 = new Vector2(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y);
		float num2 = Main.player[((ModNPC)this).npc.target].Center.X - vector2.X;
		float num3 = Main.player[((ModNPC)this).npc.target].Center.Y - vector2.Y;
		((ModNPC)this).npc.rotation = (float)Math.Atan2(num3, num2) + 4.71f;
		int num4 = (Main.expertMode ? 45 : 65);
		if (!Circling)
		{
			timer++;
			if (timer == 1)
			{
				Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/DreadRoar")?.WithVolume(1f), -1, -1);
			}
			if (timer < 180)
			{
				((ModNPC)this).npc.velocity *= 0f;
			}
			if ((timer >= 180 && timer <= 460) || (timer >= 1400 && timer <= 1460))
			{
				((ModNPC)this).npc.velocity *= 0.988f;
			}
			if (timer == 180 || timer == 240 || timer == 300 || timer == 360 || timer == 420 || timer == 1400)
			{
				Vector2 vector3 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector3.Normalize();
				vector3.X *= 22f;
				vector3.Y *= 22f;
				((ModNPC)this).npc.velocity.X = vector3.X;
				((ModNPC)this).npc.velocity.Y = vector3.Y;
				int num5 = 60;
				for (int i = 0; i < num5; i++)
				{
					Vector2 vector4 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(i - (num5 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num5) + ((ModNPC)this).npc.Center;
					Vector2 vector5 = vector4 - ((ModNPC)this).npc.Center;
					Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, 90, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
					obj.noGravity = true;
					obj.noLight = false;
					obj.velocity = Vector2.Normalize(vector5) * 3f;
					obj.fadeIn = 1.3f;
				}
			}
			if (timer == 180 || timer == 200 || timer == 220 || timer == 240 || timer == 260 || timer == 280 || timer == 300 || timer == 320 || timer == 340 || timer == 360 || timer == 380 || timer == 400 || timer == 420 || timer == 440 || timer == 460)
			{
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("DreadScythe"), num4, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer > 480 && timer < 820)
			{
				Move(new Vector2(0f, -360f));
				Vector2 vector6 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector6.Normalize();
				vector6.X *= 4f;
				vector6.Y *= 4f;
				int num6 = 1;
				for (int j = 0; j < num6; j++)
				{
					float num7 = (float)Main.rand.Next(-100, 100) * 0.01f;
					float num8 = (float)Main.rand.Next(-100, 100) * 0.01f;
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector6.X + num7, vector6.Y + num8, ((ModNPC)this).mod.ProjectileType("DreadFlames"), num4, 1f, Main.myPlayer, 0f, 0f);
					if (Main.rand.Next(3) == 0)
					{
						Main.PlaySound(SoundID.DD2_BetsyFlameBreath, ((ModNPC)this).npc.position);
					}
				}
			}
			if (timer > 880 && timer < 1060)
			{
				Move(new Vector2(0f, 0f));
			}
			if (timer == 940 || timer == 1000 || timer == 1060)
			{
				int num9 = 7;
				for (int k = 0; k < num9; k++)
				{
					float num10 = (float)Main.rand.Next(-200, 200) * 0.03f;
					float num11 = (float)Main.rand.Next(-200, 200) * 0.03f;
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, num10, num11, ((ModNPC)this).mod.ProjectileType("DreadSpit"), num4, 1f, ((ModNPC)this).npc.target, 0f, 0f);
				}
			}
			if (AttackType == 0 && ((timer >= 1120 && timer <= 1180) || (timer >= 1220 && timer <= 1280) || (timer >= 1320 && timer <= 1380)))
			{
				((ModNPC)this).npc.velocity *= 0f;
				float num12 = 13.5f;
				int num13 = ((ModNPC)this).mod.ProjectileType("DreadBolt");
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				num = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num) * (double)num12 * -1.0), (float)(Math.Sin(num) * (double)num12 * -1.0), num13, num4, 0f, Main.myPlayer, 0f, 0f);
			}
			if (AttackType == 1)
			{
				if (timer >= 1120 && timer <= 1300)
				{
					((ModNPC)this).npc.velocity *= 0f;
				}
				if (timer == 1120 || timer == 1150 || timer == 1180 || timer == 1210 || timer == 1240 || timer == 1270)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/DreadRay")?.WithVolume(70f), -1, -1);
					float num14 = 10f;
					float num15 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					_ = Main.projectile[Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num15) * (double)num14 * -1.0), (float)(Math.Sin(num15) * (double)num14 * -1.0), ((ModNPC)this).mod.ProjectileType("DreadRay"), num4 + 10, 0f, 0, 0f, 0f)];
				}
				if (timer == 1360)
				{
					timer = 1500;
				}
			}
			if (AttackType == 2)
			{
				if (timer >= 1120 && timer <= 1500)
				{
					((ModNPC)this).npc.velocity *= 0f;
				}
				if (timer == 1130)
				{
					float num16 = 5f;
					int num17 = ((ModNPC)this).mod.ProjectileType("GiantDreadOrb");
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					num = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num) * (double)num16 * -1.0), (float)(Math.Sin(num) * (double)num16 * -1.0), num17, num4, 0f, Main.myPlayer, 0f, 0f);
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/DreadRoar")?.WithVolume(1f), -1, -1);
				}
			}
			if ((timer > 1500 && timer < 1860) || (timer > 2190 && timer < 2370))
			{
				Move(new Vector2(0f, 0f));
			}
			if (timer == 1530 || timer == 1560 || timer == 1590 || timer == 1650 || timer == 1680 || timer == 1710 || timer == 1770 || timer == 1800 || timer == 1830)
			{
				float num18 = 8.5f;
				int num19 = ((ModNPC)this).mod.ProjectileType("BigToothBall");
				Main.PlaySound(SoundID.NPCDeath13, ((ModNPC)this).npc.position);
				num = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num) * (double)num18 * -1.0), (float)(Math.Sin(num) * (double)num18 * -1.0), num19, num4, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer > 1860 && timer < 1960)
			{
				Move(new Vector2(700f, 0f));
				if (timer == 1890 || timer == 1900 || timer == 1910 || timer == 1920 || timer == 1930 || timer == 1940 || timer == 1950)
				{
					float num20 = 15f;
					int num21 = ((ModNPC)this).mod.ProjectileType("TrueDreadOrbiterBolt");
					Main.PlaySound(SoundID.NPCDeath13, ((ModNPC)this).npc.position);
					num = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num) * (double)num20 * -1.0), (float)(Math.Sin(num) * (double)num20 * -1.0), num21, num4, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (timer > 2025 && timer < 2125)
			{
				Move(new Vector2(-700f, 0f));
				if (timer == 2055 || timer == 2065 || timer == 2075 || timer == 2085 || timer == 2095 || timer == 2105 || timer == 2115)
				{
					float num22 = 15f;
					int num23 = ((ModNPC)this).mod.ProjectileType("TrueDreadOrbiterBolt");
					Main.PlaySound(SoundID.NPCDeath13, ((ModNPC)this).npc.position);
					num = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num) * (double)num22 * -1.0), (float)(Math.Sin(num) * (double)num22 * -1.0), num23, num4, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (timer == 1960 || timer == 2125)
			{
				Vector2 vector7 = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
				num = (float)Math.Atan2(vector7.Y - (Main.player[((ModNPC)this).npc.target].position.Y + (float)Main.player[((ModNPC)this).npc.target].height * 0.5f), vector7.X - (Main.player[((ModNPC)this).npc.target].position.X + (float)Main.player[((ModNPC)this).npc.target].width * 0.5f));
				((ModNPC)this).npc.velocity.X = (float)(Math.Cos(num) * 25.0) * -1f;
				((ModNPC)this).npc.velocity.Y = (float)(Math.Sin(num) * 25.0) * -1f;
				int num24 = 60;
				for (int l = 0; l < num24; l++)
				{
					Vector2 vector8 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(l - (num24 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num24) + ((ModNPC)this).npc.Center;
					Vector2 vector9 = vector8 - ((ModNPC)this).npc.Center;
					Dust obj2 = Main.dust[Dust.NewDust(vector8 + vector9, 0, 0, 90, vector9.X * 2f, vector9.Y * 2f, 100, default(Color), 1.4f)];
					obj2.noGravity = true;
					obj2.noLight = false;
					obj2.velocity = Vector2.Normalize(vector9) * 10f;
					obj2.fadeIn = 1.3f;
				}
			}
			if (timer == 2190 || timer == 2250 || timer == 2310)
			{
				int num25 = 7;
				for (int m = 0; m < num25; m++)
				{
					int num26 = 360 / num25;
					NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y, ((ModNPC)this).mod.NPCType("TrueDreadOrbiter"), ((ModNPC)this).npc.whoAmI, (float)(m * num26), (float)((ModNPC)this).npc.whoAmI, 0f, 0f, 255);
				}
			}
			if (timer == 2370 && ((ModNPC)this).npc.life >= ((ModNPC)this).npc.lifeMax / 2)
			{
				timer = 120;
				if (AttackType < 3)
				{
					AttackType++;
				}
				if (AttackType >= 3)
				{
					AttackType = 0;
				}
			}
			if (AttackType2 == 0)
			{
				if (timer >= 2430 && timer < 2830)
				{
					Move(new Vector2(0f, -420f));
				}
				if (timer == 2490 || timer == 2550 || timer == 2610 || timer == 2670 || timer == 2730)
				{
					Vector2 spinningpoint = new Vector2(10f, 0f).RotatedByRandom(Math.PI * 2.0);
					for (int n = 0; n < 10; n++)
					{
						Vector2 vector10 = spinningpoint.RotatedBy(Math.PI / 5.0 * ((double)n + Main.rand.NextDouble() - 0.5));
						Projectile.NewProjectile(((ModNPC)this).npc.Center, vector10, ((ModNPC)this).mod.ProjectileType("DreadScythe"), num4, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (timer >= 2830)
				{
					timer = 120;
					Circling = true;
					if (AttackType < 3)
					{
						AttackType++;
					}
					if (AttackType >= 3)
					{
						AttackType = 0;
					}
					AttackType2 = 1;
				}
			}
			if (AttackType2 == 1)
			{
				if (timer > 2430)
				{
					((ModNPC)this).npc.velocity *= 0f;
				}
				if (timer == 2490 || timer == 2550 || timer == 2610)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/DreadRay")?.WithVolume(70f), -1, -1);
					float num27 = 2f;
					float num28 = 0f;
					if (timer == 2490)
					{
						num28 = 9f;
					}
					if (timer == 2550)
					{
						num28 = 15f;
					}
					if (timer == 2610)
					{
						num28 = 23f;
					}
					float num29 = MathHelper.ToRadians(360f);
					int num30 = -1;
					for (int num31 = 0; (float)num31 < num28; num31++)
					{
						Vector2 vector11 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num29, num29, (float)num31 / num28)) * num27;
						Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector11.X, vector11.Y, ((ModNPC)this).mod.ProjectileType("DreadRay"), num4, 2f, Main.myPlayer, (float)num30, 0f);
					}
				}
				if (timer >= 2670)
				{
					timer = 120;
					Circling = true;
					if (AttackType < 3)
					{
						AttackType++;
					}
					if (AttackType >= 3)
					{
						AttackType = 0;
					}
					AttackType2 = 0;
				}
			}
		}
		if (Circling)
		{
			CircleTimer++;
			if (CircleTimer < 240)
			{
				((ModNPC)this).npc.velocity = new Vector2(((ModNPC)this).npc.velocity.X, ((ModNPC)this).npc.velocity.Y).RotatedBy(MathHelper.ToRadians(Spin - 30));
				((ModNPC)this).npc.TargetClosest(faceTarget: false);
				rotate -= 2f;
				Vector2 vector12 = new Vector2(1000f, 0f).RotatedBy(MathHelper.ToRadians(rotate * 1.57f));
				SpinX = player.Center.X + vector12.X - ((ModNPC)this).npc.Center.X;
				SpinY = player.Center.Y + vector12.Y - ((ModNPC)this).npc.Center.Y;
				float num32 = (float)Math.Sqrt(SpinX * SpinX + SpinY * SpinY);
				if (num32 > 48f)
				{
					num32 = 6.8f / num32;
					SpinX *= num32 * 6f;
					SpinY *= num32 * 6f;
					((ModNPC)this).npc.velocity.X = SpinX;
					((ModNPC)this).npc.velocity.Y = SpinY;
				}
				else
				{
					((ModNPC)this).npc.position.X = player.Center.X + vector12.X - (float)(((ModNPC)this).npc.height / 2);
					((ModNPC)this).npc.position.Y = player.Center.Y + vector12.Y - (float)(((ModNPC)this).npc.width / 2);
					num32 = 6.8f / num32;
					SpinX *= num32 * 6f;
					SpinY *= num32 * 6f;
					((ModNPC)this).npc.velocity.X = 0f;
					((ModNPC)this).npc.velocity.Y = 0f;
				}
				CircleShootTimer++;
				if (CircleShootTimer == 40)
				{
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("DreadScythe"), num4, 1f, Main.myPlayer, 0f, 0f);
					CircleShootTimer = 0;
				}
			}
			if (CircleTimer == 320)
			{
				CircleTimer = 0;
				Circling = false;
			}
		}
		if (CircleTimer > 240 && CircleTimer < 260)
		{
			((ModNPC)this).npc.velocity *= 0f;
		}
		if (CircleTimer == 260)
		{
			Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/DreadRoar")?.WithVolume(1f), -1, -1);
			Vector2 vector13 = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
			num = (float)Math.Atan2(vector13.Y - (Main.player[((ModNPC)this).npc.target].position.Y + (float)Main.player[((ModNPC)this).npc.target].height * 0.5f), vector13.X - (Main.player[((ModNPC)this).npc.target].position.X + (float)Main.player[((ModNPC)this).npc.target].width * 0.5f));
			((ModNPC)this).npc.velocity.X = (float)(Math.Cos(num) * 30.0) * -1f;
			((ModNPC)this).npc.velocity.Y = (float)(Math.Sin(num) * 30.0) * -1f;
		}
	}

	private void Move(Vector2 offset)
	{
		if ((timer > 1860 && timer < 1960) || (timer > 2025 && timer < 2125))
		{
			speed = 30f;
		}
		else if ((timer > 480 && timer < 780) || (timer >= 2430 && timer < 2830))
		{
			speed = 14f;
		}
		else
		{
			speed = 10f;
		}
		Vector2 vector = player.Center + offset - ((ModNPC)this).npc.Center;
		float num = Magnitude(vector);
		if (num > speed)
		{
			vector *= speed / num;
		}
		float num2 = 10f;
		vector = (((ModNPC)this).npc.velocity * num2 + vector) / (num2 + 1f);
		num = Magnitude(vector);
		if (num > speed)
		{
			vector *= speed / num;
		}
		((ModNPC)this).npc.velocity = vector;
	}

	private float Magnitude(Vector2 mag)
	{
		return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
	}

	public override void NPCLoot()
	{
		if (Main.bloodMoon)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("ExistentialDread"), 1, false, 0, false, false);
			if (!UltraniumWorld.ExistentialDread)
			{
				UltraniumWorld.ExistentialDread = true;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(7);
				}
			}
		}
		if (Main.expertMode)
		{
			((ModNPC)this).npc.DropBossBags();
		}
		else
		{
			int num = Main.rand.Next(7);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadSpear"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadYoyo"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadDisc"), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadFlameBlaster"), 1, false, 0, false, false);
			}
			if (num == 4)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("FearStaff"), 1, false, 0, false, false);
			}
			if (num == 5)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadTome"), 1, false, 0, false, false);
			}
			if (num == 6)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("DreadScepter"), 1, false, 0, false, false);
			}
			if (Main.rand.Next(8) == 0)
			{
				Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("DreadBreadItem"), 1, false, 0, false, false);
			}
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("NightmareFuel"), Main.rand.Next(20, 35), false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("DreadMask"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("DreadTrophyItem"), 1, false, 0, false, false);
		}
		if (!UltraniumWorld.downedTrueDread)
		{
			UltraniumWorld.downedTrueDread = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
	}

	public override bool CheckDead()
	{
		for (int i = 0; i < 30; i++)
		{
			int num = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 90, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = false;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModNPC)this).npc.Center)
			{
				Main.dust[num].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num].position) * 3f;
			}
		}
		for (int j = 0; j < 60; j++)
		{
			int num2 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 90, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num2].noGravity = false;
			Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num2].position != ((ModNPC)this).npc.Center)
			{
				Main.dust[num2].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num2].position) * 6f;
			}
		}
		for (int k = 0; k < 80; k++)
		{
			int num3 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 90, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num3].noGravity = false;
			Main.dust[num3].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num3].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num3].position != ((ModNPC)this).npc.Center)
			{
				Main.dust[num3].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num3].position) * 9f;
			}
		}
		for (int l = 0; l < 120; l++)
		{
			int num4 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 90, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num4].noGravity = false;
			Main.dust[num4].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num4].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num4].position != ((ModNPC)this).npc.Center)
			{
				Main.dust[num4].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num4].position) * 12f;
			}
		}
		for (int m = 0; m < 150; m++)
		{
			int num5 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, 90, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num5].noGravity = false;
			Main.dust[num5].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num5].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num5].position != ((ModNPC)this).npc.Center)
			{
				Main.dust[num5].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num5].position) * 15f;
			}
		}
		return true;
	}

	public override void BossLoot(ref string name, ref int potionType)
	{
		potionType = 3544;
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
}
