using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Ultranium.Items.Shade;

namespace Ultranium.NPCs.Enemy.Shadow;

public class EldritchCaster : ModNPC
{
	public bool Shooting;

	private float Timer
	{
		get
		{
			return NPC.ai[0];
		}
		set
		{
			NPC.ai[0] = value;
		}
	}

	private float TeleportTimer
	{
		get
		{
			return NPC.ai[1];
		}
		set
		{
			NPC.ai[1] = value;
		}
	}

	private float TeleportX
	{
		get
		{
			return NPC.ai[2];
		}
		set
		{
			NPC.ai[2] = value;
		}
	}

	private float TeleportY
	{
		get
		{
			return NPC.ai[3];
		}
		set
		{
			NPC.ai[3] = value;
		}
	}

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Eldritch Caster");
		Main.npcFrameCount[NPC.type] = 2;
	}

	public override void SetDefaults()
	{
		NPC.width = 30;
		NPC.height = 48;
		NPC.damage = 45;
		NPC.defense = 25;
		NPC.lifeMax = 50;
		NPC.knockBackResist = 0f;
		NPC.value = Item.buyPrice(0, 0, 1);
		NPC.HitSound = SoundID.NPCHit2;
		NPC.DeathSound = SoundID.NPCDeath2;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("EldritchCasterBanner").Type;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0)
		{
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("CasterGore1").Type);
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("CasterGore2").Type);
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("CasterGore3").Type);
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.Player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.SpawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.SpawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.Player.GetModPlayer<UltraniumPlayer>().ZoneShadow)
			{
				return 0f;
			}
			return 1f;
		}
		return 0f;
	}

	public override void AI()
	{
		NPC.TargetClosest();
		Player player = Main.player[NPC.target];
		Vector2 vector = player.Center - NPC.Center;
		NPC.spriteDirection = Math.Sign(vector.X);
		NPC.velocity.X = 0f;
		if (Timer > 0f)
		{
			Timer -= 1f;
		}
		Shooting = Timer <= 30f;
		if (Timer <= 0f)
		{
			SoundEngine.PlaySound(SoundID.Item20, new Vector2(NPC.position.X, NPC.position.Y));
			float num = 3f;
			float num2 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num2) * (double)num * -1.0), (float)(Math.Sin(num2) * (double)num * -1.0), Mod.Find<ModProjectile>("CasterBolt").Type, 20, 0f, 0, 0f, 0f);
			Timer = 120f;
		}
		TeleportTimer--;
		if (TeleportTimer <= 0f)
		{
			TeleportTimer = 200f;
			Teleport(player, 0);
		}
	}

	private void Teleport(Player player, int attemptNum)
	{
		int num = (int)player.position.X / 16;
		int num2 = (int)player.position.Y / 16;
		int num3 = (int)NPC.position.X / 16;
		int num4 = (int)NPC.position.Y / 16;
		int num5 = 20;
		bool flag = false;
		int num6 = Main.rand.Next(num - num5, num + num5);
		for (int i = Main.rand.Next(num2 - num5, num2 + num5); i < num2 + num5; i++)
		{
			if ((i < num2 - 4 || i > num2 + 4 || num6 < num - 4 || num6 > num + 4) && (i < num4 - 1 || i > num4 + 1 || num6 < num3 - 1 || num6 > num3 + 1) && Main.tile[num6, i].HasUnactuatedTile)
			{
				bool flag2 = true;
				if ((Main.tile[num6, i - 1].LiquidType == LiquidID.Lava))
				{
					flag2 = false;
				}
				if (flag2 && Main.tileSolid[Main.tile[num6, i].TileType] && !Collision.SolidTiles(num6 - 1, num6 + 1, i - 4, i - 1))
				{
					TeleportX = num6;
					TeleportY = i;
					flag = true;
					break;
				}
			}
		}
		SoundEngine.PlaySound(SoundID.Item8, NPC.position);
		if (TeleportX != 0f && TeleportY != 0f && flag)
		{
			NPC.position.X = (float)((double)TeleportX * 16.0 - (double)(NPC.width / 2) + 8.0);
			NPC.position.Y = TeleportY * 16f - (float)NPC.height;
			NPC.netUpdate = true;
			for (int j = 0; j < 20; j++)
			{
				Dust obj = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, 89)];
				obj.noGravity = true;
				obj.scale = 1f;
				obj.velocity *= 0.1f;
			}
		}
		else if (attemptNum < 10)
		{
			Teleport(player, attemptNum + 1);
		}
	}

	public override void FindFrame(int frameHeight)
	{
		if (!Shooting)
		{
			NPC.frame.Y = 0;
		}
		if (Shooting)
		{
			NPC.frame.Y = frameHeight;
		}
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadowEssence>(), 3));
    }
}
