using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;
using Ultranium.Items.Shade;

namespace Ultranium.NPCs.Enemy.Depths;

public class DepthsMimic : ModNPC
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Depths Mimic");
		Main.npcFrameCount[NPC.type] = Main.npcFrameCount[473];
	}

	public override void SetDefaults()
	{
		NPC.npcSlots = 1f;
		NPC.width = 42;
		NPC.height = 52;
		NPC.damage = 90;
		NPC.defense = 34;
		NPC.lifeMax = 3500;
		NPC.knockBackResist = 0.1f;
		NPC.aiStyle = 87;
		base.AIType = 473;
		base.AnimationType = 473;
		NPC.HitSound = SoundID.NPCHit4;
		NPC.DeathSound = SoundID.NPCDeath6;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("DepthsMimicBanner").Type;
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
			int num = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, Mod.Find<ModDust>("ShadowDustBlack").Type, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.Player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.SpawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.SpawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f && !NPC.AnyNPCs(ModContent.NPCType<DepthsMimic>()))
		{
			if (!spawnInfo.Player.GetModPlayer<UltraniumPlayer>().ZoneDepth)
			{
				return 0f;
			}
			return 1.5f;
		}
		return 0f;
    }

    public override void ModifyNPCLoot(NPCLoot npcLoot)
    {
        npcLoot.Add(ItemDropRule.OneFromOptions(1, ModContent.ItemType<DepthsFlail>(), ModContent.ItemType<DepthsBow>(), ModContent.ItemType<DepthsTome>(), ModContent.ItemType<DepthsStaff>()));
		npcLoot.Add(ItemDropRule.Common(499, 1, 5, 9));
		npcLoot.Add(ItemDropRule.Common(500, 1, 5, 9));
    }
}
