# BioRand: A Resident Evil Randomizer
A new randomizer for the classic Resident Evil games for PC. Supports sophisticated key item placement, randomized non-key items, randomized enemies, randomized NPCs with matching random voices and random background music. All highly configurable and easy to share seeds and configurations.

The randomizer currently only supports the Sourcenext port of Resident Evil 2 with the Classic REbirth patch.

<a href="https://openrct2.io">
    <img src="docs/images/screenshot.png" style="width: 256px;" alt="BioRand screenshot"/>
</a>

## How to use

1. Download the latest release from https://github.com/IntelOrca/biorand/releases.
2. Extract all the files to a directory of your choice.
3. Run `biorand.exe` and type or browse for your RE2 game directory under the generate box. If browsing, select your `bio2 1.10.exe` file.
4. Configure your settings, click `seed` for a random seed and then click generate.
5. Run RE2 and select the mod: "BioRand: A Resident Evil Randomizer" from the list.

## Reporting issues
Please report any bugs, crashes or suggestions by raising an issue on https://github.com/OpenRCT2/OpenRCT2/issues.
Include the seed you were using in your report.

## Features

### Door randomization (coming soon)
All doors will be changed to link to different rooms. A graph is constructed to provide an alternative route through the game, key items must still be picked up and doors must still be unlocked.

### Key item randomization
This randomizer is able to place key items accordingly so that they always appear in a location that can be accessed prior to the door or object that requires the key. There are two options that affect the placement:

#### Allow alternative routes

This will spawn key items in such a way that you may not be able to unlock doors or use objects in the same order as the original game. For example, in Resident Evil 2, the heart key might be spawned in the main hall, and the spade key might be spawned in the basement. This means you **must** first visit the basement before you can visit the library.

#### Protect from soft lock

This will ensure all key items are placed within an area of the game that is accessible from the door or object that requires the key item. If this is disabled, lab key items can be placed in the police station. The player must therefore be thorough and check every item to make sure all key items are collected before activating any point-of-no-return.

In Resident Evil 2, there are 4 significant points-of-no-return:
* The gate leading to the front of the police station.
* The door leading to the sewer (activates cutscene of Tyrant / Berkin)
* The cable car transporting you from the sewer to the marshaling yard.
* The train / lift transporting you from the marshaling yard to the lab.

### Non-key item randomization

Each compatible weapon is randomly placed. Some may not be placed at all.
Only ammo for weapons that are placed in the game will be placed. You will not find ammo for a weapon that you will never pick up.
The ratio of ammo, health and ink ribbons can be adjusted. Setting the ratio to 0 will ensure that items of that kind are never placed.
The average quantity of ammo found in each location can be adjusted.

### Enemy randomization

Enemies can be randomized with a difficulty value. An easy difficulty will more likely spawn slower, and easier to dodge enemies such as crows, ivies, spiders, and zombies.
A higher difficulty will more likely spawn faster, and more lethal enemies such as cerebrus', lickers, and tyrants.

### Character, voice randomization

Randomizes all NPCs in the game and their voice lines. Voice lines are picked based on the character that is swapped in.

### Background music randomization

All background music tracks are shuffled. Music is shuffled by genre, so danger tracks are swapped with other danger tracks, calm tracks are swapped with other calm tracks.
Some rooms contining only ambient sound effects, such as the power room in the basement are replaced with music tracks.

## License
BioRand is licensed under the MIT License.
