When using NuGet packages, please be aware of the following.

1. Don't enable package restore during build. This will break the TeamCity CI build. TeamCity explicitly restores all NuGet packages using a
	'safe update' prior to running the build. To update your NuGet packages, use:
	Tools -> Library Package Manager -> Manage NuGet Packages for Solution

2. Don't commit binaries or other content from teh Packages/ folder. No exceptions.