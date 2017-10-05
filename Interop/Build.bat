set WINSDK=C:\Program Files (x86)\Windows Kits\10\Include\10.0.15063.0\um\
set ASMVERSION=0.1.17.0
set TEMP=tmp
call "%VS140COMNTOOLS%vsvars32.bat"

mkdir %TEMP%

@REM Create Type Libraries
midl.exe UIAutomationClient.idl /nologo /out %TEMP% /char signed /tlb UIAutomationClient.tlb /header UIAutomationClient.h
pause
@REM Create the original dlls from the tlbs
REM tlbimp.exe /machine:Agnostic /silent /asmversion:1.0.0.0 /out:Interop.UIAutomationClient.dll %TEMP%\UIAutomationClient.tlb
tlbimp.exe /machine:Agnostic /silent /asmversion:%ASMVERSION% /productversion:%ASMVERSION% /out:Interop.UIAutomationClient.dll %TEMP%\UIAutomationClient.tlb /keyfile:..\Gu.Wpf.UiAutomation.snk"	
pause
RMDIR /S /Q %TEMP%
pause
