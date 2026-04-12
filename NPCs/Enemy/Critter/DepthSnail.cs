using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Critter;

public class DepthSnail : ModNPC
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Tenebris Snail");
	}

	public override void SetDefaults()
	{
		NPC.width = 20;
		NPC.height = 20;
		NPC.damage = 0;
		NPC.defense = 0;
		NPC.lifeMax = 12;
		Main.npcCatchable[NPC.type] = true;
		NPC.catchItem = (short)ModContent.ItemType<DepthSnailItem>();
		NPC.aiStyle = 67;
		base.AIType = 360;
		Main.npcFrameCount[NPC.type] = Main.npcFrameCount[360];
		base.AnimationType = 360;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.knockBackResist = 0.35f;
		NPC.noGravity = true;
		NPC.npcSlots = 0f;
		NPC.dontCountMe = true;
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
}
