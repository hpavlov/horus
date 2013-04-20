Project codename 'Horus' provides network access to ASCOM controlled equipment
and allows client applications to be built in a number of different languages.

Community - How To Get Involved
===============================

This project is open source and everyone is welcome and encouraged to get involved and contribute.
We aim to make it as easy as possible to get involved. At the simplest level, to make a
contribution, simply go to our Jira issue tracking site, pick an outstanding issue or
technical task, assign it to yourself and work on it.

We expect to hold regular online meetings, which anyone is welcome to attend, but you don't have to.

We hope to get a lot of people involved in this project, so coordinating the developers and
the work load could present significant challenges. On day 1 of the project, at out kick-off meeting,
we were already in 5 different time zones across 3 continents. Some sort of 'process' is needed
otherwise it will be chaos. We've put several project support systems in place and have come up with
a few guidelines, so that everyone knows what's going on and what to expect.


Source Code Control
===================

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
code, developers should try to keep to the style already established in the source file,
unless they plan to take on full responsibility for it.

Our 'master' Git repository is located at:
http://stash.teamserver.tigranetworks.co.uk/projects/HORUS
(you need a Jira login to access this).

Versioning
==========

This subject is very much up for discussion. The scheme I've proposed here appears to work well with the
C.I. Build and with Jira, etc.

Suggested format for build version numbers:
	Major.Minor.Sprint.Counter-Label

Example:
	1.0.1.10-Beta

Where:
	Major	- is the major version number and is incremented when new features are added
	Minor	- is the minor version and is incremented when minor changes and bugfixes are implemented.
	Sprint	- is the sprint number being worked on as defined in the Jira/GreenHopper Agile RapidBoard
	Counter	- is the TeamCity auto-incrementing build counter and may be omitted in some cases.
	Label	- An optional label or tag that provides additional information about the build.

The label is for information only and the version number can be written with or withouth the label.
For example, when planning the roadmap in Jira, it is often convenient to label versions this way,
whereas	in TeamCity, it is easier not to.

The TeamCity build server will automatically insert the correct version number into all AssemblyVersion,
AssemblyFileVersion and AssemblyInformationalVersion attributes during the build process. It will also label
the sources in the Git/Stash repository each time it completes a build. Thus, any given built output can be
easily traced back to the build that created it and the source revisions that went into making it.

Continuous Integration Build
============================

Horus will be built by TeamCity whenever any developer pushes changes to the prime repository. This includes:
. Changes pushed or merged into the 'master' branch
. Changes pushed to any other branch
. changes pushed to a personal branch

Builds on the master branch are potential release candidates, no code should be pushed directly to master but
should always arrive via a pull request from another branch. Builds on the master branch generate labels in VCS.

TeamCity will automatically build all code pushed to any top level branch, and a successful build will be required
before a pull request from that branch is considered valid. These builds are public.

Contributors may create a personal branch, on the path /personal/<your-name> - TeamCity gives these 'personal
branches' special treatment. It will trigger a private build belonging to the last committer on the branch.
Personal builds are only visible to their owner and breaking a personal build is acceptable. This is
your own private sandbox area where you can work in relative privacy without fear of breaking the
public builds (although your source code will still be public, or course).

The Continuous Integration Build will build the project from end to end, it will set up any
prerequisites, build all of the solution configurations including any setup package, run unit test
and code coverage, perform code analysis, bundle up and archive any relevant build artifacts
gather statistics and metrics and produce a build results page where any reports will be presented.

Contributors will be automatically notified by email about any builds triggered by their changes.
Other notifications can be subscribed to vis the TeamCity web site. There is also a tray notifier
application, and RSS feed and a Visual Studio plugin available by clicking on your user name
on the TeamCity web site.

Project Management
==================

Project management is provided by Atlassian Jira, located at
http://jira.teamserver.tigranetworks.co.uk
Users may create their own login from teh Jira home page.

It is suggested that users focus on the Agile RapidBoard, which provides a simplified drag-and-drop
interface and doesn't require any particular knowledge of Jira. Users who wish to can use the full
jira interface.

It is expected that end users will create bug reports and other changes requests within Jira, or have
a project developer do so on their behalf. Users can receive email notifications about status changes
in their issues.

Code Reviews
============

We want the project to be as open as possible, but that means we have to take steps to ensure
a few minimum standards for contributions. The contribution should not break the build in any way
and ideally should be of reasonably good quality and not contain any bugs. The idea here is not
to expect perfection or make it difficult for anyone to contribute, but contributors should be
comfortable showing their code to their peers. It is also necessary to stop any individual
from completely dominating the code to the exclusion of others. The best way to meet these
objectives is through peer code reviews, open to anyone involved in the project.

Every pull request is expected to undergo a full code review. Participants should include
. at least two reviewers other than the author
. all of the previos contributors to that code being reviewed
. anyone else who wants to join the review

Code reviews are normally open for 5 calendar days and all reviewers should have completed their review
within that time.

At the end of the review, a moderator will decide whether it has been successful or not. A review that
throws up any defects will not be successful.

Once a pull request has a successful review, it becomes eligible to be merged into the master branch.

Code reviews are provided by Atlassian Fisheye+Crucible and are carried out at:
http://fisheye.teamserver.tigranetworks.co.uk/
(you need a Jira login to access this)

Lightweight code reviews can also be carried out directly on the pull request in Stash, but for a
commit of any appreciable size it will be better to use the Fisheye server.


Developer Tools
===============

We expect to be able to provide free licenses for certain productivity tools. I'll add the
license codes here as and when I obtain them.

Developer Support
=================

In case of any difficulty with any of the developer tools, or if you have any questions about anything
contained in this document, please email Support@tigranetworks.co.uk - this should be your first
port of call for all technical issues and queries.