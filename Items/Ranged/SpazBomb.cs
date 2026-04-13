using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Ranged;

public class SpazBomb : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Spazma-Bomb");
		//Tooltip.SetDefault("Explodes on death, and inflicts the \"Cursed Inferno\" debuff");
	}

	public override void SetDefaults()
	{
		Item.useStyle = 1;
		((Entity)(object)Item).width = 30;
		((Entity)(object)Item).height = 26;
		Item.noUseGraphic = true;
		Item.DamageType = DamageClass.Ranged;
		Item.noMelee = true;
		Item.autoReuse = true;
		Item.UseSound = SoundID.Item1;
		Item.useAnimation = 30;
		Item.useTime = 30;
		Item.shoot = Mod.Find<ModProjectile>("C4Pro").Type;
		Item.shootSpeed = 7f;
		Item.knockBack = 2.5f;
		Item.damage = 43;
		Item.value = Item.buyPrice(0, 30);
		Item.rare = 5;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient(1225, 10);
		val.AddIngredient(549, 5);
		val.AddTile(134);
		val.Register();
	}
}
