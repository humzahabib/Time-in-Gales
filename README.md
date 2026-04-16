# Time in Gales

> *“The Most Thrilling Sci-Fi Showdown”*

A third-person top-down sci-fi shooter built in Unity (C#) for CS 353 – Game Design and Development, Summer 2024. You play as **Chronos**, an elite time guardian tasked with hunting down time anomalies across three eras — Year 2548, the Mesozoic Era, and Year 3178.

-----

## Team

|Name                |Role|
|--------------------|----|
|Muhammad Hamza Habib|—   |
|Muhammad Jawad      |—   |
|Syed Zain Ali Shah  |—   |
|Muhammad Farooq     |—   |
|Mahad Mustafa       |—   |

-----

## Status

**On hold.** Core gameplay systems are functional, but the following features were cut due to time constraints and remain unimplemented:

- Shop / upgrade store
- Power-ups (Shield, Decoy + Invisibility, Echoing Bullet, Time Slash)
- Free Play mode (post-campaign level replay)
- New Game+ (weapon carry-over)

-----

## What’s Implemented

- Third-person 2.5D movement (WASD + mouse aim)
- Weapon system with heat-up mechanic (Chrono Pistol, Temporal Rifle)
- Enemy AI via FSM + Unity NavMesh:
  - Time Freaks (idle → chase → attack → fury)
  - Time Beasts (idle → chase → stunned → charge attack)
  - Robot Enemies (idle → patrol → nervous → chase → attack)
- Wave-based enemy spawning
- Boss encounters (Time Beast, Robot Enemy, Rift Walker)
- Checkpoint system (respawn before boss)
- Auto-save after level completion
- Three levels with distinct environments and narrative beats
- Health regeneration and weapon heat-up HUD

-----

## Gameplay Overview

|Level                 |Era                            |Boss               |Est. Time|
|----------------------|-------------------------------|-------------------|---------|
|1 – Temporal Terrors  |Year 2548 (lab interior)       |Time Beast         |15 min   |
|2 – Primal Disturbance|Mesozoic Era (forest / outpost)|Rift Walker’s Robot|20 min   |
|3 – Final Rift        |Year 3178 (abandoned Earth)    |Rift Walker        |25 min   |

Each level escalates by reintroducing the previous boss type as a regular enemy alongside a new, stronger boss.

-----

## Controls

|Input            |Action                                   |
|-----------------|-----------------------------------------|
|WASD / Arrow Keys|Move                                     |
|Mouse            |Aim                                      |
|Left Click       |Primary Fire                             |
|Right Click      |Secondary Fire / Cooling                 |
|Shift            |Dodge / Roll                             |
|Q                |Defensive Ability *(not yet implemented)*|
|E                |Offensive Ability *(not yet implemented)*|
|F                |Interact                                 |

-----

## Tech Stack

- **Engine:** Unity (C#)
- **AI:** Finite State Machines + Unity NavMesh (A*)
- **Assets:** Kenney, Quaternius (free/open-license 3D assets)
- **Physics:** Unity standard physics (kinematic Rigidbody for player)

-----

## Enemy Stats (Reference)

|Enemy            |HP |Move Speed    |Notes                                  |
|-----------------|---|--------------|---------------------------------------|
|Time Freak       |50 |5             |60% fury proc on hit; drops gun in fury|
|Time Freak (Fury)|10 |12            |Dies after one attack                  |
|Time Beast       |200|5 / 10 (chase)|Stunned 5s if charged into wall        |
|Robot Enemy      |150|3             |Fires every 2s, 0.5s flash warning     |
|Rift Walker      |250|3             |Missiles + rotating laser + gun        |

-----

## Project Structure

```
Assets/
├── Scripts/
│   ├── Player/
│   ├── Enemies/
│   │   ├── TimeFreak/
│   │   ├── TimeBeast/
│   │   └── RobotEnemy/
│   ├── Weapons/
│   ├── UI/
│   └── GameManager/
├── Scenes/
│   ├── Level1_TemporalTerrors
│   ├── Level2_PrimalDisturbance
│   └── Level3_FinalRift
├── Prefabs/
├── Audio/
└── Art/
```

-----

## Known Issues / Future Work

- [ ] Implement shop and Time Remnants economy
- [ ] Implement all four power-ups
- [ ] Balance weapon heat-up values (Plasma Gun, Laser Machine Gun)
- [ ] Add Free Play mode
- [ ] Add achievement system (The Beast Slayer, The Master, etc.)
- [ ] Voice lines for Sarah (support agent narration)
- [ ] Collectible pieces (development team photo)
- [ ] Easy difficulty mode

-----

## License

Assets by [Kenney](https://kenney.nl) and [Quaternius](https://quaternius.com) are used under their respective free/open licenses. All original code and design is property of the project team.
