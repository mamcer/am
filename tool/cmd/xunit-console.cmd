@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  xunit-console.cmd
@REM
@REM  author: m4mc3r@gmail.com
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set working_dir=%CD%\..\..
set msbuild_bin_path=C:\Program Files (x86)\Microsoft Visual Studio\2017\Community\MSBuild\15.0\Bin
set xunit-runner-console_bin_path="C:\root\bin\xunit.runner.console.2.2.0\tools\xunit.console.exe"

@REM  Shorten the command prompt for making the output easier to read
set savedPrompt=%prompt%
set prompt=$$$g$s

@REM copy xunit-console.proj to source folder
COPY /Y xunit-console.proj %working_dir%\xunit-console.proj

pushd %CD%

@REM run xunit-runner-console
CD "%working_dir%"
"%msbuild_bin_path%\MSBuild.exe" xunit-console.proj /p:WorkingDirectory="%working_dir%" /p:XunitRunnerConsoleBinPath=%xunit-runner-console_bin_path% /p:OutDir="%working_dir%"
@if %errorlevel% NEQ 0 goto error
goto success

:error
echo an error has occurred.
GOTO finish

:success
echo process successfully finished
echo start time: %start_time%
echo end time: %time%

:finish
popd
set prompt=%savedPrompt%

ENDLOCAL
echo on