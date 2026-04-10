using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Depths;

public class AbyssShark : ModNPC
{
	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Abyssal Goblin Shark");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.lifeMax = 400;
		((ModNPC)this).npc.damage = 60;
		((ModNPC)this).npc.defense = 65;
		((ModNPC)this).npc.knockBackResist = 0.1f;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit1;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).npc.width = 156;
		((ModNPC)this).npc.height = 44;
		((ModNPC)this).npc.noGravity = true;
		((ModNPC)this).npc.aiStyle = 16;
		base.aiType = 65;
		Main.npcFrameCount[((ModNPC)this).npc.type] = Main.npcFrameCount[65];
		base.animationType = 65;
		((ModNPC)this).npc.buffImmune[31] = true;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("AbyssSharkBanner");
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (((ModNPC)this).npc.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/Depths/SharkGore1"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/Depths/SharkGore2"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/Depths/SharkGore3"));
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.player.GetModPlayer<UltraniumPlayer>().ZoneDepth || !spawnInfo.water)
			{
				return 0f;
			}
			return 85f;
		}
		return 0f;
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(5) == 0)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("ShadowEssence"), 1, false, 0, false, false);
		}
	}
}
