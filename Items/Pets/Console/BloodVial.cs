using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets.Console;

public class BloodVial : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Vial of Blood");
		// Tooltip.SetDefault("Summons a pet bat");
	}

	public override void SetDefaults()
	{
		Item.rare = 3;
		Item.CloneDefaults(669);
		Item.shoot = ModContent.ProjectileType<PetBat>();
		Item.buffType = ModContent.BuffType<BatBuff>();
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(Item.buffType, 3600, quiet: false);
		}
	}

	public override bool CanUseItem(Player player)
	{
		return player.miscEquips[0].IsAir;
	}
}
