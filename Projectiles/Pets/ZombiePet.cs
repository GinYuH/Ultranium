using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Projectiles.Pets;

public class ZombiePet : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Zombie");
		Main.projFrames[((ModProjectile)this).projectile.type] = 3;
		Main.projPet[((ModProjectile)this).projectile.type] = true;
	}

	public override void SetDefaults()
	{
		((ModProjectile)this).projectile.CloneDefaults(111);
		base.aiType = 111;
		((ModProjectile)this).projectile.width = 34;
		((ModProjectile)this).projectile.height = 44;
		((ModProjectile)this).projectile.timeLeft = 999999999;
		((ModProjectile)this).projectile.timeLeft *= 999999999;
	}

	public override void AI()
	{
		Player player = Main.player[((ModProjectile)this).projectile.owner];
		((ModProjectile)this).projectile.frameCounter++;
		if (((ModProjectile)this).projectile.frameCounter > 18)
		{
			((ModProjectile)this).projectile.frame++;
			((ModProjectile)this).projectile.frameCounter = 0;
		}
		if (((ModProjectile)this).projectile.frame >= 3)
		{
			((ModProjectile)this).projectile.frame = 0;
		}
		if (((ModProjectile)this).projectile.localAI[0] >= 800f)
		{
			((ModProjectile)this).projectile.localAI[0] = 0f;
		}
		if (Vector2.Distance(player.Center, ((ModProjectile)this).projectile.Center) > 500f)
		{
			((ModProjectile)this).projectile.position.X = player.position.X;
			((ModProjectile)this).projectile.position.Y = player.position.Y;
		}
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
		if (player.dead)
		{
			modPlayer.ZombiePet = false;
		}
		if (modPlayer.ZombiePet)
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
