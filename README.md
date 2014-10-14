IntelUSBHostDeviceReset
=======================

IntelUSBHostDeviceReset

Install service:
IntelUSBHostDeviceReset.exe /install

Uninstall service:
IntelUSBHostDeviceReset.exe /uninstall

Configuration - app.config:

Appsettings:
	*  devicespath - devices path sepatated comma  
	*  logfilename - service log
	*  logfilelocation - service log folder location  - must have privileges to write for local system user, empty value - working directory

Logs:
Event viewer -> Application & Services -> IntelUSBHostDeviceReset
Log file
	