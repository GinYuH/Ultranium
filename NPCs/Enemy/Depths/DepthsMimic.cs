using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Depths;

public class DepthsMimic : ModNPC
{
	public override void SetStaticDefaults()
	{
		// ((ModNPC)this).DisplayName.SetDefault("Depths Mimic");
		Main.npcFrameCount[((ModNPC)this).NPC.type] = Main.npcFrameCount[473];
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.npcSlots = 1f;
		((ModNPC)this).NPC.width = 42;
		((ModNPC)this).NPC.height = 52;
		((ModNPC)this).NPC.damage = 90;
		((ModNPC)this).NPC.defense = 34;
		((ModNPC)this).NPC.lifeMax = 3500;
		((ModNPC)this).NPC.knockBackResist = 0.1f;
		((ModNPC)this).NPC.aiStyle = 87;
		base.AIType = 473;
		base.AnimationType = 473;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit4;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath6;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("DepthsMimicBanner").Type;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		if (((ModNPC)this).NPC.life > 0)
		{
			return;
		}
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X + (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y + (float)(((ModNPC)this).NPC.height / 2);
		((ModNPC)this).NPC.width = 30;
		((ModNPC)this).NPC.height = 30;
		((ModNPC)this).NPC.position.X = ((ModNPC)this).NPC.position.X - (float)(((ModNPC)this).NPC.width / 2);
		((ModNPC)this).NPC.position.Y = ((ModNPC)this).NPC.position.Y - (float)(((ModNPC)this).NPC.height / 2);
		for (int i = 0; i < 20; i++)
		{
			int num = Dust.NewDust(new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y), ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("ShadowDustBlack").Type, 0f, 0f, 100, default(Color), 2f);
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

	public override void OnKill()
	{
		int num = Main.rand.Next(4);
		if (num == 0)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DepthsFlail").Type, 1, false, 0, false, false);
		}
		if (num == 1)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DepthsBow").Type, 1, false, 0, false, false);
		}
		if (num == 2)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DepthsTome").Type, 1, false, 0, false, false);
		}
		if (num == 3)
		{
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("DepthsStaff").Type, 1, false, 0, false, false);
		}
		Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 499, Main.rand.Next(5, 10), false, 0, false, false);
		Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, 500, Main.rand.Next(5, 15), false, 0, false, false);
	}
}
