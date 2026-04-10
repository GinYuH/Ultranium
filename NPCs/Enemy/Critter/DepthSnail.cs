using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Critter;

public class DepthSnail : ModNPC
{
	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Tenebris Snail");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 20;
		((ModNPC)this).NPC.height = 20;
		((ModNPC)this).NPC.damage = 0;
		((ModNPC)this).NPC.defense = 0;
		((ModNPC)this).NPC.lifeMax = 12;
		Main.npcCatchable[((ModNPC)this).NPC.type] = true;
		((ModNPC)this).NPC.catchItem = (short)ModContent.ItemType<DepthSnailItem>();
		((ModNPC)this).NPC.aiStyle = 67;
		base.AIType = 360;
		Main.npcFrameCount[((ModNPC)this).NPC.type] = Main.npcFrameCount[360];
		base.AnimationType = 360;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit1;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).NPC.knockBackResist = 0.35f;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.npcSlots = 0f;
		((ModNPC)this).NPC.dontCountMe = true;
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
			if (!spawnInfo.Player.GetModPlayer<UltraniumPlayer>().ZoneDepth || spawnInfo.SpawnTileType != ((ModNPC)this).Mod.Find<ModTile>("PurpleShadowGrass").Type)
			{
				return 0f;
			}
			return 40f;
		}
		return 0f;
	}
}
