# AM aka Sound Golem

A .NET Winforms application from 2008.

In `original` branch you will find the original source code for this application. In `master` an upgraded, refactored version.

## Description

AM or Sound Golem is a simple Winforms .NET 3.5 application. With little resource consumption (intended at least). The main goal of the application was to play .mp3 audio files from a specific folder. With full keyboard control, no UI with playback options provided.

Intended to accompany coding sessions, being able to change songs without disrupting the current task. With full keyboard control, no context switching. The thing is that even at that moment there were different options available that did the same in definitely a better way. Even from the resource consuption perspective. I initially thought that a simple Winform with only one form will consume almost no memory. It wasn't the case, at least without further tweaks.

It was fun to implement it. Just a couple of hours in one or maybe two nights.

## Technologies

- Visual Studio 2008
- .NET Framework 3.5

## Requirements

- Windows Media Player installed (to being able to play mp3 files)
- Libraries: `AxInterop.WMPLib.dll`, `Interop.WMPLib.dll` (included in the `publish` folder from `original` branch)