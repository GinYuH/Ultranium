using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class GuineaPig : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Guinea Pig");
		Main.projFrames[((ModProjectile)this).projectile.type] = 8;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.CloneDefaults(111);
		base.aiType = 111;
		((ModProjectile)this).projectile.height = 36;
		((ModProjectile)this).projectile.width = 30;
		((ModProjectile)this).projectile.timeLeft = 999999999;
		((ModProjectile)this).projectile.timeLeft *= 999999999;
	}

	public override void AI()
	{
		Player obj = Main.player[((ModProjectile)this).projectile.owner];
		UltraniumPlayer modPlayer = obj.GetModPlayer<UltraniumPlayer>();
		if (obj.dead)
		{
			modPlayer.GuineaPig = false;
		}
		if (modPlayer.GuineaPig)
		{
			((ModProjectile)this).projectile.timeLeft = 2;
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
