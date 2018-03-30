@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  report-generator.cmd
@REM
@REM  author: mamcer@outlook.com
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set working_dir=%CD%\..\..
set reportgenerator_bin=C:\root\bin\report-generator\tools\ReportGenerator.exe
set opencover_file=%working_dir%\open-cover.xml
set target_dir=coverage-report

@REM  Shorten the command prompt for making the output easier to read
set savedPrompt=%prompt%
set prompt=$$$g$s

pushd %CD%

cd %working_dir%

@REM remove previous coverate-report directory if it exists  
IF NOT EXIST "%working_dir%\%target_dir%" GOTO NoCoverageReport
rmdir /s /q "%target_dir%"
:NoCoverageReport
md "%target_dir%"

@REM run report generator
"%reportgenerator_bin%" -reports:"%opencover_file%" -targetdir:"%CD%\%target_dir%" -reporttypes:Html
@if %errorlevel% NEQ 0 goto error

@REM  Restore the command prompt and exit
@goto :success

:error
echo an error has occured: %errorLevel%
echo start time: %start_time%
echo end time: %time%
goto :finish

:success
echo process successfully finished
echo start time: %start_time%
echo end time: %time%

:finish
popd
set prompt=%savedPrompt%

ENDLOCAL
echo on