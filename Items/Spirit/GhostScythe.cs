using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Spirit;

public class GhostScythe : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Phantom Slicer");
		// ((ModItem)this).Tooltip.SetDefault("Throws ectoplasmic scythes images");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 60;
		((Entity)(object)((ModItem)this).Item).width = 62;
		((Entity)(object)((ModItem)this).Item).height = 62;
		((ModItem)this).Item.useTime = 20;
		((ModItem)this).Item.useAnimation = 20;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 6f;
		((ModItem)this).Item.value = Item.buyPrice(0, 55, 50);
		((ModItem)this).Item.rare = 8;
		((ModItem)this).Item.UseSound = SoundID.Item1;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.alpha = 60;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("SoulScytheProjectile").Type;
		((ModItem)this).Item.shootSpeed = 12f;
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
