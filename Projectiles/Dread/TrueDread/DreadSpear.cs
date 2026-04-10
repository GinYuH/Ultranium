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
		// DisplayName.SetDefault("Spear of Fear");
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(47);
		Projectile.height = 122;
		Projectile.width = 122;
		base.AIType = 47;
		Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		timer--;
		if (timer == 0)
		{
			SoundEngine.PlaySound(SoundID.Item8, new Vector2(Projectile.position.X, Projectile.position.Y));
			Projectile.NewProjectile(null, Projectile.Center, Projectile.velocity, Mod.Find<ModProjectile>("DreadSickle").Type, (int)((float)Projectile.damage * 1.5f), Projectile.knockBack, Projectile.owner, 0f, 0f);
			timer = 20;
		}
	}
}
