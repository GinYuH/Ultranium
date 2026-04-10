using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class WerewolfPet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Werewolf");
		Main.projFrames[((ModProjectile)this).Projectile.type] = 14;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).Projectile.CloneDefaults(334);
		base.AIType = 334;
		Main.projPet[((ModProjectile)this).Projectile.type] = true;
		((ModProjectile)this).Projectile.width = 34;
		((ModProjectile)this).Projectile.height = 52;
	}

	public override bool PreAI()
	{
		Main.player[((ModProjectile)this).Projectile.owner].zephyrfish = false;
		return true;
	}

	public override void AI()
	{
		Player obj = Main.player[((ModProjectile)this).Projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.WerewolfPet = false;
		}
		if (modPlayer.WerewolfPet)
		{
			((ModProjectile)this).Projectile.timeLeft = 2;
		}
	}

	public override void PostAI()
	{
		((ModProjectile)this).Projectile.frameCounter++;
		if (((ModProjectile)this).Projectile.frameCounter > 32)
		{
			((ModProjectile)this).Projectile.frame++;
			((ModProjectile)this).Projectile.frameCounter = 0;
		}
		if (((ModProjectile)this).Projectile.frame >= 16)
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
