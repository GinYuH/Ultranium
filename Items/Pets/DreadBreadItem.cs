using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets;

public class DreadBreadItem : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Suspicious Looking Bread");
		// ((ModItem)this).Tooltip.SetDefault("Summons... Dread?");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.CloneDefaults(669);
		((ModItem)this).Item.shoot = ModContent.ProjectileType<DreadBread>();
		((ModItem)this).Item.buffType = ModContent.BuffType<DreadBreadBuff>();
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
