using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class StellarChaserPet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Stellar Comet");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 1;
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.CloneDefaults(197);
		base.AIType = 197;
		((ModProjectile)this).Projectile.timeLeft = 999999999;
		((ModProjectile)this).Projectile.timeLeft *= 999999999;
		((ModProjectile)this).Projectile.penetrate = -1;
	}

	public override bool PreAI()
	{
		Lighting.AddLight((int)(((ModProjectile)this).Projectile.Center.X / 16f), (int)(((ModProjectile)this).Projectile.Center.Y / 16f), 0f, 0.4f, 0.7f);
		Main.player[((ModProjectile)this).Projectile.owner].zephyrfish = false;
		return true;
	}

	public override void AI()
	{
		Player obj = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.StellarComet = false;
		}
		if (modPlayer.StellarComet)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
	}
}
