using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

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
		// ((ModNPC)this).DisplayName.SetDefault("Glacieron");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 8;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).NPC.type] = 10;
		NPCID.Sets.TrailingMode[((ModNPC)this).NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 244;
		((ModNPC)this).NPC.height = 244;
		((ModNPC)this).NPC.damage = 32;
		((ModNPC)this).NPC.defense = 20;
		((ModNPC)this).NPC.lifeMax = 5200;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit56;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath60;
		((ModNPC)this).NPC.value = Item.buyPrice(0, 8);
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.aiStyle = -1;
		((ModNPC)this).NPC.boss = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/IceDragon");
		base.bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = ((ModNPC)this).Mod.Find<ModItem>("IceDragonBag").Type;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 6300;
		((ModNPC)this).NPC.damage = 42;
		((ModNPC)this).NPC.defense = 35;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (((ModNPC)this).NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/IceDragon/IceDragonTrail").Width * 0.45f, (float)((ModNPC)this).NPC.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY);
				Color color = ((ModNPC)this).NPC.GetAlpha(drawColor) * ((float)(((ModNPC)this).NPC.oldPos.Length - i) / (float)((ModNPC)this).NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/IceDragon/IceDragonTrail"), position, ((ModNPC)this).NPC.frame, color, ((ModNPC)this).NPC.rotation, vector, ((ModNPC)this).NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool PreAI()
	{
		if (Main.player[((ModNPC)this).NPC.target].dead)
		{
			((ModNPC)this).NPC.velocity.Y = -30f;
			((ModNPC)this).NPC.ai[3] += 1f;
			if (((ModNPC)this).NPC.ai[3] >= 120f)
			{
				((Entity)((ModNPC)this).NPC).active = false;
			}
		}
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.02f;
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
		Player player = Main.player[((ModNPC)this).NPC.target];
		((ModNPC)this).NPC.TargetClosest();
		Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
		vector.Normalize();
		int num = (Main.expertMode ? 20 : 35);
		if (((ModNPC)this).NPC.life > ((ModNPC)this).NPC.lifeMax / 2)
		{
			((ModNPC)this).NPC.velocity *= 0.985f;
			Timer1++;
			if (Timer1 == 60 || Timer1 == 120 || Timer1 == 180)
			{
				vector.X *= 12f;
				vector.Y *= 12f;
				((ModNPC)this).NPC.velocity.X = vector.X;
				((ModNPC)this).NPC.velocity.Y = vector.Y;
				Vector2 vector2 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
				vector2.Normalize();
				vector2.X *= 12f;
				vector2.Y *= 12f;
			}
			if (Timer1 > 270)
			{
				if (((ModNPC)this).NPC.Center.X >= player.Center.X && MoveSpeed >= -53)
				{
					MoveSpeed--;
				}
				else if (((ModNPC)this).NPC.Center.X <= player.Center.X && MoveSpeed <= 53)
				{
					MoveSpeed++;
				}
				((ModNPC)this).NPC.velocity.X = (float)MoveSpeed * 0.07f;
				if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -30)
				{
					MoveSpeedY--;
					HomeY = 100f;
				}
				else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 30)
				{
					MoveSpeedY++;
				}
				((ModNPC)this).NPC.velocity.Y = (float)MoveSpeedY * 0.07f;
				if (Main.rand.Next(220) == 6)
				{
					HomeY = -34f;
				}
			}
			if (AttackType == 0 && (Timer1 == 280 || Timer1 == 300 || Timer1 == 320))
			{
				float num2 = 15f;
				int num3 = 128;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
				float num4 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num4) * (double)num2 * -1.0), (float)(Math.Sin(num4) * (double)num2 * -1.0), num3, num, 0f, Main.myPlayer, 0f, 0f);
			}
			if (AttackType == 1 && (Timer1 == 280 || Timer1 == 320))
			{
				for (int i = -1; i <= 1; i++)
				{
					Projectile.NewProjectile(((ModNPC)this).NPC.Center, 7f * ((ModNPC)this).NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)i), ((ModNPC)this).Mod.Find<ModProjectile>("IceSpike").Type, num, 0f, Main.myPlayer, 0f, 0f);
				}
			}
			if (AttackType == 2)
			{
				if (Timer1 == 280)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/DragonRoar")?.WithVolume(7f)?.WithPitchVariance(0.6f), -1, -1);
				}
				if (Timer1 == 300)
				{
					float num5 = 14f;
					int num6 = ((ModNPC)this).Mod.Find<ModProjectile>("IceTwisterSmall").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num7 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num7) * (double)num5 * -1.0), (float)(Math.Sin(num7) * (double)num5 * -1.0), num6, num, 0f, Main.myPlayer, 0f, 0f);
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
			if (((ModNPC)this).NPC.life < ((ModNPC)this).NPC.lifeMax / 2 && !Phase2)
			{
				Phase2 = true;
				Transition = true;
			}
			if (Transition)
			{
				((ModNPC)this).NPC.immortal = true;
				((ModNPC)this).NPC.dontTakeDamage = true;
				((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.02f;
				Timer1 = 0;
				TransitionTimer++;
				if (TransitionTimer < 300)
				{
					((ModNPC)this).NPC.velocity *= 0f;
				}
				if (TransitionTimer == 250)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/DragonRoar")?.WithVolume(7f)?.WithPitchVariance(0.6f), -1, -1);
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
				((ModNPC)this).NPC.velocity *= 0.989f;
				((ModNPC)this).NPC.immortal = false;
				((ModNPC)this).NPC.dontTakeDamage = false;
				Timer2++;
				if (Timer2 == 60 || Timer2 == 120 || Timer2 == 180)
				{
					vector.X *= 13f;
					vector.Y *= 13f;
					((ModNPC)this).NPC.velocity.X = vector.X;
					((ModNPC)this).NPC.velocity.Y = vector.Y;
					Vector2 vector3 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
					vector3.Normalize();
					vector3.X *= 13f;
					vector3.Y *= 13f;
				}
				if (Timer2 == 240)
				{
					for (int j = 0; j < 50; j++)
					{
						int num8 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 45);
						Main.dust[num8].scale = 1.5f;
					}
					if (FlyDirection == 0)
					{
						((ModNPC)this).NPC.position.X = player.position.X + 400f;
						((ModNPC)this).NPC.position.Y = player.position.Y - 500f;
					}
					if (FlyDirection == 1)
					{
						((ModNPC)this).NPC.position.X = player.position.X - 600f;
						((ModNPC)this).NPC.position.Y = player.position.Y - 500f;
					}
					for (int k = 0; k < 50; k++)
					{
						int num9 = Dust.NewDust(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 45);
						Main.dust[num9].scale = 1.5f;
					}
				}
				if (Timer2 > 240 && Timer2 < 320)
				{
					if (FlyDirection == 0)
					{
						((ModNPC)this).NPC.velocity.X = -14f;
						((ModNPC)this).NPC.velocity.Y = 0f;
					}
					if (FlyDirection == 1)
					{
						((ModNPC)this).NPC.velocity.X = 14f;
						((ModNPC)this).NPC.velocity.Y = 0f;
					}
					if (Main.rand.Next(8) == 0)
					{
						int num10 = 128;
						SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
						Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
						Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 7f, num10, num + 10, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (Timer2 == 320)
				{
					((ModNPC)this).NPC.velocity *= 0f;
				}
				if (AttackType2 == 0 && (Timer2 == 360 || Timer2 == 390 || Timer2 == 420 || Timer2 == 450 || Timer2 == 480))
				{
					float num11 = 6f;
					int num12 = ((ModNPC)this).Mod.Find<ModProjectile>("IceWave").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num13 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num13) * (double)num11 * -1.0), (float)(Math.Sin(num13) * (double)num11 * -1.0), num12, num + 10, 0f, Main.myPlayer, 0f, 0f);
				}
				if (AttackType2 == 1 && (Timer2 == 360 || Timer2 == 420 || Timer2 == 480))
				{
					for (int l = -2; l <= 2; l++)
					{
						Projectile.NewProjectile(((ModNPC)this).NPC.Center, 6f * ((ModNPC)this).NPC.DirectionTo(player.Center).RotatedBy(MathHelper.ToRadians(5f) * (float)l), ((ModNPC)this).Mod.Find<ModProjectile>("IceSpike").Type, num + 10, 0f, Main.myPlayer, 0f, 0f);
					}
				}
				if (AttackType2 == 2 && Timer2 == 400)
				{
					SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/DragonRoar")?.WithVolume(7f)?.WithPitchVariance(0.6f), -1, -1);
					float num14 = 4f;
					int num15 = ((ModNPC)this).Mod.Find<ModProjectile>("IceVortex").Type;
					SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
					float num16 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num16) * (double)num14 * -1.0), (float)(Math.Sin(num16) * (double)num14 * -1.0), num15, num + 10, 0f, Main.myPlayer, 0f, 0f);
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
		if (!((Entity)((ModNPC)this).NPC).active)
		{
			BlizzardEffect = false;
			Transition = false;
			Phase2 = false;
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/DragonGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/DragonGore2"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/DragonGore3"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/DragonGore4"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/DragonGore5"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/DragonGore6"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/DragonGore7"));
		return true;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter > 5.0)
		{
			((ModNPC)this).NPC.frame.Y = ((ModNPC)this).NPC.frame.Y + frameHeight;
			((ModNPC)this).NPC.frameCounter = 0.0;
		}
		if (((ModNPC)this).NPC.frame.Y >= frameHeight * 7)
		{
			((ModNPC)this).NPC.frame.Y = 0;
		}
		if (Timer1 > 240 || TransitionTimer >= 250 || (Timer2 >= 360 && Timer2 < 520))
		{
			((ModNPC)this).NPC.frame.Y = frameHeight * 7;
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
			int num = Main.rand.Next(3);
			if (num == 0)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("GlacialFlail").Type, 1, false, 0, false, false);
			}
			if (num == 1)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("GlacialGun").Type, 1, false, 0, false, false);
			}
			if (num == 2)
			{
				Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("GlacialWand").Type, 1, false, 0, false, false);
			}
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("IcePelt").Type, Main.rand.Next(12, 18), false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("IceDragonMask").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("IceDragonTrophyItem").Type, 1, false, 0, false, false);
		}
		if (!UltraniumWorld.downedDragon)
		{
			UltraniumWorld.downedDragon = true;
			if (Main.netMode == 2)
			{
				NetMessage.SendData(7);
			}
		}
		BlizzardEffect = false;
		Transition = false;
		Phase2 = false;
	}

	public override void BossLoot(ref string name, ref int potionType)
	{
		potionType = 188;
	}
}
