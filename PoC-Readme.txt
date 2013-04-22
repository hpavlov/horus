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