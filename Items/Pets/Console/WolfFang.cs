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
		// ((ModItem)this).DisplayName.SetDefault("Wolf Fang");
		// ((ModItem)this).Tooltip.SetDefault("Summons a pet werewolf");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.rare = 3;
		((ModItem)this).Item.CloneDefaults(669);
		((ModItem)this).Item.shoot = ModContent.ProjectileType<WerewolfPet>();
		((ModItem)this).Item.buffType = ModContent.BuffType<WerewolfBuff>();
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(((ModItem)this).Item.buffType, 3600, fromNetPvP: true);
		}
	}

	public override bool CanUseItem(Player player)
	{
		return player.miscEquips[0].IsAir;
	}
}
