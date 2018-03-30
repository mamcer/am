@echo off
SETLOCAL

@REM  ----------------------------------------------------------------------------
@REM  open-cover.cmd
@REM
@REM  author: mamcer@outlook.com
@REM  ----------------------------------------------------------------------------

set start_time=%time%
set working_dir=%CD%\..\..
set opencover_bin=C:\root\bin\open-cover\tools\OpenCover.Console.exe
set xunit_runner_console=C:\root\bin\xunit.runner.console.2.2.0\tools\xunit.console.exe
set core_test_path=Src\AM.Core.Test\bin\Debug\
set opencover_xml=%working_dir%\open-cover.xml

@REM  Shorten the command prompt for making the output easier to read
set savedPrompt=%prompt%
set prompt=$$$g$s

pushd %CD%

@REM copy open-cover.proj to source folder
COPY /Y open-cover.proj %working_dir%\open-cover.proj

@REM run open-cover
CD "%working_dir%\%core_test_path%"
"%opencover_bin%" -register:user -target:"%xunit_runner_console%" -targetargs:"AM.Core.Test.dll" -filter:"+[*]* -[*.Tests]* -[*.Test]* -[xunit.*]* -[Microsoft.*]*" -output:"%opencover_xml%" -mergebyhash -mergeoutput -hideskipped:All -log:Verbose -skipautoprops
@if %errorlevel%  NEQ 0  goto :error

REM  Restore the command prompt and exit
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