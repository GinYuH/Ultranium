using Terraria;
using Terraria.ModLoader;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Buffs.Pet;

public class CacodemonBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		DisplayName.SetDefault("Cacodemon");
		Description.SetDefault("It makes weird noises");
		Main.buffNoTimeDisplay[((ModBuff)this).Type] = true;
		Main.vanityPet[((ModBuff)this).Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.buffTime[buffIndex] = 18000;
		player.GetModPlayer<UltraniumPlayer>().Cacodemon = true;
		if (player.ownedProjectileCounts[ModContent.ProjectileType<Cacodemon>()] <= 0 && player.whoAmI == Main.myPlayer)
		{
			Projectile.NewProjectile(player.GetSource_Buff(buffIndex), player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Cacodemon>(), 0, 0f, player.whoAmI, 0f, 0f);
		}
	}
}
