using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Dread;

[AutoloadBossHead]
public class DreadBoss : ModNPC
{
	public int changeLocationTimer;

	public float vectorX;

	public float vectorY;

	public int timer;

	public static int _type;

	public int players;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dread");
		Main.npcFrameCount[NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		NPC.width = 174;
		NPC.height = 190;
		NPC.scale = 1.2f;
		NPC.lifeMax = 14000;
		NPC.damage = 55;
		NPC.defense = 0;
		NPC.knockBackResist = 0f;
		NPC.lavaImmune = true;
		NPC.noGravity = true;
		NPC.noTileCollide = true;
		NPC.netAlways = true;
		NPC.HitSound = SoundID.NPCHit7;
		NPC.npcSlots = 1f;
		base.Music = MusicLoader.GetMusicSlot(Mod, "Sounds/Music/Dread");
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
		NPC.lifeMax = 17500 + numPlayers * 1750;
		NPC.damage = 68;
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
		if (Main.netMode != NetmodeID.MultiplayerClient)
		{
			int num = 6000;
			if (Math.Abs(NPC.Center.X - Main.player[NPC.target].Center.X) + Math.Abs(NPC.Center.Y - Main.player[NPC.target].Center.Y) > (float)num)
			{
				((Entity)NPC).active = false;
				NPC.life = 0;
				if (Main.netMode == NetmodeID.Server)
				{
					NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, NPC.whoAmI);
				}
			}
		}
		NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 4.71f;
		if (NPC.localAI[0] == 0f && Main.netMode != NetmodeID.MultiplayerClient)
		{
			NPC.localAI[0] = 1f;
			NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("DreadHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("DreadHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("DreadHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, Mod.Find<ModNPC>("DreadHook").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
		}
		int[] array = new int[3];
		float num2 = 0f;
		float num3 = 0f;
		int num4 = 0;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (((Entity)Main.npc[i]).active && Main.npc[i].type == Mod.Find<ModNPC>("DreadHook").Type)
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
		float num6 = 0.05f;
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
		_ = Main.expertMode;
		timer++;
		if (timer == 60 || timer == 90 || timer == 120 || timer == 180 || timer == 210 || timer == 240 || timer == 300 || timer == 330 || timer == 360)
		{
			float num14 = 7f;
			int num15 = Mod.Find<ModProjectile>("DreadBolt").Type;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
			float num16 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num16) * (double)num14 * -1.0), (float)(Math.Sin(num16) * (double)num14 * -1.0), num15, 30, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 440 || timer == 500 || timer == 560)
		{
			float num17 = 5f;
			int num18 = Mod.Find<ModProjectile>("ToothBall").Type;
			SoundEngine.PlaySound(SoundID.NPCDeath13, NPC.position);
			float num19 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num19) * (double)num17 * -1.0), (float)(Math.Sin(num19) * (double)num17 * -1.0), num18, 30, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 640 || timer == 700 || timer == 760)
		{
			int num20 = 2;
			for (int j = 0; j < num20; j++)
			{
				float num21 = (float)Main.rand.Next(-200, 200) * 0.02f;
				float num22 = (float)Main.rand.Next(-200, 200) * 0.02f;
				Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, num21, num22, Mod.Find<ModProjectile>("DreadSpit").Type, 30, 1f, NPC.target, 0f, 0f);
			}
		}
		if (timer == 850 || timer == 970 || timer == 1090)
		{
			Vector2 vector3 = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
			float num23 = (float)Math.Atan2(vector3.Y - (Main.player[NPC.target].position.Y + (float)Main.player[NPC.target].height * 0.5f), vector3.X - (Main.player[NPC.target].position.X + (float)Main.player[NPC.target].width * 0.5f));
			NPC.velocity.X = (float)(Math.Cos(num23) * 14.0) * -1f;
			NPC.velocity.Y = (float)(Math.Sin(num23) * 14.0) * -1f;
			int num24 = 45;
			for (int k = 0; k < num24; k++)
			{
				Vector2 vector4 = (Vector2.One * new Vector2((float)NPC.width / 7f, (float)NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(k - (num24 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num24) + NPC.Center;
				Vector2 vector5 = vector4 - NPC.Center;
				Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, DustID.GemRuby, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
				obj.noGravity = true;
				obj.noLight = false;
				obj.velocity = Vector2.Normalize(vector5) * 3f;
				obj.fadeIn = 1.3f;
			}
		}
		if (timer >= 1210)
		{
			timer = 0;
		}
		if (!NPC.AnyNPCs(Mod.Find<ModNPC>("DreadHook").Type))
		{
			((Entity)NPC).active = false;
			NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y + 100, Mod.Find<ModNPC>("DreadBossP2").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
		}
		if (NPC.AnyNPCs(Mod.Find<ModNPC>("DreadHook").Type))
		{
			NPC.immortal = true;
			NPC.dontTakeDamage = true;
		}
	}

	public override void OnKill()
	{
		NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y + 100, Mod.Find<ModNPC>("DreadBossP2").Type, NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
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
