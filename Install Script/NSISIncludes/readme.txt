When using the environment variable one, you can use the following to set an env. variable: 

; set variable
WriteRegExpandStr ${env_hklm} MYVAR MYVAL
; make sure windows knows about the change
SendMessage ${HWND_BROADCAST} ${WM_WININICHANGE} 0 "STR:Environment" /TIMEOUT=5000
