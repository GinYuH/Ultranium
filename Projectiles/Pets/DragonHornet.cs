using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class DragonHornet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Dragon Hornet");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 3;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.CloneDefaults(198);
		base.AIType = 198;
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
		((ModProjectile)this).Projectile.width = 42;
		((ModProjectile)this).Projectile.height = 36;
	}

	public override void AI()
	{
		Player obj = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.DragonHornet = false;
		}
		if (modPlayer.DragonHornet)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
		((ModProjectile)this).Projectile.frameCounter++;
		if (((ModProjectile)this).Projectile.frameCounter > 9)
		{
			((ModProjectile)this).Projectile.frameCounter = 0;
		}
		if (((ModProjectile)this).Projectile.frame >= 3)
		{
			((ModProjectile)this).Projectile.frame = 0;
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
