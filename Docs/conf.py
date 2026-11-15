"""Sphinx configuration for the DaemonEngine documentation site.

Build from the Engine root:

    doxygen
    uv run --project Docs --locked sphinx-build -W --keep-going -b html Docs Docs/_build/html

`doxygen` parses Code/Engine into Docs/doxygen/xml. Exhale turns that XML into one page per
file and class under Docs/api/, and Breathe renders those pages, so the API reference follows
the headers without hand-written pages.
"""

project = "DaemonEngine"
author = "Yu-Wei Tseng"
copyright = "2025, Yu-Wei Tseng"
release = "1.0.0"

extensions = [
    "myst_parser",
    "breathe",
    "exhale",
]
# Pages are Markdown; Exhale writes its generated API pages as reStructuredText.
source_suffix = {".md": "markdown", ".rst": "restructuredtext"}
# doxygen/ holds Doxygen's XML, _build/ Sphinx's output, and .venv/ the uv environment.
exclude_patterns = ["_build", "doxygen", ".venv"]

# The code here is C++, so bare `name` roles and code blocks default to the C++ domain.
primary_domain = "cpp"
highlight_language = "cpp"

# Exhale renders nested types and members both on their parent's page and on their own page,
# so the C++ domain sees each twice. The duplicate is expected, not a documentation error.
suppress_warnings = ["duplicate_declaration.cpp"]

html_theme = "furo"
html_title = "DaemonEngine"
html_logo = "_static/images/daemon-engine-logo.png"
html_favicon = "_static/images/daemon-engine-logo.ico"
html_static_path = ["_static"]
# Published by GitHub Pages; the custom domain is set in the repository's Pages settings.
html_baseurl = "https://daemonengine.dadavidtseng.com/"

# -- Breathe: render Doxygen XML ----------------------------------------------------------------

breathe_projects = {"DaemonEngine": "doxygen/xml"}
breathe_default_project = "DaemonEngine"

# -- Exhale: generate the API pages from the same XML -------------------------------------------

exhale_args = {
    "containmentFolder": "./api",
    "rootFileName": "index.rst",
    "doxygenStripFromPath": "..",
    "rootFileTitle": "API Reference",
    # Furo's sidebar already navigates the pages; Exhale's tree view would add one huge HTML line.
    "createTreeView": False,
    # Doxygen runs as a separate step against the Engine root Doxyfile.
    "exhaleExecutesDoxygen": False,
}
