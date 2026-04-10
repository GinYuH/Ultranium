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
		// ((ModNPC)this).DisplayName.SetDefault("Mind Flayer");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 12;
		NPCID.Sets.TrailCacheLength[((ModNPC)this).NPC.type] = 15;
		NPCID.Sets.TrailingMode[((ModNPC)this).NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.scale = 1f;
		((ModNPC)this).NPC.width = 176;
		((ModNPC)this).NPC.height = 252;
		((ModNPC)this).NPC.damage = 90;
		((ModNPC)this).NPC.lifeMax = 100000;
		((ModNPC)this).NPC.defense = 75;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit55;
		((ModNPC)this).NPC.DeathSound = ((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/MindFlayerRoar")?.WithVolume(1.2f)?.WithPitchVariance(0.5f);
		base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/MindFlayer");
		((ModNPC)this).NPC.defense = 60;
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.lavaImmune = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.netAlways = true;
		((ModNPC)this).NPC.aiStyle = 0;
		((ModNPC)this).NPC.alpha = 255;
		for (int i = 0; i < 206; i++)
		{
			((ModNPC)this).NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		((ModNPC)this).NPC.lifeMax = 200000;
		((ModNPC)this).NPC.damage = 120;
		((ModNPC)this).NPC.defense = 75;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (((ModNPC)this).NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/MindFlayerTrail").Width * 0.5f, (float)((ModNPC)this).NPC.height * 0.5f);
			for (int i = 0; i < ((ModNPC)this).NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((((ModNPC)this).NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = ((ModNPC)this).NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, ((ModNPC)this).NPC.gfxOffY);
				Color color = ((ModNPC)this).NPC.GetAlpha(drawColor) * ((float)(((ModNPC)this).NPC.oldPos.Length - i) / (float)((ModNPC)this).NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.GetTexture("Ultranium/NPCs/ShadowEvent/MindFlayerTrail"), position, ((ModNPC)this).NPC.frame, color, ((ModNPC)this).NPC.rotation, vector, ((ModNPC)this).NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/MindFlayerGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/MindFlayerGore2"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/ShadowEvent/MindFlayerGore3"));
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		player.AddBuff(163, 60, fromNetPvP: true);
		player.AddBuff(((ModNPC)this).Mod.Find<ModBuff>("DarkDebuff").Type, 300);
	}

	public override void FindFrame(int frameHeight)
	{
		if (!HentaiFace)
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
		if (HentaiFace)
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
		((ModNPC)this).NPC.rotation = ((ModNPC)this).NPC.velocity.X * 0.02f;
		Player player = Main.player[((ModNPC)this).NPC.target];
		bool expertMode = Main.expertMode;
		((ModNPC)this).NPC.netUpdate = true;
		((ModNPC)this).NPC.TargetClosest();
		int num = (expertMode ? 40 : 55);
		if (((ModNPC)this).NPC.ai[0] == 0f)
		{
			if (((ModNPC)this).NPC.Center.X >= player.Center.X && MoveSpeed >= -75)
			{
				MoveSpeed--;
			}
			else if (((ModNPC)this).NPC.Center.X <= player.Center.X && MoveSpeed <= 75)
			{
				MoveSpeed++;
			}
			((ModNPC)this).NPC.velocity.X = (float)MoveSpeed * 0.1f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -75)
			{
				MoveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 75)
			{
				MoveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)MoveSpeedY * 0.12f;
		}
		if (((ModNPC)this).NPC.ai[0] == 1f)
		{
			if (((ModNPC)this).NPC.Center.X >= player.Center.X && MoveSpeed >= -150)
			{
				MoveSpeed--;
			}
			else if (((ModNPC)this).NPC.Center.X <= player.Center.X && MoveSpeed <= 150)
			{
				MoveSpeed++;
			}
			((ModNPC)this).NPC.velocity.X = (float)MoveSpeed * 0.1f;
			if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && MoveSpeedY >= -150)
			{
				MoveSpeedY--;
				HomeY = 150f;
			}
			else if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && MoveSpeedY <= 150)
			{
				MoveSpeedY++;
			}
			((ModNPC)this).NPC.velocity.Y = (float)MoveSpeedY * 0.12f;
		}
		RoarTimer++;
		if (RoarTimer == 360)
		{
			SoundEngine.PlaySound(((ModNPC)this).Mod.GetLegacySoundSlot((SoundType)50, "Sounds/MindFlayerGrowl").WithVolume(2f).WithPitchVariance(0.5f), -1, -1);
			RoarTimer = 0;
		}
		if (TeleportTimer <= 0)
		{
			timer++;
		}
		if (timer < 100)
		{
			((ModNPC)this).NPC.velocity *= 0f;
			((ModNPC)this).NPC.alpha -= 5;
		}
		if (timer == 120 || timer == 170 || timer == 220 || timer == 270 || timer == 320 || timer == 370)
		{
			Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector.Normalize();
			vector.X *= 9.5f;
			vector.Y *= 9.5f;
			int num2 = Main.rand.Next(3, 5);
			for (int i = 0; i < num2; i++)
			{
				float num3 = (float)Main.rand.Next(-150, 150) * 0.04f;
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector.X + num3, vector.Y + num3, ((ModNPC)this).Mod.Find<ModProjectile>("FlayerScythe").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer >= 430 && timer <= 730)
		{
			((ModNPC)this).NPC.velocity *= 0f;
			HentaiFace = true;
		}
		if (timer == 470 || timer == 510 || timer == 550 || timer == 590 || timer == 630 || timer == 670 || timer == 710)
		{
			SoundEngine.PlaySound(SoundID.Item78, ((ModNPC)this).NPC.Center);
			float num4 = 26f;
			float num5 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Vector2 spinninpoint = new Vector2((float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0));
			spinninpoint = spinninpoint.RotatedByRandom(MathHelper.ToRadians(30f));
			Main.projectile[Projectile.NewProjectile(((ModNPC)this).NPC.Center, spinninpoint, ((ModNPC)this).Mod.Find<ModProjectile>("FlayerTentacleBody").Type, num, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 200f;
		}
		if (timer == 730)
		{
			((ModNPC)this).NPC.ai[0] = 1f;
			HentaiFace = false;
		}
		if (timer == 790 || timer == 880)
		{
			Vector2 vector2 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector2.Normalize();
			vector2.X *= 10.5f;
			vector2.Y *= 10.5f;
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector2.X, vector2.Y, ((ModNPC)this).Mod.Find<ModProjectile>("FlayerVortex").Type, num, 1f, ((ModNPC)this).NPC.target, 0f, 0f);
		}
		if (timer >= 1010)
		{
			if (TeleportNum < 6)
			{
				TeleportTimer++;
				((ModNPC)this).NPC.velocity *= 0f;
				if (TeleportTimer == 1)
				{
					Projectile.NewProjectile(player.Center + Main.rand.NextVector2Square(-600f, 600f), Main.rand.NextVector2Square(-1f, 1f), ((ModNPC)this).Mod.Find<ModProjectile>("FlayerTelegraph").Type, 0, 6f, player.whoAmI, 0f, 0f);
				}
				if (TeleportTimer == 100 || TeleportTimer == 110 || TeleportTimer == 120 || TeleportTimer == 130 || TeleportTimer == 140)
				{
					Vector2 vector3 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
					vector3.Normalize();
					vector3.X *= 8.5f;
					vector3.Y *= 8.5f;
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector3.X, vector3.Y, ((ModNPC)this).Mod.Find<ModProjectile>("FlayerSpit").Type, num, 1f, ((ModNPC)this).NPC.target, 0f, 0f);
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
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, 0f, 0f, ((ModNPC)this).Mod.Find<ModProjectile>("FlayerAuraBase").Type, num + 30, 1f, Main.myPlayer, 0f, 0f);
			}
			if (timer > 1120 && timer < 1660)
			{
				((ModNPC)this).NPC.velocity *= 0f;
				HentaiFace = true;
			}
			if (timer > 1120 && timer < 1660)
			{
				((ModNPC)this).NPC.immortal = true;
				((ModNPC)this).NPC.dontTakeDamage = true;
				if (Main.rand.Next(7) == 0)
				{
					Vector2 vector4 = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
					vector4.Normalize();
					vector4.X *= 8.5f;
					vector4.Y *= 8.5f;
					Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector4.X, vector4.Y, ((ModNPC)this).Mod.Find<ModProjectile>("FlayerSpit").Type, num, 1f, ((ModNPC)this).NPC.target, 0f, 0f);
				}
			}
			else
			{
				((ModNPC)this).NPC.immortal = false;
				((ModNPC)this).NPC.dontTakeDamage = false;
			}
		}
		if (timer == 1720)
		{
			if (((ModNPC)this).NPC.life <= ((ModNPC)this).NPC.lifeMax / 2 && NPC.CountNPCS(((ModNPC)this).Mod.Find<ModNPC>("MindFlayerClone").Type) < 1)
			{
				NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y + 1000, ((ModNPC)this).Mod.Find<ModNPC>("MindFlayerClone").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y - 1000, ((ModNPC)this).Mod.Find<ModNPC>("MindFlayerClone").Type, 0, 0f, 0f, 0f, 0f, 255);
			}
			timer = 100;
			((ModNPC)this).NPC.ai[0] = 0f;
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
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("FlayerBrain").Type, 1, false, 0, false, false);
		}
		int num = Main.rand.Next(3);
		if (num == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("FlayerBlade").Type, 1, false, 0, false, false);
		}
		if (num == 1)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("FlayerStaff").Type, 1, false, 0, false, false);
		}
		if (num == 2)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("FlayerBow").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(7) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("MindFlayerMask").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(10) == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("FlayerTrophyItem").Type, 1, false, 0, false, false);
		}
		Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("DarkMatter").Type, Main.rand.Next(5, 12), false, 0, false, false);
		Item.NewItem(((ModNPC)this).NPC.getRect(), ((ModNPC)this).Mod.Find<ModItem>("EldritchBlood").Type, Main.rand.Next(15, 25), false, 0, false, false);
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
