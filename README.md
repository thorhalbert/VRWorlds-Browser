# VRWorlds-Browser
VRWorlds World Browser (in Unity3D)

The browser presents a first person view upon the user's Avatar into a World.   

The browser is designed with some of the features of a regular web browser.  A web browser contains a DOM and a javascript engine for each page.

In VRWorlds, each type of Entity--the World, the Avatar, and all of the various Entities, and the Security (Kudo) server--have their own javascript code-engines, and they share a series of v8 javascript engines.   Each one of these interact with the 3DOM, a 3d abstraction foreach entity.   The browser composits alll of these together to form the experience for the user, as a VR or conventional gamespace.

The Code-engines can be javascript, but the intention is for it to be webassembly driven in some better language.   The Entity is at one end of the converstation, at the other end (potentially) is a server which can control an entities operation.   An Entity which is a brick needs no backend, whereas your Avatar could be very complex.  I am intending to write the first versions of this in C# (mono LLVM like the Blazor tools).   Serverside will initially be C# .NET Core (ASP.NET under kubernetes/docker).  Though someday there may be Go and Python too.

There are several V8 engines which are allocated to different tasks:

* The World engine - also contains any entities from the same manufacturer
* The Avatar engine
* Associated Entity - these are controllers for entities which are assocated or possessed by the Avatar
* Entity Server - contains all of the other entities.

There can be more than one of each of these servers as needed.   I would love to be able to do the multi-process operations like the Chrome browser does (I have to figure that out).

![image](https://1.bp.blogspot.com/-byA4W1zU3ss/Xa-r_CjOI5I/AAAAAAAAMWE/3IbWOIYmyR8n-R2qqimmxJUxIjWRA7pfQCLcBGAsYHQ/s800/loader.JPG)

