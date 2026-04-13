using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets.Console;

public class PetriDish : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Petri Dish");
		//Tooltip.SetDefault("Summons a pet slime");
	}

	public override void SetDefaults()
	{
		Item.rare = 3;
		Item.CloneDefaults(669);
		Item.shoot = ModContent.ProjectileType<SlimePet>();
		Item.buffType = ModContent.BuffType<SlimeBuff>();
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
