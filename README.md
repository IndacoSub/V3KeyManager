# V3KeyManager

Danganronpa V3 Config File Editor, with the main purpose of being able to reassign input keys, as well as being an alternative "launcher" for the game.

Contributions and bug/issue-reporting are welcome and appreciated.

More documentation coming in the near future.

# Files

This repository does not contain any image file.

If you want a background image, you must provide it yourself.

# License

This tool is licensed under the ISC License.

# Credits

    “DANGANRONPA” is a registered trademark of Spike Chunsoft Co., Ltd., Too Kyo Games, LLC and NIS America Inc.
	We are not in any way affiliated or associated with them.
	
	Thanks to all of our collaborators and reviewers.

# Development and usage (.NET 10)

The rewritten application targets **.NET 10 LTS** and Windows Forms. It separates the game configuration logic into `V3KeyManager.Core`, which can also be built and tested on Linux. The desktop interface and game launcher require Windows.

## Build and run

Install the .NET 10 SDK, then run these commands from the repository root:

```powershell
dotnet restore V3KeyManager.sln
dotnet build V3KeyManager.sln --configuration Release --no-restore
dotnet test V3KeyManager.Tests --configuration Release --no-build
dotnet run --project V3KeyManager
```

For Linux development, enable Windows targeting when building the desktop project:

```sh
dotnet build V3KeyManager.sln --configuration Release -p:EnableWindowsTargeting=true
dotnet test V3KeyManager.Tests --configuration Release
```

Linux builds do not validate the Windows interface. Follow [the Windows smoke test](docs/windows-smoke-test.md) before releasing a desktop build.

A framework-dependent desktop build needs the .NET 10 **Windows Desktop Runtime** on the destination PC. To produce a self-contained Windows x64 build instead:

```powershell
dotnet publish V3KeyManager --configuration Release --runtime win-x64 --self-contained true --output artifacts/windows-x64
```

## Editing a configuration

Open the game's `config.txt`, or create a new configuration using the original game defaults. The interface groups keyboard bindings, display settings, and input options into separate pages. Search the keyboard action list and edit an action's bindings without opening a separate window for each settings category. Optional artwork is supplied by the user; no game images are distributed.

Save writes to the current file; Save As creates a separate copy. Existing files receive a `.bak` backup when replaced. Unknown configuration entries and comments are retained. Keep an independent copy of valuable game settings before editing them.

The launcher looks beside the saved configuration for `Dangan3Win.exe` (Steam) or `Dangan3Desktop.exe` (Xbox). When both are present, choose the executable to launch. Launching requires a local game installation on Windows.

## Validation

`V3KeyManager.Tests` exercises configuration parsing, defaults, round trips, validation, backups, and executable discovery. GitHub Actions builds the Windows application and runs the core tests on Windows and Linux. Graphical behavior and launching the installed game are covered by the manual Windows smoke test.


## Windows layout regression checks

Run `dotnet test V3KeyManager.UI.Tests --configuration Release` on Windows to check command visibility at normal and compact window sizes, aligned filter fields, and literal ampersands in labels and file paths. The tests capture each settings page under `artifacts/ui`. GitHub Actions uploads these screenshots and test results as `windows-ui-regressions`. These checks complement the manual DPI and game-launch checks above; they cannot run on Linux.
