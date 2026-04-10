using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class CrimsonJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Crimtane Pike");
		// ((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 18;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 66;
		((Entity)(object)((ModItem)this).Item).height = 60;
		((ModItem)this).Item.useTime = 23;
		((ModItem)this).Item.useAnimation = 23;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 12);
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("CrimsonJavelin").Type;
		((ModItem)this).Item.shootSpeed = 8f;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(1257, 10);
		val.AddTile(16);
		val.Register();
	}
}
