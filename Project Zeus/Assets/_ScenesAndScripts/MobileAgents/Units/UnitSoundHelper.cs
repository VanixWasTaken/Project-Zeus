using UnityEngine;
using static FMODUnity.RuntimeManager;
using static FMODAudioData.SoundID;
using UnityEngine.Analytics;

public class UnitSoundHelper
{
    private UnitStateManager unit;
    private UnitStateManager.UnitClass unitClass;
    private FMODAudioData audioSheet;

    private FMOD.Studio.EventInstance shooting;
    private FMOD.Studio.EventInstance moving;
    private FMOD.Studio.EventInstance activate;
    private FMOD.Studio.EventInstance deactivate;
    private FMOD.Studio.EventInstance dying;



    public enum SoundType
    {
        SHOOTING,
        MOVING,
        ACTIVATE,
        DEACTIVATE,
        DEATH
    }

    public void Initialize(UnitStateManager _unit, UnitStateManager.UnitClass _unitClass, FMODAudioData _audioSheet)
    {
        unit = _unit;
        unitClass = _unitClass;
        audioSheet = _audioSheet;
    }

    public void PlaySoundByType(SoundType _type)
    {
        switch (_type)
        {
            case SoundType.SHOOTING:
                PlayShooting();
                break;

            case SoundType.MOVING:
                PlayMoving();
                break;

            case SoundType.ACTIVATE:
                PlayActivation();
                break;

            case SoundType.DEACTIVATE:
                PlayDeactivation();
                break;

            case SoundType.DEATH:
                //PlayDeath();
                break;
        }
    }

    public void StopSoundByType(SoundType _type)
    {
        switch (_type)
        {
            case SoundType.SHOOTING:
                StopShooting();
                break;

            case SoundType.MOVING:
                StopMoving();
                break;

            case SoundType.DEATH:
                //StopDeath();
                break;
        }
    }

    private void PlayShooting()
    {
        if (unitClass == UnitStateManager.UnitClass.Fighter)
        {
            if (!IsPlaying(shooting))
            {
                shooting = CreateInstance(audioSheet.GetSFXByName(SFXUnitFighterShooting));

                AttachInstanceToGameObject(shooting, unit.gameObject);
                shooting.setParameterByName("Firing", 0);
                shooting.start();
                shooting.release();
            }
        }

        else if (unitClass == UnitStateManager.UnitClass.Recon)
        {
            shooting = CreateInstance(audioSheet.GetSFXByName(SFXUnitReconShot));

            AttachInstanceToGameObject(shooting, unit.gameObject);

            shooting.start();
            shooting.release();
        }

        
    }

    private void StopShooting()
    {
        if (unitClass == UnitStateManager.UnitClass.Fighter)
        {
            shooting.setParameterByName("Firing", 1);
            shooting.release();
        }
    }

    private void PlayMoving()
    {
        if (unitClass == UnitStateManager.UnitClass.Fighter)
        {
            if (!IsPlaying(moving))
            {
                moving = CreateInstance(audioSheet.GetSFXByName(SFXUnitFighterMoving));

                AttachInstanceToGameObject(moving, unit.gameObject);

                moving.setParameterByName("Moving", 0);
                moving.start();
                moving.release();
            }
        }

        else if (unitClass == UnitStateManager.UnitClass.Recon)
        {
            moving = CreateInstance(audioSheet.GetSFXByName(SFXUnitReconFootstep));

            moving.setParameterByName("Pitch", Random.Range(0.9f, 1.1f));
            AttachInstanceToGameObject(moving, unit.gameObject);
            moving.start();
            moving.release();
        }

        
    }

    private void StopMoving()
    {
        if (unitClass == UnitStateManager.UnitClass.Fighter)
        {
            moving.setParameterByName("Moving", 1);
            moving.release();
        }
    }

    private void PlayActivation()
    {
        activate = CreateInstance(audioSheet.GetSFXByName(SFXUnitPoweringUp));

        AttachInstanceToGameObject(activate, unit.gameObject);

        activate.start();
        activate.release();
    }

    private void PlayDeactivation()
    {
        deactivate = CreateInstance(audioSheet.GetSFXByName(SFXUnitPoweringDown));

        AttachInstanceToGameObject(deactivate, unit.gameObject);

        deactivate.start();
        deactivate.release();
    }

    private bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        instance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE state);
        return state != FMOD.Studio.PLAYBACK_STATE.STOPPED;
    }
}
