using FMODUnity;

public class ObjectReferenceBank
{
    public enum UnitWorkerEvents
    {
        SFXUnitWorkerFootstep,
        SFXUnitWorkerShutDown,
        SFXUnitWorkerPowerUp,
        SFXUnitWorkerThrowPunch,
        SFXUnitWorkerHitPunch,
        SFXUnitWorkerDropLoot,
        SFXUnitWorkerCollectLoot,
        SFXUnitWorkerIdle,
        SFXUnitWorkerBuilding,
        SFXUnitWorkerDeath
    }

    public enum UnitReconEvents
    {
        SFXUnitReconFootstep,
        SFXUnitReconShutDown,
        SFXUnitReconPowerUp,
        SFXUnitReconShot,
        SFXUnitReconCollectLoot,
        SFXUnitReconIdle,
        SFXUnitReconDeath
    }

    public enum UnitFighterEvents
    {
        SFXUnitFighterMoving,
        SFXUnitFighterShutDown,
        SFXUnitFighterPowerUp,
        SFXUnitFighterShooting,
        SFXUnitFighterOverheating,
        SFXUnitFighterMelee,
        SFXUnitFighterIdle,
        SFXUnitfighterDeath
    }

    public enum EnemyScreamerEvents
    {
        SFXEnemyScreamerVocalisations,
        SFXEnemyScreamerSteps,
        SFXEnemyScreamerAttack,
        SFXEnemyScreamerAttackImpact,
        SFXEnemyScreamerScream,
        SFXEnemyScreamerDeath
    }

    public enum  UIEvents
    {
        SFXMenuUIHover,
        SFXMenuUIClick,
        SFXMenuUIDeselect,
        SFXMenuUIError,
        SFXDeployMenuUITooHeavy
    }

    public enum AmbienceEvents
    {
        AMBBaseLevelBirdsong,
        AMBBaseLevelWind,
        AMBBaseLevelCritters,
        AMBBaseLevelRoars
    }

    public enum MusicEvents
    {
        MUSDeployMenuMainTheme,
        MUSDeathMenuLostTheme,
        MUSExtractionMenuTheme,
        MUSBaseLevelMainLoop,
        MUSBaseLevelCombatLoop
    }
}
