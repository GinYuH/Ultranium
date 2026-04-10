using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class PetBat : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Bat");
		Main.projFrames[((ModProjectile)this).projectile.type] = 4;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.CloneDefaults(198);
		base.aiType = 198;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
		((ModProjectile)this).projectile.width = 42;
		((ModProjectile)this).projectile.height = 36;
	}

	public override void AI()
	{
		Player obj = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.PetBat = false;
		}
		if (modPlayer.PetBat)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
		}
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter > 2)
		{
			((ModProjectile)this).projectile.frameCounter = 0;
		}
		if (((ModProjectile)this).projectile.frame >= 4)
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
