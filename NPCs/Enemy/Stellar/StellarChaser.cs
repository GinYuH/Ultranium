using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Ultranium.NPCs.Enemy.Stellar;

public class StellarChaser : ModNPC
{
	private int moveSpeed;

	private int moveSpeedY;

	private float HomeY = 150f;

	private int ShootTimer;

	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Stellar Chaser");
	}

	public override void SetDefaults()
	{
		NPC.width = 38;
		NPC.height = 38;
		NPC.damage = 22;
		NPC.defense = 11;
		NPC.noTileCollide = true;
		NPC.lifeMax = 230;
		NPC.HitSound = SoundID.NPCHit3;
		NPC.DeathSound = SoundID.NPCDeath43;
		NPC.value = 360f;
		NPC.knockBackResist = 0.16f;
		NPC.noGravity = true;
		base.Banner = NPC.type;
		base.BannerItem = Mod.Find<ModItem>("StellarChaserBanner").Type;
	}

	public override void AI()
	{
		NPC.spriteDirection = NPC.direction;
		Player player = Main.player[NPC.target];
		if (NPC.Center.X >= player.Center.X && moveSpeed >= -45)
		{
			moveSpeed--;
		}
		if (NPC.Center.X <= player.Center.X && moveSpeed <= 45)
		{
			moveSpeed++;
		}
		NPC.velocity.X = (float)moveSpeed * 0.1f;
		if (NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -27)
		{
			moveSpeedY--;
			HomeY = 150f;
		}
		if (NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 27)
		{
			moveSpeedY++;
		}
		NPC.velocity.Y = (float)moveSpeedY * 0.12f;
		if (Main.rand.Next(220) == 6)
		{
			HomeY = -35f;
		}
		NPC.rotation += 0.1f;
		ShootTimer++;
		if (ShootTimer == 300)
		{
			Vector2 vector = Main.player[NPC.target].Center - NPC.Center;
			vector.Normalize();
			vector.X *= 6f;
			vector.Y *= 6f;
			Projectile.NewProjectile(null, NPC.Center.X, NPC.Center.Y, vector.X, vector.Y, Mod.Find<ModProjectile>("EyeBolt").Type, 18, 1f, Main.myPlayer, 0f, 0f);
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
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("StellarChaserGore1").Type);
		Gore.NewGore(null, NPC.position, NPC.velocity, Mod.Find<ModGore>("StellarChaserGore2").Type);
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
