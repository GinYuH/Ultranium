using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class DemoniteJavelin : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Demonite Javelin");
		// ((ModItem)this).Tooltip.SetDefault("");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 18;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 58;
		((Entity)(object)((ModItem)this).Item).height = 56;
		((ModItem)this).Item.useTime = 23;
		((ModItem)this).Item.useAnimation = 23;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 12);
		((ModItem)this).Item.rare = 1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("DemoniteJavelin").Type;
		((ModItem)this).Item.shootSpeed = 8f;
		((ModItem)this).Item.useTurn = true;
		((ModItem)this).Item.maxStack = 1;
		((ModItem)this).Item.noUseGraphic = true;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(57, 10);
		val.AddTile(16);
		val.Register();
	}
}
