# QuickImgShare
A tool to quickly share images via Imgur.

This Windows tool is my first attempt to communicate with API's with OAuth 2 authentication. The main goal is to quickly upload images to Imgur to share them with friends.

Features:
* Drag and drop a jpg image on the application to upload it to imgur privately. The application displays a link to access the image.

Planned Features/Improvements:
* Option to configure the session id?
* png support
* download individual images
* download albums
* refactor structure with design patterns in mind, split of features
* general code rework

Open Bugfixes/Issues:
* Should not reuse HttpClient
* Dragging wrong file type on the application results in error

How to get info on where to upload it to? session id in config?
