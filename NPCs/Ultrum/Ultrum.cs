using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
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
		// ((ModNPC)this).DisplayName.SetDefault("Ultrum");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 6;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).NPC.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.scale = 1.4f;
		((ModNPC)this).NPC.width = 128;
		((ModNPC)this).NPC.height = 212;
		((ModNPC)this).NPC.HitSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GodHit");
		((ModNPC)this).NPC.DeathSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GodDeath")?.WithVolume(0.7f)?.WithPitchVariance(0.5f);
		((ModNPC)this).NPC.damage = 50;
		((ModNPC)this).NPC.defense = 50;
		((ModNPC)this).NPC.lifeMax = 220000;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.boss = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.alpha = 255;
		base.bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ((ModNPC)this).Mod.Find<ModItem>("UltrumBag").Type;
		((ModNPC)this).NPC.aiStyle = -1;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 35);
		players = 1;
		if (!Phase2)
		{
			base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/GuardiansPhase1");
		}
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		((ModNPC)this).NPC.lifeMax = 380000 + numPlayers * 38000;
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (projectile.type == 632)
		{
			damage /= 3;
		}
	}

	public override bool PreAI()
	{
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).NPC.target];
		_ = Main.expertMode;
		((ModNPC)this).NPC.TargetClosest();
		int num = (Main.expertMode ? 35 : 55);
		if (Main.player[((ModNPC)this).NPC.target].dead || Main.player[((ModNPC)this).NPC.target].dead || (double)player.Center.Y > Main.worldSurface * 16.0 || player.ZoneUnderworldHeight)
		{
			((ModNPC)this).NPC.velocity.Y = -45f;
			((ModNPC)this).NPC.ai[3] += 1f;
			if (((ModNPC)this).NPC.ai[3] >= 100f)
			{
				((Entity)((ModNPC)this).NPC).active = false;
			}
		}
		if (((ModNPC)this).NPC.life > ((ModNPC)this).NPC.lifeMax / 2)
		{
			(Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center).Normalize();
			((ModNPC)this).NPC.velocity *= 0.985f;
			Timer++;
			if (Timer < 100)
			{
				((ModNPC)this).NPC.velocity *= 0f;
				((ModNPC)this).NPC.alpha -= 5;
			}
			if (Timer == 100)
			{
				for (int i = 0; i < 60; i++)
				{
					int num2 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("UltraniumDust").Type, 0f, -2f, 0, default(Color), 1.5f);
					Main.dust[num2].noGravity = true;
					Main.dust[num2].scale = 2f;
					Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
					if (Main.dust[num2].position != ((ModNPC)this).NPC.Center)
					{
						Main.dust[num2].velocity = ((ModNPC)this).NPC.DirectionTo(Main.dust[num2].position) * 10f;
					}
				}
			}
			if (Timer == 150)
			{
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("ShockWave").Type, 0, 0f, 255, 0f, 0f);
				SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianGrowl")?.WithVolume(10f), -1, -1);
			}
			if (Timer <= 800 && Timer > 150)
			{
				if (Timer == 300 || Timer == 400 || Timer == 500 || Timer == 600 || Timer == 700)
				{
					for (int j = -2; j <= 2; j++)
					{
						Projectile.NewProjectile(((ModNPC)this).NPC.Center, 17f * ((ModNPC)this).NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)j), ((ModNPC)this).Mod.Find<ModProjectile>("UltraBeam").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				float num3 = 0.13f;
				Vector2 vector = new Vector2(((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width * 0.5f, ((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height * 0.5f);
				float num4 = Main.player[((ModNPC)this).NPC.target].position.X + (float)(Main.player[((ModNPC)this).NPC.target].width / 2) - vector.X;
				float num5 = Main.player[((ModNPC)this).NPC.target].position.Y + (float)(Main.player[((ModNPC)this).NPC.target].height / 2) - 120f - vector.Y;
				float num6 = (float)Math.Sqrt(num4 * num4 + num5 * num5);
				float num7 = 13f / num6;
				num4 *= num7;
				num5 *= num7;
				if (((ModNPC)this).NPC.velocity.X < num4)
				{
					((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + num3;
					if (((ModNPC)this).NPC.velocity.X < 0f && num4 > 0f)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + num3;
					}
				}
				else if (((ModNPC)this).NPC.velocity.X > num4)
				{
					((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - num3;
					if (((ModNPC)this).NPC.velocity.X > 0f && num4 < 0f)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - num3;
					}
				}
				if (((ModNPC)this).NPC.velocity.Y < num5)
				{
					((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + num3;
					if (((ModNPC)this).NPC.velocity.Y < 0f && num5 > 0f)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + num3;
					}
				}
				else if (((ModNPC)this).NPC.velocity.Y > num5)
				{
					((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - num3;
					if (((ModNPC)this).NPC.velocity.Y > 0f && num5 < 0f)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - num3;
					}
				}
			}
			if (AttackType == 0)
			{
				if (Timer >= 800)
				{
					((ModNPC)this).NPC.velocity *= 0f;
				}
				if (Timer == 860)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num8 = 10f;
					float num9 = 19f;
					float num10 = MathHelper.ToRadians(360f);
					int num11 = 1;
					for (int k = 0; (float)k < num9; k++)
					{
						Vector2 vector2 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num10, num10, (float)k / (num9 - 1f))) * num8;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector2.X, vector2.Y, ((ModNPC)this).Mod.Find<ModProjectile>("UltraniumBlastSpiral").Type, num, 2f, Main.myPlayer, (float)num11, 0f);
					}
				}
				if (Timer == 920)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num12 = 10f;
					float num13 = 19f;
					float num14 = MathHelper.ToRadians(360f);
					int num15 = -1;
					for (int l = 0; (float)l < num13; l++)
					{
						Vector2 vector3 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num14, num14, (float)l / (num13 - 1f))) * num12;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector3.X, vector3.Y, ((ModNPC)this).Mod.Find<ModProjectile>("UltraniumBlastSpiral").Type, num, 2f, Main.myPlayer, (float)num15, 0f);
					}
				}
			}
			if (AttackType == 1)
			{
				if (Timer >= 800)
				{
					((ModNPC)this).NPC.velocity *= 0f;
				}
				if (Timer == 840 || Timer == 880 || Timer == 920)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(25f), -1, -1);
					float num16 = 12.5f;
					int num17 = ((ModNPC)this).Mod.Find<ModProjectile>("NatureTornado").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num18 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num18) * (double)num16 * -1.0), (float)(Math.Sin(num18) * (double)num16 * -1.0), num17, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 2)
			{
				if (Timer >= 800 && Timer <= 860)
				{
					((ModNPC)this).NPC.velocity *= 0f;
				}
				if (Timer == 860 || Timer == 905)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianGrowl")?.WithVolume(10f), -1, -1);
					Vector2 vector4 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
					vector4.Normalize();
					vector4.X *= 25f;
					vector4.Y *= 25f;
					((ModNPC)this).NPC.velocity.X = vector4.X;
					((ModNPC)this).NPC.velocity.Y = vector4.Y;
					Vector2 vector5 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
					vector5.Normalize();
					vector5.X *= 25f;
					vector5.Y *= 25f;
				}
				if (Timer == 860 || Timer == 870 || Timer == 880 || Timer == 890 || Timer == 900 || Timer == 910 || Timer == 920 || Timer == 930)
				{
					float num19 = 0f;
					int num20 = ((ModNPC)this).Mod.Find<ModProjectile>("UltrumEnergyBolt").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num21 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num21) * (double)num19 * -1.0), (float)(Math.Sin(num21) * (double)num19 * -1.0), num20, 30, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 3)
			{
				if (Timer >= 800)
				{
					((ModNPC)this).NPC.velocity *= 0f;
				}
				if (Timer == 860 || Timer == 890 || Timer == 920 || Timer == 950)
				{
					Vector2 vector6 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
					vector6.Normalize();
					vector6.X *= 8.5f;
					vector6.Y *= 8.5f;
					int num22 = Main.rand.Next(3, 5);
					for (int m = 0; m < num22; m++)
					{
						float num23 = (float)Main.rand.Next(-300, 300) * 0.01f;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector6.X + num23, vector6.Y + num23, ((ModNPC)this).Mod.Find<ModProjectile>("UltrumEnergyBolt").Type, num, 1f, Main.myPlayer, 0f, 0f);
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
			if (((ModNPC)this).NPC.life < ((ModNPC)this).NPC.lifeMax / 2 && !Phase2)
			{
				Phase2 = true;
				Transition = true;
				base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/GuardiansPhase2");
			}
			if (Transition)
			{
				((ModNPC)this).NPC.velocity *= 0f;
				((ModNPC)this).NPC.immortal = true;
				((ModNPC)this).NPC.dontTakeDamage = true;
				TransitionTimer++;
				if (TransitionTimer == 120)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianUNUN")?.WithVolume(50f), -1, -1);
					for (int n = 0; n < 60; n++)
					{
						int num24 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("UltraniumDust").Type, 0f, -2f, 0, default(Color), 1.5f);
						Main.dust[num24].noGravity = true;
						Main.dust[num24].scale = 2f;
						Main.dust[num24].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						Main.dust[num24].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						if (Main.dust[num24].position != ((ModNPC)this).NPC.Center)
						{
							Main.dust[num24].velocity = ((ModNPC)this).NPC.DirectionTo(Main.dust[num24].position) * 10f;
						}
					}
				}
				if (TransitionTimer == 180)
				{
					Transition = false;
					TransitionTimer = 0;
				}
			}
			if (((ModNPC)this).NPC.life < ((ModNPC)this).NPC.lifeMax / 2 && ((ModNPC)this).NPC.life >= ((ModNPC)this).NPC.lifeMax / 8 && !Transition)
			{
				((ModNPC)this).NPC.immortal = false;
				((ModNPC)this).NPC.dontTakeDamage = false;
				Vector2 vector7 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
				vector7.Normalize();
				((ModNPC)this).NPC.TargetClosest();
				((ModNPC)this).NPC.velocity *= 0.986f;
				Timer2++;
				if (Timer2 == 100 || Timer2 == 160 || Timer2 == 220 || Timer2 == 280 || Timer2 == 340 || Timer2 == 400 || Timer2 == 460 || Timer2 == 520)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianGrowl")?.WithVolume(10f), -1, -1);
					int num25 = 36;
					for (int num26 = 0; num26 < num25; num26++)
					{
						Vector2 vector8 = (Vector2.One * new Vector2((float)((ModNPC)this).NPC.width / 7f, (float)((ModNPC)this).NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(num26 - (num25 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num25) + ((ModNPC)this).NPC.Center;
						Vector2 vector9 = vector8 - ((ModNPC)this).NPC.Center;
						Dust obj = Main.dust[Dust.NewDust(vector8 + vector9, 0, 0, ((ModNPC)this).Mod.Find<ModDust>("UltraniumDust").Type, vector9.X * 2f, vector9.Y * 2f, 100, default(Color), 1.4f)];
						obj.noGravity = true;
						obj.noLight = false;
						obj.velocity = Vector2.Normalize(vector9) * 3f;
						obj.fadeIn = 1.3f;
					}
					vector7.X *= 22f;
					vector7.Y *= 22f;
					((ModNPC)this).NPC.velocity.X = vector7.X;
					((ModNPC)this).NPC.velocity.Y = vector7.Y;
					Vector2 vector10 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
					vector10.Normalize();
					vector10.X *= 15f;
					vector10.Y *= 15f;
				}
				if (Timer2 >= 540 && Timer2 <= 700)
				{
					((ModNPC)this).NPC.velocity.X = 0f;
					((ModNPC)this).NPC.velocity.Y = 0f;
				}
				if (Timer2 == 590 || Timer2 == 620 || Timer2 == 650 || Timer2 == 680)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					Vector2 spinningpoint = new Vector2(6f, 0f).RotatedByRandom(Math.PI * 2.0);
					for (int num27 = 0; num27 < 12; num27++)
					{
						Vector2 vector11 = spinningpoint.RotatedBy(0.8975979010256552 * ((double)num27 + Main.rand.NextDouble() - 0.5));
						Projectile.NewProjectile(((ModNPC)this).NPC.Center, vector11, ((ModNPC)this).Mod.Find<ModProjectile>("UltrumBolt").Type, num, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer2 > 760)
				{
					if (Timer2 == 760 || Timer2 == 820 || Timer2 == 880 || Timer2 == 940 || Timer2 == 1000 || Timer2 == 1060 || Timer2 == 1120 || Timer2 == 1180 || Timer2 == 1240)
					{
						for (int num28 = -2; num28 <= 2; num28++)
						{
							Projectile.NewProjectile(((ModNPC)this).NPC.Center, 17f * ((ModNPC)this).NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)num28), ((ModNPC)this).Mod.Find<ModProjectile>("UltraBeam").Type, num, 0f, Main.myPlayer, 0f, 0f);
						}
					}
					float num29 = 0.15f;
					Vector2 vector12 = new Vector2(((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width * 0.5f, ((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height * 0.5f);
					float num30 = Main.player[((ModNPC)this).NPC.target].position.X + (float)(Main.player[((ModNPC)this).NPC.target].width / 2) - vector12.X;
					float num31 = Main.player[((ModNPC)this).NPC.target].position.Y + (float)(Main.player[((ModNPC)this).NPC.target].height / 2) - 120f - vector12.Y;
					float num32 = (float)Math.Sqrt(num30 * num30 + num31 * num31);
					float num33 = 15f / num32;
					num30 *= num33;
					num31 *= num33;
					if (((ModNPC)this).NPC.velocity.X < num30)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + num29;
						if (((ModNPC)this).NPC.velocity.X < 0f && num30 > 0f)
						{
							((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + num29;
						}
					}
					else if (((ModNPC)this).NPC.velocity.X > num30)
					{
						((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - num29;
						if (((ModNPC)this).NPC.velocity.X > 0f && num30 < 0f)
						{
							((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - num29;
						}
					}
					if (((ModNPC)this).NPC.velocity.Y < num31)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + num29;
						if (((ModNPC)this).NPC.velocity.Y < 0f && num31 > 0f)
						{
							((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + num29;
						}
					}
					else if (((ModNPC)this).NPC.velocity.Y > num31)
					{
						((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - num29;
						if (((ModNPC)this).NPC.velocity.Y > 0f && num31 < 0f)
						{
							((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - num29;
						}
					}
				}
				if (Timer2 >= 1300)
				{
					((ModNPC)this).NPC.velocity.X = 0f;
					((ModNPC)this).NPC.velocity.Y = 0f;
				}
				if (Timer2 == 1360 || Timer2 == 1420 || Timer2 == 1480)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianAttack")?.WithVolume(10f), -1, -1);
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num34 = 9f;
					float num35 = 21f;
					float num36 = MathHelper.ToRadians(360f);
					int num37 = -1;
					for (int num38 = 0; (float)num38 < num35; num38++)
					{
						Vector2 vector13 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num36, num36, (float)num38 / (num35 - 1f))) * num34;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector13.X, vector13.Y, ((ModNPC)this).Mod.Find<ModProjectile>("UltraniumBlastSpiral").Type, num, 2f, Main.myPlayer, (float)num37, 0f);
					}
				}
				if (Timer2 == 1390 || Timer2 == 1450)
				{
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num39 = 9f;
					float num40 = 21f;
					float num41 = MathHelper.ToRadians(360f);
					int num42 = 1;
					for (int num43 = 0; (float)num43 < num40; num43++)
					{
						Vector2 vector14 = Vector2.One.RotatedBy(MathHelper.Lerp(0f - num41, num41, (float)num43 / (num40 - 1f))) * num39;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector14.X, vector14.Y, ((ModNPC)this).Mod.Find<ModProjectile>("UltraniumBlastSpiral").Type, num, 2f, Main.myPlayer, (float)num42, 0f);
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
						Main.projectile[Projectile.NewProjectile(vector15.X, vector15.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("UltrumTelegraph").Type, 0, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 125f;
					}
				}
				if (Timer2 > 1700)
				{
					Timer2 = 40;
				}
			}
			if (((ModNPC)this).NPC.life <= ((ModNPC)this).NPC.lifeMax / 8 && !Transition)
			{
				((ModNPC)this).NPC.velocity *= 0f;
				DesperationTimer++;
				if (DesperationTimer < 120)
				{
					((ModNPC)this).NPC.immortal = true;
					((ModNPC)this).NPC.dontTakeDamage = true;
				}
				if (DesperationTimer >= 120)
				{
					((ModNPC)this).NPC.immortal = false;
					((ModNPC)this).NPC.dontTakeDamage = false;
				}
				if (DesperationTimer == 80)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/GuardianUNUN")?.WithVolume(50f), -1, -1);
					for (int num48 = 0; num48 < 60; num48++)
					{
						int num49 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("UltraniumDust").Type, 0f, -2f, 0, default(Color), 1.5f);
						Main.dust[num49].noGravity = true;
						Main.dust[num49].scale = 2f;
						Main.dust[num49].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						Main.dust[num49].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
						if (Main.dust[num49].position != ((ModNPC)this).NPC.Center)
						{
							Main.dust[num49].velocity = ((ModNPC)this).NPC.DirectionTo(Main.dust[num49].position) * 10f;
						}
					}
				}
				if (DesperationTimer == 180)
				{
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					for (int num50 = 0; num50 < 12; num50++)
					{
						Vector2 vector16 = ((float)Math.PI / 6f * (float)num50).ToRotationVector2();
						vector16.Normalize();
						vector16 *= 2f;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector16.X, vector16.Y, ((ModNPC)this).Mod.Find<ModProjectile>("UltrumBeam").Type, num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
				if (DesperationTimer == 200)
				{
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					for (int num51 = 0; num51 < 18; num51++)
					{
						Vector2 vector17 = ((float)Math.PI / 6f * (float)num51).ToRotationVector2();
						vector17.Normalize();
						vector17 *= 2f;
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector17.X, vector17.Y, ((ModNPC)this).Mod.Find<ModProjectile>("UltrumBeam").Type, num, 1f, Main.myPlayer, 0f, 0f);
					}
				}
				if (DesperationTimer >= 240 && DesperationTimer <= 300)
				{
					float num52 = 3.5f;
					float num53 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num53) * (double)num52 * -1.0), (float)(Math.Sin(num53) * (double)num52 * -1.0), ((ModNPC)this).Mod.Find<ModProjectile>("UltrumBeam").Type, num, 0f, 0, 0f, 0f);
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
						_ = Main.projectile[Projectile.NewProjectile(vector18.X, vector18.Y, vector19.X, vector19.Y, ((ModNPC)this).Mod.Find<ModProjectile>("UltrumBeam").Type, num, 6f, 0, 0f, 0f)];
					}
				}
			}
			if (DesperationTimer >= 520)
			{
				DesperationTimer = 120;
				for (int num57 = 0; num57 < 50; num57++)
				{
					int num58 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("UltraniumDust").Type);
					Main.dust[num58].scale = 1.5f;
				}
				((ModNPC)this).NPC.position.X = player.position.X - 100f;
				((ModNPC)this).NPC.position.Y = player.position.Y - 500f;
				for (int num59 = 0; num59 < 50; num59++)
				{
					int num60 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("UltraniumDust").Type);
					Main.dust[num60].scale = 1.5f;
				}
			}
		}
		return false;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (((ModNPC)this).NPC.life > 0)
		{
			return;
		}
		for (int i = 0; i < 40; i++)
		{
			int num = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("UltraniumDust").Type, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = true;
			Main.dust[num].scale = 2f;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != ((ModNPC)this).NPC.Center)
			{
				Main.dust[num].velocity = ((ModNPC)this).NPC.DirectionTo(Main.dust[num].position) * 2f;
			}
		}
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X + (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y + (float)(((ModNPC)this).NPC.height / 2);
		((ModNPC)this).NPC.width = 128;
		((ModNPC)this).NPC.height = 212;
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X - (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y - (float)(((ModNPC)this).NPC.height / 2);
		for (int j = 0; j < 20; j++)
		{
			int num2 = Dust.NewDust(new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y), ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("UltraniumDust").Type, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num2].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num2].scale = 1.5f;
				Main.dust[num2].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override void OnKill()
	{
		if (Main.expertMode)
		{
			((ModNPC)this).NPC.DropBossBags();
		}
		else
		{
			int num = Main.rand.Next(7);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("UltraFlail").Type, 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("UltraniumBow").Type, 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("UltraniumKunai").Type, 1, false, 0, false, false);
			}
			if (num == 3)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("UltraniumStaff").Type, 1, false, 0, false, false);
			}
			if (num == 4)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("UltraniumSword").Type, 1, false, 0, false, false);
			}
			if (num == 5)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("UltraTome").Type, 1, false, 0, false, false);
			}
			if (num == 6)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("UltraniumScepter").Type, 1, false, 0, false, false);
			}
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("UltrumShard").Type, Main.rand.Next(25, 32), false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("UltrumMask").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("UltrumTrophyItem").Type, 1, false, 0, false, false);
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
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter >= 8.0)
		{
			((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
			((ModNPC)this).NPC.frameCounter = 1.0;
		}
	}
}
