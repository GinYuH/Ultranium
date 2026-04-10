using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Melee;

public class MeteorThrow : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Meteor Throw");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.DamageType = DamageClass.Melee;
		((ModProjectile)this).Projectile.CloneDefaults(542);
		((ModProjectile)this).Projectile.damage = 16;
		((ModProjectile)this).Projectile.extraUpdates = 1;
		base.AIType = 542;
	}

	public override void PostAI()
	{
		((ModProjectile)this).Projectile.rotation -= 10f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 160, quiet: true);
	}
}
