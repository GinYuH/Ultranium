using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Eldritch.ShadowEvent;

public class FlayerStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		// Tooltip.SetDefault("Fires A whispering death bolt");
		// DisplayName.SetDefault("Death's Whisper");
		Item.staff[Item.type] = true;
	}

	public override void SetDefaults()
	{
		Item.damage = 185;
		Item.DamageType = DamageClass.Magic;
		Item.mana = 15;
		Item.width = 40;
		Item.height = 40;
		Item.useTime = 20;
		Item.useAnimation = 20;
		Item.useStyle = 5;
		Item.noMelee = true;
		Item.knockBack = 5f;
		Item.value = Item.buyPrice(1);
		Item.rare = 11;
		Item.UseSound = SoundID.Item20;
		Item.autoReuse = true;
		Item.shoot = Mod.Find<ModProjectile>("DeathBolt").Type;
		Item.shootSpeed = 10f;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(34, 166, 118);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create(Type, 1);
		val.AddIngredient((Mod)null, "DarkMatter", 32);
		val.AddIngredient((Mod)null, "EldritchBlood", 8);
		val.AddTile(412);
		val.Register();
	}
}
