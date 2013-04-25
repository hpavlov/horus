The initial idea was to test the versioning in a number of cases including:

- Connecting with V1 client to V1 interface
- Connecting with V1 client to V2 interface
- Connecting with V2 client to V1 interface
- Connecting with V2 client to V2 interface

However in order to do a real test I needed to either add too much noise in my class implementation which will be a PoC but will make everything hard to understand, or I needed to create separate assemblies (projects) for each of the versions which would have added too much noise in the source control.

Because the intention is to try and reuse parts of this PoC in the solution I deciced to only demonstrate local and remote connectivity using a V1 client and connecting to V1 and V2 drivers. In order to implement a V2 client the following needs to be done:

- Implement the ICameraV2 memebers in the HorusCamera class
- Add the corresponding calls in the HorusClientApp to try calling V1 and V2 drivers. Note that you will not be able to call a V2 method on a V1 client simply because the API will not allow you to create an instance of such a client because the returned Devices and Drivers are specific about which version they support and implement.

------------------

Other notes:

- There is currently no Authentication implemented in the Horus.Server
- No session management is done

-------------------

To install Horus.Server run \Horus.Server\install.bat with admin account from a VS.NET command prompts. To rebuild it (including starting and stopping the service) run \Horus.Server\bhs.bat with admin account (bhs = build horus server)

-------------------

Final notes: I expect the majority of this initial code to be completely redone so only use it as a starting point for a further more specific discussion.

-------------------

HOW TO MAKE THE VIDEO DRIVER/CLIENT WORK ON LOCAL CONNECTION:

- Download and build the DirectShowVideoCapture driver from here: http://stash.teamserver.tigranetworks.co.uk/scm/horus/video.directshowvideocapture.git

- Choose a location for the test bed for the Horus system (e.g. I have C:\Horus). Then create a system environment variable called HORUS_HOME and set it to your test bed location

- Create a sub folder called Drivers and then DirectShowVideoCapture inside it. e.g. you will have a folder called C:\Horus\Drivers\DirectShowVideoCapture. Now copy all files from the DirectShowVideoCapture driver to this place. Those files should be: DirectShowVideoCapture.dll, DirectShowLib-2005.dll and Koyash.VideoUtilities.Native.dll

- Now build the Horus solution

- Run the Horus.Configurator. Move to the second tab called "Logical Devices" and press the "Search for Attached Devices" button. If there are any video cameras attached to your system you should see them listed at the top and they will have [Available] [Not Configured] at the end. Select the camera you want to use and click "Configure Driver". You can simply press "OK" for this. Now the device should show as "[Available] [Configured]"

- Close the Horus.Configurator and run the HorusClientApp. Move to the "Video Client" tab and press "List Devices". You should see the camera you configured being selected in the dropdown and the button will change its text to "Connect". Press the "Connect" button. You should see video coming through.
