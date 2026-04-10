using Terraria;
using Terraria.ModLoader;

namespace Ultranium.Items.BossBags;

public class AldinBag : ModItem
{
	public override int BossBagNPC => ((ModItem)this).Mod.Find<ModNPC>("Aldin").Type;

	public override void SetStaticDefaults()
	{
		// ((ModItem)this).DisplayName.SetDefault("Treasure Bag");
		// ((ModItem)this).Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
	}

	public override void SetDefaults()
	{
		((ModItem)this).Item.maxStack = 999;
		((ModItem)this).Item.consumable = true;
		((Entity)(object)((ModItem)this).Item).width = 36;
		((Entity)(object)((ModItem)this).Item).height = 34;
		((ModItem)this).Item.rare = -12;
		((ModItem)this).Item.expert = true;
	}

	public override bool CanRightClick()
	{
		return true;
	}

	public override void OpenBossBag(Player player)
	{
		player.TryGettingDevArmor();
		int num = Main.rand.Next(3);
		if (num == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("CosmicBlade").Type, 1);
		}
		if (num == 1)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("CosmicBow").Type, 1);
		}
		if (num == 2)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("CosmicStaff").Type, 1);
		}
		int num2 = Main.rand.Next(3);
		if (num2 == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("AldinHood").Type, 1);
		}
		if (num2 == 1)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("AldinBody").Type, 1);
		}
		if (num2 == 2)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("AldinRobe").Type, 1);
		}
		if (Main.rand.Next(10) == 0)
		{
			player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("CosmicIdol").Type, 1);
		}
		player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("CosmicWings").Type, 1);
		if (Main.rand.Next(20) == 0)
		{
			int num3 = Main.rand.Next(4);
			if (num3 == 0)
			{
				player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("RayGun").Type, 1);
			}
			if (num3 == 1)
			{
				player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DevotedKatana").Type, 1);
			}
			if (num3 == 3)
			{
				player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("ShadowFlute").Type, 1);
			}
			if (num3 == 4)
			{
				player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("DemonicSingularity").Type, 1);
			}
			if (num3 == 5)
			{
				player.QuickSpawnItem(((ModItem)this).Mod.Find<ModItem>("Necromicon").Type, 1);
			}
		}
	}
}
