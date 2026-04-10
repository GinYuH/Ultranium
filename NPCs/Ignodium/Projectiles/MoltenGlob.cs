using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Ultranium.NPCs.Ignodium.Projectiles;

public class MoltenGlob : ModProjectile
{
	public override void SetStaticDefaults()
	{
		((ModProjectile)this).DisplayName.SetDefault("Molten Glob");
	}

	public override void SetDefaults()
	{
		Main.projFrames[((ModProjectile)this).projectile.type] = 4;
		((ModProjectile)this).projectile.CloneDefaults(467);
		base.aiType = 467;
	}

	public override bool PreKill(int timeLeft)
	{
		((ModProjectile)this).projectile.type = 467;
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		return Color.White;
	}
}
