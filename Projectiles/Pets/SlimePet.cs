using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class SlimePet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Slime");
		Main.projFrames[((ModProjectile)this).projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.CloneDefaults(334);
		base.aiType = 334;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
		((ModProjectile)this).projectile.width = 32;
		((ModProjectile)this).projectile.height = 22;
		((ModProjectile)this).projectile.alpha = 50;
	}

	public override void AI()
	{
		Player obj = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.SlimePet = false;
		}
		if (modPlayer.SlimePet)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
	}

	public override void PostAI()
	{
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter > 200)
		{
			((ModProjectile)this).projectile.frame++;
			((ModProjectile)this).projectile.frameCounter = 0;
		}
		if (((ModProjectile)this).projectile.frame >= 2)
		{
			((ModProjectile)this).projectile.frame = 0;
		}
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
	{
		fallThrough = false;
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
