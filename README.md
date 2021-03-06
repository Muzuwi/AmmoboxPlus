[icon]: https://raw.githubusercontent.com/Muzuwi/AmmoboxPlus/master/icon.png

# ![icon] Ammobox+
Expanding ammo variety for Terraria [tModLoader]

* [**Known Issues**](#known-issues)
* [**Changelog**](#changelog)
* [**Download**](#download)
* [**Dev Team**](#dev-team)
* [**For Modders and contributors: Adding compatibility with Ammobox+**](https://github.com/Muzuwi/AmmoboxPlus/blob/master/CONTRIBUTING.md)

Ammobox+ is a mod that's entirely dedicated to the ranger specialization of Terraria, and adds 43 new ammo types with new effects and their own recipes to the game.

Examples of implemented ammo types (full list on the forum post):

- Rubber Bullet - has a very high knockback
- Peculiar Bullet - has a chance to turn enemies into a Bunny. Does **not** count as a kill
- Drugged Bullet - creates an aura of damage around a shot enemy. Other drugged enemies are not affected by the aura.
- Ice Bullet - slows down enemies, with a chance to freeze them. The chance is doubled in water.
- Markershot - shot enemy takes 15% more damage
- Miner Bullet - has a low chance of dropping a random amount of ore
- Spectral Bullet - can penetrate up to 4 blocks, but the accuracy decreases substantially with each block
- Starfall Bullet - has a low chance to turn into a star

- And more! Arrow, Rocket and Dart updates add 35 new ammo types to the game!

## Known Issues
- Ice/Slime ammo may rarely cause erratic movement in enemies, please report such cases on the Issue Tracker on GitHub
- Debuffs do not apply to players in PvP
- **Rocket special effects only work with Rocket Launcher/Grenade Launcher/Snowman Cannon/Proximity Mine Launcher (for now, Terraria's implementation of rockets makes it very hard to add custom ones that work with all launchers, and each rocket has to be added specifically for every launcher.**

## Changelog

- v1.5.0.1
	* Updated for tModLoader v0.11.6.2
	* Fix a crash when spawning dev weapons when no weapon conditions were met
	* Fix double spawning of rocket effects on MP

- v1.5.0.0 - Bugfix Update
	* New Feature - Ammo Belts! They allow you to switch between ammo in your ammo slots
	* Drugged/Cactus ammo now respect the npc.dontTakeDamage (fixes weird behaviour with Golem)
	* Fix phantasmal ammo flying straight through blocks when shot at a certain angle
	* Add ice sound effect
	* Potential compatibility for rockets with modded launchers (might not actually work)
	* Fixed aiType for pre-hardmode bullets

- v1.4.4.1 - Endless update
	* Fix miner ammo dropping lunar ore/ammoboxes dropping post-ML ammo after defeating any mech boss
	* Fix post-plantera ammo dropping from ammoboxes after defeating any mech boss
	* Added Call abilities, modders can now use several different commands to enhance compatibility of their mods with Ammobox+
	* New UI Element: Ammo Display - Shows ammo that will be shot from held weapon. Off by default - Default hotkey is 'P'
	* Add Ammo Display
	* Add Dev weapons (reskins of certain vanilla weapons)
	* Minor optimizations

- v1.3.3.1 - Add rockets to Ammoboxes

- v1.3.3.0 - Rocket update! 13 new rocket types added 

	* **special effects only work with Rocket Launcher/Grenade Launcher/Snowman Cannon/Proximity Mine Launcher.** Celebration, Electrosphere Launcher and modded launchers can use the rockets, however they behave just like Rocket III/IV's, and have no special effects (to be changed in the future, this is the case mainly because of the way Terraria implements rockets and the launchers) 
	* Fix ammo dropped from ammoboxes very rarely getting prefixes
	* Fix bullet hitboxes

- v1.2.2.9 - Fix possible multiplayer Ice/Slime desync
	* Changed ice ammo recipe
	* Changed slime ammo recipe
	* Nerfed slime slowdown
	* Possibly a few more things I forgot


- v1.2.2.7 - **Arrow Update!** 10 new arrow types, 2 new items (Ammo Box, Ammo Box Plus)
	* Fix cactus dart rarity
	* Changed rubber bullet recipe, now requires 10 pink gel (previously 1)
	* Reworked cactus shield, should be more reliable now
	* Fixed Marked multiplying the multiplier instead of adding onto it
	* Reworked drugged status, more visual feedback (?)
	* New system for miner ammo, preparation for cross-mod support
	* New bullet projectiles, vanilla style
	* New light effects for ammo
	* Turn down the effects for bullets

- v1.1.2.4 - **Dart Update!** 12 new ammo types (Darts), ranging from Pre-Hardmode to Hardmode.
	* Netsync should be more stable now
	* Redid slime and ice effects, should work without any bugs on most enemies (a blacklist for certain enemies still exists)
- v1.0.2.2 - Fixed Starfall Bullet recipe

## Download

  Use the in-game Mod Browser to download the mod, or visit the **[Releases](https://github.com/Muzuwi/AmmoboxPlus/releases)** tab on this repo for current and past releases. 

## Planned Features
- Better cross-mod compatibility

## Dev Team

   **Kranot** - graphical design
   
   **Muzuwi** - programming
