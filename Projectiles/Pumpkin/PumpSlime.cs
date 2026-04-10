using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pumpkin;

public class PumpSlime : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Pumpkin Minion");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 1;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.CultistIsResistantTo[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Main.projFrames[((ModProjectile)this).Projectile.type] = 6;
		((ModProjectile)this).Projectile.CloneDefaults(266);
		((ModProjectile)this).Projectile.width = 26;
		((ModProjectile)this).Projectile.height = 26;
		((ModProjectile)this).Projectile.minion = true;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.netImportant = true;
		base.AIType = 266;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.timeLeft = 18000;
		((ModProjectile)this).Projectile.minionSlots = 1f;
		((ModProjectile)this).Projectile.alpha = 0;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (((ModProjectile)this).Projectile.penetrate == 0)
		{
			((ModProjectile)this).Projectile.Kill();
		}
		return false;
	}

	public override void AI()
	{
		bool num = ((ModProjectile)this).Projectile.type == ((ModProjectile)this).Mod.Find<ModProjectile>("PumpSlime").Type;
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (num)
		{
			if (player.dead)
			{
				modPlayer.PumpSlime = false;
			}
			if (modPlayer.PumpSlime)
			{
				((ModProjectile)this).Projectile.timeLeft = 2;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
