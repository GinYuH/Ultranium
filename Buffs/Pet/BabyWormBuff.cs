using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Buffs.Pet;

public class BabyWormBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Zephyr Serpent");
		Description.SetDefault("It likes to float around you...");
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
		Main.vanityPet[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.GetModPlayer<UltraniumPlayer>().BabyWorm = true;
		if (player.ownedProjectileCounts[((ModBuff)this).Mod.Find<ModProjectile>("BabyWorm").Type] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(null, player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ((ModBuff)this).Mod.Find<ModProjectile>("BabyWorm").Type, 0, 0f, player.whoAmI, 0f, 0f);
		}
	}
}
