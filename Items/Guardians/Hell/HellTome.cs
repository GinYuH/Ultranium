using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Hell;

public class HellTome : ModItem
{
	public override void SetStaticDefaults()
	{
		Tooltip.SetDefault("Shoots a molten glob that explodes into bolts when it hits enemies");
		DisplayName.SetDefault("Molten Purge");
	}

	public override void SetDefaults()
	{
		Item.damage = 200;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 25;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 26;
		Item.useAnimation = 26;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 6f;
		Item.rare = 11;
		Item.value = Item.buyPrice(1);
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("FlameGlob").Type;
		Item.shootSpeed = 16f;
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
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "HellShard", 10);
		val.AddTile(412);
		val.Register();
	}
}
