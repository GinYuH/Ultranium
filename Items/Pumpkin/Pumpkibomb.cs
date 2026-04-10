using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Pumpkin;

public class Pumpkibomb : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Pumpki-Bomb");
		// ((ModItem)this).Tooltip.SetDefault("A throwable, explosive pumpkin");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 12;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 20;
		((Entity)(object)((ModItem)this).Item).height = 20;
		((ModItem)this).Item.useTime = 35;
		((ModItem)this).Item.useAnimation = 35;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 0, 50);
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("Pumpkibomb").Type;
		((ModItem)this).Item.shootSpeed = 5f;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Expected O, but got Unknown
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(1725, 10);
		val.AddIngredient(9, 20);
		((Recipe)val).anyWood = true;
		val.AddTile(18);
		val.Register();
	}
}
