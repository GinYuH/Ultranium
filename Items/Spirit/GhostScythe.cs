using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Spirit;

public class GhostScythe : ModItem
{
	public override void SetStaticDefaults()
	{
		// DisplayName.SetDefault("Phantom Slicer");
		// Tooltip.SetDefault("Throws ectoplasmic scythes images");
	}

	public override void SetDefaults()
	{
		Item.damage = 60;
		((Entity)(object)Item).width = 62;
		((Entity)(object)Item).height = 62;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = 1;
		Item.knockBack = 6f;
		Item.value = Item.buyPrice(0, 55, 50);
		Item.rare = 8;
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		Item.noUseGraphic = true;
		Item.alpha = 60;
		Item.shoot = Mod.Find<ModProjectile>("SoulScytheProjectile").Type;
		Item.shootSpeed = 12f;
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient(3261, 12);
		val.AddTile(134);
		val.Register();
	}
}
