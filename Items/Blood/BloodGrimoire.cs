using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Blood;

public class BloodGrimoire : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).DisplayName.SetDefault("Bloody Grimoire");
		((ModItem)this).Tooltip.SetDefault("Conjures a blood dripper minion to fight with you");
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 20;
		((ModItem)this).item.mana = 20;
		((Entity)(object)((ModItem)this).item).width = 26;
		((Entity)(object)((ModItem)this).item).height = 26;
		((ModItem)this).item.useTime = 30;
		((ModItem)this).item.useAnimation = 30;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.summon = true;
		((ModItem)this).item.knockBack = 0f;
		((ModItem)this).item.value = Item.buyPrice(0, 1, 35);
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.UseSound = SoundID.Item44;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("BloodMinion");
		((ModItem)this).item.shootSpeed = 10f;
		((ModItem)this).item.buffType = ((ModItem)this).mod.BuffType("BloodBuff");
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
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		ModRecipe val = new ModRecipe(((ModItem)this).mod);
		val.AddIngredient((Mod)null, "BloodClot", 12);
		val.AddRecipeGroup("Ultranium:Silver/Tungsten", 8);
		val.AddTile(16);
		val.SetResult((ModItem)(object)this, 1);
		val.AddRecipe();
	}
}
