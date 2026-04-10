using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class StellarChaserPet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Stellar Comet");
		Main.projFrames[((ModProjectile)this).projectile.type] = 1;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.CloneDefaults(197);
		base.aiType = 197;
		((ModProjectile)this).projectile.timeLeft = 999999999;
		((ModProjectile)this).projectile.timeLeft *= 999999999;
		((ModProjectile)this).projectile.penetrate = -1;
	}

	public override bool PreAI()
	{
		Lighting.AddLight((int)(((ModProjectile)this).projectile.Center.X / 16f), (int)(((ModProjectile)this).projectile.Center.Y / 16f), 0f, 0.4f, 0.7f);
		Main.player[((ModProjectile)this).projectile.owner].zephyrfish = false;
		return true;
	}

	public override void AI()
	{
		Player obj = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.StellarComet = false;
		}
		if (modPlayer.StellarComet)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
	}
}
