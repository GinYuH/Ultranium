using System;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Ultranium.Items.Shade;

namespace Ultranium.NPCs.Enemy.Shadow;

public class DarkDemon : ModNPC
{
	private int Timer;

	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Dark Demon");
		Main.npcFrameCount[NPC.type] = 4;
	}

	public override void SetDefaults()
	{
		NPC.width = 30;
		NPC.height = 30;
		NPC.damage = 18;
		NPC.defense = 10;
		NPC.lifeMax = 55;
		NPC.HitSound = SoundID.NPCHit21;
		NPC.DeathSound = SoundID.NPCDeath24;
		NPC.value = Item.buyPrice(0, 0, 1);
		NPC.knockBackResist = 0.5f;
		NPC.aiStyle = NPCAIStyleID.Bat;
		NPC.noGravity = true;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("DarkDemonBanner").Type;
	}

	public override void AI()
	{
		NPC.TargetClosest();
		Player player = Main.player[NPC.target];
		NPC.rotation = NPC.velocity.X * 0.03f;
		NPC.spriteDirection = NPC.direction;
		Timer++;
		if (Timer > 600)
		{
			NPC.velocity *= 0f;
		}
		if (Timer == 650)
		{
			float num = 6f;
			float num2 = (float)Math.Atan2(NPC.Center.Y - player.Center.Y, NPC.Center.X - player.Center.X);
			Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, (float)(Math.Cos(num2) * (double)num * -1.0), (float)(Math.Sin(num2) * (double)num * -1.0), Mod.Find<ModProjectile>("DarkDemonScythe").Type, 20, 0f, 0, 0f, 0f);
			Timer = 0;
		}
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0)
		{
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("DemonGore1").Type);
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("DemonGore2").Type);
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("DemonGore3").Type);
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("DemonGore4").Type);
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
        npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadowEssence>(), 2, 1, 3));
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
			return 20f;
		}
		return 0f;
	}
}
