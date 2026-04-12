using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Melee;

public class MeteorThrow : ModProjectile
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Meteor Throw");
		ProjectileID.Sets.TrailCacheLength[Projectile.type] = 4;
		ProjectileID.Sets.TrailingMode[Projectile.type] = 0;
	}

	public override void SetDefaults()
	{
		Projectile.DamageType = DamageClass.Melee;
		Projectile.CloneDefaults(542);
		Projectile.damage = 16;
		Projectile.extraUpdates = 1;
		base.AIType = 542;
	}

	public override void PostAI()
	{
		Projectile.rotation -= 10f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 160, quiet: true);
	}
}
