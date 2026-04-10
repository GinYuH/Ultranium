using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Guardians.Nature;

public class UltraniumScepter : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Nature Scepter");
		// ((ModItem)this).Tooltip.SetDefault("Summons a miniature ultrum to fight with you\nOnly one can be summoned at once\nDoes not take up any minion slots");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.mana = 35;
		((ModItem)this).Item.damage = 200;
		((Entity)(object)((ModItem)this).Item).width = 26;
		((Entity)(object)((ModItem)this).Item).height = 26;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.useStyle = 1;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.DamageType = DamageClass.Summon;
		((ModItem)this).Item.knockBack = 0f;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.rare = 11;
		((ModItem)this).Item.UseSound = SoundID.Item44;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("Ultrum").Type;
		((ModItem)this).Item.shootSpeed = 10f;
		((ModItem)this).Item.buffType = ((ModItem)this).Mod.Find<ModBuff>("UltrumBuff").Type;
		((ModItem)this).Item.buffTime = 3600;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips[0].OverrideColor = new Color(241, 166, 0);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (player.ownedProjectileCounts[((ModItem)this).Mod.Find<ModProjectile>("Ultrum").Type] > 0)
		{
			return false;
		}
		return true;
	}

	public override bool? UseItem(Player player)/* tModPorter Suggestion: Return null instead of false */
	{
		if (player.altFunctionUse == 2)
		{
			player.MinionNPCTargetAim();
		}
		return ((ModItem)this).UseItem(player);
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
