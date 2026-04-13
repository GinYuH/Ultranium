using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class StellarChaserPet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Stellar Comet");
		Main.projFrames[Projectile.type] = 1;
		Main.projPet[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(197);
		base.AIType = ProjectileID.BabySkeletronHead;
		Projectile.timeLeft = 999999999;
		Projectile.timeLeft *= 999999999;
		Projectile.penetrate = -1;
	}

	public override bool PreAI()
	{
		Lighting.AddLight((int)(Projectile.Center.X / 16f), (int)(Projectile.Center.Y / 16f), 0f, 0.4f, 0.7f);
		Main.player[Projectile.owner].zephyrfish = false;
		return true;
	}

	public override void AI()
	{
		Player obj = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.StellarComet = false;
		}
		if (modPlayer.StellarComet)
		{
			Projectile.timeLeft = 2;
		}
	}
}
