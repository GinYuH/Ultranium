using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Minion.Armor;

[AutoloadEquip(/*Could not decode attribute arguments.*/)]
public class ShadowflameHood : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Shadowflame Cowl");
		// ((ModItem)this).Tooltip.SetDefault("5% increased summon damage and +1 max minions");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 34;
		((Entity)(object)((ModItem)this).Item).height = 22;
		((ModItem)this).Item.value = Item.buyPrice(0, 45);
		((ModItem)this).Item.rare = 5;
		((ModItem)this).Item.defense = 7;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ((ModItem)this).Mod.Find<ModItem>("ShadowflameBody").Type)
		{
			return legs.type == ((ModItem)this).Mod.Find<ModItem>("ShadowflameLegs").Type;
		}
		return false;
	}

	public override void UpdateArmorSet(Player player)
	{
		player.setBonus = "\nYour minions will inflict shadow flame on enemies";
		player.GetModPlayer<UltraniumPlayer>().ShadowflameSet = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage(DamageClass.Summon) += 0.05f;
		player.maxMinions++;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "ShadowFlame", 7);
		val.AddIngredient(521, 7);
		val.AddIngredient(225, 12);
		val.AddTile(134);
		val.Register();
	}
}
