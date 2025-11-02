const GAME_DATA = {
    heroes: [
        {
            id: 'auron',
            name: 'Auron',
            title: 'Solaris Vanguard',
            role: 'Balanced DPS',
            weapon: 'Greatsword',
            image: 'attached_assets/generated_images/Auron_hero_character_sprite_8cfb17d1.png',
            lore: 'First Lightbringer, awakened from 1000-year stasis. Seeks redemption for failing to prevent the Eclipse.',
            stats: {
                hp: 500,
                atk: 45,
                def: 20,
                spd: 100,
                crit: 0.05
            },
            skills: [
                {
                    name: 'Solar Cleave',
                    cooldown: 3.0,
                    damage: 1.8,
                    description: 'Swing greatsword in 180° arc, dealing 180% ATK damage'
                },
                {
                    name: 'Eclipse Breaker',
                    cooldown: 15.0,
                    damage: 3.5,
                    description: 'Leap and slam ground, 350% ATK AoE damage, stun 1.5s',
                    isUltimate: true
                }
            ]
        },
        {
            id: 'lyra',
            name: 'Lyra',
            title: 'Lightweaver',
            role: 'Healer / Support',
            weapon: 'Staff',
            image: 'attached_assets/generated_images/Lyra_healer_character_sprite_96d4ac30.png',
            lore: 'Priestess of the Twilight Circle who defected to help Sanctuary. Believes in redemption through light.',
            stats: {
                hp: 380,
                atk: 30,
                def: 15,
                spd: 95,
                crit: 0.03
            },
            skills: [
                {
                    name: 'Healing Radiance',
                    cooldown: 5.0,
                    heal: 1.2,
                    description: 'Heal nearest ally for 120% ATK, +10% DEF for 5s'
                },
                {
                    name: 'Sanctuary Blessing',
                    cooldown: 20.0,
                    heal: 0.8,
                    description: 'AoE heal all allies for 80% ATK, cleanse debuffs',
                    isUltimate: true
                }
            ]
        },
        {
            id: 'ronan',
            name: 'Ronan',
            title: 'Ironclad Sentinel',
            role: 'Tank / Defender',
            weapon: 'Shield & Mace',
            image: 'attached_assets/generated_images/Ronan_tank_character_sprite_09614f5a.png',
            lore: 'Former Iron Covenant captain. Lost his squad to Umbra, now fights to protect what remains.',
            stats: {
                hp: 700,
                atk: 35,
                def: 35,
                spd: 85,
                crit: 0.02
            },
            skills: [
                {
                    name: 'Shield Bash',
                    cooldown: 4.0,
                    damage: 1.2,
                    description: 'Charge forward, dealing 120% ATK and stunning'
                },
                {
                    name: 'Unbreakable Bulwark',
                    cooldown: 18.0,
                    defense: 0.5,
                    description: 'Taunt all enemies, gain +50% DEF for 6s',
                    isUltimate: true
                }
            ]
        },
        {
            id: 'cira',
            name: 'Cira',
            title: 'Stormcaller',
            role: 'Burst Mage / AoE',
            weapon: 'Elemental Orbs',
            image: 'attached_assets/generated_images/Cira_mage_character_sprite_84c9f07c.png',
            lore: 'Prodigy from Twilight Circle. Studies Umbra to turn their power against them. Volatile and ambitious.',
            stats: {
                hp: 350,
                atk: 55,
                def: 12,
                spd: 90,
                crit: 0.08
            },
            skills: [
                {
                    name: 'Arcane Missiles',
                    cooldown: 3.5,
                    damage: 0.6,
                    count: 5,
                    description: 'Fire 5 homing projectiles, each dealing 60% ATK'
                },
                {
                    name: 'Meteor Storm',
                    cooldown: 16.0,
                    damage: 2.0,
                    description: 'Call meteors dealing 200% ATK, burn enemies',
                    isUltimate: true
                }
            ]
        },
        {
            id: 'milo',
            name: 'Milo',
            title: 'Shadowstep',
            role: 'Rogue / Single-Target DPS',
            weapon: 'Dual Daggers',
            image: 'attached_assets/generated_images/Milo_rogue_character_sprite_6bf87aa4.png',
            lore: 'Ex-thief from underground. Knows Umbra-corrupted zones better than anyone. Cynical but loyal.',
            stats: {
                hp: 420,
                atk: 50,
                def: 18,
                spd: 120,
                crit: 0.12
            },
            skills: [
                {
                    name: 'Shadowstep',
                    cooldown: 2.5,
                    damage: 1.0,
                    description: 'Dash behind target; next attack has 100% crit chance'
                },
                {
                    name: 'Blade Dance',
                    cooldown: 14.0,
                    damage: 1.5,
                    count: 8,
                    description: '8 rapid strikes, each dealing 150% ATK',
                    isUltimate: true
                }
            ]
        }
    ],

    story: [
        {
            text: "A thousand years ago, the Eternal Sun vanished during a cosmic event called The Eclipse. The world plunged into twilight, overrun by shadow creatures called Umbra.",
            chapter: 1
        },
        {
            text: "Humanity retreated to the last bastion: Sanctuary, a mystical citadel powered by Solar Shards—crystallized fragments of the lost sun.",
            chapter: 1
        },
        {
            text: "You are the Lightbringer, commander of the Solaris Guard. Your mission: reclaim corrupted lands, gather Solar Shards, and reignite the sun.",
            chapter: 1
        },
        {
            text: "But first, you must rebuild Sanctuary and train your heroes...",
            chapter: 1
        }
    ],

    enemies: {
        basic_shade: {
            name: 'Shade',
            hp: 100,
            atk: 15,
            def: 5,
            xp: 25,
            gold: 15,
            image: 'attached_assets/generated_images/Basic_shade_enemy_sprite_a04ae306.png'
        },
        elite_shade: {
            name: 'Elite Shade',
            hp: 200,
            atk: 25,
            def: 10,
            xp: 50,
            gold: 30,
            image: 'attached_assets/generated_images/Basic_shade_enemy_sprite_a04ae306.png'
        },
        boss_gorath: {
            name: 'Gorath - Shade Warden',
            hp: 500,
            atk: 40,
            def: 20,
            xp: 150,
            gold: 100,
            image: 'attached_assets/generated_images/Gorath_boss_enemy_sprite_978e28a4.png'
        }
    },

    buildings: {
        barracks: { name: 'Barracks', level: 1, maxLevel: 15, cost: 500 },
        workshop: { name: 'Workshop', level: 1, maxLevel: 12, cost: 800 },
        reactor: { name: 'Shard Reactor', level: 1, maxLevel: 10, cost: 1200 },
        treasury: { name: 'Treasury', level: 1, maxLevel: 12, cost: 600 }
    },

    xpTable: {
        1: 0, 2: 150, 3: 336, 4: 559, 5: 820,
        6: 1120, 7: 1461, 8: 1843, 9: 2267, 10: 2734
    }
};
