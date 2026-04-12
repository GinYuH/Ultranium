using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class BabyWorm : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Zephyr Serpent");
		Main.projFrames[Projectile.type] = 4;
		Main.projPet[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(380);
		base.AIType = 380;
		Projectile.timeLeft = 999999999;
		Projectile.timeLeft *= 999999999;
		Projectile.penetrate = -1;
	}

	public override void AI()
	{
		Player obj = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.BabyWorm = false;
		}
		if (modPlayer.BabyWorm)
		{
			Projectile.timeLeft = 2;
		}
	}
}
