using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class DreadBread : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Dread Bread");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 3;
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.CloneDefaults(112);
		base.AIType = 112;
		((ModProjectile)this).Projectile.width = 24;
		((ModProjectile)this).Projectile.height = 26;
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
			modPlayer.DreadBread = false;
		}
		if (modPlayer.DreadBread)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
	}
}
