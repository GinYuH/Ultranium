using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Stellar;

public class StellarSlime : ModNPC
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Stellar Slime");
		Main.npcFrameCount[NPC.type] = 2;
	}

	public override void SetDefaults()
	{
		NPC.width = 40;
		NPC.height = 28;
		NPC.damage = 35;
		NPC.defense = 20;
		NPC.lifeMax = 220;
		NPC.HitSound = SoundID.NPCHit1;
		NPC.DeathSound = SoundID.NPCDeath1;
		NPC.value = 60f;
		NPC.value = Item.buyPrice(0, 0, 5);
		NPC.knockBackResist = 0.5f;
		NPC.aiStyle = 1;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("StellarSlimeBanner").Type;
	}

	public override void FindFrame(int frameHeight)
	{
		NPC.frameCounter += 1.0;
		if (NPC.frameCounter >= 6.0)
		{
			NPC.frame.Y = (NPC.frame.Y + frameHeight) % (Main.npcFrameCount[NPC.type] * frameHeight);
			NPC.frameCounter = 1.0;
		}
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
			int num = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, Mod.Find<ModDust>("StellarDust").Type, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("StellarSlimeGore1").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("StellarSlimeGore2").Type);
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
			if (!spawnInfo.Player.ZoneSkyHeight || !Main.hardMode)
			{
				return 0f;
			}
			return 10f;
		}
		return 0f;
	}

	public override void OnKill()
	{
		if (Main.rand.Next(2) == 0)
		{
			Item.NewItem(null, (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("StellarDust").Type, Main.rand.Next(1, 3), false, 0, false, false);
		}
	}
}
