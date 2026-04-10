using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Erebus.Biome;

public class ShadeWisp : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Shade Wisp");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 8;
		ProjectileID.Sets.MinionSacrificable[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.CultistIsResistantTo[((ModProjectile)this).Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[((ModProjectile)this).Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.CloneDefaults(317);
		((ModProjectile)this).Projectile.width = 30;
		((ModProjectile)this).Projectile.height = 30;
		((ModProjectile)this).Projectile.minion = true;
		((ModProjectile)this).Projectile.friendly = true;
		((ModProjectile)this).Projectile.ignoreWater = true;
		((ModProjectile)this).Projectile.tileCollide = true;
		((ModProjectile)this).Projectile.netImportant = true;
		base.AIType = 317;
		((ModProjectile)this).Projectile.penetrate = -1;
		((ModProjectile)this).Projectile.timeLeft = 18000;
		((ModProjectile)this).Projectile.minionSlots = 1f;
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
		bool num = ((ModProjectile)this).Projectile.type == ((ModProjectile)this).Mod.Find<ModProjectile>("ShadeWisp").Type;
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (num)
		{
			if (player.dead)
			{
				modPlayer.ShadeWisp = false;
			}
			if (modPlayer.ShadeWisp)
			{
				((ModProjectile)this).Projectile.timeLeft = 2;
			}
		}
		if (++((ModProjectile)this).Projectile.frameCounter >= 5)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
			if (++((ModProjectile)this).Projectile.frame >= 8)
			{
				((ModProjectile)this).Projectile.frame = 0;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
