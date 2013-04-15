Project codename 'Horus' provides network access to ASCOM controlled equipment
and allows client applications to be built in a number of different languages.

Some Guidelines For Contributors

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

Suggested format for project version numbers:
	Major.Minor.Build-Label

Example:
	1.0.239-Beta

Where:
	Major - is the major version number and is incremented when new features are added
	Minor - is the minor version and is incremented when minor changes and bugfixes are implemented.
	Build - is the automatic build counter from TeamCity, which increments with every build.
	        For manual builds done on developer workstations, this should be set to 0.
	Label - An optional label or tag that provides additional information about the build.
		This is for information only and the version may be written with or withouth the label.
		For example, in Jira, it is often convenient to lavel versions this way, whereas
		in TeamCity, it is easier not to.

Developer Tools
===============

We expect to be able to provide free licenses for certain productivity tools. I'll add the
license codes here as and when I obtain them.