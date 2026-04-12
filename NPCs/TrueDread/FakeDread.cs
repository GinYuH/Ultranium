using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.TrueDread;

[AutoloadBossHead]
public class FakeDread : ModNPC
{
	public int changeLocationTimer;

	public float vectorX;

	public float vectorY;

	public int timer;

	public int TransitionTimer;

	public static int _type;

	public int players;

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Dread???");
		Main.npcFrameCount[NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		NPC.width = 174;
		NPC.height = 190;
		NPC.scale = 1.2f;
		NPC.lifeMax = 220000;
		NPC.damage = 80;
		NPC.defense = 75;
		NPC.knockBackResist = 0f;
		NPC.lavaImmune = true;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.netAlways = true;
		NPC.HitSound = SoundID.NPCHit7;
		NPC.npcSlots = 1f;
		base.Music = MusicLoader.GetMusicSlot(Mod,"Sounds/Music/Dread");
		NPC.aiStyle = -1;
		players = 1;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = NPC.rotation;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		NPC.lifeMax = 220000 + numPlayers * 22000;
		NPC.damage = 120;
	}

	public override void AI()
	{
		Player player = Main.player[NPC.target];
		if (Main.player[NPC.target].dead || Main.dayTime)
		{
			NPC.velocity.Y = 10f;
			NPC.ai[3] += 1f;
			if (NPC.ai[3] >= 120f)
			{
				((Entity)NPC).active = false;
			}
		}
		NPC.TargetClosest();
		if (Main.netMode != 1)
		{
			int num = 6000;
			if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)num)
			{
				((Entity)NPC).active = false;
				NPC.life = 0;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(23, -1, -1, null, NPC.whoAmI);
				}
			}
		}
		NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 4.71f;
		if (NPC.localAI[0] == 0f && Main.netMode != 1)
		{
			NPC.localAI[0] = 1f;
			NPC.NewNPC(null, (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("FakeDreadHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC(null, (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("FakeDreadHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC(null, (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("FakeDreadHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC(null, (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("FakeDreadHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
		}
		int[] array = new int[3];
		float num2 = 0f;
		float num3 = 0f;
		int num4 = 0;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (((Entity)Main.npc[i]).active && Main.npc[i].type == Mod.Find<ModNPC>("FakeDreadHook").Type)
			{
				num2 += Main.npc[i].Center.X;
				num3 += Main.npc[i].Center.Y;
				array[num4] = i;
				num4++;
				if (num4 > 2)
				{
					break;
				}
			}
		}
		num2 /= (float)num4;
		num3 /= (float)num4;
		float num5 = 2.5f;
		float num6 = 0.08f;
		float num7 = 2f;
		Vector2 vector = new Vector2(num2, num3);
		float num8 = Main.player[NPC.target].Center.X - vector.X;
		float num9 = Main.player[NPC.target].Center.Y - vector.Y;
		if (!((Entity)player).active && Main.dayTime)
		{
			num9 *= -1f;
			num8 *= -1f;
			num5 += 8f;
		}
		float num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
		int num11 = 500;
		if (num10 >= (float)num11)
		{
			num10 = (float)num11 / num10;
			num8 *= num10;
			num9 *= num10;
		}
		num2 += num8;
		num3 += num9;
		vector = new Vector2(NPC.Center.X, NPC.Center.Y);
		num8 = num2 - vector.X;
		num9 = num3 - vector.Y;
		num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
		if (num10 < num5)
		{
			num8 = NPC.velocity.X;
			num9 = NPC.velocity.Y;
		}
		else
		{
			num10 = num5 / num10;
			num8 *= num10 * num7;
			num9 *= num10 * num7;
		}
		if (NPC.velocity.X < num8)
		{
			NPC.velocity.X = NPC.velocity.X + num6;
			if (NPC.velocity.X < 0f && num8 > 0f)
			{
				NPC.velocity.X = NPC.velocity.X + num6 * 2f;
			}
		}
		else if (NPC.velocity.X > num8)
		{
			NPC.velocity.X = NPC.velocity.X - num6;
			if (NPC.velocity.X > 0f && num8 < 0f)
			{
				NPC.velocity.X = NPC.velocity.X - num6 * 2f;
			}
		}
		if (NPC.velocity.Y < num9)
		{
			NPC.velocity.Y = NPC.velocity.Y + num6;
			if (NPC.velocity.Y < 0f && num9 > 0f)
			{
				NPC.velocity.Y = NPC.velocity.Y + num6 * 2f;
			}
		}
		else if (NPC.velocity.Y > num9)
		{
			NPC.velocity.Y = NPC.velocity.Y - num6;
			if (NPC.velocity.Y > 0f && num9 < 0f)
			{
				NPC.velocity.Y = NPC.velocity.Y - num6 * 2f;
			}
		}
		Vector2 vector2 = new Vector2(NPC.Center.X, NPC.Center.Y);
		float num12 = Main.player[NPC.target].Center.X - vector2.X;
		float num13 = Main.player[NPC.target].Center.Y - vector2.Y;
		NPC.rotation = (float)Math.Atan2(num13, num12) + 4.71f;
		int num14 = (Main.expertMode ? 35 : 50);
		if (NPC.AnyNPCs(Mod.Find<ModNPC>("FakeDreadHook").Type))
		{
			timer++;
			if (timer == 60 || timer == 90 || timer == 120 || timer == 150 || timer == 180 || timer == 210 || timer == 240 || timer == 270 || timer == 300 || timer == 330 || timer == 360)
			{
				float num15 = 7f;
				int num16 = Mod.Find<ModProjectile>("DreadBolt").Type;
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				float num17 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num17) * (double)num15 * -1.0), (float)(Math.Sin(num17) * (double)num15 * -1.0), num16, num14, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 440 || timer == 470 || timer == 500 || timer == 530 || timer == 560)
			{
				float num18 = 5f;
				int num19 = Mod.Find<ModProjectile>("ToothBall2").Type;
				SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.position);
				float num20 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
				Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num20) * (double)num18 * -1.0), (float)(Math.Sin(num20) * (double)num18 * -1.0), num19, num14, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 640 || timer == 700 || timer == 760)
			{
				int num21 = 4;
				for (int j = 0; j < num21; j++)
				{
					float num22 = (float)Main.rand.Next(-200, 200) * 0.03f;
					float num23 = (float)Main.rand.Next(-200, 200) * 0.03f;
					Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, num22, num23, Mod.Find<ModProjectile>("DreadSpit").Type, num14, 1f, NPC.target, 0f, 0f);
				}
			}
			if (timer == 850 || timer == 970 || timer == 1090)
			{
				Vector2 vector3 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
				float num24 = (float)Math.Atan2(vector3.Y - (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f), vector3.X - (Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f));
				NPC.velocity.X = (float)(Math.Cos(num24) * 20.0) * -1f;
				NPC.velocity.Y = (float)(Math.Sin(num24) * 20.0) * -1f;
				NPC.ai[0] %= (float)Math.PI * 2f;
				new Vector2((float)Math.Cos(NPC.ai[0]), (float)Math.Sin(NPC.ai[0]));
				SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
				NPC.ai[2] = -300f;
				Color newColor = default(Color);
				Rectangle rectangle = new Rectangle((int)NPC.position.X, (int)(NPC.position.Y + (float)((NPC.height - NPC.width) / 2)), NPC.width, NPC.width);
				int num25 = 30;
				for (int k = 1; k <= num25; k++)
				{
					int num26 = Dust.NewDust(NPC.position, rectangle.Width, rectangle.Height, 90, 0f, 0f, 100, newColor, 2.5f);
					Main.dust[num26].noGravity = false;
				}
			}
		}
		if (timer >= 1210)
		{
			timer = 0;
		}
		if (!NPC.AnyNPCs(Mod.Find<ModNPC>("FakeDreadHook").Type))
		{
			NPC.velocity *= 0f;
			TransitionTimer++;
			if (TransitionTimer > 60)
			{
				int num27 = 36;
				for (int l = 0; l < num27; l++)
				{
					Vector2 vector4 = (Vector2.One * new Vector2((float)NPC.width / 7f, (float)NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(l - (num27 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num27) + NPC.Center;
					Vector2 vector5 = vector4 - NPC.Center;
					Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, 90, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
					obj.noGravity = true;
					obj.noLight = false;
					obj.velocity = Vector2.Normalize(vector5) * 3f;
					obj.fadeIn = 1.3f;
				}
				NPC.Center = new Vector2(NPC.Center.X, NPC.Center.Y);
				NPC.Center += Main.rand.NextVector2Square(-2f, 2f);
			}
			if (TransitionTimer == 300)
			{
				NPC.NewNPC(null, (int)NPC.Center.X, (int)NPC.Center.Y + 100, Mod.Find<ModNPC>("TrueDread").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
				Ultranium.seizureAmount = 20f;
				((Entity)NPC).active = false;
			}
		}
		if (NPC.AnyNPCs(Mod.Find<ModNPC>("FakeDreadHook").Type))
		{
			NPC.immortal = true;
			NPC.dontTakeDamage = true;
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter >= 9.0)
		{
			NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
			NPC.frameCounter = 1.0;
		}
	}
}
