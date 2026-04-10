using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Shade.Warden;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class AbyssTitanHead : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Abyss Titan Helmet");
		// ((ModItem)this).Tooltip.SetDefault("10% increased ranged damage and critical strike chance");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 34;
		((Entity)(object)((ModItem)this).Item).height = 22;
		((ModItem)this).Item.value = Item.buyPrice(0, 45);
		((ModItem)this).Item.rare = 7;
		((ModItem)this).Item.defense = 18;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Ranged) += 0.1f;
		player.GetCritChance(DamageClass.Ranged) += 10;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).Mod.Find<ModItem>("AbyssWardenBody").Type)
		{
			return legs.type == ((ModItem)this).Mod.Find<ModItem>("AbyssWardenLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\n25% chance to not consume ammo";
		player.ammoCost75 = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "AbyssRockItem", 45);
		val.AddIngredient((Mod)null, "DepthGlowstoneItem", 35);
		val.AddTile(134);
		val.Register();
	}
}
