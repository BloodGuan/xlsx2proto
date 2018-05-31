@echo off
set DIR=%~dp0
cd /d "%DIR%"
setlocal enabledelayedexpansion
for /r %%i in (*.proto) do ( 
	set pbname=%%~nxi
	set pbname=!pbname:~0,-5!pbc	
	protoc --descriptor_set_out ./!pbname! %%~nxi 
	echo %%~nxi
)
echo "finished" 

pause