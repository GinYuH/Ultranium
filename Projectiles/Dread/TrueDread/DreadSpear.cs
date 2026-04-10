using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadSpear : ModProjectile
{
	private int timer = 10;

	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Spear of Fear");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.CloneDefaults(47);
		((ModProjectile)this).projectile.height = 122;
		((ModProjectile)this).projectile.width = 122;
		base.aiType = 47;
		((ModProjectile)this).projectile.melee = true;
	}

	public override void AI()
	{
		timer--;
		if (timer == 0)
		{
			Main.PlaySound(2, (int)((ModProjectile)this).projectile.position.X, (int)((ModProjectile)this).projectile.position.Y, 8, 1f, 0f);
			Projectile.NewProjectile(((ModProjectile)this).projectile.Center, ((ModProjectile)this).projectile.velocity, ((ModProjectile)this).mod.ProjectileType("DreadSickle"), (int)((float)((ModProjectile)this).projectile.damage * 1.5f), ((ModProjectile)this).projectile.knockBack, ((ModProjectile)this).projectile.owner, 0f, 0f);
			timer = 20;
		}
	}
}
