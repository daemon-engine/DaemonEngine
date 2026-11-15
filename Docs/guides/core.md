# Core

The Core module holds the engine's basic data types, string utilities, and helper classes that
other modules build on.

## String utilities

`Engine/Core/StringUtils.hpp` provides printf-style formatting, C++20 `std::format`-style
formatting, and string splitting.

```cpp
#include "Engine/Core/StringUtils.hpp"

// printf-style formatting
String message = Stringf("Player has %d health", playerHealth);

// Type-safe C++20 formatting
String modernMsg = StringFormat("Player {} has {} health", playerName, playerHealth);

// Split on a delimiter
StringList tokens = SplitStringOnDelimiter("apple,banana,orange", ',');

// Split into lines, handling different line endings
StringList lines;
int lineCount = SplitStringIntoLines(lines, multiLineText);
```

## Named strings

{cpp:class}`NamedStrings` stores key/value pairs and reads them back as typed values, with a
default when the key is missing.

```cpp
#include "Engine/Core/NamedStrings.hpp"

NamedStrings settings;
settings.SetValue("shader_name", "basic_shader");

String shaderName = settings.GetValue("shader_name", "default");
int    width      = settings.GetValue("width", 1600);
```
