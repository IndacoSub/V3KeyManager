# Windows desktop smoke test

Run these checks on Windows with the .NET 10 SDK or Windows Desktop Runtime. A Linux cross-build cannot exercise Windows Forms. Use a disposable copy of `config.txt` and retain the original separately.

1. Start the application. Check that navigation, labels, and action buttons remain readable at 100%, 150%, and 200% display scaling, including the minimum supported window size. Navigate using Tab and Shift+Tab and verify that focus is visible.
2. Create a new configuration. Visit every settings page and confirm that the original game defaults appear. Change a value, then attempt to create another document, open a file, or close the window. Check that Save, Discard, and Cancel preserve the expected document.
3. Open a game configuration. Search for a keyboard action, change its bindings, clear an optional binding, and cancel another edit. Confirm that duplicate or malformed bindings receive an actionable error and that cancel leaves the action unchanged.
4. Change window mode, window position, all resolution fields, input prompts, mouse sensitivity, anti-aliasing, and game configuration version. Save As to a new location, reopen the file, and check that every value survived.
5. Save another change over the same file. Confirm that `.bak` contains the previous contents and that unknown settings and comments still exist in the current file. Open a malformed file and try saving to an unwritable location; check that errors are visible and the current document stays usable.
6. Revert edits and restore default bindings. Check that the changed-state indicator and Save action reflect the resulting document. If using binding randomization, verify that it completes promptly and produces valid bindings.
7. Load optional artwork, replace it, and try an invalid image. Confirm that text and controls stay readable and the invalid image does not terminate the application.
8. With the game installed, save `config.txt` beside its executable and launch it. Check discovery of each supported executable and the explicit choice when both exist. Verify that cancelling a save or launch prompt does not start the game.

Record Windows version, display scaling, build commit, and any failing step when reporting results. Do not mark these checks as passed based only on a successful build or core test run.
