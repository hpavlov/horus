Project codename 'Horus' provides network access to ASCOM controlled equipment
and allows client applications to be built in a number of different languages.

Some Guidelines For Contributors
================================

Contributors are expected to add code to the repository by means of a Pull Request.
Code can be initially pushed here, to a branch other than 'master', or pushed
to another publicly-accessible repository such as GitHub. The pull request can
then be created from either location as appropriate. Branches should typically 
be named for the feature or Jira issue being worked on.

Submitting a Pull Request will trigger a code review that will be open to all
project members. The code review must complete satisfactorily before your pull
request can be accepted and merged into the 'master' branch. Your Pull Request
will also trigger a Continuous Integration Build that must complete successfully.

It makes sense if contributions are kept 'coherent', that is, contain only changes
related to a specific feature or issue. It is better to make multiple small coherent
contributions than one monolithic sprawling contribution. The smaller contributions
will be easier to get through code review and CI build.

Any developer may check out and edit any part of the codebase at any time. When editing
code, developers should try to keep to the style already established in the source file.

Versioning
==========

This subject is up for discussion.

Suggested format for build version numbers:
	Major.Minor.Sprint.Counter-Label

Example:
	1.0.1.10-Beta

Where:
	Major	- is the major version number and is incremented when new features are added
	Minor	- is the minor version and is incremented when minor changes and bugfixes are implemented.
	Sprint	- is the sprint number being worked on as defined in the Jira/GreenHopper Agile RapidBoard
	Counter	- is the TeamCity auto-incrementing build counter.
	Label	- An optional label or tag that provides additional information about the build.

The label is for information only and the version number can be written with or withouth the label.
For example, when planning the roadmap in Jira, it is often convenient to label versions this way,
whereas	in TeamCity, it is easier not to.

The TeamCity build server will automatically insert the correct version number into all AssemblyVersion,
AssemblyFileVersion and AssemblyInformationalVersion attributes during the build process. It will also label
the sources in the Git/Stash repository each time it completes a build. Thus, any given built output can be
easily traced back to the build that created it and the source revisions that went into making it.

Developer Tools
===============

We expect to be able to provide free licenses for certain productivity tools. I'll add the
license codes here as and when I obtain them.