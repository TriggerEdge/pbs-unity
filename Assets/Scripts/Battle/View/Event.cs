﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PBS.Battle.View.Events
{
    /// <summary>
    /// Displays events to the player's view.
    /// </summary>
    public class Base { 

    }

    // Battle Phases

    public class StartBattle : Base
    {

    }
    public class EndBattle : Base
    {
        public int winningTeam;
    }

    // Messages

    /// <summary>
    /// Displays a message to the player's dialog box.
    /// </summary>
    public class Message : Base
    {
        public string message;
    }
    public class MessageParameterized : Base
    {
        public string messageCode;
        public int playerPerspectiveID = 0;
        public int teamPerspectiveID = 0;

        public string pokemonID = "";
        public string pokemonUserID = "";
        public string pokemonTargetID = "";
        public List<string> pokemonListIDs = new List<string>();

        public int trainerID = -1;

        public int teamID = -1;

        public string typeID = "";
        public List<string> typeIDs = new List<string>();

        public string moveID = "";
        public List<string> moveIDs = new List<string>();

        public string abilityID = "";
        public List<string> abilityIDs = new List<string>();

        public string itemID = "";
        public List<string> itemIDs = new List<string>();

        public string statusID = "";
        public string statusTeamID = "";
        public string statusEnvironmentID = "";

        public List<int> intArgs = new List<int>();
        public List<PokemonStats> statList = new List<PokemonStats>();
    }
    public class MessagePokemon : Base
    {
        public string preMessage = "";
        public string postMessage = "";
        public List<string> pokemonUniqueIDs;
    }
    public class MessageTrainer : Base
    {
        public string preMessage = "";
        public string postMessage = "";
        public List<int> playerIDs;
    }
    public class MessageTeam : Base
    {
        public string preMessage = "";
        public string postMessage = "";
        public int teamID;
    }

    // Backend
    public class ModelUpdate : Base
    {
        public enum UpdateType
        {
            None,
            LoadAssets
        }
        public UpdateType updateType;
        public bool synchronize = true;
        public Battle.View.Model model;
    }

    // Command Prompts
    public class CommandAgent
    {
        public class Moveslot
        {
            public string moveID;
            public int PP;
            public int maxPP;

            public int basePower;
            public float accuracy;

            public bool useable = true;
            public string failMessageCode = "";

            public Moveslot() { }
            public Moveslot(string moveID)
            {
                MoveData moveData = MoveDatabase.instance.GetMoveData(moveID);
                this.moveID = moveID;
                this.PP = moveData.PP;
                this.maxPP = moveData.PP;
                this.basePower = moveData.basePower;
                this.accuracy = moveData.accuracy;
            }
        }

        public string pokemonUniqueID;
        public bool canMegaEvolve = false;
        public bool canZMove = false;
        public bool canDynamax = false;

        public List<BattleCommandType> commandTypes;
        public List<Moveslot> moveslots;
        public List<Moveslot> zMoveSlots;
        public List<Moveslot> dynamaxMoveSlots;
    }
    public class CommandGeneralPrompt : Base
    {
        public int playerID;
        public bool canMegaEvolve;
        public bool canZMove;
        public bool canDynamax;

        public List<string> items;
        public List<CommandAgent> pokemonToCommand;
    }
    public class CommandReplacementPrompt : Base
    {
        public int playerID;
        public int[] fillPositions;
    }

    // Trainer Interactions
    public class TrainerSendOut : Base
    {
        public int playerID;
        public List<string> pokemonUniqueIDs;
    }
    public class TrainerMultiSendOut : Base
    {
        public List<TrainerSendOut> sendEvents;
    }
    public class TrainerWithdraw : Base
    {
        public int playerID;
        public List<string> pokemonUniqueIDs;
    }
    public class TrainerItemUse : Base
    {
        public int playerID;
        public string itemID;
    }

    // Weather / Environmental Conditions
    public class EnvironmentalConditionStart : Base
    {
        public string conditionID;
    }
    public class EnvironmentalConditionEnd : Base
    {
        public string conditionID;
    }


    // --- Pokemon Interactions ---

    // General
    public class PokemonChangeForm : Base
    {
        public string pokemonUniqueID;
        public string preForm;
        public string postForm;
    }
    public class PokemonSwitchPosition : Base
    {
        public string pokemonUniqueID1;
        public string pokemonUniqueID2;
    }

    // Health
    public class PokemonHealthDamage : Base
    {
        public string pokemonUniqueID;
        public int preHP;
        public int postHP;
        public int damageDealt
        {
            get
            {
                return preHP - postHP;
            }
        }
    }
    public class PokemonHealthHeal : Base
    {
        public string pokemonUniqueID;
        public int preHP;
        public int postHP;
        public int hpHealed
        {
            get
            {
                return postHP - preHP;
            }
        }
    }
    public class PokemonHealthFaint : Base
    {
        public string pokemonUniqueID;
    }
    public class PokemonHealthRevive : Base
    {
        public string pokemonUniqueID;
    }

    // Abilities
    public class PokemonAbilityActivate : Base
    {
        public string pokemonUniqueID;
        public string abilityID;
    }
    public class PokemonAbilityQuickDraw : PokemonAbilityActivate { }

    // Moves
    public class PokemonMoveUse : Base
    {
        public string pokemonUniqueID;
        public string moveID;
    }
    
    public class PokemonMoveHitTarget
    {
        public string pokemonUniqueID;
        public bool affectedByMove = false;
        public bool missed = false;
        public bool criticalHit = false;
        public float effectiveness = 1f;

        public PokemonMoveHitTarget() { }
        public PokemonMoveHitTarget(BattleHitTarget hitTarget)
        {
            pokemonUniqueID = hitTarget.pokemon.uniqueID;
            missed = hitTarget.missed;
            criticalHit = hitTarget.criticalHit;
            effectiveness = hitTarget.effectiveness.GetTotalEffectiveness();
        }
    }
    public class PokemonMoveHit : Base
    {
        public string pokemonUniqueID;
        public string moveID;
        public int currentHit = 1;
        public List<PokemonMoveHitTarget> hitTargets;
    }
    
    public class PokemonMoveCelebrate : Base { }

    // Stats
    public class PokemonStatChange : Base
    {
        public string pokemonUniqueID;
        public int modValue;
        public bool runAnim = false;
        public bool maximize = false;
        public bool minimize = false;
        public List<PokemonStats> statsToMod;
    }
    public class PokemonStatUnchangeable : Base
    {
        public string pokemonUniqueID;
        public bool tooHigh;
        public List<PokemonStats> statsToMod;
    }

    // Items
    public class PokemonItemQuickClaw : Base
    {
        public string pokemonUniqueID;
        public string itemID;
    }

    // Status

    // Misc
    public class PokemonMiscProtect : Base
    {
        public string pokemonUniqueID;
    }
    public class PokemonMiscMatBlock : Base
    {
        public int teamID;
    }


}

