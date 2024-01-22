# NetCheck

_NetCheck_ is a simple internet connectivity monitoring application for Windows 10+. I've been using a personal version of NetCheck for over a year now but lost the source code when I had reinstalled Windows, so I decided to recreate the project with additional features and improvements.

Compared to the original project, this version includes an average latency display, network type display, and optional notifying of internet issues (planned feature).

## Requirements

- [.NET Desktop Runtime 8.0.x](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-desktop-8.0.1-windows-x64-installer)
- [ASP.NET Core Runtime 8.0.x](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-8.0.1-windows-x64-installer)

### Architecture

NetCheck has two processes: the "client" and "worker". The client is a simple WinForms application with a WebView2 control. The worker is a simple ASP.NET Core server that does the actual work by probing servers and recording latency.

When the client is started, it starts the worker and waits for the worker to respond to pings before continuing its initilization.

IPC between the client and worker is achieved through a combination of HTTP requests and real-time SignalR messaging.

This architecture was chosen over something like Electron because, while lacking many features of Electron, applications using WebView2 are generally faster and far more lightweight as long as you have the installed runtimes and are on a relatively recent version of Windows 10 or Windows 11.
