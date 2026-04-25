using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.BossBags;
using Ultranium.Items.Ice;
using Ultranium.Items.Vanity.BossMasks;
using Ultranium.Tiles.Trophy;

namespace Ultranium.NPCs.IceDragon;

[AutoloadBossHead]
public class IceDragon : ModNPC
{
	private int MoveSpeed;

	private int MoveSpeedY;

	private float HomeY = 150f;

	private int Timer1;

	private int Timer2;

	private int AttackType;

	private int AttackType2;

	private int FlyDirection;

	private int TransitionTimer;

	public static bool Transition;

	public static bool Phase2;

	public static bool BlizzardEffect;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Glacieron");
		Main.npcFrameCount[NPC.type] = 8;
		NPCID.Sets.TrailCacheLength[NPC.type] = 10;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		NPC.width = 244;
		NPC.height = 244;
		NPC.damage = 32;
		NPC.defense = 20;
		NPC.lifeMax = 5200;
		NPC.HitSound = SoundID.NPCHit56;
		NPC.DeathSound = SoundID.NPCDeath60;
		NPC.value = Item.buyPrice(0, 8);
		NPC.knockBackResist = 0f;
		NPC.aiStyle = -1;
		NPC.boss = true;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/IceDragon");
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.75f * balance * bossAdjustment);
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/IceDragon/IceDragonTrail").Width() * 0.45f, (float)NPC.height * 0.5f);
			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/IceDragon/IceDragonTrail").Value, position, NPC.frame, color, NPC.rotation, vector, NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool PreAI()
	{
		if (Main.player[NPC.target].dead)
		{
			NPC.velocity.Y = -30f;
			NPC.ai[3] += 1f;
			if (NPC.ai[3] >= 120f)
			{
				NPC.active = false;
			}
		}
		NPC.rotation = NPC.velocity.X * 0.02f;
		NPC.spriteDirection = NPC.direction;
		Player player = Main.player[NPC.target];
		NPC.TargetClosest();
		Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
		vector.Normalize();
		int num = (Main.expertMode ? 20 : 35);
		if (NPC.life > NPC.lifeMax / 2)
		{
			NPC.velocity *= 0.985f;
			Timer1++;
			if (Timer1 == 60 || Timer1 == 120 || Timer1 == 180)
			{
				vector.X *= 12f;
				vector.Y *= 12f;
				NPC.velocity.X = vector.X;
				NPC.velocity.Y = vector.Y;
				Vector2 vector2 = Main.player[NPC.target].Center - NPC.Center;
				vector2.Normalize();
				vector2.X *= 12f;
				vector2.Y *= 12f;
			}
			if (Timer1 > 270)
			{
				if (NPC.Center.X >= player.Center.X && MoveSpeed >= -53)
				{
					MoveSpeed--;
				}
				else if (NPC.Center.X <= player.Center.X && MoveSpeed <= 53)
				{
					MoveSpeed++;
				}
				NPC.velocity.X = (float)MoveSpeed * 0.07f;
				if (NPC.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -30)
				{
					MoveSpeedY--;
					HomeY = 100f;
				}
				else if (NPC.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 30)
				{
					MoveSpeedY++;
				}
				NPC.velocity.Y = (float)MoveSpeedY * 0.07f;
				if (Main.rand.Next(220) == 6)
				{
					HomeY = -34f;
				}
			}
			if (AttackType == 0 && (Timer1 == 280 || Timer1 == 300 || Timer1 == 320))
			{
				float num2 = 15f;
				int num3 = 128;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				float num4 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num4) * (double)num2 * -1.0), (float)(Math.Sin(num4) * (double)num2 * -1.0), num3, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (AttackType == 1 && (Timer1 == 280 || Timer1 == 320))
			{
				for (int i = -1; i <= 1; i++)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 7f * NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)i), Mod.Find<ModProjectile>("IceSpike").Type, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 2)
			{
				if (Timer1 == 280)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/DragonRoar") with { PitchVariance = 0.6f });
                }
				if (Timer1 == 300)
				{
					float num5 = 14f;
					int num6 = Mod.Find<ModProjectile>("IceTwisterSmall").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					float num7 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num7) * (double)num5 * -1.0), (float)(Math.Sin(num7) * (double)num5 * -1.0), num6, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (Timer1 >= 360)
			{
				Timer1 = 0;
				if (AttackType <= 2)
				{
					AttackType++;
				}
				if (AttackType > 2)
				{
					AttackType = 0;
				}
			}
		}
		else
		{
			if (NPC.life < NPC.lifeMax / 2 && !Phase2)
			{
				Phase2 = true;
				Transition = true;
			}
			if (Transition)
			{
				NPC.immortal = true;
				NPC.dontTakeDamage = true;
				NPC.rotation = NPC.velocity.X * 0.02f;
				Timer1 = 0;
				TransitionTimer++;
				if (TransitionTimer < 300)
				{
					NPC.velocity *= 0f;
				}
				if (TransitionTimer == 250)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/DragonRoar") with { PitchVariance = 0.6f });
                    Ultranium.seizureAmount = 12f;
					BlizzardEffect = true;
				}
				if (TransitionTimer == 300)
				{
					TransitionTimer = 0;
					Transition = false;
				}
			}
			if (Phase2 && !Transition)
			{
				NPC.velocity *= 0.989f;
				NPC.immortal = false;
				NPC.dontTakeDamage = false;
				Timer2++;
				if (Timer2 == 60 || Timer2 == 120 || Timer2 == 180)
				{
					vector.X *= 13f;
					vector.Y *= 13f;
					NPC.velocity.X = vector.X;
					NPC.velocity.Y = vector.Y;
					Vector2 vector3 = Main.player[NPC.target].Center - NPC.Center;
					vector3.Normalize();
					vector3.X *= 13f;
					vector3.Y *= 13f;
				}
				if (Timer2 == 240)
				{
					for (int j = 0; j < 50; j++)
					{
						int num8 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ManaRegeneration);
						Main.dust[num8].scale = 1.5f;
					}
					if (FlyDirection == 0)
					{
						NPC.position.X = player.position.X + 400f;
						NPC.position.Y = player.position.Y - 500f;
					}
					if (FlyDirection == 1)
					{
						NPC.position.X = player.position.X - 600f;
						NPC.position.Y = player.position.Y - 500f;
					}
					for (int k = 0; k < 50; k++)
					{
						int num9 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.ManaRegeneration);
						Main.dust[num9].scale = 1.5f;
					}
				}
				if (Timer2 > 240 && Timer2 < 320)
				{
					if (FlyDirection == 0)
					{
						NPC.velocity.X = -14f;
						NPC.velocity.Y = 0f;
					}
					if (FlyDirection == 1)
					{
						NPC.velocity.X = 14f;
						NPC.velocity.Y = 0f;
					}
					if (Main.rand.Next(8) == 0)
					{
						int num10 = 128;
						SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
						Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 7f, num10, num + 10, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer2 == 320)
				{
					NPC.velocity *= 0f;
				}
				if (AttackType2 == 0 && (Timer2 == 360 || Timer2 == 390 || Timer2 == 420 || Timer2 == 450 || Timer2 == 480))
				{
					float num11 = 6f;
					int num12 = Mod.Find<ModProjectile>("IceWave").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					float num13 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num13) * (double)num11 * -1.0), (float)(Math.Sin(num13) * (double)num11 * -1.0), num12, num + 10, 0f, Main.myPlayer, 0f, 0f);
				}
				if (AttackType2 == 1 && (Timer2 == 360 || Timer2 == 420 || Timer2 == 480))
				{
					for (int l = -2; l <= 2; l++)
					{
						Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, 6f * NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)l), Mod.Find<ModProjectile>("IceSpike").Type, num + 10, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (AttackType2 == 2 && Timer2 == 400)
				{
					SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/DragonRoar") with { PitchVariance = 0.6f });
                    float num14 = 4f;
					int num15 = Mod.Find<ModProjectile>("IceVortex").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
					float num16 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num16) * (double)num14 * -1.0), (float)(Math.Sin(num16) * (double)num14 * -1.0), num15, num + 10, 0f, Main.myPlayer, 0f, 0f);
				}
				if (Timer2 >= 520)
				{
					Timer2 = 0;
					if (AttackType2 <= 2)
					{
						AttackType2++;
					}
					if (AttackType2 > 2)
					{
						AttackType2 = 0;
					}
					if (FlyDirection <= 1)
					{
						FlyDirection++;
					}
					if (FlyDirection > 1)
					{
						FlyDirection = 0;
					}
				}
			}
		}
		if (!NPC.active)
		{
			BlizzardEffect = false;
			Transition = false;
			Phase2 = false;
		}
		return true;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0)
		{
			if (!Main.dedServ)
            {
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonGore1").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonGore2").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonGore3").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonGore4").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonGore5").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonGore6").Type);
                Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("DragonGore7").Type);
            }
		}
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter > 5.0)
		{
			NPC.frame.Y = NPC.frame.Y + frameHeight;
			NPC.frameCounter = 0.0;
		}
		if (NPC.frame.Y >= frameHeight * 7)
		{
			NPC.frame.Y = 0;
		}
		if (Timer1 > 240 || TransitionTimer >= 250 || (Timer2 >= 360 && Timer2 < 520))
		{
			NPC.frame.Y = frameHeight * 7;
		}
	}

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
		npcLoot.Add(ItemDropRule.BossBag(ModContent.ItemType<IceDragonBag>()));
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceDragonMask>(), 7));
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<IceDragonTrophyItem>()));
        LeadingConditionRule notExpert = new LeadingConditionRule(new Conditions.NotExpert());
        notExpert.OnSuccess(ItemDropRule.OneFromOptions(1, ModContent.ItemType<GlacialFlail>(), ModContent.ItemType<GlacialGun>(), ModContent.ItemType<GlacialWand>()));
        npcLoot.Add(notExpert);
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.NotExpert(), ModContent.ItemType<IcePelt>(), 1, 12, 17));
    }

	public override void OnKill()
	{
		if (!UltraniumWorld.downedDragon)
		{
			UltraniumWorld.downedDragon = true;
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.WorldData);
			}
		}
		BlizzardEffect = false;
		Transition = false;
		Phase2 = false;
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 188;
	}
}
