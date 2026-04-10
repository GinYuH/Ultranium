using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Depths;

public class AbyssJelly : ModNPC
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Abyssal Jelly");
		Main.npcFrameCount[NPC.type] = Main.npcFrameCount[242];
	}

	public override void SetDefaults()
	{
		NPC.damage = 60;
		NPC.lifeMax = 260;
		NPC.defense = 20;
		NPC.knockBackResist = 0.1f;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.width = 50;
		NPC.height = 50;
		NPC.noGravity = true;
		NPC.aiStyle = 18;
		base.AIType = 242;
		base.AnimationType = 242;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("AbyssJellyBanner").Type;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (NPC.life <= 0)
		{
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Depths/AbyssJellyGore1"));
			Gore.NewGore(null, NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/Depths/AbyssJellyGore2"));
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.Player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.SpawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.SpawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.Player.GetModPlayer<UltraniumPlayer>().ZoneDepth || !spawnInfo.Water)
			{
				return 0f;
			}
			return 150f;
		}
		return 0f;
	}

	public override void OnKill()
	{
		if (Main.rand.Next(5) == 0)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("ShadowEssence").Type, 1, false, 0, false, false);
		}
	}
}
