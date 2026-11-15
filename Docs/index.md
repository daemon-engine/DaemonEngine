# DaemonEngine

DaemonEngine is a modular game engine written in modern C++, with a DirectX 11 renderer, FMOD
audio, and JavaScript scripting through V8.

```{note}
DaemonEngine is in active development. Features and APIs may change before the first stable
release.
```

## Features

- **Rendering**: DirectX 11 backend with a batched graphics pipeline.
- **Math**: vectors, matrices, and geometric primitives.
- **Audio**: FMOD integration with 2D and 3D spatial audio.
- **Scripting**: V8 JavaScript with an asynchronous worker-thread architecture.
- **Modular architecture**: independent subsystems that communicate through an event system.
- **Platform**: Windows today, with Linux and macOS planned.

## Where to start

- {doc}`quickstart`: set up the engine and build your first project.
- {doc}`guides/index`: how the main modules fit together, with examples.
- {doc}`api/index`: every class, function, and file, generated from the headers.

## System requirements

| | Minimum | Recommended |
|---|---|---|
| Operating system | Windows 10 (64-bit) | Windows 11 (64-bit) |
| Compiler | Visual Studio 2019 with C++20 | Visual Studio 2022 |
| Graphics | DirectX 11 compatible GPU | Dedicated GPU with 2 GB+ VRAM |
| Memory | 4 GB RAM | 8 GB RAM |

## License

DaemonEngine is released under the MIT License. See
[LICENSE](https://github.com/daemon-engine/DaemonEngine/blob/main/LICENSE).

```{toctree}
:hidden:
:caption: Documentation

quickstart
guides/index
api/index
```

```{toctree}
:hidden:
:caption: Project

GitHub repository <https://github.com/daemon-engine/DaemonEngine>
Issue tracker <https://github.com/daemon-engine/DaemonEngine/issues>
```
