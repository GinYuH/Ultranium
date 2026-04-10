using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Critter;

public class DepthSnail : ModNPC
{
	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Tenebris Snail");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.width = 20;
		((ModNPC)this).npc.height = 20;
		((ModNPC)this).npc.damage = 0;
		((ModNPC)this).npc.defense = 0;
		((ModNPC)this).npc.lifeMax = 12;
		Main.npcCatchable[((ModNPC)this).npc.type] = true;
		((ModNPC)this).npc.catchItem = (short)ModContent.ItemType<DepthSnailItem>();
		((ModNPC)this).npc.aiStyle = 67;
		base.aiType = 360;
		Main.npcFrameCount[((ModNPC)this).npc.type] = Main.npcFrameCount[360];
		base.animationType = 360;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit1;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).npc.knockBackResist = 0.35f;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.npcSlots = 0f;
		((ModNPC)this).npc.dontCountMe = true;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.player.GetModPlayer<UltraniumPlayer>().ZoneDepth || spawnInfo.spawnTileType != ((ModNPC)this).mod.TileType("PurpleShadowGrass"))
			{
				return 0f;
			}
			return 40f;
		}
		return 0f;
	}
}
