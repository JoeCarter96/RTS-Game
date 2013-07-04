@echo off 
set dir=C:\Git Projects\RTS-Game
set /p ID= Enter the commit ID to merge with:

cd %dir%
git pull
git stash
git merge %ID%

pause