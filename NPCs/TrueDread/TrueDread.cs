using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.BossBags;
using Ultranium.Items.Dread.Materials;
using Ultranium.Items.Dread.TrueDread;
using Ultranium.Items.Ice;

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
		//DisplayName.SetDefault("Absolute Dread");
		Main.npcFrameCount[NPC.type] = 6;
		NPCID.Sets.TrailCacheLength[NPC.type] = 6;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		NPC.width = 262;
		NPC.height = 262;
		NPC.scale = 1.2f;
		NPC.lifeMax = 320000;
		NPC.damage = 80;
		NPC.defense = 100;
		NPC.knockBackResist = 0f;
		NPC.boss = true;
		NPC.lavaImmune = true;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.netAlways = true;
		NPC.HitSound = SoundID.NPCHit7;
		NPC.DeathSound = new SoundStyle("Ultranium/Sounds/DreadRoar") with { PitchVariance = 0.5f };
		NPC.value = Item.buyPrice(0, 50);
		NPC.npcSlots = 1f;
		Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/RealDread");
		NPC.aiStyle = -1;
		players = 1;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance * bossAdjustment);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/TrueDread/TrueDreadTrail").Width() * 0.5f, (float)NPC.height * 0.5f);
			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				Vector2 position = NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/TrueDread/TrueDreadTrail").Value, position, NPC.frame, color, NPC.rotation, vector, NPC.scale, SpriteEffects.None, 0f);
			}
		}
		return true;
	}

	public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
	{
		if (projectile.type == ProjectileID.NebulaBlaze1 || projectile.type == ProjectileID.NebulaArcanum || projectile.type == ProjectileID.NebulaArcanumExplosionShotShard || projectile.type == ProjectileID.LastPrismLaser || projectile.type == ProjectileID.PhantasmArrow || projectile.type == ProjectileID.MoonlordArrow || projectile.type == ProjectileID.VortexBeaterRocket || projectile.type == ProjectileID.Meowmere || projectile.type == ProjectileID.StarWrath || projectile.type == ProjectileID.Daybreak)
		{
			modifiers.SourceDamage /= 2;
		}
	}

	public override void AI()
	{
		player = Main.player[NPC.target];
		Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
		vector.Normalize();
		if (!player.active || player.dead || Main.dayTime)
		{
			NPC.TargetClosest(faceTarget: false);
			player = Main.player[NPC.target];
			if (!player.active || player.dead || Main.dayTime)
			{
				NPC.velocity = new Vector2(0f, -10f);
				if (NPC.timeLeft > 120)
				{
					NPC.timeLeft = 120;
				}
				return;
			}
		}
		float num = (float)Math.Atan2(vector.Y, vector.X);
		Vector2 vector2 = new Vector2(NPC.Center.X, NPC.Center.Y);
		float num2 = Main.player[NPC.target].Center.X - vector2.X;
		float num3 = Main.player[NPC.target].Center.Y - vector2.Y;
		NPC.rotation = (float)Math.Atan2(num3, num2) + 4.71f;
		int num4 = (Main.expertMode ? 45 : 65);
		if (!Circling)
		{
			timer++;
			if (timer == 1)
			{
				SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/DreadRoar"));
			}
			if (timer < 180)
			{
				NPC.velocity *= 0f;
			}
			if ((timer >= 180 && timer <= 460) || (timer >= 1400 && timer <= 1460))
			{
				NPC.velocity *= 0.988f;
			}
			if (timer == 180 || timer == 240 || timer == 300 || timer == 360 || timer == 420 || timer == 1400)
			{
				Vector2 vector3 = Main.player[NPC.target].Center - NPC.Center;
				vector3.Normalize();
				vector3.X *= 22f;
				vector3.Y *= 22f;
				NPC.velocity.X = vector3.X;
				NPC.velocity.Y = vector3.Y;
				int num5 = 60;
				for (int i = 0; i < num5; i++)
				{
					Vector2 vector4 = (Vector2.One * new Vector2((float)NPC.width / 7f, (float)NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(i - (num5 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num5) + NPC.Center;
					Vector2 vector5 = vector4 - NPC.Center;
					Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, DustID.GemRuby, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
					obj.noGravity = true;
					obj.noLight = false;
					obj.velocity = Vector2.Normalize(vector5) * 3f;
					obj.fadeIn = 1.3f;
				}
			}
			if (timer == 180 || timer == 200 || timer == 220 || timer == 240 || timer == 260 || timer == 280 || timer == 300 || timer == 320 || timer == 340 || timer == 360 || timer == 380 || timer == 400 || timer == 420 || timer == 440 || timer == 460)
			{
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("DreadScythe").Type, num4, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer > 480 && timer < 820)
			{
				Move(new Vector2(0f, -360f));
				Vector2 vector6 = Main.player[NPC.target].Center - NPC.Center;
				vector6.Normalize();
				vector6.X *= 4f;
				vector6.Y *= 4f;
				int num6 = 1;
				for (int j = 0; j < num6; j++)
				{
					float num7 = (float)Main.rand.Next(-100, 100) * 0.01f;
					float num8 = (float)Main.rand.Next(-100, 100) * 0.01f;
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector6.X + num7, vector6.Y + num8, Mod.Find<ModProjectile>("DreadFlames").Type, num4, 1f, Main.myPlayer, 0f, 0f);
					if (Main.rand.Next(3) == 0)
					{
						SoundEngine.PlaySound(SoundID.DD2_BetsyFlameBreath, NPC.position);
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
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, num10, num11, Mod.Find<ModProjectile>("DreadSpit").Type, num4, 1f, NPC.target, 0f, 0f);
				}
			}
			if (AttackType == 0 && ((timer >= 1120 && timer <= 1180) || (timer >= 1220 && timer <= 1280) || (timer >= 1320 && timer <= 1380)))
			{
				NPC.velocity *= 0f;
				float num12 = 13.5f;
				int num13 = Mod.Find<ModProjectile>("DreadBolt").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				num = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num) * (double)num12 * -1.0), (float)(Math.Sin(num) * (double)num12 * -1.0), num13, num4, 0f, Main.myPlayer, 0f, 0f);
			}
			if (AttackType == 1)
			{
				if (timer >= 1120 && timer <= 1300)
				{
					NPC.velocity *= 0f;
				}
				if (timer == 1120 || timer == 1150 || timer == 1180 || timer == 1210 || timer == 1240 || timer == 1270)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/DreadRay"));
					float num14 = 10f;
					float num15 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					_ = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num15) * (double)num14 * -1.0), (float)(Math.Sin(num15) * (double)num14 * -1.0), Mod.Find<ModProjectile>("DreadRay").Type, num4 + 10, 0f, 0, 0f, 0f)];
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
					NPC.velocity *= 0f;
				}
				if (timer == 1130)
				{
					float num16 = 5f;
					int num17 = Mod.Find<ModProjectile>("GiantDreadOrb").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					num = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num) * (double)num16 * -1.0), (float)(Math.Sin(num) * (double)num16 * -1.0), num17, num4, 0f, Main.myPlayer, 0f, 0f);
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/DreadRoar"));
				}
			}
			if ((timer > 1500 && timer < 1860) || (timer > 2190 && timer < 2370))
			{
				Move(new Vector2(0f, 0f));
			}
			if (timer == 1530 || timer == 1560 || timer == 1590 || timer == 1650 || timer == 1680 || timer == 1710 || timer == 1770 || timer == 1800 || timer == 1830)
			{
				float num18 = 8.5f;
				int num19 = Mod.Find<ModProjectile>("BigToothBall").Type;
				SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.position);
				num = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num) * (double)num18 * -1.0), (float)(Math.Sin(num) * (double)num18 * -1.0), num19, num4, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer > 1860 && timer < 1960)
			{
				Move(new Vector2(700f, 0f));
				if (timer == 1890 || timer == 1900 || timer == 1910 || timer == 1920 || timer == 1930 || timer == 1940 || timer == 1950)
				{
					float num20 = 15f;
					int num21 = Mod.Find<ModProjectile>("TrueDreadOrbiterBolt").Type;
					SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.position);
					num = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num) * (double)num20 * -1.0), (float)(Math.Sin(num) * (double)num20 * -1.0), num21, num4, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (timer > 2025 && timer < 2125)
			{
				Move(new Vector2(-700f, 0f));
				if (timer == 2055 || timer == 2065 || timer == 2075 || timer == 2085 || timer == 2095 || timer == 2105 || timer == 2115)
				{
					float num22 = 15f;
					int num23 = Mod.Find<ModProjectile>("TrueDreadOrbiterBolt").Type;
					SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.position);
					num = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num) * (double)num22 * -1.0), (float)(Math.Sin(num) * (double)num22 * -1.0), num23, num4, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (timer == 1960 || timer == 2125)
			{
				Vector2 vector7 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				num = (float)Math.Atan2(vector7.Y - (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f), vector7.X - (Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f));
				NPC.velocity.X = (float)(Math.Cos(num) * 25.0) * -1f;
				NPC.velocity.Y = (float)(Math.Sin(num) * 25.0) * -1f;
				int num24 = 60;
				for (int l = 0; l < num24; l++)
				{
					Vector2 vector8 = (Vector2.One * new Vector2((float)NPC.width / 7f, (float)NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(l - (num24 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num24) + NPC.Center;
					Vector2 vector9 = vector8 - NPC.Center;
					Dust obj2 = Main.dust[Dust.NewDust(vector8 + vector9, 0, 0, DustID.GemRuby, vector9.X * 2f, vector9.Y * 2f, 100, default(Color), 1.4f)];
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
					NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("TrueDreadOrbiter").Type, NPC.whoAmI, (float)(m * num26), (float)NPC.whoAmI, 0f, 0f, 255);
				}
			}
			if (timer == 2370 && NPC.life >= NPC.lifeMax / 2)
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
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, vector10, Mod.Find<ModProjectile>("DreadScythe").Type, num4, 0f, Main.myPlayer, 0f, 0f);
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
					NPC.velocity *= 0f;
				}
				if (timer == 2490 || timer == 2550 || timer == 2610)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/DreadRay"));
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
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector11.X, vector11.Y, Mod.Find<ModProjectile>("DreadRay").Type, num4, 2f, Main.myPlayer, (float)num30, 0f);
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
				NPC.velocity = new Vector2(NPC.velocity.X, NPC.velocity.Y).RotatedBy(MathHelper.ToRadians(Spin - 30));
				NPC.TargetClosest(faceTarget: false);
				rotate -= 2f;
				Vector2 vector12 = new Vector2(1000f, 0f).RotatedBy(MathHelper.ToRadians(rotate * 1.57f));
				SpinX = player.Center.X + vector12.X - NPC.Center.X;
				SpinY = player.Center.Y + vector12.Y - NPC.Center.Y;
				float num32 = (float)Math.Sqrt(SpinX * SpinX + SpinY * SpinY);
				if (num32 > 48f)
				{
					num32 = 6.8f / num32;
					SpinX *= num32 * 6f;
					SpinY *= num32 * 6f;
					NPC.velocity.X = SpinX;
					NPC.velocity.Y = SpinY;
				}
				else
				{
					NPC.position.X = player.Center.X + vector12.X - (float)(NPC.height / 2);
					NPC.position.Y = player.Center.Y + vector12.Y - (float)(NPC.width / 2);
					num32 = 6.8f / num32;
					SpinX *= num32 * 6f;
					SpinY *= num32 * 6f;
					NPC.velocity.X = 0f;
					NPC.velocity.Y = 0f;
				}
				CircleShootTimer++;
				if (CircleShootTimer == 40)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("DreadScythe").Type, num4, 1f, Main.myPlayer, 0f, 0f);
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
			NPC.velocity *= 0f;
		}
		if (CircleTimer == 260)
		{
			SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/DreadRoar"));
			Vector2 vector13 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			num = (float)Math.Atan2(vector13.Y - (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f), vector13.X - (Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f));
			NPC.velocity.X = (float)(Math.Cos(num) * 30.0) * -1f;
			NPC.velocity.Y = (float)(Math.Sin(num) * 30.0) * -1f;
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
		Vector2 vector = player.Center + offset - NPC.Center;
		float num = Magnitude(vector);
		if (num > speed)
		{
			vector *= speed / num;
		}
		float num2 = 10f;
		vector = (NPC.velocity * num2 + vector) / (num2 + 1f);
		num = Magnitude(vector);
		if (num > speed)
		{
			vector *= speed / num;
		}
		NPC.velocity = vector;
	}

	private float Magnitude(Vector2 mag)
	{
		return (float)Math.Sqrt(mag.X * mag.X + mag.Y * mag.Y);
	}

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<TrueDreadBag>()));
		npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DreadMask").Type, 7));
		npcLoot.Add(ItemDropRule.Common(Mod.Find<ModItem>("DreadTrophyItem").Type, 10));
        LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
        notExpert.OnSuccess(ItemDropRule.OneFromOptions(1, Mod.Find<ModItem>("DreadSpear").Type, Mod.Find<ModItem>("DreadYoyo").Type, Mod.Find<ModItem>("DreadDisc").Type, Mod.Find<ModItem>("DreadFlameBlaster").Type, Mod.Find<ModItem>("FearStaff").Type, Mod.Find<ModItem>("DreadTome").Type, Mod.Find<ModItem>("DreadScepter").Type));
        npcLoot.Add(notExpert);
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<NightmareFuel>(), 1, 20, 34));
    }

	public override void OnKill()
	{
		if (Main.bloodMoon)
		{
			Item.NewItem(NPC.GetSource_Death(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("ExistentialDread").Type, 1, false, 0, false, false);
			if (!UltraniumWorld.ExistentialDread)
			{
				UltraniumWorld.ExistentialDread = true;
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.WorldData);
				}
			}
		}
		if (!UltraniumWorld.downedTrueDread)
		{
			UltraniumWorld.downedTrueDread = true;
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData);
			}
		}
	}

	public override bool CheckDead()
	{
		for (int i = 0; i < 30; i++)
		{
			int num = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemRuby, 0f, -2f, 0, default(Color), 1.5f);
			Main.dust[num].noGravity = false;
			Main.dust[num].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num].position != NPC.Center)
			{
				Main.dust[num].velocity = NPC.DirectionTo(Main.dust[num].position) * 3f;
			}
		}
		for (int j = 0; j < 60; j++)
		{
			int num2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemRuby, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num2].noGravity = false;
			Main.dust[num2].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num2].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num2].position != NPC.Center)
			{
				Main.dust[num2].velocity = NPC.DirectionTo(Main.dust[num2].position) * 6f;
			}
		}
		for (int k = 0; k < 80; k++)
		{
			int num3 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemRuby, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num3].noGravity = false;
			Main.dust[num3].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num3].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num3].position != NPC.Center)
			{
				Main.dust[num3].velocity = NPC.DirectionTo(Main.dust[num3].position) * 9f;
			}
		}
		for (int l = 0; l < 120; l++)
		{
			int num4 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemRuby, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num4].noGravity = false;
			Main.dust[num4].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num4].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num4].position != NPC.Center)
			{
				Main.dust[num4].velocity = NPC.DirectionTo(Main.dust[num4].position) * 12f;
			}
		}
		for (int m = 0; m < 150; m++)
		{
			int num5 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemRuby, 0f, -2f, 0, default(Color), 2f);
			Main.dust[num5].noGravity = false;
			Main.dust[num5].position.X += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			Main.dust[num5].position.Y += (float)Main.rand.Next(-50, 51) * 0.05f - 1.5f;
			if (Main.dust[num5].position != NPC.Center)
			{
				Main.dust[num5].velocity = NPC.DirectionTo(Main.dust[num5].position) * 15f;
			}
		}
		return true;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 3544;
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter >= 8.0)
		{
			NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
			NPC.frameCounter = 1.0;
		}
	}
}
