using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Dread;

public class DreadChaser : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 2;
		// ((ModNPC)this).DisplayName.SetDefault("Dread Chaser");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 48;
		((ModNPC)this).NPC.height = 48;
		((ModNPC)this).NPC.aiStyle = -1;
		((ModNPC)this).NPC.damage = 25;
		((ModNPC)this).NPC.defense = 30;
		((ModNPC)this).NPC.lifeMax = 175;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit1;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).NPC.knockBackResist = 0f;
		Main.npcFrameCount[((ModNPC)this).NPC.type] = 2;
		((ModNPC)this).NPC.lavaImmune = true;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("DreadChaserBanner").Type;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.noTileCollide = false;
		((ModNPC)this).NPC.buffImmune[189] = true;
		((ModNPC)this).NPC.buffImmune[39] = true;
		((ModNPC)this).NPC.buffImmune[24] = true;
		((ModNPC)this).NPC.buffImmune[46] = true;
		((ModNPC)this).NPC.buffImmune[47] = true;
		((ModNPC)this).NPC.buffImmune[70] = true;
		((ModNPC)this).NPC.buffImmune[186] = true;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.Player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.SpawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.SpawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.Player.ZoneSkyHeight || !Main.hardMode || UltraniumWorld.downedDread)
			{
				return 0f;
			}
			return 0.06f;
		}
		return 0f;
	}

	public override void FindFrame(int frameHeight)
	{
		((ModNPC)this).NPC.frameCounter += 1.0;
		if (((ModNPC)this).NPC.frameCounter >= 6.0)
		{
			((ModNPC)this).NPC.frame.Y = (((ModNPC)this).NPC.frame.Y + frameHeight) % (Main.npcFrameCount[((ModNPC)this).NPC.type] * frameHeight);
			((ModNPC)this).NPC.frameCounter = 1.0;
		}
	}

	public override void OnKill()
	{
		Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DreadFlame").Type, Main.rand.Next(1, 3), false, 0, false, false);
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (((ModNPC)this).NPC.life > 0)
		{
			return;
		}
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X + (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y + (float)(((ModNPC)this).NPC.height / 2);
		((ModNPC)this).NPC.width = 30;
		((ModNPC)this).NPC.height = 30;
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X - (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y - (float)(((ModNPC)this).NPC.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y), ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 90, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override void AI()
	{
		_ = Main.player[((ModNPC)this).NPC.target];
		if (((ModNPC)this).NPC.target < 0 || ((ModNPC)this).NPC.target == 255 || Main.player[((ModNPC)this).NPC.target].dead)
		{
			((ModNPC)this).NPC.TargetClosest();
		}
		float num = 8f;
		float num2 = 0.05f;
		Vector2 vector = new Vector2(((ModNPC)this).NPC.position.X + (float)((ModNPC)this).NPC.width * 0.5f, ((ModNPC)this).NPC.position.Y + (float)((ModNPC)this).NPC.height * 0.5f);
		float num3 = Main.player[((ModNPC)this).NPC.target].position.X + (float)(Main.player[((ModNPC)this).NPC.target].width / 2);
		float num4 = Main.player[((ModNPC)this).NPC.target].position.Y + (float)(Main.player[((ModNPC)this).NPC.target].height / 2);
		num3 = (int)(num3 / 8f) * 8;
		num4 = (int)(num4 / 8f) * 8;
		vector.X = (int)(vector.X / 8f) * 8;
		vector.Y = (int)(vector.Y / 8f) * 8;
		num3 -= vector.X;
		num4 -= vector.Y;
		float num5 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
		float num6 = num5;
		bool flag = false;
		if (num5 > 600f)
		{
			flag = true;
		}
		if (num5 == 0f)
		{
			num3 = ((ModNPC)this).NPC.velocity.X;
			num4 = ((ModNPC)this).NPC.velocity.Y;
		}
		else
		{
			num5 = num / num5;
			num3 *= num5;
			num4 *= num5;
		}
		if (num6 > 100f)
		{
			((ModNPC)this).NPC.ai[0] += 1f;
			if (((ModNPC)this).NPC.ai[0] > 0f)
			{
				((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + 0.012f;
			}
			else
			{
				((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - 0.012f;
			}
			if (((ModNPC)this).NPC.ai[0] < -100f || ((ModNPC)this).NPC.ai[0] > 100f)
			{
				((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + 0.012f;
			}
			else
			{
				((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - 0.012f;
			}
			if (((ModNPC)this).NPC.ai[0] > 200f)
			{
				((ModNPC)this).NPC.ai[0] = -200f;
			}
		}
		if (Main.player[((ModNPC)this).NPC.target].dead)
		{
			num3 = (float)((ModNPC)this).NPC.direction * num / 2f;
			num4 = (0f - num) / 2f;
		}
		if (((ModNPC)this).NPC.velocity.X < num3)
		{
			((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X + num2;
		}
		else if (((ModNPC)this).NPC.velocity.X > num3)
		{
			((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X - num2;
		}
		if (((ModNPC)this).NPC.velocity.Y < num4)
		{
			((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y + num2;
		}
		else if (((ModNPC)this).NPC.velocity.Y > num4)
		{
			((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.velocity.Y - num2;
		}
		int num7 = (int)((ModNPC)this).NPC.position.X + ((ModNPC)this).NPC.width / 2;
		int num8 = (int)((ModNPC)this).NPC.position.Y + ((ModNPC)this).NPC.height / 2;
		num7 /= 16;
		_ = num8 / 16;
		if (num3 > 0f)
		{
			((ModNPC)this).NPC.spriteDirection = 1;
			((ModNPC)this).NPC.rotation = (float)Math.Atan2(num4, num3) + MathHelper.ToRadians(-90f);
		}
		if (num3 < 0f)
		{
			((ModNPC)this).NPC.spriteDirection = -1;
			((ModNPC)this).NPC.rotation = (float)Math.Atan2(num4, num3) + 3.14f + MathHelper.ToRadians(90f);
		}
		float num9 = 0.7f;
		if (((ModNPC)this).NPC.collideX)
		{
			((ModNPC)this).NPC.netUpdate = true;
			((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.oldVelocity.X * (0f - num9);
			if (((ModNPC)this).NPC.direction == -1 && ((ModNPC)this).NPC.velocity.X > 0f && ((ModNPC)this).NPC.velocity.X < 2f)
			{
				((ModNPC)this).NPC.velocity.X = 2f;
			}
			if (((ModNPC)this).NPC.direction == 1 && ((ModNPC)this).NPC.velocity.X < 0f && ((ModNPC)this).NPC.velocity.X > -2f)
			{
				((ModNPC)this).NPC.velocity.X = -2f;
			}
		}
		if (((ModNPC)this).NPC.collideY)
		{
			((ModNPC)this).NPC.netUpdate = true;
			((ModNPC)this).NPC.velocity.Y = ((ModNPC)this).NPC.oldVelocity.Y * (0f - num9);
			if (((ModNPC)this).NPC.velocity.Y > 0f && (double)((ModNPC)this).NPC.velocity.Y < 1.5)
			{
				((ModNPC)this).NPC.velocity.Y = 2f;
			}
			if (((ModNPC)this).NPC.velocity.Y < 0f && (double)((ModNPC)this).NPC.velocity.Y > -1.5)
			{
				((ModNPC)this).NPC.velocity.Y = -2f;
			}
		}
		if (flag)
		{
			if ((((ModNPC)this).NPC.velocity.X > 0f && num3 > 0f) || (((ModNPC)this).NPC.velocity.X < 0f && num3 < 0f))
			{
				if (Math.Abs(((ModNPC)this).NPC.velocity.X) < 12f)
				{
					((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X * 1.05f;
				}
			}
			else
			{
				((ModNPC)this).NPC.velocity.X = ((ModNPC)this).NPC.velocity.X * 0.9f;
			}
		}
		if (((((ModNPC)this).NPC.velocity.X > 0f && ((ModNPC)this).NPC.oldVelocity.X < 0f) || (((ModNPC)this).NPC.velocity.X < 0f && ((ModNPC)this).NPC.oldVelocity.X > 0f) || (((ModNPC)this).NPC.velocity.Y > 0f && ((ModNPC)this).NPC.oldVelocity.Y < 0f) || (((ModNPC)this).NPC.velocity.Y < 0f && ((ModNPC)this).NPC.oldVelocity.Y > 0f)) && !((ModNPC)this).NPC.justHit)
		{
			((ModNPC)this).NPC.netUpdate = true;
		}
	}
}
