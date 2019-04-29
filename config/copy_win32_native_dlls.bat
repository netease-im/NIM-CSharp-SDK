@echo off

set win32_native_dlls_path_src=..\sdk\libs\win32\
set win32_native_dlls_path_des=..\demos\wpf\Dependents\dllimport\

echo %win32_native_dlls_path_src%
echo %win32_native_dlls_path_des%

xcopy %win32_native_dlls_path_src%x64_dlls\* %win32_native_dlls_path_des%x64 /e
xcopy %win32_native_dlls_path_src%x86_dlls\* %win32_native_dlls_path_des%x86 /e

pause
