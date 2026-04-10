using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class LunaticFireStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Solar Flare Staff");
		// ((ModItem)this).Tooltip.SetDefault("Shoots solar flare bolts");
		Item.staff[((ModItem)this).Item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.damage = 80;
		((ModItem)this).Item.DamageType = DamageClass.Magic;
		((ModItem)this).Item.mana = 22;
		((Entity)(object)((ModItem)this).Item).width = 58;
		((Entity)(object)((ModItem)this).Item).height = 56;
		((ModItem)this).Item.useTime = 30;
		((ModItem)this).Item.useAnimation = 30;
		((ModItem)this).Item.useStyle = 5;
		((ModItem)this).Item.noMelee = true;
		((ModItem)this).Item.knockBack = 5f;
		((ModItem)this).Item.value = Item.buyPrice(1);
		((ModItem)this).Item.rare = 8;
		((ModItem)this).Item.UseSound = SoundID.Item20;
		((ModItem)this).Item.autoReuse = true;
		((ModItem)this).Item.shoot = ((ModItem)this).Mod.Find<ModProjectile>("CultistFire").Type;
		((ModItem)this).Item.shootSpeed = 10f;
	}
}
