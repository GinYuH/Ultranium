using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Ultranium.Buffs.Minions;

namespace Ultranium.Projectiles.Pumpkin;

public class PumpSlime : ModProjectile
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Pumpkin Minion");
		Main.projFrames[Projectile.type] = 1;
		ProjectileID.Sets.MinionSacrificable[Projectile.type] = true;
		ProjectileID.Sets.CultistIsResistantTo[Projectile.type] = true;
		ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
	}

	public override void SetDefaults()
	{
		Main.projFrames[Projectile.type] = 6;
		Projectile.CloneDefaults(266);
		Projectile.width = 26;
		Projectile.height = 26;
		Projectile.minion = true;
		Projectile.friendly = true;
		Projectile.ignoreWater = true;
		Projectile.tileCollide = true;
		Projectile.netImportant = true;
		AIType = ProjectileID.BabySlime;
		Projectile.penetrate = -1;
		Projectile.timeLeft = 18000;
		Projectile.minionSlots = 1f;
		Projectile.alpha = 0;
        Projectile.DamageType = DamageClass.Summon;
    }

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		if (Projectile.penetrate == 0)
		{
			Projectile.Kill();
		}
		return false;
	}

	public override void AI()
	{
		bool num = Projectile.type == Mod.Find<ModProjectile>("PumpSlime").Type;
		Player player = Main.player[Projectile.owner];
		UltraniumPlayer modPlayer = player.GetModPlayer<UltraniumPlayer>();
        player.AddBuff(ModContent.BuffType<PumpBuff>(), 3600, quiet: false);
        if (num)
		{
			if (player.dead)
			{
				modPlayer.PumpSlime = false;
			}
			if (modPlayer.PumpSlime)
			{
				Projectile.timeLeft = 2;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}
}
