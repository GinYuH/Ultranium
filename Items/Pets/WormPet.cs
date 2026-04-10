using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Ultranium.Buffs.Pet;
using Ultranium.Projectiles.Pets;

namespace Ultranium.Items.Pets;

public class WormPet : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Mysterious Scale");
		// ((ModItem)this).Tooltip.SetDefault("Summons a long lost serpent");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.CloneDefaults(2420);
		((ModItem)this).Item.shoot = ModContent.ProjectileType<BabyWorm>();
		((ModItem)this).Item.buffType = ModContent.BuffType<BabyWormBuff>();
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
