using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Ocean;

public class ZephyrTrident : ModProjectile
{
	private int timer = 10;

	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Zephyr Trident");
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.CloneDefaults(47);
		((ModProjectile)this).Projectile.height = 122;
		((ModProjectile)this).Projectile.width = 122;
		base.AIType = 47;
		((ModProjectile)this).Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		timer--;
		if (timer == 0)
		{
			SoundEngine.PlaySound(SoundID.Item8, new Vector2(((ModProjectile)this).Projectile.position.X, ((ModProjectile)this).Projectile.position.Y));
			Projectile.NewProjectile(((ModProjectile)this).Projectile.Center, ((ModProjectile)this).Projectile.velocity, ((ModProjectile)this).Mod.Find<ModProjectile>("ZephyrTridentBolt").Type, ((ModProjectile)this).Projectile.damage, ((ModProjectile)this).Projectile.knockBack, ((ModProjectile)this).Projectile.owner, 0f, 0f);
			timer = 25;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Main.player[((ModProjectile)this).Projectile.owner].statMana += 5;
	}
}
