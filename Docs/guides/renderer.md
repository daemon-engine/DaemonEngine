# Renderer

The Renderer module draws all 2D and 3D graphics through a DirectX 11 backend and manages
rendering resources such as textures and shaders.

## Frame lifecycle

{cpp:class}`Renderer` is created from a `sRendererConfig`, started once, and then brackets each
frame with `BeginFrame()` and `EndFrame()`.

```cpp
#include "Engine/Renderer/Renderer.hpp"

Renderer renderer(config);
renderer.Startup();

while (isRunning)
{
    renderer.BeginFrame();
    // Issue draw calls for the scene
    renderer.EndFrame();
}
```

## Vertex formats

{cpp:struct}`Vertex_PCUTBN` carries everything needed for normal-mapped lighting:

| Member | Meaning |
|---|---|
| `m_position` | Position in world space |
| `m_color` | RGBA8 color |
| `m_uvTexCoords` | Texture coordinates |
| `m_tangent`, `m_bitangent` | Tangent-space basis for normal mapping |
| `m_normal` | Surface normal for lighting |

```cpp
#include "Engine/Renderer/Vertex_PCUTBN.hpp"

Vertex_PCUTBN vertex(
    Vec3(0.0f, 0.0f, 0.0f),   // position
    Rgba8::WHITE,             // color
    Vec2(0.0f, 0.0f),         // UV coordinates
    Vec3(1.0f, 0.0f, 0.0f),   // tangent
    Vec3(0.0f, 1.0f, 0.0f),   // bitangent
    Vec3(0.0f, 0.0f, 1.0f));  // normal
```

## Performance

- Batch similar draw calls together.
- Avoid unnecessary pipeline state changes between draws.
