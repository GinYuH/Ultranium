using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Depths;

public class ShroomMonster : ModNPC
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Shroom Monster");
		Main.npcFrameCount[NPC.type] = Main.npcFrameCount[166];
	}

	public override void SetDefaults()
	{
		NPC.npcSlots = 1f;
		NPC.width = 42;
		NPC.height = 52;
		NPC.damage = 50;
		NPC.defense = 50;
		NPC.lifeMax = 200;
		NPC.knockBackResist = 0.1f;
		NPC.aiStyle = 3;
		base.AIType = 166;
		base.AnimationType = 166;
		NPC.HitSound = SoundID.NPCHit6;
		NPC.DeathSound = SoundID.NPCDeath8;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("ShroomMonsterBanner").Type;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.Player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.SpawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.SpawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.Player.GetModPlayer<UltraniumPlayer>().ZoneDepth || spawnInfo.SpawnTileType != Mod.Find<ModTile>("PurpleShadowGrass").Type)
			{
				return 0f;
			}
			return 40f;
		}
		return 0f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0)
		{
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("ShroomGore1").Type);
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("ShroomGore2").Type);
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("ShroomGore3").Type);
		}
	}

	public override void OnKill()
	{
		if (Main.rand.Next(2) == 1)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("GlowShroomItem").Type, 1, false, 0, false, false);
		}
		if (Main.rand.Next(5) == 0)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("ShadowEssence").Type, 1, false, 0, false, false);
		}
	}
}
