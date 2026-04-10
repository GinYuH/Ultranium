using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Melee;

public class MeteorThrow : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Meteor Throw");
		ProjectileID.Sets.TrailCacheLength[((ModProjectile)this).projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[((ModProjectile)this).projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.melee = true;
		((ModProjectile)this).projectile.CloneDefaults(542);
		((ModProjectile)this).projectile.damage = 16;
		((ModProjectile)this).projectile.extraUpdates = 1;
		base.aiType = 542;
	}

	public override void PostAI()
	{
		((ModProjectile)this).projectile.rotation -= 10f;
	}

	public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
	{
		target.AddBuff(24, 160, quiet: true);
	}
}
