using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class ZombiePet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Zombie");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 3;
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.CloneDefaults(111);
		base.AIType = 111;
		((ModProjectile)this).Projectile.width = 34;
		((ModProjectile)this).Projectile.height = 44;
		((ModProjectile)this).Projectile.timeLeft = 999999999;
		((ModProjectile)this).Projectile.timeLeft *= 999999999;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).Projectile.owner];
		((ModProjectile)this).Projectile.frameCounter++;
		if (((ModProjectile)this).Projectile.frameCounter > 18)
		{
			((ModProjectile)this).Projectile.frame++;
			((ModProjectile)this).Projectile.frameCounter = 0;
		}
		if (((ModProjectile)this).Projectile.frame >= 3)
		{
			((ModProjectile)this).Projectile.frame = 0;
		}
		if (((ModProjectile)this).Projectile.localAI[0] >= 800f)
		{
			((ModProjectile)this).Projectile.localAI[0] = 0f;
		}
		if (Vector2.Distance(player.Center, ((ModProjectile)this).Projectile.Center) > 500f)
		{
			((ModProjectile)this).Projectile.position.X = player.position.X;
			((ModProjectile)this).Projectile.position.Y = player.position.Y;
		}
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.dead)
		{
			modPlayer.ZombiePet = false;
		}
		if (modPlayer.ZombiePet)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
