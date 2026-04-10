using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class BabyWorm : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Zephyr Serpent");
		Main.projFrames[((ModProjectile)this).projectile.type] = 4;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.CloneDefaults(380);
		base.aiType = 380;
		((ModProjectile)this).projectile.timeLeft = 999999999;
		((ModProjectile)this).projectile.timeLeft *= 999999999;
		((ModProjectile)this).projectile.penetrate = -1;
	}

	public override void AI()
	{
		Player obj = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.BabyWorm = false;
		}
		if (modPlayer.BabyWorm)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
	}
}
