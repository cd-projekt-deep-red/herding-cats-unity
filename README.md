# Herding Cats

[Global Game Jam 2021 project site](https://globalgamejam.org/2021/games/herding-cats-0)

In this zany game you well... Herd Cats! After a cat delivery man crashes on the highway you must scramble to wrangle your lost cats as they sprint away from you! Play is straightforward and simple but surprisingly addictive!

[**Play the game in your browser!**](https://cd-projekt-deep-red.github.io/herding-cats-webgl/)

## Build
Requires Unity 2019.4.14f1.
Configure project to build for WebGL.

## Deploy
Builds are (manually) published and deployed by GitHub pages through another repository: https://github.com/cd-projekt-deep-red/herding-cats-webgl.

In Unity, build to an empty directory.

```
cd <build dir>
git init
git add .
git commit
git remote add origin https://github.com/cd-projekt-deep-red/herding-cats-webgl.git
git pull --rebase origin master -X ours
git push -u origin master
```
