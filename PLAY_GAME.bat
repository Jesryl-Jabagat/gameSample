@echo off
title Eclipse Reborn - Game Launcher
echo.
echo =============================================
echo    Eclipse Reborn: Chronicles of the Lost Sun
echo =============================================
echo.
echo Choose how you want to play:
echo.
echo 1. Play Web Version (Recommended)
echo 2. Open Unity Project (Requires Unity)
echo 3. Exit
echo.
set /p choice="Enter your choice (1-3): "

if "%choice%"=="1" (
    echo.
    echo Starting web version...
    start "" "eclipse_reborn_playable.html"
    goto end
)

if "%choice%"=="2" (
    echo.
    echo Launching Unity project...
    set "unityPath=C:\Program Files\Unity\Hub\Editor\6000.0.61f1\Editor\Unity.exe"
    if exist "%unityPath%" (
        start "" "%unityPath%" -projectPath "%~dp0UnityProject"
    ) else (
        echo Unity not found at the expected path.
        echo Please install Unity 6000.0.61f1 or newer from Unity Hub.
        pause
    )
    goto end
)

if "%choice%"=="3" (
    goto end
)

echo Invalid choice. Please try again.
pause
goto start

:end
echo.
echo Thank you for playing Eclipse Reborn!
timeout /t 2 >nul