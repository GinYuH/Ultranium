using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class SpazBomb : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Spazma-Bomb");
		// ((ModItem)this).Tooltip.SetDefault("Explodes on death, and inflicts the \"Cursed Inferno\" debuff");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.useStyle = 1;
		((Entity)(object)((ModItem)this).Item).width = 30;
		((Entity)(object)((ModItem)this).Item).height = 26;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("C4Pro").Type;
		((ModItem)this).Item.shootSpeed = 7f;
		((ModItem)this).Item.knockBack = 2.5f;
		((ModItem)this).Item.damage = 43;
		((ModItem)this).Item.value = Item.buyPrice(0, 30);
		((ModItem)this).Item.rare = 5;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(1225, 10);
		val.AddIngredient(549, 5);
		val.AddTile(134);
		val.Register();
	}
}
