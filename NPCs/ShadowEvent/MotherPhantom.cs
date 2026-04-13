using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Items.Eldritch;
using Ultranium.Items.Shade;

namespace Ultranium.NPCs.ShadowEvent;

public class MotherPhantom : ModNPC
{
	private int timer;

	private int moveSpeed;

	private int moveSpeedY;

	private float HomeY = 130f;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Mother Phantom");
		Main.npcFrameCount[NPC.type] = 5;
		NPCID.Sets.TrailCacheLength[NPC.type] = 10;
		NPCID.Sets.TrailingMode[NPC.type] = 0;
	}

	public override void SetDefaults()
	{
		NPC.scale = 1f;
		NPC.width = 244;
		NPC.height = 190;
		NPC.damage = 85;
		NPC.lifeMax = 28500;
		NPC.knockBackResist = 0f;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.HitSound = new SoundStyle("Ultranium/Sounds/PhantomHit");
		NPC.DeathSound = new SoundStyle("Ultranium/Sounds/MotherPhantomDeath") with { PitchVariance = 0.5f };
		NPC.defense = 35;
		NPC.npcSlots = 1f;
		NPC.lavaImmune = true;
		NPC.noGravity = true;
		NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("MotherPhantomBanner").Type;
		for (int i = 0; i < 206; i++)
		{
			NPC.buffImmune[i] = true;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		NPC.lifeMax = 35000;
		NPC.damage = 120;
		NPC.defense = 50;
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		if (NPC.velocity != Vector2.Zero)
		{
			Vector2 vector = new Vector2((float)ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowEvent/MotherPhantomTrail").Width() * 0.5f, (float)NPC.height * 0.5f);
			for (int i = 0; i < NPC.oldPos.Length; i++)
			{
				SpriteEffects effects = ((NPC.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				Vector2 position = NPC.oldPos[i] - Main.screenPosition + vector + new Vector2(0f, NPC.gfxOffY);
				Color color = NPC.GetAlpha(drawColor) * ((float)(NPC.oldPos.Length - i) / (float)NPC.oldPos.Length / 2f);
				spriteBatch.Draw(ModContent.Request<Texture2D>("Ultranium/NPCs/ShadowEvent/MotherPhantomTrail").Value, position, NPC.frame, color, NPC.rotation, vector, NPC.scale, effects, 0f);
			}
		}
		return true;
	}

	public override bool CheckDead()
	{
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("MotherPhantomGore1").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("MotherPhantomGore2").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("MotherPhantomGore3").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("MotherPhantomGore4").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("MotherPhantomGore5").Type);
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
	{
		target.AddBuff(Mod.Find<ModBuff>("DarkDebuff").Type, 300);
	}

	public override void AI()
	{
		NPC.rotation = NPC.velocity.X * 0.02f;
		Player player = Main.player[NPC.target];
		bool expertMode = Main.expertMode;
		NPC.netUpdate = true;
		NPC.TargetClosest();
		int num = (expertMode ? 38 : 42);
		timer++;
		if (timer == 120)
		{
			for (int i = 0; i < 50; i++)
			{
				int num2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemEmerald);
				Main.dust[num2].scale = 1.5f;
			}
			NPC.position.X = player.position.X - 100f;
			NPC.position.Y = player.position.Y - 400f;
			for (int j = 0; j < 50; j++)
			{
				int num3 = Dust.NewDust(NPC.position, NPC.width, NPC.height, DustID.GemEmerald);
				Main.dust[num3].scale = 1.5f;
			}
			NPC.ai[0] = 1f;
		}
		if (timer == 180 || timer == 210 || timer == 240 || timer == 270 || timer == 300)
		{
			float num4 = 8f;
			float num5 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num5) * (double)num4 * -1.0), (float)(Math.Sin(num5) * (double)num4 * -1.0), Mod.Find<ModProjectile>("MotherPhantomBolt").Type, 40, 0f, 0, 0f, 0f);
		}
		if (timer >= 360 && timer <= 540)
		{
			NPC.ai[0] = 2f;
		}
		if (timer == 540)
		{
			for (int k = 0; k < 50; k++)
			{
				int num6 = Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("ShadowDustPurple").Type);
				Main.dust[num6].scale = 1.5f;
			}
			NPC.position.X = player.position.X - 100f;
			NPC.position.Y = player.position.Y - 400f;
			for (int l = 0; l < 50; l++)
			{
				int num7 = Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("ShadowDustPurple").Type);
				Main.dust[num7].scale = 1.5f;
			}
			NPC.ai[0] = 0f;
		}
		if (timer == 600 || timer == 630)
		{
			Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
			vector.Normalize();
			vector.X *= 5f;
			vector.Y *= 5f;
			int num8 = 5;
			for (int m = 0; m < num8; m++)
			{
				float num9 = (float)Main.rand.Next(-300, 300) * 0.01f;
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, vector.X + num9, vector.Y + num9, Mod.Find<ModProjectile>("PhantomWave").Type, num, 1f, Main.myPlayer, 0f, 0f);
			}
		}
		if (timer == 660)
		{
			for (int n = 0; n < 50; n++)
			{
				int num10 = Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("ShadowDustPurple").Type);
				Main.dust[num10].scale = 1.5f;
			}
			NPC.position.X = player.position.X - 100f;
			NPC.position.Y = player.position.Y - 400f;
			for (int num11 = 0; num11 < 50; num11++)
			{
				int num12 = Dust.NewDust(NPC.position, NPC.width, NPC.height, Mod.Find<ModDust>("ShadowDustPurple").Type);
				Main.dust[num12].scale = 1.5f;
			}
		}
		if ((timer == 720 || timer == 760) && NPC.CountNPCS(Mod.Find<ModNPC>("Phantom").Type) < 2)
		{
			NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("Phantom").Type, 0, 0f, 0f, 0f, 0f, 255);
		}
		if (timer == 770)
		{
			timer = 0;
		}
		if (NPC.ai[0] == 0f)
		{
			NPC.velocity.X = (NPC.velocity.Y = 0f);
		}
		if (NPC.ai[0] == 1f)
		{
			if (NPC.Center.X >= player.Center.X && moveSpeed >= -50)
			{
				moveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && moveSpeed <= 50)
			{
				moveSpeed++;
			}
			NPC.velocity.X = (float)moveSpeed * 0.1f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -50)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 50)
			{
				moveSpeedY++;
			}
			NPC.velocity.Y = (float)moveSpeedY * 0.12f;
		}
		if (NPC.ai[0] == 2f)
		{
			if (NPC.Center.X >= player.Center.X && moveSpeed >= -100)
			{
				moveSpeed--;
			}
			else if (NPC.Center.X <= player.Center.X && moveSpeed <= 100)
			{
				moveSpeed++;
			}
			NPC.velocity.X = (float)moveSpeed * 0.1f;
			if (NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -100)
			{
				moveSpeedY--;
				HomeY = 150f;
			}
			else if (NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 100)
			{
				moveSpeedY++;
			}
			NPC.velocity.Y = (float)moveSpeedY * 0.12f;
		}
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

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DarkMatter>(), 1, 3, 7));
    }
}
