webim-for-dotnet4
====================

webim plugin for dotnet mvc3

Demo
====

1. Import 'WebimPlugin' Project to Visual Web Developer 2010. (Notice: MVC3 Required)

2. Build and Run

Developer Guide
===============

Create Database
---------------

import install.sql if your database is MySQL


Coding WebimPlugin.cs
-----------------------

implements these methods:

```dotnet

public WebimEndpoint Endpoint();

public IEnumerable<WebimEndpoint> Buddies(string uid);

public IEnumerable<WebimEndpoint> BuddiesByIds(string uid, string[] ids);

public WebimRoom findRoom(string roomId);

public IEnumerable<WebimRoom> Rooms(String uid);

public IEnumerable<WebimRoom> RoomsByIds(String uid, String[] ids);

public IEnumerable<WebimMember> Members(string roomId);

public IEnumerable<WebimNotification> Notifications(string uid);

public IEnumerable<WebimMenu> Menu(string uid);


```

Coding WebimModel.cs
-----------------------

implements these methods to access database:

```dotnet

public IEnumerable<WebimHistory> Histories(string uid, string with, string type = "chat", int limit = 50);

public IEnumerable<WebimHistory> OfflineHistories(string uid, int limit = 50);

public void InsertHistory(string uid, WebimMessage msg);

public void ClearHistories(string uid, string with);

public void OfflineHistoriesReaded(string uid);

public string GetSetting(string uid);

public void SaveSetting(string uid, string data);

```

Coding WebimConfig.cs
----------------------

You should change the WebimConfig.java, and load configurations from database or xml.

Webim Javascript
-----------------------

Insert Javascript code below to web pages that need to display Webim:

	<script type="text/javascript" src="/Webim/Boot"></script>

