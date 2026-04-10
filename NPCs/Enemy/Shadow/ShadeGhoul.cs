using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Enemy.Shadow;

public class ShadeGhoul : ModNPC
{
	public override void SetStaticDefaults()
	{
		((ModNPC)this).DisplayName.SetDefault("Shade Ghoul");
		Main.npcFrameCount[((ModNPC)this).npc.type] = Main.npcFrameCount[524];
	}

	public override void SetDefaults()
	{
		((ModNPC)this).npc.npcSlots = 1f;
		((ModNPC)this).npc.width = 42;
		((ModNPC)this).npc.height = 52;
		((ModNPC)this).npc.damage = 18;
		((ModNPC)this).npc.defense = 15;
		((ModNPC)this).npc.lifeMax = 60;
		((ModNPC)this).npc.knockBackResist = 0.1f;
		((ModNPC)this).npc.aiStyle = 3;
		base.aiType = 524;
		base.animationType = 524;
		((ModNPC)this).npc.HitSound = SoundID.NPCHit6;
		((ModNPC)this).npc.DeathSound = SoundID.NPCDeath8;
		base.banner = ((ModNPC)this).npc.type;
		base.bannerItem = ((ModNPC)this).mod.ItemType("ShadeGhoulBanner");
	}

	public override void HitEffect(int hitDirection, double damage)
	{
		if (((ModNPC)this).npc.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowBiome/GhoulGore1"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowBiome/GhoulGore2"));
			Gore.NewGore(((ModNPC)this).npc.position, ((ModNPC)this).npc.velocity, ((ModNPC)this).mod.GetGoreSlot("Gores/ShadowBiome/GhoulGore3"));
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.spawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.spawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.player.GetModPlayer<UltraniumPlayer>().ZoneShadow || Main.dayTime)
			{
				return 0f;
			}
			return 20f;
		}
		return 0f;
	}

	public override void NPCLoot()
	{
		if (Main.rand.Next(2) == 1)
		{
			Item.NewItem((int)((ModNPC)this).npc.position.X, (int)((ModNPC)this).npc.position.Y, ((ModNPC)this).npc.width, ((ModNPC)this).npc.height, ((ModNPC)this).mod.ItemType("ShadowEssence"), 1, false, 0, false, false);
		}
	}
}
