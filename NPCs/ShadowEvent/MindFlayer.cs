using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Ultranium.Items.BossBags.Acc;
using Ultranium.Items.Eldritch;
using Ultranium.Items.Eldritch.ShadowEvent;
using Ultranium.Items.Vanity.BossMasks;
using Ultranium.ShadowEvent;
using Ultranium.Tiles.Trophy;

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
		//DisplayName.SetDefault("Mind Flayer");
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
		NPC.DeathSound = new SoundStyle("Ultranium/Sounds/MindFlayerRoar") with { PitchVariance = 0.5f };
		Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/MindFlayer");
		NPC.defense = 60;
		NPC.npcSlots = 1f;
		NPC.boss = true;
		NPC.lavaImmune = true;
		NPC.noGravity = true;
		NPC.netAlways = true;
		NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
		NPC.alpha = 255;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		NPC.lifeMax = (int)(NPC.lifeMax * 0.8f * balance * bossAdjustment);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowEvent/MindFlayerTrail").Width() * 0.5f, (float)NPC.height * 0.5f);
			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowEvent/MindFlayerTrail").Value, position, NPC.frame, color, NPC.rotation, vector, NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override void HitEffect(NPC.HitInfo hit)
    {
        if (NPC.life > 0 || Main.dedServ)
            return;
        Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MindFlayerGore1").Type);
		Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MindFlayerGore2").Type);
		Gore.NewGore(NPC.GetSource_FromThis(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MindFlayerGore3").Type);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(BuffID.Obstructed, 60, quiet: false);
		target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 300);
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
			SoundEngine.PlaySound(new SoundStyle("Ultranium/Sounds/MindFlayerGrowl") with { PitchVariance = 0.5f });
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
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector.X + num3, vector.Y + num3, Mod.Find<ModProjectile>("FlayerScythe").Type, num, 1f, Main.myPlayer, 0f, 0f);
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
			Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center, spinninpoint, Mod.Find<ModProjectile>("FlayerTentacleBody").Type, num, 0f, Main.myPlayer, 0f, 0f)].localAI[1] = 200f;
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
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector2.X, vector2.Y, Mod.Find<ModProjectile>("FlayerVortex").Type, num, 1f, NPC.target, 0f, 0f);
		}
		if (timer >= 1010)
		{
			if (TeleportNum < 6)
			{
				TeleportTimer++;
				NPC.velocity *= 0f;
				if (TeleportTimer == 1)
				{
					Projectile.NewProjectile(NPC.GetSource_FromThis(), player.Center + Main.rand.NextVector2Square(-600f, 600f), Main.rand.NextVector2Square(-1f, 1f), Mod.Find<ModProjectile>("FlayerTelegraph").Type, 0, 6f, player.whoAmI, 0f, 0f);
				}
				if (TeleportTimer == 100 || TeleportTimer == 110 || TeleportTimer == 120 || TeleportTimer == 130 || TeleportTimer == 140)
				{
					Vector2 vector3 = Main.player[NPC.target].Center - NPC.Center;
					vector3.Normalize();
					vector3.X *= 8.5f;
					vector3.Y *= 8.5f;
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector3.X, vector3.Y, Mod.Find<ModProjectile>("FlayerSpit").Type, num, 1f, NPC.target, 0f, 0f);
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
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("FlayerAuraBase").Type, num + 30, 1f, Main.myPlayer, 0f, 0f);
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
					Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector4.X, vector4.Y, Mod.Find<ModProjectile>("FlayerSpit").Type, num, 1f, NPC.target, 0f, 0f);
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
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y + 1000, Mod.Find<ModNPC>("MindFlayerClone").Type, 0, 0f, 0f, 0f, 0f, 255);
				NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y - 1000, Mod.Find<ModNPC>("MindFlayerClone").Type, 0, 0f, 0f, 0f, 0f, 255);
			}
			timer = 100;
			NPC.ai[0] = 0f;
			TeleportTimer = 0;
			TeleportNum = 0;
			HentaiFace = false;
		}
		return true;
	}

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
		npcLoot.Add(ItemDropRule.ByCondition(new Conditions.IsExpert(), ModContent.ItemType<FlayerBrain>()));
		npcLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<FlayerBlade>(), ModContent.ItemType<FlayerStaff>(), ModContent.ItemType<FlayerBow>()));
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MindFlayerMask>(), 7));
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<FlayerTrophyItem>(), 10));
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkMatter>(), 1, 5, 11));
		npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<EldritchBlood>(), 1, 15, 24));
    }

	public override void OnKill()
	{
		if (ShadowEventWorld.ShadowEventActive)
		{
			ShadowEventWorld.Phase2 = true;
			string text = Ultranium.GetTextValue("Status.ShadowEventPhase2");
			if (Main.netMode == NetmodeID.SinglePlayer)
			{
				Main.NewText(text, (byte)61, byte.MaxValue, (byte)142);
			}
			if (Main.netMode == NetmodeID.Server)
			{
				NetMessage.SendData(MessageID.ChatText, -1, -1, NetworkText.FromLiteral(text), 255, 175f, 75f, 255f);
			}
		}
	}

	public override void BossLoot(ref int potionType)
	{
		potionType = 3544;
	}
}
