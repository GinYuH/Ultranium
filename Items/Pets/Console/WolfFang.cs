using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets.Console;

public class WolfFang : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Wolf Fang");
		//Tooltip.SetDefault("Summons a pet werewolf");
	}

	public override void SetDefaults()
	{
		Item.rare = 3;
		Item.CloneDefaults(669);
		Item.shoot = ModContent.ProjectileType<WerewolfPet>();
		Item.buffType = ModContent.BuffType<WerewolfBuff>();
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
