using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion;

public class StaffOfCthulhu : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Staff of Cthulhu");
		((ModItem)this).Tooltip.SetDefault("Summons a Servant of Cthulhu to fight for you");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.summon = true;
		((ModItem)this).item.mana = 20;
		((ModItem)this).item.damage = 7;
		((Entity)(object)((ModItem)this).item).width = 42;
		((Entity)(object)((ModItem)this).item).height = 42;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 0f;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 80);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.UseSound = SoundID.Item44;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("EyeMinion");
		((ModItem)this).item.shootSpeed = 10f;
		((ModItem)this).item.buffType = ((ModItem)this).mod.BuffType("EyeBuff");
		((ModItem)this).item.buffTime = 3600;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
	{
		return player.altFunctionUse != 2;
	}

	public override bool UseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			player.MinionNPCTargetAim();
		}
		return ((ModItem)this).UseItem(player);
	}
}
