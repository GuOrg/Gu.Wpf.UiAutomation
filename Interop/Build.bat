set VS=%VS140COMNTOOLS%
set WINSDK=C:\Program Files (x86)\Windows Kits\10\Include\10.0.14393.0\um\
set ASMVERSION=4.5
SET TEMP=tmp
call "%VS%vsvars32.bat"

mkdir %TEMP%

@REM Create Type Libraries
midl.exe /nologo /out %TEMP% /char signed /tlb UIAutomationClient.tlb /h UIAutomationClient_h.h "%WINSDK%UIAutomationClient.idl"
@REM Create the original dlls from the tlbs
REM tlbimp.exe /machine:Agnostic /silent /asmversion:%%A /out:%%A\Interop.UIAutomationClient.dll %TEMP%\UIAutomationClient.tlb
tlbimp.exe /machine:Agnostic /silent /asmversion:1.0.0.0 /out:%TEMP%\Interop.UIAutomationClient.dll %TEMP%\UIAutomationClient.tlb /keyfile:..\Gu.Wpf.UiAutomation.snk"	

RMDIR /S /Q %TEMP%
pause
