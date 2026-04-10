using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ultrum;

[AutoloadBossHead]
public class Ultrum : ModNPC
{
	private bool Phase2;

	private bool Transition;

	private int TransitionTimer;

	public int Timer;

	public int Timer2 = 40;

	public int BulletTimer;

	public int DesperationTimer;

	public int AttackType;

	public int players;

	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Ultrum");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 6;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).npc.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).npc.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.scale = 1.4f;
		((ModNPC)this).npc.width = 128;
		((ModNPC)this).npc.height = 212;
		((ModNPC)this).npc.HitSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GodHit");
		((ModNPC)this).npc.DeathSound = ((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GodDeath")?.WithVolume(0.7f)?.WithPitchVariance(0.5f);
		((ModNPC)this).npc.damage = 50;
		((ModNPC)this).npc.defense = 50;
		((ModNPC)this).npc.lifeMax = 220000;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.boss = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.alpha = 255;
		base.bossBag = ((ModNPC)this).mod.ItemType("UltrumBag");
		((ModNPC)this).npc.aiStyle = -1;
		((ModNPC)this).npc.value = Item.buyPrice(0, 35);
		players = 1;
		if (!Phase2)
		{
			base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/GuardiansPhase1");
		}
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).npc.buffImmune[i] = true;
		}
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		players = numPlayers;
		((ModNPC)this).npc.lifeMax = 380000 + numPlayers * 38000;
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection)
	{
		if (projectile.type == 632)
		{
			damage /= 3;
		}
	}

	public override bool PreAI()
	{
		((ModNPC)this).npc.spriteDirection = ((ModNPC)this).npc.direction;
		((ModNPC)this).npc.rotation = ((ModNPC)this).npc.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).npc.target];
		_ = Main.expertMode;
		((ModNPC)this).npc.TargetClosest();
		int num = (Main.expertMode ? 35 : 55);
		if (Main.player[((ModNPC)this).npc.target].dead || Main.player[((ModNPC)this).npc.target].dead || (double)player.Center.Y > Main.worldSurface * 16.0 || player.ZoneUnderworldHeight)
		{
			((ModNPC)this).npc.velocity.Y = -45f;
			((ModNPC)this).npc.ai[3] += 1f;
			if (((ModNPC)this).npc.ai[3] >= 100f)
			{
				((Entity)((ModNPC)this).npc).active = false;
			}
		}
		if (((ModNPC)this).npc.life > ((ModNPC)this).npc.lifeMax / 2)
		{
			(Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center).Normalize();
			((ModNPC)this).npc.velocity *= 0.985f;
			Timer++;
			if (Timer < 100)
			{
				((ModNPC)this).npc.velocity *= 0f;
				((ModNPC)this).npc.alpha -= 5;
			}
			if (Timer == 100)
			{
				for (int i = 0; i < 60; i++)
				{
					int num2 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("UltraniumDust"), 0f, -2f, 0, default(Color), 1.5f);
					Main.dust[num2].noGravity = true;
					Main.dust[num2].scale = 2f;
					Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					if (Main.dust[num2].position != ((ModNPC)this).npc.Center)
					{
						Main.dust[num2].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num2].position) * 10f;
					}
				}
			}
			if (Timer == 150)
			{
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("ShockWave"), 0, 0f, 255, 0f, 0f);
				Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianGrowl")?.WithVolume(10f), -1, -1);
			}
			if (Timer <= 800 && Timer > 150)
			{
				if (Timer == 300 || Timer == 400 || Timer == 500 || Timer == 600 || Timer == 700)
				{
					for (int j = -2; j <= 2; j++)
					{
						Projectile.NewProjectile(((ModNPC)this).npc.Center, 17f * ((ModNPC)this).npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)j), ((ModNPC)this).mod.ProjectileType("UltraBeam"), num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				float num3 = 0.13f;
				Vector2 vector = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
				float num4 = Main.player[((ModNPC)this).npc.target].position.X + (float)(Main.player[((ModNPC)this).npc.target].width / 2) - vector.X;
				float num5 = Main.player[((ModNPC)this).npc.target].position.Y + (float)(Main.player[((ModNPC)this).npc.target].height / 2) - 120f - vector.Y;
				float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
				float num7 = 13f / num6;
				num4 *= num7;
				num5 *= num7;
				if (((ModNPC)this).npc.velocity.X < num4)
				{
					((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + num3;
					if (((ModNPC)this).npc.velocity.X < 0f && num4 > 0f)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + num3;
					}
				}
				else if (((ModNPC)this).npc.velocity.X > num4)
				{
					((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - num3;
					if (((ModNPC)this).npc.velocity.X > 0f && num4 < 0f)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - num3;
					}
				}
				if (((ModNPC)this).npc.velocity.Y < num5)
				{
					((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + num3;
					if (((ModNPC)this).npc.velocity.Y < 0f && num5 > 0f)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + num3;
					}
				}
				else if (((ModNPC)this).npc.velocity.Y > num5)
				{
					((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - num3;
					if (((ModNPC)this).npc.velocity.Y > 0f && num5 < 0f)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - num3;
					}
				}
			}
			if (AttackType == 0)
			{
				if (Timer >= 800)
				{
					((ModNPC)this).npc.velocity *= 0f;
				}
				if (Timer == 860)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					float num8 = 10f;
					float num9 = 19f;
					float num10 = MathHelper.ToRadians(360f);
					int num11 = 1;
					for (int k = 0; (float)k < num9; k++)
					{
						Vector2 vector2 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num10, num10, (float)k / (num9 - 1f))) * num8;
						Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector2.X, vector2.Y, ((ModNPC)this).mod.ProjectileType("UltraniumBlastSpiral"), num, 2f, Main.myPlayer, (float)num11, 0f);
					}
				}
				if (Timer == 920)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					float num12 = 10f;
					float num13 = 19f;
					float num14 = MathHelper.ToRadians(360f);
					int num15 = -1;
					for (int l = 0; (float)l < num13; l++)
					{
						Vector2 vector3 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num14, num14, (float)l / (num13 - 1f))) * num12;
						Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector3.X, vector3.Y, ((ModNPC)this).mod.ProjectileType("UltraniumBlastSpiral"), num, 2f, Main.myPlayer, (float)num15, 0f);
					}
				}
			}
			if (AttackType == 1)
			{
				if (Timer >= 800)
				{
					((ModNPC)this).npc.velocity *= 0f;
				}
				if (Timer == 840 || Timer == 880 || Timer == 920)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(25f), -1, -1);
					float num16 = 12.5f;
					int num17 = ((ModNPC)this).mod.ProjectileType("NatureTornado");
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					float num18 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num18) * (double)num16 * -1.0), (float)(Math.Sin(num18) * (double)num16 * -1.0), num17, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 2)
			{
				if (Timer >= 800 && Timer <= 860)
				{
					((ModNPC)this).npc.velocity *= 0f;
				}
				if (Timer == 860 || Timer == 905)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianGrowl")?.WithVolume(10f), -1, -1);
					Vector2 vector4 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
					vector4.Normalize();
					vector4.X *= 25f;
					vector4.Y *= 25f;
					((ModNPC)this).npc.velocity.X = vector4.X;
					((ModNPC)this).npc.velocity.Y = vector4.Y;
					Vector2 vector5 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
					vector5.Normalize();
					vector5.X *= 25f;
					vector5.Y *= 25f;
				}
				if (Timer == 860 || Timer == 870 || Timer == 880 || Timer == 890 || Timer == 900 || Timer == 910 || Timer == 920 || Timer == 930)
				{
					float num19 = 0f;
					int num20 = ((ModNPC)this).mod.ProjectileType("UltrumEnergyBolt");
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					float num21 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num21) * (double)num19 * -1.0), (float)(Math.Sin(num21) * (double)num19 * -1.0), num20, 30, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 3)
			{
				if (Timer >= 800)
				{
					((ModNPC)this).npc.velocity *= 0f;
				}
				if (Timer == 860 || Timer == 890 || Timer == 920 || Timer == 950)
				{
					Vector2 vector6 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
					vector6.Normalize();
					vector6.X *= 8.5f;
					vector6.Y *= 8.5f;
					int num22 = Main.rand.Next(3, 5);
					for (int m = 0; m < num22; m++)
					{
						float num23 = (float)Main.rand.Next(-300, 300) * 0.01f;
						Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector6.X + num23, vector6.Y + num23, ((ModNPC)this).mod.ProjectileType("UltrumEnergyBolt"), num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
			}
			if (Timer >= 950)
			{
				Timer = 201;
				if (AttackType < 4)
				{
					AttackType++;
				}
				if (AttackType >= 4)
				{
					AttackType = 0;
				}
			}
		}
		else
		{
			if (((ModNPC)this).npc.life < ((ModNPC)this).npc.lifeMax / 2 && !Phase2)
			{
				Phase2 = true;
				Transition = true;
				base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/GuardiansPhase2");
			}
			if (Transition)
			{
				((ModNPC)this).npc.velocity *= 0f;
				((ModNPC)this).npc.immortal = true;
				((ModNPC)this).npc.dontTakeDamage = true;
				TransitionTimer++;
				if (TransitionTimer == 120)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianUNUN")?.WithVolume(50f), -1, -1);
					for (int n = 0; n < 60; n++)
					{
						int num24 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("UltraniumDust"), 0f, -2f, 0, default(Color), 1.5f);
						Main.dust[num24].noGravity = true;
						Main.dust[num24].scale = 2f;
						Main.dust[num24].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						Main.dust[num24].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						if (Main.dust[num24].position != ((ModNPC)this).npc.Center)
						{
							Main.dust[num24].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num24].position) * 10f;
						}
					}
				}
				if (TransitionTimer == 180)
				{
					Transition = false;
					TransitionTimer = 0;
				}
			}
			if (((ModNPC)this).npc.life < ((ModNPC)this).npc.lifeMax / 2 && ((ModNPC)this).npc.life >= ((ModNPC)this).npc.lifeMax / 8 && !Transition)
			{
				((ModNPC)this).npc.immortal = false;
				((ModNPC)this).npc.dontTakeDamage = false;
				Vector2 vector7 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
				vector7.Normalize();
				((ModNPC)this).npc.TargetClosest();
				((ModNPC)this).npc.velocity *= 0.986f;
				Timer2++;
				if (Timer2 == 100 || Timer2 == 160 || Timer2 == 220 || Timer2 == 280 || Timer2 == 340 || Timer2 == 400 || Timer2 == 460 || Timer2 == 520)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianGrowl")?.WithVolume(10f), -1, -1);
					int num25 = 36;
					for (int num26 = 0; num26 < num25; num26++)
					{
						Vector2 vector8 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(num26 - (num25 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num25) + ((ModNPC)this).npc.Center;
						Vector2 vector9 = vector8 - ((ModNPC)this).npc.Center;
						Dust obj = Main.dust[Dust.NewDust(vector8 + vector9, 0, 0, ((ModNPC)this).mod.DustType("UltraniumDust"), vector9.X * 2f, vector9.Y * 2f, 100, default(Color), 1.4f)];
						obj.noGravity = true;
						obj.noLight = false;
						obj.velocity = Vector2.Normalize(vector9) * 3f;
						obj.fadeIn = 1.3f;
					}
					vector7.X *= 22f;
					vector7.Y *= 22f;
					((ModNPC)this).npc.velocity.X = vector7.X;
					((ModNPC)this).npc.velocity.Y = vector7.Y;
					Vector2 vector10 = Main.player[((ModNPC)this).npc.target].Center - ((ModNPC)this).npc.Center;
					vector10.Normalize();
					vector10.X *= 15f;
					vector10.Y *= 15f;
				}
				if (Timer2 >= 540 && Timer2 <= 700)
				{
					((ModNPC)this).npc.velocity.X = 0f;
					((ModNPC)this).npc.velocity.Y = 0f;
				}
				if (Timer2 == 590 || Timer2 == 620 || Timer2 == 650 || Timer2 == 680)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					Vector2 spinningpoint = new Vector2(6f, 0f).RotatedByRandom(Math.PI * 2.0);
					for (int num27 = 0; num27 < 12; num27++)
					{
						Vector2 vector11 = spinningpoint.RotatedBy(0.8975979010256552 * ((double)num27 + Main.rand.NextDouble() - 0.5));
						Projectile.NewProjectile(((ModNPC)this).npc.Center, vector11, ((ModNPC)this).mod.ProjectileType("UltrumBolt"), num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer2 > 760)
				{
					if (Timer2 == 760 || Timer2 == 820 || Timer2 == 880 || Timer2 == 940 || Timer2 == 1000 || Timer2 == 1060 || Timer2 == 1120 || Timer2 == 1180 || Timer2 == 1240)
					{
						for (int num28 = -2; num28 <= 2; num28++)
						{
							Projectile.NewProjectile(((ModNPC)this).npc.Center, 17f * ((ModNPC)this).npc.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)num28), ((ModNPC)this).mod.ProjectileType("UltraBeam"), num, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					float num29 = 0.15f;
					Vector2 vector12 = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
					float num30 = Main.player[((ModNPC)this).npc.target].position.X + (float)(Main.player[((ModNPC)this).npc.target].width / 2) - vector12.X;
					float num31 = Main.player[((ModNPC)this).npc.target].position.Y + (float)(Main.player[((ModNPC)this).npc.target].height / 2) - 120f - vector12.Y;
					float num32 = (float)Math.Sqrt(num30 * num30 + num31 * num31);
					float num33 = 15f / num32;
					num30 *= num33;
					num31 *= num33;
					if (((ModNPC)this).npc.velocity.X < num30)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + num29;
						if (((ModNPC)this).npc.velocity.X < 0f && num30 > 0f)
						{
							((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + num29;
						}
					}
					else if (((ModNPC)this).npc.velocity.X > num30)
					{
						((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - num29;
						if (((ModNPC)this).npc.velocity.X > 0f && num30 < 0f)
						{
							((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - num29;
						}
					}
					if (((ModNPC)this).npc.velocity.Y < num31)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + num29;
						if (((ModNPC)this).npc.velocity.Y < 0f && num31 > 0f)
						{
							((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + num29;
						}
					}
					else if (((ModNPC)this).npc.velocity.Y > num31)
					{
						((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - num29;
						if (((ModNPC)this).npc.velocity.Y > 0f && num31 < 0f)
						{
							((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - num29;
						}
					}
				}
				if (Timer2 >= 1300)
				{
					((ModNPC)this).npc.velocity.X = 0f;
					((ModNPC)this).npc.velocity.Y = 0f;
				}
				if (Timer2 == 1360 || Timer2 == 1420 || Timer2 == 1480)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					float num34 = 9f;
					float num35 = 21f;
					float num36 = MathHelper.ToRadians(360f);
					int num37 = -1;
					for (int num38 = 0; (float)num38 < num35; num38++)
					{
						Vector2 vector13 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num36, num36, (float)num38 / (num35 - 1f))) * num34;
						Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector13.X, vector13.Y, ((ModNPC)this).mod.ProjectileType("UltraniumBlastSpiral"), num, 2f, Main.myPlayer, (float)num37, 0f);
					}
				}
				if (Timer2 == 1390 || Timer2 == 1450)
				{
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					float num39 = 9f;
					float num40 = 21f;
					float num41 = MathHelper.ToRadians(360f);
					int num42 = 1;
					for (int num43 = 0; (float)num43 < num40; num43++)
					{
						Vector2 vector14 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num41, num41, (float)num43 / (num40 - 1f))) * num39;
						Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector14.X, vector14.Y, ((ModNPC)this).mod.ProjectileType("UltraniumBlastSpiral"), num, 2f, Main.myPlayer, (float)num42, 0f);
					}
				}
				if (Timer2 == 1600 || Timer2 == 1640)
				{
					float num44 = 2200f;
					float num45 = 10f;
					float num46 = num44 / num45;
					for (int num47 = 0; (float)num47 < num45; num47++)
					{
						Vector2 vector15 = new Vector2(player.Center.X - num44 / 2f + num46 * (float)num47, player.Center.Y + 700f);
						Main.projectile[Projectile.NewProjectile(vector15.X, vector15.Y, 0f, 0f, ((ModNPC)this).mod.ProjectileType("UltrumTelegraph"), 0, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 125f;
					}
				}
				if (Timer2 > 1700)
				{
					Timer2 = 40;
				}
			}
			if (((ModNPC)this).npc.life <= ((ModNPC)this).npc.lifeMax / 8 && !Transition)
			{
				((ModNPC)this).npc.velocity *= 0f;
				DesperationTimer++;
				if (DesperationTimer < 120)
				{
					((ModNPC)this).npc.immortal = true;
					((ModNPC)this).npc.dontTakeDamage = true;
				}
				if (DesperationTimer >= 120)
				{
					((ModNPC)this).npc.immortal = false;
					((ModNPC)this).npc.dontTakeDamage = false;
				}
				if (DesperationTimer == 80)
				{
					Main.PlaySound(((ModNPC)this).mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianUNUN")?.WithVolume(50f), -1, -1);
					for (int num48 = 0; num48 < 60; num48++)
					{
						int num49 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("UltraniumDust"), 0f, -2f, 0, default(Color), 1.5f);
						Main.dust[num49].noGravity = true;
						Main.dust[num49].scale = 2f;
						Main.dust[num49].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						Main.dust[num49].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						if (Main.dust[num49].position != ((ModNPC)this).npc.Center)
						{
							Main.dust[num49].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num49].position) * 10f;
						}
					}
				}
				if (DesperationTimer == 180)
				{
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					for (int num50 = 0; num50 < 12; num50++)
					{
						Vector2 vector16 = ((float)Math.PI / 6f * (float)num50).ToRotationVector2();
						vector16.Normalize();
						vector16 *= 2f;
						Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector16.X, vector16.Y, ((ModNPC)this).mod.ProjectileType("UltrumBeam"), num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
				if (DesperationTimer == 200)
				{
					Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
					for (int num51 = 0; num51 < 18; num51++)
					{
						Vector2 vector17 = ((float)Math.PI / 6f * (float)num51).ToRotationVector2();
						vector17.Normalize();
						vector17 *= 2f;
						Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, vector17.X, vector17.Y, ((ModNPC)this).mod.ProjectileType("UltrumBeam"), num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
				if (DesperationTimer >= 240 && DesperationTimer <= 300)
				{
					float num52 = 3.5f;
					float num53 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num53) * (double)num52 * -1.0), (float)(Math.Sin(num53) * (double)num52 * -1.0), ((ModNPC)this).mod.ProjectileType("UltrumBeam"), num, 0f, 0, 0f, 0f);
				}
				if (DesperationTimer == 360 || DesperationTimer == 420 || DesperationTimer == 480)
				{
					int num54 = 0;
					if (DesperationTimer == 360)
					{
						num54 = 8;
					}
					if (DesperationTimer == 420)
					{
						num54 = 12;
					}
					if (DesperationTimer == 480)
					{
						num54 = 16;
					}
					int num55 = 650;
					for (float num56 = 0f; num56 < (float)num54; num56 += 1f)
					{
						Vector2 vector18 = player.Center + new Vector2(0f, num55).RotatedBy((double)num56 * (Math.PI * 2.0 / (double)num54));
						Vector2 vector19 = player.Center - vector18;
						vector19.Normalize();
						vector19 *= 1.2f;
						_ = Main.projectile[Projectile.NewProjectile(vector18.X, vector18.Y, vector19.X, vector19.Y, ((ModNPC)this).mod.ProjectileType("UltrumBeam"), num, 6f, 0, 0f, 0f)];
					}
				}
			}
			if (DesperationTimer >= 520)
			{
				DesperationTimer = 120;
				for (int num57 = 0; num57 < 50; num57++)
				{
					int num58 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("UltraniumDust"));
					Main.dust[num58].scale = 1.5f;
				}
				((ModNPC)this).npc.position.X = player.position.X - 100f;
				((ModNPC)this).npc.position.Y = player.position.Y - 500f;
				for (int num59 = 0; num59 < 50; num59++)
				{
					int num60 = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("UltraniumDust"));
					Main.dust[num60].scale = 1.5f;
				}
			}
		}
		return false;
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (((ModNPC)this).npc.life > 0)
		{
			return;
		}
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModNPC)this).npc.position, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("UltraniumDust"), 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].scale = 2f;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModNPC)this).npc.Center)
			{
				Main.dust[num].velocity = ((ModNPC)this).npc.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		((ModNPC)this).npc.position.X = ((ModNPC)this).npc.position.X + (float)(((ModNPC)this).npc.width / 2);
		((ModNPC)this).npc.position.Y = ((ModNPC)this).npc.position.Y + (float)(((ModNPC)this).npc.height / 2);
		((ModNPC)this).npc.width = 128;
		((ModNPC)this).npc.height = 212;
		((ModNPC)this).npc.position.X = ((ModNPC)this).npc.position.X - (float)(((ModNPC)this).npc.width / 2);
		((ModNPC)this).npc.position.Y = ((ModNPC)this).npc.position.Y - (float)(((ModNPC)this).npc.height / 2);
		for (int j = 0; j < 20; j++)
		{
			int num2 = Dust.NewDust(new Vector2(((ModNPC)this).npc.position.X, ((ModNPC)this).npc.position.Y), ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.DustType("UltraniumDust"), 0f, 0f, 100, default(Color), 2f);
			Main.dust[num2].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num2].scale = 1.5f;
				Main.dust[num2].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override void NPCLoot()
	{
		if (Main.expertMode)
		{
			((ModNPC)this).npc.DropBossBags();
		}
		else
		{
			int num = Main.rand.Next(7);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("UltraFlail"), 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("UltraniumBow"), 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("UltraniumKunai"), 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("UltraniumStaff"), 1, false, 0, false, false);
			}
			if (num == 4)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("UltraniumSword"), 1, false, 0, false, false);
			}
			if (num == 5)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("UltraTome"), 1, false, 0, false, false);
			}
			if (num == 6)
			{
				Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("UltraniumScepter"), 1, false, 0, false, false);
			}
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("UltrumShard"), Main.rand.Next(25, 32), false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("UltrumMask"), 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).npc.getRect(), ((ModNPC)this).mod.ItemType("UltrumTrophyItem"), 1, false, 0, false, false);
		}
		if (!UltraniumWorld.downedUltrum)
		{
			UltraniumWorld.downedUltrum = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
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
