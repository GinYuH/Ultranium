using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class CosmicDjinn : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Cosmic Djinn");
		Main.projFrames[((ModProjectile)this).projectile.type] = 4;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.CloneDefaults(198);
		base.aiType = 198;
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
			modPlayer.CosmicDjinn = false;
		}
		if (modPlayer.CosmicDjinn)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
	}
}
