using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class CosmicDjinn : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Cosmic Djinn");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 4;
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.CloneDefaults(198);
		base.AIType = 198;
		((ModProjectile)this).Projectile.timeLeft = 999999999;
		((ModProjectile)this).Projectile.timeLeft *= 999999999;
		((ModProjectile)this).Projectile.penetrate = -1;
	}

	public override void AI()
	{
		Player obj = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.CosmicDjinn = false;
		}
		if (modPlayer.CosmicDjinn)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
	}
}
