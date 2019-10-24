# Battlestar Galactica Online Private Server

<p align="center">
  <img width="460" height="300" src="https://vignette.wikia.nocookie.net/bsgoguide/images/2/2f/BGO_Logo_Glow.png/revision/latest?cb=20140128015211">
</p>

#### None of the repo, the tool, nor the repo owner is affiliated with, or sponsored or authorized by, BigPoint or its affiliates.
##### This server is for learning more about C# and Networking. I am not responsible for other people's Forks.
###### Feel free to open Issues or do Pull requests.

### Features
- Supported version:
  - Latest released by BigPoint. I'm not sure if it works for other versions.
- What's working:
  - Select a faction.
  - Customize the male Colonial character.
  - Load into the game.
- What comes next:
  - Movement?
  - Shop?
  - Show the correct ship slots?
  - Skills?
  - Multiplayer?
  - Dock/Undock?
  - Jump to another Sector?
- Known bugs:
  - Can't load the Cylon customization.
  - Can't load the female Colonial customization.
  - There's no tutorial scene.
  - There are no missions, skills, shop items etc.
  - There's a placeholder on the ranking system.
  - Chat doesn't work.
  - Server isn't aware of player positions/eulers.
  - Player 1 sees when Player 2 joins, but Player 2 doesn't see Player 1.
  - Not always you can join the game, crashes on Tick 0 and no Players.
  - Not always Player 2 can join the game for Player 1, Cards and Position doesn't work.
  
### Usage

#### Requeriments
- Visual Studio (2019 is recommended). There is no build for the server yet.
- MongoDB (https://www.mongodb.com/download-center/community).
- Battlestar Galactica Online files.
  
### How to use
#### First Time
- Add the following directory to your PATH:
> %PathToYourMongoFolder%\Server\4.2\bin
- Create the following directory:
> C:\data\db

#### Then you should be able to do the following without repeating the previous steps:
- Open a cmd and run the Mongo Daemon:
> mongod
- Run the server, open a cmd in your BSGO/client/live folder and run:
> bsgo.exe +projectID 547 +userID 5085935 +sessionID c7faac2379e35f6404eced5f484210ba +trackingID 6cc3a6e78a753f29ccabaa0f79b7041b +gameServer 127.0.0.1 +cdn C:\Program Files (x86)\BSGO/client/live/ +language en +session b1b23d2fa2769bd59d4c1b67554599b88381afd653b156aa54cb689969ab4fb7 +version 3b27980a3b7dd77e597872106ca98000 
