# TestAttachmentsIssue
project to illustrate the problems recently introduced by VS team when dealing with test attachments.

In older versions of Visual Studio, when clicking in a file attachment of a test log, the file was
open in the same way as if it would be opened from the file explorer.

Recently, Visual studio has been changing this behavior so the attached files are opened in a visual studio tab.

The first problem of this approach is that Visual Studio opens text based files as plain text.
This includes html files, that could contain test reports that should be opened as web pages, not as text.

The second problem is that binary files, or files that are unknown to Visual Studio, are opened as raw binary files.

Visual Studio should let developers to choose how the test attachments are opened within the context of test explorer, or at the very least extend
the context menu to allow opening the file using the shell, or opening the folder containing the test attachment file.
