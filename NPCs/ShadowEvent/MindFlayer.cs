using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.ShadowEvent;

namespace Ultranium.NPCs.ShadowEvent;

[AutoloadBossHead]
public class MindFlayer : ModNPC
{
	private int timer;

	private int MoveSpeed;

	private int MoveSpeedY;

	private float HomeY = 130f;

	public int RoarTimer;

	private int TeleportTimer;

	private int TeleportNum;

	private bool HentaiFace;

	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Mind Flayer");
		Main.npcFrameCount[NPC.type] = 12;
		NPCID.Sets.TrailCacheLength[NPC.type] = 15;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		NPC.scale = 1f;
		NPC.width = 176;
		NPC.height = 252;
		NPC.damage = 90;
		NPC.lifeMax = 100000;
		NPC.defense = 75;
		NPC.knockBackResist = 0f;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.HitSound = SoundID.NPCHit55;
		NPC.DeathSound = new SoundStyle("Ultranium/Sounds/MindFlayerRoar")?.WithVolume(1.2f)?.WithPitchVariance(0.5f);
		base.Music = Mod.GetSoundSlot((SoundType)51, "Sounds/Music/MindFlayer");
		NPC.defense = 60;
		NPC.npcSlots = 1f;
		NPC.lavaImmune = true;
		NPC.noGravity = true;
		NPC.netAlways = true;
		NPC.aiStyle = 0;
		NPC.alpha = 255;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 200000;
		NPC.damage = 120;
		NPC.defense = 75;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/MindFlayerTrail").Width * 0.5f, (float)NPC.height * 0.5f);
			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/MindFlayerTrail"), position, NPC.frame, color, NPC.rotation, vector, NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/ShadowEvent/MindFlayerGore1"));
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/ShadowEvent/MindFlayerGore2"));
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/ShadowEvent/MindFlayerGore3"));
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		player.AddBuff(163, 60, fromNetPvP: true);
		player.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 300);
	}

	public override void FindFrame(int frameHeight)
	{
		if (!HentaiFace)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y >= frameHeight * 6)
			{
				NPC.frame.Y = 0;
			}
		}
		if (HentaiFace)
		{
			NPC.frameCounter += 1.0;
			if (NPC.frameCounter > 6.0)
			{
				NPC.frame.Y = NPC.frame.Y + frameHeight;
				NPC.frameCounter = 0.0;
			}
			if (NPC.frame.Y >= frameHeight * 12)
			{
				NPC.frame.Y = frameHeight * 6;
			}
		}
	}

	public override bool PreAI()
	{
		NPC.rotation = NPC.velocity.X * 0.02f;
		Player player = Main.player[NPC.target];
		bool expertMode = Main.expertMode;
		NPC.netUpdate = true;
		NPC.TargetClosest();
		int num = (expertMode ? 40 : 55);
		if (NPC.ai[0] == 0f)
		{
			if (NPC.Center.X >= player.Center.X && MoveSpeed >= -75)
			{
				MoveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && MoveSpeed <= 75)
			{
				MoveSpeed++;
			}
			NPC.velocity.X = (float)MoveSpeed * 0.1f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -75)
			{
				MoveSpeedY--;
				HomeY = 150f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 75)
			{
				MoveSpeedY++;
			}
			NPC.velocity.Y = (float)MoveSpeedY * 0.12f;
		}
		if (NPC.ai[0] == 1f)
		{
			if (NPC.Center.X >= player.Center.X && MoveSpeed >= -150)
			{
				MoveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && MoveSpeed <= 150)
			{
				MoveSpeed++;
			}
			NPC.velocity.X = (float)MoveSpeed * 0.1f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -150)
			{
				MoveSpeedY--;
				HomeY = 150f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 150)
			{
				MoveSpeedY++;
			}
			NPC.velocity.Y = (float)MoveSpeedY * 0.12f;
		}
		RoarTimer++;
		if (RoarTimer == 360)
		{
			SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/MindFlayerGrowl").WithVolume(2f).WithPitchVariance(0.5f), -1, -1);
			RoarTimer = 0;
		}
		if (TeleportTimer <= 0)
		{
			timer++;
		}
		if (timer < 100)
		{
			NPC.velocity *= 0f;
			NPC.alpha -= 5;
		}
		if (timer == 120 || timer == 170 || timer == 220 || timer == 270 || timer == 320 || timer == 370)
		{
			Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
			vector.Normalize();
			vector.X *= 9.5f;
			vector.Y *= 9.5f;
			int num2 = Main.rand.Next(3, 5);
			for (int i = 0; i < num2; i++)
			{
				float num3 = (float)Main.rand.Next(-150, 150) * 0.04f;
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, vector.X + num3, vector.Y + num3, Mod.Find<ModProjectile>("FlayerScythe").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer >= 430 && timer <= 730)
		{
			NPC.velocity *= 0f;
			HentaiFace = true;
		}
		if (timer == 470 || timer == 510 || timer == 550 || timer == 590 || timer == 630 || timer == 670 || timer == 710)
		{
			SoundEngine.PlaySound(SoundID.Item78, NPC.Center);
			float num4 = 26f;
			float num5 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Vector2 spinninpoint = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0));
			spinninpoint = spinninpoint.RotatedByRandom(MathHelper.ToRadians(30f));
			Main.projectile[Projectile.NewProjectile(null, NPC.Center, spinninpoint, Mod.Find<ModProjectile>("FlayerTentacleBody").Type, num, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 200f;
		}
		if (timer == 730)
		{
			NPC.ai[0] = 1f;
			HentaiFace = false;
		}
		if (timer == 790 || timer == 880)
		{
			Vector2 vector2 = Main.player[NPC.target].Center - NPC.Center;
			vector2.Normalize();
			vector2.X *= 10.5f;
			vector2.Y *= 10.5f;
			Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, vector2.X, vector2.Y, Mod.Find<ModProjectile>("FlayerVortex").Type, num, 1f, NPC.target, 0f, 0f);
		}
		if (timer >= 1010)
		{
			if (TeleportNum < 6)
			{
				TeleportTimer++;
				NPC.velocity *= 0f;
				if (TeleportTimer == 1)
				{
					Projectile.NewProjectile(null, player.Center + Main.rand.NextVector2Square(-600f, 600f), Main.rand.NextVector2Square(-1f, 1f), Mod.Find<ModProjectile>("FlayerTelegraph").Type, 0, 6f, player.whoAmI, 0f, 0f);
				}
				if (TeleportTimer == 100 || TeleportTimer == 110 || TeleportTimer == 120 || TeleportTimer == 130 || TeleportTimer == 140)
				{
					Vector2 vector3 = Main.player[NPC.target].Center - NPC.Center;
					vector3.Normalize();
					vector3.X *= 8.5f;
					vector3.Y *= 8.5f;
					Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, vector3.X, vector3.Y, Mod.Find<ModProjectile>("FlayerSpit").Type, num, 1f, NPC.target, 0f, 0f);
					HentaiFace = true;
				}
				if (TeleportTimer >= 160)
				{
					TeleportTimer = 0;
					TeleportNum++;
					HentaiFace = false;
				}
			}
			if (timer == 1120)
			{
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("FlayerAuraBase").Type, num + 30, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer > 1120 && timer < 1660)
			{
				NPC.velocity *= 0f;
				HentaiFace = true;
			}
			if (timer > 1120 && timer < 1660)
			{
				NPC.immortal = true;
				NPC.dontTakeDamage = true;
				if (Main.rand.Next(7) == 0)
				{
					Vector2 vector4 = Main.player[NPC.target].Center - NPC.Center;
					vector4.Normalize();
					vector4.X *= 8.5f;
					vector4.Y *= 8.5f;
					Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, vector4.X, vector4.Y, Mod.Find<ModProjectile>("FlayerSpit").Type, num, 1f, NPC.target, 0f, 0f);
				}
			}
			else
			{
				NPC.immortal = false;
				NPC.dontTakeDamage = false;
			}
		}
		if (timer == 1720)
		{
			if (NPC.life <= NPC.lifeMax / 2 && NPC.CountNPCS(Mod.Find<ModNPC>("MindFlayerClone").Type) < 1)
			{
				NPC.NewNPC(null, (int)NPC.Center.X, (int)NPC.Center.Y + 1000, Mod.Find<ModNPC>("MindFlayerClone").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(null, (int)NPC.Center.X, (int)NPC.Center.Y - 1000, Mod.Find<ModNPC>("MindFlayerClone").Type, 0, 0f, 0f, 0f, 0f, 255);
			}
			timer = 100;
			NPC.ai[0] = 0f;
			TeleportTimer = 0;
			TeleportNum = 0;
			HentaiFace = false;
		}
		return true;
	}

	public override void OnKill()
	{
		if (Main.expertMode)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FlayerBrain").Type, 1, false, 0, false, false);
		}
		int num = Main.rand.Next(3);
		if (num == 0)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FlayerBlade").Type, 1, false, 0, false, false);
		}
		if (num == 1)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FlayerStaff").Type, 1, false, 0, false, false);
		}
		if (num == 2)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FlayerBow").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("MindFlayerMask").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("FlayerTrophyItem").Type, 1, false, 0, false, false);
		}
		Item.NewItem(null, NPC.getRect(), Mod.Find<ModItem>("DarkMatter").Type, Main.rand.Next(5, 12), false, 0, false, false);
		Item.NewItem(null, NPC.getRect(), Mod.Find<ModItem>("EldritchBlood").Type, Main.rand.Next(15, 25), false, 0, false, false);
		if (ShadowEventWorld.ShadowEventActive)
		{
			ShadowEventWorld.Phase2 = true;
			string text = "The darkness thickens...";
			if (Main.netMode == 0)
			{
				Main.NewText(text, (byte)61, byte.MaxValue, (byte)142, false);
			}
			if (Main.netMode == 2)
			{
				NetMessage.SendData(25, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f);
			}
		}
	}

	public override void BossLoot(ref string name, ref int potionType)
	{
		potionType = 3544;
	}
}
