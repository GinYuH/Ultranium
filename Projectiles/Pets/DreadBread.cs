using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class DreadBread : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Dread Bread");
		Main.projFrames[((ModProjectile)this).projectile.type] = 3;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.CloneDefaults(112);
		base.aiType = 112;
		((ModProjectile)this).projectile.width = 24;
		((ModProjectile)this).projectile.height = 26;
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
			modPlayer.DreadBread = false;
		}
		if (modPlayer.DreadBread)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
	}
}
