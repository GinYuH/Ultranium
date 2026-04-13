using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Ultranium.Items.Dread.Materials;
using Ultranium.Items.Shade;

namespace Ultranium.NPCs.Enemy.Dread;

public class DreadChaser : ModNPC
{
	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[NPC.type] = 2;
		//DisplayName.SetDefault("Dread Chaser");
	}

	public override void SetDefaults()
	{
		NPC.width = 48;
		NPC.height = 48;
		NPC.aiStyle = -1;
		NPC.damage = 25;
		NPC.defense = 30;
		NPC.lifeMax = 175;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.knockBackResist = 0f;
		Main.npcFrameCount[NPC.type] = 2;
		NPC.lavaImmune = true;
		Banner = NPC.type;
		BannerItem = Mod.Find<ModItem>("DreadChaserBanner").Type;
		NPC.noGravity = true;
		NPC.noTileCollide = false;
		NPC.buffImmune[189] = true;
		NPC.buffImmune[39] = true;
		NPC.buffImmune[24] = true;
		NPC.buffImmune[46] = true;
		NPC.buffImmune[47] = true;
		NPC.buffImmune[70] = true;
		NPC.buffImmune[186] = true;
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
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter >= 6.0)
		{
			NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
			NPC.frameCounter = 1.0;
		}
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DreadFlame>(), 1, 1, 2));
    }

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life > 0)
		{
			return;
		}
		NPC.position.X = NPC.position.X + (float)(NPC.width / 2);
		NPC.position.Y = NPC.position.Y + (float)(NPC.height / 2);
		NPC.width = 30;
		NPC.height = 30;
		NPC.position.X = NPC.position.X - (float)(NPC.width / 2);
		NPC.position.Y = NPC.position.Y - (float)(NPC.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, DustID.GemRuby, 0f, 0f, 100, default(Color), 2f);
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
		_ = Main.player[NPC.target];
		if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
		{
			NPC.TargetClosest();
		}
		float num = 8f;
		float num2 = 0.05f;
		Vector2 vector = new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);
		float num3 = Main.player[NPC.target].position.X + (float)(Main.player[NPC.target].width / 2);
		float num4 = Main.player[NPC.target].position.Y + (float)(Main.player[NPC.target].height / 2);
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
			num3 = NPC.velocity.X;
			num4 = NPC.velocity.Y;
		}
		else
		{
			num5 = num / num5;
			num3 *= num5;
			num4 *= num5;
		}
		if (num6 > 100f)
		{
			NPC.ai[0] += 1f;
			if (NPC.ai[0] > 0f)
			{
				NPC.velocity.Y = NPC.velocity.Y + 0.012f;
			}
			else
			{
				NPC.velocity.Y = NPC.velocity.Y - 0.012f;
			}
			if (NPC.ai[0] < -100f || NPC.ai[0] > 100f)
			{
				NPC.velocity.X = NPC.velocity.X + 0.012f;
			}
			else
			{
				NPC.velocity.X = NPC.velocity.X - 0.012f;
			}
			if (NPC.ai[0] > 200f)
			{
				NPC.ai[0] = -200f;
			}
		}
		if (Main.player[NPC.target].dead)
		{
			num3 = (float)NPC.direction * num / 2f;
			num4 = (0f - num) / 2f;
		}
		if (NPC.velocity.X < num3)
		{
			NPC.velocity.X = NPC.velocity.X + num2;
		}
		else if (NPC.velocity.X > num3)
		{
			NPC.velocity.X = NPC.velocity.X - num2;
		}
		if (NPC.velocity.Y < num4)
		{
			NPC.velocity.Y = NPC.velocity.Y + num2;
		}
		else if (NPC.velocity.Y > num4)
		{
			NPC.velocity.Y = NPC.velocity.Y - num2;
		}
		int num7 = (int)NPC.position.X + NPC.width / 2;
		int num8 = (int)NPC.position.Y + NPC.height / 2;
		num7 /= 16;
		_ = num8 / 16;
		if (num3 > 0f)
		{
			NPC.spriteDirection = 1;
			NPC.rotation = (float)Math.Atan2(num4, num3) + MathHelper.ToRadians(-90f);
		}
		if (num3 < 0f)
		{
			NPC.spriteDirection = -1;
			NPC.rotation = (float)Math.Atan2(num4, num3) + 3.14f + MathHelper.ToRadians(90f);
		}
		float num9 = 0.7f;
		if (NPC.collideX)
		{
			NPC.netUpdate = true;
			NPC.velocity.X = NPC.oldVelocity.X * (0f - num9);
			if (NPC.direction == -1 && NPC.velocity.X > 0f && NPC.velocity.X < 2f)
			{
				NPC.velocity.X = 2f;
			}
			if (NPC.direction == 1 && NPC.velocity.X < 0f && NPC.velocity.X > -2f)
			{
				NPC.velocity.X = -2f;
			}
		}
		if (NPC.collideY)
		{
			NPC.netUpdate = true;
			NPC.velocity.Y = NPC.oldVelocity.Y * (0f - num9);
			if (NPC.velocity.Y > 0f && (double)NPC.velocity.Y < 1.5)
			{
				NPC.velocity.Y = 2f;
			}
			if (NPC.velocity.Y < 0f && (double)NPC.velocity.Y > -1.5)
			{
				NPC.velocity.Y = -2f;
			}
		}
		if (flag)
		{
			if ((NPC.velocity.X > 0f && num3 > 0f) || (NPC.velocity.X < 0f && num3 < 0f))
			{
				if (Math.Abs(NPC.velocity.X) < 12f)
				{
					NPC.velocity.X = NPC.velocity.X * 1.05f;
				}
			}
			else
			{
				NPC.velocity.X = NPC.velocity.X * 0.9f;
			}
		}
		if (((NPC.velocity.X > 0f && NPC.oldVelocity.X < 0f) || (NPC.velocity.X < 0f && NPC.oldVelocity.X > 0f) || (NPC.velocity.Y > 0f && NPC.oldVelocity.Y < 0f) || (NPC.velocity.Y < 0f && NPC.oldVelocity.Y > 0f)) && !NPC.justHit)
		{
			NPC.netUpdate = true;
		}
	}
}
