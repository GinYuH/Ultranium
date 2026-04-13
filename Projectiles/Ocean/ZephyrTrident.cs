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
		//DisplayName.SetDefault("Zephyr Trident");
	}

	public override void SetDefaults()
	{
		Projectile.CloneDefaults(47);
		Projectile.height = 122;
		Projectile.width = 122;
		base.AIType = ProjectileID.Trident;
		Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		timer--;
		if (timer == 0)
		{
			SoundEngine.PlaySound(SoundID.Item8, new Vector2(Projectile.position.X, Projectile.position.Y));
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Projectile.velocity, Mod.Find<ModProjectile>("ZephyrTridentBolt").Type, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
			timer = 25;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Main.player[Projectile.owner].statMana += 5;
	}
}
