using System;
using Microsoft.Xna.Framework;
using Terraria;
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
		((ModNPC)this).DisplayName.SetDefault("Dread???");
		Main.npcFrameCount[((ModNPC)this).npc.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 174;
		((ModNPC)this).npc.height = 190;
		((ModNPC)this).npc.scale = 1.2f;
		((ModNPC)this).npc.lifeMax = 220000;
		((ModNPC)this).npc.damage = 80;
		((ModNPC)this).npc.defense = 75;
		((ModNPC)this).npc.knockBackResist = 0f;
		((ModNPC)this).npc.lavaImmune = true;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.noTileCollide = true;
		((ModNPC)this).npc.netAlways = true;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit7;
		((ModNPC)this).npc.npcSlots = 1f;
		base.music = ((ModNPC)this).mod.GetSoundSlot((SoundType)51, "Sounds/Music/Dread");
		((ModNPC)this).npc.aiStyle = -1;
		players = 1;
	}

	public override void BossHeadRotation(ref float rotation)
	{
		rotation = ((ModNPC)this).npc.rotation;
	}

	public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
	{
		players = numPlayers;
		((ModNPC)this).npc.lifeMax = 220000 + numPlayers * 22000;
		((ModNPC)this).npc.damage = 120;
	}

	public override void AI()
	{
		Player player = Main.player[((ModNPC)this).npc.target];
		if (Main.player[((ModNPC)this).npc.target].dead || Main.dayTime)
		{
			((ModNPC)this).npc.velocity.Y = 10f;
			((ModNPC)this).npc.ai[3] += 1f;
			if (((ModNPC)this).npc.ai[3] >= 120f)
			{
				((Entity)((ModNPC)this).npc).active = false;
			}
		}
		((ModNPC)this).npc.TargetClosest();
		if (Main.netMode != 1)
		{
			int num = 6000;
			if (Math.Abs(((ModNPC)this).npc.Center.X - Main.player[((ModNPC)this).npc.target].Center.X) + Math.Abs(((ModNPC)this).npc.Center.Y - Main.player[((ModNPC)this).npc.target].Center.Y) > (float)num)
			{
				((Entity)((ModNPC)this).npc).active = false;
				((ModNPC)this).npc.life = 0;
				if (Main.netMode == 2)
				{
					NetMessage.SendData(23, -1, -1, null, ((ModNPC)this).npc.whoAmI);
				}
			}
		}
		((ModNPC)this).npc.rotation = (float)Math.Atan2(((ModNPC)this).npc.velocity.Y, ((ModNPC)this).npc.velocity.X) + 4.71f;
		if (((ModNPC)this).npc.localAI[0] == 0f && Main.netMode != 1)
		{
			((ModNPC)this).npc.localAI[0] = 1f;
			NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y, ((ModNPC)this).mod.NPCType("FakeDreadHook"), ((ModNPC)this).npc.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y, ((ModNPC)this).mod.NPCType("FakeDreadHook"), ((ModNPC)this).npc.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y, ((ModNPC)this).mod.NPCType("FakeDreadHook"), ((ModNPC)this).npc.whoAmI, 0f, 0f, 0f, 0f, 255);
			NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y, ((ModNPC)this).mod.NPCType("FakeDreadHook"), ((ModNPC)this).npc.whoAmI, 0f, 0f, 0f, 0f, 255);
		}
		int[] array = new int[3];
		float num2 = 0f;
		float num3 = 0f;
		int num4 = 0;
		for (int i = 0; i < Main.npc.Length; i++)
		{
			if (((Entity)Main.npc[i]).active && Main.npc[i].type == ((ModNPC)this).mod.NPCType("FakeDreadHook"))
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
		float num8 = Main.player[((ModNPC)this).npc.target].Center.X - vector.X;
		float num9 = Main.player[((ModNPC)this).npc.target].Center.Y - vector.Y;
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
		vector = new Vector2(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y);
		num8 = num2 - vector.X;
		num9 = num3 - vector.Y;
		num10 = (float)Math.Sqrt(num8 * num8 + num9 * num9);
		if (num10 < num5)
		{
			num8 = ((ModNPC)this).npc.velocity.X;
			num9 = ((ModNPC)this).npc.velocity.Y;
		}
		else
		{
			num10 = num5 / num10;
			num8 *= num10 * num7;
			num9 *= num10 * num7;
		}
		if (((ModNPC)this).npc.velocity.X < num8)
		{
			((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + num6;
			if (((ModNPC)this).npc.velocity.X < 0f && num8 > 0f)
			{
				((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X + num6 * 2f;
			}
		}
		else if (((ModNPC)this).npc.velocity.X > num8)
		{
			((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - num6;
			if (((ModNPC)this).npc.velocity.X > 0f && num8 < 0f)
			{
				((ModNPC)this).npc.velocity.X = ((ModNPC)this).npc.velocity.X - num6 * 2f;
			}
		}
		if (((ModNPC)this).npc.velocity.Y < num9)
		{
			((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + num6;
			if (((ModNPC)this).npc.velocity.Y < 0f && num9 > 0f)
			{
				((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y + num6 * 2f;
			}
		}
		else if (((ModNPC)this).npc.velocity.Y > num9)
		{
			((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - num6;
			if (((ModNPC)this).npc.velocity.Y > 0f && num9 < 0f)
			{
				((ModNPC)this).npc.velocity.Y = ((ModNPC)this).npc.velocity.Y - num6 * 2f;
			}
		}
		Vector2 vector2 = new Vector2(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y);
		float num12 = Main.player[((ModNPC)this).npc.target].Center.X - vector2.X;
		float num13 = Main.player[((ModNPC)this).npc.target].Center.Y - vector2.Y;
		((ModNPC)this).npc.rotation = (float)Math.Atan2(num13, num12) + 4.71f;
		int num14 = (Main.expertMode ? 35 : 50);
		if (NPC.AnyNPCs(((ModNPC)this).mod.NPCType("FakeDreadHook")))
		{
			timer++;
			if (timer == 60 || timer == 90 || timer == 120 || timer == 150 || timer == 180 || timer == 210 || timer == 240 || timer == 270 || timer == 300 || timer == 330 || timer == 360)
			{
				float num15 = 7f;
				int num16 = ((ModNPC)this).mod.ProjectileType("DreadBolt");
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				float num17 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num17) * (double)num15 * -1.0), (float)(Math.Sin(num17) * (double)num15 * -1.0), num16, num14, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 440 || timer == 470 || timer == 500 || timer == 530 || timer == 560)
			{
				float num18 = 5f;
				int num19 = ((ModNPC)this).mod.ProjectileType("ToothBall2");
				Main.PlaySound(SoundID.NPCDeath13, ((ModNPC)this).npc.position);
				float num20 = (float)Math.Atan2(((ModNPC)this).npc.Center.Y - player.Center.Y, ((ModNPC)this).npc.Center.X - player.Center.X);
				Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, (float)(Math.Cos(num20) * (double)num18 * -1.0), (float)(Math.Sin(num20) * (double)num18 * -1.0), num19, num14, 0f, Main.myPlayer, 0f, 0f);
			}
			if (timer == 640 || timer == 700 || timer == 760)
			{
				int num21 = 4;
				for (int j = 0; j < num21; j++)
				{
					float num22 = (float)Main.rand.Next(-200, 200) * 0.03f;
					float num23 = (float)Main.rand.Next(-200, 200) * 0.03f;
					Projectile.NewProjectile(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y, num22, num23, ((ModNPC)this).mod.ProjectileType("DreadSpit"), num14, 1f, ((ModNPC)this).npc.target, 0f, 0f);
				}
			}
			if (timer == 850 || timer == 970 || timer == 1090)
			{
				Vector2 vector3 = new Vector2(((ModNPC)this).npc.position.X + (float)((ModNPC)this).npc.width * 0.5f, ((ModNPC)this).npc.position.Y + (float)((ModNPC)this).npc.height * 0.5f);
				float num24 = (float)Math.Atan2(vector3.Y - (Main.player[((ModNPC)this).npc.target].position.Y + (float)Main.player[((ModNPC)this).npc.target].height * 0.5f), vector3.X - (Main.player[((ModNPC)this).npc.target].position.X + (float)Main.player[((ModNPC)this).npc.target].width * 0.5f));
				((ModNPC)this).npc.velocity.X = (float)(Math.Cos(num24) * 20.0) * -1f;
				((ModNPC)this).npc.velocity.Y = (float)(Math.Sin(num24) * 20.0) * -1f;
				((ModNPC)this).npc.ai[0] %= (float)Math.PI * 2f;
				new Vector2((float)Math.Cos(((ModNPC)this).npc.ai[0]), (float)Math.Sin(((ModNPC)this).npc.ai[0]));
				Main.PlaySound(2, (int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, 20, 1f, 0f);
				((ModNPC)this).npc.ai[2] = -300f;
				Color newColor = default(Color);
				Rectangle rectangle = new Rectangle((int)((ModNPC)this).npc.position.X, (int)(((ModNPC)this).npc.position.Y + (float)((((ModNPC)this).npc.height - ((ModNPC)this).npc.width) / 2)), ((ModNPC)this).npc.width, ((ModNPC)this).npc.width);
				int num25 = 30;
				for (int k = 1; k <= num25; k++)
				{
					int num26 = Dust.NewDust(((ModNPC)this).npc.position, rectangle.Width, rectangle.Height, 90, 0f, 0f, 100, newColor, 2.5f);
					Main.dust[num26].noGravity = false;
				}
			}
		}
		if (timer >= 1210)
		{
			timer = 0;
		}
		if (!NPC.AnyNPCs(((ModNPC)this).mod.NPCType("FakeDreadHook")))
		{
			((ModNPC)this).npc.velocity *= 0f;
			TransitionTimer++;
			if (TransitionTimer > 60)
			{
				int num27 = 36;
				for (int l = 0; l < num27; l++)
				{
					Vector2 vector4 = (Vector2.One * new Vector2((float)((ModNPC)this).npc.width / 7f, (float)((ModNPC)this).npc.height / 7f) * 0.75f * 0.5f).RotatedBy((float)(l - (num27 / 2 - 1)) * ((float)Math.PI * 2f) / (float)num27) + ((ModNPC)this).npc.Center;
					Vector2 vector5 = vector4 - ((ModNPC)this).npc.Center;
					Dust obj = Main.dust[Dust.NewDust(vector4 + vector5, 0, 0, 90, vector5.X * 2f, vector5.Y * 2f, 100, default(Color), 1.4f)];
					obj.noGravity = true;
					obj.noLight = false;
					obj.velocity = Vector2.Normalize(vector5) * 3f;
					obj.fadeIn = 1.3f;
				}
				((ModNPC)this).npc.Center = new Vector2(((ModNPC)this).npc.Center.X, ((ModNPC)this).npc.Center.Y);
				((ModNPC)this).npc.Center += Main.rand.NextVector2Square(-2f, 2f);
			}
			if (TransitionTimer == 300)
			{
				NPC.NewNPC((int)((ModNPC)this).npc.Center.X, (int)((ModNPC)this).npc.Center.Y + 100, ((ModNPC)this).mod.NPCType("TrueDread"), ((ModNPC)this).npc.whoAmI, 0f, 0f, 0f, 0f, 255);
				Ultranium.seizureAmount = 20f;
				((Entity)((ModNPC)this).npc).active = false;
			}
		}
		if (NPC.AnyNPCs(((ModNPC)this).mod.NPCType("FakeDreadHook")))
		{
			((ModNPC)this).npc.immortal = true;
			((ModNPC)this).npc.dontTakeDamage = true;
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).npc.frameCounter += 1.0;
		if (((ModNPC)this).npc.frameCounter >= 9.0)
		{
			((ModNPC)this).npc.frame.Y = (((ModNPC)this).npc.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).npc.type] * frameHeight);
			((ModNPC)this).npc.frameCounter = 1.0;
		}
	}
}
