# herding-cats-unity

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
