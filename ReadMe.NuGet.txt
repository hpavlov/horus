When using NuGet packages, please be aware of the following.

1. Don't commit packages, binaries or other content from the Packages/ folder. No exceptions.
	We use the 'no-commit' workflow which is documented at:
	http://docs.nuget.org/docs/workflows/using-nuget-without-committing-packages
	We have created a .gitignore file that automatically excludes content from the packages folder.

2. TeamCity explicitly restores all packages configured for the solution
	as a seperate build step. This is done prior to commencing
	the solution build; it doesn't rely on the restore-on-build option being enabled.