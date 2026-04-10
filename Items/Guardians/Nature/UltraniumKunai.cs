using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraniumKunai : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).Tooltip.SetDefault("Throws homing Ultranium kunai blades");
		// ((ModItem)this).DisplayName.SetDefault("Ultranium Kunai");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 230;
		((ModItem)this).Item.DamageType = DamageClass.Ranged;
		((Entity)(object)((ModItem)this).Item).width = 42;
		((Entity)(object)((ModItem)this).Item).height = 42;
		((ModItem)this).Item.useTime = 15;
		((ModItem)this).Item.useAnimation = 15;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.knockBack = 8f;
		((ModItem)this).Item.noUseGraphic = true;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.UseSound = SoundID.Item60;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("UltraniumKunai").Type;
		((ModItem)this).Item.shootSpeed = 15f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "UltrumShard", 10);
		val.AddTile(412);
		val.Register();
	}
}
