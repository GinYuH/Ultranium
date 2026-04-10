using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class NatureJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Jungle's Wrath");
		// ((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 18;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 54;
		((Entity)(object)((ModItem)this).Item).height = 56;
		((ModItem)this).Item.useTime = 28;
		((ModItem)this).Item.useAnimation = 28;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 20);
		((ModItem)this).Item.rare = 2;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("NatureJavelin").Type;
		((ModItem)this).Item.shootSpeed = 9f;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(331, 12);
		val.AddIngredient(209, 12);
		val.AddTile(16);
		val.Register();
	}
}
