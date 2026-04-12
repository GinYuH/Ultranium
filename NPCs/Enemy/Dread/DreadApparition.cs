using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Dread;

public class DreadApparition : ModNPC
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Dread Apparition");
		Main.npcFrameCount[NPC.type] = 6;
	}

	public override void SetDefaults()
	{
		NPC.width = 48;
		NPC.height = 34;
		NPC.damage = 35;
		NPC.defense = 15;
		NPC.lifeMax = 200;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath6;
		NPC.aiStyle = 86;
		NPC.noGravity = true;
		NPC.lavaImmune = true;
		base.AIType = 472;
		base.AnimationType = 472;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("DreadApparitionBanner").Type;
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Player player = spawnInfo.Player;
		if (!player.ZoneTowerSolar && !player.ZoneTowerVortex && !player.ZoneTowerNebula && !player.ZoneTowerStardust && ((!Main.pumpkinMoon && !Main.snowMoon) || (double)spawnInfo.SpawnTileY > Main.worldSurface || Main.dayTime) && (!Main.eclipse || (double)spawnInfo.SpawnTileY > Main.worldSurface || !Main.dayTime) && SpawnCondition.GoblinArmy.Chance == 0f)
		{
			if (!spawnInfo.Player.ZoneSkyHeight || !Main.hardMode || UltraniumWorld.downedDread)
			{
				return 0f;
			}
			return 0.06f;
		}
		return 0f;
	}

	public override void OnKill()
	{
		Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("DreadFlame").Type, Main.rand.Next(1, 3), false, 0, false, false);
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
			int num = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, 90, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
	}
}
