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
		// ((ModNPC)this).DisplayName.SetDefault("Stellar Chaser");
	}

	public override void SetDefaults()
	{
		((ModNPC)this).NPC.width = 38;
		((ModNPC)this).NPC.height = 38;
		((ModNPC)this).NPC.damage = 22;
		((ModNPC)this).NPC.defense = 11;
		((ModNPC)this).NPC.noTileCollide = true;
		((ModNPC)this).NPC.lifeMax = 230;
		((ModNPC)this).NPC.HitSound = SoundID.NPCHit3;
		((ModNPC)this).NPC.DeathSound = SoundID.NPCDeath43;
		((ModNPC)this).NPC.value = 360f;
		((ModNPC)this).NPC.knockBackResist = 0.16f;
		((ModNPC)this).NPC.noGravity = true;
		base.Banner = ((ModNPC)this).NPC.type;
		base.BannerItem = ((ModNPC)this).Mod.Find<ModItem>("StellarChaserBanner").Type;
	}

	public override void AI()
	{
		((ModNPC)this).NPC.spriteDirection = ((ModNPC)this).NPC.direction;
		Player player = Main.player[((ModNPC)this).NPC.target];
		if (((ModNPC)this).NPC.Center.X >= player.Center.X && moveSpeed >= -45)
		{
			moveSpeed--;
		}
		if (((ModNPC)this).NPC.Center.X <= player.Center.X && moveSpeed <= 45)
		{
			moveSpeed++;
		}
		((ModNPC)this).NPC.velocity.X = (float)moveSpeed * 0.1f;
		if (((ModNPC)this).NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -27)
		{
			moveSpeedY--;
			HomeY = 150f;
		}
		if (((ModNPC)this).NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 27)
		{
			moveSpeedY++;
		}
		((ModNPC)this).NPC.velocity.Y = (float)moveSpeedY * 0.12f;
		if (Main.rand.Next(220) == 6)
		{
			HomeY = -35f;
		}
		((ModNPC)this).NPC.rotation += 0.1f;
		ShootTimer++;
		if (ShootTimer == 300)
		{
			Vector2 vector = Main.player[((ModNPC)this).NPC.target].Center - ((ModNPC)this).NPC.Center;
			vector.Normalize();
			vector.X *= 6f;
			vector.Y *= 6f;
			Projectile.NewProjectile(((ModNPC)this).NPC.Center.X, ((ModNPC)this).NPC.Center.Y, vector.X, vector.Y, ((ModNPC)this).Mod.Find<ModProjectile>("EyeBolt").Type, 18, 1f, Main.myPlayer, 0f, 0f);
		}
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
			int num = Dust.NewDust(new Vector2(((ModNPC)this).NPC.position.X, ((ModNPC)this).NPC.position.Y), ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModDust>("StellarDust").Type, 0f, 0f, 100, default(Color), 2f);
			Main.dust[num].velocity *= 3f;
			if (Main.rand.Next(2) == 0)
			{
				Main.dust[num].scale = 0.5f;
				Main.dust[num].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/StellarChaserGore1"));
		Gore.NewGore(((ModNPC)this).NPC.position, ((ModNPC)this).NPC.velocity, ((ModNPC)this).Mod.GetGoreSlot("Gores/StellarChaserGore2"));
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
			Item.NewItem((int)((ModNPC)this).NPC.position.X, (int)((ModNPC)this).NPC.position.Y, ((ModNPC)this).NPC.width, ((ModNPC)this).NPC.height, ((ModNPC)this).Mod.Find<ModItem>("StellarDust").Type, Main.rand.Next(1, 3), false, 0, false, false);
		}
	}
}
