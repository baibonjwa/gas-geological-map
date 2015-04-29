@echo off
REM 进入当前批处理文件所在目录
cd %~dp0
REM 复制资源/配置文件
set srcPath=RequiredFiles
xcopy /E /Y %srcPath% %~dp0\bin\Debug\
if errorlevel 0 (echo 复制资源/配置文件成功!)^
else (call :PrintError 复制资源/配置文件失败，请检查!)
call :PrintSeperator
exit

:PrintError
echo %1
pause

:PrintSeperator
echo ======