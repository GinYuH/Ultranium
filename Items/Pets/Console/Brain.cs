using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets.Console;

public class Brain : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Brain");
		// Tooltip.SetDefault("Summons a pet zombie");
	}

	public override void SetDefaults()
	{
		Item.rare = 3;
		Item.CloneDefaults(669);
		Item.shoot = ModContent.ProjectileType<ZombiePet>();
		Item.buffType = ModContent.BuffType<ZombieBuff>();
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(Item.buffType, 3600, fromNetPvP: true);
		}
	}

	public override bool CanUseItem(Player player)
	{
		return player.miscEquips[0].IsAir;
	}
}
