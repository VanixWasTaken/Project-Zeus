using System;
using System.Collections;

namespace UnityEngine
{
    namespace Audio
    {
        public class AudioController : MonoBehaviour
        {
            // members
            public static AudioController instance;

            public bool debug;
            public AudioTrack[] tracks;

            private Hashtable m_AudioTable; //relationship between audio types (key) and audio tracks (value)
            private Hashtable m_JobTable;   //relationship between audio types (key) and jobs (value) (Coroutine, IEnumerator)

            [System.Serializable]
            public class AudioObject
            {
                public AudioType type;
                public AudioClip clip;
            }

            [System.Serializable]
            public class AudioTrack
            {
                public AudioSource source;
                public AudioObject[] audio;
            }

            private class AudioJob
            {
                public AudioAction action;
                public AudioType type;
                public AudioType type2;
                public float pitch;
                public float pitch2;

                public AudioJob(AudioAction _action, AudioType _type, AudioType _type2 = AudioType.none, float _pitch = 1.0f, float _pitch2 = 1.0f)
                {
                    action = _action;
                    type = _type;
                    type2 = _type2;
                    pitch = _pitch;
                    pitch2 = _pitch2;
                }
            }

            private enum AudioAction
            {
                START,
                RANDOMIZE_CLIP,
                RANDOMIZE_PITCH,
                STOP,
                RESTART
            }

            #region Unity Functions

            private void Awake()
            {
                if (!instance)
                {
                    Configure();
                }
            }

            private void OnDisable()
            {
                Dispose();
            }

            #endregion

            #region Public Functions
            public void PlayAudio(AudioType _type)
            {
                AddJob(new AudioJob(AudioAction.START, _type));
            }

            public void RandomizeAudioClip(AudioType _type, AudioType _type2)
            {
                AddJob(new AudioJob(AudioAction.RANDOMIZE_CLIP, _type, _type2));
            }

            public void RandomizeAudioPitch(AudioType _type, float _min_pitch, float _max_pitch)
            {
                AddJob(new AudioJob(AudioAction.RANDOMIZE_PITCH, _type, AudioType.none, _min_pitch, _max_pitch));
            }

            public void StopAudio(AudioType _type)
            {
                AddJob(new AudioJob(AudioAction.STOP, _type));
            }

            public void RestartAudio(AudioType _type)
            {
                AddJob(new AudioJob(AudioAction.RESTART, _type));
            }

            #endregion

            #region Private Functions
            private void Configure()
            {
                instance = this;
                m_AudioTable = new Hashtable();
                m_JobTable = new Hashtable();
                GenerateAudioTable();
            }

            private void Dispose()
            {
                foreach (DictionaryEntry _entry in m_JobTable)
                {
                    IEnumerator _job = (IEnumerator)_entry.Value;
                    StopCoroutine(_job);
                }
            }

            private void GenerateAudioTable()
            {
                foreach(AudioTrack _track in tracks)
                {
                    foreach(AudioObject _obj in _track.audio)
                    {
                        // do not duplicate keys
                        if (m_AudioTable.ContainsKey(_obj.type))
                        {
                            LogWarning("You are trying to register audio [" + _obj.type + "] that has already been registered.");
                        }
                        else
                        {
                            m_AudioTable.Add(_obj.type, _track);
                        }
                    }
                }
            }

            private IEnumerator RunAudioJob(AudioJob _job)
            {
                AudioTrack _track = (AudioTrack)m_AudioTable[_job.type];
                _track.source.clip = GetAudioClipFromAudioTrack(_job.type, _track);
                _track.source.pitch = GetRandomPitch(_job.pitch, _job.pitch2);

                switch (_job.action)
                {
                    case AudioAction.START:
                        _track.source.Play();
                    break;

                    case AudioAction.RANDOMIZE_CLIP:
                        _track.source.Play();
                        break;

                    case AudioAction.RANDOMIZE_PITCH:
                        _track.source.Play();
                        break;

                    case AudioAction.STOP:
                        _track.source.Stop();
                    break;

                    case AudioAction.RESTART:
                        _track.source.Stop();
                        _track.source.Play();
                    break;
                }

                m_JobTable.Remove(_job.type);

                yield return null;
            }

            private void AddJob(AudioJob _job)
            {
                // randomize types
                _job.type = GetRandomType(_job.type, _job.type2);

                // remove conflicting jobs
                RemoveConflictingJobs(_job.type);

                // start job
                IEnumerator _jobRunner = RunAudioJob(_job);
                m_JobTable.Add(_job.type, _jobRunner);
                StartCoroutine(_jobRunner);
            }

            private AudioType GetRandomType(AudioType _type, AudioType _type2)
            {
                if ( _type2 != AudioType.none)
                {
                    int rng = Random.Range((int)_type, (int) _type2 + 1);
                    return (AudioType)rng;
                }
                else
                {
                    return _type;
                }
            }

            private float GetRandomPitch(float _min_pitch, float _max_pitch)
            {
                if (_min_pitch != _max_pitch)
                {
                    float randomPitch = Random.Range(_min_pitch, _max_pitch);
                    return (float)randomPitch;
                }
                else
                {
                    return 1.0f;
                }
            }

            private void RemoveJob(AudioType _type)
            {
                if (!m_JobTable.ContainsKey(_type))
                {
                    LogWarning("Trying to stop a job [" + _type + "] that is not running.");
                    return;
                }

                IEnumerator _runningJob = (IEnumerator)m_JobTable[_type];
                StopCoroutine(_runningJob);
                m_JobTable.Remove(_type);
            }

            private void RemoveConflictingJobs(AudioType _type)
            {
                if (m_JobTable.ContainsKey(_type))
                {
                    RemoveJob(_type);
                }

                AudioType _conflictAudio = AudioType.none;
                
                foreach(DictionaryEntry _entry in m_JobTable)
                {
                    AudioType _audioType = (AudioType)_entry.Key;
                    AudioTrack _audioTrackInUse = (AudioTrack)m_AudioTable[_audioType];
                    AudioTrack _audioTrackNeeded = (AudioTrack)m_AudioTable[_type];

                    if (_audioTrackNeeded.source == _audioTrackInUse.source) 
                    {
                        // conflict
                        _conflictAudio = _audioType;
                    }
                }

                if (_conflictAudio != AudioType.none) 
                {
                    RemoveJob(_conflictAudio);
                }
            }

            private AudioClip GetAudioClipFromAudioTrack(AudioType _type, AudioTrack _track)
            {
                foreach(AudioObject _obj in _track.audio)
                {
                    if (_obj.type == _type)
                    {
                        return _obj.clip;
                    }
                }
                return null;
            }

            private void Log(string _msg)
            {
                if (!debug) return;
                Debug.Log("[AudioController]: " + _msg);
            }

            private void LogWarning(string _msg)
            {
                if (!debug) return;
                Debug.LogWarning("[AudioController]: " + _msg);
            }
#endregion
        }
    }
}


