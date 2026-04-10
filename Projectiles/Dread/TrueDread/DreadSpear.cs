using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Dread.TrueDread;

public class DreadSpear : ModProjectile
{
	private int timer = 10;

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Spear of Fear");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.CloneDefaults(47);
		((ModProjectile)this).Projectile.height = 122;
		((ModProjectile)this).Projectile.width = 122;
		base.AIType = 47;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		timer--;
		if (timer == 0)
		{
			SoundEngine.PlaySound(SoundID.Item8, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
			Projectile.NewProjectile(((ModProjectile)this).Projectile.Center, ((ModProjectile)this).Projectile.velocity, ((ModProjectile)this).Mod.Find<ModProjectile>("DreadSickle").Type, (int)((float)((ModProjectile)this).Projectile.damage * 1.5f), ((ModProjectile)this).Projectile.knockBack, ((ModProjectile)this).Projectile.owner, 0f, 0f);
			timer = 20;
		}
	}
}
