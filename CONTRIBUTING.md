## Adding compatibility with Ammobox+ to your mod
Ammobox+ adds several Call commands for other modders to use. The first argument is the command string (as seen below), following arguments are listed below for each command.

**Important note!** Lists containing modded boss bags and the NPC blacklist are **not** reset during runtime - no need to add them multiple times (Good to use PostSetupContent for adding them).
However, Ammo and Ore lists are reset every time the player enters a world. Because of this, it is necessary to re-add them ([Ammobox+ does it in ModWorld.PostUpdate](https://github.com/Muzuwi/AmmoboxPlus/blob/master/AmmoboxWorld.cs#L23))

	AddOre(int id) - Adds an item with id to the drop table of miner ammo
	id - ItemID of the ore you want to add

	AddAmmo(bool hm, int id, int amount) - Adds ammo to the ammo box drop table
	hm - If true, the ammo will drop from Ammo Boxes+ (Hardmode), otherwise, normal Ammo Boxes
	id - ItemID of the ammo item
	amount - how many items to drop

	AddBossBag(bool hm, int id) - Adds your mods' boss bag to the list of bags that are allowed to drop Ammo Boxes
	hm - true if the boss bag belongs to a hardmode boss, otherwise false
	id - ItemID of the boss bag

	AddNPCToBlacklist(int id) - Blacklists an NPC from the effects of Ice and Slime ammo
	id - NPC type to blacklist
	
