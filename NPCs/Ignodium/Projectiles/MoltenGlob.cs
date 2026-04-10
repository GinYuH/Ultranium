using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ignodium.Projectiles;

public class MoltenGlob : ModProjectile
{
	public override void SetStaticDefaults()
	{
		// ((ModProjectile)this).DisplayName.SetDefault("Molten Glob");
	}

	public override void SetDefaults()
	{
		Main.projFrames[((ModProjectile)this).Projectile.type] = 4;
		((ModProjectile)this).Projectile.CloneDefaults(467);
		base.AIType = 467;
	}

	public override bool PreKill(int timeLeft)
	{
		((ModProjectile)this).Projectile.type = 467;
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
