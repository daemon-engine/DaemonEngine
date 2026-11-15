# Audio

The Audio module wraps FMOD to load, cache, and play sounds, with 2D playback for UI and music
and 3D spatial playback with listener tracking.

## Setup and 2D playback

{cpp:class}`AudioSystem` stores its configuration on construction; FMOD starts in `Startup()`.
`CreateOrGetSound()` caches each file, so loading the same path twice returns the same
`SoundID`.

```cpp
#include "Engine/Audio/AudioSystem.hpp"

sAudioSystemConfig config;
AudioSystem audioSystem(config);
audioSystem.Startup();

SoundID click = audioSystem.CreateOrGetSound("assets/ui/button_click.wav",
                                             eAudioSystemSoundDimension::Sound2D);
if (click == MISSING_SOUND_ID)
{
    // The file could not be loaded
}

SoundPlaybackID playback = audioSystem.StartSound(click, false, 0.8f);
```

## 3D spatial audio

Configure listeners first, then start sounds at a position and move them as their source moves.

```cpp
SoundID engine = audioSystem.CreateOrGetSound("assets/vehicles/engine.ogg",
                                              eAudioSystemSoundDimension::Sound3D);

audioSystem.SetNumListeners(1);
audioSystem.UpdateListener(0, cameraPos, cameraForward, cameraUp);

SoundPlaybackID vehicleAudio = audioSystem.StartSoundAt(engine, vehiclePosition, true);

// Every frame, as the vehicle moves
audioSystem.SetSoundPosition(vehicleAudio, vehiclePosition);
```

## Playback control

```cpp
audioSystem.SetSoundPlaybackVolume(playback, 0.5f);
audioSystem.SetSoundPlaybackSpeed(playback, 0.5f);     // half speed
audioSystem.SetSoundPlaybackBalance(playback, -0.5f);  // favor the left channel

if (audioSystem.IsPlaying(playback))
{
    audioSystem.StopSound(playback);
}
```

## Frame integration

Call `BeginFrame()` and `EndFrame()` once per frame, and `Shutdown()` before destroying the
system; every `SoundID` and `SoundPlaybackID` becomes invalid after shutdown.

```cpp
while (isRunning)
{
    audioSystem.BeginFrame();
    audioSystem.UpdateListener(0, camera.GetPosition(), camera.GetForward(), camera.GetUp());
    // Game logic
    audioSystem.EndFrame();
}

audioSystem.Shutdown();
```

## Notes

- Built against the FMOD 2.02 API; see the [FMOD API reference](https://fmod.com/docs/2.02/api/).
- Pass FMOD return codes to `ValidateResult()` to catch errors early.
