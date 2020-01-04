# BatchHeaderFooterAddRemove
Batch add and or remove headers an or footers. All at once!

# Documentation
"Add files" will open a file selection dialog to add a file. _It will not respect the "(only with the extension )" setting_ next to the "Add files from a folder" buttons.

"Add files from a folder (with subfolders)" buttons will respect the "(only with the extension )" preference if said section is not empty and add files from a selected folder to the list. It will search into subfolders while looking for files.

"Add files from a folder (with subfolders)" buttons will respect the "(only with the extension )" preference if said section is not empty and add files from a selected folder to the list. It will _not_ search into subfolders while looking for files.

"Remove selected" will remove the selected files from the "File List:".

_The text boxes in the "Remove" section of the UI will default to "0" if empty, meaning that will not be applied to files._

_The text boxes in the "Add" section of the UI will default to an empty string ("") if empty, meaning that will not be applied to files._

When clicked on "Start", it will copy each file in the list individually to a temporary byte array, remove specified amount of bytes both from the beginning of the file (header) and end of the file (footer). It will then insert the specified strings converted to hexadecimal bytes (ASCII) both to the beginning of the file (as a header) and to the ending of the file (as a footer).

_Windows Clipboard does not respect line break characters, so those characters will be replaced with a "00" (null). It's a limitation I can not do anything about._

It will output the files to their original locations if the "Output files to:" section is empty.

It will output the files with the "with the extension" next to the "Output files to:" section is not empty.

_If  "Output files to:" and the "with the extension" section next to the former are both empty, it will overwrite the original file._