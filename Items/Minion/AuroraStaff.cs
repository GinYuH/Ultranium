using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion;

public class AuroraStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Aurora Crystal Staff");
		((ModItem)this).Tooltip.SetDefault("Summons a spacial star to float above you\nThe star will shoot blasts at the cursor when enemies are near");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.mana = 20;
		((ModItem)this).item.damage = 25;
		((Entity)(object)((ModItem)this).item).width = 42;
		((Entity)(object)((ModItem)this).item).height = 42;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useStyle = 1;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.summon = true;
		((ModItem)this).item.knockBack = 0f;
		((ModItem)this).item.value = Item.buyPrice(0, 0, 80);
		((ModItem)this).item.rare = 1;
		((ModItem)this).item.UseSound = SoundID.Item44;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("StarMinion");
		((ModItem)this).item.shootSpeed = 0f;
		((ModItem)this).item.buffType = ((ModItem)this).mod.BuffType("StarMinionBuff");
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

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "AuroraBar", 5);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
