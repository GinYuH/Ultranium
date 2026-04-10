using Terraria;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets.Console;

public class BloodVial : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Vial of Blood");
		((ModItem)this).Tooltip.SetDefault("Summons a pet bat");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.rare = 3;
		((ModItem)this).item.CloneDefaults(669);
		((ModItem)this).item.shoot = ModContent.ProjectileType<PetBat>();
		((ModItem)this).item.buffType = ModContent.BuffType<BatBuff>();
	}

	public override void UseStyle(Player player)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(((ModItem)this).item.buffType, 3600, fromNetPvP: true);
		}
	}

	public override bool CanUseItem(Player player)
	{
		return player.miscEquips[0].IsAir;
	}
}
