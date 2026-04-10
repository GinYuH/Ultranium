using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Dread.TrueDread;

public class DreadScepter : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Dread Energy Scepter");
		// ((ModItem)this).Tooltip.SetDefault("Summons stationary dread orbs to shoot bolts at nearby enemies\nEach orb will last for 30 seconds, and will then explode into a giant circle of bolts upon death\nEach existing orb takes up 1 minion slot");
	}

	public override void SetDefaults()
	{
		((Entity)(object)((ModItem)this).Item).width = 26;
		((Entity)(object)((ModItem)this).Item).height = 28;
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.mana = 30;
		((ModItem)this).Item.damage = 200;
		((ModItem)this).Item.knockBack = 1f;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.DamageType = DamageClass.Summon;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.UseSound = SoundID.Item117;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("DreadSummonOrb").Type;
		((ModItem)this).Item.shootSpeed = 10f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		Vector2 vector = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
		position = vector;
		return true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(200, 0, 0);
	}

	public override void AddRecipes()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Recipe val = /* ((ModItem)this) */Recipe.Create((ModItem)(object)this.Type, 1);
		val.AddIngredient((Mod)null, "NightmareFuel", 10);
		val.AddIngredient((Mod)null, "DreadScale", 6);
		val.AddTile(412);
		val.Register();
	}
}
