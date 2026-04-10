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
		// ((ModNPC)this).DisplayName.SetDefault("Dread");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 174;
		((ModNPC)this).NPC.height = 190;
		((ModNPC)this).NPC.scale = 1.2f;
		((ModNPC)this).NPC.lifeMax = 14000;
		((ModNPC)this).NPC.damage = 55;
		((ModNPC)this).NPC.defense = 0;
		((ModNPC)this).NPC.knockBackResist = 0f;
		((ModNPC)this).NPC.lavaImmune = true;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.netAlways = true;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit7;
		((ModNPC)this).NPC.npcSlots = 1f;
		base.Music = ((ModNPC)this).Mod.GetSoundSlot((SoundType)51, "Sounds/Music/Dread");
		((ModNPC)this).NPC.aiStyle = -1;
		players = 1;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = ((ModNPC)this).NPC.rotation;
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
	{
		players = numPlayers;
		((ModNPC)this).NPC.lifeMax = 17500 + numPlayers * 1750;
		((ModNPC)this).NPC.damage = 68;
	}

	public override void AI()
	{
		Player player = Main.player[((ModNPC)this).NPC.target];
		if (Main.player[((ModNPC)this).NPC.target].dead || Main.dayTime)
		{
			((ModNPC)this).NPC.velocity.Y = 10f;
			((ModNPC)this).NPC.ai[3] += 1f;
			if (((ModNPC)this).NPC.ai[3] >= 120f)
			{
				((Entity)((ModNPC)this).NPC).active = false;
			}
		}
		((ModNPC)this).NPC.TargetClosest();
		if (Main.netMode != 1)
		{
			int num = 6000;
			if (Math.Abs(((ModNPC)this).NPC.Center.X - Main.player[((ModNPC)this).NPC.target].Center.X) + Math.Abs(((ModNPC)this).NPC.Center.Y - Main.player[((ModNPC)this).NPC.target].Center.Y) > (float)num)
			{
				((Entity)((ModNPC)this).NPC).active = false;
				((ModNPC)this).NPC.life = 0;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(23, -1, -1, null, ((ModNPC)this).NPC.whoAmI);
				}
			}
		}
		((ModNPC)this).NPC.rotation = (float)Math.Atan2(((ModNPC)this).NPC.velocity.Y, ((ModNPC)this).NPC.velocity.X) + 4.71f;
		if (((ModNPC)this).NPC.localAI[0] == 0f && Main.netMode != 1)
		{
			((ModNPC)this).NPC.localAI[0] = 1f;
			NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y, ((ModNPC)this).Mod.Find<ModNPC>("DreadHook").Type, ((ModNPC)this).NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y, ((ModNPC)this).Mod.Find<ModNPC>("DreadHook").Type, ((ModNPC)this).NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y, ((ModNPC)this).Mod.Find<ModNPC>("DreadHook").Type, ((ModNPC)this).NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y, ((ModNPC)this).Mod.Find<ModNPC>("DreadHook").Type, ((ModNPC)this).NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
		}
		int[] array = new int[3];
		float num2 = 0f;
		float num3 = 0f;
		int num4 = 0;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (((Entity)Main.npc[i]).active && Main.npc[i].type == ((ModNPC)this).Mod.Find<ModNPC>("DreadHook").Type)
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
		float num8 = Main.player[((ModNPC)this).NPC.target].Center.X - vector.X;
		float num9 = Main.player[((ModNPC)this).NPC.target].Center.Y - vector.Y;
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
		vector = new Vector2(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y);
		num8 = num2 - vector.X;
		num9 = num3 - vector.Y;
		num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
		if (num10 < num5)
		{
			num8 = ((ModNPC)this).NPC.velocity.X;
			num9 = ((ModNPC)this).NPC.velocity.Y;
		}
		else
		{
			num10 = num5 / num10;
			num8 *= num10 * num7;
			num9 *= num10 * num7;
		}
		if (((ModNPC)this).NPC.velocity.X < num8)
		{
			((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + num6;
			if (((ModNPC)this).NPC.velocity.X < 0f && num8 > 0f)
			{
				((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + num6 * 2f;
			}
		}
		else if (((ModNPC)this).NPC.velocity.X > num8)
		{
			((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - num6;
			if (((ModNPC)this).NPC.velocity.X > 0f && num8 < 0f)
			{
				((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - num6 * 2f;
			}
		}
		if (((ModNPC)this).NPC.velocity.Y < num9)
		{
			((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + num6;
			if (((ModNPC)this).NPC.velocity.Y < 0f && num9 > 0f)
			{
				((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + num6 * 2f;
			}
		}
		else if (((ModNPC)this).NPC.velocity.Y > num9)
		{
			((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - num6;
			if (((ModNPC)this).NPC.velocity.Y > 0f && num9 < 0f)
			{
				((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - num6 * 2f;
			}
		}
		Vector2 vector2 = new Vector2(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y);
		float num12 = Main.player[((ModNPC)this).NPC.target].Center.X - vector2.X;
		float num13 = Main.player[((ModNPC)this).NPC.target].Center.Y - vector2.Y;
		((ModNPC)this).NPC.rotation = (float)Math.Atan2(num13, num12) + 4.71f;
		_ = Main.expertMode;
		timer++;
		if (timer == 60 || timer == 90 || timer == 120 || timer == 180 || timer == 210 || timer == 240 || timer == 300 || timer == 330 || timer == 360)
		{
			float num14 = 7f;
			int num15 = ((ModNPC)this).Mod.Find<ModProjectile>("DreadBolt").Type;
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y));
			float num16 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num16) * (double)num14 * -1.0), (float)(Math.Sin(num16) * (double)num14 * -1.0), num15, 30, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 440 || timer == 500 || timer == 560)
		{
			float num17 = 5f;
			int num18 = ((ModNPC)this).Mod.Find<ModProjectile>("ToothBall").Type;
			SoundEngine.PlaySound(SoundID.NPCDeath13, ((ModNPC)this).NPC.position);
			float num19 = (float)Math.Atan2(((ModNPC)this).NPC.Center.Y - player.Center.Y, ((ModNPC)this).NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, (float)(Math.Cos(num19) * (double)num17 * -1.0), (float)(Math.Sin(num19) * (double)num17 * -1.0), num18, 30, 0f, Main.myPlayer, 0f, 0f);
		}
		if (timer == 640 || timer == 700 || timer == 760)
		{
			int num20 = 2;
			for (int j = 0; j < num20; j++)
			{
				float num21 = (float)Main.rand.Next(-200, 200) * 0.02f;
				float num22 = (float)Main.rand.Next(-200, 200) * 0.02f;
				Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, num21, num22, ((ModNPC)this).Mod.Find<ModProjectile>("DreadSpit").Type, 30, 1f, ((ModNPC)this).NPC.target, 0f, 0f);
			}
		}
		if (timer == 850 || timer == 970 || timer == 1090)
		{
			Vector2 vector3 = new Vector2(((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width * 0.5f, ((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height * 0.5f);
			float num23 = (float)Math.Atan2(vector3.Y - (Main.player[((ModNPC)this).NPC.target].position.Y + (float)Main.player[((ModNPC)this).NPC.target].height * 0.5f), vector3.X - (Main.player[((ModNPC)this).NPC.target].position.X + (float)Main.player[((ModNPC)this).NPC.target].width * 0.5f));
			((ModNPC)this).NPC.velocity.X = (float)(Math.Cos(num23) * 14.0) * -1f;
			((ModNPC)this).NPC.velocity.Y = (float)(Math.Sin(num23) * 14.0) * -1f;
			int num24 = 45;
			for (int k = 0; k < num24; k++)
			{
				Vector2 vector4 = (Vector2.One * new Vector2((float)((ModNPC)this).NPC.width / 7f, (float)((ModNPC)this).NPC.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(k - (num24 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num24) + ((ModNPC)this).NPC.Center;
				Vector2 vector5 = vector4 - ((ModNPC)this).NPC.Center;
				Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, 90, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
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
		if (!NPC.AnyNPCs(((ModNPC)this).Mod.Find<ModNPC>("DreadHook").Type))
		{
			((Entity)((ModNPC)this).NPC).active = false;
			NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y + 100, ((ModNPC)this).Mod.Find<ModNPC>("DreadBossP2").Type, ((ModNPC)this).NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
		}
		if (NPC.AnyNPCs(((ModNPC)this).Mod.Find<ModNPC>("DreadHook").Type))
		{
			((ModNPC)this).NPC.immortal = true;
			((ModNPC)this).NPC.dontTakeDamage = true;
		}
	}

	public override void OnKill()
	{
		NPC.NewNPC((int)((ModNPC)this).NPC.Center.X, (int)((ModNPC)this).NPC.Center.Y + 100, ((ModNPC)this).Mod.Find<ModNPC>("DreadBossP2").Type, ((ModNPC)this).NPC.whoAmI, 0f, 0f, 0f, 0f, 255);
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter >= 9.0)
		{
			((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
			((ModNPC)this).NPC.frameCounter = 1.0;
		}
	}
}
