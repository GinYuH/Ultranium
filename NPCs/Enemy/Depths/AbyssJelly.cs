using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Depths;

public class AbyssJelly : ModNPC
{
	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Abyssal Jelly");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = Main.npcFrameCount[242];
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.damage = 60;
		((ModNPC)this).NPC.lifeMax = 260;
		((ModNPC)this).NPC.defense = 20;
		((ModNPC)this).NPC.knockBackResist = 0.1f;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit1;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath1;
		((ModNPC)this).NPC.width = 50;
		((ModNPC)this).NPC.height = 50;
		((ModNPC)this).NPC.noGravity = true;
		((ModNPC)this).NPC.aiStyle = 18;
		base.AIType = 242;
		base.AnimationType = 242;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("AbyssJellyBanner").Type;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (((ModNPC)this).NPC.life <= 0)
		{
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/Depths/AbyssJellyGore1"));
			Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/Depths/AbyssJellyGore2"));
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
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("ShadowEssence").Type, 1, false, 0, false, false);
		}
	}
}
