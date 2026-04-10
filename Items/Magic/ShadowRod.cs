using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Ultranium.Items.Magic;

public class ShadowRod : ModItem
{
	public override void SetStaticDefaults()
	{
		((ModItem)this).Tooltip.SetDefault("Conjures a short lived shadow bolt that pierces through enemies");
		Item.staff[((ModItem)this).item.type] = true;
	}

	public override void SetDefaults()
	{
		((ModItem)this).item.damage = 20;
		((ModItem)this).item.magic = true;
		((ModItem)this).item.mana = 10;
		((Entity)(object)((ModItem)this).item).width = 40;
		((Entity)(object)((ModItem)this).item).height = 40;
		((ModItem)this).item.useTime = 40;
		((ModItem)this).item.useAnimation = 40;
		((ModItem)this).item.useStyle = 5;
		((ModItem)this).item.noMelee = true;
		((ModItem)this).item.knockBack = 5f;
		((ModItem)this).item.value = 10000;
		((ModItem)this).item.rare = 2;
		((ModItem)this).item.value = Item.buyPrice(0, 10);
		((ModItem)this).item.UseSound = SoundID.Item20;
		((ModItem)this).item.autoReuse = true;
		((ModItem)this).item.shoot = ((ModItem)this).mod.ProjectileType("ShadowRodBolt");
		((ModItem)this).item.shootSpeed = 5f;
	}
}
